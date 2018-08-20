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

using System;

namespace Downloader
{
    /// <summary>
    /// Enumeration for the download states.
    /// </summary>
    public enum State
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,
        /// <summary>
        /// Ready to download.
        /// </summary>
        Ready,
        /// <summary>
        /// Queued to start downloading.
        /// </summary>
        Queued,
        /// <summary>
        /// Currently downloading.
        /// </summary>
        Downloading,
        /// <summary>
        /// The download is paused and can be resumed.
        /// </summary>
        Paused,
        /// <summary>
        /// The download is completed.
        /// </summary>
        Completed,
        /// <summary>
        /// The download failed.
        /// </summary>
        Failed,
        /// <summary>
        /// A user cancels the download request.
        /// </summary>
        Canceled
    } 

    /// <summary>
    /// Interface to call Tizen.Content.Download
    /// </summary>
    public interface IDownload
    {
        event EventHandler<DownloadStateChangedEventArgs> DownloadStateChanged;
        event EventHandler<DownloadProgressEventArgs> DownloadProgress;

        string GetContentName();
        ulong GetContentSize();
        string GetDownloadedPath();
        string GetMimeType();
        int GetDownloadState();
        string GetUrl();
        
        void StartDownload(String url);
        void Dispose();
        void DownloadLog(String msg);
    }

    /// <summary>
    /// An extended EventArgs class for download state
    /// </summary>
    public class DownloadStateChangedEventArgs : EventArgs
    {
        public String stateMsg = "";
        
        public DownloadStateChangedEventArgs(String msg)
        {
            stateMsg = msg;
        }
    }

    /// <summary>
    /// An extended EventArgs class for download progress
    /// </summary>
    public class DownloadProgressEventArgs : EventArgs
    {
        public ulong ReceivedSize = 0;
        public DownloadProgressEventArgs(ulong size)
        {
            ReceivedSize = size;
        }
    }
}
