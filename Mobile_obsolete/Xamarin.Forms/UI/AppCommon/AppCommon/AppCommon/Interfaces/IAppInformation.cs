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

namespace AppCommon.Interfaces
{
    /// <summary>
    /// A interface about application information
    /// </summary>
    public interface IAppInformation
    {
        /// <summary>
        /// An application ID
        /// </summary>
        string ID { get; }

        /// <summary>
        /// An application name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// An application icon path
        /// </summary>
        string IconPath { get; }

        /// <summary>
        /// A path for cache data
        /// </summary>
        string CachePath { get; }

        /// <summary>
        /// A path for external cache data
        /// </summary>
        string ExternalCachePath { get; }

        /// <summary>
        /// A path for external data
        /// </summary>
        string ExternalDataPath { get; }

        /// <summary>
        /// A path for external shared data
        /// </summary>
        string ExternalSharedDataPath { get; }

        /// <summary>
        /// A path for resource data
        /// </summary>
        string ResourcePath { get; }

        /// <summary>
        /// A path for resources data
        /// </summary>
        string ResourcesPath { get; }

        /// <summary>
        /// A path for shared data
        /// </summary>
        string SharedDataPath { get; }

        /// <summary>
        /// A path for shared resource data
        /// </summary>
        string SharedResourcePath { get; }

        /// <summary>
        /// A path for shared trusted data
        /// </summary>
        string SharedTrustedPath { get; }
    }
}