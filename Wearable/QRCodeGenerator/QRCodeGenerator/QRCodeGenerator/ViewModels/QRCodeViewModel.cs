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
using Xamarin.Forms;

namespace QRCodeGenerator.ViewModels
{
    /// <summary>
    /// Provides QR page view abstraction.
    /// </summary>
    public class QRCodeViewModel
    {
        #region fields

        /// <summary>
        /// Reference to object handling navigation between pages obtained in constructor using DependencyService.
        /// </summary>
        private readonly IPageNavigation _navigation;

        #endregion

        #region properties

        /// <summary>
        /// Navigates to previous page.
        /// </summary>
        public Command GoToPreviousPageCommand { get; }

        /// <summary>
        /// QR code image path.
        /// </summary>
        public string ImagePath { get; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes QRCodeViewModel class instance.
        /// </summary>
        public QRCodeViewModel()
        {
            _navigation = DependencyService.Get<IPageNavigation>();
            GoToPreviousPageCommand = new Command(ExecuteGoToPreviousPageCommand);

            ImagePath = MainModel.Instance.Generate();
        }

        /// <summary>
        /// Handles execution of GoToPreviousPageCommand.
        /// Navigates to previous page.
        /// </summary>
        private void ExecuteGoToPreviousPageCommand()
        {
            _navigation.GoToPreviousPage();
        }

        #endregion
    }
}