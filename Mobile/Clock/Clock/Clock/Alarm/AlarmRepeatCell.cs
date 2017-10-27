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

using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// AlarmRepeatCell class includes repeat row UI for alarm repeat setting page
    /// </summary>
    public class AlarmRepeatCell : ViewCell
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="weekFlag">The week day repeat setting (bit stream)</param>
        /// <seealso cref="AlarmWeekFlag">
        public AlarmRepeatCell(AlarmWeekFlag weekFlag)
        {
            View = new AlarmRepeatRow(weekFlag);
        }
    }
}
