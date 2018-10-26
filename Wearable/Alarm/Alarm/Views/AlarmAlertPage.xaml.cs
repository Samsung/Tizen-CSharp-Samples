/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Alarm.Implements;
using Alarm.Models;
using Alarm.Resx;
using Alarm.ViewModels;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Alarm.Views
{
    /// <summary>
    /// AlarmAlertPage class
    /// It shows the Alert View of alarm
    /// User can dismiss alarm alert and remove record using dismiss button in the bottom of view.
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlarmAlertPage : CirclePageEx
    {
        AlertPageModel _viewModel;
        /// <summary>
        /// AlarmAlertPage constructor.
        /// </summary>
        /// <param name="viewModel">AlertPageModel</param>
        public AlarmAlertPage(AlertPageModel viewModel)
        {
            Console.WriteLine("AlarmAlertPage");

            InitializeComponent();
            BindingContext = _viewModel = viewModel;

            _viewModel.StartAlert();

            // Subscribe notification of locale changes to update text based on locale
            MessagingCenter.Subscribe<LocaleHandler, CultureInfo>(this, MessageKeys.LanguageChanged, (obj, culture) =>
            {
                Console.WriteLine($"AlarmAlertPage received LanguageChanged");
                // Update text that has been translated into the current language.
                TimeLabel.Text = _viewModel.Record.ScheduledDateTime.ToString("hh:mm tt");
                DismissButton.Text = AppResources.Dismiss;
            });
        }

        /// <summary>
        /// Request to dissmiss alarm alert and remove alarm record.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="args">EventArgs</param>
        void OnDismissButtonClicked(object sender, EventArgs args)
        {
            Console.WriteLine("OnDismissButtonClicked()");

            _viewModel.Dismiss();
            PopAsyncAlertPage();
        }

        /// <summary>
        /// Invoked when backbutton is pressed
        /// Alarm record is dismissed.
        /// </summary>
        /// <returns>bool value</returns>
        protected override bool OnBackButtonPressed()
        {
            Console.WriteLine("[AlarmAlertPage]  OnBackButtonPressed PopAsync");
            _viewModel.Dismiss();
            PopAsyncAlertPage();

            return true;
        }

        async void PopAsyncAlertPage()
        {
            await Navigation.PopAsync();
        }

    }
}