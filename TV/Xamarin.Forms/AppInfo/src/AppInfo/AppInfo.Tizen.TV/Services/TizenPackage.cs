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

namespace AppInfo.Tizen.TV.Services
{
    /// <summary>
    /// TizenPackage class.
    /// Defines properties describing package provided by the Tizen Applications API.
    /// </summary>
    class TizenPackage : IPackage
    {
        #region fields

        /// <summary>
        /// An instance of Package class.
        /// </summary>
        private Package _source;

        #endregion

        #region properties

        /// <summary>
        /// PackageId property.
        /// </summary>
        public string PackageId => _source.Id;

        /// <summary>
        /// Label property.
        /// </summary>
        public string Label => _source.Label;

        /// <summary>
        /// IconPath property.
        /// </summary>
        public string IconPath => _source.IconPath;

        /// <summary>
        /// Version property.
        /// </summary>
        public string Version => _source.Version;

        /// <summary>
        /// PackageType property.
        /// </summary>
        public string PackageType => _source.PackageType.ToString();

        /// <summary>
        /// InstalledStorageType property.
        /// </summary>
        public string InstalledStorageType => _source.InstalledStorageType.ToString();

        /// <summary>
        /// RootPath property.
        /// </summary>
        public string RootPath => _source.RootPath;

        /// <summary>
        /// TizenExpansionPackageName property.
        /// </summary>
        public string TizenExpansionPackageName => _source.TizenExpansionPackageName;

        /// <summary>
        /// IsSystemPackage property.
        /// </summary>
        public bool IsSystemPackage => _source.IsSystemPackage;

        /// <summary>
        /// IsRemovable property.
        /// </summary>
        public bool IsRemovable => _source.IsRemovable;

        /// <summary>
        /// IsPreloaded property.
        /// </summary>
        public bool IsPreloaded => _source.IsPreloaded;

        /// <summary>
        /// IsAccessible property.
        /// </summary>
        public bool IsAccessible => _source.IsAccessible;

        #endregion

        #region methods

        /// <summary>
        /// TizenPackage class constructor.
        /// </summary>
        /// <param name="source">An instance of the Package class.</param>
        internal TizenPackage(Package source)
        {
            _source = source;
        }

        #endregion
    }
}