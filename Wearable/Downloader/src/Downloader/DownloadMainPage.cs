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
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace Downloader
{
    /// <summary>
    ///  A class for download main page.
    /// </summary>
    internal class DownloadMainPage : CirclePage
    {
        private ActionButtonItem downloadButton;
        private string downloadUrl;
        private Label headerLabel;
        private CircleProgressBarSurfaceItem progressBar;
        private Label progressLabel;
        private Entry urlEntry;
        private Label urlEntryLabel;

        /// <summary>
        /// Constructor.
        /// </summary>
        public DownloadMainPage()
        {
            InitializeComponent();
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
        /// Create a new Button to start download.
        /// </summary>
        /// <returns>Button</returns>
        private ActionButtonItem CreateDownloadButton()
        {
            return new ActionButtonItem()
            {
                Text = "Start Download",
            };
        }

        /// <summary>
        /// Create a new Entry to get URL.
        /// </summary>
        /// <returns>Entry</returns>
        private Entry CreateEntry()
        {
            return new Entry()
            {
                Placeholder = "Type URL here to download",
                Keyboard = Keyboard.Url,
                FontSize = 6,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
            };
        }

        /// <summary>
        /// Create a new Label.
        /// </summary>
        /// <returns>Label</returns>
        private Label CreateEntryLabel()
        {
            return new Label()
            {
                Text = "URL:",
                FontSize = 6,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End
            };
        }

        /// <summary>
        /// Create a new Label.
        /// </summary>
        /// <returns>Label</returns>
        private Label CreateHeaderLabel()
        {
            return new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Text = "Download",
                FontSize = 8,
                FontAttributes = FontAttributes.Bold
            };
        }

        /// <summary>
        /// Create a new ProgressBar to present download state.
        /// </summary>
        /// <returns><ProgressBar</returns>
        private CircleProgressBarSurfaceItem CreateProgressbar()
        {
            return new CircleProgressBarSurfaceItem()
            {
                Value = 0
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
                Text = "0 bytes / 0 bytes",
                FontAttributes = FontAttributes.Bold,
                FontSize = 6,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };
        }

        /// <summary>
        /// Initialize main page.
        /// Add components and events.
        /// </summary>
        private void InitializeComponent()
        {
            downloadButton = CreateDownloadButton();
            progressBar = CreateProgressbar();

            urlEntryLabel = CreateEntryLabel();
            urlEntry = CreateEntry();
            progressLabel = CreateProgressLabel();
            headerLabel = CreateHeaderLabel();

            headerLabel.SetValue(Grid.RowProperty, 0);
            headerLabel.SetValue(Grid.ColumnSpanProperty, 2);

            urlEntryLabel.SetValue(Grid.RowProperty, 1);
            urlEntryLabel.SetValue(Grid.ColumnProperty, 0);

            urlEntry.SetValue(Grid.RowProperty, 1);
            urlEntry.SetValue(Grid.ColumnProperty, 1);

            progressLabel.SetValue(Grid.RowProperty, 2);
            progressLabel.SetValue(Grid.ColumnSpanProperty, 2);

            Content = new Grid()
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star)},
                    new ColumnDefinition() { Width = new GridLength(12, GridUnitType.Star)},
                },
                RowDefinitions =
                {
                    new RowDefinition(){ Height = new GridLength(100, GridUnitType.Absolute)},
                    new RowDefinition(){ Height = new GridLength(100, GridUnitType.Absolute)},
                    new RowDefinition(){ Height = new GridLength(50, GridUnitType.Absolute)}
                },
                Children =
                {
                    headerLabel,
                    urlEntryLabel,
                    urlEntry,
                    progressLabel
                },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            CircleSurfaceItems.Add(progressBar);
            ActionButton = downloadButton;

            AddEvent();
        }

        /// <summary>
        /// Event handler when button is clicked.
        /// Once a button is clicked and download is started, a button is disabled.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void OnButtonClicked(object sender, EventArgs e)
        {
            downloadUrl = urlEntry.Text;
            DependencyService.Get<IDownload>().DownloadLog("Start Download: " + downloadUrl);
            try
            {
                progressBar.Value = 0;
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

        /// <summary>
        /// Event handler when data is received.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments including received data size</param>
        private void OnProgressbarChanged(object sender, DownloadProgressEventArgs e)
        {
            ulong ContentSize = DependencyService.Get<IDownload>().GetContentSize();

            if (e.ReceivedSize > 0)
            {
                progressBar.Value = (double)e.ReceivedSize / ContentSize;
                progressLabel.Text = e.ReceivedSize + "bytes / " + ContentSize + "bytes";
            }
        }

        /// <summary>
        /// Event handler when download state is changed.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments including download state</param>
        private void OnStateChanged(object sender, DownloadStateChangedEventArgs e)
        {
            if (e.stateMsg.Length > 0)
            {
                DependencyService.Get<IDownload>().DownloadLog("State: " + e.stateMsg);

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
                    // Update a download state
                    downloadButton.Text = e.stateMsg;
                }
            }
        }
    }
}