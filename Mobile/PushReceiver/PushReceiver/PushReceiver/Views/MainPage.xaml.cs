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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PushReceiver.Models;
using PushReceiver.Utils;

namespace PushReceiver.Views
{
    /// <summary>
    /// PushReceiver app main UI page
    /// </summary>
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

            // Data binding to UI
            MyListView.ItemsSource = notification;

            // Process when notification is received
            App.NotificationReceivedListener += (s, e) =>
            {
                if (e is NotificationReceivedEventArgs)
                {
                    // parsing action and alertMessage from received message
                    string[] val = e.message.Split('&');
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

                    Console.WriteLine("Notification Received : " + e.message);

                    //Add your code here when push messages arrive

                    // Example UI : add notification to UI
                    // "Type : ALERT / Message : 1st Push"
                    // "20171019 11:20:22 PM"
                    notification.Add(new Notification("Type : " + msg1 + " / Message : " + msg2, e.receiveTime.ToString()));
                }
            };

            // Process when registration state is changed
            App.RegistrationStateChangedListener += (s, e) =>
            {
                if (e is RegistrationStateChangedListener)
                {
                    Console.WriteLine("State Changed : [" + e.state + "]");

                    // Add your code here when registration state is changed
                }
            };
        }

        /// <summary>
        /// This method will be called when the clear button is pressed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="args">args</param>
        async void OnClearClicked(object sender, EventArgs args)
        {
            // Remove all the UI list items
            notification.Clear();
            await DisplayAlert("Push Receiver", "Removed all the notification list", "OK");
        }
    }
}
