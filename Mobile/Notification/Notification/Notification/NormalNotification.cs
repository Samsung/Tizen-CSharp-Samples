/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Tizen.Applications;
using Tizen.Applications.Notifications;

namespace NotificationApp
{
    /// <summary>
    /// Class for NormalNotification testing
    /// </summary>
    class NormalNotification : INotificationOperation
    {
        // Set shared resource path
        private String sharedPath = Application.Current.DirectoryInfo.SharedResource;

        /// <summary>
        /// Operate normal notification
        /// </summary>
        public void Operate()
        {
            Notification notification = new Notification
            {
                // Set notification title
                Title = "Normal Notification",
                // Set notification content
                Content = "Testing Normal Notification",
                // Set notification icon
                Icon = sharedPath + "Notification.Tizen.png",
                // Set notification sub icon path
                SubIcon = sharedPath + "Notification.Tizen.png",
                // Set a value indicating TimeStamp of notification is visible
                IsTimeStampVisible = true,
                // Set notification tag
                Tag = "my normal notification",
                // Set action which is invoked when the notification is clicked
                Action = new AppControl
                {
                    // Set Application ID
                    ApplicationId = Application.Current.ApplicationInfo.ApplicationId
                }
            };

            // Set accessory for hardware feedback
            notification.Accessory = new Notification.AccessorySet
            {
                // Set vibration
                CanVibrate = true,
                // Set LED option
                LedOption = AccessoryOption.Custom,
                // Set the time the LED in on
                LedOnMillisecond = 100,
                // Set the time the LED in off
                LedOffMillisecond = 50,
            };

            try
            {
                // Post notification
                NotificationManager.Post(notification);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }
    }
}
