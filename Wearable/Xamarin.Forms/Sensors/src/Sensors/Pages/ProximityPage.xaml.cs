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

using Sensors.Model;
using System;
using Tizen.Sensor;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace Sensors.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProximityPage : CirclePage
    {
        public ProximityPage()
        {
            Model = new ProximityModel
            {
                IsSupported = ProximitySensor.IsSupported,
                SensorCount = ProximitySensor.Count
            };

            InitializeComponent();

            if (Model.IsSupported)
            {
                Proximity = new ProximitySensor();
                Proximity.DataUpdated += Proximity_DataUpdated;
            }
        }

        public ProximityModel Model { get; private set; }

        public ProximitySensor Proximity { get; private set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Proximity?.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Proximity?.Stop();
        }

        private void Proximity_DataUpdated(object sender, ProximitySensorDataUpdatedEventArgs e)
        {
            Model.Proximity = Enum.GetName(e.Proximity.GetType(), e.Proximity);
        }
    }
}