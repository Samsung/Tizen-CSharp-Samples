/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
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
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Tizen;
using Tizen.Network.Connection;
using Tizen.System;
using Xamarin.Forms;

namespace WebProxySample
{
    class MainPageModel : INotifyPropertyChanged
    {
        // The model name of Tizen Emulator
        public const string Tizen_Emulator = "Emulator";
        string DownloadsFolder;
        string ModelName;
        WebClient webClient;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainPageModel()
        {
            LabelText = "Make sure your watch is connected to mobile phone through Bluetooth.";
            CanDownload = false;
            CanGetData = true;
            DownloadInfo = "File is not downloaded yet.";
            DownloadsFolder = Path.Combine(Tizen.Applications.Application.Current.DirectoryInfo.Data, "Downloads");
            // the key to get the device's model name
            Information.TryGetValue<string>("http://tizen.org/system/model_name", out ModelName);
        }

        /// <summary>
        /// Result text
        /// </summary>
        string _label;
        public string LabelText
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        /// <summary>
        /// Info text about downloading the file
        /// </summary>
        string _downloadInfo;
        public string DownloadInfo
        {
            get => _downloadInfo;
            set => SetProperty(ref _downloadInfo, value);
        }

        /// <summary>
        /// Indicate whether downloading a file can be started or not.
        /// </summary>
        bool _canDownload;
        public bool CanDownload
        {
            get => _canDownload;
            set => SetProperty(ref _canDownload, value);
        }

        /// <summary>
        /// Indicate whether getting some web data can be started or not.
        /// </summary>
        bool _canGetData;
        public bool CanGetData
        {
            get => _canGetData;
            set => SetProperty(ref _canGetData, value);
        }

        /// <summary>
        /// Command to download a content file
        /// </summary>
        public ICommand DownloadContentCommand => new Command(DownloadContent);

        /// <summary>
        /// Download a File Asynchronously
        /// </summary>
        void DownloadContent()
        {
            CanDownload = false;
            DownloadInfo = "Start downloading";

            // If downloads folder exists, delete it.
            if (Directory.Exists(DownloadsFolder))
            {
                Directory.Delete(DownloadsFolder, true);
            }

            // Create the Downloads folder.
            Directory.CreateDirectory(DownloadsFolder);

            try
            {
                webClient = new WebClient();

                ConnectionItem currentConnection = ConnectionManager.CurrentConnection;
                Log.Info(Program.LOG_TAG, "Connection(" + currentConnection.Type + ", " + currentConnection.State + ")");
                LabelText = "Connection(" + currentConnection.Type + ", " + currentConnection.State + ")\n";

                if (currentConnection.Type == ConnectionType.Disconnected)
                {
                    Log.Info(Program.LOG_TAG, "There's no available data connectivity!!");
                    DownloadInfo = "There's no available data connectivity for downloading.";
                    return;
                }
                else if (currentConnection.Type == ConnectionType.Ethernet)
                {
                    // For Tizen Emulator, it is not necessary to set up web proxy.
                    // It's for Samsung Galaxy Watch which is paired with the mobile phone.
                    if (string.Compare(Tizen_Emulator, ModelName) != 0)
                    {
                        // Use web proxy
                        string proxyAddr = ConnectionManager.GetProxy(AddressFamily.IPv4);
                        WebProxy myproxy = new WebProxy(proxyAddr, true);
                        // Set proxy information to be used by WebClient
                        webClient.Proxy = myproxy;
                    }
                }

                webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
                webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;

                string pathToNewFile = Path.Combine(DownloadsFolder, Path.GetFileName("https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4"));
                // Download a file asynchronously
                webClient.DownloadFileAsync(new Uri("https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4"), pathToNewFile);
            }
            catch (Exception ex)
            {
                Log.Error(Program.LOG_TAG, "[DownloadContent] Error: " + ex.Message);
                if (webClient != null)
                {
                    webClient.Dispose();
                }
            }
        }

        /// <summary>
        /// Called when an asynchronous download operation successfully transfers some or all of the data.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">DownloadProgressChangedEventArgs</param>
        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // Displays the operation identifier, and the transfer progress.
            //Log.Info(Program.LOG_TAG, "[WebClient_DownloadProgressChanged] ("
            //    + e.BytesReceived + ") bytes, (" + e.ProgressPercentage + ")%, ("
            //    + e.TotalBytesToReceive + ") total bytes, user state: " + e.UserState);
            DownloadInfo = e.BytesReceived / 1024 + " KB";
            if (e.ProgressPercentage == 100)
            {
                DownloadInfo += " Done.";
            }
        }

        /// <summary>
        /// Called when an asynchronous file download operation completes.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">AsyncCompletedEventArgs</param>
        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Log.Info(Program.LOG_TAG, "[WebClient_DownloadFileCompleted] Cancelled: "
                + e.Cancelled + ")");
            if (e.Cancelled)
            {
                DownloadInfo += "\n[WebClient_DownloadFileCompleted] cancelled.\n";
            }

            if (e.Error != null)
            {
                Log.Error(Program.LOG_TAG, "[WebClient_DownloadFileCompleted] Error : " + e.Error.GetType() + ", " + e.Error.Message);
                DownloadInfo += "\n[WebClient_DownloadFileCompleted] Error : " + e.Error.GetType() + ", " + e.Error.Message;
            }
            webClient.Dispose();
        }

        /// <summary>
        /// Command to get web data
        /// </summary>
        public ICommand GetDataCommand => new Command(GetData);

        void GetData()
        {
            CanGetData = false;
            try
            {
                ConnectionItem currentConnection = ConnectionManager.CurrentConnection;
                Log.Info(Program.LOG_TAG, "Connection(" + currentConnection.Type + ", " + currentConnection.State + ")");
                LabelText = "Connection(" + currentConnection.Type + ", " + currentConnection.State + ")\n";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://samsung.github.io/Tizen.NET/");
                // When watch is paired with a mobile device, we can use WebProxy.
                if (currentConnection.Type == ConnectionType.Disconnected)
                {
                    Log.Info(Program.LOG_TAG, "There's no available data connectivity!!");
                    LabelText = "There's no available data connectivity!!";
                    return;
                }
                else if (currentConnection.Type == ConnectionType.Ethernet)
                {
                    // For Tizen Emulator, it is not necessary to set up web proxy.
                    // It's for Samsung Galaxy Watch which is paired with the mobile phone.
                    if (string.Compare(Tizen_Emulator, ModelName) != 0)
                    {
                        string proxyAddr = ConnectionManager.GetProxy(AddressFamily.IPv4);
                        WebProxy myproxy = new WebProxy(proxyAddr, true);
                        // Set proxy information for the HttpWebRequest
                        request.Proxy = myproxy;
                    }
                }

                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Log.Info(Program.LOG_TAG, "HttpWebResponse : status - " + ((HttpWebResponse)response).StatusDescription);
                LabelText += "\nHttpWebResponse : status - " + ((HttpWebResponse)response).StatusDescription + "\n";
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content and print log.
                LabelText += responseFromServer;
                Log.Info(Program.LOG_TAG, "responseFromServer :" + responseFromServer);
                // Clean up the streams and the response.
                reader.Close();
                response.Close();
            }
            catch (Exception e)
            {
                Log.Error(Program.LOG_TAG, "An error occurs : " + e.GetType() + " , " + e.Message);
                LabelText += "An error occurs : " + e.GetType() + " , " + e.Message;
            }

            CanDownload = true;
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Called to notify that a change of property happened
        /// </summary>
        /// <param name="propertyName">The name of the property that changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
