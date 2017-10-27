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

using Clock.Controls;
using Clock.Styles;
using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// AlarmEditName class.
    /// This class defines layout of alarm name row
    /// </summary>
    public class AlarmEditName : RelativeLayout
    {
        /// <summary>
        /// Main label
        /// </summary>
        private Label mainLabel;

        /// <summary>
        /// Entry to get alarm name from users. If there is a name already set by users, then 
        /// this name should be set as placeholder.
        /// If no placeholder is set then, it shows "Alarm"
        /// </summary>
        internal Entry mainEntry;

        /// <summary>
        /// Construct alarm name row UI
        /// </summary>
        /// <param name="recod">AlarmRecord</param>
        /// <seealso cref="AlarmRecord">
        public AlarmEditName(AlarmRecord recod)
        {
            /// Fills horizontally
            HorizontalOptions = LayoutOptions.FillAndExpand;
            /// Fills vertically
            VerticalOptions = LayoutOptions.Start;
            HeightRequest = 198;

            if (mainLabel == null)
            {
                mainLabel = new Label
                {
                    WidthRequest = 720 - 32 * 2,
                    HeightRequest = 54,
                    Text = "Alarm Name",
                    Style = AlarmStyle.T023,
                };
                // to meet To meet thin attribute for font, need to use custom feature
                FontFormat.SetFontWeight(mainLabel, FontWeight.Light);
            }

            /// Adds main label
            Children.Add(mainLabel,
                Constraint.RelativeToParent((parent) => { return 32; }),
                Constraint.RelativeToParent((parent) => { return 24; }));
            /// Creates entry
            mainEntry = new Entry()
            {
                WidthRequest = 720 - (32 + 10) * 2,
                HeightRequest = 54,
                Text = recod.AlarmName,
            };
            mainEntry.SetBinding(Entry.TextProperty, new Binding("AlarmName", BindingMode.Default, source: recod));

            /// Checks whether already set alarm name
            if (string.IsNullOrEmpty(AlarmModel.BindableAlarmRecord.AlarmName))
            {
                mainEntry.Placeholder = "Alarm";
            }
            else
            {
                mainEntry.Text = AlarmModel.BindableAlarmRecord.AlarmName;
            }

            /// Sets font size (DP base)
            mainEntry.FontSize = CommonStyle.GetDp(40);
            /// Adds to layout
            Children.Add(mainEntry,
                Constraint.RelativeToView(mainLabel, (parent, sibling) => { return (sibling.X + 10); }),
                Constraint.RelativeToView(mainLabel, (parent, sibling) => { return (sibling.Y + sibling.Height + 33); }));
        }
    }
}
