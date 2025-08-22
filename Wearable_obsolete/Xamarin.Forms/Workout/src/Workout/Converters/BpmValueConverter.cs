using System;
using System.Globalization;
using Xamarin.Forms;

namespace Workout.Converters
{
    /// <summary>
    /// Class that converts bpm values which are lower than or equal to 0 to "--" string.
    /// </summary>
    public class BpmValueConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts values which are lower than or equal to 0 to "--" string.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToInt32(value) <= 0 ? "--" : value;
        }

        /// <summary>
        /// Does nothing, but it must be defined, because it is in "IValueConverter" interface.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
