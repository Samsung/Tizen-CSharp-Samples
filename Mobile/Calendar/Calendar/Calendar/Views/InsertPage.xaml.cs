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
using Xamarin.Forms;
using Calendar.Models;

namespace Calendar.Views
{
    /// <summary>
    /// A custom layout for displaying the text and buttons to input event properties.
    /// </summary>
    public partial class InsertPage : ContentPage
    {
        /// <summary>
        /// A RecordItem property sets to or gets form the source.
        /// </summary>
        public RecordItem item;

        /// <summary>
        /// A event handler for the Allday toggle.
        /// <param name="sender">The object what got the event</param>
        /// <param name="e">Data of the event</param>
        /// </summary>
        void OnAlldayToggled(object sender, EventArgs e)
        {
            Switch s = (Switch)sender;
            int columnSpan = (s.IsToggled == true) ? 2 : 1;
            bool isVisible = (s.IsToggled == true) ? false : true;

            StartDate.SetValue(Grid.ColumnSpanProperty, columnSpan);
            StartTime.IsVisible = isVisible;

            EndDate.SetValue(Grid.ColumnSpanProperty, columnSpan);
            EndTime.IsVisible = isVisible;
        }

        /// <summary>
        /// A event handler for the Repeat picker.
        /// <param name="sender">The object what got the event</param>
        /// <param name="e">Data of the event</param>
        /// </summary>
        void OnRepeatPickerChanged(object sender, EventArgs e)
        {
            bool showUntil = false;
            switch (RepeatPicker.SelectedIndex)
            {
            default:
            case 0:
                showUntil = false;
                break;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                showUntil = true;
                break;
            }

            RepeatUntilLabel.IsVisible = showUntil;
            RepeatUntilPicker.IsVisible = showUntil;

            if (showUntil == true)
            {
                if (RepeatUntilPicker.SelectedIndex == 0)
                {
                    CountLabel.IsVisible = true;
                    CountEntry.IsVisible = true;
                    UntilLabel.IsVisible = false;
                    UntilDate.IsVisible = false;
                    UntilTime.IsVisible = false;
                }
                else
                {
                    CountLabel.IsVisible = false;
                    CountEntry.IsVisible = false;
                    UntilLabel.IsVisible = true;
                    UntilDate.IsVisible = true;
                    UntilTime.IsVisible = true;
                }
            }
            else
            {
                RepeatUntilPicker.SelectedIndex = 0;
                CountLabel.IsVisible = false;
                CountEntry.IsVisible = false;
                UntilLabel.IsVisible = false;
                UntilDate.IsVisible = false;
                UntilTime.IsVisible = false;
            }
        }

        /// <summary>
        /// A event handler for the RepeatUntil picker.
        /// <param name="sender">The object what got the event</param>
        /// <param name="e">Data of the event</param>
        /// </summary>
        void OnRepeatUntilPickerChanged(object sender, EventArgs e)
        {
            if (RepeatUntilPicker.SelectedIndex == 0)
            {
                CountLabel.IsVisible = true;
                CountEntry.IsVisible = true;
                UntilLabel.IsVisible = false;
                UntilDate.IsVisible = false;
                UntilTime.IsVisible = false;
            }
            else
            {
                CountLabel.IsVisible = false;
                CountEntry.IsVisible = false;
                UntilLabel.IsVisible = true;
                UntilDate.IsVisible = true;
                UntilTime.IsVisible = true;
            }
        }

        /// <summary>
        /// A event handler for the Save button.
        /// <param name="sender">The object what got the event</param>
        /// <param name="e">Data of the event</param>
        /// </summary>
        void OnSaveClicked(object sender, EventArgs e)
        {
            item.Summary = SummaryEntry.Text;
            item.Location = LocationEntry.Text;
            item.Description = DescriptionEntry.Text;
            item.StartTime = new DateTime(StartDate.Date.Year,
                    StartDate.Date.Month,
                    StartDate.Date.Day,
                    StartTime.Time.Hours,
                    StartTime.Time.Minutes,
                    0,
                    DateTimeKind.Local);
            item.EndTime = new DateTime(EndDate.Date.Year,
                    EndDate.Date.Month,
                    EndDate.Date.Day,
                    EndTime.Time.Hours,
                    EndTime.Time.Minutes,
                    0,
                    DateTimeKind.Local);
            item.IsAllday = AlldaySwitch.IsToggled;
            item.Reminder = ReminderPicker.SelectedIndex;
//            item.Recurrence = RecurrencePicker.SelectedIndex;
            item.Priority = PriorityPicker.SelectedIndex;
            item.Sensitivity = SensitivityPicker.SelectedIndex;
            item.Status = StatusPicker.SelectedIndex;

            if (ButtonName.Text.CompareTo("Save") == 0)
            {
                RecordItemProvider.Instance.Insert(item);
            }
            else
            {
                RecordItemProvider.Instance.Update(item);
            }

            Navigation.PopAsync();
        }

        /// <summary>
        /// A Constructor.
        /// Shows event properties to modify data.
        /// <param name="inItem">The item to be shown.</param>
        /// <param name="ButtonText">The name of button.</param>
        /// <param name="index">The selected event id if it exists.</param>
        /// </summary>
        public InsertPage(RecordItem inItem, string ButtonText, int index)
        {
            InitializeComponent();
            // Setting the BindContext is going to update the RecordItem.
            item = inItem;

            SummaryEntry.Text = inItem.Summary;
            LocationEntry.Text = inItem.Location;
            DescriptionEntry.Text = inItem.Description;

            if (ButtonText.CompareTo("Save") == 0)
            {
                inItem.Index = index;
                inItem.StartTime = CacheData.CurrentDateTime;
                inItem.StartTime = inItem.StartTime.AddHours(1);
                inItem.StartTime = inItem.StartTime.AddTicks(-(inItem.StartTime.Ticks % TimeSpan.TicksPerHour));
                inItem.EndTime = inItem.StartTime.AddHours(1);
            }

            if (inItem.IsAllday == true)
            {
                StartDate.Date = inItem.StartTime;
                EndDate.Date = inItem.EndTime;
            }
            else
            {
                StartDate.Date = inItem.StartTime.ToLocalTime();
                EndDate.Date = inItem.EndTime.ToLocalTime();
                StartTime.Time = new TimeSpan(inItem.StartTime.ToLocalTime().Hour,
                        inItem.StartTime.ToLocalTime().Minute, 0);
                EndTime.Time = new TimeSpan(inItem.EndTime.ToLocalTime().Hour,
                        inItem.EndTime.ToLocalTime().Minute, 0);
            }

            TimeZoneText.Text = TimeZoneInfo.Local.DisplayName;
            AlldaySwitch.Toggled += OnAlldayToggled;
            AlldaySwitch.IsToggled = inItem.IsAllday;
            RepeatPicker.SelectedIndexChanged += OnRepeatPickerChanged;
            RepeatUntilPicker.SelectedIndexChanged += OnRepeatUntilPickerChanged;
            ReminderPicker.SelectedIndex = inItem.Reminder;
            PriorityPicker.SelectedIndex = inItem.Priority;
            SensitivityPicker.SelectedIndex = inItem.Sensitivity;
            StatusPicker.SelectedIndex = inItem.Status;
            ButtonName.Text = ButtonText;
        }
    }
}
