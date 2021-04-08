/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace QRCodeGenerator.Tizen.Wearable.Views
{
    /// <summary>
    /// Page providing input for service set identifier.
    /// </summary>
    public partial class SettingsPage : CirclePage
    {
        #region methods

        /// <summary>
        /// Initializes SSIDSettings class instance.
        /// </summary>
        public SettingsPage()
        {
            InitializeComponent();

            // Set proper binding context for listview items.
            foreach (var item in listView.ItemsSource)
            {
                if (item is BindableObject)
                {
                    ((BindableObject)item).BindingContext = BindingContext;
                }
            }
        }

        #endregion
    }
}