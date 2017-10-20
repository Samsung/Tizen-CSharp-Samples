using System;
using System.Globalization;
using Xamarin.Forms;

namespace Clock.Converters
{
    public class StringToDecoratedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return value.ToString() + " Converter ";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return " RConverter "  + value.ToString();
        }
    }
}
