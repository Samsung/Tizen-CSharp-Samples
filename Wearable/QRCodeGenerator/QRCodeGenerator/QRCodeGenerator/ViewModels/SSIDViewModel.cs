/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
using QRCodeGenerator.Views;
using QRCodeGenerator.Models;
using Xamarin.Forms;

namespace QRCodeGenerator.ViewModels
{
    /// <summary>
    /// Provides service set identifier page view abstraction.
    /// </summary>
    public class SSIDViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Reference to object handling navigation between pages obtained in constructor using DependencyService.
        /// </summary>
        private readonly IPageNavigation _navigation;

        /// <summary>
        /// Backing field of SSID property.
        /// </summary>
        private string _ssid;

        #endregion

        #region properties

        /// <summary>
        /// SSID text value.
        /// </summary>
        public string SSID
        {
            get => _ssid;
            set => SetProperty(ref _ssid, value);
        }

        /// <summary>
        /// Navigates to previous page.
        /// </summary>
        public Command GoToPreviousPageCommand { get; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes SSIDViewModel class instance.
        /// </summary>
        public SSIDViewModel()
        {
            _navigation = DependencyService.Get<IPageNavigation>();

            SSID = MainModel.Instance.SSID;

            GoToPreviousPageCommand = new Command(ExecuteGoToPreviousPageCommand);
        }

        /// <summary>
        /// Handles execution of GoToPreviousPageCommand.
        /// Navigates to previous page.
        /// </summary>
        private void ExecuteGoToPreviousPageCommand()
        {
            MainModel.Instance.SSID = SSID;
            _navigation.GoToPreviousPage();
        }

        #endregion
    }
}