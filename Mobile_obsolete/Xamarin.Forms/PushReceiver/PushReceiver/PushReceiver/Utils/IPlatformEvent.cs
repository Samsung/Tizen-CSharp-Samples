/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
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

using System;
using System.Collections.Generic;
using System.Text;
using PushReceiver.Models;

namespace PushReceiver.Utils
{
    public interface IPlatformEvent
    {
        /// <summary>
        /// A method will be called when push notification is received.
        /// </summary>
        /// <param name="appData">App data loaded on the notification</param>
        /// <param name="message">Notification message</param>
        /// <param name="receiveTime">Time when the noti is generated</param>
        /// <param name="sender">Optional sender information</param>
        /// <param name="sessionInfo">Optional session information</param>
        /// <param name="requestId">Optional request ID</param>
        /// <param name="type">Optional type/param>
        /// <returns></returns>
        int OnNotificationReceived(string appData, string message, DateTime receiveTime, string sender, string sessionInfo, string requestId, int type);

        /// <summary>
        /// A method will be called when registration state is changed.
        /// </summary>
        /// <param name="state">registration state</param>
        void OnStateChanged(int state);
    }
}
