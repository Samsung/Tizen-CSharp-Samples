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

using Xamarin.Forms;

namespace SystemInfo.View
{
    /// <summary>
    /// Interface that contains all pages, which have different layout on different platforms.
    /// </summary>
    public interface IPageResolver
    {
        /// <summary>
        /// Main page of application.
        /// </summary>
        Page MainPage { get; }

        /// <summary>
        /// Page that contains information about device's battery.
        /// </summary>
        Page BatteryPage { get; }

        /// <summary>
        /// Page that contains information about device's capabilities.
        /// </summary>
        Page CapabilitiesPage { get; }

        /// <summary>
        /// Page that contains information about device's display.
        /// </summary>
        Page DisplayPage { get; }

        /// <summary>
        /// Page that contains information about device's LEDs.
        /// </summary>
        Page LedPage { get; }

        /// <summary>
        /// Page that contains information about device's settings.
        /// </summary>
        Page SettingsPage { get; }

        /// <summary>
        /// Page that contains information about device's USB.
        /// </summary>
        Page UsbPage { get; }


        /// <summary>
        /// Page that contains information about device's vibrators.
        /// </summary>
        Page VibratorPage { get; }
    }
}