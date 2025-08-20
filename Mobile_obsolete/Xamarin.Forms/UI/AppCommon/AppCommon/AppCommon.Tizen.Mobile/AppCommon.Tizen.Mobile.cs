/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Xamarin.Forms;
using Tizen.Applications;
using System.Globalization;

namespace AppCommon.Tizen.Mobile
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        private App _app;

        protected override void OnCreate()
        {
            base.OnCreate();
            var width = MainWindow.ScreenSize.Width;
            var height = MainWindow.ScreenSize.Height;
            _app = new App(width, height);
            LoadApplication(_app);
            MainWindow.AvailableRotations = ElmSharp.DisplayRotation.Degree_0;
        }

        /// <summary>
        /// Handle LowBattery event
        /// </summary>
        /// <param name="e">An event data</param>
        protected override void OnLowBattery(LowBatteryEventArgs e)
        {
            base.OnLowBattery(e);
            var status = (LowBatteryStatus)e.LowBatteryStatus;
            _app.ApplicationInformation.UpdateLowBatteryLEDColor(status);
        }

        /// <summary>
        /// Handle LowMemory event
        /// </summary>
        /// <param name="e">An event data</param>
        protected override void OnLowMemory(LowMemoryEventArgs e)
        {
            base.OnLowMemory(e);
            var status = (LowMemoryStatus)e.LowMemoryStatus;
            _app.ApplicationInformation.UpdateLowMemoryLEDColor(status);
        }

        /// <summary>
        /// Handle LocaleChanged event
        /// </summary>
        /// <param name="e">An event data</param>
        protected override void OnLocaleChanged(LocaleChangedEventArgs e)
        {
            base.OnLocaleChanged(e);
            _app.ApplicationInformation.Language = new CultureInfo(e.Locale).DisplayName;
        }

        /// <summary>
        /// Handle RegionFormatChanged event
        /// </summary>
        /// <param name="e">An event data</param>
        protected override void OnRegionFormatChanged(RegionFormatChangedEventArgs e)
        {
            base.OnRegionFormatChanged(e);
            _app.ApplicationInformation.RegionFormat = new CultureInfo(e.Region).DisplayName;
        }

        /// <summary>
        /// Handle DeviceOrientation event
        /// </summary>
        /// <param name="e">An event data</param>
        protected override void OnDeviceOrientationChanged(DeviceOrientationEventArgs e)
        {
            base.OnDeviceOrientationChanged(e);
            var orientation = (DeviceOrientationStatus)e.DeviceOrientation;
            _app.ApplicationInformation.UpdateDeviceOrientation(orientation);
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            app.Run(args);
        }
    }
}
