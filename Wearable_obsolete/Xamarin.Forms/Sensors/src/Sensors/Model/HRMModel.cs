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

namespace Sensors.Model
{
    /// <summary>
    /// Heart Rate Monitor (HRM) Sensor data model class.
    /// </summary>
    public class HRMModel : BaseSensorModel
    {
        private int heartRate;

        /// <summary>
        /// Property for heart rate.
        /// </summary>
        public int HeartRate
        {
            get { return heartRate; }
            set
            {
                heartRate = value;
                OnPropertyChanged();
            }
        }
    }
}