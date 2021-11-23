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

using Xamarin.Forms;

namespace NotificationManager.Models
{
    /// <summary>
    /// Notification class.
    /// Stores information about a notification.
    /// </summary>
    public class Notification
    {
        #region properties

        /// <summary>
        /// Title of the notification.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Content of the notification.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Notification's background image.
        /// </summary>
        public string BackgroundPath { get; set; }

        /// <summary>
        /// Notification's icon image.
        /// </summary>
        public string IconPath { get; set; }

        /// <summary>
        /// Notification's sound.
        /// </summary>
        public string SoundPath { get; set; }

        /// <summary>
        /// Notification's thumbnail image.
        /// </summary>
        public string ThumbnailPath { get; set; }

        /// <summary>
        /// Notification's 'Vibrate' option.
        /// </summary>
        public bool Vibration { get; set; }

        /// <summary>
        /// Notification's 'Sound' option.
        /// </summary>
        public bool Sound { get; set; }

        /// <summary>
        /// Notification's 'Led' option.
        /// </summary>
        public bool Led { get; set; }

        /// <summary>
        /// Duration of the notification's 'Led On' option.
        /// </summary>
        public int LedOnMs { get; set; }

        /// <summary>
        /// Duration of the notification's 'Led Off' option.
        /// </summary>
        public int LedOffMs { get; set; }

        /// <summary>
        /// Color of the notification's Led.
        /// </summary>
        public Color LedColor { get; set; }

        /// <summary>
        /// Current progress of ongoing notification.
        /// </summary>
        public int CurrentProgress { get; set; }

        /// <summary>
        /// Max progress of ongoing notification.
        /// </summary>
        public int MaxProgress { get; set; }

        #endregion
    }
}