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
    /// Class that is passed with UserSettingsChanged event.
    /// </summary>
    public class UserSettingsChangedEventArgs : EventArgs
    {
        #region properties

        /// <summary>
        /// The file path of the current ringtone.
        /// </summary>
        public string IncomingCallRingtone { get; set; }

        /// <summary>
        /// The file path of the current email alert ringtone.
        /// </summary>
        public string EmailAlertRingtone { get; set; }

        /// <summary>
        /// The file path of the current home screen wallpaper.
        /// </summary>
        public string WallpaperHomeScreen { get; set; }

        /// <summary>
        /// The file path of the current lock screen wallpaper.
        /// </summary>
        public string WallpaperLockScreen { get; set; }

        #endregion
    }
}