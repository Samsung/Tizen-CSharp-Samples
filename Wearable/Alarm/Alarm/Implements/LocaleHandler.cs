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
using System.Threading;
using Xamarin.Forms;
using Alarm.Models;

namespace Alarm.Implements
{
    public class LocaleHandler
    {
        CultureInfo _currentCultureInfo;

        public LocaleHandler()
        {
            _currentCultureInfo = GetCurrentCultureInfo();
            // To get notified when system locale settings has been changed
            SystemSettings.LocaleLanguageChanged += OnLanguageChanged;
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
        /// Set the current CultureInfo
        /// </summary>
        /// <param name="info">CultureInfo</param>
        public void SetLocale(CultureInfo info)
        {
            Thread.CurrentThread.CurrentCulture = info;
            Thread.CurrentThread.CurrentUICulture = info;
        }

        /// <summary>
        /// Get CultureInfo from the locale information
        /// </summary>
        /// <returns>CultureInfo</returns>
        CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            var TizenLocale = SystemSettings.LocaleLanguage;
            netLanguage = TizenToDotnetLanguage(TizenLocale.ToString().Replace("_", "-"));
            CultureInfo info = null;
            try
            {
                info = new CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException e1)
            {
                Console.WriteLine($"cannot find the current cultureInfo. so use 'en'. {e1.Message}");
                info = new CultureInfo("en");
            }

            return info;
        }

        /// <summary>
        /// Invoked when system locale setting has been changed
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">LocaleLanguageChangedEventArgs</param>
        private void OnLanguageChanged(object sender, LocaleLanguageChangedEventArgs e)
        {
            Console.WriteLine($"OnLanguageChanged");
            CultureInfo info = GetCurrentCultureInfo();
            _currentCultureInfo = info;
            SetLocale(info);

            // set the RESX for resource localization
            Resx.AppResources.Culture = info;

            // Notify the change of locale information
            MessagingCenter.Send<LocaleHandler,CultureInfo>(this, MessageKeys.LanguageChanged, info);
        }

        string TizenToDotnetLanguage(string tizenLanguage)
        {
            var netLanguage = tizenLanguage;
            //certain languages need to be converted to CultureInfo equivalent
            switch (tizenLanguage)
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
            }

            //Console.WriteLine($".NET Language/Locale:{netLanguage}");
            return netLanguage;
        }
    }
}
