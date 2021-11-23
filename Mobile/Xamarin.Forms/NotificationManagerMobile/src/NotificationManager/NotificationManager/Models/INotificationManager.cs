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

namespace NotificationManager.Models
{
    /// <summary>
    /// INotificationManager interface.
    /// </summary>
    public interface INotificationManager
    {
        #region methods

        /// <summary>
        /// Deletes all notifications.
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// Posts normal notification.
        /// </summary>
        /// <param name="notification">Notification which is to be posted as a 'normal notification'.</param>
        void PostNormal(Notification notification);

        /// <summary>
        /// Posts ongoing notification.
        /// </summary>
        /// <param name="notification">Notification which is to be posted as an 'ongoing notification'.</param>
        void PostOngoing(Notification notification);

        #endregion
    }
}