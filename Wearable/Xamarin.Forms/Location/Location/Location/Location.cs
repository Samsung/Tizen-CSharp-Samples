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

namespace Location
{
    /// <summary>
    ///  The location application
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Initializes a new instance
        /// </summary>
        public App()
        {
            // The root page of your application
            MainPage = new LocationServices.InitializePage();
        }

        /// <summary>
        /// Called when your app starts.
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Called when your app sleeps.
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Called when your app resumes.
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
