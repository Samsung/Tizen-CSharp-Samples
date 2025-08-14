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
    /// The Gyroscope Data View Model
    /// </summary>
    public class GyroscopeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private float x = 0.0f;
        private float y = 0.0f;
        private float z = 0.0f;

        /// <summary>
        /// Updates X value view with the new value.
        /// </summary>
        /// <value> X </value>
        public float X
        {
            get { return x; }
            set { x = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Updates Y value view with the new value.
        /// </summary>
        /// <value> Y </value>
        public float Y
        {
            get { return y; }
            set { y = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Updates Z value view with the new value.
        /// </summary>
        /// <value> Z </value>
        public float Z
        {
            get { return z; }
            set { z = value; RaisePropertyChanged(); }
        }

        public void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
