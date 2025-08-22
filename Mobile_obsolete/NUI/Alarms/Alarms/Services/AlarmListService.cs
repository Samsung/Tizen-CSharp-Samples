/* 
  * Copyright (c) 2022 Samsung Electronics Co., Ltd 
  * 
  * Licensed under the Flora License, Version 1.1 (the "License"); 
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
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Tizen.Applications;

namespace Alarms.Services
{
    public static class AlarmListService
    {
        public static List<Alarm> GetAlarmList()
        {
            List<Alarm> alarmList = new List<Alarm>();
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
                if (alarm != null)
                {
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

                    alarmList.Add(alarm);
                }
            }

            return alarmList;
        }

        /// <summary>
        /// Remove alarm passed in object reference.
        /// </summary>
        /// <param name="alarmReference">Alarm object.</param>
        static public void RemoveAlarm(object alarmReference)
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
    }
}