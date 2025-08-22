/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Alarms.Models;
using Alarms.Tizen.Mobile.Services;
using Alarms.ViewModels;
using Tizen.Applications;

[assembly: Xamarin.Forms.Dependency(typeof(AlarmListService))]

namespace Alarms.Tizen.Mobile.Services
{
    /// <summary>
    /// AlarmListService class.
    /// </summary>
    internal class AlarmListService : IAlarmListService
    {
        #region methods

        /// <summary>
        /// Converts value of AlarmWeekFlag enum to the DaysOfWeekFlags enum value.
        /// </summary>
        /// <param name="alarmWeekFlag">Input AlarmWeekFlag value.</param>
        /// <returns>Output DaysOfWeekFlags value.</returns>
        private DaysOfWeekFlags AlarmWeek2DaysOfWeek(AlarmWeekFlag alarmWeekFlag)
        {
            return new DaysOfWeekFlags
            {
                Monday = (alarmWeekFlag & AlarmWeekFlag.Monday) != 0,
                Tuesday = (alarmWeekFlag & AlarmWeekFlag.Tuesday) != 0,
                Wednesday = (alarmWeekFlag & AlarmWeekFlag.Wednesday) != 0,
                Thursday = (alarmWeekFlag & AlarmWeekFlag.Thursday) != 0,
                Friday = (alarmWeekFlag & AlarmWeekFlag.Friday) != 0,
                Saturday = (alarmWeekFlag & AlarmWeekFlag.Saturday) != 0,
                Sunday = (alarmWeekFlag & AlarmWeekFlag.Sunday) != 0
            };
        }

        /// <summary>
        /// Converts value of DaysOfWeekFlags enum to the AlarmWeekFlag enum value.
        /// </summary>
        /// <param name="weekFlags">Input DaysOfWeekFlags value.</param>
        /// <returns>Output AlarmWeekFlag value.</returns>
        private AlarmWeekFlag DaysOfWeek2AlarmWeek(DaysOfWeekFlags weekFlags)
        {
            AlarmWeekFlag retFlag = 0;  // Result flag returned at the end of method.

            retFlag = weekFlags.Monday ? retFlag | AlarmWeekFlag.Monday : retFlag;
            retFlag = weekFlags.Tuesday ? retFlag | AlarmWeekFlag.Tuesday : retFlag;
            retFlag = weekFlags.Wednesday ? retFlag | AlarmWeekFlag.Wednesday : retFlag;
            retFlag = weekFlags.Thursday ? retFlag | AlarmWeekFlag.Thursday : retFlag;
            retFlag = weekFlags.Friday ? retFlag | AlarmWeekFlag.Friday : retFlag;
            retFlag = weekFlags.Saturday ? retFlag | AlarmWeekFlag.Saturday : retFlag;
            retFlag = weekFlags.Sunday ? retFlag | AlarmWeekFlag.Sunday : retFlag;

            return retFlag;
        }

        /// <summary>
        /// Get list of currently set alarms for this application.
        /// </summary>
        /// <returns>Alarm list.</returns>
        public List<AlarmInfoViewModel> GetAlarmList()
        {
            var alarmList = new List<AlarmInfoViewModel>();

            IEnumerable<Alarm> systemAlarmList;
            try
            {
                systemAlarmList = AlarmManager.GetAllScheduledAlarms();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Cannot list scheduled alarms. Exception message: " + e.Message);
                return alarmList;
            }

            if (systemAlarmList == null)
            {
                return alarmList;
            }

            foreach (var alarm in systemAlarmList)
            {
                var appInfo = ApplicationManager.GetInstalledApplication(alarm.AlarmAppControl.ApplicationId);

                if (appInfo == null)
                {
                    continue;
                }

                int delay = 0;
                if (alarm.WeekFlag == 0 && alarm.Period != 0)
                {
                    delay = Convert.ToInt32((alarm.ScheduledDate - DateTime.Now).TotalSeconds);

                    // We don't allow delay equal or greater from 1 day so it would be some error we log and skip.
                    if (delay >= 24 * 60 * 60)
                    {
                        Debug.WriteLine("ERROR: Alarm delay over accepted maximum value!");
                        continue;
                    }
                }

                alarmList.Add(new AlarmInfoViewModel
                {
                    AlarmReference = alarm,
                    AlarmId = alarm.AlarmId,
                    AppInfo = new AppInfo(appInfo.Label, appInfo.ApplicationId),
                    Date = alarm.ScheduledDate,
                    IsRepeatEnabled = alarm.WeekFlag != 0 || alarm.Period > 0,
                    DaysFlags = AlarmWeek2DaysOfWeek(alarm.WeekFlag),
                    Delay = delay
                });
            }

            return alarmList;
        }

        /// <summary>
        /// Creates new alarm according to settings from alarmInfo values. If the Delay value is greater than 0,
        /// the alarm type is set to "delay" alarm type. Otherwise it is "exact date/time" alarm type.
        /// </summary>
        /// <param name="alarmInfo">Alarm object.</param>
        /// <returns>Created Alarm ID.</returns>
        public int SetAlarm(AlarmInfoViewModel alarmInfo)
        {
            AppControl appControl = new AppControl { ApplicationId = alarmInfo.AppInfo.AppId };
            Alarm createdAlarm;

            RemoveAlarm(alarmInfo.AlarmReference);

            try
            {
                if (alarmInfo.Delay > 0) // Delay alarm version.
                {
                    createdAlarm = AlarmManager.CreateAlarm(alarmInfo.Delay, appControl);
                }
                else // Exact date/time alarm version.
                {
                    if (alarmInfo.IsRepeatEnabled && alarmInfo.DaysFlags.IsAny())
                    {
                        createdAlarm = AlarmManager.CreateAlarm(alarmInfo.Date, DaysOfWeek2AlarmWeek(alarmInfo.DaysFlags), appControl);
                    }
                    else
                    {
                        createdAlarm = AlarmManager.CreateAlarm(alarmInfo.Date, appControl);
                    }
                }

                return createdAlarm.AlarmId;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Cannot create alarm. Exception message: " + e.Message);
            }

            return -1;
        }

        /// <summary>
        /// Remove alarm passed in object reference.
        /// </summary>
        /// <param name="alarmReference">Alarm object.</param>
        public void RemoveAlarm(object alarmReference)
        {
            try
            {
                ((Alarm)alarmReference)?.Cancel();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Cannot remove alarm. Exception message: " + e.Message);
            }
        }

        #endregion
    }
}
