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

using Tizen.Applications;

namespace AppCommon.Services
{
    /// <summary>
    /// Implementation of application information service
    /// </summary>
    public class AppInformationService : IAppInformationService
    {
        private static readonly Application Current = Application.Current;

        public string ID
        {
            get { return Current.ApplicationInfo.ApplicationId; }
        }

        public string Name
        {
            get { return Current.ApplicationInfo.Label; }
        }

        public string IconPath
        {
            get { return Current.ApplicationInfo.IconPath; }
        }

        public string CachePath
        {
            get { return Current.DirectoryInfo.Cache; }
        }

        public string ExternalCachePath
        {
            get { return Current.DirectoryInfo.ExternalCache; }
        }

        public string ExternalDataPath
        {
            get { return Current.DirectoryInfo.ExternalData; }
        }

        public string ExternalSharedDataPath
        {
            get { return Current.DirectoryInfo.ExternalSharedData; }
        }

        public string ResourcePath
        {
            get { return Current.DirectoryInfo.Resource; }
        }

        public string ResourcesPath
        {
            get { return Current.DirectoryInfo.Resource; }
        }

        public string SharedDataPath
        {
            get { return Current.ApplicationInfo.SharedDataPath; }
        }

        public string SharedResourcePath
        {
            get { return Current.ApplicationInfo.SharedResourcePath; }
        }

        public string SharedTrustedPath
        {
            get { return Current.ApplicationInfo.SharedTrustedPath; }
        }
    }
}
