/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using CircularUIMediaPlayer.Utilities;
using Tizen;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CircularUIMediaPlayer
{
    /// <summary>
    /// App class
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            GrantInternetUserPermission();
            MainPage = new NavigationPage(new CircularUIMediaPlayer.Views.MainPage());
        }

        async void GrantInternetUserPermission()
        {
            // Request user's permission for internet privilege
            // Check detailed info : https://developer.tizen.org/development/guides/.net-application/security/privacy-related-permissions
            // https://developer.tizen.org/development/training/.net-application/security-and-api-privileges
            UserPermission userperm = new UserPermission();
            var result = await userperm.CheckAndRequestPermission(Utility.InternetPrivilege);
            if (!result)
            {
                Log.Error(Utility.LOG_TAG, "Failed to obtain user consent.");
                // Terminate this application.
                Tizen.Applications.Application.Current.Exit();
            }
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
    }
}

