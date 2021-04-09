/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Tizen.Sensor;
using Xamarin.Forms;

namespace Level.ViewModels
{
    /// <summary>
    /// Provides Level page view abstraction
    /// </summary>
    public class LevelViewModel : INotifyPropertyChanged, IDisposable
    {
        private const double InitPosition = 0.5d;
        private const int SampleLimit = 10;
        private double xSum = InitPosition * SampleLimit;
        private double ySum = InitPosition * SampleLimit;
        private GravitySensor _gravitySensor;
        private Queue<(double x, double y)> positions;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Position of pointer on absoluteLayout
        /// </summary>
        public Rectangle PositionCoordinates { get; set; }

        /// <summary>
        /// Initializes LevelViewModel class instance
        /// </summary>
        public LevelViewModel()
        {
            positions = new Queue<(double, double)>(
                Enumerable.Repeat((InitPosition, InitPosition), SampleLimit));
            PositionCoordinates = new Rectangle(InitPosition, InitPosition, 34, 34);
            _gravitySensor = new GravitySensor();
            _gravitySensor.Interval = 50;
            _gravitySensor.DataUpdated += OnGravityUpdated;
            _gravitySensor.Start();
        }

        /// <summary>
        /// Handler for gravity updated event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void OnGravityUpdated(object sender, Tizen.Sensor.GravitySensorDataUpdatedEventArgs e)
        {
            var oldPosition = positions.Dequeue();
            xSum -= oldPosition.x;
            ySum -= oldPosition.y;

            double xAbsolute = (e.X + 10d) / 20d;
            double yAbsolute = (e.Y + 10d) / 20d;
            xSum += xAbsolute;
            ySum += yAbsolute;
            positions.Enqueue((xAbsolute, yAbsolute));

            UpdateLevel(xSum / (double)SampleLimit, ySum / (double)SampleLimit);
        }

        /// <summary>
        /// Updates level pointer
        /// </summary>
        /// <param name="x">absolute value 0..1</param>
        /// <param name="y">absolute value 0..1</param>
        private void UpdateLevel(double x, double y)
        {
            PositionCoordinates = new Rectangle(x, (y - 1) * (-1), 34, 34);
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(PositionCoordinates)));
        }

        /// <summary>
        /// Called when page is popped
        /// </summary>
        public void Dispose()
        {
            _gravitySensor?.Stop();
            _gravitySensor?.Dispose();
            _gravitySensor = null;
        }
    }
}
