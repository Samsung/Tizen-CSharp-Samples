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
using System.Threading;
using Tizen.Applications;
using Tizen.Applications.Notifications;

namespace NotificationApp
{
    class ProgressNotification : INotificationOperation
    {
        private String sharedPath = Application.Current.DirectoryInfo.SharedResource;

        public void Operate()
        {
            Notification notification = new Notification
            {
                Title = "Normal Notification",
                Content = "Testing Progress Notification",
                Icon = sharedPath + "Notification.Tizen.png",
                IsTimeStampVisible = true,
            };

            notification.Progress = new Notification.ProgressType(ProgressCategory.Percent, 0, 100);

            try
            {
                NotificationManager.Post(notification);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }

            Thread thread = new Thread(new ParameterizedThreadStart(UpdateProgress));
            thread.IsBackground = true;
            thread.Start(notification);
        }

        private void UpdateProgress(Object obj)
        {
            Notification notification = (Notification)obj;

            for (double current = 1.0; current <= 100.0; current += 1.0)
            {
                notification.Progress.ProgressCurrent = current;
                NotificationManager.Update(notification);
                Thread.Sleep(500);
            }
        }
    }
}
