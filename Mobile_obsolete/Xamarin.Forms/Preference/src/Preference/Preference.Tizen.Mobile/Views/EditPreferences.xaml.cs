﻿/*
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

using Preference.ViewModels;
using Preference.Interfaces;
using Preference.Tizen.Mobile.Views;
using System;
using Preference.Tizen.Mobile.Controls;
using Xamarin.Forms;

[assembly: Dependency(typeof(EditPreferences))]
namespace Preference.Tizen.Mobile.Views
{
    /// <summary>
    /// EditPreferences class.
    /// </summary>
    public partial class EditPreferences : IPage
    {
        #region properties

        /// <summary>
        /// EditPreferencesViewModel instance.
        /// </summary>
        public EditPreferencesViewModel ViewModel = new EditPreferencesViewModel();

        #endregion

        #region methods

        /// <summary>
        /// Class constructor.
        /// </summary>
        public EditPreferences()
        {
            BindingContext = ViewModel;
            InitializeComponent();
            BindEvents();
        }

        /// <summary>
        /// Binds events.
        /// </summary>
        private void BindEvents()
        {
            ViewModel.OnSaveCompleteEvent += OnSaved;
            ViewModel.OnLoadCompleteEvent += OnLoaded;
            ViewModel.OnInvalidDataEvent += OnDataError;
        }

        /// <summary>
        /// Displays information about save success operation using Toast Message.
        /// </summary>
        /// <param name="sender">ViewModel instance. Not used.</param>
        /// <param name="arg">Event arguments. Not used.</param>
        public void OnSaved(object sender, EventArgs arg)
        {
            Toast.DisplayText("Preferences saved.");
        }

        /// <summary>
        /// Displays information about load success operation using Toast Message.
        /// </summary>
        /// <param name="sender">ViewModel instance. Not used.</param>
        /// <param name="args">Event arguments. Not used.</param>
        public void OnLoaded(object sender, EventArgs args)
        {
            Toast.DisplayText("Preferences loaded.");
        }

        /// <summary>
        /// Displays information about invalid data provided by user using Toast Message.
        /// </summary>
        /// <param name="sender">ViewModel instance. Not used.</param>
        /// <param name="args">Event arguments. Not used.</param>
        public void OnDataError(object sender, EventArgs args)
        {
            Toast.DisplayText("Invalid data.");
        }

        #endregion
    }
}