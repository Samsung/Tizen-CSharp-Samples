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
    /// Provides password page view abstraction.
    /// </summary>
    public class PasswordViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Reference to object handling navigation between pages obtained in constructor using DependencyService.
        /// </summary>
        private readonly IPageNavigation _navigation;

        /// <summary>
        /// Backing field of Password property.
        /// </summary>
        private string _password;

        /// <summary>
        /// Backing field of PasswordVisible property.
        /// </summary>
        private bool _passwordVisible = false;

        #endregion

        #region properties

        /// <summary>
        /// Navigates to previous page.
        /// </summary>
        public Command GoToPreviousPageCommand { get; }

        /// <summary>
        /// Password text value.
        /// </summary>
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        /// <summary>
        /// Indicates if password is visible.
        /// </summary>
        public bool PasswordVisible
        {
            get => _passwordVisible;
            set => SetProperty(ref _passwordVisible, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes PasswordViewModel class instance.
        /// </summary>
        public PasswordViewModel()
        {
            _navigation = DependencyService.Get<IPageNavigation>();

            Password = MainModel.Instance.Password;

            GoToPreviousPageCommand = new Command(ExecuteGoToPreviousPageCommand);
        }

        /// <summary>
        /// Handles execution of GoToPreviousPageCommand.
        /// Navigates to previous page.
        /// </summary>
        private void ExecuteGoToPreviousPageCommand()
        {
            MainModel.Instance.Password = Password;
            _navigation.GoToPreviousPage();
        }

        #endregion
    }
}
