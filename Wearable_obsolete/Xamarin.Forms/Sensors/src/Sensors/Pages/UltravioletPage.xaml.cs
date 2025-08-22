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
    public partial class UltravioletPage : CirclePage
    {
        public UltravioletPage()
        {
            Model = new UltravioletModel
            {
                IsSupported = UltravioletSensor.IsSupported,
                SensorCount = UltravioletSensor.Count
            };

            InitializeComponent();

            if (Model.IsSupported)
            {
                Ultraviolet = new UltravioletSensor();
                Ultraviolet.DataUpdated += Ultraviolet_DataUpdated;

                canvas.ChartScale = 200;
                canvas.Series = new List<Series>()
                {
                    new Series()
                    {
                        Color = SKColors.Red,
                        Name = "UltravioletIndex",
                        FormattedText = "Ultraviolet Index={0}",
                    },
                };
            }
        }

        public UltravioletModel Model { get; private set; }

        public UltravioletSensor Ultraviolet { get; private set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Ultraviolet?.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Ultraviolet?.Stop();
        }

        private void Ultraviolet_DataUpdated(object sender, UltravioletSensorDataUpdatedEventArgs e)
        {
            Model.UltravioletIndex = e.UltravioletIndex;
            canvas.Series[0].Points.Add(new Extensions.Point() { Ticks = DateTime.UtcNow.Ticks, Value = e.UltravioletIndex });
            canvas.InvalidateSurface();
        }
    }
}