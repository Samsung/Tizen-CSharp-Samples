/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace Downloader
{
    /// <summary>
    /// Main page of the application.
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// There is the tabbed page including a download page and a download information page.
        /// </summary>
        public App()
        {
            // Download main page
            var download = new DownloadMainPage();
            // Download information page
            var downloadInfo = new DownloadInfoPage();

            // The root page of your application
            MainPage = new IndexPage
            {
                Children =
                {
                    download,
                    downloadInfo,
                }
            };
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
