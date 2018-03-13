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

namespace AppInfo.ViewModels
{
    /// <summary>
    /// PackageSizeInfoViewModel class.
    /// Provides properties describing package size provided by the Tizen Applications API.
    /// </summary>
    public class PackageSizeInfoViewModel
    {
        #region properties

        /// <summary>
        /// PackageSizeInfo property.
        /// </summary>
        public IPackageSizeInfo PackageSizeInfo { private set; get; }

        /// <summary>
        /// PackageDataSize property.
        /// </summary>
        public long PackageDataSize { private set; get; }

        /// <summary>
        /// PackageCacheSize property.
        /// </summary>
        public long PackageCacheSize { private set; get; }

        /// <summary>
        /// PackageAppSize property.
        /// </summary>
        public long PackageAppSize { private set; get; }

        /// <summary>
        /// PackageExternalDataSize property.
        /// </summary>
        public long PackageExternalDataSize { private set; get; }

        /// <summary>
        /// PackageExternalCacheSize property.
        /// </summary>
        public long PackageExternalCacheSize { private set; get; }

        /// <summary>
        /// PackageExternalAppSize property.
        /// </summary>
        public long PackageExternalAppSize { private set; get; }

        #endregion

        #region methods

        /// <summary>
        /// PackageSizeInfoViewModel class constructor.
        /// </summary>
        /// <param name="packageSizeInfo">An instance of the IPackageSizeInfo class.</param>
        public PackageSizeInfoViewModel(IPackageSizeInfo packageSizeInfo)
        {
            if (packageSizeInfo == null)
            {
                return;
            }

            PackageSizeInfo = packageSizeInfo;
            PackageDataSize = packageSizeInfo.DataSize;
            PackageCacheSize = packageSizeInfo.CacheSize;
            PackageAppSize = packageSizeInfo.AppSize;
            PackageExternalDataSize = packageSizeInfo.ExternalDataSize;
            PackageExternalCacheSize = packageSizeInfo.ExternalCacheSize;
            PackageExternalAppSize = packageSizeInfo.ExternalAppSize;
        }

        #endregion
    }
}