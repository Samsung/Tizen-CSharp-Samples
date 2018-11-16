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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sensors.Model
{
    /// <summary>
    /// BaseSensorModel abstract class.
    /// </summary>
	public abstract class BaseSensorModel : INotifyPropertyChanged
    {
        private bool isSupported;

        private int sensorCount;

        /// <summary>
        /// Raised when the property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Property for is supported.
        /// </summary>
        public bool IsSupported
        {
            get { return isSupported; }
            set
            {
                isSupported = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Property for sensor count.
        /// </summary>
        public int SensorCount
        {
            get { return sensorCount; }
            set
            {
                sensorCount = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Call method when property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
		public void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}