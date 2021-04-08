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
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Level
{
    /// <summary>
    /// Main class of Level application
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Initializes App class instance
        /// </summary>
        public App()
        {
            var navigationPage = new NavigationPage(new Views.WelcomePage());
            navigationPage.Popped += NavigationPage_Popped;
            MainPage = navigationPage;
        }

        /// <summary>
        /// Method calls dispose on popped page
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void NavigationPage_Popped(object sender, NavigationEventArgs e)
        {
            if (e.Page?.BindingContext is IDisposable viewModel)
            {
                viewModel.Dispose();
            }
        }
    }
}
