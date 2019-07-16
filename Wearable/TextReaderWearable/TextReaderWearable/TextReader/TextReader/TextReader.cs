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

using TextReader.Views;
using Xamarin.Forms;

namespace TextReader
{
    /// <summary>
    /// Application main class.
    /// </summary>
    public class App : Application
    {
        #region methods

        /// <summary>
        /// Initializes application.
        /// </summary>
        public App()
        {
            DependencyService.Get<IPageNavigation>().NavigateToWelcomePage();
        }

        /// <summary>
        /// Publishes a 'sleep' message.
        /// </summary>
        protected override void OnSleep()
        {
            base.OnSleep();

            MessagingCenter.Send<Application>(this, "sleep");
        }

        /// <summary>
        /// Publishes a 'resume' message.
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();

            MessagingCenter.Send<Application>(this, "resume");
        }

        #endregion
    }
}
