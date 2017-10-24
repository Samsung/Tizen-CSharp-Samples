/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
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

using Downloader.Tizen.Mobile;
using System;
using Tizen;
using Tizen.Content.Download;

[assembly: Xamarin.Forms.Dependency(typeof(DownloadImplementation))]

namespace Downloader.Tizen.Mobile
{
    /// <summary>
    /// Implementation class of IDownload interface
    /// </summary>
    public class DownloadImplementation : IDownload
    {
        public event EventHandler<DownloadStateChangedEventArgs> DownloadStateChanged;
        public event EventHandler<DownloadProgressEventArgs> DownloadProgress;

        private Request req = new Request("empty");
  
        /// <summary>
        /// Register event handler and call Request.Start() to start download
        /// </summary>
        /// <param name="url">The URL to download</param>
        public void StartDownload(string url)
        {
            req.Url = url;
            req.StateChanged += StateChanged;
            req.ProgressChanged += ProgressChanged;
            req.Start();
        }

        /// <summary>
        /// Get the URL to download
        /// </summary>
        /// <returns>The URL</returns>
        public string GetUrl()
        {
            if (req.Url == "empty")
            {
                return "";
            }

            return req.Url;
        }

        /// <summary>
        /// Get the content name of the downloaded file 
        /// </summary>
        /// <returns>The content name</returns>
        public string GetContentName()
        {
            if (req.Url == "empty")
            {
                return "";
            }

            return req.ContentName;
        }

        /// <summary>
        /// Get the content size of the downloaded file
        /// </summary>
        /// <returns>The content size</returns>
        public ulong GetContentSize()
        {
            if (req.Url == "empty")
            {
                return 0;
            }

            return req.ContentSize;
        }

        /// <summary>
        /// Get the downloaded path
        /// </summary>
        /// <returns>The downloaded path</returns>
        public string GetDownloadedPath()
        {
            if (req.Url == "empty")
            {
                return "";
            }

            return req.DownloadedPath;
        }
        
        /// <summary>
        /// Get the MIME type of the downloaded content
        /// </summary>
        /// <returns>The MIME type</returns>
        public string GetMimeType()
        {
            if (req.Url == "empty")
            {
                return "";
            }

            return req.MimeType;
        }

        /// <summary>
        /// Get the download state
        /// </summary>
        /// <returns>The download state</returns>
        public int GetDownloadState()
        {
            return (int)req.State;
        }

        /// <summary>
        /// Print log
        /// </summary>
        /// <param name="msg">Log message</param>
        public void DownloadLog(String msg)
        {
            Log.Info("DOWNLOADER", msg);
        }

        /// <summary>
        /// Event handler for download state.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">State changed event arguments</param>
        private void StateChanged(object sender, StateChangedEventArgs e)
        {
            String stateMsg = "";

            switch (e.State)
            {
                /// Download is ready.
                case DownloadState.Ready:
                    stateMsg = "Ready";
                    break;
                /// Content is downloading.
                case DownloadState.Downloading:
                    stateMsg = "Downloading";
                    break;
                /// Download is completed.
                case DownloadState.Completed:
                    stateMsg = "Completed";
                    break;
                default:
                    stateMsg = "";
                    break;
            }

            DownloadStateChanged(sender, new DownloadStateChangedEventArgs(stateMsg));
        }

        /// <summary>
        /// Event handler for download progress
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Progress changed event arguments</param>
        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ReceivedDataSize > 0)
            {
                DownloadProgress(sender, new DownloadProgressEventArgs(e.ReceivedDataSize));
            }
        }
    }
}