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
using static Sensor.SensorEventArgs;

namespace Sensor.Models
{
    /// <summary>
    /// The Walking Activity Detector Data View Model
    /// </summary>
    public class WalkingActivityDetectorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DetectorState state = DetectorState.Unknown;

        /// <summary>
        /// Updates walking state view with the new value.
        /// </summary>
        /// <value> The walking state. </value>
        public DetectorState State
        {
            get { return state; }
            set { state = value; RaisePropertyChanged(); }
        }

        public void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
