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
    /// <summary>
    /// The type of view
    /// </summary>
    public enum ViewType
    {
        /// <summary>
        /// View which is shown when there is no record
        /// </summary>
        NoRecordView,
        /// <summary>
        /// List view when there are some records
        /// </summary>
        RecordListView,
        RecordCheckView,
    }

    /// <summary>
    /// converter class
    /// Change the visibility of view based on the number of voice records
    /// </summary>
    public class RecordsCountToViewVisibilityConverter : IValueConverter
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
            int RecordsCnt = System.Convert.ToInt32(value);
            ViewType viewType = (ViewType)parameter;
            //Console.WriteLine("[RecordsCountToViewVisibilityConverter] RecordsCount = " + RecordsCnt + ", viewType :" + viewType);

            switch (viewType)
            {
                case ViewType.NoRecordView:
                    return RecordsCnt == 0 ? true : false;
                case ViewType.RecordListView:
                    return RecordsCnt == 0 ? false : true;
                case ViewType.RecordCheckView:
                    return RecordsCnt == 0 ? false : true;
                default:
                    return true;
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
