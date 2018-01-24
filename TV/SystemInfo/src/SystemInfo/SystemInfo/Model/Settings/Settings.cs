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
using System.IO;
using Xamarin.Forms;

namespace SystemInfo.Model.Settings
{
    /// <summary>
    /// Class that holds information about settings.
    /// </summary>
    public class Settings
    {
        #region fields

        /// <summary>
        /// Service that provides information about settings.
        /// </summary>
        private readonly ISettings _service;

        #endregion

        #region properties

        /// <summary>
        /// Indicates the current country setting.
        /// </summary>
        public string LocaleCountry => _service.LocaleCountry;

        /// <summary>
        /// Indicates the current language setting.
        /// </summary>
        public string LocaleLanguage => _service.LocaleLanguage;

        /// <summary>
        /// Indicates if the 24-hour or 12-hour clock is used.
        /// </summary>
        public string LocaleTimeFormat => _service.LocaleTimeFormat;

        /// <summary>
        /// Indicates the current time zone.
        /// </summary>
        public string LocaleTimeZone => _service.LocaleTimeZone;


        /// <summary>
        /// The file name of the current ringtone.
        /// </summary>
        public string IncomingCallRingtone => Path.GetFileName(_service.IncomingCallRingtone);

        /// <summary>
        /// The file name of the current email alert ringtone.
        /// </summary>
        public string EmailAlertRingtone => Path.GetFileName(_service.EmailAlertRingtone);

        /// <summary>
        /// The file name of the current home screen wallpaper.
        /// </summary>
        public string WallpaperHomeScreen => Path.GetFileName(_service.WallpaperHomeScreen);

        /// <summary>
        /// The file name of the current lock screen wallpaper.
        /// </summary>
        public string WallpaperLockScreen => Path.GetFileName(_service.WallpaperLockScreen);

        /// <summary>
        /// The current system font size.
        /// </summary>
        public FontSize FontSize => _service.FontSize;

        /// <summary>
        /// The current system font type.
        /// </summary>
        public string FontType => _service.FontType;

        /// <summary>
        /// The current system default font type.
        /// </summary>
        public string DefaultFontType => _service.DefaultFontType;

        /// <summary>
        /// Indicates if the screen lock sound is enabled.
        /// </summary>
        public bool SoundLockEnabled => _service.SoundLockEnabled;

        /// <summary>
        /// Indicates if the device is in the silent mode.
        /// </summary>
        public bool SilentModeEnabled => _service.SilentModeEnabled;

        /// <summary>
        /// Indicates if the screen touch sound is enabled.
        /// </summary>
        public bool SoundTouchEnabled => _service.SoundTouchEnabled;

        /// <summary>
        /// Indicates the file name of the current notification tone set by the user.
        /// </summary>
        public string SoundNotification => Path.GetFileName(_service.SoundNotification);

        /// <summary>
        /// Indicates the time period for notification repetitions.
        /// </summary>
        public int SoundNotificationRepetitionPeriod => _service.SoundNotificationRepetitionPeriod;

        /// <summary>
        /// Indicates device name.
        /// </summary>
        public string DeviceName => _service.DeviceName;

        /// <summary>
        /// Indicates if the device user has enabled motion feature.
        /// </summary>
        public bool MotionEnabled => _service.MotionEnabled;

        /// <summary>
        /// Indicates if the motion service is activated.
        /// </summary>
        public bool MotionActivationEnabled => _service.MotionActivationEnabled;

        /// <summary>
        /// Event invoked when locales settings have changed.
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
        /// Default class constructor.
        /// </summary>
        public Settings()
        {
            _service = DependencyService.Get<ISettings>();

            _service.LocaleChanged += (s, e) => { LocaleChanged?.Invoke(s, e); };
            _service.UserSettingsChanged += (s, e) => { UserSettingsChanged?.Invoke(s, e); };
            _service.FontChanged += (s, e) => { FontChanged?.Invoke(s, e); };
            _service.SoundChanged += (s, e) => { SoundChanged?.Invoke(s, e); };
            _service.OtherChanged += (s, e) => { OtherChanged?.Invoke(s, e); };

            _service.StartListening();
        }

        #endregion
    }
}