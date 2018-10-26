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

namespace SystemInfo.Model.Battery
{
    /// <summary>
    /// Class that holds information about battery.
    /// </summary>
    public class Battery
    {
        #region fields

        /// <summary>
        /// Service that provides information about battery.
        /// </summary>
        private readonly IBattery _service;

        #endregion

        #region properties

        /// <summary>
        /// Event invoked when some information about battery was changed.
        /// </summary>
        public event EventHandler<BatteryEventArgs> BatteryChanged;

        /// <summary>
        /// Gets battery level.
        /// </summary>
        public int Level => _service.BatteryLevel;

        /// <summary>
        /// Gets charging status of battery.
        /// </summary>
        public bool IsCharging => _service.IsCharging;

        /// <summary>
        /// Gets battery level status.
        /// </summary>
        public BatteryLevelStatus BatteryLevelStatus => _service.BatteryLevelStatus;

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public Battery()
        {
            _service = DependencyService.Get<IBattery>();
            _service.StartListening();

            _service.BatteryInfoChanged += (s, e) => { OnBatteryChanged(e); };
        }

        /// <summary>
        /// Invokes BatteryInfoChanged event.
        /// </summary>
        /// <param name="e">Arguments passed with event.</param>
        protected virtual void OnBatteryChanged(BatteryEventArgs e)
        {
            BatteryChanged?.Invoke(this, e);
        }

        #endregion
    }
}