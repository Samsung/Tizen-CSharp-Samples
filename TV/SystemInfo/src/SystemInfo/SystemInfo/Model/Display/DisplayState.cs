/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

namespace SystemInfo.Model.Display
{
    /// <summary>
    /// Enumerator that contains all states of the display.
    /// </summary>
    public enum DisplayState
    {
        /// <summary>
        /// Display is in normal state.
        /// </summary>
        Normal,

        /// <summary>
        /// Display is dimmed.
        /// </summary>
        Dim,

        /// <summary>
        /// Display is turned off.
        /// </summary>
        Off
    }
}