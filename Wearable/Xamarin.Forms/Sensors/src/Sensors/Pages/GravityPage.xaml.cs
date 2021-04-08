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
    public partial class GravityPage : CirclePage
    {
        public GravityPage()
        {
            Model = new GravityModel
            {
                IsSupported = GravitySensor.IsSupported,
                SensorCount = GravitySensor.Count
            };

            InitializeComponent();

            if (Model.IsSupported)
            {
                Gravity = new GravitySensor();
                Gravity.DataUpdated += Gravity_DataUpdated;
                Gravity.AccuracyChanged += Gravity_AccuracyChanged;

                canvas.Series = new List<Series>()
                {
                    new Series()
                    {
                        Color = SKColors.Red,
                        Name = "X",
                        FormattedText = "X={0:f2}m/s^2",
                    },
                    new Series()
                    {
                        Color = SKColors.Green,
                        Name = "Y",
                        FormattedText = "Y={0:f2}m/s^2",
                    },
                    new Series()
                    {
                        Color = SKColors.Blue,
                        Name = "Z",
                        FormattedText = "Z={0:f2}m/s^2",
                    },
                };
            }
        }

        public GravitySensor Gravity { get; private set; }

        public GravityModel Model { get; private set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Gravity?.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Gravity?.Stop();
        }

        private void Gravity_AccuracyChanged(object sender, SensorAccuracyChangedEventArgs e)
        {
            Model.Accuracy = Enum.GetName(e.Accuracy.GetType(), e.Accuracy);
        }

        private void Gravity_DataUpdated(object sender, GravitySensorDataUpdatedEventArgs e)
        {
            Model.X = e.X;
            Model.Y = e.Y;
            Model.Z = e.Z;

            long ticks = DateTime.UtcNow.Ticks;
            foreach (var serie in canvas.Series)
            {
                switch (serie.Name)
                {
                    case "X":
                        serie.Points.Add(new Extensions.Point() { Ticks = ticks, Value = e.X });
                        break;

                    case "Y":
                        serie.Points.Add(new Extensions.Point() { Ticks = ticks, Value = e.Y });
                        break;

                    case "Z":
                        serie.Points.Add(new Extensions.Point() { Ticks = ticks, Value = e.Z });
                        break;
                }
            }
            canvas.InvalidateSurface();
        }
    }
}