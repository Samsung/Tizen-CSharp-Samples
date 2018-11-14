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
using QRCodeGenerator.Models;
using QRCodeGenerator.Views;
using System;
using Xamarin.Forms;

namespace QRCodeGenerator.ViewModels
{
    /// <summary>
    /// Provides main page view abstraction.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Reference to object handling navigation between pages obtained in constructor using DependencyService.
        /// </summary>
        private readonly IPageNavigation _navigation;

        /// <summary>
        /// Backing field of FirstView property.
        /// </summary>
        private bool _firstView;

        /// <summary>
        /// Backing field of GeneratingAllowed property.
        /// </summary>
        private bool _generatingAllowed;

        #endregion

        #region properties

        /// <summary>
        /// Navigates to settings page.
        /// </summary>
        public Command GoToSettingsCommand { get; }

        /// <summary>
        /// Navigates to page with generated QR Code.
        /// </summary>
        public Command GoToQRCommand { get; }

        /// <summary>
        /// Indicates if view is displayed for first time.
        /// </summary>
        public bool FirstView
        {
            get => _firstView;
            set => SetProperty(ref _firstView, value);
        }

        /// <summary>
        /// Indicates if generating QR code is allowed.
        /// </summary>
        public bool GeneratingAllowed
        {
            get => _generatingAllowed;
            set => SetProperty(ref _generatingAllowed, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes MainViewModel class instance.
        /// </summary>
        public MainViewModel()
        {
            _navigation = DependencyService.Get<IPageNavigation>();

            MainModel.Instance.SettingChanged += OnSettingValueChanged;

            GoToSettingsCommand = new Command(ExecuteGoToSettingsCommand);
            GoToQRCommand = new Command(ExecuteGoToQRCommand, CanExecute);

            FirstView = true;
            UpdateProperties();
        }

        /// <summary>
        /// Handles execution of GoToSettingsCommand.
        /// Navigates to settings page.
        /// </summary>
        private void ExecuteGoToSettingsCommand()
        {
            _navigation.GoToSettings();
            FirstView = false;
        }

        /// <summary>
        /// Handles execution of GoToQRCommand.
        /// Navigates to page with generated QR Code.
        /// </summary>
        private void ExecuteGoToQRCommand()
        {
            _navigation.GoToQR();
        }

        /// <summary>
        /// Handles SettingValueChanged event from the model.
        /// Changes CanExecute state of GoToQRCommand.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void OnSettingValueChanged(object sender, EventArgs e)
        {
            UpdateProperties();
            GoToQRCommand.ChangeCanExecute();
        }

        /// <summary>
        /// Updates properties from model.
        /// </summary>
        private void UpdateProperties()
        {
            GeneratingAllowed = MainModel.Instance.CanBeGenerated();
        }

        /// <summary>
        /// Determines if GoToQRCommand can be executed.
        /// </summary>
        /// <returns>Bool value indicating if command execution is allowed.</returns>
        private bool CanExecute()
        {
            return GeneratingAllowed;
        }

        #endregion
    }
}