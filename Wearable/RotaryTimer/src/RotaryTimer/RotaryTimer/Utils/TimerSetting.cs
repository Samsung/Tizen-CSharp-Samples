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
namespace RotaryTimer.Utils
{
    /// <summary>
    /// Timer setting class.
    /// </summary>
    public class TimerSetting
    {
        #region properties

        /// <summary>
        /// Hours value.
        /// </summary>
        public int Hours { get; }

        /// <summary>
        /// Minutes value.
        /// </summary>
        public int Minutes { get; }

        /// <summary>
        /// Seconds value.
        /// </summary>
        public int Seconds { get; }

        /// <summary>
        /// Setting mode.
        /// </summary>
        public SettingMode SettingMode { get; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes TimerSetting class instance.
        /// </summary>
        /// <param name="hours">Hours value.</param>
        /// <param name="minutes">Minutes value.</param>
        /// <param name="seconds">Seconds value.</param>
        /// <param name="settingMode">Setting mode.</param>
        public TimerSetting(int hours, int minutes, int seconds, SettingMode settingMode)
        {
            this.Hours = hours;
            this.Minutes = minutes;
            this.Seconds = seconds;
            this.SettingMode = settingMode;
        }

        #endregion
    }
}
