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
using SystemInfo.Model.Settings;
using SystemInfo.Utils;
using SystemInfo.ViewModel.List;

namespace SystemInfo.ViewModel
{
    /// <summary>
    /// ViewModel class for settings page.
    /// </summary>
    public class SetingsViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Properties of system locale settings.
        /// </summary>
        public static readonly string[] Locale =
        {
            "Country",
            "Language",
            "Time Format",
            "Time zone"
        };

        /// <summary>
        /// Properties of user settings.
        /// </summary>
        public static readonly string[] UserSettings =
        {
            "Incoming call ringtone",
            "New email ringtone",
            "Wallpaper on home screen",
            "Wallpaper on lock screen"
        };

        /// <summary>
        /// Properties of system font settings.
        /// </summary>
        public static readonly string[] Font =
        {
            "Font Size",
            "Default Font Type",
            "Font Type"
        };

        /// <summary>
        /// Properties of system sound settings.
        /// </summary>
        public static readonly string[] Sound =
        {
            "Sound Lock Enabled",
            "Silent Mode Enabled",
            "Sound Touch Enabled",
            "Notification",
            "Notification Repetition Period"
        };

        /// <summary>
        /// Properties of other settings.
        /// </summary>
        public static readonly string[] Other =
        {
            "DeviceName",
            "MotionEnabled",
            "MotionActivationEnabled"
        };

        /// <summary>
        /// Data source.
        /// </summary>
        private readonly Settings _settings;

        /// <summary>
        /// Local storage of collection of system settings.
        /// </summary>
        private GroupViewModel _groupList;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets collection of system settings.
        /// </summary>
        public GroupViewModel GroupList
        {
            get => _groupList;
            set => SetProperty(ref _groupList, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public SetingsViewModel()
        {
            _settings = new Settings();

            string[] localeInitialValues =
            {
                _settings.LocaleCountry,
                _settings.LocaleLanguage,
                _settings.LocaleTimeFormat,
                _settings.LocaleTimeZone
            };

            string[] userSettingsInitialValues =
            {
                _settings.IncomingCallRingtone,
                _settings.EmailAlertRingtone,
                _settings.WallpaperHomeScreen,
                _settings.WallpaperLockScreen
            };

            string[] fontInitialValues =
            {
                _settings.FontSize.ToString(),
                _settings.DefaultFontType,
                _settings.FontType
            };

            string[] soundInitialValues =
            {
                _settings.SoundLockEnabled.ToString(),
                _settings.SilentModeEnabled.ToString(),
                _settings.SoundTouchEnabled.ToString(),
                _settings.SoundNotification,
                _settings.SoundNotificationRepetitionPeriod.ToString()
            };

            string[] otherInitialValues =
            {
                _settings.DeviceName,
                _settings.MotionEnabled.ToString(),
                _settings.MotionActivationEnabled.ToString()
            };

            _groupList = new GroupViewModel
            {
                ListUtils.CreateGroupedItemsList(Locale, nameof(Locale), localeInitialValues),
                ListUtils.CreateGroupedItemsList(UserSettings, nameof(UserSettings), userSettingsInitialValues),
                ListUtils.CreateGroupedItemsList(Font, nameof(Font), fontInitialValues),
                ListUtils.CreateGroupedItemsList(Sound, nameof(Sound), soundInitialValues),
                ListUtils.CreateGroupedItemsList(Other, nameof(Other), otherInitialValues)
            };

            _settings.LocaleChanged += OnLocaleChanged;

            _settings.UserSettingsChanged += OnUserSettingsChanged;

            _settings.FontChanged += OnFontChanged;

            _settings.SoundChanged += OnSoundChanged;

            _settings.OtherChanged += OnOtherChanged;
        }

        /// <summary>
        /// Updates other settings.
        /// </summary>
        /// <param name="s">Object that sent event.</param>
        /// <param name="e">Event's argument.</param>
        private void OnOtherChanged(object s, EventArgs e)
        {
            const string otherName = nameof(Other);

            _groupList[otherName]["DeviceName"] = _settings.DeviceName;
            _groupList[otherName]["MotionEnabled"] = _settings.MotionEnabled.ToString();
            _groupList[otherName]["MotionActivationEnabled"] = _settings.MotionActivationEnabled.ToString();
        }

        /// <summary>
        /// Updates sound settings.
        /// </summary>
        /// <param name="s">Object that sent event.</param>
        /// <param name="e">Event's argument.</param>
        private void OnSoundChanged(object s, EventArgs e)
        {
            const string soundName = nameof(Sound);

            _groupList[soundName]["Sound Lock Enabled"] = _settings.SoundLockEnabled.ToString();
            _groupList[soundName]["Silent Mode Enabled"] = _settings.SilentModeEnabled.ToString();
            _groupList[soundName]["Sound Touch Enabled"] = _settings.SoundTouchEnabled.ToString();
            _groupList[soundName]["Notification"] = _settings.SoundNotification;
            _groupList[soundName]["Notification Repetition Period"] =
                _settings.SoundNotificationRepetitionPeriod.ToString();
        }

        /// <summary>
        /// Updates font settings.
        /// </summary>
        /// <param name="s">Object that sent event.</param>
        /// <param name="e">Event's argument.</param>
        private void OnFontChanged(object s, EventArgs e)
        {
            const string fontName = nameof(Font);

            _groupList[fontName]["Font Size"] = _settings.FontSize.ToString();
            _groupList[fontName]["Font Type"] = _settings.FontType;
        }

        /// <summary>
        /// Updates user settings.
        /// </summary>
        /// <param name="s">Object that sent event.</param>
        /// <param name="e">Event's argument.</param>
        private void OnUserSettingsChanged(object s, EventArgs e)
        {
            const string userSettingsName = nameof(UserSettings);

            _groupList[userSettingsName]["Incoming call ringtone"] = _settings.IncomingCallRingtone;
            _groupList[userSettingsName]["New email ringtone"] = _settings.EmailAlertRingtone;
            _groupList[userSettingsName]["Wallpaper on home screen"] = _settings.WallpaperHomeScreen;
            _groupList[userSettingsName]["Wallpaper on lock screen"] = _settings.WallpaperLockScreen;
        }

        /// <summary>
        /// Updates locale settings.
        /// </summary>
        /// <param name="s">Object that sent event.</param>
        /// <param name="e">Event's argument.</param>
        private void OnLocaleChanged(object s, EventArgs e)
        {
            const string localeName = nameof(Locale);

            _groupList[localeName]["Country"] = _settings.LocaleCountry;
            _groupList[localeName]["Language"] = _settings.LocaleLanguage;
            _groupList[localeName]["Time Format"] = _settings.LocaleTimeFormat;
            _groupList[localeName]["Time zone"] = _settings.LocaleTimeZone;
        }

        #endregion
    }
}