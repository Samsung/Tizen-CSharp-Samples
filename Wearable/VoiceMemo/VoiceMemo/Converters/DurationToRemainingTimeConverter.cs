/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
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

namespace VoiceMemo.Converters
{
    public enum RemainingTimeType
    {
        TimeText,
    }

    /// <summary>
    /// Class DurationToRemainingTimeConverter
    /// It converts duration information to remaining time.
    /// format - minutes:seconds
    /// </summary>
    public class DurationToRemainingTimeConverter : IValueConverter
    {
        /// <summary>
        /// Converting source value to target value
        /// </summary>
        /// <param name="value">Source object</param>
        /// <param name="targetType">The target type to convert</param>
        /// <param name="parameter">parameter object</param>
        /// <param name="culture">The culture info</param>
        /// <returns>Returns converted bool to decide UI widget's visibility</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int remains = System.Convert.ToInt32(value);
            RemainingTimeType type = (RemainingTimeType)parameter;
            switch (type)
            {
                case RemainingTimeType.TimeText:
                    int minutes = remains / 60000;
                    int seconds = (remains - minutes * 60000) / 1000;
                    //Console.WriteLine("[DurationToRemainingTimeConverter  -  TimeText] remaining time : " + remains);
                    // return the remaining time, formatted as {minutes}:{seconds}
                    return String.Format("{0:00}:{1:00}", minutes, seconds);
                default:
                    return null;
            }
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
