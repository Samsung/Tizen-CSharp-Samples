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

using Downloader;
using System;
using Tizen;
using Tizen.Content.Download;

[assembly: Xamarin.Forms.Dependency(typeof(Download))]

namespace Downloader
{
    /// <summary>
    /// Implementation class of IDownload interface
    /// </summary>
    public class Download : IDownload
    {
        public event EventHandler<DownloadStateChangedEventArgs> DownloadStateChanged;
        public event EventHandler<DownloadProgressEventArgs> DownloadProgress;

        private Request req;

        // Flag to check download is started or not
        private bool is_started = false;

        /// <summary>
        /// Register event handler and call Request.Start() to start download
        /// </summary>
        /// <param name="url">The URL to download</param>
        public void StartDownload(string url)
        {
            // Create new Request with URL
            req = new Request(url);
            // Register state changed event handler
            req.StateChanged += StateChanged;
            // Register progress changed event handler
            req.ProgressChanged += ProgressChanged;
            is_started = true;
            // Start download content
            req.Start();
        }

        /// <summary>
        /// Get the URL to download
        /// </summary>
        /// <returns>The URL</returns>
        public string GetUrl()
        {
            if (!is_started)
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
            if (!is_started)
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
            if (!is_started)
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
            if (!is_started)
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
            if (!is_started)
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
            if (!is_started)
            {
                return 0;
            }

            return (int)req.State;
        }

        /// <summary>
        /// Dispose the request
        /// </summary>
        public void Dispose()
        {
            if (is_started)
            {
                req.Dispose();
                is_started = false;
            }
        }

        /// <summary>
        /// Show log message
        /// </summary>
        /// <param name="message">Log message</param>
        public void DownloadLog(String message)
        {
            // Show received message as DLOG
            Log.Info("DOWNLOADER", message);
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
                /// Download is failed.
                case DownloadState.Failed:
                    stateMsg = "Failed";
                    break;
                default:
                    stateMsg = "";
                    break;
            }

            // Send current state to event handler
            DownloadStateChanged(sender, new DownloadStateChangedEventArgs(stateMsg));
        }

        /// <summary>
        /// Event handler for download progress
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Progress changed event arguments</param>
        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // If received data is exist, send data size to event handler
            if (e.ReceivedDataSize > 0)
            {
                DownloadProgress(sender, new DownloadProgressEventArgs(e.ReceivedDataSize));
            }
        }
    }
}