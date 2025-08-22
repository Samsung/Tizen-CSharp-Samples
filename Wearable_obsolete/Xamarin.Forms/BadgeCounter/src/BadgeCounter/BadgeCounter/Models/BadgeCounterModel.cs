/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
using Xamarin.Forms;

namespace BadgeCounter.Models
{
    /// <summary>
    /// Model class which allows to manage current application's badge counter.
    /// </summary>
    class BadgeCounterModel
    {
        #region fields

        /// <summary>
        /// Default value of the badge counter.
        /// Used to reset the value.
        /// </summary>
        private static readonly int DEFAULT_VALUE = 0;

        /// <summary>
        /// Interval for auto-increment feature (in milliseconds).
        /// </summary>
        private static readonly int AUTOINCREMENT_INTERVAL = 1500;

        /// <summary>
        /// Service which allows to manage badge count for current application.
        /// </summary>
        private ICurrentAppBadgeControlService _badgeControlService;

        /// <summary>
        /// Backing field for "AutoIncrement" property.
        /// Indicates if auto-increment feature is enabled.
        /// </summary>
        private bool _autoIncrement = false;

        #endregion

        #region properties

        /// <summary>
        /// Current value of the badge counter.
        /// </summary>
        public int Value
        {
            get => _badgeControlService.BadgeCount;
            set => _badgeControlService.BadgeCount = value;
        }

        /// <summary>
        /// Indicates if auto-increment feature is enabled.
        /// If so, the badge counter is automatically incremented using defined interval.
        /// </summary>
        public bool AutoIncrement
        {
            get => _autoIncrement;
            set
            {
                if (value == _autoIncrement)
                {
                    return;
                }

                _autoIncrement = value;

                // start timer (using defined interval) which increase badge counter value
                if (_autoIncrement)
                {
                    Device.StartTimer(TimeSpan.FromMilliseconds(AUTOINCREMENT_INTERVAL), () =>
                    {
                        if (AutoIncrement)
                        {
                            Value++;

                            // return true to keep timer running
                            return true;
                        }

                        // return false to stop the timer
                        return false;
                    });
                }
            }
        }

        /// <summary>
        /// Event invoked when badge counter value was changed.
        /// </summary>
        public event EventHandler Changed;

        #endregion

        #region methods

        /// <summary>
        /// Creates instance of the model.
        /// </summary>
        public BadgeCounterModel()
        {
            _badgeControlService = DependencyService.Get<ICurrentAppBadgeControlService>();
            _badgeControlService.Changed += OnBadgeCountChanged;
        }

        /// <summary>
        /// Handles change of the badge count for current application.
        /// Invokes own (model) event.
        /// </summary>
        /// <param name="sender">Event sender (service).</param>
        /// <param name="e">Event arguments.</param>
        private void OnBadgeCountChanged(object sender, EventArgs e)
        {
            Changed?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Resets the badge counter to default value.
        /// </summary>
        public void Reset()
        {
            Value = DEFAULT_VALUE;
        }

        #endregion
    }
}
