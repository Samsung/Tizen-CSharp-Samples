/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
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

using Alarm.Converters;
using Alarm.Models;
using System;
using Tizen.Wearable.CircularUI;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;


namespace Alarm.Views
{
    /// <summary>
    /// This class defines list cell for Alarm.
    /// This inherits from ViewCell which provides custom UI for a ListView
    /// It is commonly used for any cell for this ListView
    /// </summary>
    public class AlarmListCell : ViewCell
    {

        public StackLayout alarmItemLayout;

        /// <summary>
        /// time label for the alarm list
        /// </summary>
        public Label timeLabel;

        /// <summary>
        /// Active/Inactive switch object
        /// </summary>
        public Switch switchObj;

        /// <summary>
        /// Draws alarm list
        /// </summary>
        /// <returns>Returns RelativeLayout</returns>
        protected virtual StackLayout Draw()
        {

            /// Need to get bindable context to assign list value
            AlarmRecord alarmData = (AlarmRecord)BindingContext;

            /// If binding context is null, can't proceed further action
            if (alarmData == null)
            {
                return null;
            }


            /// Alarm item layout should be set if null
            if (alarmItemLayout == null)
            {
                // The layout of item cell
                alarmItemLayout = new StackLayout
                {
                    HeightRequest = 120,
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };

                // Time Label
                timeLabel = new Label()
                {
                    WidthRequest = 280,
                    FontSize = 12,
                    TextColor = Color.White,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                };

                timeLabel.SetBinding(Label.TextProperty, new Binding("ScheduledDateTime", BindingMode.Default, new ScheduledDateTimeToTextConverter()));
                alarmItemLayout.Children.Add(timeLabel);

                switchObj = new Check
                {
                    HeightRequest = 80,
                    WidthRequest = 80,
                    DisplayStyle = CheckDisplayStyle.Default,
                    IsToggled = alarmData.AlarmState == AlarmStates.Inactive ? false : true,
                    HorizontalOptions = LayoutOptions.End,
                    VerticalOptions = LayoutOptions.Center,
                };

                alarmItemLayout.Children.Add(switchObj);

                /// Adds an event
                switchObj.Toggled += (s, e) =>
                {
                    AlarmRecord am = (AlarmRecord)BindingContext;
                    /// Modify state and re-draw it. Redraw must be called to redraw
                    if (e.Value)
                    {
                        AlarmModel.ReactivatelAlarm(am);
                    }
                    else
                    {
                        AlarmModel.DeactivatelAlarm(am);
                    }
                };
            }

            return alarmItemLayout;
        }

        /// <summary>
        /// When binding context is changed, need to redraw
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext == null)
            {
                return;
            }
            else
            {
                View = Draw();
            }
        }
    }
}
