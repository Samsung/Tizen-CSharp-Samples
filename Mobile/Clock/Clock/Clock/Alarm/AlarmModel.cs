/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
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

//#define PRINT_DEBUG
using Clock.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// The enumeration of AlarmStates
    /// </summary>
    public enum AlarmStates
    {
        /// <summary>
        /// Identifier for Snooze
        /// </summary>
        Snooze,
        /// <summary>
        /// Identifier for Active
        /// </summary>
        Active,
        /// <summary>
        /// Identifier for Inactive
        /// </summary>
        Inactive,
    }

    /// <summary>
    /// The enumeration of AlarmTone(media files to ring by playing)
    /// </summary>
    public enum AlarmToneTypes
    {
        /// <summary>
        /// Identifier for default alarm tone type
        /// </summary>
        Default,
        /// <summary>
        /// Identifier for alarm mp3 type
        /// </summary>
        AlarmMp3,
        /// <summary>
        /// Identifier for sdk ringtone type
        /// </summary>
        RingtoneSdk,
    }

    /// <summary>
    /// The enumeration of alarm type
    /// </summary>
    public enum AlarmTypes
    {
        /// <summary>
        /// Identifier for sound
        /// </summary>
        Sound,
        /// <summary>
        /// Identifier for vibration without making a sound
        /// </summary>
        Vibration,
        /// <summary>
        /// Identifier for vibration and sound
        /// </summary>
        SoundVibration
    }

    /// <summary>
    /// The enumeration of alarm editing type
    /// </summary>
    public enum AlarmEditTypes
    {
        /// <summary>
        /// Identifier for alarm edit
        /// </summary>
        Edit,
        /// <summary>
        /// Identifier for editing alarm repeat
        /// </summary>
        Repeat,
        /// <summary>
        /// Identifier for editing alarm type
        /// </summary>
        Type,
        /// <summary>
        /// Identifier for editing alarm sound
        /// </summary>
        Sound,
        /// <summary>
        /// Identifier for editing alarm tone
        /// </summary>
        Tone,
        /// <summary>
        /// Identifier for editing alarm snooze
        /// </summary>
        Snooze,
        /// <summary>
        /// Identifier for editing alarm name
        /// </summary>
        Name
    }


    /// <summary>
    /// Date Formate
    /// - Is it 24-hour format or 12-hour format
    /// - Is it AM or PM in case of 12-hour format
    /// </summary>
    public enum DateFormat
    {
        // 13:00
        Format24Hour,
        // 1:00 AM
        Format12HourAM,
        // 1:00 PM
        Format12HourPM
    }

    /// <summary>
    /// AlarmModel class
    /// </summary>
    public class AlarmModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets/Sets AlarmRecordDictionary
        /// </summary>
        static public IDictionary<string, AlarmRecord> AlarmRecordDictionary { get; set; }

        /// <summary>
        /// ObservableAlarmList property
        /// </summary>
        static public ObservableCollection<AlarmRecord> ObservableAlarmList { get; set; }

        /// <summary>
        /// This static field holds current alarm record which is being displayed for alarm info input.
        /// Through this record, edit view is automatically updated when users select certain option in sub pages.
        /// </summary>
        ///*static internal*/public AlarmRecord BindableAlarmRecord { get; set; }
        public static AlarmRecord BindableAlarmRecord { get; set; }

        public AlarmModel()
        {
            ObservableAlarmList = new ObservableCollection<AlarmRecord>();
            AlarmRecordDictionary = DependencyService.Get<IAlarmPersistentHandler>().DeserializeAlarmRecord();
            BindableAlarmRecord = new AlarmRecord();
            if (AlarmRecordDictionary == null)
            {
                AlarmRecordDictionary = new Dictionary<string, AlarmRecord>();
            }
            else
            {
                foreach (var alarmItem in AlarmRecordDictionary)
                {
                    // Key is DateCreated
                    var keyString = alarmItem.Key;
                    // Retrieve alarm record
                    AlarmRecord retrieved = alarmItem.Value;
                    DependencyService.Get<ILog>().Debug("AlarmModel - Key : " + keyString);
                    DependencyService.Get<ILog>().Debug("AlarmModel - Value : " + retrieved);
                    // Add the retrieved alarm to list for AlarmListUI
                    ObservableAlarmList.Add(retrieved);
                }
            }
        }

        /// <summary>
        /// Cancel native alarm
        /// and then remove AlarmRecord from ObservableAlarmList and AlarmRecordDictionary
        /// </summary>
        /// <param name="alarm">AlarmRecord</param>
        public static void DeleteAlarm(AlarmRecord alarm)
        {
            DependencyService.Get<ILog>().Debug("======[START]  AlarmModel.DeleteAlarm >> " + alarm);
            // Cancel the native alarm
            DependencyService.Get<IAlarm>().DeleteAlarm(alarm);
            string UID = alarm.GetUniqueIdentifier();
            // Delete alarm from dictionary
            AlarmModel.AlarmRecordDictionary.Remove(UID);
            // Delete alarm from List
            for (int i = AlarmModel.ObservableAlarmList.Count - 1; i >= 0; i--)
            {
                if (AlarmModel.ObservableAlarmList[i].GetUniqueIdentifier() == UID)
                {
                    ObservableAlarmList.RemoveAt(i);
                    break;
                }
            }
            // Save
            AlarmModel.SaveDictionary();
            DependencyService.Get<ILog>().Debug("======[END]   AlarmModel.DeleteAlarm >> " + alarm);
        }

        public static void UpdateAlarm(AlarmRecord record)
        {
            var obj = record;
            // Update native alarm
            DependencyService.Get<IAlarm>().UpdateAlarm(ref obj);
            record = obj;
            string alarmUID = record.GetUniqueIdentifier();

            // Update list
            for (int i = 0; i < ObservableAlarmList.Count; i++)
            {
                AlarmRecord item = ObservableAlarmList[i];
                //AlarmModel.Compare(record, item);
                if (item.GetUniqueIdentifier() == alarmUID)
                {
                    DependencyService.Get<ILog>().Debug("  Found AlarmRecord(UID: " + item.GetUniqueIdentifier() + ") in ObservableAlarmList.");
                    AlarmRecordDictionary.Remove(alarmUID);
                    item.DeepCopy(record);
                    AlarmRecordDictionary.Add(alarmUID, item);
                    SaveDictionary();
                    break;
                }
            }
        }

        /// <summary>
        /// Create native alarm
        /// </summary>
        public static void CreateAlarmAndSave()
        {
            // create a native alarm using AlarmModel.BindableAlarmRecord
            // After then, update Native alarm ID.
            BindableAlarmRecord.NativeAlarmID = DependencyService.Get<IAlarm>().CreateAlarm(BindableAlarmRecord);
            DependencyService.Get<ILog>().Debug("====== AlarmModel.CreateAlarm >> BindableAlarmRecord.NativeAlarmID : " + BindableAlarmRecord.NativeAlarmID);
            // ObservableAlarmList is ItemsSource for AlarmList view. This is bindable so this model
            // should be changed to reflect record change (adding a new alarm)

            AlarmRecord record = new AlarmRecord();
            record.DeepCopy(BindableAlarmRecord);
            //AlarmModel.Compare(BindableAlarmRecord, record);

            ObservableAlarmList.Add(record);
            AlarmRecordDictionary.Add(record.GetUniqueIdentifier(), record);
            SaveDictionary();
        }

        public static void DeactivatelAlarm(AlarmRecord record)
        {
            //1. cancel native alarm
            DependencyService.Get<IAlarm>().DeleteAlarm(record);
            //2. Make AlarmRecord's AlarmState
            string UID = record.GetUniqueIdentifier();
            UpdateDictionaryAndList(UID, 0, false);
        }

        public static void ReactivatelAlarm(AlarmRecord record)
        {
            //1. register native alarm
            int NativeAlarmID = DependencyService.Get<IAlarm>().CreateAlarm(record);
            //2. Update NativeAlarmID and AlarmState
            string UID = record.GetUniqueIdentifier();
            UpdateDictionaryAndList(UID, NativeAlarmID, true);
        }

        private static void UpdateDictionaryAndList(string UID, int NativeAlarmID, bool active)
        {
            for (int i = 0; i < AlarmModel.ObservableAlarmList.Count; i++)
            {
                AlarmRecord item = AlarmModel.ObservableAlarmList[i];
                if (item.GetUniqueIdentifier() == UID)
                {
                    AlarmModel.AlarmRecordDictionary.Remove(UID);
                    item.NativeAlarmID = NativeAlarmID;
                    if (active)
                    {
                        item.AlarmState = AlarmStates.Active;
                    }
                    else
                    {
                        item.AlarmState = AlarmStates.Inactive;
                    }

                    AlarmModel.AlarmRecordDictionary.Add(UID, item);
                    AlarmModel.SaveDictionary();
                    break;
                }
            }
        }

        /// <summary>
        /// Serializes alarm record dictionary object and save it to file
        /// </summary>
        public static void SaveDictionary()
        {
            DependencyService.Get<IAlarmPersistentHandler>().SerializeAlarmRecordAsync((IDictionary<string, AlarmRecord>)AlarmModel.AlarmRecordDictionary).Wait();
        }

        ///<summary>
        ///Event that is raised when the properties of AlarmRecord change
        ///</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// When property is changed, registered event callback is invoked
        /// </summary>
        /// <param name="propertyName">Property name of the change event occured</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        public static bool Compare(AlarmRecord firstItem, AlarmRecord secondItem)
        {
            bool same = true;

            if (firstItem.IsSerialized != secondItem.IsSerialized)
            {
                DependencyService.Get<ILog>().Debug("[Compare] IsSerialized " + firstItem.IsSerialized + "  vs " + secondItem.IsSerialized);
                same = false;
            }

            if (firstItem.AlarmDateFormat != secondItem.AlarmDateFormat)
            {
                DependencyService.Get<ILog>().Debug("[Compare] AlarmDateFormat " + firstItem.AlarmDateFormat + "  vs " + secondItem.AlarmDateFormat);
                same = false;
            }

            if (firstItem.ScheduledDateTime != secondItem.ScheduledDateTime)
            {
                DependencyService.Get<ILog>().Debug("[Compare] ScheduledDateTime " + firstItem.ScheduledDateTime + "  vs " + secondItem.ScheduledDateTime);
                same = false;
            }

            if (firstItem.Repeat != secondItem.Repeat)
            {
                DependencyService.Get<ILog>().Debug("[Compare] Repeat " + firstItem.Repeat + "  vs " + secondItem.Repeat);
                same = false;
            }

            if (firstItem.WeekFlag != secondItem.WeekFlag)
            {
                DependencyService.Get<ILog>().Debug("[Compare] WeekFlag " + firstItem.WeekFlag + "  vs " + secondItem.WeekFlag);
                same = false;
            }

            if (firstItem.WeekdayRepeatText != secondItem.WeekdayRepeatText)
            {
                DependencyService.Get<ILog>().Debug("[Compare] WeekdayRepeatText " + firstItem.WeekdayRepeatText + "  vs " + secondItem.WeekdayRepeatText);
                same = false;
            }

            if (firstItem.AlarmType != secondItem.AlarmType)
            {
                DependencyService.Get<ILog>().Debug("[Compare] AlarmType " + firstItem.AlarmType + "  vs " + secondItem.AlarmType);
                same = false;
            }

            if (firstItem.Volume != secondItem.Volume)
            {
                DependencyService.Get<ILog>().Debug("[Compare] Volume " + firstItem.Volume + "  vs " + secondItem.Volume);
                same = false;
            }

            if (firstItem.IsMute != secondItem.IsMute)
            {
                DependencyService.Get<ILog>().Debug("[Compare] IsMute " + firstItem.IsMute + "  vs " + secondItem.IsMute);
                same = false;
            }

            if (firstItem.AlarmToneType != secondItem.AlarmToneType)
            {
                DependencyService.Get<ILog>().Debug("[Compare] AlarmToneType " + firstItem.AlarmToneType + "  vs " + secondItem.AlarmToneType);
                same = false;
            }

            if (firstItem.Snooze != secondItem.Snooze)
            {
                DependencyService.Get<ILog>().Debug("[Compare] Snooze " + firstItem.Snooze + "  vs " + secondItem.Snooze);
                same = false;
            }

            if (firstItem.AlarmState != secondItem.AlarmState)
            {
                DependencyService.Get<ILog>().Debug("[Compare] AlarmState " + firstItem.AlarmState + "  vs " + secondItem.AlarmState);
                same = false;
            }

            if (firstItem.AlarmName != secondItem.AlarmName)
            {
                DependencyService.Get<ILog>().Debug("[Compare] AlarmName " + firstItem.AlarmName + "  vs " + secondItem.AlarmName);
                same = false;
            }

            if (firstItem.DateCreated != secondItem.DateCreated)
            {
                DependencyService.Get<ILog>().Debug("[Compare] DateCreated " + firstItem.DateCreated + "  vs " + secondItem.DateCreated);
                same = false;
            }

            if (firstItem.NativeAlarmID != secondItem.NativeAlarmID)
            {
                DependencyService.Get<ILog>().Debug("[Compare] NativeAlarmID " + firstItem.NativeAlarmID + "  vs " + secondItem.NativeAlarmID);
                same = false;
            }

            if (firstItem.Delete != secondItem.Delete)
            {
                DependencyService.Get<ILog>().Debug("[Compare] Delete " + firstItem.Delete + "  vs " + secondItem.Delete);
                same = false;
            }

            if (firstItem.IsVisibleDateLabel != secondItem.IsVisibleDateLabel)
            {
                DependencyService.Get<ILog>().Debug("[Compare] IsVisibleDateLabel " + firstItem.IsVisibleDateLabel + "  vs " + secondItem.IsVisibleDateLabel);
                same = false;
            }

            return same;
        }

        public static void PrintAll(string methodName)
        {
#if PRINT_DEBUG
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("+++++++++++++++      PrintAll      ++++++++++");
            System.Diagnostics.Debug.WriteLine("  START " + methodName);
            if (AlarmModel.AlarmRecordDictionary != null)
            {
                System.Diagnostics.Debug.WriteLine("   AlarmRecordDictionary");
                foreach (var item in AlarmModel.AlarmRecordDictionary)
                {
                    System.Diagnostics.Debug.WriteLine(" Key: " + item.Key + ", Value:" + item.Value);
                    item.Value.PrintProperty();
                }
            }
            System.Diagnostics.Debug.WriteLine("");
            int count = 1;
            if (AlarmModel.ObservableAlarmList != null)
            {
                System.Diagnostics.Debug.WriteLine("   ObservableAlarmList");
                foreach (AlarmRecord item in AlarmModel.ObservableAlarmList)
                {
                    System.Diagnostics.Debug.WriteLine(count++ + ")" + item);
                    item.PrintProperty();
                }
            }
            System.Diagnostics.Debug.WriteLine("");
            count = 1;
            var allAlarms = Native.AlarmManager.GetAllSceduledAlarms();
            System.Diagnostics.Debug.WriteLine("   Native Scheduled Alarms");
            foreach (Native.Alarm item in allAlarms)
            {
                System.Diagnostics.Debug.WriteLine(count++ + ") Native.Alarm - Id: " + item.AlarmId + ", WeekFlage:" + item.WeekFlag + ", ScheduledDate:" + item.ScheduledDate);
            }
            System.Diagnostics.Debug.WriteLine("  END   " + methodName);
            System.Diagnostics.Debug.WriteLine("");
#endif
        }
    }
}