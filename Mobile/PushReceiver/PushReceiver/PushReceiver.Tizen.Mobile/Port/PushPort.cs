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
using System.Threading.Tasks;
using Tizen.System;
using Tizen.Messaging.Push;
using PushReceiver.Models;
using PushReceiver.Utils;
using Tizen;
using Tizen.Applications;


namespace PushReceiver.Tizen.Port
{
    public class PushPort : IPushAPIs
    {
        internal static string Tag = "PushReceiver";
        static IPlatformEvent pEvent;

        public PushPort()
        {
        }

        public int PushConnect()
        {
            // Use your push app id received from Tizen push team
            string AppId = "Your Push App ID";
            try
            {
                // Connect to local push service
                PushClient.PushServiceConnect(AppId);

                // When the connection state is changed, this handler will be called (ex, registered -> unregistered state)
                EventHandler<PushConnectionStateEventArgs> handler = (s, e) =>
                {
                    Console.WriteLine("Push State: [" + e.State + "], Error String: [" + e.Error + "]");

                    if (e.State == PushConnectionStateEventArgs.PushState.Registered)
                    {
                        // 0 is for registered state
                        pEvent.OnStateChanged(0);

                        // Get the registration id
                        string id = PushClient.GetRegistrationId();
                        Console.WriteLine("RegId: [" + id + "]");

                        // send registration id to your application server if necessary

                        // If the connection with the push service succeeds,
                        // the application must request the unread notification messages
                        // which are sent during the disconnected state
                        // handlerNotification method is called if there are unread notifications
                        PushClient.GetUnreadNotifications();
                    }
                    else if (e.State == PushConnectionStateEventArgs.PushState.Unregistered)
                    {
                        // 1 is for registered state
                        pEvent.OnStateChanged(1);

                        Console.WriteLine("Call PushServiceRegister()");

                        // Send a registration request to the push service
                        Task<ServerResponse> tr = PushClient.PushServerRegister();
                        tr.GetAwaiter().OnCompleted(() => 
                        {
                            // You will get result for register API
                            ServerResponse res = tr.Result;
                            Console.WriteLine("ServerResult: [" + res.ServerResult + "], ServerMessage: [" + res.ServerMessage + "]");
                        });
                    }
                };

                // When push notification is received, this handler will be called
                EventHandler<PushMessageEventArgs> handlerNotification = (object sender, PushMessageEventArgs e) =>
                {
                    Console.WriteLine("========================== Notification Received ==========================");
                    // App data loaded on the notification
                    Console.WriteLine("AppData: [" + e.AppData + "]");
                    // Notification message
                    Console.WriteLine("Message: [" + e.Message + "]");
                    // Time when the noti is generated/
                    Console.WriteLine("ReceivedAt: [" + e.ReceivedAt + "]");
                    // Optional sender information
                    Console.WriteLine("Sender: [" + e.Sender + "]");
                    // Optional session information
                    Console.WriteLine("SessionInfo: [" + e.SessionInfo + "]");
                    // Optional request ID
                    Console.WriteLine("RequestId: [" + e.RequestId + "]");
                    // Optional type
                    Console.WriteLine("Type: [" + e.Type + "]");
                    Console.WriteLine("===========================================================================");

                    // send notification received event to common interface
                    pEvent.OnNotificationReceived(e.AppData, e.Message, e.ReceivedAt, e.Sender, e.SessionInfo, e.RequestId, e.Type);
                };
                PushClient.StateChanged += handler;
                PushClient.NotificationReceived += handlerNotification;

            }
            catch (Exception e)
            {
                // Exception handling
                Console.WriteLine("Caught Exception: " + e.ToString());
            }

            return 0;
        }

        /// <summary>
        /// Used to disconnect from Push Service
        /// </summary>
        /// <returns>0 for success, other values for failure</returns>
        public int PushDisconnect()
        {
            Console.WriteLine("Push Disconnect");
            try
            {
                PushClient.PushServiceDisconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught Exception: " + e.ToString());
                return -1;
            }

            return 0;

        }

        /// <summary>
        /// Used to get registration id
        /// </summary>
        /// <returns>registration id</returns>
        public string PushGetRegistrationId()
        {
            return PushClient.GetRegistrationId(); ;
        }

        /// <summary>
        /// Used to register platform event
        /// </summary>
        /// <param name="_pEvent">platform event</param>
        public static void Register(IPlatformEvent _pEvent)
        {
            pEvent = _pEvent;
        }
    }
}
