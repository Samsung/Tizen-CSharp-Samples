using System;
using System.Globalization;
using Xamarin.Forms;

namespace Clock.Converters
{
    public class DateTimeToAmPmConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime))
            {
                throw new InvalidOperationException("The target must be a DateTime");
            }

            var date = (DateTime)value;
            if (date.Hour >= 12 && date.Hour <= 23) return "PM";
            else return "AM";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}