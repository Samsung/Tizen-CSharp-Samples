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

﻿using System;
using System.Linq;
using System.Reflection;
using Ultraviolet.Interfaces;
using Ultraviolet.Tizen.Wearable.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(NavigationService))]
namespace Ultraviolet.Tizen.Wearable.Services
{
    /// <summary>
    /// Provides navigation between pages across application.
    /// </summary>
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// Field storing navigation page.
        /// </summary>
        private readonly NavigationPage _navigationPage;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public NavigationService()
        {
            _navigationPage = new NavigationPage();
            App.Current.MainPage = _navigationPage;
        }

        /// <summary>
        /// Navigate to a Page based on a ViewModel type.
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel type.</typeparam>
        /// <param name="viewModel">Instance of view model.</param>
        /// <param name="clearStack">After navigating to a new page, remove all previous pages from NavigationStack.</param>
        public void NavigateTo<TViewModel>(TViewModel viewModel = null, bool clearStack = false) where TViewModel : ViewModels.BaseViewModel
        {
            string pageName = typeof(TViewModel).Name.Replace("ViewModel", "Page");

            Type pageType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.Name.EndsWith(pageName))
                .SingleOrDefault();
            if (pageType == null)
            {
                return;
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            if (viewModel != null)
            {
                page.BindingContext = viewModel;
            }

            NavigationPage.SetHasNavigationBar(page, false);
            _navigationPage.PushAsync(page);

            if (clearStack)
            {
                while (_navigationPage.Navigation.NavigationStack.Count > 1)
                {
                    _navigationPage.Navigation.RemovePage(_navigationPage.Navigation.NavigationStack.FirstOrDefault());
                }
            }
        }
    }
}
