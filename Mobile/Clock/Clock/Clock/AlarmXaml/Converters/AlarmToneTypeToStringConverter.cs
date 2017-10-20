using Clock.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Clock.Converters 
{
    public class AlarmToneTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            AlarmToneTypes flag = (AlarmToneTypes)value;

            if (flag == AlarmToneTypes.AlarmMp3)
            {
                return "alarm.mp3";
            }
            else if (flag == AlarmToneTypes.RingtoneSdk)
            {
                return "Sdk";
            }
            else
            {
                return "Default";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
