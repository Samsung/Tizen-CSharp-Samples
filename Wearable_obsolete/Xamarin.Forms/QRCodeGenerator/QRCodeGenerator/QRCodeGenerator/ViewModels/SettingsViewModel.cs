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
using QRCodeGenerator.Utils;
using Xamarin.Forms;
using System;

namespace QRCodeGenerator.ViewModels
{
    /// <summary>
    /// Provides settings page view abstraction.
    /// </summary>
    public class SettingsViewModel : ViewModelBase
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

        /// <summary>
        /// Backing field of Password property.
        /// </summary>
        private string _password;

        /// <summary>
        /// Backing field of Encryption property.
        /// </summary>
        private EncryptionType _encryption;

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
        /// Password text value.
        /// </summary>
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        /// <summary>
        /// Encryption enumeration value.
        /// </summary>
        public EncryptionType Encryption
        {
            get => _encryption;
            set => SetProperty(ref _encryption, value);
        }

        /// <summary>
        /// Navigates to page with SSID settings.
        /// </summary>
        public Command GoToSSIDPageCommand { get; }

        /// <summary>
        /// Navigates to page with password settings.
        /// </summary>
        public Command GoToPasswordPageCommand { get; }

        /// <summary>
        /// Navigates to page with encryption settings.
        /// </summary>
        public Command GoToEncryptionPageCommand { get; }

        /// <summary>
        /// Navigates to previous page.
        /// </summary>
        public Command GoToPreviousPageCommand { get; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes SettingsViewModel class instance.
        /// </summary>
        public SettingsViewModel()
        {
            _navigation = DependencyService.Get<IPageNavigation>();

            MainModel.Instance.SettingChanged += OnSettingValueChanged;

            GoToPreviousPageCommand = new Command(ExecuteGoToPreviousPageCommand);
            GoToSSIDPageCommand = new Command(ExecuteGoToSSIDPageCommand);
            GoToPasswordPageCommand = new Command(ExecuteGoToPasswordPageCommand, CanExecute);
            GoToEncryptionPageCommand = new Command(ExecuteGoToEncryptionPageCommand);

            UpdateProperties();
        }

        /// <summary>
        /// Handles execution of GoToSSIDPageCommand.
        /// Navigates to page with SSID settings.
        /// </summary>
        private void ExecuteGoToSSIDPageCommand()
        {
            _navigation.GoToSSIDPage();
        }

        /// <summary>
        /// Handles execution of GoToPasswordPageCommand.
        /// Navigates to page with password settings.
        /// </summary>
        private void ExecuteGoToPasswordPageCommand()
        {
            _navigation.GoToPasswordPage();
        }

        /// <summary>
        /// Handles execution of GoToEncryptionPageCommand.
        /// Navigates to page with encryption settings.
        /// </summary>
        private void ExecuteGoToEncryptionPageCommand()
        {
            _navigation.GoToEncryptionPage();
        }

        /// <summary>
        /// Handles execution of GoToPreviousPageCommand.
        /// Navigates to previous page.
        /// </summary>
        private void ExecuteGoToPreviousPageCommand()
        {
            _navigation.GoToPreviousPage();
        }

        /// <summary>
        /// Handles SettingValueChanged event from the model.
        /// Updates properties from model.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void OnSettingValueChanged(object sender, EventArgs e)
        {
            UpdateProperties();
            GoToPasswordPageCommand.ChangeCanExecute();
        }

        /// <summary>
        /// Updates properties from model.
        /// </summary>
        private void UpdateProperties()
        {
            SSID = MainModel.Instance.SSID;
            Password = MainModel.Instance.Password;
            Encryption = MainModel.Instance.Encryption;
        }

        /// <summary>
        /// Determines if GoToPasswordPageCommand can be executed.
        /// </summary>
        /// <returns>Bool value indicating if command execution is allowed.</returns>
        private bool CanExecute()
        {
            return Encryption != EncryptionType.None;
        }

        #endregion
    }
}