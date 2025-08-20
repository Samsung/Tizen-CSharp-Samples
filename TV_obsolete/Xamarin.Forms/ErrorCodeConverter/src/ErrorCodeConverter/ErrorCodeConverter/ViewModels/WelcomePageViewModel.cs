/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using ErrorCodeConverter.Interfaces;
using System.Windows.Input;
using Xamarin.Forms;

namespace ErrorCodeConverter.ViewModels
{
    /// <summary>
    /// View model providing abstraction of welcome page.
    /// </summary>
    public class WelcomePageViewModel
    {
        #region properties

        /// <summary>
        /// Command starting application.
        /// </summary>
        public ICommand StartCommand { get; }

        #endregion

        #region methods

        /// <summary>
        /// Class constructor.
        /// </summary>
        public WelcomePageViewModel()
        {
            StartCommand = new Command(ExecuteStartCommand);
        }

        /// <summary>
        /// Handles execution of StartCommand.
        /// Shows application's main page.
        /// </summary>
        private void ExecuteStartCommand()
        {
            DependencyService.Get<IViewNavigation>().ShowMainPage();
        }

        #endregion
    }
}
