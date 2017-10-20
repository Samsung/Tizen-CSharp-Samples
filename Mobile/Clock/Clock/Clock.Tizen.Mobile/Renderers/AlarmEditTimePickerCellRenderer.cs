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

using Clock.Alarm;
using Clock.Tizen.Mobile.Renderers;
using ElmSharp;
using System;
using System.Globalization;
using TimePicker = Xamarin.Forms.Platform.Tizen.Native.TimePicker;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportCell(typeof(AlarmEditTimePickerCell), typeof(AlarmEditTimePickerCellRenderer))]

namespace Clock.Tizen.Mobile.Renderers
{
    /// <summary>
    /// The renderer for AlarmTimePicker
    /// </summary>
    class AlarmEditTimePickerCellRenderer : CellRenderer
    {
        static readonly string s_defaultFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;

        /// <summary>
        /// Constructor
        /// </summary>
        public AlarmEditTimePickerCellRenderer() : base("full")
        {
            MainContentPart = "elm.swallow.content";
        }

        protected string MainContentPart { get; set; }

        /// <summary>
        /// Return EvasObject which consists of a cell
        /// </summary>
        /// <param name="cell">Cell</param>
        /// <param name="part">Cell's style name</param>
        /// <returns>EvasObject</returns>
        protected override EvasObject OnGetContent(Cell cell, string part)
        {
            if (part == MainContentPart)
            {
                var pickerCell = (AlarmEditTimePickerCell)cell;
                return CreateReusableContent(pickerCell);
            }

            return null;
        }

        EvasObject CreateReusableContent(AlarmEditTimePickerCell viewCell)
        {
            TimePicker _timePicker = new TimePicker(Forms.Context.MainWindow)
            {
                Time = DateTime.Now.TimeOfDay,
                //DateTimeFormat = s_defaultFormat,
                // Added temporarily because of Tizen EFL limitation
            };
            // According to date time format, UI components are decided.
            if (((App)Application.Current).Is24hourFormat)
            {
                _timePicker.Format = "%d/%b/%Y %I:%M";
            }
            else
            {
                _timePicker.Format = "%d/%b/%Y %I:%M %p";
            }

            _timePicker.DateTimeChanged += (sender, e) =>
            {
                // DateTime can be converted to TimeSpan via .TimeOfDay
                DateTime dt = e.NewDate;
                // update Native wrapper and local variable
                _timePicker.Time = dt.TimeOfDay;
                viewCell.Time = _timePicker.Time;
            };

            return _timePicker;
        }

        /// <summary>
        /// Return the reusable EvasObject which would be shown in a cell.
        /// </summary>
        /// <param name="cell">Cell</param>
        /// <param name="part">Cell's style name</param>
        /// <param name="old">EvasObject which has been already added in Cell</param>
        /// <returns>EvasObject</returns>
        protected override EvasObject OnReusableContent(Cell cell, string part, EvasObject old)
        {
            ((TimePicker)old).Time = ((AlarmEditTimePickerCell)cell).Time;
            return old;
        }
    }
}
