//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using Tizen.System;
using Xamarin.Forms;

namespace SystemInfo.Components
{
    /// <summary>
    /// An easy to access fascade for Tizen.System.SystemSettings with information like current language or timezone.  
    /// </summary>
    public class Settings : BindableObject
    {
        /// <summary>
        /// Indicates the current country setting.
        /// </summary>
        public string LocaleCountry => SystemSettings.LocaleCountry;

        /// <summary>
        /// Indicates the current language setting.
        /// </summary>
        public string LocaleLanguage => SystemSettings.LocaleLanguage;

        /// <summary>
        /// Indicates if the 24-hour or 12-hour clock is used.
        /// </summary>
        public string LocaleTimeFormat
            => SystemSettings.LocaleTimeFormat24HourEnabled ? "24-hour" : "12-hour";

        /// <summary>
        /// Indicates the current time zone.
        /// </summary>
        public string LocaleTimeZone => SystemSettings.LocaleTimeZone;

        /// <summary>
        /// The current system font size.
        /// </summary>
        public SystemSettingsFontSize FontSize => SystemSettings.FontSize;

        /// <summary>
        /// The current system font type.
        /// </summary>
        public string FontType => SystemSettings.FontType;

        /// <summary>
        /// The current system default font type.
        /// </summary>
        public string DefaultFontType => SystemSettings.DefaultFontType;

        /// <summary>
        /// Indicates device name.
        /// </summary>
        public string DeviceName => SystemSettings.DeviceName;

        /// <summary>
        /// Indicates if the device user has enabled motion feature.
        /// </summary>
        public bool MotionEnabled => SystemSettings.MotionEnabled;

        /// <summary>
        /// Indicates if the motion service is activated.
        /// </summary>
        public bool MotionActivationEnabled => SystemSettings.MotionActivationEnabled;

        /// <summary>
        /// Makes sure all the relevant "Changed" events are handled
        /// </summary>
        public Settings()
        {
            SystemSettings.FontSizeChanged += OnFontSizeChanged;
            SystemSettings.FontTypeChanged += OnFontTypeChanged;
            SystemSettings.DeviceNameChanged += OnDeviceNameChanged;
            SystemSettings.MotionSettingChanged += OnMotionSettingChanged;
            SystemSettings.LocaleCountryChanged += OnCountryChanged;
            SystemSettings.LocaleLanguageChanged += OnLanguageChanged;
            SystemSettings.LocaleTimeFormat24HourSettingChanged += OnTimeFormatChanged;
            SystemSettings.LocaleTimeZoneChanged += OnTimeZoneChanged;
        }


        /// <summary>
        /// Cleanup the event handlers assignement
        /// </summary>
        ~Settings()
        {
            SystemSettings.FontSizeChanged -= OnFontSizeChanged;
            SystemSettings.FontTypeChanged -= OnFontTypeChanged;
            SystemSettings.DeviceNameChanged -= OnDeviceNameChanged;
            SystemSettings.MotionSettingChanged -= OnMotionSettingChanged;
            SystemSettings.LocaleCountryChanged -= OnCountryChanged;
            SystemSettings.LocaleLanguageChanged -= OnLanguageChanged;
            SystemSettings.LocaleTimeFormat24HourSettingChanged -= OnTimeFormatChanged;
            SystemSettings.LocaleTimeZoneChanged -= OnTimeZoneChanged;
        }

        private void OnFontSizeChanged(object sender, FontSizeChangedEventArgs args)
        {
            OnPropertyChanged(nameof(FontSize));
        }

        private void OnFontTypeChanged(object sender, FontTypeChangedEventArgs args)
        {
            OnPropertyChanged(nameof(FontType));
        }
        private void OnDeviceNameChanged(object sender, DeviceNameChangedEventArgs args)
        {
            OnPropertyChanged(nameof(DeviceName));
        }

        private void OnMotionSettingChanged(object sender, MotionSettingChangedEventArgs args)
        {
            OnPropertyChanged(nameof(MotionEnabled));
        }

        private void OnCountryChanged(object sender, LocaleCountryChangedEventArgs args) {
            OnPropertyChanged(nameof(LocaleCountry));
        }

        private void OnLanguageChanged(object sender, LocaleLanguageChangedEventArgs args)
        {
            OnPropertyChanged(nameof(LocaleLanguage));
        }

        private void OnTimeFormatChanged(object sender, LocaleTimeFormat24HourSettingChangedEventArgs args)
        {
            OnPropertyChanged(nameof(LocaleTimeFormat));
        }

        private void OnTimeZoneChanged(object sender, LocaleTimeZoneChangedEventArgs args)
        {
            OnPropertyChanged(nameof(LocaleTimeZone));
        }
    }
}
