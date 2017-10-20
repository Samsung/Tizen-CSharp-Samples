using Clock.Models;
using System;
using System.Globalization;
using Xamarin.Forms;


namespace Clock.Converters
{
    public class WeekdayFlagToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            AlarmWeekFlag flag = (AlarmWeekFlag)value;
            string s = "";
            if (flag == 0)
            {
                s = "Never";
            }
            else if (flag == AlarmWeekFlag.AllDays)
            {
                s = "Everyday";
            }
            else
            {
                for (int i = 1; i < 7; i++)
                {
                    int mask = 1 << i;
                    if (((int)flag & mask) > 0)
                    {
                        switch (mask)
                        {
                            case (int)AlarmWeekFlag.Monday:
                                s += "Mon ";
                                break;
                            case (int)AlarmWeekFlag.Tuesday:
                                s += "Tue ";
                                break;
                            case (int)AlarmWeekFlag.Wednesday:
                                s += "Wed ";
                                break;
                            case (int)AlarmWeekFlag.Thursday:
                                s += "Thu ";
                                break;
                            case (int)AlarmWeekFlag.Friday:
                                s += "Fri ";
                                break;
                            case (int)AlarmWeekFlag.Saturday:
                                s += "Sat ";
                                break;
                        }
                    }
                }

                if (((int)flag & 0x1) > 0)
                {
                    s += "Sun ";
                }
            }

            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}