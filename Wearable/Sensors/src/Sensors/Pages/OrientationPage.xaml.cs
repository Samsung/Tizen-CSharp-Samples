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
    public partial class OrientationPage : CirclePage
    {
        public OrientationPage()
        {
            Model = new OrientationModel
            {
                IsSupported = OrientationSensor.IsSupported,
                SensorCount = OrientationSensor.Count
            };

            InitializeComponent();

            if (Model.IsSupported)
            {
                Orientation = new OrientationSensor();
                Orientation.AccuracyChanged += Orientation_AccuracyChanged;
                Orientation.DataUpdated += Orientation_DataUpdated;

                canvas.Series = new List<Series>()
                {
                    new Series()
                    {
                        Color = SKColors.Red,
                        Name = "Azimuth",
                        FormattedText = "Azimuth={0:f2}",
                    },
                    new Series()
                    {
                        Color = SKColors.Green,
                        Name = "Pitch",
                        FormattedText = "Pitch={0:f2}",
                    },
                    new Series()
                    {
                        Color = SKColors.Blue,
                        Name = "Roll",
                        FormattedText = "Roll={0:f2}",
                    },
                };
            }
        }

        public OrientationModel Model { get; private set; }

        public OrientationSensor Orientation { get; private set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Orientation?.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Orientation?.Stop();
        }

        private void Orientation_AccuracyChanged(object sender, SensorAccuracyChangedEventArgs e)
        {
            Model.Accuracy = Enum.GetName(e.Accuracy.GetType(), e.Accuracy);
        }

        private void Orientation_DataUpdated(object sender, OrientationSensorDataUpdatedEventArgs e)
        {
            Model.Azimuth = e.Azimuth;
            Model.Pitch = e.Pitch;
            Model.Roll = e.Roll;

            long ticks = DateTime.UtcNow.Ticks;
            foreach (var serie in canvas.Series)
            {
                switch (serie.Name)
                {
                    case "Azimuth":
                        serie.Points.Add(new Extensions.Point() { Ticks = ticks, Value = e.Azimuth });
                        break;

                    case "Pitch":
                        serie.Points.Add(new Extensions.Point() { Ticks = ticks, Value = e.Pitch });
                        break;

                    case "Roll":
                        serie.Points.Add(new Extensions.Point() { Ticks = ticks, Value = e.Roll });
                        break;
                }
            }
            canvas.InvalidateSurface();
        }
    }
}