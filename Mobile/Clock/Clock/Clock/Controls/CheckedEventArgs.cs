/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Clock.Controls
{
    /// <summary>
    /// EventArgs for the Checkbox's check events.
    /// </summary>
    public class CheckedEventArgs
    {
        /// <summary>
        /// Gets if the Checkbox is checked or not
        /// </summary>
        public bool Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of CheckedEventArgs
        /// </summary>
        /// <param name="value">The boolean value to indicate if the Checkbox is checked or not.</param>
        public CheckedEventArgs(bool value)
        {
            Value = value;
        }
    }
}
