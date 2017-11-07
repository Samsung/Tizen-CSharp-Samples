using System;
using System.Reflection;
using SQLite;
using Native = Tizen.Applications;

namespace Clock.Models
{
    public class AlarmRecord
    {
        public DateTime ScheduledDateTime { get; set; }
        public AlarmWeekFlag WeekFlag { get; set; }
        public AlarmTypes AlarmType { get; set; }
        public float Volume { get; set; }
        public AlarmToneTypes AlarmToneType { get; set; }
        public bool Snooze { get; set; }
        public string AlarmName { get; set; }
        public bool Repeat { get; set; }
        public bool OnOff { get; set; }
        [PrimaryKey]
        public TimeSpan DateCreated { get; set; }
        public int? NativeAlarmId { get; set; }

        public override string ToString()
        {
            string s = "";

            foreach (
                PropertyInfo propertyInfo in
                this.GetType().GetRuntimeProperties())
            {
                s += propertyInfo + ":" + propertyInfo.GetValue(this, null) + "\n";
            }

            return s;
        }

        private static Native.AlarmWeekFlag Convert(AlarmWeekFlag flag)
        {
            Native.AlarmWeekFlag ret = Native.AlarmWeekFlag.AllDays;
            switch (flag)
            {
                case AlarmWeekFlag.Monday:
                    ret = Native.AlarmWeekFlag.Monday;
                    break;
                case AlarmWeekFlag.Tuesday:
                    ret = Native.AlarmWeekFlag.Tuesday;
                    break;
                case AlarmWeekFlag.Wednesday:
                    ret = Native.AlarmWeekFlag.Wednesday;
                    break;
                case AlarmWeekFlag.Thursday:
                    ret = Native.AlarmWeekFlag.Thursday;
                    break;
                case AlarmWeekFlag.Friday:
                    ret = Native.AlarmWeekFlag.Friday;
                    break;
                case AlarmWeekFlag.WeekDays:
                    ret = Native.AlarmWeekFlag.WeekDays;
                    break;
                case AlarmWeekFlag.AllDays:
                    ret = Native.AlarmWeekFlag.AllDays;
                    break;
                default:
                    break;
            }

            return ret;
        }

        internal static int? SaveAlarm(AlarmRecord alarmRecord)
        {
            Native.AppControl appControl = new Native.AppControl()
            {
                ApplicationId = Native.Application.Current.ApplicationInfo.ApplicationId
            };
            appControl.ExtraData.Add("AlarmRecord.DateCreated", alarmRecord.DateCreated.ToString());
            Native.AlarmWeekFlag nativeFlag = Convert(alarmRecord.WeekFlag);
            Native.Alarm nativeAlarm = Native.AlarmManager.CreateAlarm(alarmRecord.ScheduledDateTime, nativeFlag, appControl);
            return nativeAlarm.AlarmId;
        }
    }
}
