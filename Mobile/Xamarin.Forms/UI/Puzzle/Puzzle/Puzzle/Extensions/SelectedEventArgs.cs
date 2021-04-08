/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;

namespace Puzzle.Extensions
{
    /// <summary>
    /// Event arguments for events of RadioButton.
    /// </summary>
    public class SelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new SelectedEventArgs object that represents a change from RadioButton.
        /// </summary>
        /// <param name="value">The boolean value that checks whether the RadioButton is selected.</param>
        public SelectedEventArgs(bool value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the value object for the SelectedEventArgs object.
        /// </summary>
        public bool Value { get; private set; }
    }
}
