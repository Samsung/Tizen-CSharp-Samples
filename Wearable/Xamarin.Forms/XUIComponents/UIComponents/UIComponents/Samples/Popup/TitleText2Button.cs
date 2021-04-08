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

namespace UIComponents.Samples.Popup
{
    public class TitleText2Button : TwoButtonPopup
    {
        /// <summary>
        /// Constructor of TitleText2Button class
        /// </summary>
        public TitleText2Button()
        {
            //Set title of popup
            Title = "Popup title";
            //Set text of popup
            Text = @"This is scrollable popup text.
This part is made by adding long text in popup. Popup internally added
scroller to this layout when size of text is greater than total popup
height. This has two button in action area and title text in title area";

            // Create FirstButton
            FirstButton = new MenuItem()
            {
                // Set icon
                IconImageSource = new FileImageSource
                {
                    File = "tw_ic_popup_btn_delete.png",
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
                    File = "tw_ic_popup_btn_check.png",
                },
                //Set command
                Command = new Command(() =>
                {
                    Console.WriteLine("right button1 Command!!");
                    this.Dismiss();
                })
            };

            //Request to dismiss this popup when back button event occurs
            BackButtonPressed += (s, e) => { this.Dismiss(); };
        }
    }
}