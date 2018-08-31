
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
using System.Collections.ObjectModel;
using Xamarin.Forms.Xaml;
using PushReceiver.Models;
using Tizen.Wearable.CircularUI.Forms;
using Tizen;

namespace PushReceiver.Views
{
    /// <summary>
    /// PushReceiver app main UI page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CirclePage
    {
        private PushModel pushModel;
        ObservableCollection<Notification> Notifications { get; set; }

        public MainPage()
        {
            pushModel = new PushModel();
            Notifications = new ObservableCollection<Notification>();

            InitializeComponent();

            // Data binding to UI
            list.ItemsSource = Notifications;

            pushModel.NotificationReceived += MainPage_NotificationReceived;
            pushModel.RegistrationStateChanged += MainPage_RegistrationStateChanged;
        }

        /// <summary>
        /// Called when registration state changes.
        /// </summary>
        /// <param name="sender"> Sender </param>
        /// <param name="e"> Event arguments </param>
        private void MainPage_RegistrationStateChanged(object sender, RegistrationStateChangedEventArgs e)
        {
            Log.Debug("PushReceiver", $"State Changed : [{e.state}]");

            // Add your code handling registration state change here
        }

        /// <summary>
        /// Called when notification is received.
        /// Adds received notification to Notifications collection.
        /// </summary>
        /// <param name="sender"> Sender </param>
        /// <param name="e"> Event arguments </param>
        private void MainPage_NotificationReceived(object sender, NotificationReceivedEventArgs e)
        {
            // parsing action and alertMessage from received message
            string[] val = e.Message.Split('&');
            string msg1 = "";
            string msg2 = "";

            foreach (string temp in val)
            {
                if (temp.Contains("action"))
                {
                    msg1 = temp.Substring(temp.IndexOf("=") + 1);
                }

                if (temp.Contains("alertMessage"))
                {
                    msg2 = temp.Substring(temp.IndexOf("=") + 1);
                }
            }

            var notification = new Notification($"Type: {msg1} / Message: {msg2}", e.ReceivedAt.ToString());

            Notifications.Add(notification);
        }

        /// <summary>
        /// This method will be called when the clear button is pressed.
        /// Removes all received notifications.
        /// </summary>
        /// <param name="sender"> Sender </param>
        /// <param name="args"> Event arguments </param>
        async void OnClearClicked(object sender, EventArgs args)
        {
            Notifications.Clear();
            await DisplayAlert("Push Receiver", "Removed all the notification list", "OK");
        }
    }
}
