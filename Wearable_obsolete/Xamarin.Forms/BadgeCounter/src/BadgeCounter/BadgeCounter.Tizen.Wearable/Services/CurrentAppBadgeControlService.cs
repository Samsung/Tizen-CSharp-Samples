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
using BadgeCounter.Models;
using BadgeCounter.Tizen.Wearable.Services;
using global::Tizen.Applications;

[assembly: Xamarin.Forms.Dependency(typeof(CurrentAppBadgeControlService))]

namespace BadgeCounter.Tizen.Wearable.Services
{
    /// <summary>
    /// Service which allows to manage badge count for current application.
    /// </summary>
    class CurrentAppBadgeControlService : ICurrentAppBadgeControlService
    {
        #region fields

        /// <summary>
        /// Identifier of current application.
        /// Required to find and change badge count for it.
        /// </summary>
        private string _currentAppId;

        /// <summary>
        /// Badge information for current application.
        /// </summary>
        private Badge _currentAppBadge;

        #endregion

        #region properties

        /// <summary>
        /// Badge count for current application.
        /// </summary>
        public int BadgeCount
        {
            get => _currentAppBadge == null ? 0 : _currentAppBadge.Count;
            set
            {
                if (_currentAppBadge == null)
                {
                    return;
                }

                _currentAppBadge.Count = value;
                BadgeControl.Update(_currentAppBadge);
            }
        }

        /// <summary>
        /// Event invoked when badge count for current application was changed.
        /// </summary>
        public event EventHandler Changed;

        #endregion

        #region methods

        /// <summary>
        /// Creates instance of the service.
        /// </summary>
        public CurrentAppBadgeControlService()
        {
            _currentAppId = Application.Current.ApplicationInfo.ApplicationId;

            // try to find existing badge instance, if not exist create new one
            try
            {
                _currentAppBadge = BadgeControl.Find(_currentAppId);
            }
            catch (InvalidOperationException)
            {
                _currentAppBadge = new Badge(_currentAppId, count: 0, visible: true);
                BadgeControl.Add(_currentAppBadge);
            }

            BadgeControl.Changed += OnBadgeChanged;
        }

        /// <summary>
        /// Handles badge changes events.
        /// Filters for current application badge change and invokes own (service) event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void OnBadgeChanged(object sender, BadgeEventArgs e)
        {
            if (e.Badge.AppId != _currentAppId)
            {
                return;
            }

            _currentAppBadge = e.Badge;
            Changed?.Invoke(this, new EventArgs());
        }

        #endregion
    }
}
