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
using SystemInfo.Model.Led;
using SystemInfo.Tizen.Mobile.Service;
using Led = Tizen.System.Led;

[assembly: Xamarin.Forms.Dependency(typeof(LedService))]

namespace SystemInfo.Tizen.Mobile.Service
{
    /// <summary>
    /// Provides methods that allow to obtain information about device's LED.
    /// </summary>
    public class LedService : ILed
    {
        #region properties

        /// <summary>
        /// Gets LED's max brightness.
        /// </summary>
        public int MaxBrightness => Led.MaxBrightness;

        /// <summary>
        /// Gets LED's brightness.
        /// </summary>
        public int Brightness => Led.Brightness;

        /// <summary>
        /// Event invoked when LED's brightness has changed.
        /// </summary>
        public event EventHandler<LedEventArgs> LedChanged;

        #endregion

        #region methods

        /// <summary>
        /// Starts observing LED's brightness for changes.
        /// </summary>
        /// <remarks>
        /// Event LedChanged will be never invoked before calling this method.
        /// </remarks>
        public void StartListening()
        {
            Led.BrightnessChanged += (s, e) => { LedChanged?.Invoke(s, new LedEventArgs(e.Brightness)); };
        }

        #endregion
    }
}