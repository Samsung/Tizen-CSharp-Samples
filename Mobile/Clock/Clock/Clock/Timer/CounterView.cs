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
using Clock.Styles;
using Xamarin.Forms;

namespace Clock.Timer
{
    public enum CounterType
    {
        COUNTER_TYPE_STOPWATCH,
        COUNTER_TYPE_TIMER
    }

    //start_time;
    public class CounterView
    {
        RelativeLayout layout_;
        Label hmsLabel_;
        Label msLabel_;

        CounterType type_;

        static Thickness hmsLabelMargin = new Thickness(96, 305, 0, 0);
        static Thickness hmsLabelExpandedMargin = new Thickness(36, 326, 0, 0);
        static Thickness hmsLabelMarginWithList = new Thickness(96, 126, 0, 0);
        static Thickness hmsLabelExpandedMarginWithList = new Thickness(36, 126, 0, 0);

        static Thickness msLabelMargin = new Thickness(488 + 6, 305 + 65, 0, 0);
        static Thickness msLabelExpandedMargin = new Thickness(568 + 6, 326 + 66, 0, 0);
        static Thickness msLabelMarginWithList = new Thickness(488 + 6, 191, 0, 0);
        static Thickness msLabelExpandedMarginWithList = new Thickness(568 + 6, 191, 0, 0);

        Style stopwatchLayoutStyle = new Style(typeof(RelativeLayout))
        {
            Setters =
            {
                new Setter { Property = RelativeLayout.HorizontalOptionsProperty, Value = LayoutOptions.FillAndExpand },
                new Setter { Property = RelativeLayout.VerticalOptionsProperty, Value = LayoutOptions.FillAndExpand },
                //new Setter { Property = RelativeLayout.BackgroundColorProperty, Value = Color.Silver },
            }
        };

        // label  :  00:00
        Style stopwatchHMSLabelStyle = new Style(typeof(Label))
        {
            BasedOn = StopwatchStyle.ATO011,
            Setters =
            {
                new Setter { Property = Label.MarginProperty, Value = hmsLabelMargin },
            }
        };
        Style stopwatchHMSLabelExpandStyle = new Style(typeof(Label))
        {
            BasedOn = StopwatchStyle.ATO011L,
            Setters =
            {
                new Setter { Property = Label.MarginProperty, Value = hmsLabelExpandedMargin },
            }
        };
        Style stopwatchHMSLabelStyleWithList = new Style(typeof(Label))
        {
            BasedOn = StopwatchStyle.ATO011,
            Setters =
            {
                new Setter { Property = Label.MarginProperty, Value = hmsLabelMarginWithList },
            }
        };
        Style stopwatchHMSLabelExpandStyleWithList = new Style(typeof(Label))
        {
            BasedOn = StopwatchStyle.ATO011L,
            Setters =
            {
                new Setter { Property = Label.MarginProperty, Value = hmsLabelExpandedMarginWithList },
            }
        };

        // label  :  .00
        Style stopwatchMSLabelStyle = new Style(typeof(Label))
        {
            BasedOn = StopwatchStyle.ATO012,
            Setters =
            {
                new Setter { Property = Label.MarginProperty, Value = msLabelMargin },
            }
        };
        Style stopwatchMSLabelExpandStyle = new Style(typeof(Label))
        {
            BasedOn = StopwatchStyle.ATO012L,
            Setters =
            {
                new Setter { Property = Label.MarginProperty, Value = msLabelExpandedMargin },
            }
        };
        Style stopwatchMSLabelStyleWithList = new Style(typeof(Label))
        {
            BasedOn = StopwatchStyle.ATO012,
            Setters =
            {
                new Setter { Property = Label.MarginProperty, Value = msLabelMarginWithList },
            }
        };
        Style stopwatchMSLabelExpandStyleWithList = new Style(typeof(Label))
        {
            BasedOn = StopwatchStyle.ATO012L,
            Setters =
            {
                new Setter { Property = Label.MarginProperty, Value = msLabelExpandedMarginWithList },
            }
        };

        // CounterView for Timer Page
        Style timerLayoutStyle = new Style(typeof(RelativeLayout))
        {
            Setters =
            {
                new Setter { Property = RelativeLayout.WidthRequestProperty, Value = 620 },
                new Setter { Property = RelativeLayout.HeightRequestProperty, Value = 204 },
            }
        };

        Style timerLabelStyle = new Style(typeof(Label))
        {
            BasedOn = TimerStyle.ATO010,
            Setters =
            {
                new Setter { Property = Label.WidthRequestProperty, Value = 620 },
                new Setter { Property = Label.HeightRequestProperty, Value = 204 },
                new Setter { Property = Label.TextProperty, Value = "00:00:00" },
            }
        };

