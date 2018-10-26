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

using Alarm.Models;
using System;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Alarm.Views
{
    /// <summary>
    /// AlarmEditPage class
    /// It shows the Edit View of alarm
    /// User can set alarm time using CircleDateTimeSelector. and then save alarm record.
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AlarmEditPage : CirclePageEx
    {
        public static readonly BindableProperty SelectedDateTimeProperty = BindableProperty.Create("SelectedDateTime", typeof(DateTime), typeof(AlarmEditPage), DateTime.Now);

        private bool _alarmSaving;

        /// <summary>
        /// Selected date time for saved record or current time.
        /// </summary>
        public DateTime SelectedDateTime
        {
            get
            {
                return (DateTime)GetValue(SelectedDateTimeProperty);
            }

            set
            {
                SetValue(SelectedDateTimeProperty, value);
            }
        }

        /// <summary>
        /// AlarmEditPage constructor.
        /// </summary>
        /// <param name="alarmRecordData">AlarmRecord</param>
        public AlarmEditPage(AlarmRecord alarmRecordData)
		{
            InitializeComponent();
            TimeSelector.BindingContext = this;
            _alarmSaving = false;
            SelectedDateTime = alarmRecordData.ScheduledDateTime;
        }

        /// <summary>
        /// Update SelectedDateTime with alarmRecordData time.
        /// </summary>
        /// <param name="alarmRecordData">Alarm record data to set SelectedDateTime</param>
        internal void Update(AlarmRecord alarmRecordData)
        {
            Console.WriteLine("Update" + alarmRecordData.ToString());
            SelectedDateTime = alarmRecordData.ScheduledDateTime;
            _alarmSaving = false;
        }

        /// <summary>
        /// Request to save alarm event.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="args">EventArgs</param>
        async void OnSaveButtonClicked(object sender, EventArgs args)
        {
            Console.WriteLine("OnSaveButtonClicked");
            if (_alarmSaving)
            {
                return;
            }

            _alarmSaving = true;
            //subtract second from selectedTime.
            SelectedDateTime = SelectedDateTime.AddSeconds(-SelectedDateTime.Second);
            Console.WriteLine($"SelectedDateTime :{SelectedDateTime}");
            AlarmModel.BindableAlarmRecord.ScheduledDateTime = SelectedDateTime;

            AlarmRecord duplicate = new AlarmRecord();
            bool existSameAlarm = AlarmModel.CheckAlarmExist(ref duplicate);
            if (existSameAlarm)
            {
                // Use alarm created date for unique identifier for an alarm record
                string alarmUID = AlarmModel.BindableAlarmRecord.GetUniqueIdentifier();
                if (!AlarmModel.BindableAlarmRecord.IsSerialized)
                {
                    // in case that AlarmEditPage is shown by clicking at add alarm
                    // when trying to create a system alarm and save it, find the same alarm
                    // expected behavior : update the previous one and do not create a new alarm
                    AlarmModel.BindableAlarmRecord.IsSerialized = true;
                    duplicate.DeepCopy(AlarmModel.BindableAlarmRecord, false);
                    // Update the existing alarm(duplicate)
                    Console.WriteLine("exist same alarm! update previous alarm, not create new alarm:" + AlarmModel.BindableAlarmRecord.ToString());
                    AlarmModel.UpdateAlarm(duplicate);
                }
                else if (alarmUID.Equals(duplicate.GetUniqueIdentifier()))
                {
                    // in case that AlarmEditPage is shown by selecting an item of ListView in AlarmListPage
                    // At saving time, just update itself. (It doesn't affect other alarms)
                    Console.WriteLine("exist same alarm! update current alarm:" + AlarmModel.BindableAlarmRecord.ToString());
                    AlarmModel.UpdateAlarm(AlarmModel.BindableAlarmRecord);
                }
                else
                {
                    // in case that AlarmEditPage is shown by selecting an item of ListView in AlarmListPage
                    // At saving time, the same alarm is found.
                    // In case that this alarm is not new, the existing alarm(duplicate) will be deleted and it will be updated.
                    // 1. delete duplicate alarm
                    Console.WriteLine("exist same alarm! delete duplicate alarm and update current alarm:" + AlarmModel.BindableAlarmRecord.ToString());
                    AlarmModel.DeleteAlarm(duplicate);
                    // 2. update bindableAlarmRecord
                    AlarmModel.UpdateAlarm(AlarmModel.BindableAlarmRecord);
                }
            }
            else
            {
                if (!AlarmModel.BindableAlarmRecord.IsSerialized)
                {
                    // In case that AlarmEditPage is shown by clicking FloatingButton
                    // There's no same alarm. So, just create a new alarm and add to list and dictionary.
                    AlarmModel.BindableAlarmRecord.IsSerialized = true;
                    Console.WriteLine("new Alarm create:" + AlarmModel.BindableAlarmRecord.ToString());
                    AlarmModel.CreateAlarm();
                }
                else
                {
                    // in case that AlarmEditPage is shown by selecting an item of ListView in AlarmListPage
                    // There's no same alarm. So, just update itself.
                    Console.WriteLine("update current alarm:" + AlarmModel.BindableAlarmRecord.ToString());
                    AlarmModel.UpdateAlarm(AlarmModel.BindableAlarmRecord);
                }
            }

            //Create SavePopupPage, and then close current EditPage.
            Navigation.InsertPageBefore(new SavePopupPage(AlarmModel.BindableAlarmRecord), this);
            await Navigation.PopAsync();
            _alarmSaving = false;
        }
    }
}