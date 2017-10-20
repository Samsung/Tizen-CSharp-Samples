using System;
using System.Globalization;
using Xamarin.Forms;

namespace Clock.Converters
{
    class AlarmDateTimeToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime))
            {
                throw new InvalidOperationException("The target must be a DateTime");
            }

            var date = (DateTime)value;

            return date.TimeOfDay;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TimeSpan))
            {
                throw new InvalidOperationException("The target must be a DateTime");
            }

            DateTime dt = new DateTime(1970, 1, 1) + (TimeSpan)value;

            return dt;
        }
    }
}
