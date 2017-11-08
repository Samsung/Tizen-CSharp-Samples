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
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Sensor.Models
{
    /// <summary>
    /// The Orientation Sensor Data View Model
    /// </summary>
    public class OrientationSensorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private float azimuth = 0.0f;
        private float pitch = 0.0f;
        private float roll = 0.0f;

        /// <summary>
        /// Updates azimuth value view with the new value.
        /// </summary>
        /// <value> The azimuth </value>
        public float Azimuth
        {
            get { return azimuth; }
            set { azimuth = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Updates pitch value view with the new value.
        /// </summary>
        /// <value> The pitch. </value>
        public float Pitch
        {
            get { return pitch; }
            set { pitch = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Updates roll value view with the new value.
        /// </summary>
        /// <value> The roll. </value>
        public float Roll
        {
            get { return roll; }
            set { roll = value; RaisePropertyChanged(); }
        }

        public void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
