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
using System;
using System.Collections.Generic;
using AppCommon.Cells;
using AppCommon.Extensions;

namespace AppCommon
{
    /// <summary>
    /// A class about paths information page
    /// </summary>
    public partial class PathsPage : ContentPage
    {
        Dialog _popup;

        /// <summary>
        /// A handler be called when an item on the list is selected
        /// </summary>
        /// <param name="s">sender</param>
        /// <param name="e">event</param>
        private void OnListItemSelected(object s, SelectedItemChangedEventArgs e)
        {
            var selectedItem = (PathInformation)e.SelectedItem;
            var title = selectedItem.Title;
            var path = selectedItem.Path;
            var count = 0;

            try
            {
                ((PathsPageViewModel)BindingContext).GetFilesCount(path);
            }
            catch
            {
                count = 0;
            }

            /// A pop-up be invoked when a list item is selected
            _popup = new Dialog { };
            var closeButton = new Button
            {
                Text = "CLOSE",
            };
            closeButton.Clicked += (sender, arg) =>
            {
                _popup.Hide();
            };
            _popup.Positive = closeButton;

            _popup.Title = title;
            _popup.Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = path
                    },
                    new Label
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = "File count: " + count,
                    },
                }
            };
            _popup.Show();
        }

        /// <summary>
        /// Initialize UI Components
        /// </summary>
        void InitializeComponent()
        {
            Title = "Paths";
            BackgroundColor = Color.Gray;

            /// mainLayout
            var mainLayout = new RelativeLayout { };

            /// list
            var list = new ListView
            {
                RowHeight = (int)(227.0 * _horizontalScale),
                ItemTemplate = new DataTemplate(typeof(PathItemCell)),
            };
            BindingContextChanged += (s, e) =>
            {
                if (BindingContext == null)
                {
                    return;
                }

                list.ItemsSource = ((PathsPageViewModel)BindingContext).Paths;
            };
            list.ItemSelected += new EventHandler<SelectedItemChangedEventArgs>(OnListItemSelected);

            /// floatingbutton
            var floatingButton = new AppCommon.Extensions.ImageButton
            {
                Source = "list_top_icon.png",
                HeightRequest = 100 * _horizontalScale,
            };

            floatingButton.Clicked += (s, e) =>
            {
                list.ScrollTo((list.ItemsSource as IList<PathInformation>)[0], ScrollToPosition.Start, true);
            };


            mainLayout.Children.Add(
                list,
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height;
                }));

            mainLayout.Children.Add(
                floatingButton,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width * 0.8194;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height * 0.8948;
                }));

            /// Set mainLayout as Content of PathsPage
            Content = mainLayout;
        }
    }
}