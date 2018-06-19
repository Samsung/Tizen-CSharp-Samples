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
using Tizen.System;
using VoiceMemo.Services;
using VoiceMemo.Tizen.Wearable.Services;
using VoiceMemo.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocaleService))]

namespace VoiceMemo.Tizen.Wearable.Services
{
    class LocaleService : ILocaleService
    {
        CultureInfo _currentCultureInfo;

        public LocaleService()
        {
            _currentCultureInfo = GetCurrentCultureInfo();
            // To get notified when system locale settings has been changed
            SystemSettings.LocaleLanguageChanged += SystemSettings_LocaleLanguageChanged;
        }

        /// <summary>
        /// Get the current CultureInfo
        /// </summary>
        public CultureInfo CurrentCultureInfo
        {
            get
            {
                return _currentCultureInfo;
            }
        }

        /// <summary>
        /// Get CultureInfo from the locale information
        /// </summary>
        /// <returns>CultureInfo</returns>
        CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            var TizenLocale = SystemSettings.LocaleLanguage;
            //Console.WriteLine("[GetCurrentCultureInfo] TizenLocale (" + TizenLocale + ")");
            netLanguage = TizenToDotnetLanguage(TizenLocale.ToString().Replace("_", "-"));
            //Console.WriteLine("[GetCurrentCultureInfo] netLanguage (" + netLanguage + ")");
            CultureInfo info = null;
            try
            {
                info = new CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException e1)
            {
                Console.WriteLine("[GetCurrentCultureInfo] cannot find the current cultureInfo. so use 'en'. (" + e1.Message + ")");
                info = new CultureInfo("en");
            }

            return info;
        }
        /// <summary>
        /// Invoked when system locale setting has been changed
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">LocaleLanguageChangedEventArgs</param>
        private void SystemSettings_LocaleLanguageChanged(object sender, LocaleLanguageChangedEventArgs e)
        {
            CultureInfo info = GetCurrentCultureInfo();
            _currentCultureInfo = info;
            // Notify the change of locale information
            MessagingCenter.Send<ILocaleService, CultureInfo>(this, MessageKeys.LanguageChanged, info);
        }

        string TizenToDotnetLanguage(string androidLanguage)
        {
            var netLanguage = androidLanguage;
            //certain languages need to be converted to CultureInfo equivalent
            switch (androidLanguage)
            {
                case "zh-CN":   // Chinese Simplified (People's Republic of China)
                    netLanguage = "zh-Hans"; // correct code for .NET
                    break;
                case "zh-HK":  // Chinese Traditional (Hong Kong)
                case "zh-hk":
                case "zh-tw":  // Chinese Traditional (Taiwan)
                case "zh-TW":
                    netLanguage = "zh-Hant"; // correct code for .NET
                    break;
                    //case "ar-ae":   // Arabic
                    //    netLanguage = "ar-sa"; // closest supported for .Net : Arabic (Saudi Arabia)
                    //    break;
                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }

            Console.WriteLine(".NET Language/Locale:" + netLanguage);
            return netLanguage;
        }
    }
}
