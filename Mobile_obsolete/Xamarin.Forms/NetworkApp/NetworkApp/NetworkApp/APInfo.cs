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
using System.Text;

namespace NetworkApp
{
    /// <summary>
    /// The Wi-Fi AP information
    /// </summary>
    public class APInfo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Name">ESSID of Wi-Fi AP</param>
        /// <param name="State">State of Wi-Fi AP</param>
        public APInfo(String Name, String State)
        {
            this.Name = Name;
            this.State = State;
        }

        /// <summary>
        /// ESSID of Wi-Fi AP
        /// </summary>
        public String Name;
        /// <summary>
        /// State of Wi-Fi AP
        /// </summary>
        public String State;
    }
}
