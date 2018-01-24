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

namespace SystemInfo.Model.Led
{
    /// <summary>
    /// Interface that contains all necessary methods to get information about LED.
    /// </summary>
    public interface ILed
    {
        #region properties

        /// <summary>
        /// Gets LED's max brightness.
        /// </summary>
        int MaxBrightness { get; }

        /// <summary>
        /// Gets LED's brightness.
        /// </summary>
        int Brightness { get; }

        /// <summary>
        /// Event invoked when LED's brightness has changed.
        /// </summary>
        event EventHandler<LedEventArgs> LedChanged;

        #endregion

        #region methods

        /// <summary>
        /// Starts observing LED's brightness for changes.
        /// </summary>
        /// <remarks>
        /// Event LedChanged will be never invoked before calling this method.
        /// </remarks>
        void StartListening();

        #endregion
    }
}