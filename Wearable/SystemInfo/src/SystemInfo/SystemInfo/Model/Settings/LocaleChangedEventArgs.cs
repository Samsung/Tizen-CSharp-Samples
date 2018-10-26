/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;

namespace SystemInfo.Model.Settings
{
    /// <summary>
    /// Class that is passed with LocaleChanged event.
    /// </summary>
    public class LocaleChangedEventArgs : EventArgs
    {
        #region properties

        /// <summary>
        /// Indicates the current country setting.
        /// </summary>
        public string LocaleCountry { get; set; }

        /// <summary>
        /// Indicates the current language setting.
        /// </summary>
        public string LocaleLanguage { get; set; }

        /// <summary>
        /// Indicates if the 24-hour or 12-hour clock is used.
        /// </summary>
        public string LocaleTimeFormat { get; set; }

        /// <summary>
        /// Indicates the current time zone.
        /// </summary>
        public string LocaleTimeZone { get; set; }

        #endregion
    }
}