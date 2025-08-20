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

using Xamarin.Forms;

namespace SNSUI
{
    /// <summary>
    /// NewsFeedPage partial class declare.
    /// </summary>
    public class NewsFeedPage : ContentPage
    {
        bool _contentLoaded;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsFeedPage"/> class.
        /// </summary>
        public NewsFeedPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initial method for the class of NewsFeedPage.
        /// </summary>
        void InitializeComponent()
        {
            Title = "News Feed";
            IconImageSource = "00_controlbar_icon_playlist.png";

            if (_contentLoaded)
            {
                return;
            }

            _contentLoaded = true;

            var newsList = new StackLayout
            {
                Orientation = StackOrientation.Vertical,

                Spacing = 30,
            };

            for (var i = 0; i < 20; ++i)
            {
                newsList.Children.Add(new NewsItem { });
            }

            Content = new ScrollView
            {
                Content = newsList,
            };
        }
    }

    /// <summary>
    /// A class to describe an item for a NewsFeedPage.
    /// </summary>
    public class NewsItem : StackLayout
    {
        /// <summary>
        /// A constructor for the class.
        /// </summary>
        public NewsItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// To initialize UI components for the class.
        /// </summary>
        void InitializeComponent()
        {
            Orientation = StackOrientation.Vertical;

            var newsText = new Label
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                WidthRequest = App.ScreenWidth,
                LineBreakMode = LineBreakMode.CharacterWrap,
            };

            newsText.Text = "EFL is a collection of libraries that are independent or may build on top of each-other to provide useful features that complement an OS's existing environment, rather than wrap and abstract it, trying to be their own environment and OS in its entirety.";

            var firstButton = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            firstButton.Text = "I like it";

            var secondButton = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            secondButton.Text = "Message";

            var thirdButton = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            thirdButton.Text = "Share";

            var newsButtonLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,

                Spacing = 15,

                Children =
                {
                    firstButton,
                    secondButton,
                    thirdButton,
                }
            };

            Children.Add(newsText);
            Children.Add(newsButtonLayout);
        }
    }
}