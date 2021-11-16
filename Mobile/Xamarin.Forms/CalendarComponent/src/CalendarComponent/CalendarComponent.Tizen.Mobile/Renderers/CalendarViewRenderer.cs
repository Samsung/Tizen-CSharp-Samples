/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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

using CalendarComponent.Tizen.Mobile.Renderers;
using CalendarComponent.Tizen.Mobile.Components;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using ECalendar = ElmSharp.Calendar;

[assembly: ExportRenderer(typeof(CalendarView), typeof(CalendarViewRenderer))]

namespace CalendarComponent.Tizen.Mobile.Renderers
{
    /// <summary>
    /// CalendarView component's renderer class.
    /// </summary>
    public class CalendarViewRenderer : ViewRenderer<CalendarView, ECalendar>
    {
        #region methods

        /// <summary>
        /// Registers properties handlers.
        /// </summary>
        public CalendarViewRenderer()
        {
            RegisterPropertyHandler(CalendarView.MinimumDateProperty, UpdateMinimumDate);
            RegisterPropertyHandler(CalendarView.MaximumDateProperty, UpdateMaximumDate);
            RegisterPropertyHandler(CalendarView.FirstDayOfWeekProperty, UpdateFirstDayOfWeek);
            RegisterPropertyHandler(CalendarView.WeekDayNamesProperty, UpdateWeekDayNames);
            RegisterPropertyHandler(CalendarView.SelectedDateProperty, UpdateSelectedDate);
        }

        /// <summary>
        /// Handles control's change.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<CalendarView> e)
        {
            if (Control == null)
            {
                var calrendarView = new ECalendar(Forms.NativeParent);
                SetNativeControl(calrendarView);
            }

            if (Control != null)
            {
                if (e.OldElement != null)
                {
                    Control.DateChanged -= DateChangedHandler;
                }

                if (e.NewElement != null)
                {
                    Control.DateChanged += DateChangedHandler;
                }
            }

            base.OnElementChanged(e);
        }

        /// <summary>
        /// Sets new date for calendar model.
        /// </summary>
        /// <param name="sender">Event sender. Not used.</param>
        /// <param name="e">Event arguments.</param>
        void DateChangedHandler(object sender, ElmSharp.DateChangedEventArgs e)
        {
            ((IElementController)Element).SetValueFromRenderer(CalendarView.SelectedDateProperty, e.NewDate);
        }

        /// <summary>
        /// Updates lowest possible date value.
        /// </summary>
        void UpdateMinimumDate()
        {
            Control.MinimumYear = Element.MinimumDate.Year;
        }

        /// <summary>
        /// Updates highest possible date value.
        /// </summary>
        void UpdateMaximumDate()
        {
            Control.MaximumYear = Element.MaximumDate.Year;
        }

        /// <summary>
        /// Updates week's starting day.
        /// </summary>
        void UpdateFirstDayOfWeek()
        {
            Control.FirstDayOfWeek = Element.FirstDayOfWeek;
        }

        /// <summary>
        /// Updates days names.
        /// </summary>
        void UpdateWeekDayNames()
        {
            Control.WeekDayNames = Element.WeekDayNames;
        }

        /// <summary>
        /// Updates current date.
        /// </summary>
        void UpdateSelectedDate()
        {
            Control.SelectedDate = Element.SelectedDate;
        }

        #endregion
    }
}