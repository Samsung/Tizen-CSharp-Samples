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
    public partial class LightPage : CirclePage
    {
        public LightPage()
        {
            Model = new LightModel
            {
                IsSupported = LightSensor.IsSupported,
                SensorCount = LightSensor.Count
            };

            InitializeComponent();

            if (Model.IsSupported)
            {
                Light = new LightSensor();
                Light.DataUpdated += Light_DataUpdated;

                canvas.ChartScale = 2000;
                canvas.Series = new List<Series>()
                {
                    new Series()
                    {
                        Color = SKColors.Red,
                        Name = "Level",
                        FormattedText = "Light Level={0}lux",
                    },
                };
            }
        }

        public LightSensor Light { get; private set; }

        public LightModel Model { get; private set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Light?.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Light?.Stop();
        }

        private void Light_DataUpdated(object sender, LightSensorDataUpdatedEventArgs e)
        {
            Model.Level = e.Level;
            canvas.Series[0].Points.Add(new Extensions.Point() { Ticks = DateTime.UtcNow.Ticks, Value = e.Level });
            canvas.InvalidateSurface();
        }
    }
}