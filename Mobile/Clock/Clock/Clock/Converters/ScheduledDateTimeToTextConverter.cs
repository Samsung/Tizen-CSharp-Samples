/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Globalization;
using Xamarin.Forms;

namespace Clock.Converters
{
    /// <summary>
    /// Label type in AlarmList's custom cell
    /// </summary>
    public enum LabelType
    {
        // For Time Label
        Time,
        // For AM/PM Label
        AmPm,
    }

    /// <summary>
    /// Converter class
    /// This class converts bindable source value to proper target value
    /// Depending on the repeat image's visibility, control Alarm name label's translationX value
    /// </summary>
    class ScheduledDateTimeToTextConverter : IValueConverter
    {
        /// <summary>
        /// Converting source value to target value
        /// </summary>
        /// <param name="value">Source object</param>
        /// <seealso cref="System.object">
        /// <param name="targetType">The target type to convert</param>
        /// <seealso cref="Type">
        /// <param name="CultureInfo">The culture info</param>
        /// <seealso cref="CultureInfo">
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime time = (DateTime)value;
            LabelType type = (LabelType)parameter;
            string text = "";
            switch (type)
            {
                case LabelType.Time:
                    if (((App)Application.Current).Is24hourFormat)
                    {
                        text = time.ToString("HH") + ":" + time.ToString("mm");
                    }
                    else
                    {
                        text = time.ToString("hh") + ":" + time.ToString("mm");
                    }

                    break;
                case LabelType.AmPm:
                    text = time.ToString("tt");
                    break;
                default:
                    break;
            }

            return text;
        }

        /// <summary>
        /// Converting back source value to target value
        /// This method is not being used in this app.
        /// </summary>
        /// <param name="value">Source object</param>
        /// <seealso cref="System.object">
        /// <param name="targetType">The target type to convert</param>
        /// <seealso cref="Type">
        /// <param name="CultureInfo">The culture info</param>
        /// <seealso cref="CultureInfo">
        /// <returns>Returns null</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}