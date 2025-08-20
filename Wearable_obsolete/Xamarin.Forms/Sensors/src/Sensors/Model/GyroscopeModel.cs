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
    /// GyroscopeModel class.
    /// </summary>
    public class GyroscopeModel : BaseSensorModel
    {
        private float x;

        private float y;

        private float z;

        /// <summary>
        /// Property for actual X value.
        /// </summary>
        public float X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Property for actual Y value.
        /// </summary>
        public float Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Property for actual Z value.
        /// </summary>
        public float Z
        {
            get { return z; }
            set
            {
                z = value;
                OnPropertyChanged();
            }
        }
    }
}