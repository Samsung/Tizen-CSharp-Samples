/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

using SystemInfo.Model.Usb;
using SystemInfo.Utils;

namespace SystemInfo.ViewModel
{
    /// <summary>
    /// ViewModel class for USB page.
    /// </summary>
    public class UsbViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Properties of device's USB services.
        /// </summary>
        public static readonly string[] Properties = {"USB debugging enabled", "Host", "Client or Accessory"};

        /// <summary>
        /// Local storage of collection of USB services.
        /// </summary>
        private ListItem _itemList;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets collection of USB services.
        /// </summary>
        public ListItem ItemList
        {
            get => _itemList;
            set => SetProperty(ref _itemList, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public UsbViewModel()
        {
            var usb = new Usb();

            string[] initialValues =
            {
                usb.UsbDebuggingEnabled.ToString(),
                usb.Host,
                usb.ClientAccessory
            };

            ItemList = ListUtils.CreateItemsList(Properties, initialValues, ListItemType.Standard);

            usb.UsbDebuggingChanged += OnUsbDebuggingChanged;
        }

        /// <summary>
        /// Updates USB debugging mode.
        /// </summary>
        /// <param name="s">Object that sent event.</param>
        /// <param name="e">Event's argument.</param>
        private void OnUsbDebuggingChanged(object s, UsbDebuggingEventArgs e)
        {
            ItemList["USB debugging enabled"] = e.UsbDebuggingEnabled.ToString();
        }

        #endregion
    }
}