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

using NotificationManager.ViewModels;
using System;

namespace NotificationManager.Views
{
    /// <summary>
    /// MainPage Xaml C# partial class code.
    /// </summary>
    public partial class MainPage
    {
        #region methods

        /// <summary>
        /// Class constructor. Initializes component.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Displays alert to ensure if all notifications are to be removed.
        /// </summary>
        /// <param name="sender">An object that raised the event.</param>
        /// <param name="args">An event data.</param>
        public async void ClearNotificationsAlertAsync(object sender, EventArgs args)
        {
            var result = await DisplayAlert(
                "Clear",
                "Are you sure you want to clear all notifications?",
                "Done",
                "Cancel");

            if (result)
            {
                if (BindingContext is NotificationManagerViewModel viewModel)
                {
                    viewModel.DeleteNotificationsCommand.Execute(null);
                }
            }
        }

        #endregion
    }
}