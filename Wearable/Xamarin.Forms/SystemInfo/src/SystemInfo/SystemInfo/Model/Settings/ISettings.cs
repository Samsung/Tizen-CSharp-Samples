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
    /// Interface that contains all necessary methods to get information about system settings.
    /// </summary>
    public interface ISettings
    {
        #region properties

        /// <summary>
        /// Indicates the current country setting.
        /// </summary>
        string LocaleCountry { get; }

        /// <summary>
        /// Indicates the current language setting.
        /// </summary>
        string LocaleLanguage { get; }

        /// <summary>
        /// Indicates if the 24-hour or 12-hour clock is used.
        /// </summary>
        string LocaleTimeFormat { get; }

        /// <summary>
        /// Indicates the current time zone.
        /// </summary>
        string LocaleTimeZone { get; }

        /// <summary>
        /// The file path of the current ringtone.
        /// </summary>
        string IncomingCallRingtone { get; }

        /// <summary>
        /// The file path of the current email alert ringtone.
        /// </summary>
        string EmailAlertRingtone { get; }

        /// <summary>
        /// The file path of the current home screen wallpaper.
        /// </summary>
        string WallpaperHomeScreen { get; }

        /// <summary>
        /// The file path of the current lock screen wallpaper.
        /// </summary>
        string WallpaperLockScreen { get; }

        /// <summary>
        /// The current system font size.
        /// </summary>
        FontSize FontSize { get; }

        /// <summary>
        /// The current system font type.
        /// </summary>
        string FontType { get; }

        /// <summary>
        /// The current system default font type.
        /// </summary>
        string DefaultFontType { get; }

        /// <summary>
        /// Indicates if the screen lock sound is enabled.
        /// </summary>
        bool SoundLockEnabled { get; }

        /// <summary>
        /// Indicates if the device is in the silent mode.
        /// </summary>
        bool SilentModeEnabled { get; }

        /// <summary>
        /// Indicates if the screen touch sound is enabled.
        /// </summary>
        bool SoundTouchEnabled { get; }

        /// <summary>
        /// Indicates the file path of the current notification tone set by the user.
        /// </summary>
        string SoundNotification { get; }

        /// <summary>
        /// Indicates the time period for notification repetitions.
        /// </summary>
        int SoundNotificationRepetitionPeriod { get; }

        /// <summary>
        /// Indicates device name.
        /// </summary>
        string DeviceName { get; }

        /// <summary>
        /// Indicates if the device user has enabled motion feature.
        /// </summary>
        bool MotionEnabled { get; }

        /// <summary>
        /// Indicates if the motion service is activated.
        /// </summary>
        bool MotionActivationEnabled { get; }

        /// <summary>
        /// Event invoked when locales settings have changed.
        /// </summary>
        event EventHandler<EventArgs> LocaleChanged;

        /// <summary>
        /// Event invoked when user settings have changed.
        /// </summary>
        event EventHandler<EventArgs> UserSettingsChanged;

        /// <summary>
        /// Event invoked when fonts settings have changed.
        /// </summary>
        event EventHandler<EventArgs> FontChanged;

        /// <summary>
        /// Event invoked when sounds settings have changed.
        /// </summary>
        event EventHandler<EventArgs> SoundChanged;

        /// <summary>
        /// Event invoked when other settings have changed.
        /// </summary>
        event EventHandler<EventArgs> OtherChanged;

        #endregion

        #region methods

        /// <summary>
        /// Starts observing system settings for changes.
        /// </summary>
        /// <remarks>
        /// System settings events will be never invoked before calling this method.
        /// </remarks>
        void StartListening();

        #endregion
    }
}