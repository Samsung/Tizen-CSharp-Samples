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

    public class MasterPageItem
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// The master page of the EmailUI application.
    /// </summary>
    public class MasterPageCS : ContentPage
    {
        private ListView listView;

        public ListView ListView
        {
            get
            {
                return this.listView;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterPageCS"/> class.
        /// </summary>
        public MasterPageCS()
        {
            Title = "MasterPage";
            //create masterPage items
            var masterPageItems = new List<MasterPageItem>();
            //add masterPage item in Box
            masterPageItems.Add(new MasterPageItem { Name = "In Box" });
            //add masterPage item out Box
            masterPageItems.Add(new MasterPageItem { Name = "Out Box" });

            //create list view
            this.listView = new ListView
            {
                // Set the list item row height
                RowHeight = (int)(0.09 * App.screenHeight),
                ItemsSource = masterPageItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    //Create the label
                    Label label = new Label
                    {
                        TextColor = Color.Black,
                        FontSize = 24,
                        VerticalTextAlignment = TextAlignment.Center,
                        HeightRequest = 200,
                        Margin = 20,
                    };

                    // Set the label text binding
                    label.SetBinding(Label.TextProperty, "Name");
                    var layout = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        VerticalOptions = LayoutOptions.StartAndExpand,
                        Children =
                        {
                            label,
                        }
                    };
                    return new ViewCell
                    {
                        View = layout
                    };
                }),
                VerticalOptions = LayoutOptions.FillAndExpand,
                SeparatorVisibility = SeparatorVisibility.None
            };

            //set the content of this page
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
