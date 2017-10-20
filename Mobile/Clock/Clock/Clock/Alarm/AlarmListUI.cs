/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// Alarm list ui Stack layout to shows alarm list view 
    /// This inherits from content page
    /// </summary>
    public class AlarmListUI : StackLayout
    {
        //public event NotifyCollectionChangedEventHandler CollectionChanged;
        /// <summary>
        /// bindable command property
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(AlarmListUI));

        /// <summary>
        /// Command property to run commmand
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// internal alarm ListView
        /// </summary>
        internal ListView alarmListView;

        /// <summary>
        /// Constructor for Alarm list ui class
        /// </summary>
        public AlarmListUI()
        {
            /// Creates alarm custom cell template based on AlarmListCell
            var customCell = new DataTemplate(typeof(AlarmListCell));

            /// AlarmList view property setting
            alarmListView = new ListView
            {
                /// Sets vertical option to fill
                VerticalOptions = LayoutOptions.FillAndExpand,
                /// All rows are in even size
                HasUnevenRows = false,
                /// Sets item source by Observable alarm list
                ItemsSource = AlarmModel.ObservableAlarmList,
                /// Sets template to customCell object
                ItemTemplate = customCell
            };

            /// Command to trigger page push (AlarmEditPage)
            Command = new Command(async (o) =>
            {
                /// Needs to await for page push
                await Navigation.PushAsync(AlarmPageController.GetInstance(AlarmPages.EditPage, o));
            });

            /// When an item is selected, execute a command if command is set.
            alarmListView.ItemSelected += (s, e) =>
            {
                /// skips for no selected item case
                if (e.SelectedItem == null)
                {
                    // Todo: handle deselect case
                    return; 
                }

                /// checks selected item 
                AlarmRecord alarm = e.SelectedItem as AlarmRecord;
                /// Deselects first
                ((ListView)s).SelectedItem = null; // de-select the row
                /// Executes commands if it has beens set.
                if (Command != null && Command.CanExecute(null))
                {
                    Command.Execute(alarm);
                }
            };

            /// Adds alarmListView to this StackLayout
            Children.Add(alarmListView);
        }
    }
}
