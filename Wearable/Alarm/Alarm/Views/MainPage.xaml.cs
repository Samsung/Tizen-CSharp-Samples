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

using Alarm.Converters;
using Alarm.Implements;
using Alarm.Models;
using Alarm.Resx;
using Alarm.ViewModels;
using System;
using System.Globalization;
using System.Windows.Input;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Alarm.Views
{
    /// <summary>
    /// Main Page class
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : CirclePageEx
    {
        /// <summary>
        /// Constructor of MainPage class
        /// </summary>
        /// <param name="viewModel">MainPageModel</param>
        public MainPage(MainPageModel viewModel)
		{
            BindingContext = viewModel;
            InitializeComponent();

            // Subscribe notification of locale changes to update text based on locale
            MessagingCenter.Subscribe<LocaleHandler, CultureInfo>(this, MessageKeys.LanguageChanged, (obj, culture) =>
            {
                Console.WriteLine($"MainPage received LanguageChanged");
                // Update text that has been translated into the current language.
                HeaderLabel.Text = EmptyHeaderLabel.Text = AppResources.Alarm;
                EmptyBottomLabel.Text = AppResources.AddAlarm;
                AddButton.Text = AppResources.Add;

                //This is workaround solution for refresh Listview following to language change.
                AlarmList.ItemsSource = null;
                AlarmList.ItemsSource = viewModel.RecordList;
            });
        }

        /// <summary>
        /// ListItem tapped event handler
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">SelectedItemChangedEventArgs</param>
        void OnItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            /// skips for no selected item case
            if (e.SelectedItem == null)
            {
                return;
            }

            /// check selected item
            AlarmRecord alarm = e.SelectedItem as AlarmRecord;
            /// de-select first item
            ((ListView)sender).SelectedItem = null; // de-select the row
            EditAlarm(alarm);
        }

        /// <summary>
        /// Image Button event handler
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="args">EventArgs</param>
        void OnButtonReleased(object sender, EventArgs args)
        {
            CreateNewAlarm();
        }

        /// <summary>
        /// Action button event handler
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="args">EventArgs</param>
        void OnAddButtonClicked(object sender, EventArgs args)
        {
            CreateNewAlarm();
        }

        /// <summary>
        /// Launch AlarmEdit page
        /// </summary>
        async void CreateNewAlarm()
        {
            /// Creates default alarm record
            AlarmRecord defaultAlarmRecord = new AlarmRecord();
            defaultAlarmRecord.SetDefault();
            await Navigation.PushAsync(AlarmPageController.GetInstance(AlarmPages.EditPage, defaultAlarmRecord));
        }

        /// <summary>
        /// Launch Edit page
        /// </summary>
        /// <param name="record">AlarmRecord</param>
        async void EditAlarm(AlarmRecord record)
        {
            await Navigation.PushAsync(AlarmPageController.GetInstance(AlarmPages.EditPage, record));
        }
    }
}