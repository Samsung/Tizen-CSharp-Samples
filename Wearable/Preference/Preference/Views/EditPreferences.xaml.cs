//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.


using Preference.ViewModels;
using System;
using Tizen.Wearable.CircularUI.Forms;

namespace Preference.Tizen.Mobile.Views
{
    /// <summary>
    /// EditPreferences class.
    /// </summary>
    public partial class EditPreferences 
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
        private void OnSaved(object sender, EventArgs arg)
        {
            Toast.DisplayText("Preference saved.");
        }

        /// <summary>
        /// Displays information about load success operation using Toast Message.
        /// </summary>
        /// <param name="sender">ViewModel instance. Not used.</param>
        /// <param name="args">Event arguments. Not used.</param>
        public void OnLoaded(object sender, EventArgs args)
        {
            Toast.DisplayText("Preference loaded.");
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