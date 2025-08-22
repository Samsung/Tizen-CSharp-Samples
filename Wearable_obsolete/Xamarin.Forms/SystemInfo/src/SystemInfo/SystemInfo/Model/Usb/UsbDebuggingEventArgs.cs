/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;

namespace SystemInfo.Model.Usb
{
    /// <summary>
    /// Class that is passed with UsbDebuggingChanged event.
    /// </summary>
    public class UsbDebuggingEventArgs : EventArgs
    {
        #region properties

        /// <summary>
        /// Indicates if the USB debugging is enabled.
        /// </summary>
        public bool UsbDebuggingEnabled { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Class constructor that allows to set USB debugging enabled flag.
        /// </summary>
        /// <param name="usbDebuggingEnabled">USB debugging enabled flag.</param>
        public UsbDebuggingEventArgs(bool usbDebuggingEnabled)
        {
            UsbDebuggingEnabled = usbDebuggingEnabled;
        }

        #endregion
    }
}