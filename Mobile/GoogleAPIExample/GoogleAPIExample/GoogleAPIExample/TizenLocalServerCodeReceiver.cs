/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Logging;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleAPIExample
{
    public interface IEmbeddedBrowser
    {
        event EventHandler Cancelled;
        void Start(string url);
    }

    class ConsoleLogger : ILogger
    {
        public bool IsDebugEnabled => false;

        public void Debug(string message, params object[] formatArgs)
        {
            Console.WriteLine(message, formatArgs);
        }

        public void Error(Exception exception, string message, params object[] formatArgs)
        {
            Debug(message, formatArgs);
        }

        public void Error(string message, params object[] formatArgs)
        {
            Debug(message, formatArgs);
        }

        public ILogger ForType(Type type)
        {
            return this;
        }

        public ILogger ForType<T>()
        {
            return this;
        }

        public void Info(string message, params object[] formatArgs)
        {
            Debug(message, formatArgs);
        }

        public void Warning(string message, params object[] formatArgs)
        {
            Debug(message, formatArgs);
        }
    }


    public class TizenLocalServerCodeReceiver : ICodeReceiver
    {
        private static readonly ILogger Logger = new ConsoleLogger();

        /// <summary>The call back request path.</summary>
        private const string LoopbackCallbackPath = "/authorize/";

        /// <summary>127.0.0.1 callback URI, expects a port parameter.</summary>
        private static readonly string CallbackUriTemplate127001 = $"http://127.0.0.1:{{0}}{LoopbackCallbackPath}";

        /// <summary>Close HTML tag to return the browser so it will close itself.</summary>
        private const string DefaultClosePageResponse =
@"<html>
  <head><title>OAuth 2.0 Authentication Token Received</title></head>
  <body>
    Received verification code. You may now close this window.
    <script type='text/javascript'>
      // This doesn't work on every browser.
      window.setTimeout(function() {
          this.focus();
          window.opener = this;
          window.open('', '_self', ''); 
          window.close(); 
        }, 1000);
      //if (window.opener) { window.opener.checkToken(); }
    </script>
  </body>
</html>";

        /// <summary>
        /// Create an instance of <see cref="TizenLocalServerCodeReceiver"/>.
        /// </summary>
        public TizenLocalServerCodeReceiver() : this(DefaultClosePageResponse) { }

        /// <summary>
        /// Create an instance of <see cref="TizenLocalServerCodeReceiver"/>.
        /// </summary>
        /// <param name="closePageResponse">Custom close page response for this instance</param>
        public TizenLocalServerCodeReceiver(string closePageResponse)
        {
            _closePageResponse = closePageResponse;
        }

        // Close page response for this instance.
        private readonly string _closePageResponse;

        public IEmbeddedBrowser EmbeddedBrowser { get; set; }


        // There is a race condition on the port used for the loopback callback.
        // This is not good, but is now difficult to change due to RedirectUri and ReceiveCodeAsync
        // being public methods.

        private string redirectUri;
        /// <inheritdoc />
        public string RedirectUri
        {
            get
            {
                if (string.IsNullOrEmpty(redirectUri))
                {
                    redirectUri = string.Format(CallbackUriTemplate127001, GetRandomUnusedPort());
                }
                return redirectUri;
            }
        }

        /// <inheritdoc />
        public async Task<AuthorizationCodeResponseUrl> ReceiveCodeAsync(AuthorizationCodeRequestUrl url,
            CancellationToken taskCancellationToken)
        {
            var authorizationUrl = url.Build().AbsoluteUri;
            // The listener type depends on platform:
            // * .NET desktop: System.Net.HttpListener
            // * .NET Core: LimitedLocalhostHttpServer (above, HttpListener is not available in any version of netstandard)
            using (var listener = StartListener())
            {
                Logger.Debug("Open a browser with \"{0}\" URL", authorizationUrl);
                bool browserOpenedOk;
                try
                {
                    browserOpenedOk = OpenBrowser(authorizationUrl);
                }
                catch (Exception e)
                {
                    Logger.Error(e, "Failed to launch browser with \"{0}\" for authorization", authorizationUrl);
                    throw new NotSupportedException(
                        $"Failed to launch browser with \"{authorizationUrl}\" for authorization. See inner exception for details.", e);
                }
                if (!browserOpenedOk)
                {
                    Logger.Error("Failed to launch browser with \"{0}\" for authorization; platform not supported.", authorizationUrl);
                    throw new NotSupportedException(
                        $"Failed to launch browser with \"{authorizationUrl}\" for authorization; platform not supported.");
                }

                return await GetResponseFromListener(listener, taskCancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>Returns a random, unused port.</summary>
        private static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            try
            {
                listener.Start();
                return ((IPEndPoint)listener.LocalEndpoint).Port;
            }
            finally
            {
                listener.Stop();
            }
        }
        private HttpListener StartListener()
        {
            var listener = new HttpListener();
            listener.Prefixes.Add(RedirectUri);
            listener.Start();
            return listener;
        }

        private async Task<AuthorizationCodeResponseUrl> GetResponseFromListener(HttpListener listener, CancellationToken ct)
        {
            HttpListenerContext context;
            // Set up cancellation. HttpListener.GetContextAsync() doesn't accept a cancellation token,
            // the HttpListener needs to be stopped which immediately aborts the GetContextAsync() call.
            using (ct.Register(listener.Stop))
            {
                // Wait to get the authorization code response.
                try
                {
                    context = await listener.GetContextAsync().ConfigureAwait(false);
                }
                catch (Exception) when (ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                    // Next line will never be reached because cancellation will always have been requested in this catch block.
                    // But it's required to satisfy compiler.
                    throw new InvalidOperationException();
                }
            }
            NameValueCollection coll = context.Request.QueryString;

            // Write a "close" response.
            var bytes = Encoding.UTF8.GetBytes(_closePageResponse);
            context.Response.ContentLength64 = bytes.Length;
            context.Response.SendChunked = false;
            context.Response.KeepAlive = false;
            var output = context.Response.OutputStream;
            await output.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
            await output.FlushAsync().ConfigureAwait(false);
            output.Close();
            context.Response.Close();

            // Create a new response URL with a dictionary that contains all the response query parameters.
            return new AuthorizationCodeResponseUrl(coll.AllKeys.ToDictionary(k => k, k => coll[k]));
        }

        private bool OpenBrowser(string url)
        {
            EmbeddedBrowser.Start(url);
            return true;
        }
    }
}
