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


namespace EmailUI
{
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;
    using Label = Xamarin.Forms.Label;

    /// <summary>
    /// The detail page of the EmailUI application.
    /// </summary>
    public class DetailPageCS : ContentPage
    {
        private ListView listView;
        // basic font size of text
        private const int FONT_SIZE = 24;

        public ListView ListView
        {
            get
            {
                return this.listView;
            }
        }

        private class DetailPageItem
        {
            public string Title { get; set; }

            public string Content { get; set; }
        }

        private class DetailGroup : List<DetailPageItem>
        {
            public string ShortName { get; set; }

            public string DisplayName
            {
                get
                {
                    return this.ShortName;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailPageCS"/> class.
        /// </summary>
        public DetailPageCS()
        {
            // create items list
            var groupItems = new List<DetailGroup>();

            // add group 1 items data
            DetailGroup group1 = new DetailGroup { ShortName = "Group Index 1" };

            int i;
            for (i = 1; i < 10; i++)
            {
                group1.Add(new DetailPageItem { Title = "[" + i + "]elm.text.Subject", Content = "Content text will be written here" });
            }

            // add group 2 items data
            DetailGroup group2 = new DetailGroup { ShortName = "Group Index 2" };
            for (i = 11; i < 20; i++)
            {
                group2.Add(new DetailPageItem { Title = "[" + i + "]elm.text.Subject", Content = "Content text will be written here" });
            }

            groupItems.Add(group1);
            groupItems.Add(group2);

            // create list view
            this.listView = new ListView
            {
                // Set the list item row height
                RowHeight = (int)(0.13 * App.screenHeight),
                HasUnevenRows = true,
                IsGroupingEnabled = true,

                // Set the group item display name binding
                GroupDisplayBinding = new Binding("DisplayName"),

                // Set the group item short name binding
                GroupShortNameBinding = new Binding("ShortName"),

                ItemsSource = groupItems,

                ItemTemplate = new DataTemplate(() =>
                {
                    Label titleLabel = new Label
                    {
                        TextColor = Color.Black,
                        FontSize = FONT_SIZE,
                    };

                    Label contentLabel = new Label
                    {
                        TextColor = Color.Gray,
                        FontSize = FONT_SIZE - 5,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                    };

                    Label countLabel = new Label
                    {
                        TextColor = Color.Gray,
                        HorizontalTextAlignment = TextAlignment.End,
                        FontSize = FONT_SIZE - 5,
                        Text = "[7/14]",
                    };
                    titleLabel.SetBinding(Label.TextProperty, "Title");
                    contentLabel.SetBinding(Label.TextProperty, "Content");

                    var layout = new StackLayout
                    {
                        Padding = new Thickness(20, 25),
                        Orientation = StackOrientation.Vertical,
                        Children =
                        {
                            titleLabel,
                            new StackLayout
                            {
                                Spacing = 30,
                                Orientation = StackOrientation.Horizontal,
                                Children =
                                {
                                    contentLabel,
                                    countLabel
                                }
                            }
                        }
                    };
                    return new ViewCell
                    {
                        View = layout
                    };
                }),
            };

            // The title of this page
            this.Title = "Email UI";

            // Content view of this page.
            this.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    this.listView
                }
            };
        }
    }
}
