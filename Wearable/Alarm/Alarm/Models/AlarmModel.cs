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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Alarm.Implements;

namespace Alarm.Models
{
    /// <summary>
    /// AlarmModel class
    /// </summary>
    public class AlarmModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets/Sets AlarmRecordDictionary
        /// </summary>
        public static IDictionary<string, AlarmRecord> AlarmRecordDictionary { get; set; }

        /// <summary>
        /// ObservableAlarmList property
        /// </summary>
        public static ObservableCollection<AlarmRecord> ObservableAlarmList { get; set; }

        /// <summary>
        /// This static field holds current alarm record which is being displayed for alarm info input.
        /// Through this record, edit view is automatically updated when users select certain option in sub pages.
        /// </summary>
        public static AlarmRecord BindableAlarmRecord { get; set; }

        /// <summary>
        /// AlarmModel constructor
        /// </summary>
        public AlarmModel()
        {
            ObservableAlarmList = new ObservableCollection<AlarmRecord>();
            AlarmRecordDictionary = AlarmPersistentHandler.DeserializeAlarmRecord();
            BindableAlarmRecord = new AlarmRecord();
            if (AlarmRecordDictionary == null)
            {
                AlarmRecordDictionary = new Dictionary<string, AlarmRecord>();
            }
            else
            {
                foreach (var alarmItem in AlarmRecordDictionary)
                {
                    Console.WriteLine("alarmItem key:" + alarmItem.Key);
                    // Key is DateCreated
                    var keyString = alarmItem.Key;
                    // Retrieve alarm record
                    AlarmRecord retrieved = alarmItem.Value;
                    Console.WriteLine("retrieved:" + retrieved.ToString());
                    // Add the retrieved alarm to list for AlarmListUI
                    ObservableAlarmList.Add(retrieved);
                }
            }
        }

        /// <summary>
        /// Update alarm record
        /// </summary>
        /// <param name="record">AlarmRecord</param>
        public static void UpdateAlarm(AlarmRecord record)
        {
            var obj = record;
            // Update native alarm
            AlarmNativeHandler.UpdateAlarm(ref obj);
            record = obj;
            string alarmUID = record.GetUniqueIdentifier();

            // Update list
            for (int i = 0; i < ObservableAlarmList.Count; i++)
            {
                AlarmRecord item = ObservableAlarmList[i];
                if (item.GetUniqueIdentifier() == alarmUID)
                {
                    Console.WriteLine("Found AlarmRecord(UID: " + item.GetUniqueIdentifier() + ") in ObservableAlarmList.");
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
        public static void CreateAlarm()
        {
            // create a native alarm using AlarmModel.BindableAlarmRecord
            // After then, update Native alarm ID.
            BindableAlarmRecord.NativeAlarmID = AlarmNativeHandler.CreateAlarm(BindableAlarmRecord);

            AlarmRecord record = new AlarmRecord();
            record.DeepCopy(BindableAlarmRecord);
            ObservableAlarmList.Add(record);
            AlarmRecordDictionary.Add(record.GetUniqueIdentifier(), record);
            SaveDictionary();
        }

        /// <summary>
        /// Cancel native alarm
        /// and then remove AlarmRecord from ObservableAlarmList and AlarmRecordDictionary
        /// </summary>
        /// <param name="alarm">AlarmRecord</param>
        public static void DeleteAlarm(AlarmRecord alarm)
        {
            Console.WriteLine("DeleteAlarm:" + alarm.ToString());
            // Cancel the native alarm
            AlarmNativeHandler.DeleteAlarm(alarm);
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

            AlarmModel.SaveDictionary();
        }

        /// <summary>
        /// Cancel native alarm
        /// and then deactivate AlarmRecord from AlarmRecordDictionary
        /// </summary>
        /// <param name="record">AlarmRecord</param>
        public static void DeactivatelAlarm(AlarmRecord record)
        {
            //1. cancel native alarm
            AlarmNativeHandler.DeleteAlarm(record);
            //2. Make AlarmRecord's AlarmState
            string UID = record.GetUniqueIdentifier();
            UpdateDictionaryAndList(UID, 0, false);
        }

        /// <summary>
        /// Reactivate AlarmRecord
        /// </summary>
        /// <param name="record">AlarmRecord for reactivating</param>
        public static void ReactivatelAlarm(AlarmRecord record)
        {
            //1. register native alarm
            int NativeAlarmID = AlarmNativeHandler.CreateAlarm(record);
            //2. Update NativeAlarmID and AlarmState
            string UID = record.GetUniqueIdentifier();
        }

        /// <summary>
        /// Update AlarmRecord Dictionary and ObservableAlarmList
        /// </summary>
        /// <param name="UID">AlarmRecord unique ID</param>
        /// <param name="NativeAlarmID">Native AlarmID</param>
        /// <param name="active">active state of AlarmRecord</param>
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
           AlarmPersistentHandler.SerializeAlarmRecordAsync(AlarmModel.AlarmRecordDictionary).Wait();
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

        /// <summary>
        /// PrintAll method for debugging.
        /// </summary>
        /// <param name="methodName">method name for printing</param>
        public static void PrintAll(string methodName)
        {
            Console.WriteLine("");
            Console.WriteLine("+++++++++++++++      PrintAll      ++++++++++");
            Console.WriteLine("  START " + methodName);
            if (AlarmModel.AlarmRecordDictionary != null)
            {
                Console.WriteLine("   AlarmRecordDictionary");
                foreach (var item in AlarmModel.AlarmRecordDictionary)
                {
                    Console.WriteLine(" Key: " + item.Key + ", Value:" + item.Value.ToString());
                }
            }

            Console.WriteLine("");
            int count = 1;
            if (AlarmModel.ObservableAlarmList != null)
            {
                Console.WriteLine("   ObservableAlarmList");
                foreach (AlarmRecord item in AlarmModel.ObservableAlarmList)
                {
                    Console.WriteLine(count++ + ")" + item.ToString());
                }
            }

            Console.WriteLine("");
            count = 1;
            Console.WriteLine("  END   " + methodName);
        }

        /// <summary>
        /// Determines whether ObservableAlarmList contains an alarm which is scheduled at the same time.
        /// </summary>
        /// <param name="duplicate">AlarmRecord</param>
        /// <returns> true if a same alarm is found in the list; otherwise, false.</returns>
        public static bool CheckAlarmExist(ref AlarmRecord duplicate)
        {
            foreach (AlarmRecord item in AlarmModel.ObservableAlarmList)
            {
                // Check scheduled time and alarm name
                if (item.ScheduledDateTime.Equals(AlarmModel.BindableAlarmRecord.ScheduledDateTime))
                {
                    // a same alarm is found.
                    duplicate.DeepCopy(item);
                    return true;
                }
            }
            // there's no same alarm.
            duplicate = null;
            return false;
        }
    }
}