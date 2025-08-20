/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
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
 *
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Tizen.NUI.MediaHub
{
    /// <summary>
    /// Item type.
    /// </summary>
    public static class ContentItemType
    {
        /// <summary>
        /// eItemNone, for null
        /// </summary>
        public const int eItemNone = -1;
        /// <summary>
        /// eItemVideo, for video
        /// </summary>
        public const int eItemVideo = 0;
        /// <summary>
        /// eItemPhoto, for photo
        /// </summary>
        public const int eItemPhoto = 1;
        /// <summary>
        /// eItemMusic, for music
        /// </summary>
        public const int eItemMusic = 2;
        /// <summary>
        /// eItemFolder, for folder
        /// </summary>
        public const int eItemFolder = 3;
        /// <summary>
        /// eItemUpFolder, for upFolder
        /// </summary>
        public const int eItemUpFolder = 4;
    }

    public class Utility
    {
        /// <summary>
        /// Transfer color value(#rgb) to Color.
        /// </summary>
        /// <param name="hex">rgb value</param>
        /// <param name="a">alpha value</param>
        /// <returns>Transfered color</returns>
        public static Color Hex2Color(uint hex, float a)
        {
            float r = 0, g = 0, b = 0;
            b = (hex & 0xff) / 255.0f;
            g = (hex >> 8 & 0xff) / 255.0f;
            r = (hex >> 16 & 0xff) / 255.0f;

            return new Color(r, g, b, a);
        }
    }
}