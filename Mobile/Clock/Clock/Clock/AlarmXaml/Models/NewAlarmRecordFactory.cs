using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock.Models
{
    public class NewAlarmRecordFactory
    {
        public static AlarmRecord GetNewAlarm()
        {
            DateTime dt = DateTime.Now;

            AlarmRecord newRecord = new AlarmRecord
            {
                AlarmName = "",
                AlarmToneType = AlarmToneTypes.Default,
                AlarmType = AlarmTypes.SoundVibration,
                DateCreated = dt.TimeOfDay,
                OnOff = true,
                Repeat = false,
                ScheduledDateTime = dt,
                Snooze = false,
                Volume = 0.5f,
                WeekFlag = AlarmWeekFlag.AllDays,
                NativeAlarmId = null
            };
            return newRecord;
        }

        public static AlarmRecord GetNewAlarm(AlarmRecord source)
        {
            AlarmRecord newRecord = new AlarmRecord
            {
                AlarmName = source.AlarmName,
                AlarmToneType = source.AlarmToneType,
                AlarmType = source.AlarmType,
                DateCreated = source.DateCreated,
                OnOff = source.OnOff,
                Repeat = source.Repeat,
                ScheduledDateTime = source.ScheduledDateTime,
                Snooze = source.Snooze,
                Volume = source.Volume,
                WeekFlag = source.WeekFlag,
                NativeAlarmId = source.NativeAlarmId
            };
            return newRecord;
        }
    }
}
