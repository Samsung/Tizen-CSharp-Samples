/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd
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

using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Calendar.Models;

namespace Calendar.Views
{
    /// <summary>
    /// A custom layout for displaying the date, previous button, next button and event list in the page.
    /// </summary>
    public partial class MonthPage : ContentPage
    {
        /// <summary>
        /// Creating a bindable property named "RecordList" of type List<RecordItem> which is owned by the MonthPage and has default value of null.
        /// </summary>
        public static readonly BindableProperty RecordListProperty = BindableProperty.Create("RecordList", typeof(List<RecordItem>), typeof(MonthPage), null);
        /// <summary>
        /// The RecordList is only updated by the MonthPagetViewModel, so it is defined with the OneWay binding mode.
        /// Therefore, the property does not necessarily implement the setter.
        /// </summary>
        public List<RecordItem> RecordList
        {
            get
            {
                return (List<RecordItem>)GetValue(RecordListProperty);
            }
        }

        /// <summary>
        /// Creating a bindable property named "UpdateListCommandProperty" of type Command which is owned by the MonthPage and has default value of null.
        /// </summary>
        public static readonly BindableProperty UpdateListCommandProperty = BindableProperty.Create("UpdateListCommand", typeof(Command), typeof(MonthPage), null);
        /// <summary>
        /// The UpdateListCommand is only updated by the MonthPageViewModel, so it is defined with the OneWay binding mode.
        /// Therefore, the property does not necessarily implement the setter.
        /// </summary>
        public Command UpdateListCommand
        {
            get
            {
                return (Command)GetValue(UpdateListCommandProperty);
            }
        }

        /// <summary>
        /// A method for updating list in MonthPage.
        /// When RecordItem property is modified, this method will be called.
        /// </summary>
        private void UpdateList()
        {
            MonthPageListView.ItemsSource = null;
            MonthPageListView.ItemsSource = RecordList;
        }

        /// <summary>
        /// This is invoked when the bound properties are updated.
        /// The property is monitored to run the UpdateList method.
        /// <param name="sender">The object what got the event</param>
        /// <param name="e">Data of the event</param>
        /// </summary>
        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateList();
        }

        /// <summary>
        /// The event handler of the click event.
        /// When a user clicks a create button in order to create event, this event handler would be invoked.
        /// This handler would create an instance of the InsertPaget.xaml
        /// and push the instance to the navigation stack in an asynchronous manner.
        /// <param name="sender">The object what got the event</param>
        /// <param name="e">Data of the event</param>
        /// </summary>
        async void OnButtonClicked(object sender, EventArgs e)
        {
            RecordItem item = new RecordItem();
            InsertPage insertPage = new InsertPage(item, "Save", 0);
            await Navigation.PushAsync(insertPage);
        }

        /// <summary>
        /// This is invoked after the view appears on the screen.
        /// This handler executes the update command of the MonthPageViewModel.
        /// The command is going to change the RecordList of the MonthPageViewModel.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            bool isAccepted = SecurityProvider.Instance.CheckPrivilege();
            if (isAccepted)
                UpdateListCommand.Execute(null);
        }

        /// <summary>
        /// A Constructor.
        /// Adds PropertyChanged event handler.
        /// Adds event handler for MonthPageListView.ItemSelected to move InsertPage with selected item.
        /// </summary>
        public MonthPage()
        {
            DependencyService.Get<ISecurityAPIs>().PrivilageAccepted += MonthPage_PrivilageAccepted;
            InitializeComponent();
            PropertyChanged += OnPropertyChanged;
            MonthPageListView.ItemSelected += async (o, e) =>
            {
                RecordItem item = e.SelectedItem as RecordItem;
                InsertPage insertPage = new InsertPage(item, "Update", item.Index);
                await Navigation.PushAsync(insertPage);
            };
        }

        /// <summary>
        /// This is invoked after permission to update the events created.
        /// </summary>
        private void MonthPage_PrivilageAccepted(object sender, EventArgs e)
        {
            UpdateListCommand.Execute(null);
        }
    }
}
