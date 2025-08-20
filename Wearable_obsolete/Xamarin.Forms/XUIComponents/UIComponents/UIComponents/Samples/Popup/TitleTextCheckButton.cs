/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using WCheck = Tizen.Wearable.CircularUI.Forms.Check;

namespace UIComponents.Samples.Popup
{
    public class TitleTextCheckButton : TwoButtonPopup
    {
        /// <summary>
        /// Constructor of TitleTextCheckButton class
        /// </summary>
        public TitleTextCheckButton()
        {
            Title = "Popup title";

            // Create FirstButton
            FirstButton = new MenuItem()
            {
                // Set icon
                IconImageSource = new FileImageSource
                {
                    File = "b_option_list_icon_share.png",
                },
                //Set command
                Command = new Command(() =>
                {
                    Console.WriteLine("left button1 Command!!");
                    this.Dismiss();
                })
            };

            // Create SecondButton
            SecondButton = new MenuItem()
            {
                // Set icon
                IconImageSource = new FileImageSource
                {
                    File = "b_option_list_icon_delete.png",
                },
                //Set command
                Command = new Command(() =>
                {
                    Console.WriteLine("right button1 Command!!");
                    this.Dismiss();
                })
            };

            //Create Check
            var checkbox = new WCheck
            {
                DisplayStyle = CheckDisplayStyle.Small
            };
            
            //Add Check Toggled event handler
            checkbox.Toggled += (s, e) =>
            {
                Console.WriteLine($"checkbox toggled. checkbox.IsToggled:{checkbox.IsToggled}");
            };

            // Set content of this popup with Label and Check
            Content = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new Label
                    {
                        Text = "Will be saved, and sound, only on the Gear.",
                    },
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Padding = new Thickness(0, 40, 0, 40),
                        Children =
                        {
                            checkbox,
                            new Label
                            {
                                Text = "Do not repeat",
                            }
                        }
                    }
                }
            };

            //Request to dismiss this popup when back button event occurs
            BackButtonPressed += (s, e) => { this.Dismiss(); };
        }
    }
}