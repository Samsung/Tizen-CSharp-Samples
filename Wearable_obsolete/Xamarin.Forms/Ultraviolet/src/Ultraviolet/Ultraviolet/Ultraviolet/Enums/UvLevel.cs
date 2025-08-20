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

﻿using Xamarin.Forms;

namespace Ultraviolet.Enums
{
    /// <summary>
    /// Enum containing available uv levels.
    /// </summary>
    public enum UvLevel
    {
        [UvLevelDescription("Low", "low.png", "0.0-2.9", "30B00B", "Low")]
        Low,
        [UvLevelDescription("Moderate", "moderate.png", "3.0-5.9", "FFFF00", "Moderate")]
        Moderate,
        [UvLevelDescription("High", "high.png", "6.0-7.9", "F05A23", "High")]
        High,
        [UvLevelDescription("Extreme", "extreme.png", "11.0+", "6B48C7", "Extreme")]
        Extreme,
        [UvLevelDescription("Very high", "veryhigh.png", "8.0-10.9", "D7001D", "VeryHigh")]
        VeryHigh,
        [UvLevelDescription("None", "", "", "000000", "")]
        None
    }
}
