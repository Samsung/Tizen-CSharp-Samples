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
        /// Set visiblity of range properties.
        /// When recurrence is set, one of the range type should be shown.
        /// One is count and the other is until.
        /// <param name="countVisible">Visible of count</param>
        /// <param name="untilVisible">Visible of until</param>
        /// </summary>
        private void RepeatVisible(bool countVisible, bool untilVisible)
        {
            if (countVisible == false && untilVisible == false)
            {
                xRange.IsVisible = false;
            }
            else
            {
                xRange.IsVisible = countVisible;
                xRange.IsVisible = untilVisible;
                xRange.Text = countVisible == true ? "  Count" : "  Until";
            }

            xCount.IsVisible = countVisible;
            UntilDate.IsVisible = untilVisible;
            UntilTime.IsVisible = untilVisible;
        }

        /// <summary>
        /// A event handler for the Allday toggle.
        /// When allday is set, time field should be dispeared not to be selected.
        /// <param name="sender">The object what got the event</param>
        /// <param name="e">Data of the event</param>
        /// </summary>
        public void OnAlldayToggled(object sender, EventArgs e)
        {
            Switch s = (Switch)sender;
            int columnSpan = (s.IsToggled == true) ? 4 : 2;
            bool isVisible = (s.IsToggled == true) ? false : true;

            StartDate.SetValue(Grid.ColumnSpanProperty, columnSpan);
            StartTime.IsVisible = isVisible;

            EndDate.SetValue(Grid.ColumnSpanProperty, columnSpan);
            EndTime.IsVisible = isVisible;
        }


        /// <summary>
        /// A event handler for the Repeat picker.
        /// When recurrece is set, range tye should be shown.
        /// Range type has two type : count and until.
        /// Count should be inserted with count integer.
        /// Until should be inserted with date and time.
        /// index 0 is none recurrence,
        /// 1 is daily, 2 is weekly, 3 is monthly, 4 is yearly.
        /// <param name="sender">The object what got the event</param>
        /// <param name="e">Data of the event</param>
        /// </summary>
        public void OnRepeatPickerChanged(object sender, EventArgs e)
        {
            bool showUntil = RepeatPicker.SelectedIndex == 0 ? false : true;
            RepeatUntilLabel.IsVisible = showUntil;
            RepeatUntilPicker.IsVisible = showUntil;

            if (showUntil == true)
            {
                bool visible = RepeatUntilPicker.SelectedIndex == 0 ? true : false;
                RepeatVisible(visible, !visible);
            }
            else
            {
                RepeatUntilPicker.SelectedIndex = 0;
                RepeatVisible(false, false);
            }
        }

        /// <summary>
        /// A event handler for the RepeatUntil picker.
        /// <param name="sender">The object what got the event</param>
        /// <param name="e">Data of the event</param>
        /// </summary>
        public void OnRepeatUntilPickerChanged(object sender, EventArgs e)
        {
            bool visible = RepeatUntilPicker.SelectedIndex == 0 ? true : false;
            RepeatVisible(visible, !visible);
        }

        /// <summary>
        /// A event handler for the Save button.
        /// <param name="sender">The object what got the event</param>
        /// <param name="e">Data of the event</param>
        /// </summary>
        public void OnLeftClicked(object sender, EventArgs e)
        {
            item.Summary = SummaryEntry.Text;
            item.Location = LocationEntry.Text;
            item.Description = DescriptionEntry.Text;
            item.IsAllday = AlldaySwitch.IsToggled;
            item.Reminder = ReminderPicker.SelectedIndex;
            item.Recurrence = RepeatPicker.SelectedIndex;
            item.Priority = PriorityPicker.SelectedIndex;
            item.Sensitivity = SensitivityPicker.SelectedIndex;
            item.Status = StatusPicker.SelectedIndex;

            item.StartTime = new DateTime(StartDate.Date.Year, StartDate.Date.Month, StartDate.Date.Day,
                    StartTime.Time.Hours, StartTime.Time.Minutes, 0, DateTimeKind.Local);
            item.EndTime = new DateTime(EndDate.Date.Year, EndDate.Date.Month, EndDate.Date.Day,
                    EndTime.Time.Hours, EndTime.Time.Minutes, 0, DateTimeKind.Local);

            if (item.Recurrence > 0)
            {
                item.UntilType = RepeatUntilPicker.SelectedIndex;
                switch (RepeatUntilPicker.SelectedIndex)
                {
                /// Count
                default:
                case 0:
                    item.UntilCount = Int32.Parse(xCount.Text);
                    break;
                /// Until
                case 1:
                    item.UntilTime = new DateTime(UntilDate.Date.Year, UntilDate.Date.Month, UntilDate.Date.Day,
                            UntilTime.Time.Hours, UntilTime.Time.Minutes, 0, DateTimeKind.Local);
                    break;
                }
            }

            /// 0 index indicates that inItem is created.
            if (item.Index == 0)
            {
                item.Index = RecordItemProvider.Instance.Insert(item);
            }
            else
            {
                RecordItemProvider.Instance.Update(item);
            }

            Navigation.PopAsync();
        }

        /// <summary>
        /// A event handler for the Delete button.
        /// <param name="sender">The object what got the event</param>
        /// <param name="e">Data of the event</param>
        /// </summary>
        public void OnRightClicked(object sender, EventArgs e)
        {
            RecordItemProvider.Instance.Delete(item);
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

            /// 0 index indicates that inItem is created.
            /// Minute and second is set as 0.
            /// Start time is set as the next hour.
            /// End time is set as 1 hour later than start time.
            if (index == 0)
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

            /// Depending on the recurrence index, range visibility could be set.
            /// When recurrence is set, range type should be visible.
            RepeatPicker.SelectedIndex = inItem.Recurrence;
            if (RepeatPicker.SelectedIndex > 0)
            {
                RepeatUntilLabel.IsVisible = true;
                RepeatUntilPicker.IsVisible = true;
                RepeatUntilPicker.SelectedIndex = inItem.UntilType;
                switch (RepeatUntilPicker.SelectedIndex)
                {
                default:
                /// count
                case 0:
                    RepeatVisible(true, false);
                    xCount.Text = item.UntilCount.ToString();
                    break;
                /// until
                case 1:
                    RepeatVisible(false, true);
                    UntilDate.Date = inItem.UntilTime.ToLocalTime();
                    UntilTime.Time = new TimeSpan(inItem.UntilTime.ToLocalTime().Hour,
                            inItem.UntilTime.ToLocalTime().Minute, 0);
                    break;
                }
            }
            else
            {
                RepeatUntilLabel.IsVisible = false;
                RepeatUntilPicker.IsVisible = false;
                RepeatVisible(false, false);
            }

            TimeZoneText.Text = TimeZoneInfo.Local.DisplayName;
            AlldaySwitch.IsToggled = inItem.IsAllday;
            ReminderPicker.SelectedIndex = inItem.Reminder;
            PriorityPicker.SelectedIndex = inItem.Priority;
            SensitivityPicker.SelectedIndex = inItem.Sensitivity;
            StatusPicker.SelectedIndex = inItem.Status;

            /// Event handle is set when allday toggle is changed.
            AlldaySwitch.Toggled += OnAlldayToggled;
            /// Event handle is set when repeat picker is changed.
            RepeatPicker.SelectedIndexChanged += OnRepeatPickerChanged;
            /// Event handle is set when repeat until picker is changed.
            RepeatUntilPicker.SelectedIndexChanged += OnRepeatUntilPickerChanged;

            Title = (index == 0) ? "Create Event" : inItem.Summary;
            /// In creating event, delete button is not needed, so set visible false.
            xRight.IsVisible = (index == 0) ? false : true;
            ButtonName.Text = ButtonText;
        }
    }
}
