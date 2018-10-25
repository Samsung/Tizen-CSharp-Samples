/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System.Collections.Generic;

namespace Compass.Utils
{
    /// <summary>
    /// Indicates compass direction.
    /// </summary>
    public class DirectionIndicator
    {
        #region fields

        /// <summary>
        /// Limit values for directions.
        /// </summary>
        private static readonly int[] _directionBoundaries = { 23, 67, 113, 157, 203, 247, 293, 337 };

        /// <summary>
        /// Dictionary with all possible options of the compass direction.
        /// </summary>
        private static readonly Dictionary<int, CompassDirections> _directionDictionary =
            new Dictionary<int, CompassDirections>
            {
                { 0, CompassDirections.NorthEast },
                { 1, CompassDirections.East },
                { 2, CompassDirections.SouthEast },
                { 3, CompassDirections.South },
                { 4, CompassDirections.SouthWest },
                { 5, CompassDirections.West },
                { 6, CompassDirections.NorthWest },
                { 7, CompassDirections.North }
            };

        #endregion

        #region methods

        /// <summary>
        /// Returns compass direction.
        /// </summary>
        /// <param name="azimuth">Compass deviation.</param>
        /// <returns>Compass direction.</returns>
        public static CompassDirections GetCompassDirection(float azimuth)
        {
            CompassDirections compassDirection = CompassDirections.North;

            for (int i = 0; i < _directionBoundaries.Length; i++)
            {
                if (azimuth > _directionBoundaries[i])
                {
                    compassDirection = _directionDictionary[i];
                }
            }

            return compassDirection;
        }

        #endregion
    }
}
