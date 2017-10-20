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

using Clock.Controls;
using Clock.Converters;
using Clock.Styles;
using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// This class defines list cell for Alarm.
    /// This inherits from ViewCell which provides custom UI for a ListView
    /// It is commonly used for any cell for this ListView
    /// </summary>
    public class AlarmListCell : ViewCell
    {
        /// <summary>
        /// alarm item layout to layout ui components
        /// </summary>
        public RelativeLayout alarmItemLayout;

        /// <summary>
        /// time label for the alarm list
        /// </summary>
        public Label timeLabel;

        /// <summary>
        /// AM or PM label for alarm time
        /// </summary>
        public Label amPmLabel;

        /// <summary>
        /// Image to indicate repeat of this alarm
        /// </summary>
        public Image repeatImage;

        /// <summary>
        /// Alarm name by users
        /// </summary>
        public Label alamNameLabel;

        /// <summary>
        /// Weekdays (M T W Th F S S) label
        /// </summary>
        public Label weekDaysLabel;

        /// <summary>
        /// Active/Inactive switch object
        /// </summary>
        public Switch switchObj;

        /// <summary>
        /// Date Label
        /// Alarm name's existence and repeat week day decide date label's visibility.
        /// </summary>
        public Label dateLabel;

        /// <summary>
        /// Draws alarm list 
        /// </summary>
        /// <returns>Returns RelativeLayout</returns>
        protected virtual RelativeLayout Draw()
        {
            /// Need to get bindable context to assign list value
            AlarmRecord alarmData = (AlarmRecord)BindingContext;

            /// If binding context is null, can't proceed further action
            if (alarmData == null)
            {
                return null;
            }

            alarmData.PrintProperty();

            /// Alarm item layout should be set if null
            if (alarmItemLayout == null)
            {
                // The layout of item cell
                alarmItemLayout = new RelativeLayout
                {
                    HeightRequest = 22 + 93 + 29,
                };

                // Time Label
                timeLabel = new Label()
                {
                    Text = (((App)Application.Current).Is24hourFormat) ?
                        alarmData.ScheduledDateTime.ToString("HH:mm") : alarmData.ScheduledDateTime.ToString("hh:mm"),
                    Style = alarmData.AlarmState == AlarmStates.Inactive ? AlarmStyle.ATO001D : AlarmStyle.ATO001,
                };
                // to meet To meet thin attribute for font, need to use custom feature
                FontFormat.SetFontWeight(timeLabel, FontWeight.Light);
                /// Style set for time label for normal case
                timeLabel.SetBinding(Label.StyleProperty, new Binding("AlarmState", BindingMode.Default, new AlarmStateToPropertyConverter(), AlarmModelComponent.Time));
                /// Needs to set binding context for scheduled time
                timeLabel.SetBinding(Label.TextProperty, new Binding("ScheduledDateTime", BindingMode.Default, new ScheduledDateTimeToTextConverter(), LabelType.Time));
                // Added to layout
                alarmItemLayout.Children.Add(timeLabel,
                    Constraint.RelativeToParent((parent) =>
                    {
                        return 32;
                    }),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return 22;
                    }));

                // AM/PM Label
                amPmLabel = new Label()
                {
                    //the text of AM/PM label
                    Text = (((App)Application.Current).Is24hourFormat) ?
                        "" : alarmData.ScheduledDateTime.ToString("tt"),
                    Style = alarmData.AlarmState == AlarmStates.Inactive ? AlarmStyle.ATO002D : AlarmStyle.ATO002,
                };
                // to meet To meet thin attribute for font, need to use custom feature
                FontFormat.SetFontWeight(amPmLabel, FontWeight.Light);
                //amPmLabel.IsVisible = (((Tizen.App)Application.Current).Is24hourFormat) ? false : true;
                amPmLabel.SetBinding(Label.IsVisibleProperty, new Binding("AlarmDateFormat", BindingMode.Default, new DateFormatToVisibleConverter()));
                // Set style depending on alarm state
                amPmLabel.SetBinding(Label.StyleProperty, new Binding("AlarmState", BindingMode.Default, new AlarmStateToPropertyConverter(), AlarmModelComponent.AmPm));
                amPmLabel.SetBinding(Label.TextProperty, new Binding("ScheduledDateTime", BindingMode.Default, new ScheduledDateTimeToTextConverter(), LabelType.AmPm));
                // Added to layout
                alarmItemLayout.Children.Add(amPmLabel,
                    Constraint.RelativeToView(timeLabel, (parent, sibling) => sibling.X + sibling.Width + 10),
                    Constraint.RelativeToView(timeLabel, (parent, sibling) => sibling.Y + 36));

                // Repeat Image
                repeatImage = new Image
                {
                    Source = "alarm/clock_ic_repeat.png",
                    WidthRequest = 38,
                    HeightRequest = 38,
                };
                // Bind repeat Image's visibiliy to weekly repeating value
                repeatImage.SetBinding(Image.IsVisibleProperty, new Binding("Repeat", mode: BindingMode.Default));
                // Set repeat image's blending color on alarm state
                ImageAttributes.SetBlendColor(repeatImage, alarmData.AlarmState == AlarmStates.Inactive ? Color.FromHex("66000000") : Color.FromHex("FFFFFF"));
                repeatImage.SetBinding(ImageAttributes.BlendColorProperty, new Binding("AlarmState", BindingMode.OneWay, new AlarmStateToPropertyConverter(), AlarmModelComponent.Repeat));
                // Added to layout
                alarmItemLayout.Children.Add(repeatImage,
                        Constraint.RelativeToParent((parent) => (720 - 104 - 32 - 268)),
                        Constraint.RelativeToParent((parent) => (22 + 93) - (43 + 43)));

                /// Alarm Name Label
                alamNameLabel = new Label();
                /// For alarm name label, to meet To meet thin attribute for font, need to use custom feature
                FontFormat.SetFontWeight(alamNameLabel, FontWeight.Normal);
                /// Bind alarm lable's style to alarm state
                alamNameLabel.SetBinding(Label.StyleProperty, new Binding("AlarmState", BindingMode.OneWay, new AlarmStateToPropertyConverter(), AlarmModelComponent.Name));
                /// Bind label's text property to AlarmMode's AlarmName.
                alamNameLabel.SetBinding(Label.TextProperty, "AlarmName");
                // Update alarm name label's TranslationX property value according to repeat image's visibility
                alamNameLabel.SetBinding(Label.TranslationXProperty, new Binding("Repeat", BindingMode.OneWay, converter: new AlarmNameLabelPositionConverter()));
                // Bind alarm name label's visibility
                alamNameLabel.SetBinding(Label.IsVisibleProperty, new Binding("IsVisibleDateLabel", BindingMode.Default, new DateLabelVisibleToVisibility(), false));
                //alamNameLabel.Text = alarmData.AlarmName;
                // Added to relative layout
                alarmItemLayout.Children.Add(alamNameLabel,
                    Constraint.RelativeToParent((parent) => (720 - 104 - 32 - 268)),
                    Constraint.RelativeToParent((parent) => (22 + 93 - (43 + 43))));

                /// WeekDays Label
                weekDaysLabel = new Label()
                {
                    FormattedText = alarmData.GetFormatted(alarmData.WeekFlag, alarmData.AlarmState < AlarmStates.Inactive ? true : false),
                    IsVisible = !alarmData.IsVisibleDateLabel,
                    Style = alarmData.AlarmState == AlarmStates.Inactive ? AlarmStyle.ATO004D : AlarmStyle.ATO004,
                };
                // to meet To meet thin attribute for font, need to use custom feature
                FontFormat.SetFontWeight(weekDaysLabel, FontWeight.Normal);
                /// Style set for time label for normal case
                weekDaysLabel.SetBinding(Label.StyleProperty, new Binding("AlarmState", BindingMode.OneWay, new AlarmStateToPropertyConverter(), AlarmModelComponent.Weekly));
                /// Sets binding context for weekdays label
                weekDaysLabel.SetBinding(Label.FormattedTextProperty, new Binding("WeekdayRepeatText", BindingMode.Default));
                weekDaysLabel.SetBinding(Label.IsVisibleProperty, new Binding("IsVisibleDateLabel", BindingMode.Default, new DateLabelVisibleToVisibility(), false));
                /// Adds to relative layout
                alarmItemLayout.Children.Add(weekDaysLabel,
                    Constraint.RelativeToParent((parent) => (720 - 104 - 32 - 268)),
                    Constraint.RelativeToParent((parent) => (22 + 93) - 43));

                // Date label
                // DateLabel is only visible when the alarm name is empty and repeat weekly is Never.
                dateLabel = new Label
                {
                    Text = alarmData.ScheduledDateTime.ToString("ddd, d MMM"),
                };
                // to meet To meet thin attribute for font, need to use custom feature
                FontFormat.SetFontWeight(dateLabel, FontWeight.Normal);
                dateLabel.SetBinding(Label.IsVisibleProperty, new Binding("IsVisibleDateLabel", BindingMode.Default, new DateLabelVisibleToVisibility(), true));
                dateLabel.SetBinding(Label.StyleProperty, new Binding("AlarmState", BindingMode.Default, new AlarmStateToPropertyConverter(), AlarmModelComponent.Date));
                alarmItemLayout.Children.Add(dateLabel,
                    Constraint.RelativeToParent((parent) => (720 - 104 - 32 - 268)),
                    Constraint.RelativeToParent((parent) => 22));

                /// Switch object to represent that the alarm is active or not
                switchObj = new Switch
                {
                    HeightRequest = 72,
                    WidthRequest = 72,
                    IsToggled = alarmData.AlarmState == AlarmStates.Inactive ? false : true,
                };
                /// Bind IsToggled property to alarm state
                //switchObj.SetBinding(Switch.IsToggledProperty, new Binding("AlarmState", BindingMode.OneWay, new AlarmStateToPropertyConverter(), AlarmModelComponent.State));
                /// Adds to relative layout
                alarmItemLayout.Children.Add(switchObj,
                    Constraint.RelativeToParent((parent) =>
                    {
                        return (720 - 104);
                    }),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return (22 + 93 - 72);
                    }));

                /// Adds an event 
                switchObj.Toggled += (s, e) =>
                {
                    //Switch sObj = s as Switch;
                    ///// Needs valid parent to proceed
                    //if (sObj.Parent == null || sObj.Parent.Parent == null)
                    //{
                    //    return;
                    //}

                    ///// Need binding context to check state
                    //AlarmRecord am = (AlarmRecord)((AlarmListCell)sObj.Parent.Parent).BindingContext;
                    //if (am == null)
                    //{
                    //    return;
                    //}
                    AlarmRecord am = (AlarmRecord)BindingContext;
                    /// Modify state and re-draw it. Redraw must be called to redraw
                    //am.AlarmState = e.Value ? AlarmStates.Active : AlarmStates.Inactive;
                    if (e.Value)
                    {
                        AlarmModel.ReactivatelAlarm(am);
                    }
                    else
                    {
                        AlarmModel.DeactivatelAlarm(am);
                    }

                    AlarmModel.PrintAll("After switch is toggled...");
                };
            }
            else
            {
                switchObj.IsVisible = true;
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
