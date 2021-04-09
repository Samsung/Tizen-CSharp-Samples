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
using SystemInfo.Tizen.Mobile;
using TizenOs = Tizen.System;

[assembly: Xamarin.Forms.Dependency(typeof(SystemInfoImplementation))]

namespace SystemInfo.Tizen.Mobile
{
    public class SystemInfoImplementation : ISystemInfo
    {

        /// <summary>
        /// a native implement class construct method
        /// </summary>
        public SystemInfoImplementation()
        {

        }


        /// <summary>
        /// return true or false from key input
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>bool</returns>
        public string TryGetValue(string key)
        {
            string result = "";

            if (TizenOs.Information.TryGetValue(key, out string stringValue) == true)
            {
                result = stringValue;
            }
            else if (TizenOs.Information.TryGetValue(key, out bool  boolValue) == true)
            {
                result = boolValue ? "Supported" : "Not Supported";
            }
            else if (TizenOs.Information.TryGetValue(key, out int intValue) == true)
            {
                result = intValue.ToString();
            }
            else if (TizenOs.Information.TryGetValue(key, out double doubleValue) == true)
            {
                result = doubleValue.ToString();
            }
            else if (TizenOs.Information.TryGetValue(key, out DateTime dateTimeValue) == true)
            {
                result = dateTimeValue.ToString();
            }

            return result;
        }
    }





}
