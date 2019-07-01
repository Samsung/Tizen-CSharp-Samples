/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
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
using Tizen;
using Tizen.Uix.InputMethodManager;
using Xamarin.Forms;

namespace IMEManager
{
    /// <summary>
    /// The result page of the Input Method Manager application.
    /// </summary>
    internal class ResultPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultPage"/> class.
        /// </summary>
        /// <param name="detail"> The string object in which the ResultPage should attach. </param>
        public ResultPage(string detail)
        {
            // Add new Label
            Label label = new Label
            {
                // Set the horizontal text alignment mode, the text is center aligned.
                HorizontalTextAlignment = TextAlignment.Center,
                // Set the font size.
                FontSize = 36,
                // Set the text to be shown.
                Text = detail,
                // Set the LineBreakMode.
                LineBreakMode = LineBreakMode.CharacterWrap
            };

            // Title of this page.
            this.Title = "IMEManager Sample";
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

            switch (detail)
            {
                case "ShowIMEList":
                    // Show all installed IME list.
                    Manager.ShowIMEList();
                    break;
                case "ShowIMESelector":
                    // Open the IME selector.
                    Manager.ShowIMESelector();
                    break;
                case "IsIMEEnabled":
                    string appId = "ise-default";
                    // Checks if the specific IME is enabled or disabled.
                    if (Manager.IsIMEEnabled(appId))
                    {
                        label.Text = "IME state : On";
                    }
                    else
                    {
                        label.Text = "IME state : Off";
                    }

                    break;
                case "GetActiveIME":
                    // Get the active IME.
                    label.Text = "Active IME : " + Manager.GetActiveIME();
                    break;
                case "GetEnabledIMECount":
                    // Get the enabled IME count.
                    label.Text = "IME count : " + Manager.GetEnabledIMECount().ToString();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Called right after the back button is clicked.
        /// </summary>
        /// <param name="sender"> The sender object. </param>
        /// <param name="e"> Argument of Event. </param>
        private void OnButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
