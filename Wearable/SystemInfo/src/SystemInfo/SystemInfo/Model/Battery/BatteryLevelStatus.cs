/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
namespace SystemInfo.Model.Battery
{
    /// <summary>
    /// Enumerator that contains all statuses of battery.
    /// </summary>
    public enum BatteryLevelStatus
    {
        /// <summary>
        /// Battery is empty.
        /// </summary>
        Empty = 0,

        /// <summary>
        /// Battery level is critical.
        /// </summary>
        Critical = 1,

        /// <summary>
        /// Battery level is low.
        /// </summary>
        Low = 2,

        /// <summary>
        /// Battery level is high.
        /// </summary>
        High = 3,

        /// <summary>
        /// Battery is full.
        /// </summary>
        Full = 4
    }
}