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

namespace Settings
{
    /// <summary>
    /// The second page of the settings application, to show the text only.
    /// </summary>
    public class SecondPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecondPage"/> class.
        /// </summary>
        /// <param name="detail">The <see cref="string"/> object in which the SecondPage should attach.</param>
        public SecondPage(string detail)
        {
            // Add new Label
            Label label = new Label
            {
                // Set the horizontal text alignment mode, the text is center aligned.
                HorizontalTextAlignment = TextAlignment.Center,
                // Set the font size.
                FontSize = 36,
                // Set the text to be shown.
                Text = detail
            };

            // Title of this page.
            this.Title = "Settings";
            // Content view of this page.
            this.Content = new StackLayout
            {
                // Set the VerticalOptions of the StackLayout, the layout is center aligned.
                VerticalOptions = LayoutOptions.Center,
                // Add all the children of the StackLayout.
                Children =
                {
                    label
                }
            };
        }

        /// <summary>
        /// Called right after the back button is clicked.
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void OnButtonClicked(object sender, EventArgs e)
        {
            // Back to previous page.
            Navigation.PopModalAsync();
        }
    }
}