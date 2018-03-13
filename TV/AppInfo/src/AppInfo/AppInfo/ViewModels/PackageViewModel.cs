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
    /// PackageViewModel class.
    /// Provides properties describing package provided by the Tizen Applications API.
    /// </summary>
    public class PackageViewModel
    {
        #region properties

        /// <summary>
        /// Package property.
        /// </summary>
        public IPackage Package { get; private set; }

        /// <summary>
        /// PackageId property.
        /// </summary>
        public string PackageId { get; private set; }

        /// <summary>
        /// Label property.
        /// </summary>
        public string Label { get; private set; }

        /// <summary>
        /// IconPath property.
        /// </summary>
        public string IconPath { get; private set; }

        /// <summary>
        /// Version property.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// PackageType property.
        /// </summary>
        public string PackageType { get; private set; }

        /// <summary>
        /// InstalledStorageType property.
        /// </summary>
        public string InstalledStorageType { get; private set; }

        /// <summary>
        /// RootPath property.
        /// </summary>
        public string RootPath { get; private set; }

        /// <summary>
        /// TizenExpansionPackageName property.
        /// </summary>
        public string TizenExpansionPackageName { get; private set; }

        /// <summary>
        /// IsSystemPackage property.
        /// </summary>
        public bool IsSystemPackage { get; private set; }

        /// <summary>
        /// IsRemovable property.
        /// </summary>
        public bool IsRemovable { get; private set; }

        /// <summary>
        /// IsPreloaded property.
        /// </summary>
        public bool IsPreloaded { get; private set; }

        /// <summary>
        /// IsAccessible property.
        /// </summary>
        public bool IsAccessible { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// PackageViewModel class constructor.
        /// </summary>
        /// <param name="package">An instance of the IPackage class.</param>
        public PackageViewModel(IPackage package)
        {
            if (package == null)
            {
                return;
            }

            Package = package;
            PackageId = Package.PackageId;
            Label = Package.Label;
            IconPath = Package.IconPath;
            Version = Package.Version;
            InstalledStorageType = Package.InstalledStorageType;
            RootPath = Package.RootPath;
            TizenExpansionPackageName = Package.TizenExpansionPackageName;
            IsSystemPackage = Package.IsSystemPackage;
            IsRemovable = Package.IsRemovable;
            IsPreloaded = Package.IsPreloaded;
            IsAccessible = Package.IsAccessible;

            switch (Package.PackageType)
            {
                case "TPK":
                    PackageType = "Tizen native application";
                    break;
                case "WGT":
                    PackageType = "Tizen web/hybrid application";
                    break;
                default:
                    PackageType = "Unknown";
                    break;
            }
        }

        #endregion
    }
}