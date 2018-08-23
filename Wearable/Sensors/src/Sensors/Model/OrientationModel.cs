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
    /// OrientationModel class.
    /// </summary>
    public class OrientationModel : BaseSensorModel
    {
        private string accuracy;

        private float azimuth;

        private float pitch;

        private float roll;

        /// <summary>
        /// Property for accuracy.
        /// </summary>
        public string Accuracy
        {
            get { return accuracy; }
            set
            {
                accuracy = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Property for azimuth.
        /// </summary>
        public float Azimuth
        {
            get { return azimuth; }
            set
            {
                azimuth = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Property for pitch.
        /// </summary>
        public float Pitch
        {
            get { return pitch; }
            set
            {
                pitch = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Property for roll.
        /// </summary>
        public float Roll
        {
            get { return roll; }
            set
            {
                roll = value;
                OnPropertyChanged();
            }
        }
    }
}