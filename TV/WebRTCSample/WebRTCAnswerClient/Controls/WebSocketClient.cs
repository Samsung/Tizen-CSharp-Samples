/*
* Copyright (c) 2021 Samsung Electronics Co., Ltd All Rights Reserved
*
* Licensed under the Apache License, Version 2.0 (the License);
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an AS IS BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tizen;
using System.Net.WebSockets;

namespace WebRTCAnswerClient
{
    public class WebSocketClient : IDisposable
    {
        private ClientWebSocket _clientWebSocket;

        public WebSocketClient()
        {
            _clientWebSocket = new ClientWebSocket();
            _clientWebSocket.Options.Proxy = null;
        }

        ~WebSocketClient()
        {
            Dispose(false);
        }

        public async Task ConnectAsync(string uri) => await ConnectAsync(uri, CancellationToken.None);

        public async Task ConnectAsync(string uri, CancellationToken token)
        {
            await _clientWebSocket.ConnectAsync(new Uri(uri), token);
        }

        public async Task SendAsync(string message) => await SendAsync(message, CancellationToken.None);

        public async Task SendAsync(string message, CancellationToken token)
        {
            Log.Info(WebRTCLog.Tag, $"Enter - Msg:{message}");

            byte[] sendBytes = Encoding.UTF8.GetBytes(message);
            var sendBuffer = new ArraySegment<byte>(sendBytes);

            await _clientWebSocket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, token);

            Log.Info(WebRTCLog.Tag, "Leave");
        }

        public async Task<string> ReceiveAsync() => await ReceiveAsync(CancellationToken.None);

        public async Task<string> ReceiveAsync(CancellationToken token)
        {
            Log.Info(WebRTCLog.Tag, "Enter");

            var rcvBytes = new byte[100000];
            var rcvBuffer = new ArraySegment<byte>(rcvBytes);

            var rcvResult = await _clientWebSocket.ReceiveAsync(rcvBuffer, token);
            byte[] msgBytes = rcvBuffer.Skip(rcvBuffer.Offset).Take(rcvResult.Count).ToArray();
            string rcvMsg = Encoding.UTF8.GetString(msgBytes);

            Log.Info(WebRTCLog.Tag, $"Leave - Message:{rcvMsg}");

            return rcvMsg;
        }

        #region Dispose pattern support
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // to be used if there are any other disposable objects
                    _clientWebSocket?.Dispose();
                }

                _disposed = true;
            }
        }
        #endregion Dispose pattern support
    }
}