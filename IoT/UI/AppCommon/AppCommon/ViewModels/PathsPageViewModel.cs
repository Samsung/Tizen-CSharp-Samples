/*
 * Copyright (c) 2025 Samsung Electronics Co., Ltd All Rights Reserved
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

using System.Collections.Generic;
using AppCommon.Models;
using AppCommon.Services;

namespace AppCommon.ViewModels
{
    /// <summary>
    /// ViewModel for paths page
    /// </summary>
    public class PathsPageViewModel
    {
        private IAppInformationService _appInfo;
        private IDirectoryService _directory;

        public PathsPageViewModel()
        {
            Initialize();
        }

        public List<PathInformation> Paths { get; set; }

        public int GetFilesCount(string path)
        {
            try
            {
                return _directory.GetFiles(path).Length;
            }
            catch
            {
                return 0;
            }
        }

        void Initialize()
        {
            Paths = new List<PathInformation>();
            _appInfo = ServiceLocator.Get<IAppInformationService>();
            _directory = ServiceLocator.Get<IDirectoryService>();

            SetListItems();
        }

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
