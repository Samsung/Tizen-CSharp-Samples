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

using ImageViewer.Views;
using System.Threading.Tasks;
using Tizen.Security;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImageViewer
{
    /// <summary>
    /// ImageViewer Application class
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        private const string SAMPLE_LOG_TAG = "ImageViewer";

        /// <summary>
        /// The media storage privilege key.
        /// </summary>
        private const string MEDIASTORAGE_PRIVILEGE = "http://tizen.org/privilege/mediastorage";

        private MainListPage _mainPage;
        public App()
        {
            InitializeComponent();

            AskForPermissions();   
            MainPage = _mainPage = new MainListPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        /// <summary>
        /// Triggers required permission requests.
        /// </summary>
        private void AskForPermissions()
        {
            var result = PrivacyPrivilegeManager.CheckPermission(MEDIASTORAGE_PRIVILEGE);
            if (result != CheckResult.Allow)
            {
                RequestPermission();
            }
        }

        private Task RequestPermission()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            var response = PrivacyPrivilegeManager.GetResponseContext(MEDIASTORAGE_PRIVILEGE);
            PrivacyPrivilegeManager.ResponseContext target;
            response.TryGetTarget(out target);
            target.ResponseFetched += (s, e) =>
            {
                tcs.SetResult(true);
                _mainPage.RestartScan();
            };
            PrivacyPrivilegeManager.RequestPermission(MEDIASTORAGE_PRIVILEGE);

            return tcs.Task;
        }
    }
}

