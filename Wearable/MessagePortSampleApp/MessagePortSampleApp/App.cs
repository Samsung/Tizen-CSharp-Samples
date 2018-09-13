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
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Applications.Messages;
using Tizen.Applications;

namespace MessagePortSampleApp
{
    /// <summary>
    /// MessagePort sample main class.
    /// This class creates an UI app to send a message port message to service application (ServiceApp).
    /// Note that to send a message port message successfully, you need to install and run the ServiceApp 
    /// sample app in the wearable sample app folder.
    /// </summary>
    public class App : Xamarin.Forms.Application
    {
        string uiAppPortName = "uiAppPort";
        string serviceAppPortName = "serviceAppPort";
        public App()
        {
            try
            {
                MessagePort messagePort = null;
                messagePort = new MessagePort(uiAppPortName, true);
                // You need to first call Listen even you need to send a message.
                messagePort.Listen();
                // Register MessageReceived event callback
                messagePort.MessageReceived += (s, e) =>
                {
                    Console.WriteLine("UI application received a message");
                    if (e.Message.Contains("greetingReturn"))
                    {
                        Toast.DisplayText("Received:" + e.Message.GetItem("greetingReturn") + ", Array: " + BitConverter.ToInt32((byte[])e.Message.GetItem("intByteArray"), 0));
                    }
                };

                // The root page of your application
                MainPage = new CirclePage
                {
                    Content = new StackLayout
                    {
                        VerticalOptions = LayoutOptions.Center,
                        Children =
                        {
                            new Label
                            {
                                HorizontalTextAlignment = TextAlignment.Center,
                                Text = "Talk over message port APIs"
                            },
                            new Button
                            {
                                Text = "Send a message",
                                Command = new Command(() =>
                                {
                                    Bundle bundleToSend = new Bundle();
                                    bundleToSend.AddItem("greeting","hello");
                                    bundleToSend.AddItem("intByteArray", BitConverter.GetBytes(1024 * 1024));

                                    try
                                    {
                                       // Need to call Listen() before calling Send
                                       messagePort.Send(bundleToSend, "org.tizen.example.ServiceApp", serviceAppPortName, false); 
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Exception: " + e.Message + ", " + e);
                                        Toast.DisplayText("An exception occurs!!   " + e.Message + ",   " + e, 10000);
                                    }
                                })
                            }
                        }
                    }
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        /// <summary>
        /// Called when the application starts.
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Called when the application enters the sleeping state.
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Called when the application resumes from a sleeping state.
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
