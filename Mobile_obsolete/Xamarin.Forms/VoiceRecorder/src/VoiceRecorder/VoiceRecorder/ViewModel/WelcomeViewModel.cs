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
using VoiceRecorder.View;
using Xamarin.Forms;

namespace VoiceRecorder.ViewModel
{
    /// <summary>
    /// WelcomeViewModel class.
    /// Provides commands and methods responsible for welcome page view model state.
    /// </summary>
    public class WelcomeViewModel : ViewModelBase
    {
        #region properties

        /// <summary>
        /// Command which changes page to main application page.
        /// </summary>
        public Command WelcomePageCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes the view model for welcome page.
        /// </summary>
        public WelcomeViewModel()
        {
            WelcomePageCommand = new Command(ExecuteWelcomePageCommand);
        }

        /// <summary>
        /// Handles execution of "WelcomePageCommand".
        /// Changes page to main application page.
        /// </summary>
        private void ExecuteWelcomePageCommand()
        {
            Application.Current.MainPage = new NavigationPage(DependencyService.Get<IPageResolver>().MainTabbedPage);
        }

        #endregion
    }
}