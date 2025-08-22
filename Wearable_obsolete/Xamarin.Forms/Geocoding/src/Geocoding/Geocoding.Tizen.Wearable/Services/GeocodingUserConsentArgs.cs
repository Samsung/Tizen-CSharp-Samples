/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
using Geocoding.ViewModels;
using System;

namespace Geocoding.Tizen.Wearable.Services
{
    /// <summary>
    /// GeocodingUserConsentArgs class.
    /// Defines structure of object being parameter of the UserConsent event.
    /// </summary>
    class GeocodingUserConsentArgs : EventArgs, IGeocodingUserConsentArgs
    {
        #region properties

        /// <summary>
        /// Consent status.
        /// </summary>
        public bool IsConsent { get; }

        #endregion

        #region methods

        /// <summary>
        /// GeocodingUserConsentArgs class constructor.
        /// </summary>
        /// <param name="isConsent">Consent status value.</param>
        public GeocodingUserConsentArgs(bool isConsent)
        {
            IsConsent = isConsent;
        }

        #endregion
    }
}
