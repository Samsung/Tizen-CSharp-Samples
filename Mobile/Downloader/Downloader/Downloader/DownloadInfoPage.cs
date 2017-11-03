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

using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Downloader
{
    /// <summary>
    ///  A public class to show download information page.
    /// </summary>
    public class DownloadInfoPage : ContentPage
    {
        private Label downloadInfoLabel;
        private ListView downloadInfoListView;
        private ObservableCollection<DownloadInfo> downloadInfoList = new ObservableCollection<DownloadInfo>();
        private bool isUpdatedInfoList;

        /// <summary>
        /// Constructor
        /// </summary>
        public DownloadInfoPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Refresh download information page.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Update a download information list when this page is focused
            UpdateDownloadInfoList();
        }

        /// <summary>
        /// Initialize components of page.
        /// </summary>
        void InitializeComponent()
        {
            Title = "Download Info";
            downloadInfoLabel = CreateDownloadInfoLabel();
            downloadInfoListView = CreateListView();
            isUpdatedInfoList = false;

            Content = new StackLayout
            {
                Spacing = 0,
                Children =
                {
                   downloadInfoLabel,
                   downloadInfoListView,
                }
            };
        }

        /// <summary>
        /// Update a download information list.
        /// </summary>
        private void UpdateDownloadInfoList()
        {
            // Update a download information list when download is completed
            if (!isUpdatedInfoList && (State)DependencyService.Get<IDownload>().GetDownloadState() == State.Completed)
            {
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

                isUpdatedInfoList = true;
            }
        }

        /// <summary>
        /// Create a new Label.
        /// </summary>
        /// <returns>Label</returns>
        private Label CreateDownloadInfoLabel()
        {
            return new Label()
            {
                HorizontalTextAlignment = TextAlignment.Start,
                Text = "Download Information",
                TextColor = Color.White,
                FontSize = 20,
                Margin = new Thickness(10, 10),
            };
        }

        /// <summary>
        /// Create a new ListView
        /// This ListView shows download information list.
        /// </summary>
        /// <returns>ListView</returns>
        private ListView CreateListView()
        {
            ListView ListView = new ListView()
            {
                ItemsSource = downloadInfoList,

                ItemTemplate = new DataTemplate(() =>
                {
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "Name");

                    Label valueLabel = new Label();
                    valueLabel.FontSize = 22;
                    valueLabel.SetBinding(Label.TextProperty, "Value");

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(30, 20),
                            Orientation = StackOrientation.Horizontal,
                            Children =
                                  {
                                      new StackLayout
                                      {
                                          VerticalOptions = LayoutOptions.Center,
                                          Spacing = 0,
                                          Children =
                                          {
                                              nameLabel,
                                              valueLabel
                                          }
                                      }
                                  }
                        }
                    };
                }),
            };
            return ListView;
        }
    }
}