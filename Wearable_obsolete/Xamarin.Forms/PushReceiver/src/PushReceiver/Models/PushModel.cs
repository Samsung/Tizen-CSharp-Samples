
//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Threading.Tasks;
using Tizen.Messaging.Push;
using Tizen;

namespace PushReceiver.Models
{
    public class PushModel
    {
        internal static string Tag = "PushReceiver";

        /// <summary>
        /// NotificationReceivedListener event.
        /// Notifies about received notification.
        /// </summary>
        public event EventHandler<NotificationReceivedEventArgs> NotificationReceived;

        /// <summary>
        /// RegistrationStateChangedListener event.
        /// Notifies about change in registration state.
        /// </summary>
        public event EventHandler<RegistrationStateChangedEventArgs> RegistrationStateChanged;

        /// <summary>
        /// PushModel class constructor.
        /// </summary>
        public PushModel()
        {
            // Use your push app id received from Tizen push team
            string AppId = "Your Push App ID";
            try
            {
                PushClient.StateChanged += PushModel_StateChanged;
                PushClient.NotificationReceived += PushModel_NotificationReceived;

                /*
                 * Connect to local push service.
                 * 
                 * It is not necessary to disconnect manually from the push server as 
                 * the connection is closed automatically when apllication terminates.
                 */
                PushClient.PushServiceConnect(AppId);

            }
            catch (Exception e)
            {
                // Exception handling
                Log.Debug(Tag, $"Caught Exception: {e}");
            }
        }

        /// <summary>
        /// Called when notification is received.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PushModel_NotificationReceived(object sender, PushMessageEventArgs e)
        {
            Log.Debug(Tag, "================== Notification Received ==================");
            Log.Debug(Tag, $"AppData: [{e.AppData}]");
            Log.Debug(Tag, $"Message: [{e.Message}]");
            Log.Debug(Tag, $"ReceivedAt: [{e.ReceivedAt}]");
            Log.Debug(Tag, $"Sender: [{e.Sender}]");
            Log.Debug(Tag, $"SessionInfo: [{e.SessionInfo}]");
            Log.Debug(Tag, $"RequestId: [{e.RequestId}]");
            Log.Debug(Tag, $"Type: [{e.Type}]");
            Log.Debug(Tag, "===========================================================");

            NotificationReceived?.Invoke(this, new NotificationReceivedEventArgs
            {
                AppData = e.AppData,
                Message = e.Message,
                ReceivedAt = e.ReceivedAt,
                Sender = e.Sender,
                SessionInfo = e.SessionInfo,
                RequestId = e.RequestId,
                Type = e.Type,
            });
        }

        /// <summary>
        /// Called when registration state changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PushModel_StateChanged(object sender, PushConnectionStateEventArgs e)
        {
            Log.Debug(Tag, $"Push State: [{e.State}], Error String: [{e.Error}]");

            State newState = State.Unregistered;

            if (e.State == PushConnectionStateEventArgs.PushState.Registered)
            {
                newState = State.Registered;

                // Get the registration id
                string id = PushClient.GetRegistrationId();
                Log.Debug(Tag, $"RegId: [{id}]");

                // If the application successfully connects with the push service,
                // it must request the unread notification messages
                // which are sent during the disconnected state
                // handlerNotification method is called if there are unread notifications
                PushClient.GetUnreadNotifications();
            }
            else if (e.State == PushConnectionStateEventArgs.PushState.Unregistered)
            {
                newState = State.Unregistered;

                Log.Debug(Tag, "Call PushServiceRegister()");

                // Send a registration request to the push service
                Task<ServerResponse> registerTask = PushClient.PushServerRegister();
                registerTask.GetAwaiter().OnCompleted(() =>
                {
                    // You will get result for register API
                    ServerResponse res = registerTask.Result;
                    Log.Debug(Tag, $"ServerResult: [{res.ServerResult}], ServerMessage: [{res.ServerMessage}]");
                });
            }

            RegistrationStateChanged?.Invoke(this, new RegistrationStateChangedEventArgs
            {
                state = newState
            });
        }

        public enum State { Registered, Unregistered }
    }

    public class NotificationReceivedEventArgs
    {
        public string AppData { get; set; }
        public string Message { get; set; }
        public DateTime ReceivedAt { get; set; }
        public string Sender { get; set; }
        public string SessionInfo { get; set; }
        public string RequestId { get; set; }
        public int Type { get; set; }
    }

    public class RegistrationStateChangedEventArgs
    {
        public PushModel.State state;
    }
}
