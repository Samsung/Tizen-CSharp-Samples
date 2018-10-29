/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
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
using RotaryTimer.Utils;

namespace RotaryTimer.Converters
{
    /// <summary>
    /// Converts timer setting mode value to background filename.
    /// </summary>
    public class ModeToBackgroundConverter : IValueConverter
    {
        #region fields

        /// <summary>
        /// Filename of image for setting hours.
        /// </summary>
        private const string BACKGROUND_HOURS = "board_setup_hour_bg.png";

        /// <summary>
        /// Filename of image for setting minutes and seconds.
        /// </summary>
        private const string BACKGROUND_MINS_SECS = "board_setup_min_sec_bg.png";

        #endregion

        #region methods

        /// <summary>
        /// Converts mode value to filename.
        /// </summary>
        /// <param name="value">Timer setting mode.</param>
        /// <param name="targetType">Type of the data binding target property.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">CultureInfo object allowing culture specific conversion.</param>
        /// <returns>Image source filename.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string filename = "";

            if (value is SettingMode settingMode)
            {
                switch (settingMode)
                {
                    case SettingMode.HOURS:
                        filename = BACKGROUND_HOURS;
                        break;
                    case SettingMode.MINUTES:
                        filename = BACKGROUND_MINS_SECS;
                        break;
                    case SettingMode.SECONDS:
                        filename = BACKGROUND_MINS_SECS;
                        break;
                    default:
                        break;
                }
            }

            return filename;
        }

        /// <summary>
        /// Converts filename to setting mode.
        /// Not required by the application, so it is not implemented.
        /// </summary>
        /// <param name="value">Value to be converted back.</param>
        /// <param name="targetType">Type of the data binding target property.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">CultureInfo object allowing culture specific conversion.</param>
        /// <returns>Setting mode representing converted filename.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("Not implemented");
        }

        #endregion
    }
}