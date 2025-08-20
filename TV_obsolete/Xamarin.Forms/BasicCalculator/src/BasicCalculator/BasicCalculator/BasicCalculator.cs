/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

using BasicCalculator.ViewModels;
using BasicCalculator.Views;
using Xamarin.Forms;

namespace BasicCalculator
{
    /// <summary>
    /// Portable application part main class.
    /// </summary>
    public class App : Application
    {
        #region fields

        /// <summary>
        /// List of the keys maintained by the application.
        /// </summary>
        private readonly string[] _keyList = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

        #endregion

        #region methods

        /// <summary>
        /// Portable application part constructor method.
        /// The root page of application (creating MainPage object).
        /// Registers for keyboard events.
        /// </summary>
        public App()
        {
            MainPage = DependencyService.Get<IViewResolver>().GetRootPage();

            // Register for keyboard events
            if (MainPage.BindingContext is CalculatorViewModel)
            {
                KeyboardHandler keyboardHandler = new KeyboardHandler((CalculatorViewModel)MainPage.BindingContext);
                DependencyService.Get<IKeyboardService>().RegisterKeys(_keyList, keyboardHandler.KeyPressed);
            }
        }

        #endregion
    }
}
