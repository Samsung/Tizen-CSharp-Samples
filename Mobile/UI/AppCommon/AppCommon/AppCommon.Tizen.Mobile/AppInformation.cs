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

using AppCommon.Tizen.Mobile;
using AppCommon.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(AppInformation))]

namespace AppCommon.Tizen.Mobile
{
    /// <summary>
    /// A class for an application information
    /// </summary>
    class AppInformation : Xamarin.Forms.Platform.Tizen.FormsApplication, IAppInformation
    {
        /// <summary>
        /// An application ID
        /// </summary>
        public string ID
        {
            get
            {
                return Current.ApplicationInfo.ApplicationId;
            }
        }

        /// <summary>
        /// An application name
        /// </summary>
        public string AppName
        {
            get
            {
                return Current.ApplicationInfo.Label;
            }
        }

        /// <summary>
        /// An application icon path
        /// </summary>
        public string IconPath
        {
            get
            {
                return Current.ApplicationInfo.IconPath;
            }
        }

        /// <summary>
        /// A path for cache data
        /// </summary>
        public string CachePath
        {
            get
            {
                return Current.DirectoryInfo.Cache;
            }
        }

        /// <summary>
        /// A path for external cache data
        /// </summary>
        public string ExternalCachePath
        {
            get
            {
                return Current.DirectoryInfo.ExternalCache;
            }
        }

        /// <summary>
        /// A path for external data
        /// </summary>
        public string ExternalDataPath
        {
            get
            {
                return Current.DirectoryInfo.ExternalData;
            }
        }

        /// <summary>
        /// A path for external shared data
        /// </summary>
        public string ExternalSharedDataPath
        {
            get
            {
                return Current.DirectoryInfo.ExternalSharedData;
            }
        }

        /// <summary>
        /// A path for resource data
        /// </summary>
        public string ResourcePath
        {
            get
            {
                return Current.DirectoryInfo.Resource;
            }
        }

        /// <summary>
        /// A path for resources data
        /// </summary>
        public string ResourcesPath
        {
            get
            {
                return Current.DirectoryInfo.Resource;
            }
        }

        /// <summary>
        /// A path for shared data
        /// </summary>
        public string SharedDataPath
        {
            get
            {
                return Current.ApplicationInfo.SharedDataPath;
            }
        }

        /// <summary>
        /// A path for shared resource data
        /// </summary>
        public string SharedResourcePath
        {
            get
            {
                return Current.ApplicationInfo.SharedResourcePath;
            }
        }

        /// <summary>
        /// A path for shared trusted data
        /// </summary>
        public string SharedTrustedPath
        {
            get
            {
                return Current.ApplicationInfo.SharedTrustedPath;
            }
        }
    }
}