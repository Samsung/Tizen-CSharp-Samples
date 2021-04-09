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
using System;

namespace FindPlace.Enums
{
    /// <summary>
    /// Place type description attributes class.
    /// </summary>
    public class PlaceTypeDescriptionAttribute : Attribute
    {
        #region properties

        /// <summary>
        /// Name of place type.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Filename of image associated with place type.
        /// </summary>
        public string Filename { get; }

        /// <summary>
        /// Place category id associated with place type.
        /// </summary>
        public string PlaceCategoryId { get; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes class.
        /// </summary>
        /// <param name="name">Name of place type.</param>
        /// <param name="filename">Filename of image associated with place type.</param>
        /// <param name="placeCategoryId">Place category id associated with place type.</param>
        public PlaceTypeDescriptionAttribute(string name, string filename, string placeCategoryId)
        {
            Name = name;
            Filename = filename;
            PlaceCategoryId = placeCategoryId;
        }

        #endregion
    }
}
