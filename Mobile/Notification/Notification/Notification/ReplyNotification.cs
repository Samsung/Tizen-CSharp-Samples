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
using System.Collections.Generic;
using System.Text;
using Tizen.Applications;
using Tizen.Applications.Notifications;

namespace NotificationApp
{
    class ReplyNotification : INotificationOperation
    {
        private String sharedPath = Application.Current.DirectoryInfo.SharedResource;

        public void Operate()
        {
            Notification notification = new Notification
            {
                // Set notification title
                Title = "Reply Notification",
                // Set notification content
                Content = "Testing Reply Style Notification",
                // Set notification icon
                Icon = sharedPath + "Notification.Tizen.png",
                // Set invisible to the notification-tray
                IsVisible = false
            };

            // Create ActiveStyle
            Notification.ActiveStyle style = new Notification.ActiveStyle { IsAutoRemove = true };

            // Create Button
            Notification.ButtonAction firstBtn = new Notification.ButtonAction
            {
                Index = ButtonIndex.First,
                Text = "Reply",
                Action = new AppControl { ApplicationId = Application.Current.ApplicationInfo.ApplicationId }
            };

            // Add buttons to style
            style.AddButtonAction(firstBtn);

            // Create ReplyAction
            Notification.ReplyAction replyAction = new Notification.ReplyAction
            {
                // set button
                Button = firstBtn,
                // Set index matched button
                ParentIndex = ButtonIndex.First,
                // Set PlaceHolderText
                PlaceHolderText = "Please write your reply",
                // Set ReplyMax
                ReplyMax = 160
            };

            style.ReplyAction = replyAction;
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
