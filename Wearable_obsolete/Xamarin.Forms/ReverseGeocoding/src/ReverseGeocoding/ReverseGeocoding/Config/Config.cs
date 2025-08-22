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
namespace ReverseGeocoding
{
    /// <summary>
    /// Provides application settings.
    /// </summary>
    public static class Config
    {
        #region fields

        /// <summary>
        /// Map provider's name.
        /// </summary>
        private const string PROVIDER_NAME = "HERE";

        /// <summary>
        /// ID registered with provider.
        /// Please visit http://developer.here.com to obtain application id.
        /// </summary>
        private const string APP_ID = "hgTt8y5V91b7gWydK2df";

        /// <summary>
        /// Key assigned with application ID.
        /// Please visit http://developer.here.com to obtain application key.
        /// </summary>
        private const string APP_KEY = "YwiXR-_JkJ4R1yWz4eMjbw";

        #endregion

        #region properties

        /// <summary>
        /// Provider name.
        /// </summary>
        public static string ProviderName => PROVIDER_NAME;

        /// <summary>
        /// Authentication token used to initialize map library.
        /// </summary>
        public static string AuthenticationToken => APP_ID + "/" + APP_KEY;

        /// <summary>
        /// Map credentials indicator.
        /// True if map application ID and key are not empty.
        /// </summary>
        /// <returns>True if both fields are not empty.</returns>
        public static bool IsMapProviderConfigured() => APP_ID.Length > 0 && APP_KEY.Length > 0;

        #endregion
    }
}