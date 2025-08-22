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
using System.Linq;
using System.Text;

using Xamarin.Forms;
using PushReceiver.Views;
using PushReceiver.Models;
using PushReceiver.Utils;

namespace PushReceiver
{
    public class NotificationReceivedEventArgs
    {
        public string appData;
        public string message;
        public DateTime receiveTime;
        public string sender;
        public string sessionInfo;
        public string requestId;
        public int type;
    }

    public class RegistrationStateChangedListener
    {
        public int state;
    }

    /// <summary>
    /// Push Receiver Application Class
    /// </summary>
    public partial class App : Application, IPlatformEvent
    {
        private static IPushAPIs pushAPIs;

        public static event EventHandler<NotificationReceivedEventArgs> NotificationReceivedListener;
        public static event EventHandler<RegistrationStateChangedListener> RegistrationStateChangedListener;

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            pushAPIs = DependencyService.Get<IPushAPIs>();
        }

        ~App()
        {
            int ret = -1;

            /* Connect to the push service when the application is launched */
            ret = pushAPIs.PushDisconnect();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Console.WriteLine("OnStart");

            int ret = -1;
            ret = pushAPIs.PushConnect();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        /// <summary>
        /// A method will be called when push notification is received.
        /// </summary>
        /// <param name="_appData">App data loaded on the notification</param>
        /// <param name="_message">Notification message</param>
        /// <param name="_receiveTime">Time when the noti is generated</param>
        /// <param name="_sender">Optional sender information</param>
        /// <param name="_sessionInfo">Optional session information</param>
        /// <param name="_requestId">Optional request ID</param>
        /// <param name="_type">Optional type/param>
        /// <returns></returns>
        public int OnNotificationReceived(string _appData, string _message, DateTime _receiveTime, string _sender, string _sessionInfo, string _requestId, int _type)
        {
            NotificationReceivedListener?.Invoke(this, new NotificationReceivedEventArgs
            {
                appData = _appData,
                message = _message,
                receiveTime = _receiveTime,
                sender = _sender,
                sessionInfo = _sessionInfo,
                requestId = _requestId,
                type = _type
            });
            return 0;
        }

        /// <summary>
        /// A method will be called when registration state is changed.
        /// </summary>
        /// <param name="_state">rgistration state</param>
        public void OnStateChanged(int _state)
        {
            RegistrationStateChangedListener?.Invoke(this, new RegistrationStateChangedListener
            {
                state = _state,
            });
        }
    }
}
