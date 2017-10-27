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
    /// Timezone for World clock map
    /// </summary>
    public struct Timezone
    {
        // X Coordinate
        public int xCoord;
        // Zone's width
        public int zoneWidth;
        // GMT offset
        public int gmtOffset;
        // The principal cities at the time zone
        public Location[] places;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_xCoord">int</param>
        /// <param name="_zoneWidth">int</param>
        /// <param name="_gmtOffset">int</param>
        /// <param name="_places">Location[]</param>
        public Timezone(int _xCoord, int _zoneWidth, int _gmtOffset, Location[] _places)
        {
            xCoord = _xCoord;
            zoneWidth = _zoneWidth;
            gmtOffset = _gmtOffset;
            places = _places;
        }
    }
}
