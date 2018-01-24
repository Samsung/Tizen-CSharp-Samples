/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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
using SystemInfo.Model.Settings;
using SystemInfo.Tizen.Mobile.Service;
using Tizen;
using SystemSettings = Tizen.System.SystemSettings;

[assembly: Xamarin.Forms.Dependency(typeof(SettingsService))]

namespace SystemInfo.Tizen.Mobile.Service
{
    /// <summary>
    /// Provides methods that allow to obtain information about system settings.
    /// </summary>
    public class SettingsService : ISettings
    {
        #region fields

        /// <summary>
        /// Message that is displayed when device is not supported on this platform or device.
        /// </summary>
        private const string NOT_SUPPORTED_EXCEPTION_MSG = "Not supported on this device";

        #endregion

        #region properties

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
        /// The file path of the current ring tone.
        /// </summary>
        public string IncomingCallRingtone
        {
            get
            {
                try
                {
                    return SystemSettings.IncomingCallRingtone;
                }
                catch (NotSupportedException)
                {
                    return NOT_SUPPORTED_EXCEPTION_MSG;
                }
            }
        }

        /// <summary>
        /// The file path of the current email alert ring tone.
        /// </summary>
        public string EmailAlertRingtone
        {
            get
            {
                try
                {
                    return SystemSettings.EmailAlertRingtone;
                }
                catch (NotSupportedException)
                {
                    return NOT_SUPPORTED_EXCEPTION_MSG;
                }
            }
        }

        /// <summary>
        /// The file path of the current home screen wallpaper.
        /// </summary>
        public string WallpaperHomeScreen
        {
            get
            {
                try
                {
                    return SystemSettings.WallpaperHomeScreen;
                }
                catch (NotSupportedException)
                {
                    return NOT_SUPPORTED_EXCEPTION_MSG;
                }
            }
        }

        /// <summary>
        /// The file path of the current lock screen wallpaper.
        /// </summary>
        public string WallpaperLockScreen
        {
            get
            {
                try
                {
                    return SystemSettings.WallpaperLockScreen;
                }
                catch (NotSupportedException)
                {
                    return NOT_SUPPORTED_EXCEPTION_MSG;
                }
            }
        }

        /// <summary>
        /// The current system font size.
        /// </summary>
        public FontSize FontSize => EnumMapper.FontSizeMapper(SystemSettings.FontSize);

        /// <summary>
        /// The current system font type.
        /// </summary>
        public string FontType => SystemSettings.FontType;

        /// <summary>
        /// The current system default font type.
        /// </summary>
        public string DefaultFontType => SystemSettings.DefaultFontType;

        /// <summary>
        /// Indicates if the screen lock sound is enabled.
        /// </summary>
        public bool SoundLockEnabled => SystemSettings.SoundLockEnabled;

        /// <summary>
        /// Indicates if the device is in the silent mode.
        /// </summary>
        public bool SilentModeEnabled => SystemSettings.SoundSilentModeEnabled;

        /// <summary>
        /// Indicates if the screen touch sound is enabled.
        /// </summary>
        public bool SoundTouchEnabled => SystemSettings.SoundTouchEnabled;

        /// <summary>
        /// Indicates the file path of the current notification tone set by the user.
        /// </summary>
        public string SoundNotification
        {
            get
            {
                try
                {
                    return SystemSettings.SoundNotification;
                }
                catch (NotSupportedException)
                {
                    return NOT_SUPPORTED_EXCEPTION_MSG;
                }
            }
        }

        /// <summary>
        /// Indicates the interval for sending notifications.
        /// </summary>
        public int SoundNotificationRepetitionPeriod => SystemSettings.SoundNotificationRepetitionPeriod;

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
        /// Event invoked when locale settings have changed.
        /// </summary>
        public event EventHandler<EventArgs> LocaleChanged;

        /// <summary>
        /// Event invoked when user settings have changed.
        /// </summary>
        public event EventHandler<EventArgs> UserSettingsChanged;

        /// <summary>
        /// Event invoked when fonts settings have changed.
        /// </summary>
        public event EventHandler<EventArgs> FontChanged;

        /// <summary>
        /// Event invoked when sounds settings have changed.
        /// </summary>
        public event EventHandler<EventArgs> SoundChanged;

        /// <summary>
        /// Event invoked when other settings have changed.
        /// </summary>
        public event EventHandler<EventArgs> OtherChanged;

        #endregion

        #region methods

        /// <summary>
        /// Starts observing system settings for changes.
        /// </summary>
        /// <remarks>
        /// System settings events will be never invoked before calling this method.
        /// </remarks>
        public void StartListening()
        {
            SystemSettings.LocaleCountryChanged +=
                (s, e) => { LocaleChanged?.Invoke(s, new EventArgs()); };
            SystemSettings.LocaleLanguageChanged +=
                (s, e) => { LocaleChanged?.Invoke(s, new EventArgs()); };
            SystemSettings.LocaleTimeFormat24HourSettingChanged +=
                (s, e) => { LocaleChanged?.Invoke(s, new EventArgs()); };
            SystemSettings.LocaleTimeZoneChanged +=
                (s, e) => { LocaleChanged?.Invoke(s, new EventArgs()); };

            try
            {
                SystemSettings.IncomingCallRingtoneChanged +=
                    (s, e) => { UserSettingsChanged?.Invoke(s, new EventArgs()); };
            }
            catch (NotSupportedException e)
            {
                Log.Warn("SystemInfo", e.Message);
            }

            try
            {
                SystemSettings.EmailAlertRingtoneChanged +=
                    (s, e) => { UserSettingsChanged?.Invoke(s, new EventArgs()); };
            }
            catch (NotSupportedException e)
            {
                Log.Warn("SystemInfo", e.Message);
            }

            try
            {
                SystemSettings.WallpaperHomeScreenChanged +=
                    (s, e) => { UserSettingsChanged?.Invoke(s, new EventArgs()); };
            }
            catch (NotSupportedException e)
            {
                Log.Warn("SystemInfo", e.Message);
            }

            try
            {
                SystemSettings.WallpaperLockScreenChanged +=
                    (s, e) => { UserSettingsChanged?.Invoke(s, new EventArgs()); };
            }
            catch (NotSupportedException e)
            {
                Log.Warn("SystemInfo", e.Message);
            }

            SystemSettings.FontSizeChanged += (s, e) => { FontChanged?.Invoke(s, new EventArgs()); };
            SystemSettings.FontTypeChanged += (s, e) => { FontChanged?.Invoke(s, new EventArgs()); };

            SystemSettings.SoundLockSettingChanged +=
                (s, e) => { SoundChanged?.Invoke(s, new EventArgs()); };
            SystemSettings.SoundSilentModeSettingChanged +=
                (s, e) => { SoundChanged?.Invoke(s, new EventArgs()); };
            SystemSettings.SoundTouchSettingChanged +=
                (s, e) => { SoundChanged?.Invoke(s, new EventArgs()); };
            SystemSettings.SoundNotificationRepetitionPeriodChanged +=
                (s, e) => { SoundChanged?.Invoke(s, new EventArgs()); };

            SystemSettings.DeviceNameChanged += (s, e) => { OtherChanged?.Invoke(s, new EventArgs()); };
            SystemSettings.MotionSettingChanged += (s, e) => { OtherChanged?.Invoke(s, new EventArgs()); };
        }

        #endregion
    }
}