/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd. All rights reserved.
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
using Xamarin.Forms;
using Pedometer.Views;
using Pedometer.Privilege;

namespace Pedometer.ViewModels
{
    /// <summary>
    /// Provides start page view abstraction
    /// </summary>
    public class StartViewModel : ViewModelBase
    {
        /// <summary>
        /// Navigates to main page
        /// </summary>
        public Command GoToMainPageCommand { get; }

        /// <summary>
        /// Initializes StartViewModel class instance
        /// </summary>
        public StartViewModel()
        {
            GoToMainPageCommand = new Command(ExecuteGoToMainPageCommand);
        }

        /// <summary>
        /// Handles execution of GoToMainPageCommand
        /// Navigates to main page
        /// </summary>
        private void ExecuteGoToMainPageCommand()
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}