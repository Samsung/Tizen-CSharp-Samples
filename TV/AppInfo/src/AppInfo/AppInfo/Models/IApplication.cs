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

using System.Collections.Generic;

namespace AppInfo.Models
{
    /// <summary>
    /// IApplication interface class.
    /// Defines properties describing application,
    /// provided by the Tizen Applications API.
    /// </summary>
    public interface IApplication
    {
        #region properties

        /// <summary>
        /// ApplicationId property.
        /// </summary>
        string ApplicationId { get; }

        /// <summary>
        /// PackageId property.
        /// </summary>
        string PackageId { get; }

        /// <summary>
        /// Label property.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// ExecutablePath property.
        /// </summary>
        string ExecutablePath { get; }

        /// <summary>
        /// IconPath property.
        /// </summary>
        string IconPath { get; }

        /// <summary>
        /// ApplicationType property.
        /// </summary>
        string ApplicationType { get; }

        /// <summary>
        /// Metadata property.
        /// </summary>
        IDictionary<string, string> Metadata { get; }

        /// <summary>
        /// IsNoDisplay property.
        /// </summary>
        bool IsNoDisplay { get; }

        /// <summary>
        /// IsOnBoot property.
        /// </summary>
        bool IsOnBoot { get; }

        /// <summary>
        /// IsPreload property.
        /// </summary>
        bool IsPreload { get; }

        /// <summary>
        /// SharedDataPath property.
        /// </summary>
        string SharedDataPath { get; }

        /// <summary>
        /// SharedResourcePath property.
        /// </summary>
        string SharedResourcePath { get; }

        /// <summary>
        /// SharedTrustedPath property.
        /// </summary>
        string SharedTrustedPath { get; }

        /// <summary>
        /// ExternalSharedDataPath property.
        /// </summary>
        string ExternalSharedDataPath { get; }

        #endregion
    }
}