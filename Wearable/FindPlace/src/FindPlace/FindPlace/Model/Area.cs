/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
namespace FindPlace.Model
{
    /// <summary>
    /// Stores information about area.
    /// </summary>
    public class Area
    {
        #region properties

        /// <summary>
        /// Top left point of the area.
        /// </summary>
        public Geocoordinates TopLeftPoint { get; }

        /// <summary>
        /// Bottom right point of the area.
        /// </summary>
        public Geocoordinates BottomRightPoint { get; }

        /// <summary>
        /// Center point of the area.
        /// </summary>
        public Geocoordinates CenterPoint { get; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes Area class.
        /// </summary>
        /// <param name="topLeftPoint">Top left point of the area.</param>
        /// <param name="bottomRightPoint">Bottom right of the area.</param>
        public Area(Geocoordinates topLeftPoint, Geocoordinates bottomRightPoint)
        {
            TopLeftPoint = topLeftPoint;
            BottomRightPoint = bottomRightPoint;
            CenterPoint = new Geocoordinates(
                (topLeftPoint.Latitude + bottomRightPoint.Latitude) / 2,
                (topLeftPoint.Longitude + bottomRightPoint.Longitude) / 2);
        }

        #endregion
    }
}
