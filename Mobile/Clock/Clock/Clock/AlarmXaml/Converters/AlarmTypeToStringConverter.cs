using Clock.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Clock.Converters
{
    public class AlarmTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            AlarmTypes flag = (AlarmTypes)value;

            if (flag == AlarmTypes.Sound)
            {
                return "Sound";
            }
            else if (flag == AlarmTypes.Vibration)
            {
                return "Vibration";
            }
            else
            {
                return "Vibration and sound";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
