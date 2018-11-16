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
using Camera.Model;
using Camera.Navigation;
using Camera.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Camera
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// Camera cross-platform application class.
    /// </summary>
    public partial class Camera : Application
    {
        #region properties

        /// <summary>
        /// An instance of the CameraViewModel class.
        /// </summary>
        public CameraViewModel AppMainViewModel { private set; get; }

        #endregion

        #region methods

        /// <summary>
        /// The application constructor.
        /// Checks camera privileges. Shows pop-up if permissions are not granted.
        /// Sets application main page and view model.
        /// </summary>
        public Camera()
        {
            InitializeComponent();
            PageNavigation pageNavigation = new PageNavigation();

            if (!DependencyService.Get<IPrivilegeService>().AllPermissionsGranted())
            {
                pageNavigation.NavigateToPrivilegeDeniedPage();
            }
            else
            {
                AppMainViewModel = new CameraViewModel(pageNavigation);
                pageNavigation.CreateMainPage();
            }
        }

        /// <summary>
        /// Starts camera preview on application resume.
        /// </summary>
        protected override void OnResume()
        {
            AppMainViewModel.StartCameraPreview();
            base.OnResume();
        }

        #endregion
    }
}