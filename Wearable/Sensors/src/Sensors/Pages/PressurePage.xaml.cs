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
    public partial class PressurePage : CirclePage
    {
        public PressurePage()
        {
            Model = new PressureModel
            {
                IsSupported = PressureSensor.IsSupported,
                SensorCount = PressureSensor.Count
            };

            InitializeComponent();

            if (Model.IsSupported)
            {
                Pressure = new PressureSensor();
                Pressure.DataUpdated += Pressure_DataUpdated;

                canvas.ChartScale = 2500;
                canvas.Series = new List<Series>()
                {
                    new Series()
                    {
                        Color = SKColors.Red,
                        Name = "Pressure",
                        FormattedText = "Pressure={0}hPa",
                    },
                };
            }
        }

        public PressureModel Model { get; private set; }

        public PressureSensor Pressure { get; private set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Pressure?.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Pressure?.Stop();
        }

        private void Pressure_DataUpdated(object sender, PressureSensorDataUpdatedEventArgs e)
        {
            Model.Pressure = e.Pressure;
            canvas.Series[0].Points.Add(new Extensions.Point() { Ticks = DateTime.UtcNow.Ticks, Value = e.Pressure });
            canvas.InvalidateSurface();
        }
    }
}