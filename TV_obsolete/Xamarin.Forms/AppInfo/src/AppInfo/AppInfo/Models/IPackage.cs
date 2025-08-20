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
    /// IPackage interface class.
    /// Defines properties describing package,
    /// provided by the Tizen Applications API.
    /// </summary>
    public interface IPackage
    {
        #region properties

        /// <summary>
        /// PackageId property.
        /// </summary>
        string PackageId { get; }

        /// <summary>
        /// Label property.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// IconPath property.
        /// </summary>
        string IconPath { get; }

        /// <summary>
        /// Version property.
        /// </summary>
        string Version { get; }

        /// <summary>
        /// PackageType property.
        /// </summary>
        string PackageType { get; }

        /// <summary>
        /// InstalledStorageType property.
        /// </summary>
        string InstalledStorageType { get; }

        /// <summary>
        /// RootPath property.
        /// </summary>
        string RootPath { get; }

        /// <summary>
        /// TizenExpansionPackageName property.
        /// </summary>
        string TizenExpansionPackageName { get; }

        /// <summary>
        /// IsSystemPackage property.
        /// </summary>
        bool IsSystemPackage { get; }

        /// <summary>
        /// IsRemovable property.
        /// </summary>
        bool IsRemovable { get; }

        /// <summary>
        /// IsPreloaded property.
        /// </summary>
        bool IsPreloaded { get; }

        /// <summary>
        /// IsAccessible property.
        /// </summary>
        bool IsAccessible { get; }

        #endregion
    }
}