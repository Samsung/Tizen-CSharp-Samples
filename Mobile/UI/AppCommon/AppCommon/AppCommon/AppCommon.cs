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

using Xamarin.Forms;
using AppCommon.Extensions;

namespace AppCommon
{
    /// <summary>
    /// A class for the application
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// The root page of your application
        /// </summary>
        /// <param name="width">the width of the device's screen</param>
        /// <param name="height">the height of the device's screen</param>
        public App(int width, int height)
        {
            /// A model for an ApplicationInformationPage
            ApplicationInformation = new ApplicationInformationViewModel();

            var applicationInformation = new ApplicationInformationPage();
            applicationInformation.BindingContext = ApplicationInformation;

            var paths = new PathsPage(width, height);
            paths.BindingContext = new PathsPageViewModel();

            var main = new ColoredTabbedPage
            {
                BarBackgroundColor = Color.FromRgb(180, 52, 127),
            };
            main.Children.Add(applicationInformation);
            main.Children.Add(paths);

            /// The main page of your application
            MainPage = main;
        }

        /// <summary>
        /// The model for the ApplicationInformationPage
        /// </summary>
        public ApplicationInformationViewModel ApplicationInformation { get; private set; }

        /// <summary>
        /// Handle when your application starts
        /// </summary>
        protected override void OnStart()
        {
        }

        /// <summary>
        /// Handle when your application sleeps
        /// </summary>
        protected override void OnSleep()
        {
        }

        /// <summary>
        /// Handle when your application resumes
        /// </summary>
        protected override void OnResume()
        {
        }
    }
}
