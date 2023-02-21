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

using System;
using Tizen.NUI.Components;
using Sensor.Tizen.Mobile;

namespace Sensor.Pages
{
    /// <summary>
    /// GravitySensor Information Page
    /// </summary>
    public partial class GravitySensorPage : ContentPage
    {
        private Sensor.Models.GravitySensorViewModel gravitySensor;
        private EventHandler<SensorEventArgs> listener;
        private SensorInfo info;

        /// <summary>
        /// Constructor of GravitySensorPage
        /// </summary>
        /// <param name="info">Sensor Information</param>
        public GravitySensorPage(SensorInfo info)
        {
            InitializeComponent();

            gravitySensor = new Sensor.Models.GravitySensorViewModel();

            BindingContext = gravitySensor;
            SensorInfo.BindingContext = info;
            this.info = info;
        }

        /// <summary>
        /// Called when start button is clicked.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="args">Event arguments</param>
        public void StartClicked(object sender, EventArgs args)
        {
            listener = (s, e) =>
            {
                gravitySensor.X = e.Values[0];
                gravitySensor.Y = e.Values[1];
                gravitySensor.Z = e.Values[2];
            };

            try
            {
                Program.sensorManager.StartSensor(info.Type, listener);
            }
            catch (Exception e)
            {
                global::Tizen.Log.Info("Sensor", e.Message.ToString());
            }

            global::Tizen.Log.Info("Sensor", "Sensor Start");
        }

        /// <summary>
        /// Called when stop button is clicked.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="args">Event arguments</param>
        public void StopClicked(object sender, EventArgs args)
        {
            try
            {
                Program.sensorManager.StopSensor(info.Type, listener);
            }
            catch (Exception e)
            {
                global::Tizen.Log.Info("Sensor", e.Message.ToString());
            }

            global::Tizen.Log.Info("Sensor", "Sensor Stop");
        }
    }
}
