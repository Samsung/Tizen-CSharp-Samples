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

using NotificationManager.Models;
using NotificationManager.TizenMobile.Service;
using TizenNotifications = Tizen.Applications.Notifications;
using TizenCommon = Tizen.Common;
using Tizen.Applications;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationManagerService))]

namespace NotificationManager.TizenMobile.Service
{
    /// <summary>
    /// NotificationManagerService class.
    /// Provides methods responsible for creating and deleting notifications.
    /// </summary>
    public class NotificationManagerService : INotificationManager
    {
        #region methods

        /// <summary>
        /// Creates a normal notification.
        /// </summary>
        /// <param name="notification">The object with detailed information about the notification.</param>
        /// <returns>The newly created Notification object.</returns>
        private TizenNotifications.Notification PrepareNotification(Notification notification)
        {
            var tizenNotification = new TizenNotifications.Notification
            {
                Title = notification.Title,
                Content = notification.Content,
            };

            if (!string.IsNullOrEmpty(notification.IconPath))
            {
                tizenNotification.Icon = notification.IconPath;
            }

            if (!string.IsNullOrEmpty(notification.ThumbnailPath))
            {
                var style = new TizenNotifications.Notification.LockStyle
                {
                    ThumbnailPath = notification.ThumbnailPath
                };

                if (!string.IsNullOrEmpty(notification.IconPath))
                {
                    style.IconPath = notification.IconPath;
                }

                tizenNotification.AddStyle(style);
            }

            var tizenAccessory = new TizenNotifications.Notification.AccessorySet
            {
                CanVibrate = notification.Vibration,
            };

            if (notification.Sound && !string.IsNullOrEmpty(notification.SoundPath))
            {
                tizenAccessory.SoundOption = TizenNotifications.AccessoryOption.Custom;
                tizenAccessory.SoundPath = notification.SoundPath;
            }
            else if (notification.Sound)
            {
                tizenAccessory.SoundOption = TizenNotifications.AccessoryOption.On;
            }
            else
            {
                tizenAccessory.SoundOption = TizenNotifications.AccessoryOption.Off;
            }

            if (notification.Led)
            {
                tizenAccessory.LedOption = TizenNotifications.AccessoryOption.Custom;
                tizenAccessory.LedOnMillisecond = notification.LedOnMs;
                tizenAccessory.LedOffMillisecond = notification.LedOffMs;
                tizenAccessory.LedColor = new TizenCommon.Color(
                (int)System.Math.Floor(notification.LedColor.R >= 1.0 ? 255 : notification.LedColor.R * 256.0),
                (int)System.Math.Floor(notification.LedColor.G >= 1.0 ? 255 : notification.LedColor.G * 256.0),
                (int)System.Math.Floor(notification.LedColor.B >= 1.0 ? 255 : notification.LedColor.B * 256.0),
                (int)System.Math.Floor(notification.LedColor.A >= 1.0 ? 255 : notification.LedColor.A * 256.0));
            }

            tizenNotification.Accessory = tizenAccessory;

            AppControl appControl = new AppControl()
            {
                ApplicationId = Application.Current.ApplicationInfo.ApplicationId
            };

            tizenNotification.Action = appControl;

            return tizenNotification;
        }

        /// <summary>
        /// Deletes all notifications.
        /// </summary>
        public void DeleteAll()
        {
            TizenNotifications.NotificationManager.DeleteAll();
        }

        /// <summary>
        /// Posts a normal notification.
        /// </summary>
        /// <param name="notification">The notification which is to be posted as a 'normal notification'.</param>
        public void PostNormal(Notification notification)
        {
            var tizenNotification = PrepareNotification(notification);
            TizenNotifications.NotificationManager.Post(tizenNotification);
        }

        /// <summary>
        /// Posts an ongoing notification.
        /// </summary>
        /// <param name="notification">The notification which is to be posted as an 'ongoing notification'.</param>
        public void PostOngoing(Notification notification)
        {
            var tizenNotification = PrepareNotification(notification);

            tizenNotification.Progress = new TizenNotifications.Notification.ProgressType(
                TizenNotifications.ProgressCategory.Percent,
                notification.CurrentProgress,
                notification.MaxProgress);

            TizenNotifications.NotificationManager.Post(tizenNotification);
        }

        #endregion
    }
}