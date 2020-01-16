/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

﻿using System.Reflection;
using Xamarin.Forms;

namespace Ultraviolet.Enums
{
    /// <summary>
    /// Class with extentions of UvLevel enum.
    /// </summary>
    public static class UvLevelExtensions
    {
        /// <summary>
        /// Gets UvLevelDescriptionAttribute associated with UvLevel value.
        /// </summary>
        /// <param name="value">Type of uv level.</param>
        /// <returns>Attributes associated with UvLevel enum value.</returns>
        private static UvLevelDescriptionAttribute GetLevelDescriptionAttribute(UvLevel value)
        {
            if (!(value.GetType().GetField(value.ToString()) is FieldInfo fieldInfo))
            {
                return null;
            }

            if (!(fieldInfo.GetCustomAttribute(typeof(UvLevelDescriptionAttribute))
                is UvLevelDescriptionAttribute levelAttribute))
            {
                return null;
            }

            return levelAttribute;
        }

        /// <summary>
        /// Gets name associated with UvLevel enum value.
        /// </summary>
        /// <param name="value">Type of uv level.</param>
        /// <returns>Name associated with UvLevel enum value.</returns>
        public static string GetName(this UvLevel value)
        {
            if (!(GetLevelDescriptionAttribute(value) is UvLevelDescriptionAttribute levelAttribute))
            {
                return null;
            }

            return levelAttribute.Name;
        }

        /// <summary>
        /// Gets filename associated with UvLevel enum value.
        /// </summary>
        /// <param name="value">Type of uv level.</param>
        /// <returns>Filename associated with UvLevel enum value.</returns>
        public static string GetFilename(this UvLevel value)
        {
            if (!(GetLevelDescriptionAttribute(value) is UvLevelDescriptionAttribute levelAttribute))
            {
                return null;
            }

            return levelAttribute.Filename;
        }

        /// <summary>
        /// Gets uv index associated with UvLevel enum value.
        /// </summary>
        /// <param name="value">Type of uv level.</param>
        /// <returns>Uv index associated with UvLevel enum value.</returns>
        public static string GetUvIndex(this UvLevel value)
        {
            if (!(GetLevelDescriptionAttribute(value) is UvLevelDescriptionAttribute levelAttribute))
            {
                return null;
            }

            return levelAttribute.UvIndex;
        }

        /// <summary>
        /// Gets text color associated with UvLevel enum value.
        /// </summary>
        /// <param name="value">Type of uv level.</param>
        /// <returns>Color of the text associated with UvLevel enum value.</returns>
        public static Color GetTextColor(this UvLevel value)
        {
            if (!(GetLevelDescriptionAttribute(value) is UvLevelDescriptionAttribute levelAttribute))
            {
                return Color.Black;
            }

            return levelAttribute.TextColor;
        }

        /// <summary>
        /// Gets description text associated with UvLevel enum value.
        /// </summary>
        /// <param name="value">Type of uv level.</param>
        /// <returns>Description text associated with UvLevel enum value.</returns>
        public static string GetDescription(this UvLevel value)
        {
            if (!(GetLevelDescriptionAttribute(value) is UvLevelDescriptionAttribute levelAttribute))
            {
                return null;
            }

            return levelAttribute.Description;
        }
    }
}
