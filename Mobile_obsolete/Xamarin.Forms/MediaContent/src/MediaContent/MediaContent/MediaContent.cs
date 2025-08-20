/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
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

using MediaContent.Models;
using MediaContent.Navigation;
using MediaContent.ViewModels;
using MediaContent.Views;
using Xamarin.Forms;

namespace MediaContent
{
    /// <summary>
    /// Portable application part main class.
    /// </summary>
    public class App : Application
    {
        #region properties

        /// <summary>
        /// An instance of the MediaContentViewModel class.
        /// </summary>
        public MediaContentViewModel AppMediaContentViewModel { private set; get; }

        #endregion

        #region methods

        /// <summary>
        /// Portable application part constructor method. The root page of application (creating MainPage object).
        /// </summary>
        public App()
        {
            PageNavigation pageNavigation = new PageNavigation();

            if (DependencyService.Get<IPrivilegeService>().AllPermissionsGranted())
            {
                AppMediaContentViewModel = new MediaContentViewModel(pageNavigation);
                pageNavigation.CreateMainPage();
            }
            else
            {
                pageNavigation.NavigateToPrivilegeDeniedPage();
            }
        }

        #endregion
    }
}
