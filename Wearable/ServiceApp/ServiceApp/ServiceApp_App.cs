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
using Tizen.Applications;
using Tizen.Applications.Messages;

namespace ServiceApp
{
    /// <summary>
    /// ServiceApplication sample main class.
    /// This class creates a background service and be ready to accept message port messages.
    /// Note that to receive a message port message, run the MessagePortSampleApp sample app 
    /// in the wearable sample app folder after install this service app. 
    /// </summary>
    class App : ServiceApplication
    {
        string uiAppPortName = "uiAppPort";
        string serviceAppPortName = "serviceAppPort";
        MessagePort messagePort;

        protected override void OnCreate()
        {
            // Start this service.
            base.OnCreate();
            // Create a MessagePort object.
            messagePort = new MessagePort(serviceAppPortName, false);
            // Register an event handler for MessagePort
            messagePort.MessageReceived += (s, e) => {
                if (e.Message.Contains("greeting"))
                {
                    // Create a bundle to send with the message port message
                    Bundle bundleToSend = new Bundle();
                    // Add a string type bundle message
                    bundleToSend.AddItem("greetingReturn", e.Message.GetItem("greeting") + " world");
                    // Add a byte array type bundle message
                    bundleToSend.AddItem("intByteArray", (byte[])e.Message.GetItem("intByteArray"));

                    messagePort.Send(bundleToSend, "org.tizen.example.MessagePortSampleApp", uiAppPortName, true); // last argument should match type of receiver port(trusted or not).
                }
            };
            // Listen regiser this local message port so this must be called to Send
            messagePort.Listen();
        }

        protected override void OnAppControlReceived(AppControlReceivedEventArgs e)
        {
            base.OnAppControlReceived(e);
        }

        protected override void OnDeviceOrientationChanged(DeviceOrientationEventArgs e)
        {
            base.OnDeviceOrientationChanged(e);
        }

        protected override void OnLocaleChanged(LocaleChangedEventArgs e)
        {
            base.OnLocaleChanged(e);
        }

        protected override void OnLowBattery(LowBatteryEventArgs e)
        {
            base.OnLowBattery(e);
        }

        protected override void OnLowMemory(LowMemoryEventArgs e)
        {
            base.OnLowMemory(e);
        }

        protected override void OnRegionFormatChanged(RegionFormatChangedEventArgs e)
        {
            base.OnRegionFormatChanged(e);
        }

        protected override void OnTerminate()
        {
            base.OnTerminate();
        }

        static void Main(string[] args)
        {
            App app = new App();
            app.Run(args);
        }
    }
}
