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
    /// The Pedometer Data View Model
    /// </summary>
    public class PedometerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private uint stepCount = 0;
        private uint walkStepCount = 0;
        private uint runStepCount = 0;
        private float movingDistance = 0.0f;
        private float lastSpeed = 0.0f;
        private float lastSteppingFrequency = 0.0f;
        private PedometerState lastStepStatus = PedometerState.Unknown;

        /// <summary>
        /// Updates step count value view with the new value.
        /// </summary>
        /// <value> The step count. </value>
        public uint StepCount
        {
            get { return stepCount; }
            set { stepCount = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Updates walk step count value view with the new value.
        /// </summary>
        /// <value> The walk step count. </value>
        public uint WalkStepCount
        {
            get { return walkStepCount; }
            set { walkStepCount = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Updates run step count value view with the new value.
        /// </summary>
        /// <value> The run step count. </value>
        public uint RunStepCount
        {
            get { return runStepCount; }
            set { runStepCount = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Updates moving distance value view with the new value.
        /// </summary>
        /// <value> The moving distance. </value>
        public float MovingDistance
        {
            get { return movingDistance; }
            set { movingDistance = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Updates last speed value view with the new value.
        /// </summary>
        /// <value> The last speed. </value>
        public float LastSpeed
        {
            get { return lastSpeed; }
            set { lastSpeed = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Updates last stepping frequency value view with the new value.
        /// </summary>
        /// <value> The last stepping frequency </value>
        public float LastSteppingFrequency
        {
            get { return lastSteppingFrequency; }
            set { lastSteppingFrequency = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Updates last step status value view with the new value.
        /// </summary>
        /// <value> The last step status </value>
        public PedometerState LastStepStatus
        {
            get { return lastStepStatus; }
            set { lastStepStatus = value; RaisePropertyChanged(); }
        }

        public void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
