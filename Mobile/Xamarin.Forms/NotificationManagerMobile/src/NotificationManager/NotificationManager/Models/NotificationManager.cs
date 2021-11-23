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
    /// NotificationManager class.
    /// Provides methods responsible for creating and deleting notifications.
    /// </summary>
    public class NotificationManager
    {
        #region fields

        /// <summary>
        /// An instance of class that implements INotificationManager interface.
        /// </summary>
        private readonly INotificationManager _service;

        #endregion

        #region methods

        /// <summary>
        /// NotificationManager class constructor.
        /// </summary>
        public NotificationManager()
        {
            _service = DependencyService.Get<INotificationManager>();
        }

        /// <summary>
        /// Deletes all notifications.
        /// </summary>
        public void DeleteAll()
        {
            _service.DeleteAll();
        }

        /// <summary>
        /// Posts a normal notification.
        /// </summary>
        /// <param name="notification">Notification which is to be posted as a 'normal notification'.</param>
        public void PostNormal(Notification notification)
        {
            _service.PostNormal(notification);
        }

        /// <summary>
        /// Posts an ongoing notification.
        /// </summary>
        /// <param name="notification">Notification which is to be posted as an 'ongoing notification'.</param>
        public void PostOngoing(Notification notification)
        {
            _service.PostOngoing(notification);
        }

        #endregion
    }
}