        public CounterView(/*StackLayout parent, */CounterType type)
        {
            type_ = type;

            if (type == CounterType.COUNTER_TYPE_STOPWATCH)
            {
                layout_ = new RelativeLayout
                {
                    Style = stopwatchLayoutStyle,
                };
                hmsLabel_ = new Label
                {
                    Style = stopwatchHMSLabelStyle,
                };
                // to meet To meet thin attribute for font, need to use custom feature
                FontFormat.SetFontWeight(hmsLabel_, FontWeight.Thin);

                msLabel_ = new Label
                {
                    Style = stopwatchMSLabelStyle,
                };
                // to meet To meet thin attribute for font, need to use custom feature
                FontFormat.SetFontWeight(msLabel_, FontWeight.Thin);
            }
            else
            {
                layout_ = new RelativeLayout
                {
                    Style = timerLayoutStyle,
                };
                hmsLabel_ = new Label
                {
                    Style = timerLabelStyle,
                };
                // to meet To meet thin attribute for font, need to use custom feature
                FontFormat.SetFontWeight(hmsLabel_, FontWeight.Thin);
            }

            AddChildren();
        }

        public RelativeLayout GetCounterLayout()
        {
            return layout_;
        }

        public void SetTime(string hms)
        {
            if (type_ == CounterType.COUNTER_TYPE_STOPWATCH)
            {
                return;
            }
        }

        public void SetTime(string hms, string ms, bool expand, bool change, bool enabledLap = false)
        {
            if (type_ == CounterType.COUNTER_TYPE_TIMER)
            {
                return;
            }

            if (enabledLap)
            {
                if (expand)
                {
                    hmsLabel_.Style = stopwatchHMSLabelExpandStyleWithList;
                    msLabel_.Style = stopwatchMSLabelExpandStyleWithList;
                }
                else
                {
                    hmsLabel_.Style = stopwatchHMSLabelStyleWithList;
                    msLabel_.Style = stopwatchMSLabelStyleWithList;
                }
            }
            else
            {
                if (expand)
                {
                    hmsLabel_.Style = stopwatchHMSLabelExpandStyle;
                    msLabel_.Style = stopwatchMSLabelExpandStyle;
                }
                else
                {
                    hmsLabel_.Style = stopwatchHMSLabelStyle;
                    msLabel_.Style = stopwatchMSLabelStyle;
                }
            }

            hmsLabel_.Text = hms;
            msLabel_.Text = ms;

            if (change)
            {
                layout_.Children.Clear();
                layout_.Children.Add(hmsLabel_,
                    Constraint.RelativeToParent((parent) => { return 0; }),
                    Constraint.RelativeToParent((parent) => { return 0; }));
                layout_.Children.Add(msLabel_,
                    Constraint.RelativeToParent((parent) => { return 0; }),
                    Constraint.RelativeToParent((parent) => { return 0; }));
            }
        }

        public void DisplayTime(string hms)
        {
            if (type_ == CounterType.COUNTER_TYPE_STOPWATCH)
            {
                return;
            }

            hmsLabel_.Text = hms;
        }

        public void AddChildren()
        {
            if (type_ == CounterType.COUNTER_TYPE_STOPWATCH)
            {
                layout_.Children.Add(hmsLabel_,
                    Constraint.RelativeToParent((parent) => { return 0; }),
                    Constraint.RelativeToParent((parent) => { return 0; }));

                layout_.Children.Add(msLabel_,
                    Constraint.RelativeToParent((parent) => { return 0; }),
                    Constraint.RelativeToParent((parent) => { return 0; }));
            }
            else
            {
                layout_.Children.Add(hmsLabel_,
                    Constraint.RelativeToParent((parent) => { return 0; }),
                    Constraint.RelativeToParent((parent) => { return 0; }));
            }
        }

        public void SetMargin(bool expand, bool lapListEnabled)
        {
            if (lapListEnabled)
            {
                if (expand)
                {
                    hmsLabel_.Margin = hmsLabelExpandedMarginWithList;
                    msLabel_.Margin = msLabelExpandedMarginWithList;
                }
                else
                {
                    hmsLabel_.Margin = hmsLabelMarginWithList;
                    msLabel_.Margin = msLabelMarginWithList;
                }
            }
            else
            {
                if (expand)
                {
                    hmsLabel_.Margin = hmsLabelExpandedMargin;
                    msLabel_.Margin = msLabelExpandedMargin;
                }
                else
                {
                    hmsLabel_.Margin = hmsLabelMargin;
                    msLabel_.Margin = msLabelMargin;
                }
            }
        }

        public void Reset()
        {
            // 00:00.00
            for (int i = 0; i < 3; i++)
            {
                SetDigitText("00", i);
            }
        }

        //public bool IsVisible(void);

        private bool SetDigitText(string text, int col)
        {
            // Set time..
            return true;
        }

    }
}
