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
    /// Class that is passed with SoundChanged event.
    /// </summary>
    public class SoundChangedEventArgs : EventArgs
    {
        #region properties

        /// <summary>
        /// Indicates if the screen lock sound is enabled.
        /// </summary>
        public bool SoundLockEnabled { get; set; }

        /// <summary>
        /// Indicates if the device is in the silent mode.
        /// </summary>
        public bool SilentModeEnabled { get; set; }

        /// <summary>
        /// Indicates if the screen touch sound is enabled.
        /// </summary>
        public bool SoundTouchEnabled { get; set; }

        /// <summary>
        /// Indicates the file path of the current notification tone set by the user.
        /// </summary>
        public string SoundNotification { get; set; }

        /// <summary>
        /// Indicates the time period for notification repetitions.
        /// </summary>
        public int SoundNotificationRepetitionPeriod { get; set; }

        #endregion
    }
}