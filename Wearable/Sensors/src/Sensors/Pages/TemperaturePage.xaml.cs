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
using Tizen.Sensor;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace Sensors.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TemperaturePage : CirclePage
    {
        public TemperaturePage()
        {
            Model = new TemperatureModel
            {
                IsSupported = TemperatureSensor.IsSupported,
                SensorCount = TemperatureSensor.Count
            };

            InitializeComponent();

            if (Model.IsSupported)
            {
                Temperature = new TemperatureSensor();
                Temperature.DataUpdated += Temperature_DataUpdated;

                canvas.ChartScale = 200;
                canvas.Series = new List<Series>()
                {
                    new Series()
                    {
                        Color = SKColors.Red,
                        Name = "Temperature",
                        FormattedText = "Temperature={0}°",
                    },
                };
            }
        }

        public TemperatureModel Model { get; private set; }

        public TemperatureSensor Temperature { get; private set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Temperature?.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Temperature?.Stop();
        }

        private void Temperature_DataUpdated(object sender, TemperatureSensorDataUpdatedEventArgs e)
        {
            Model.Temperature = e.Temperature;
            canvas.Series[0].Points.Add(new Extensions.Point() { Ticks = DateTime.UtcNow.Ticks, Value = e.Temperature });
            canvas.InvalidateSurface();
        }
    }
}