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
using Clock.Interfaces;
using Clock.Styles;
using Clock.Timer;
using System;
using Xamarin.Forms;

namespace Clock.Common
{
    /// <summary>
    /// The UI page shown when alarm or timer rings
    /// </summary>
    public partial class RingPage : ContentPage
    {
        RingType type_;
        bool SnoozeOn = false;

        private RelativeLayout rLayout;
        private AbsoluteLayout swipeAreaLayout;
        private SwipeImage dismissImage;
        private SwipeImage snoozeImage;
        private Image ringImage;
        private Image snoozeRingImage;
        private Image snoozeBackgroundRingImage;

        private CounterView counterview;
        private RelativeLayout counterLayout;
        private Label hLabel, mLabel, sLabel;
        private Label _mLabel, _sLabel;
        private Image minusImage, _minusImage;
        private Image backgroundRingImage;

        /// <summary>
        /// Create StackLayout for RingPage UI
        /// </summary>
        /// <param name="type">RingType</param>
        /// <returns>StackLayout</returns>
        public StackLayout CreateRingPage(RingType type)
        {
            type_ = type;
            rLayout = new RelativeLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            if (type == RingType.RING_TYPE_ALARM)
            {
                CreateAlarmInfo();
                // Tone type (Phase 3)
                // Alarm Type
                if (_alarmRecord.AlarmType == Alarm.AlarmTypes.Sound)
                {
                    DependencyService.Get<IAlarm>().PlaySound((float)_alarmRecord.Volume, _alarmRecord.AlarmToneType);
                }
                else if (_alarmRecord.AlarmType == Alarm.AlarmTypes.Vibration)
                {
                    DependencyService.Get<IAlarm>().StartVibration();
                }
                else if (_alarmRecord.AlarmType == Alarm.AlarmTypes.SoundVibration)
                {
                    DependencyService.Get<IAlarm>().PlaySound((float)_alarmRecord.Volume, _alarmRecord.AlarmToneType);
                    DependencyService.Get<IAlarm>().StartVibration();
                }

                swipeAreaLayout = new AbsoluteLayout
                {
                    HeightRequest = 438,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.White,
                };
                if (SnoozeOn)
                {
                    CreateSnoozeAlarmOnOffArea();
                }
                else
                {
                    CreateAlarmOnOffArea();
                }
            }
            else
            {
                CreateRingLabels();
                swipeAreaLayout = new AbsoluteLayout
                {
                    HeightRequest = 438,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.White,
                };
                CreateTimerOnOffArea();
            }

            StackLayout mainView = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 0,
                Children =
                {
                    rLayout,
                    swipeAreaLayout
                }
            };

            return mainView;
        }

