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
using VoiceMemo.Resx;
using Xamarin.Forms;

namespace VoiceMemo.Converters
{
    public enum SttPropertyType
    {
        // Text : "On", "Off"
        TextString,
        // Image
        ImageSource,
        RecordImageSource,
    }

    /// <summary>
    /// Converter class
    /// According to whether or not STT feature usability is on,
    /// this converter class will provider the proper text and the path of image file .
    /// </summary>
    public class SttToPropertyConverter : IValueConverter
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
            bool isSttOn = (bool)System.Convert.ToBoolean(value);
            SttPropertyType type = (SttPropertyType)parameter;
            //Console.WriteLine("[SttToPropertyConverter] isSttOn: " + isSttOn + ", type:" + type);
            switch (type)
            {
                case SttPropertyType.TextString:
                    return isSttOn ? AppResources.SttOn : AppResources.SttOff;
                case SttPropertyType.ImageSource:
                    return isSttOn ? "more_option_icon_stt_on.png" : "more_option_icon_stt_off.png";
                case SttPropertyType.RecordImageSource:
                    return isSttOn ? "voicerecorder_icon_stt.png" : "voicerecorder_icon_stt_off.png";
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
