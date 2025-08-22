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

using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppInfo.Models
{
    /// <summary>
    /// PackageModel class.
    /// Defines methods that allow the application to access
    /// detailed information about installed packages,
    /// provided by the Tizen Applications API.
    /// </summary>
    public class PackageModel
    {
        #region methods

        /// <summary>
        /// Returns information about installed package based on its id.
        /// </summary>
        /// <param name="packageId">Package id.</param>
        /// <returns>An instance of the IPackage class.</returns>
        public IPackage GetPackage(string packageId)
        {
            return DependencyService.Get<IPackageService>().GetPackage(packageId);
        }

        /// <summary>
        /// Returns an object describing package size
        /// based on the corresponding instance of IApplication class.
        /// </summary>
        /// <param name="application">An instance of IApplication class.</param>
        /// <returns>List of instances of the IPackageSizeInfo class.</returns>
        public async Task<IPackageSizeInfo> GetPackageSizeInfo(IApplication application)
        {
            return await DependencyService.Get<IPackageService>().GetPackageSizeInfo(application);
        }

        #endregion
    }
}