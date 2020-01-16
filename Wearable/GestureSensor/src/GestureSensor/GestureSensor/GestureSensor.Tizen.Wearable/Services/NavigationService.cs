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

﻿using GestureSensor.Interfaces;
using GestureSensor.Tizen.Wearable.Services;
using Xamarin.Forms;
using System.Linq;
using System;
using System.Reflection;
using Tizen.Wearable.CircularUI.Forms;
using GestureSensor.ViewModels;
using GestureSensor.Tizen.Wearable.Views;

[assembly: Dependency(typeof(NavigationService))]
namespace GestureSensor.Tizen.Wearable.Services
{
    /// <summary>
    /// Provides navigation between pages across application.
    /// </summary>
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// Name of the assembly containing ViewModels.
        /// </summary>
        private const string ViewModelAssemblyName = "GestureSensor";

        private readonly NavigationPage _navigationPage;
        private readonly IndexPage _indexPage;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationService"/> class.
        /// </summary>
        public NavigationService()
        {
            _navigationPage = new NavigationPage();
            _indexPage = new IndexPage();
            App.Current.MainPage = _navigationPage;
        }

        /// <summary>
        /// Navigate to a Page based on a ViewModel type.
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel type.</typeparam>
        /// <param name="viewModel">Instance of view model.</param>
        /// <param name="clearStack">After navigating to a new page, remove all previous pages from NavigationStack.</param>
        public void NavigateTo<TViewModel>(TViewModel viewModel = null, bool clearStack = false) where TViewModel : BaseViewModel
        {
            if (typeof(TViewModel).IsSubclassOf(typeof(SingleGestureViewModel)))
            {
                InitializeIndexPage();
                NavigateToIndexPage(viewModel);
            }
            else
            {
                NavigateToNavigationStack(viewModel);
            }

            if (clearStack)
            {
                while (_navigationPage.Navigation.NavigationStack.Count > 1)
                {
                    _navigationPage.Navigation.RemovePage(_navigationPage.Navigation.NavigationStack.FirstOrDefault());
                }
            }
        }

        /// <summary>
        /// Initializes <see cref="IndexPage"/> with pages with ViewModel of type <see cref="SingleGestureViewModel"/>
        /// </summary>
        private void InitializeIndexPage()
        {
            if (_indexPage.Children.Count > 0)
            {
                return;
            }

            NavigationPage.SetHasNavigationBar(_indexPage, false);

            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == ViewModelAssemblyName);

            var viewModelTypes = assembly?.GetTypes()
                .Where(type => type.IsSubclassOf(typeof(SingleGestureViewModel))).ToList();

            foreach (var type in viewModelTypes)
            {
                if (Activator.CreateInstance(type) is SingleGestureViewModel viewModel)
                {
                    var page = new SingleGesturePage { BindingContext = viewModel };
                    _indexPage?.Children.Add(page);
                }
            }
        }

        /// <summary>
        /// Navigates to a page based on ViewModelType.
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel type.</typeparam>
        /// <param name="viewModel">ViewModel object that will be assigned to page's binding context.</param>
        private void NavigateToNavigationStack<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
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
        }

        /// <summary>
        /// Navigates to an index page based on ViewModelType.
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel type.</typeparam>
        /// <param name="viewModel">ViewModel object that will be assigned to page's binding context.</param>
        private async void NavigateToIndexPage<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
        {
            await _navigationPage.PushAsync(_indexPage);
            foreach (var page in _indexPage.Children)
            {
                if (page.BindingContext.GetType() == typeof(TViewModel))
                {
                    if (viewModel != null)
                    {
                        page.BindingContext = viewModel;
                    }

                    _indexPage.CurrentPage = page;
                    break;
                }
            }
        }

        /// <summary>
        /// Closes application.
        /// </summary>
        public void CloseApplication()
        {
            global::Tizen.Applications.Application.Current.Exit();
        }
    }
}
