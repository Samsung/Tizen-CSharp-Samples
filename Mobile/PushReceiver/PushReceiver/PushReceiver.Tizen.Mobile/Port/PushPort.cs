// Copyright 2016 by Samsung Electronics, Inc.,
//
// This software is the confidential and proprietary information
// of Samsung Electronics, Inc. ("Confidential Information"). You
// shall not disclose such Confidential Information and shall use
// it only in accordance with the terms of the license agreement
// you entered into with Samsung.


using System;
using System.Collections.Generic;
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
            string AppId = "Your Push App ID";
            try
            {
                PushClient.PushServiceConnect(AppId);

                EventHandler<PushConnectionStateEventArgs> handler = (s, e) =>
                {
                    Console.WriteLine("Push State: [" + e.State + "], Error String: [" + e.Error + "]");

                    if (e.State == PushConnectionStateEventArgs.PushState.Registered)
                    {
                        pEvent.OnStateChanged(0);

                        string id = PushClient.GetRegistrationId();
                        Console.WriteLine("RegId: [" + id + "]");

                        PushClient.GetUnreadNotifications();
                    } else if (e.State == PushConnectionStateEventArgs.PushState.Unregistered)
                    {
                        pEvent.OnStateChanged(1);

                        Console.WriteLine("Call PushServiceRegister()");
                        Task<ServerResponse> tr = PushClient.PushServerRegister();
                        tr.GetAwaiter().OnCompleted(() => {
                            ServerResponse res = tr.Result;
                            Console.WriteLine("ServerResult: [" + res.ServerResult + "], ServerMessage: [" + res.ServerMessage + "]");
                        });
                    }
                };

                EventHandler<PushMessageEventArgs> handlerNotification = (object sender, PushMessageEventArgs e) =>
                {
                    Console.WriteLine("========================== Notification Received ==========================");
                    Console.WriteLine("AppData: [" + e.AppData + "]");
                    Console.WriteLine("Message: [" + e.Message + "]");
                    Console.WriteLine("ReceivedAt: [" + e.ReceivedAt + "]");
                    Console.WriteLine("Sender: [" + e.Sender + "]");
                    Console.WriteLine("SessionInfo: [" + e.SessionInfo + "]");
                    Console.WriteLine("RequestId: [" + e.RequestId + "]");
                    Console.WriteLine("Type: [" + e.Type + "]");
                    Console.WriteLine("===========================================================================");

                    pEvent.OnNotificationReceived(e.AppData, e.Message, e.ReceivedAt, e.Sender, e.SessionInfo, e.RequestId, e.Type);
                };
                PushClient.StateChanged += handler;
                PushClient.NotificationReceived += handlerNotification;

            }
            catch (Exception e)
            {
                Console.WriteLine("Caught Exception: " + e.ToString());
            }
            return 0;
        }

        public int PushDisconnect()
        {
            Console.WriteLine("Push Disconnect");
            PushClient.PushServiceDisconnect();
            return 0;
        }

        /*
        public int PushRegister()
        {
            PushClient.PushServerRegister();
            return 0;
        }*/

        public string PushGetRegistrationId()
        {
            return PushClient.GetRegistrationId(); ;
        }

        public static void Register(IPlatformEvent _pEvent)
        {
            pEvent = _pEvent;
        }
    }
}
