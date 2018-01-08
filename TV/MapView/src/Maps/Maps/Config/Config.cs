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

namespace Maps
{
    /// <summary>
    /// Provides application settings.
    /// </summary>
    public static class Config
    {
        #region fields

        /// <summary>
        /// Location privilege.
        /// </summary>
        private const string _LOCATION_PRIVILEGE = "http://tizen.org/privilege/location";

        /// <summary>
        /// Map provider's name.
        /// </summary>
        private const string _PROVIDER_NAME = "HERE";

        /// <summary>
        /// ID registered with provider.
        /// Please visit http://developer.here.com to obtain application id.
        /// </summary>
        private const string _APP_ID = "";

        /// <summary>
        /// Key assigned with application ID.
        /// Please visit http://developer.here.com to obtain application key.
        /// </summary>
        private const string _APP_KEY = "";

        #endregion

        #region properties

        /// <summary>
        /// Not configured map notification content.
        /// </summary>
        public static string MAP_NOT_CONFIGURED_MSG = "Map is not configured. \nPlease provide valid credentials " +
                                                      "\nin Maps/Config/Config.cs file. \n\nVisit http://developer.here.com.";

        /// <summary>
        /// Provider name.
        /// </summary>
        public static string ProviderName => _PROVIDER_NAME;

        /// <summary>
        /// Authentication token used to initialize map library.
        /// </summary>
        public static string AuthenticationToken => _APP_ID + "/" + _APP_KEY;

        /// <summary>
        /// Map credentials indicator.
        /// True if map application ID and key are not empty.
        /// </summary>
        /// <returns>True if both fields are not empty.</returns>
        public static bool IsMapProviderConfigured() => _APP_ID.Length > 0 && _APP_KEY.Length > 0;

        /// <summary>
        /// Location privilege
        /// </summary>
        public static string LocationPrivilege => _LOCATION_PRIVILEGE;

        #endregion
    }
}