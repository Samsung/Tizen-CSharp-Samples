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
using Xamarin.Forms;

namespace Downloader
{
    /// <summary>
    ///  A class for download main page.
    /// </summary>
    class DownloadMainPage : ContentPage
    {
        private View urlEntryView;
        private EntryCell urlEntryCell;
        private Button downloadButton;
        private ProgressBar progressBar;
        private Label progressLabel;
        private string downloadUrl;

        /// <summary>
        /// Constructor.
        /// </summary>
        public DownloadMainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize main page.
        /// Add components and events.
        /// </summary>
        private void InitializeComponent()
        {
            Title = "Download";
            IsVisible = true;

            BackgroundColor = Color.White;

            urlEntryCell = CreateEntryCell();
            urlEntryView = CreateEntryView();
            downloadButton = CreateDownloadButton();
            progressBar = CreateProgressbar();
            progressLabel = CreateProgressLabel();

            AddEvent();

            Content = new StackLayout
            {
                Spacing = 0,
                Children =
                {
                   urlEntryView,
                   downloadButton,
                   progressBar,
                   progressLabel,
                }
            };
        }

        /// <summary>
        /// Create a new EntryCell to get URL.
        /// </summary>
        /// <returns>EntryCell</returns>
        private EntryCell CreateEntryCell()
        {
            return new EntryCell()
            {
                Label = "URL: ",
                Placeholder = "Type URL here to download",
                Height = 400,
                Keyboard = Keyboard.Url,
            };
        }

        /// <summary>
        /// Create a new EntryView.
        /// </summary>
        /// <returns>EntryView</returns>
        private View CreateEntryView()
        {
            return new TableView()
            {
                HeightRequest = 200,
                Intent = TableIntent.Form,
                Margin = new Thickness(10, 10),
                VerticalOptions = LayoutOptions.Start,
                Root = new TableRoot
                {
                    new TableSection
                    {
                        urlEntryCell
                    },
                }
            };
        }

        /// <summary>
        /// Create a new Button to start download.
        /// </summary>
        /// <returns>Button</returns>
        private Button CreateDownloadButton()
        {
            return new Button()
            {
                Text = "Start Download",
                Font = Font.SystemFontOfSize(NamedSize.Default),
                FontAttributes = FontAttributes.Bold,
                BorderWidth = 1,
                VerticalOptions = LayoutOptions.Start,
            };
        }

        /// <summary>
        /// Create a new ProgressBar to present download state.
        /// </summary>
        /// <returns><ProgressBar</returns>
        private ProgressBar CreateProgressbar()
        {
            return new ProgressBar()
            {
                HeightRequest = 100,
                Progress = .0,
                VerticalOptions = LayoutOptions.Start,
            };
        }

        /// <summary>
        /// Create a new Label to present receiving data size in bytes.
        /// </summary>
        /// <returns>Label</returns>
        private Label CreateProgressLabel()
        {
            return new Label()
            {
                BackgroundColor = Color.Transparent,
                HorizontalTextAlignment = TextAlignment.End,
                Text = "0 bytes / 0 bytes",
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Start,
            };
        }

        /// <summary>
        /// Register event handlers for entry, button and state callback.
        /// </summary>
        private void AddEvent()
        {
            downloadButton.Clicked += OnButtonClicked;
            DependencyService.Get<IDownload>().DownloadStateChanged += OnStateChanged;
            DependencyService.Get<IDownload>().DownloadProgress += OnProgressbarChanged;
        }

        /// <summary>
        /// Event handler when download state is changed.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments including download state</param>
        private void OnStateChanged(object sender, DownloadStateChangedEventArgs e)
        {
           if (e.stateMsg.Length <= 0)
                return;

           DependencyService.Get<IDownload>().DownloadLog("State: " + e.stateMsg);
           Device.BeginInvokeOnMainThread (() =>
           {
              if (e.stateMsg == "Failed")
              {
                  downloadButton.Text = e.stateMsg + "! Please start download again.";
                  // If download is failed, dispose a request
                  DependencyService.Get<IDownload>().Dispose();
                  // Enable a donwload button
                  downloadButton.IsEnabled = true;
              }
              else if (e.stateMsg != downloadButton.Text)
              {
                  downloadButton.Text = e.stateMsg;
              }
          });
        }

        /// <summary>
        /// Event handler when data is received.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments including received data size</param>
        private void OnProgressbarChanged(object sender, DownloadProgressEventArgs e)
        {
            if (e.ReceivedSize <= 0)
                return;

            ulong ContentSize =  DependencyService.Get<IDownload>().GetContentSize();
            Device.BeginInvokeOnMainThread (() =>
            {
                progressBar.Progress = (double)(e.ReceivedSize / ContentSize);
                progressLabel.Text = e.ReceivedSize + "bytes / " + ContentSize + "bytes";
            });
        }

        /// <summary>
        /// Event handler when button is clicked.
        /// Once a button is clicked and download is started, a button is disabled.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void OnButtonClicked(object sender, EventArgs e)
        {
            downloadUrl = urlEntryCell.Text;

            DependencyService.Get<IDownload>().DownloadLog("Start Download: " + downloadUrl);
            try
            {
                // Start to download content
                DependencyService.Get<IDownload>().StartDownload(downloadUrl);
                // Disable a button to avoid duplicated request.
                downloadButton.IsEnabled = false;
            }
            catch (Exception ex)
            {
                DependencyService.Get<IDownload>().DownloadLog("Request.Start() is failed" + ex);
                // In case download is failed, enable a button.
                downloadButton.IsEnabled = true;
            }
        }
    }
}
