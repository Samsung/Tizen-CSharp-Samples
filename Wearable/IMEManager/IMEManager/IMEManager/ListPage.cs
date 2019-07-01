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
using System.Collections.Generic;
using System.Text;
using Tizen.Uix.InputMethodManager;
using Xamarin.Forms;

namespace IMEManager
{
    /// <summary>
    /// The main page of the Input Method Manager application.
    /// </summary>
    public class ListPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListPage"/> class.
        /// </summary>
        public ListPage()
        {
            // Title of this page.
            this.Title = "IMEManager Sample";
            // Create a new List.
            var menu = new List<string>
            {
                "ShowIMEList",
                "ShowIMESelector",
                "IsIMEEnabled",
                "GetActiveIME",
                "GetEnabledIMECount",
            };

            var menuListView = new ListView()
            {
                Header = "",
                ItemsSource = menu,
                Footer = "",
            };

            this.Content = menuListView;
            menuListView.ItemTapped += MenuItemTapped;
        }

        /// <summary>
        /// Called when ListView item has been tapped.
        /// </summary>
        /// <param name="sender"> The sender object. </param>
        /// <param name="e"> Argument of Event. </param>
        private void MenuItemTapped(object sender, ItemTappedEventArgs e)
        {
            string title = e.Item.ToString();

            switch (title)
            {
                case "ShowIMEList":
                    Manager.ShowIMEList();
                    break;
                case "ShowIMESelector":
                    Manager.ShowIMESelector();
                    break;
                default:
                    this.Navigation.PushAsync(new ResultPage(title));
                    break;
            }
        }
    }
}
