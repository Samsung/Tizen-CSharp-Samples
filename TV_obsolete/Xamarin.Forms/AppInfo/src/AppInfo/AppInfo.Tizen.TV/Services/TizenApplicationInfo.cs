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

using AppInfo.Models;
using Tizen.Applications;
using System.Collections.Generic;

namespace AppInfo.Tizen.TV.Services
{
    /// <summary>
    /// TizenApplicationInfo class.
    /// Defines properties describing application provided by the Tizen Applications API.
    /// </summary>
    class TizenApplicationInfo : IApplication
    {
        #region fields

        /// <summary>
        /// An instance of ApplicationInfo class.
        /// </summary>
        private ApplicationInfo _source;

        #endregion

        #region properties

        /// <summary>
        /// ApplicationId property.
        /// </summary>
        public string ApplicationId => _source.ApplicationId;

        /// <summary>
        /// PackageId property.
        /// </summary>
        public string PackageId => _source.PackageId;

        /// <summary>
        /// Label property.
        /// </summary>
        public string Label => _source.Label;

        /// <summary>
        /// ExecutablePath property.
        /// </summary>
        public string ExecutablePath => _source.ExecutablePath;

        /// <summary>
        /// IconPath property.
        /// </summary>
        public string IconPath => _source.IconPath;

        /// <summary>
        /// ApplicationType property.
        /// </summary>
        public string ApplicationType => _source.ApplicationType;

        /// <summary>
        /// Metadata property.
        /// </summary>
        public IDictionary<string, string> Metadata => _source.Metadata;

        /// <summary>
        /// IsNoDisplay property.
        /// </summary>
        public bool IsNoDisplay => _source.IsNoDisplay;

        /// <summary>
        /// IsOnBoot property.
        /// </summary>
        public bool IsOnBoot => _source.IsOnBoot;

        /// <summary>
        /// IsPreload property.
        /// </summary>
        public bool IsPreload => _source.IsPreload;

        /// <summary>
        /// SharedDataPath property.
        /// </summary>
        public string SharedDataPath => _source.SharedDataPath;

        /// <summary>
        /// SharedResourcePath property.
        /// </summary>
        public string SharedResourcePath => _source.SharedResourcePath;

        /// <summary>
        /// SharedTrustedPath property.
        /// </summary>
        public string SharedTrustedPath => _source.SharedTrustedPath;

        /// <summary>
        /// ExternalSharedDataPath property.
        /// </summary>
        public string ExternalSharedDataPath => _source.ExternalSharedDataPath;

        #endregion

        #region methods

        /// <summary>
        /// TizenApplicationInfo class constructor.
        /// </summary>
        /// <param name="source">An instance of the ApplicationInfo class.</param>
        internal TizenApplicationInfo(ApplicationInfo source)
        {
            _source = source;
        }

        #endregion
    }
}