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

using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace HeartRateMonitor.Navigation
{
    /// <summary>
    /// PageNavigation class.
    /// Provides commands that allows navigation through the application pages.
    /// </summary>
    public class PageNavigation
    {
        #region properties

        /// <summary>
        /// Switches application to the page given as a command parameter.
        /// </summary>
        public ICommand NavigateToCommand { private set; get; }

        /// <summary>
        /// Switches application to the previous page.
        /// </summary>
        public ICommand NavigateBackCommand { private set; get; }

        #endregion

        #region methods

        /// <summary>
        /// PageNavigation class constructor.
        /// </summary>
        /// <param name="navigation">Page Navigation object.</param>
        public PageNavigation(INavigation navigation)
        {
            NavigateToCommand = new Command<Type>(async (Type pageType) =>
            {
                Page page = (Page)Activator.CreateInstance(pageType);
                await navigation.PushAsync(page, false);
            });

            NavigateBackCommand = new Command(async () =>
            {
                await navigation.PopAsync();
            });
        }

        #endregion
    }
}