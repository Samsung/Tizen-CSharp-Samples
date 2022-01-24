using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Tizen.Applications;

namespace Alarms.Services
{
    public static class AlarmListService
    {
        public static List<string> GetAlarmList()
        {
            List<string> alarmList = new List<string>();
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
                if(alarm != null)
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
                    alarmList.Add(alarm.ToString());
                }
            }
            return alarmList;
        }
    }
}
