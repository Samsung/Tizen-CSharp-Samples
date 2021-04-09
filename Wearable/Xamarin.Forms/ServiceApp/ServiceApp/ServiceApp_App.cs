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

        /// <summary>
        /// Called when the application is launched.
        /// If base.OnCreated() is not called, the event 'Created' will not be emitted.
        /// </summary>
        protected override void OnCreate()
        {
            // Start this service.
            base.OnCreate();
            // Create a MessagePort object.
            messagePort = new MessagePort(serviceAppPortName, false);
            // Register an event handler for MessagePort
            messagePort.MessageReceived += (s, e) =>
            {
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
            // Listen register this loal message port so this must be called to Send
            messagePort.Listen();
        }

        /// <summary>
        /// Called when the application receives the appcontrol message.
        /// If base.OnAppControlReceived() is not called, the event 'AppControlReceived' will not be emitted.
        /// </summary>
        /// <param name="e">AppControlReceivedEventArgs</param>
        protected override void OnAppControlReceived(AppControlReceivedEventArgs e)
        {
            base.OnAppControlReceived(e);
        }

        /// <summary>
        /// Called when the device orientation is changed.
        /// If base.OnRegionFormatChanged() is not called, the event 'RegionFormatChanged' will not be emitted.
        /// </summary>
        /// <param name="e">DeviceOrientationEventArgs</param>
        protected override void OnDeviceOrientationChanged(DeviceOrientationEventArgs e)
        {
            base.OnDeviceOrientationChanged(e);
        }

        /// <summary>
        /// Called when the system language is changed.
        /// If base.OnLocaleChanged() is not called, the event 'LocaleChanged' will not be emitted.
        /// </summary>
        /// <param name="e">LocaleChangedEventArgs</param>
        protected override void OnLocaleChanged(LocaleChangedEventArgs e)
        {
            base.OnLocaleChanged(e);
        }

        /// <summary>
        /// Called when the system battery is low.
        /// If base.OnLowBattery() is not called, the event 'LowBattery' will not be emitted.
        /// </summary>
        /// <param name="e">LowBatteryEventArgs</param>
        protected override void OnLowBattery(LowBatteryEventArgs e)
        {
            base.OnLowBattery(e);
        }

        /// <summary>
        /// Called when the system memory is low.
        /// If base.OnLowMemory() is not called, the event 'LowMemory' will not be emitted.
        /// </summary>
        /// <param name="e">LowMemoryEventArgs</param>
        protected override void OnLowMemory(LowMemoryEventArgs e)
        {
            base.OnLowMemory(e);
        }

        /// <summary>
        /// Called when the region format is changed.
        /// If base.OnRegionFormatChanged() is not called, the event 'RegionFormatChanged' will not be emitted.
        /// </summary>
        /// <param name="e">RegionFormatChangedEventArgs</param>
        protected override void OnRegionFormatChanged(RegionFormatChangedEventArgs e)
        {
            base.OnRegionFormatChanged(e);
        }

        /// <summary>
        /// Called when the application is terminated.
        /// If base.OnTerminate() is not called, the event 'Terminated' will not be emitted.
        /// </summary>
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
