using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace Workout.Converters
{
    /// <summary>
    /// Class that converts Bpm range value to corresponding string.
    /// </summary>
    public class BpmRangeValueConverter : IValueConverter
    {
        #region fields

        /// <summary>
        /// Dictionary of Bpm range names.
        /// </summary>
        private readonly Dictionary<string, string> _bpmRangeNamesDictionary = new Dictionary<string, string>
        {
            { "0", "rest" },
            { "1", "very light" },
            { "2", "light" },
            { "3", "moderate" },
            { "4", "hard" },
            { "5", "maximum" }
        };

        #endregion

        #region methods

        /// <summary>
        /// Converts value to corresponding string.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_bpmRangeNamesDictionary.TryGetValue(value.ToString(), out string bpmRangeValue))
            {
                return bpmRangeValue;
            }
            else
            {
                return value;
            }
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
