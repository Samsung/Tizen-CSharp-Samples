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

using Sensors.Extensions;
using Sensors.Model;
using SkiaSharp;
using System;
using System.Collections.Generic;
using Tizen.Security;
using Tizen.Sensor;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace Sensors.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HRMPage : CirclePage
    {
        private const string hrmPrivilege = "http://tizen.org/privilege/healthinfo";

        public HRMPage()
        {
            Model = new HRMModel
            {
                IsSupported = HeartRateMonitor.IsSupported,
                SensorCount = HeartRateMonitor.Count
            };

            InitializeComponent();

            SetupPrivilegeHandler();
            CheckResult result = PrivacyPrivilegeManager.CheckPermission(hrmPrivilege);
            switch (result)
            {
                case CheckResult.Allow:
                    CreateHRM();
                    break;

                case CheckResult.Deny:
                    break;

                case CheckResult.Ask:
                    PrivacyPrivilegeManager.RequestPermission(hrmPrivilege);
                    break;
            }
        }

        public HeartRateMonitor HRM { get; private set; }

        public HRMModel Model { get; private set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetupPrivilegeHandler();
            HRM?.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            HRM?.Stop();
        }

        private void CreateHRM()
        {
            if (Model.IsSupported)
            {
                HRM = new HeartRateMonitor();
                HRM.DataUpdated += HRM_DataUpdated;

                canvas.ChartScale = 200;
                canvas.Series = new List<Series>()
                {
                    new Series()
                    {
                        Color = SKColors.Red,
                        Name = "HeartRate",
                        FormattedText = "Heart Rate={0}",
                    },
                };
            }
        }

        private void HRM_DataUpdated(object sender, HeartRateMonitorDataUpdatedEventArgs e)
        {
            Model.HeartRate = e.HeartRate;
            canvas.Series[0].Points.Add(new Extensions.Point() { Ticks = DateTime.UtcNow.Ticks, Value = e.HeartRate });
            canvas.InvalidateSurface();
        }

        private void PrivilegeResponseHandler(object sender, RequestResponseEventArgs e)
        {
            if (e.cause == CallCause.Error)
            {
                return;
            }

            switch (e.result)
            {
                case RequestResult.AllowForever:
                    CreateHRM();
                    HRM.Start();
                    break;

                case RequestResult.DenyForever:
                case RequestResult.DenyOnce:
                    break;
            }
        }

        private void SetupPrivilegeHandler()
        {
            PrivacyPrivilegeManager.ResponseContext context = null;
            if (PrivacyPrivilegeManager.GetResponseContext(hrmPrivilege).TryGetTarget(out context))
            {
                context.ResponseFetched += PrivilegeResponseHandler;
            }
        }
    }
}