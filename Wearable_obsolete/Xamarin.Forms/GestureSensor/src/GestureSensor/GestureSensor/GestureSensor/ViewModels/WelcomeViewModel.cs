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
using System.Windows.Input;
using Xamarin.Forms;

namespace GestureSensor.ViewModels
{
    /// <summary>
    /// ViewModel class for WelcomePage.
    /// </summary>
    public class WelcomeViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Navigates to the next page.
        /// </summary>
        public ICommand StartCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WelcomeViewModel"/> class.
        /// </summary>
        public WelcomeViewModel()
        {
            _navigationService = DependencyService.Get<INavigationService>();

            StartCommand = new Command(ExecuteStart);
        }

        /// <summary>
        /// Executed by <see cref="StartCommand"/>.
        /// </summary>
        private void ExecuteStart()
        {
            _navigationService.NavigateTo<MainViewModel>(clearStack: true);
        }
    }
}
