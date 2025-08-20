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
using System.Windows.Input;
using Ultraviolet.Enums;
using Ultraviolet.Interfaces;
using Xamarin.Forms;

namespace Ultraviolet.ViewModels
{
    /// <summary>
    /// ViewModel class for MainPage.
    /// </summary>
    public class MainViewModel : BaseViewModel, IDisposable
    {
        /// <summary>
        /// Field backing level property.
        /// </summary>
        private UvLevel _level;

        /// <summary>
        /// Navigation service obtained by dependency service.
        /// </summary>
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Ultraviolet sensor service obtained by dependency service.
        /// </summary>
        private readonly IUltravioletSensorService _ultravioletSensorService;

        /// <summary>
        /// Property indicating uv level.
        /// </summary>
        public UvLevel Level
        {
            get => _level;
            set => SetProperty(ref _level, value);
        }

        /// <summary>
        /// Command for showing details about uv index.
        /// </summary>
        public ICommand ShowDetailsPageCommand { get; private set; }

        /// <summary>
        /// Initializes class.
        /// </summary>
        public MainViewModel()
        {
            _ultravioletSensorService = DependencyService.Get<IUltravioletSensorService>();
            _ultravioletSensorService.UvLevelUpdated += OnUvLevelUpdated;
            _ultravioletSensorService.Start();

            _navigationService = DependencyService.Get<INavigationService>();
            ShowDetailsPageCommand = new Command(ExecuteShowDetailsPage);
        }

        /// <summary>
        /// Handles UvLevelUpdated event.
        /// </summary>
        /// <param name="sender">Object which invoked the event.</param>
        /// <param name="e">Uv level - event argument.</param>
        private void OnUvLevelUpdated(object sender, UvLevel e)
        {
            Level = e;
        }

        /// <summary>
        /// Executed by <see cref="ShowDetailsPageCommand"/>.
        /// </summary>
        private void ExecuteShowDetailsPage()
        {
            _navigationService.NavigateTo(new DetailsViewModel(Level));
        }

        /// <summary>
        /// Disposes main view model.
        /// </summary>
        public void Dispose()
        {
            if (_ultravioletSensorService != null)
                _ultravioletSensorService.UvLevelUpdated -= OnUvLevelUpdated;
            _ultravioletSensorService?.Stop();
        }
    }
}
