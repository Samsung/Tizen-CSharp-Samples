using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PushReceiver.Models;
using PushReceiver.Utils;

namespace PushReceiver.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private static IPushAPIs pushAPIs;
        ObservableCollection<Notification> notification;

        public MainPage()
        {
            InitializeComponent();
            pushAPIs = DependencyService.Get<IPushAPIs>();
            notification = new ObservableCollection<Notification>();
            MyListView.ItemsSource = notification;

            App.NotificationReceivedListener += (s, e) =>
            {
                if (e is NotificationReceivedEventArgs)
                {
                    string[] val = e.message.Split('&');
                    string msg1 = "";
                    string msg2 = "";

                    foreach (string temp in val)
                    {
                        if (temp.Contains("action"))
                            msg1 = temp.Substring(temp.IndexOf("=") + 1);
                        if (temp.Contains("alertMessage"))
                            msg2 = temp.Substring(temp.IndexOf("=") + 1);
                    }

                    Console.WriteLine("Notification Reveived : " + e.message);

                    // Type : ALERT / Message : 1st Push
                    // 20171019 11:20:22 PM
                    notification.Add(new Notification("Type : " + msg1 + " / Message : " + msg2, e.receiveTime.ToString()));
                }
            };
            App.RegistrationStateChangedListener += (s, e) =>
            {
                if (e is RegistrationStateChangedListener)
                {
                    Console.WriteLine("State Changed : [" + e.state + "]");
                }
            };
        }

        async void OnClearClicked(object sender, EventArgs args)
        {
            Console.WriteLine("Clear clicked");
            notification.Clear();
            await DisplayAlert("Push Receiver", "Removed all the notification list", "OK");
        }
    }
}
