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
using System.Linq;

namespace AppInfo.ViewModels
{
    /// <summary>
    /// MainViewModel class.
    /// Provides commands and methods responsible for application view model state.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Backing field of the ApplicationCollection property.
        /// </summary>
        private List<ApplicationViewModel> _applicationCollection;

        /// <summary>
        /// Backing field of the SelectedApp property.
        /// </summary>
        private ApplicationViewModel _selectedApp;

        /// <summary>
        /// Backing field of the IsAppSelected property.
        /// </summary>
        private bool _isAppSelected = false;

        /// <summary>
        /// Backing field of the IsListOfAppsLoaded property.
        /// </summary>
        private bool _isListOfAppsLoaded = false;

        /// <summary>
        /// Backing field of the AppPackageViewModel property.
        /// </summary>
        private PackageViewModel _appPackageViewModel;

        /// <summary>
        /// Backing field of the AppPackageSizeInfoViewModel property.
        /// </summary>
        private PackageSizeInfoViewModel _appPackageSizeInfoViewModel;

        /// <summary>
        /// Reference to the instance of the ApplicationModel class.
        /// </summary>
        private ApplicationModel _appApplicationModel;

        /// <summary>
        /// Reference to the instance of the PackageModel class.
        /// </summary>
        private PackageModel _appPackageModel;

        #endregion

        #region properties

        /// <summary>
        /// AppPackageSizeInfoViewModel property.
        /// Stores an instance of the PackageSizeInfoViewModel
        /// describing size of the package corresponding to the currently selected application.
        /// </summary>
        public PackageSizeInfoViewModel AppPackageSizeInfoViewModel
        {
            set { SetProperty(ref _appPackageSizeInfoViewModel, value); }
            get { return _appPackageSizeInfoViewModel; }
        }

        /// <summary>
        /// AppPackageViewModel property.
        /// Stores an instance of the PackageViewModel
        /// describing package corresponding to the currently selected application.
        /// </summary>
        public PackageViewModel AppPackageViewModel
        {
            set { SetProperty(ref _appPackageViewModel, value); }
            get { return _appPackageViewModel; }
        }

        /// <summary>
        /// ApplicationCollection property.
        /// Stores list of the installed applications.
        /// </summary>
        public List<ApplicationViewModel> ApplicationCollection
        {
            set { SetProperty(ref _applicationCollection, value); }
            get { return _applicationCollection; }
        }

        /// <summary>
        /// SelectedApp property.
        /// Stores an instance of the ApplicationViewModel
        /// describing currently selected application.
        /// </summary>
        public ApplicationViewModel SelectedApp
        {
            set
            {
                SetProperty(ref _selectedApp, value);

                if (_selectedApp == null)
                {
                    return;
                }

                SelectApp(_selectedApp);
            }

            get { return _selectedApp; }
        }

        /// <summary>
        /// IsAppSelected property.
        /// Indicates whether any application is selected or not.
        /// </summary>
        public bool IsAppSelected
        {
            set { SetProperty(ref _isAppSelected, value); }
            get { return _isAppSelected; }
        }

        /// <summary>
        /// IsListOfAppsLoaded property.
        /// Indicates whether list of applications is loaded or not.
        /// </summary>
        public bool IsListOfAppsLoaded
        {
            set { SetProperty(ref _isListOfAppsLoaded, value); }
            get { return _isListOfAppsLoaded; }
        }

        #endregion

        #region methods

        /// <summary>
        /// MainViewModel class constructor.
        /// Initializes all necessary data models.
        /// Executes GetListOfApps method.
        /// </summary>
        public MainViewModel()
        {
            _appApplicationModel = new ApplicationModel();
            _appPackageModel = new PackageModel();

            GetListOfApps();
        }

        /// <summary>
        /// Selects application based on an instance of the ApplicationViewModel given as parameter.
        /// </summary>
        /// <param name="selectedApp">An instance of the ApplicationViewModel class.</param>
        private async void SelectApp(ApplicationViewModel selectedApp)
        {
            AppPackageSizeInfoViewModel =
                new PackageSizeInfoViewModel(await _appPackageModel.GetPackageSizeInfo(selectedApp.App));
            AppPackageViewModel = selectedApp.AppPackageViewModel;
            IsAppSelected = true;
        }

        /// <summary>
        /// Starts process of obtaining information about installed applications.
        /// </summary>
        private async void GetListOfApps()
        {
            var appList = await _appApplicationModel.GetApps();

            if (appList == null)
            {
                return;
            }

            ApplicationCollection = appList.Select((item) => new ApplicationViewModel(item)).ToList();
            IsListOfAppsLoaded = true;
        }

        #endregion
    }
}