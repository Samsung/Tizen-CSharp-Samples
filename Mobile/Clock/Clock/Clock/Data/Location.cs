/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
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

namespace Clock.Data
{
    /// <summary>
    /// Location
    /// </summary>
    public struct Location
    {
        /// <summary>
        /// Name of location
        /// </summary>
        public string Name;
        /// <summary>
        /// Country name of location
        /// </summary>
        public string Country;
        /// <summary>
        /// GMT offset of location
        /// </summary>
        public int GmtOffset;
        /// <summary>
        /// X coordinate
        /// </summary>
        public int X;
        /// <summary>
        /// Y coordinate
        /// </summary>
        public int Y;

        /// <summary>
        /// Location constructor
        /// </summary>
        /// <param name="_name">Location name</param>
        /// <param name="_country">Country info</param>
        /// <param name="_gmtOffset">GMT offset</param>
        /// <param name="_x">X coordinate</param>
        /// <param name="_y">Y coordinate</param>
        public Location(string _name, string _country, int _gmtOffset, int _x, int _y)
        {
            Name = _name;
            Country = _country;
            GmtOffset = _gmtOffset;
            X = _x;
            Y = _y;
        }

        /// <summary>
        /// Return location string
        /// </summary>
        /// <returns>Returns location string</returns>
        public override string ToString()
        {
            return Name + " - " + Country;
        }
    }
}
