/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
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

using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// Alarm type table view cell
    /// This class includes AlarmTypeRow which actual UI controls are defined
    /// </summary>
    public class AlarmTypeTableCell : ViewCell
    {
        /// <summary>
        /// Alarm type for this view cell
        /// </summary>
        AlarmTypes Type { get; set; }

        /// <summary>
        /// The page which contains this view cell
        /// </summary>
        ContentPage Container { get; set; }

        /// <summary>
        /// Constructor for this class
        /// Sets proper controls to this class
        /// </summary>
        /// <param name="type">The type of alarm type</param>
        /// <seealso cref="AlarmTypes">
        /// <param name="page">The content page which includes this table view cell</param>
        /// <seealso cref="ContentPage">
        public AlarmTypeTableCell(AlarmTypes type, ContentPage page)
        {
            Type = type;
            Container = page;
            View = new AlarmTypeRow(type);
        }
    }
}
