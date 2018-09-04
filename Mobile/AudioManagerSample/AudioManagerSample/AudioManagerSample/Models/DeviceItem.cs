/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using Xamarin.Forms;

namespace AudioManagerSample
{
    public class DeviceItem
    {
        public DeviceItem(int id, string type, string name, string state)
        {
            Id = id;
            Type = type;
            Name = name;
            State = state;
        }

        /// <summary>
        /// Gets or sets the device id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the device type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the device name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the device state.
        /// </summary>
        public string State { get; set; }
    }
}
