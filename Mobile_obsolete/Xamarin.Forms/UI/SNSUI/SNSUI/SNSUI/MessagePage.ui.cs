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
    /// MessagePage partial class declare.
    /// </summary>
    public class MessagePage : ContentPage
    {
        private bool _contentLoaded;
        private Grid _gridView;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagePage"/> class.
        /// </summary>
        public MessagePage()
        {

            InitializeComponent();
        }

        /// <summary>
        /// Initial method for the class of MessagePage.
        /// </summary>
        void InitializeComponent()
        {
            IconImageSource = "00_controlbar_icon_dialer.png";
            Title = "Message";

            if (_contentLoaded)
            {
                return;
            }

            _contentLoaded = true;

            Content = new ScrollView
            {
                HorizontalOptions = LayoutOptions.Center,
                Content = CreateGridViewWithOverlappingImages()
            };
        }

        /// <summary>
        /// A method to return grid view.
        /// </summary>
        /// <returns>Grid type as result</returns>
        private Grid CreateGridViewWithOverlappingImages()
        {
            _gridView = new Grid();

            /// Set column width
            for (int i = 0; i < 30; i++)
            {
                _gridView.ColumnDefinitions.Add(new ColumnDefinition { Width = App.ScreenWidth * 0.02 });
            }

            /// Set row height
            for (int i = 0; i < 80; i++)
            {
                _gridView.RowDefinitions.Add(new RowDefinition { Height = App.ScreenWidth * 0.02 });
            }

            /// Layout children into grid item
            for (int row = 0; row < 80;)
            {
                for (int column = 0; column < 30;)
                {
                    LayoutChildren(column, column + 10, row, row + 10);
                    column += 10;
                }

                row += 10;
            }

            return _gridView;
        }

        /// <summary>
        /// A method to put image and small image into grid item.
        /// </summary>
        /// <param name="left">The left edge of the column span</param>
        /// <param name="right">The right edge of the column span</param>
        /// <param name="top">The top edge of the row span</param>
        /// <param name="bottom">The bottom edge of the row span</param>
        private void LayoutChildren(int left, int right, int top, int bottom)
        {
            _gridView.Children.Add(
                new Image
                {
                    Source = "picture.jpg"
                }, left, right, top, bottom);

            _gridView.Children.Add(
                new Image
                {
                    Source = "picture.jpg"
                }, left + 6, right, top, bottom - 6);
        }
    }
}