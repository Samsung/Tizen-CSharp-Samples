/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace RadioSample
{
    /// <summary>
    /// Represents a frequency value.
    /// </summary>
    public class Frequency
    {
        public Frequency(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the frequency value in kHz.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Gets the text to display on the list view in MHz.
        /// </summary>
        public string DisplayText => $"{Value / 1000.0} MHz";
    }
}
