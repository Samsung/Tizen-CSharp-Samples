/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Clock.Alarm;
using Clock.Styles;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Clock.Converters
{
    /// <summary>
    /// Components in Alarm Model
    /// </summary>
    public enum AlarmModelComponent
    {
        /// <summary>
        /// for Time Label
        /// </summary>
        Time,
        /// <summary>
        /// for AM/PM label
        /// </summary>
        AmPm,
        /// <summary>
        /// for repeat image
        /// </summary>
        Repeat,
        /// <summary>
        /// for alarm name label
        /// </summary>
        Name,
        /// <summary>
        /// for week days label
        /// </summary>
        Weekly,
        /// <summary>
        /// for date label
        /// </summary>
        Date,
        State,
    }

    /// <summary>
    /// Converter class
    /// This class converts bindable source value to proper target value
    /// Depending on the repeat image's visibility, control Alarm name label's translationX value
    /// </summary>
    class AlarmStateToPropertyConverter : IValueConverter
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
        /// <returns>Depending on the repeat image's visibility, control Alarm name label's translationX value</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            AlarmStates activeItem = (AlarmStates)value;
            AlarmModelComponent comp = (AlarmModelComponent)parameter;
            if (activeItem == AlarmStates.Inactive)
            {
                switch (comp)
                {
                    case AlarmModelComponent.Time:
                        return AlarmStyle.ATO001D;
                    case AlarmModelComponent.AmPm:
                        return AlarmStyle.ATO002D;
                    case AlarmModelComponent.Name:
                        return AlarmStyle.ATO003D;
                    case AlarmModelComponent.Weekly:
                        return AlarmStyle.ATO004D;
                    case AlarmModelComponent.Date:
                        return AlarmStyle.ATO044D;
                    case AlarmModelComponent.State:
                        return false;
                    case AlarmModelComponent.Repeat:
                        return Color.FromHex("66000000");
                    default:
                        return AlarmStyle.ATO001D;
                }
            }
            else
            {
                switch (comp)
                {
                    case AlarmModelComponent.Time:
                        return AlarmStyle.ATO001;
                    case AlarmModelComponent.AmPm:
                        return AlarmStyle.ATO002;
                    case AlarmModelComponent.Name:
                        return AlarmStyle.ATO003;
                    case AlarmModelComponent.Weekly:
                        return AlarmStyle.ATO004;
                    case AlarmModelComponent.Date:
                        return AlarmStyle.ATO044;
                    case AlarmModelComponent.State:
                        return true;
                    case AlarmModelComponent.Repeat:
                        return Color.FromHex("FFFFFF");
                    default:
                        return AlarmStyle.ATO001;
                }
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