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
    /// Class for ActiveNotification testing
    /// </summary>
    class ActiveNotification : INotificationOperation
    {
        // Set shared resource path
        private String sharedPath = Application.Current.DirectoryInfo.SharedResource;

        /// <summary>
        /// Operate active notification
        /// </summary>
        public void Operate()
        {
            Notification notification = new Notification
            {
                // Set notification title
                Title = "Active Notification",
                // Set notification content
                Content = "Testing Active Style Notification",
                // Set notification icon
                Icon = sharedPath + "Notification.Tizen.png",
                // Set invisible to the notification-tray
                IsVisible = false
            };

            // Create ActiveStyle
            Notification.ActiveStyle style = new Notification.ActiveStyle { IsAutoRemove = true };

            // Add buttons to style
            style.AddButtonAction(new Notification.ButtonAction
            {
                Index = ButtonIndex.First,
                Text = "Open my app",
                Action = new AppControl { ApplicationId = Application.Current.ApplicationInfo.ApplicationId }
            });

            // Add style to notification
            notification.AddStyle(style);

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
