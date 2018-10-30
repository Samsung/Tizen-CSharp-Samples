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

namespace SystemInfo.Model.Capability
{
    /// <summary>
    /// Interface that contains all necessary methods to get information about capabilities.
    /// </summary>
    public interface ICapability
    {
        #region properties

        /// <summary>
        /// Indicates if the 3G data network is enabled.
        /// </summary>
        bool Network3G { get; }

        /// <summary>
        /// Indicates if Wi-Fi-related notifications are enabled on the device.
        /// </summary>
        bool WifiNotification { get; }

        /// <summary>
        /// Indicates if the device is in the flight mode.
        /// </summary>
        bool FlightMode { get; }

        /// <summary>
        /// Event invoked when 3G network was turned on or off.
        /// </summary>
        event EventHandler<CapabilityChangedEventArgs> Network3GChanged;

        /// <summary>
        /// Event invoked when Wi-Fi-related notifications have changed.
        /// </summary>
        event EventHandler<CapabilityChangedEventArgs> WifiNotificationChanged;

        /// <summary>
        /// Event invoked when flight mode was turned on or off.
        /// </summary>
        event EventHandler<CapabilityChangedEventArgs> FlightModeChanged;

        #endregion

        #region methods

        /// <summary>
        /// Starts observing capabilities information for changes.
        /// </summary>
        /// <remarks>
        /// Capabilities' events will be never invoked before calling this method.
        /// </remarks>
        void StartListening();

        #endregion
    }
}