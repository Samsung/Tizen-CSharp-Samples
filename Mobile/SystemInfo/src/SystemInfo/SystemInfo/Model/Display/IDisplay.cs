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

namespace SystemInfo.Model.Display
{
    /// <summary>
    /// Interface that contains all necessary methods to get information about display.
    /// </summary>
    public interface IDisplay
    {
        #region properties

        /// <summary>
        /// Gets number of available displays.
        /// </summary>
        int NumberOfDisplays { get; }

        /// <summary>
        /// Gets main display max brightness.
        /// </summary>
        int MainDisplayMaxBrigtness { get; }

        /// <summary>
        /// Gets main display brightness.
        /// </summary>
        int MainDisplayBrightness { get; }

        /// <summary>
        /// Gets main display state.
        /// </summary>
        DisplayState MainDisplayState { get; }

        /// <summary>
        /// Gets screen backlight time.
        /// </summary>
        int ScreenBacklightTime { get; }

        /// <summary>
        /// Event invoked when screen backlight time has changed.
        /// </summary>
        event EventHandler<ScreenBacklightTimeChangedEventArgs> ScreenBacklightTimeChanged;

        /// <summary>
        /// Event invoked when display state has changed.
        /// </summary>
        event EventHandler<DisplayStateEventArgs> DisplayChanged;

        #endregion

        #region methods

        /// <summary>
        /// Starts observing display information for changes.
        /// </summary>
        /// <remarks>
        /// Display events will be never invoked before calling this method.
        /// </remarks>
        void StartListening();

        #endregion
    }
}