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
using Xamarin.Forms;

namespace SystemInfo.Model.Led
{
    /// <summary>
    /// Class that holds information about LED.
    /// </summary>
    public class Led
    {
        #region fields

        /// <summary>
        /// Service that provides information about LED.
        /// </summary>
        private readonly ILed _service;

        #endregion

        #region properties

        /// <summary>
        /// Gets LED's max brightness.
        /// </summary>
        public int MaxBrightness => _service.MaxBrightness;

        /// <summary>
        /// Gets LED's brightness.
        /// </summary>
        public int Brightness => _service.Brightness;

        /// <summary>
        /// Event invoked when LED's brightness has changed.
        /// </summary>
        public event EventHandler<LedEventArgs> LedChanged;

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public Led()
        {
            _service = DependencyService.Get<ILed>();

            _service.StartListening();

            _service.LedChanged += (s, e) => { OnLedChanged(e); };
        }

        /// <summary>
        /// Invokes LedChanged event.
        /// </summary>
        /// <param name="e">Arguments passed with event.</param>
        protected virtual void OnLedChanged(LedEventArgs e)
        {
            LedChanged?.Invoke(this, e);
        }

        #endregion
    }
}