        /// <summary>
        /// RingPage UI for Alarm
        /// Alarm Name, AM/PM label, Date & Time label
        /// </summary>
        private void CreateAlarmInfo()
        {
            Label alarmTitleLabel = new Label
            {
                Style = Styles.AlarmStyle.ATO006,
                HeightRequest = 69,
                Text = _alarmRecord.AlarmName.Equals("") ? "Alarm" : _alarmRecord.AlarmName,
                VerticalTextAlignment = TextAlignment.Center,
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(alarmTitleLabel, FontWeight.Light);

            Func<RelativeLayout, double> getAlarmTitleLabelWidth = (p) => alarmTitleLabel.Measure(rLayout.Width, rLayout.Height).Request.Width;
            rLayout.Children.Add(alarmTitleLabel,
              Constraint.RelativeToParent((parent) => { return parent.Width / 2 - getAlarmTitleLabelWidth(parent) / 2; }),
              Constraint.RelativeToParent((parent) => { return 120; }));

            Label amLabel = new Label
            {
                Style = Styles.AlarmStyle.ATO006,
                HeightRequest = 69,
                Text = _alarmRecord.ScheduledDateTime.ToString("tt"),
                VerticalTextAlignment = TextAlignment.Center,
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(amLabel, FontWeight.Light);

            Func<RelativeLayout, double> getAmLabelWidth = (p) => amLabel.Measure(rLayout.Width, rLayout.Height).Request.Width;
            rLayout.Children.Add(amLabel,
              Constraint.RelativeToParent((parent) => { return parent.Width / 2 - getAmLabelWidth(parent) / 2; }),
              Constraint.RelativeToParent((parent) => { return 120 + 69 + 100; }));

            Label timeLabel = new Label
            {
                Style = Styles.AlarmStyle.ATO007,
                HeightRequest = 230,
                Text = ((App)Application.Current).Is24hourFormat ?
                    _alarmRecord.ScheduledDateTime.ToString("HH:mm") :
                    _alarmRecord.ScheduledDateTime.ToString("hh:mm"),
                VerticalTextAlignment = TextAlignment.Center,
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(timeLabel, FontWeight.Thin);

            Func<RelativeLayout, double> getTimeLabelWidth = (p) => timeLabel.Measure(rLayout.Width, rLayout.Height).Request.Width;
            rLayout.Children.Add(timeLabel,
              Constraint.RelativeToParent((parent) => { return parent.Width / 2 - getTimeLabelWidth(parent) / 2; }),
              Constraint.RelativeToParent((parent) => { return 120 + 69 + 127; }));

            Label dateLabel = new Label
            {
                Style = Styles.AlarmStyle.ATO008,
                HeightRequest = 64,
                Text = _alarmRecord.ScheduledDateTime.ToString("ddd, d MMMM"),
                VerticalTextAlignment = TextAlignment.Center,
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(dateLabel, FontWeight.Light);

            Func<RelativeLayout, double> getDateLabelWidth = (p) => dateLabel.Measure(rLayout.Width, rLayout.Height).Request.Width;
            rLayout.Children.Add(dateLabel,
              Constraint.RelativeToParent((parent) => { return parent.Width / 2 - getDateLabelWidth(parent) / 2; }),
              Constraint.RelativeToParent((parent) => { return 120 + 69 + 127 + 230; }));
        }

        /// <summary>
        /// RingPage UI for Timer
        /// title label, Hour/Minute/Second labels
        /// </summary>
        private void CreateRingLabels()
        {
            Label titleLabel = new Label
            {
                Text = "Time is up",
                Style = TimerStyle.ATO019,
                HeightRequest = 58,
                WidthRequest = 400,
                //BackgroundColor = Color.Orange,
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(titleLabel, FontWeight.Light);
            rLayout.Children.Add(titleLabel,
              Constraint.RelativeToParent((parent) => { return 160; }),
              Constraint.RelativeToParent((parent) => { return 241 - 58; }));

            // case: display H/M/S 
            hLabel = new Label
            {
                Text = "Hours",
                Style = TimerStyle.ATO009,
                IsVisible = false,
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(hLabel, FontWeight.Normal);
            rLayout.Children.Add(hLabel,
              Constraint.RelativeToParent((parent) => { return 50 + 18; }),
              Constraint.RelativeToParent((parent) => { return 241 + 157; }));

            mLabel = new Label
            {
                Text = "Minutes",
                Style = TimerStyle.ATO009,
                IsVisible = false,
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(mLabel, FontWeight.Normal);
            rLayout.Children.Add(mLabel,
              Constraint.RelativeToParent((parent) => { return 360 - 70; }),
              Constraint.RelativeToParent((parent) => { return 241 + 157; }));

            sLabel = new Label
            {
                Text = "Seconds",
                Style = TimerStyle.ATO009,
                IsVisible = false,
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(sLabel, FontWeight.Normal);
            rLayout.Children.Add(sLabel,
              Constraint.RelativeToParent((parent) => { return 720 - 50 - 18 - 140; }),
              Constraint.RelativeToParent((parent) => { return 241 + 157; }));

            //case: display M/S 
            _mLabel = new Label
            {
                Text = "Minutes",
                Style = TimerStyle.ATO009,
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(_mLabel, FontWeight.Normal);
            rLayout.Children.Add(_mLabel,
              Constraint.RelativeToParent((parent) => { return 360 - 120 - 50; }),
              Constraint.RelativeToParent((parent) => { return 241 + 157; }));

            _sLabel = new Label
            {
                Text = "Seconds",
                Style = TimerStyle.ATO009,
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(_sLabel, FontWeight.Normal);
            rLayout.Children.Add(_sLabel,
              Constraint.RelativeToParent((parent) => { return 360 + 50; }),
              Constraint.RelativeToParent((parent) => { return 241 + 157; }));

            counterview = new CounterView(CounterType.COUNTER_TYPE_TIMER);
            counterLayout = counterview.GetCounterLayout();
            //counterLayout.BackgroundColor = Color.Lime;
            counterview.DisplayTime("00:00");
            rLayout.Children.Add(counterLayout,
              Constraint.RelativeToParent((parent) => { return 50; }),
              Constraint.RelativeToParent((parent) => { return 241 + 157 + 230 - 204; }));

            minusImage = new Image
            {
                Source = "timer/timer_ringing_minus.png",
                WidthRequest = 36,
                HeightRequest = 204,
                VerticalOptions = LayoutOptions.Center,
                //BackgroundColor = Color.FromHex("#FFFAFAFA"),
                IsVisible = false,
            };

            rLayout.Children.Add(minusImage,
              Constraint.RelativeToParent((parent) => { return 50 - 36; }),
              Constraint.RelativeToParent((parent) => { return 241 + 157 + 230 - 204; }));

            _minusImage = new Image
            {
                Source = "timer/timer_ringing_minus.png",
                WidthRequest = 36,
                HeightRequest = 204,
                VerticalOptions = LayoutOptions.Center,
                //BackgroundColor = Color.FromHex("FFFAFAFA"),
            };

            rLayout.Children.Add(_minusImage,
              Constraint.RelativeToParent((parent) => { return 360 - 120 - 50 - 18 - 8 - 36; }),
              Constraint.RelativeToParent((parent) => { return 241 + 157 + 230 - 204; }));
        }

        /// <summary>
        /// Create dismiss image
        /// </summary>
        private void CreateTimerOnOffArea()
        {
            dismissImage = new SwipeImage
            {
                OriginalSource = "ring/alarm_btn_bg_dismiss.png",
                WidthRequest = 180,
                HeightRequest = 180,
                TapStartCommand = TimerTapStartCommand,
            };

            ringImage = new Image
            {
                Source = "ring/alarm_btn_circle_line_AO003P.png",
                WidthRequest = 180,
                HeightRequest = 180,
                IsVisible = false,
            };

            backgroundRingImage = new Image
            {
                Source = "ring/alarm_btn_circle_drag_A3.png",
                WidthRequest = 180,
                HeightRequest = 180,
                IsVisible = false,
            };

            AbsoluteLayout.SetLayoutBounds(dismissImage, dismissOnlyRec);
            AbsoluteLayout.SetLayoutFlags(dismissImage, AbsoluteLayoutFlags.None);
            swipeAreaLayout.Children.Add(dismissImage);
            AbsoluteLayout.SetLayoutBounds(ringImage, dismissOnlyRec);
            AbsoluteLayout.SetLayoutFlags(ringImage, AbsoluteLayoutFlags.None);
            swipeAreaLayout.Children.Add(ringImage);
            AbsoluteLayout.SetLayoutBounds(backgroundRingImage, dismissOnlyRec);
            AbsoluteLayout.SetLayoutFlags(backgroundRingImage, AbsoluteLayoutFlags.None);
            swipeAreaLayout.Children.Add(backgroundRingImage);
        }

        /// <summary>
        /// Create dismiss image for Alarm Ring type
        /// </summary>
        private void CreateAlarmOnOffArea()
        {
            dismissImage = new SwipeImage
            {
                OriginalSource = "ring/alarm_btn_bg_dismiss.png",
                WidthRequest = 180,
                HeightRequest = 180,
                TapStartCommand = AlarmTapStartCommand,
            };

            ringImage = new Image
            {
                Source = "ring/alarm_btn_circle_line_AO003P.png",
                WidthRequest = 180,
                HeightRequest = 180,
                IsVisible = false,
            };

            backgroundRingImage = new Image
            {
                Source = "ring/alarm_btn_circle_drag_A3.png",
                WidthRequest = 180,
                HeightRequest = 180,
                IsVisible = false,
            };

            AbsoluteLayout.SetLayoutBounds(dismissImage, dismissOnlyRec);
            AbsoluteLayout.SetLayoutFlags(dismissImage, AbsoluteLayoutFlags.None);
            swipeAreaLayout.Children.Add(dismissImage);
            AbsoluteLayout.SetLayoutBounds(ringImage, dismissOnlyRec);
            AbsoluteLayout.SetLayoutFlags(ringImage, AbsoluteLayoutFlags.None);
            swipeAreaLayout.Children.Add(ringImage);
            AbsoluteLayout.SetLayoutBounds(backgroundRingImage, dismissOnlyRec);
            AbsoluteLayout.SetLayoutFlags(backgroundRingImage, AbsoluteLayoutFlags.None);
            swipeAreaLayout.Children.Add(backgroundRingImage);
        }

        /// <summary>
        /// Create dismiss/snooze image for Alarm Ring type
        /// </summary>
        private void CreateSnoozeAlarmOnOffArea()
        {
            // Alarm Image
            dismissImage = new SwipeImage
            {
                OriginalSource = "ring/alarm_btn_bg_dismiss.png",
                WidthRequest = 180,
                HeightRequest = 180,
                TapStartCommand = AlarmTapStartCommand,
            };

            ringImage = new Image
            {
                Source = "ring/alarm_btn_circle_line_AO003P.png",
                WidthRequest = 180,
                HeightRequest = 180,
                IsVisible = false,
            };

            backgroundRingImage = new Image
            {
                Source = "ring/alarm_btn_circle_drag_A3.png",
                WidthRequest = 180,
                HeightRequest = 180,
                IsVisible = false,
            };

            AbsoluteLayout.SetLayoutBounds(dismissImage, dismissWithSnoozeRec);
            AbsoluteLayout.SetLayoutFlags(dismissImage, AbsoluteLayoutFlags.None);
            swipeAreaLayout.Children.Add(dismissImage);
            AbsoluteLayout.SetLayoutBounds(ringImage, dismissWithSnoozeRec);
            AbsoluteLayout.SetLayoutFlags(ringImage, AbsoluteLayoutFlags.None);
            swipeAreaLayout.Children.Add(ringImage);
            AbsoluteLayout.SetLayoutBounds(backgroundRingImage, dismissWithSnoozeRec);
            AbsoluteLayout.SetLayoutFlags(backgroundRingImage, AbsoluteLayoutFlags.None);
            swipeAreaLayout.Children.Add(backgroundRingImage);

            // Snooze Image
            snoozeImage = new SwipeImage
            {
                OriginalSource = "ring/alarm_btn_circle_snooze_AO004.png",
                WidthRequest = 180,
                HeightRequest = 180,
                TapStartCommand = SnoozeTapStartCommand,
            };

            snoozeRingImage = new Image
            {
                Source = "ring/alarm_btn_circle_line_AO004P.png",
                WidthRequest = 180,
                HeightRequest = 180,
                IsVisible = false,
            };

            snoozeBackgroundRingImage = new Image
            {
                Source = "ring/alarm_btn_circle_drag_A4.png",
                WidthRequest = 180,
                HeightRequest = 180,
                IsVisible = false,
            };

            AbsoluteLayout.SetLayoutBounds(snoozeImage, snoozeRec);
            AbsoluteLayout.SetLayoutFlags(snoozeImage, AbsoluteLayoutFlags.None);
            swipeAreaLayout.Children.Add(snoozeImage);
            AbsoluteLayout.SetLayoutBounds(snoozeRingImage, snoozeRec);
            AbsoluteLayout.SetLayoutFlags(snoozeRingImage, AbsoluteLayoutFlags.None);
            swipeAreaLayout.Children.Add(snoozeRingImage);
            AbsoluteLayout.SetLayoutBounds(snoozeBackgroundRingImage, snoozeRec);
            AbsoluteLayout.SetLayoutFlags(snoozeBackgroundRingImage, AbsoluteLayoutFlags.None);
            swipeAreaLayout.Children.Add(snoozeBackgroundRingImage);
        }
    }
}
