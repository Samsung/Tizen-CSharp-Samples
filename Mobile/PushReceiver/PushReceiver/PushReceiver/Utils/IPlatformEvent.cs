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
        int OnNotificationReceived(string appData, string message, DateTime receiveTime, string sender, string sessionInfo, string requestId, int type);

        /// <summary>
        /// A method will be called when registration state is changed.
        /// </summary>
        int OnStateChanged(int state);
    }
}
