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

        public int OnNotificationReceived(string _appData, string _message, DateTime _receiveTime, string _sender, string _sessionInfo, string _requestId, int _type)
        {
            NotificationReceivedListener?.Invoke(this, new NotificationReceivedEventArgs {
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

        public int OnStateChanged(int _state)
        {
            RegistrationStateChangedListener?.Invoke(this, new RegistrationStateChangedListener {
                state = _state,
            });
            return 0;
        }
    }
}
