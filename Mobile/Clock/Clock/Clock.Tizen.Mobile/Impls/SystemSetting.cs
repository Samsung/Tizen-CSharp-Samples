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

using Clock.Interfaces;
using Clock.Tizen.Mobile.Impls;
using Native = Tizen.System;

[assembly: Xamarin.Forms.Dependency(typeof(SystemSetting))]

namespace Clock.Tizen.Mobile.Impls
{
    /// <summary>
    /// The SystemSetting class to get system setting information
    /// </summary>
    class SystemSetting : ISystemSetting
    {
        public SystemSetting()
        {
        }

        /// <summary>
        /// Checks whether system uses 24-hour time format
        /// </summary>
        /// <returns> Returns true if 24-hour time format is set; otherwise, returns false </returns>
        public bool Is24HourTimeFormatted()
        {
            return Native.SystemSettings.LocaleTimeFormat24HourEnabled;
        }

        /// <summary>
        /// Gets system setting value associated with the specified key
        /// </summary>
        /// <param name="key">The key name of system setting</param>
        /// <returns>Returns a string representation of the current system settings value of the given key</returns>
        public string GetString(string key)
        {
            if (key == "LOCALE_TIMEZONE")
            {
                return Native.SystemSettings.LocaleTimeZone;
            }

            return null;
        }
    }
}
