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
    /// TizenPackageSizeInfo class.
    /// Defines properties describing package size provided by the Tizen Applications API.
    /// </summary>
    class TizenPackageSizeInfo : IPackageSizeInfo
    {
        #region fields

        /// <summary>
        /// An instance of PackageSizeInformation class.
        /// </summary>
        private PackageSizeInformation _source;

        #endregion

        #region properties

        /// <summary>
        /// DataSize property.
        /// </summary>
        public long DataSize => _source.DataSize;

        /// <summary>
        /// CacheSize property.
        /// </summary>
        public long CacheSize => _source.CacheSize;

        /// <summary>
        /// AppSize property.
        /// </summary>
        public long AppSize => _source.AppSize;

        /// <summary>
        /// ExternalDataSize property.
        /// </summary>
        public long ExternalDataSize => _source.ExternalDataSize;

        /// <summary>
        /// ExternalCacheSize property.
        /// </summary>
        public long ExternalCacheSize => _source.ExternalCacheSize;

        /// <summary>
        /// ExternalAppSize property.
        /// </summary>
        public long ExternalAppSize => _source.ExternalAppSize;

        #endregion

        #region methods

        /// <summary>
        /// TizenPackageSizeInfo class constructor.
        /// </summary>
        /// <param name="source">An instance of the PackageSizeInformation class.</param>
        internal TizenPackageSizeInfo(PackageSizeInformation source)
        {
            _source = source;
        }

        #endregion
    }
}