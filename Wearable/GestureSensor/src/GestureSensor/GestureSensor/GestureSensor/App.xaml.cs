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

﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GestureSensor.Interfaces;

namespace GestureSensor
{
    /// <summary>
    /// Cross-platform application class.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();

            var _navigationService = DependencyService.Get<INavigationService>();
            if (DependencyService.Get<IGestureService>().IsSupported)
            {
                _navigationService.NavigateTo<ViewModels.WelcomeViewModel>();
            }
            else
            {
                _navigationService.NavigateTo(
                    new ViewModels.ExceptionViewModel("Gestures are not supported on this device."));
            }
        }
    }
}