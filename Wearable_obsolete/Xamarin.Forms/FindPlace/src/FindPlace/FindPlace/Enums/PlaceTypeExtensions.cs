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
using System.Reflection;

namespace FindPlace.Enums
{
    /// <summary>
    /// Class with extensions of PlaceType enum.
    /// </summary>
    public static class PlaceTypeExtensions
    {
        #region methods

        /// <summary>
        /// Gets PlaceTypeDescriptionAttribute associated with PlaceType value.
        /// </summary>
        /// <param name="value">Type of place.</param>
        /// <returns>Attributes associated with PlaceType value.</returns>
        private static PlaceTypeDescriptionAttribute GetPlaceDescriptionAttribute(PlaceType value)
        {
            if (!(value.GetType().GetField(value.ToString()) is FieldInfo fieldInfo))
            {
                return null;
            }

            if (!(fieldInfo.GetCustomAttribute(typeof(PlaceTypeDescriptionAttribute))
                is PlaceTypeDescriptionAttribute placeAttribute))
            {
                return null;
            }

            return placeAttribute;
        }

        /// <summary>
        /// Gets name associated with PlaceType value.
        /// </summary>
        /// <param name="value">Type of place.</param>
        /// <returns>Name associated with PlaceType value.</returns>
        public static string GetName(this PlaceType value)
        {
            if (!(GetPlaceDescriptionAttribute(value) is PlaceTypeDescriptionAttribute placeAttribute))
            {
                return null;
            }

            return placeAttribute.Name;
        }

        /// <summary>
        /// Gets filename associated with PlaceType value.
        /// </summary>
        /// <param name="value">Type of place.</param>
        /// <returns>Filename associated with PlaceType value.</returns>
        public static string GetFilename(this PlaceType value)
        {
            if (!(GetPlaceDescriptionAttribute(value) is PlaceTypeDescriptionAttribute placeAttribute))
            {
                return null;
            }

            return placeAttribute.Filename;
        }

        /// <summary>
        /// Gets category id associated with PlaceType value.
        /// </summary>
        /// <param name="value">Type of place.</param>
        /// <returns>Category id associated with PlaceType value.</returns>
        public static string GetPlaceCategoryId(this PlaceType value)
        {
            if (!(GetPlaceDescriptionAttribute(value) is PlaceTypeDescriptionAttribute placeAttribute))
            {
                return null;
            }

            return placeAttribute.PlaceCategoryId;
        }

        #endregion
    }
}
