/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
using ImageGallery.Models;
using ImageGallery.ViewModels;
using ImageGallery.Views;
using Xamarin.Forms;

namespace ImageGallery
{
    /// <summary>
    /// Portable application part main class.
    /// </summary>
    public class App : Application
    {
        #region properties

        /// <summary>
        /// AppMainViewModel property that provides the view model for the whole application.
        /// </summary>
        public MainViewModel AppMainViewModel { private set; get; }

        #endregion

        #region methods

        /// <summary>
        /// App class constructor.
        /// Creates the application's view model and initializes its main page.
        /// </summary>
        public App()
        {
            if (!PrivilegeState.MediastoragePermissionGranted)
            {
                MainPage = DependencyService.Get<IViewResolver>().GetPopupPage();
            }
            else
            {
                AppMainViewModel = new MainViewModel();
                MainPage = DependencyService.Get<IViewResolver>().GetRootPage();
            }
        }

        #endregion
    }
}
