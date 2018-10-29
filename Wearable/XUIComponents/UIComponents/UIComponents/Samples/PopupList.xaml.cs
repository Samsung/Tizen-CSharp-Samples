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
using Xamarin.Forms.Xaml;

namespace UIComponents.Samples
{
    /// <summary>
    /// PopupList class
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupList : CirclePage
    {
        /// <summary>
        /// Constructor of PopupList class
        /// </summary>
        public PopupList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when item is tapped
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">ItemTappedEventArgs</param>
        public void OnItemTapped(object sender, ItemTappedEventArgs args)
        {
            if (args.Item == null)
            {
                return;
            }

            var desc = args.Item as Sample;
            Console.WriteLine($"OnItemTapped desc.Class:{desc.Class}");
            if (desc != null && desc.Class != null)
            {
                Type pageType = desc.Class;

                string title = desc.Title;

                ///If 2 buttons case, create TwoButtonPopup
                if (title.EndsWith("2button"))
                {
                    Console.WriteLine($"title end 2button ");
                    var twoButtonPopup = Activator.CreateInstance(pageType) as TwoButtonPopup;
                    if (twoButtonPopup != null)
                    {
                        twoButtonPopup.Show();
                    }
                }
                else if (title.Equals("Toast")) // Toast case - set text
                {
                    Console.WriteLine($"display Toast");
                    Toast.DisplayText("Saving Contacts to SIM , 1 2 3 4 5 6 7 8 9 10.", 2000);
                }
                else if (title.Equals("Graphic Toast")) // Graphic Toast case - set text and image
                {
                    Console.WriteLine($"display Graphic Toast");
                    Toast.DisplayIconText("Check my device and phone number", new FileImageSource { File = "b_option_list_icon_action.png" }, 2000);
                }
                else // Other cases. create Information popup
                {
                    var popup = Activator.CreateInstance(pageType) as InformationPopup;
                    if (popup != null)
                    {
                        popup.Show();
                    }
                }
            }
        }
    }
}
