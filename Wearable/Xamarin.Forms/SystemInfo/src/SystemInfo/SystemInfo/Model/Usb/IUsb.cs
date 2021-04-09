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
    /// Interface that contains all necessary methods to get information about USB.
    /// </summary>
    public interface IUsb
    {
        #region properties

        /// <summary>
        /// Indicates if the USB debugging is enabled.
        /// </summary>
        bool UsbDebuggingEnabled { get; }

        /// <summary>
        /// Event invoked when USB debugging state has changed.
        /// </summary>
        event EventHandler<UsbDebuggingEventArgs> UsbDebuggingChanged;

        #endregion

        #region methods

        /// <summary>
        /// Starts observing USB information for changes.
        /// </summary>
        /// <remarks>
        /// UsbDebuggingChanged event will be never invoked before calling this method.
        /// </remarks>
        void StartListening();

        #endregion
    }
}