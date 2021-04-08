/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using AppCommon.Tizen.Mobile;
using AppCommon.Interfaces;
using System.Globalization;
using TSystem = Tizen.System;
using System.Diagnostics;

[assembly: Xamarin.Forms.Dependency(typeof(SystemSettings))]

namespace AppCommon.Tizen.Mobile
{
    /// <summary>
    /// A class for an system settings
    /// </summary>
    class SystemSettings : ISystemSettings
    {
        /// <summary>
        /// Language information
        /// </summary>
        public string Language
        {
            get
            {
                return new CultureInfo(TSystem.SystemSettings.LocaleLanguage).DisplayName;
            }
        }
    }
}