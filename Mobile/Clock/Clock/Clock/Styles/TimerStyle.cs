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

namespace Clock.Styles
{
    /// <summary>
    /// The style for label/button/entry used in Timer page
    /// </summary>
    class TimerStyle
    {
        internal static Style ATO009 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(31) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("#F0FAFAFA") },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
                new Setter { Property = Label.HeightRequestProperty, Value = 230 - 204 },
                new Setter { Property = Label.WidthRequestProperty, Value =  140 },
            }
        };

        internal static Style ATO010 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(160) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("#FFFAFAFA") },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
            }
        };

        internal static Style ATO019 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(52) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("#FFFAFAFA") },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
            }
        };

        internal static Style ATO021L = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(154) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("#FFFAFAFA") },
            }
        };

        internal static Style timelabelStyle = new Style(typeof(Label))
        {
            BasedOn = TimerStyle.ATO009,
            Setters =
            {
                new Setter { Property = Label.WidthRequestProperty, Value = 146 },
                new Setter { Property = Label.HeightRequestProperty, Value = 42 },
            }
        };

        internal static Style timeSelectorEntryStyle = new Style(typeof(Entry))
        {
            Setters =
            {
                new Setter { Property = Entry.FontSizeProperty, Value = CommonStyle.GetDp(160) },
                new Setter { Property = Entry.TextColorProperty, Value = Color.FromHex("#FFFAFAFA") },
                new Setter { Property = Entry.HorizontalTextAlignmentProperty, Value = TextAlignment.Center },
                new Setter { Property = Entry.WidthRequestProperty, Value = 190 },
                new Setter { Property = Entry.HeightRequestProperty, Value = 204 },
            }
        };

        internal static Style timeSelectorLabelStyle = new Style(typeof(Label))
        {
            BasedOn = TimerStyle.ATO010,
            Setters =
            {
                new Setter { Property = Label.WidthRequestProperty, Value = 25 },
                new Setter { Property = Label.HeightRequestProperty, Value = 204 },
                new Setter { Property = Label.TextProperty, Value = ":" },
            }
        };

        internal static Style arrowUpButtonStyle = new Style(typeof(Button))
        {
            Setters =
            {
                new Setter { Property = Button.WidthRequestProperty, Value = 146 },
                new Setter { Property = Button.HeightRequestProperty, Value = 76 },
                new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromHex("#00FFFFFF") },
                new Setter { Property = Button.ImageProperty, Value = "timer/alarm_picker_arrow_up.png" },
            }
        };

        internal static Style arrowDownButtonStyle = new Style(typeof(Button))
        {
            Setters =
            {
                new Setter { Property = Button.WidthRequestProperty, Value = 146 },
                new Setter { Property = Button.HeightRequestProperty, Value = 76 },
                new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromHex("#00FFFFFF") },
                new Setter { Property = Button.ImageProperty, Value = "timer/alarm_picker_arrow_down.png" },
            }
        };
    }
}
