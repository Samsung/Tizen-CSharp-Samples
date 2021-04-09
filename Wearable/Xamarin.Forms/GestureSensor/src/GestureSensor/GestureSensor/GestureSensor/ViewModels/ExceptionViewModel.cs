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

﻿using System.Windows.Input;

namespace GestureSensor.ViewModels
{
    /// <summary>
    /// View Model for exception page.
    /// </summary>
    public class ExceptionViewModel : BaseViewModel
    {
        private string _message;
        private ICommand _exitCommand;

        /// <summary>
        /// Gets or sets exception message.
        /// </summary>
        public string Message
        {
            get => _message;
            private set => SetProperty(ref _message, value);
        }

        /// <summary>
        /// Executed on app button.
        /// </summary>
        public ICommand ExitCommand
        {
            get => _exitCommand;
            private set => SetProperty(ref _exitCommand, value);
        }


        /// <summary>
        /// Creates new instance of <see cref="ExceptionViewModel"/>
        /// </summary>
        /// <param name="message">Exception message.</param>
        public ExceptionViewModel(string message)
        {
            Message = message;

            ExitCommand = new Xamarin.Forms.Command(() =>
            {
                Xamarin.Forms.DependencyService.Get<Interfaces.INavigationService>().CloseApplication();
            });
        }
    }
}
