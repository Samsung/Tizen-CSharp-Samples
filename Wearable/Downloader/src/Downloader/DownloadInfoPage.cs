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

using System.Collections.Generic;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace Downloader
{
    /// <summary>
    ///  A public class to show download information page.
    /// </summary>
    public class DownloadInfoPage : CirclePage
    {
        private CircleScrollView downloadInfoScrollView;
        private Label headerLabel;

        /// <summary>
        /// Constructor
        /// </summary>
        public DownloadInfoPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Register event handlers for entry, button and state callback.
        /// </summary>
        private void AddEvent()
        {
            DependencyService.Get<IDownload>().DownloadStateChanged += OnDownloadStateChanged;
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
                Text = "Download Information",
                FontSize = 8,
                LineBreakMode = LineBreakMode.NoWrap,
                FontAttributes = FontAttributes.Bold
            };
        }

        /// <summary>
        /// Create a new ListView
        /// This ScrollView shows download information list.
        /// </summary>
        /// <returns>ListView</returns>
        private CircleScrollView CreateScrollView()
        {
            CircleScrollView listView = new CircleScrollView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            return listView;
        }

        /// <summary>
        /// Initialize components of page.
        /// </summary>
        private void InitializeComponent()
        {
            headerLabel = CreateHeaderLabel();
            downloadInfoScrollView = CreateScrollView();

            headerLabel.SetValue(Grid.RowProperty, 0);
            downloadInfoScrollView.SetValue(Grid.RowProperty, 1);

            Content = new Grid()
            {
                RowDefinitions =
                {
                    new RowDefinition(){ Height = new GridLength(100, GridUnitType.Absolute)},
                    new RowDefinition(){ Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition(){ Height = new GridLength(50, GridUnitType.Absolute)},
                },
                Children =
                {
                    headerLabel,
                    downloadInfoScrollView
                },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            RotaryFocusObject = downloadInfoScrollView;

            AddEvent();
        }

        /// <summary>
        /// Refresh download information page after download completed.
        /// </summary>
        private void OnDownloadStateChanged(object sender, DownloadStateChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (e.stateMsg == "Completed")
                {
                    UpdateDownloadInfoList();
                }
            });
        }

        /// <summary>
        /// Update a download information list.
        /// </summary>
        private void UpdateDownloadInfoList()
        {
            var downloadInfoList = new List<DownloadInfo>();
            // Update a download information list when download is completed
            // Add downloaded URL to list
            downloadInfoList.Add(new DownloadInfo("URL", DependencyService.Get<IDownload>().GetUrl()));
            // Add downloaded content name to list
            downloadInfoList.Add(new DownloadInfo("Content Name", DependencyService.Get<IDownload>().GetContentName()));
            // Add downloaded content size to list
            downloadInfoList.Add(new DownloadInfo("Content Size", DependencyService.Get<IDownload>().GetContentSize().ToString()));
            // Add downloaded MIME type to list
            downloadInfoList.Add(new DownloadInfo("MIME Type", DependencyService.Get<IDownload>().GetMimeType()));
            // Add downloaded path to list
            downloadInfoList.Add(new DownloadInfo("Download Path", DependencyService.Get<IDownload>().GetDownloadedPath()));

            StackLayout stackLayout = new StackLayout();

            foreach (var item in downloadInfoList)
            {
                stackLayout.Children.Add(new StackLayout()
                {
                    Children =
                    {
                        new Label()
                        {
                            Text = item.Name,
                            HorizontalTextAlignment = TextAlignment.Center,
                            HorizontalOptions = LayoutOptions.Center
                        },
                        new Label()
                        {
                            Text = item.Value,
                            FontSize = 6,
                            HorizontalTextAlignment = TextAlignment.Center,
                            HorizontalOptions = LayoutOptions.Center,
                        }
                    },
                    Margin = new Thickness(0, 0, 0, 30)
                });
            }

            downloadInfoScrollView.Content = stackLayout;
        }
    }
}