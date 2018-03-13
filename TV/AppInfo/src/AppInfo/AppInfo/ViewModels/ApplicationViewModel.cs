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
using AppInfo.Models;

namespace AppInfo.ViewModels
{
    /// <summary>
    /// ApplicationViewModel class.
    /// Provides properties describing application provided by the Tizen Applications API.
    /// </summary>
    public class ApplicationViewModel
    {
        #region fields

        /// <summary>
        /// Reference to the instance of the PackageModel class.
        /// </summary>
        private PackageModel _appPackageModel;

        #endregion

        #region properties

        /// <summary>
        /// App property.
        /// </summary>
        public IApplication App { get; private set; }

        /// <summary>
        /// ApplicationId property.
        /// </summary>
        public string ApplicationId { get; private set; }

        /// <summary>
        /// PackageId property.
        /// </summary>
        public string PackageId { get; private set; }

        /// <summary>
        /// Label property.
        /// </summary>
        public string Label { get; private set; }

        /// <summary>
        /// ExecutablePath property.
        /// </summary>
        public string ExecutablePath { get; private set; }

        /// <summary>
        /// IconPath property.
        /// </summary>
        public string IconPath { get; private set; }

        /// <summary>
        /// ApplicationType property.
        /// </summary>
        public string ApplicationType { get; private set; }

        /// <summary>
        /// Metadata property.
        /// </summary>
        public IDictionary<string, string> Metadata { get; private set; }

        /// <summary>
        /// IsNoDisplay property.
        /// </summary>
        public bool IsNoDisplay { get; private set; }

        /// <summary>
        /// IsOnBoot property.
        /// </summary>
        public bool IsOnBoot { get; private set; }

        /// <summary>
        /// IsPreload property.
        /// </summary>
        public bool IsPreload { get; private set; }

        /// <summary>
        /// SharedDataPath property.
        /// </summary>
        public string SharedDataPath { get; private set; }

        /// <summary>
        /// SharedResourcePath property.
        /// </summary>
        public string SharedResourcePath { get; private set; }

        /// <summary>
        /// SharedTrustedPath property.
        /// </summary>
        public string SharedTrustedPath { get; private set; }

        /// <summary>
        /// ExternalSharedDataPath property.
        /// </summary>
        public string ExternalSharedDataPath { get; private set; }

        /// <summary>
        /// AppPackageViewModel property.
        /// Stores an instance of the PackageViewModel class
        /// that provides properties describing package the application corresponds to.
        /// </summary>
        public PackageViewModel AppPackageViewModel { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// ApplicationViewModel class constructor.
        /// Initializes all necessary data models.
        /// </summary>
        /// <param name="app">An instance of the IApplication class.</param>
        public ApplicationViewModel(IApplication app)
        {
            _appPackageModel = new PackageModel();

            App = app;
            ApplicationId = App.ApplicationId;
            PackageId = App.PackageId;
            Label = App.Label;
            ExecutablePath = App.ExecutablePath;
            IconPath = App.IconPath;
            ApplicationType = App.ApplicationType;
            Metadata = App.Metadata;
            IsNoDisplay = App.IsNoDisplay;
            IsOnBoot = App.IsOnBoot;
            IsPreload = App.IsPreload;
            SharedDataPath = App.SharedDataPath;
            SharedResourcePath = App.SharedResourcePath;
            SharedTrustedPath = App.SharedTrustedPath;
            ExternalSharedDataPath = App.ExternalSharedDataPath;

            AppPackageViewModel = new PackageViewModel(_appPackageModel.GetPackage(PackageId));
        }

        #endregion
    }
}