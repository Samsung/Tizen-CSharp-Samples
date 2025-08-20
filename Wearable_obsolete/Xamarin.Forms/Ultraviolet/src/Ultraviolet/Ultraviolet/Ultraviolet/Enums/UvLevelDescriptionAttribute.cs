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

﻿using System;
using Ultraviolet.Resources;
using Xamarin.Forms;

namespace Ultraviolet.Enums
{
    /// <summary>
    /// UV level description attributes class.
    /// </summary>
    public class UvLevelDescriptionAttribute : Attribute
    {
        /// <summary>
        /// Name of uv level.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Filename of image associated with certain level.
        /// </summary>
        public string Filename { get; }

        /// <summary>
        /// Uv index associated with certain uv level.
        /// </summary>
        public string UvIndex { get; }

        /// <summary>
        /// Text color associated with certain uv level.
        /// </summary>
        public Color TextColor { get; }

        /// <summary>
        /// Detailed description associated with certain uv level.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Initializes class.
        /// </summary>
        /// <param name="name">Name of uv level.</param>
        /// <param name="filename">Filename of image associated with certain level.</param>
        /// <param name="uvIndex">Uv index associated with certain level.</param>
        /// <param name="textColor">Color of the text associated with certain level.</param>
        /// <param name="descriptionKey">Description text key associated with certain level.</param>
        public UvLevelDescriptionAttribute(string name, string filename, string uvIndex, string textColor, string descriptionKey)
        {
            Name = name;
            Filename = filename;
            UvIndex = uvIndex;
            TextColor = Color.FromHex(textColor);
            Description = LevelsDescriptions.ResourceManager.GetString(descriptionKey);
        }
    }
}
