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
            // Content view of this page.
            this.Content = new TableView
            {
                // A table intended to be used as a menu for selections.
                Intent = TableIntent.Menu,
                Root = new TableRoot
                {
                    // Create a new TableSection and set the sub title.
                    new TableSection()
                    {
                        new CustomCell("ShowIMEList", this),
                        new CustomCell("ShowIMESelector", this),
                        new CustomCell("IsIMEEnabled", this),
                        new CustomCell("GetActiveIME", this),
                        new CustomCell("GetEnabledIMECount", this)
                    },
                }
            };
        }
    }
}
