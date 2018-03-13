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

namespace AppInfo.Models
{
    /// <summary>
    /// IPackageSizeInfo interface class.
    /// Defines properties describing package size,
    /// provided by the Tizen Applications API.
    /// </summary>
    public interface IPackageSizeInfo
    {
        #region properties

        /// <summary>
        /// DataSize property.
        /// </summary>
        long DataSize { get; }

        /// <summary>
        /// CacheSize property.
        /// </summary>
        long CacheSize { get; }

        /// <summary>
        /// AppSize property.
        /// </summary>
        long AppSize { get; }

        /// <summary>
        /// ExternalDataSize property.
        /// </summary>
        long ExternalDataSize { get; }

        /// <summary>
        /// ExternalCacheSize property.
        /// </summary>
        long ExternalCacheSize { get; }

        /// <summary>
        /// ExternalAppSize property.
        /// </summary>
        long ExternalAppSize { get; }

        #endregion
    }
}