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

using System;
using SystemInfo.Model.Display;
using SystemInfo.Tizen.Mobile.Service;
using Tizen.System;
using Display = Tizen.System.Display;
using DisplayState = SystemInfo.Model.Display.DisplayState;


[assembly: Xamarin.Forms.Dependency(typeof(DisplayService))]

namespace SystemInfo.Tizen.Mobile.Service
{
    /// <summary>
    /// Provides methods that allow to obtain information about device's display.
    /// </summary>
    public class DisplayService : IDisplay
    {
        #region properties

        /// <summary>
        /// Gets main display max brightness.
        /// </summary>
        public int MainDisplayMaxBrigtness
        {
            get
            {
                var display = Display.Displays[0];
                return display.MaxBrightness;
            }
        }

        /// <summary>
        /// Gets main display brightness.
        /// </summary>
        public int MainDisplayBrightness
        {
            get
            {
                var display = Display.Displays[0];
                return display.Brightness;
            }
        }

        /// <summary>
        /// Gets main display state.
        /// </summary>
        public DisplayState MainDisplayState => (DisplayState)Display.State;

        /// <summary>
        /// Gets number of available displays.
        /// </summary>
        public int NumberOfDisplays => Display.NumberOfDisplays;

        /// <summary>
        /// Gets screen backlight time.
        /// </summary>
        public int ScreenBacklightTime => SystemSettings.ScreenBacklightTime;

        /// <summary>
        /// Event invoked when screen backlight time has changed.
        /// </summary>
        public event EventHandler<SystemInfo.Model.Display.ScreenBacklightTimeChangedEventArgs>
            ScreenBacklightTimeChanged;

        /// <summary>
        /// Event invoked when display state has changed.
        /// </summary>
        public event EventHandler<DisplayStateEventArgs> DisplayChanged;

        #endregion

        #region methods

        /// <summary>
        /// Starts observing display information for changes.
        /// </summary>
        /// <remarks>
        /// Display events will be never invoked before calling this method.
        /// </remarks>
        public void StartListening()
        {
            Display.StateChanged +=
                (s, e) =>
                {
                    DisplayChanged?.Invoke(s, new DisplayStateEventArgs(EnumMapper.DisplayStateMapper(e.State)));
                };

            SystemSettings.ScreenBacklightTimeChanged +=
                (s, e) =>
                {
                    ScreenBacklightTimeChanged?.Invoke(s,
                        new SystemInfo.Model.Display.ScreenBacklightTimeChangedEventArgs(e.Value));
                };
        }

        #endregion
    }
}