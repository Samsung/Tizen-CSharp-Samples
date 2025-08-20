/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Xamarin.Forms;
using AppCommon.Interfaces;
using System.Collections.Generic;

namespace AppCommon
{
    /// <summary>
    /// A class about Path Information Data
    /// </summary>
    public class PathsPageViewModel
    {
        IAppInformation _appInfo;
        IDirectory _directory;

        /// <summary>
        /// A constructor for PathsPageModel
        /// </summary>
        public PathsPageViewModel()
        {
            Initialize();
        }

        /// <summary>
        /// The list for the path's information
        /// </summary>
        public List<PathInformation> Paths { get; set; }

        /// <summary>
        /// To get how many files in the path
        /// </summary>
        /// <param name="path">A directory path to count files in the directory</param>
        /// <returns>The number of the files</returns>
        public int GetFilesCount(string path)
        {
            return _directory.GetFiles(path).Length;
        }

        /// <summary>
        /// To initialize fields when the class is instantiated
        /// </summary>
        void Initialize()
        {
            Paths = new List<PathInformation>();
            _appInfo = DependencyService.Get<IAppInformation>();
            _directory = DependencyService.Get<IDirectory>();

            SetListItems();
        }

        /// <summary>
        /// Set each path information to a list
        /// </summary>
        private void SetListItems()
        {
            Paths.Add(new PathInformation
            {
                Title = "Resources",
                Path = _appInfo.ResourcesPath
            });

            Paths.Add(new PathInformation
            {
                Title = "Cache",
                Path = _appInfo.CachePath
            });

            Paths.Add(new PathInformation
            {
                Title = "Shared Data",
                Path = _appInfo.SharedDataPath
            });

            Paths.Add(new PathInformation
            {
                Title = "Shared Resource",
                Path = _appInfo.SharedResourcePath
            });

            Paths.Add(new PathInformation
            {
                Title = "Shared Trusted",
                Path = _appInfo.SharedTrustedPath
            });

            Paths.Add(new PathInformation
            {
                Title = "External Data",
                Path = _appInfo.ExternalDataPath
            });

            Paths.Add(new PathInformation
            {
                Title = "External Cache",
                Path = _appInfo.ExternalCachePath
            });

            Paths.Add(new PathInformation
            {
                Title = "External Shared Data",
                Path = _appInfo.ExternalSharedDataPath
            });
        }
    }
}