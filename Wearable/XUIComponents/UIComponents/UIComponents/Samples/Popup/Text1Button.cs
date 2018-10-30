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
    public class Text1Button : InformationPopup
    {
        /// <summary>
        /// Constructor of Text1Button class
        /// </summary>
        public Text1Button()
        {
            //Set text of popup
            Text = @"This is scrollable popup text.
This part is made by adding long text in popup. Popup internally added
scroller to this layout when size of text is greater than total popup
height. This has one button in action area and does not have title area";

            // Create new MenuItem for bottom button
            var button = new MenuItem()
            {
                Text = "OK",
                Command = new Command(() =>
                {
                    Console.WriteLine("Bottom button Command!!");
                    this.Dismiss();
                })
            };

            // Set bottom button
            BottomButton = button;
            //Request to dismiss Popup when back button event occurs
            BackButtonPressed += (s, e) => { this.Dismiss(); };
        }
    }
}