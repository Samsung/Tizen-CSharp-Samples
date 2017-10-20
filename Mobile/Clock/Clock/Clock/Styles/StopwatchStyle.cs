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
    /// The style for label used in stopwatch page
    /// </summary>
    class StopwatchStyle
    {
        internal static Style ATO011 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(170) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FFFAFAFA") },
                new Setter { Property = Label.WidthRequestProperty, Value = 392 },
                new Setter { Property = Label.HeightRequestProperty, Value = 227 },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
                //new Setter { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Center },
                //new Setter { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.End },
                new Setter { Property = Label.TextProperty, Value = "00:00" },
                //new Setter { Property = Label.BackgroundColorProperty, Value = Color.Pink },
            }
        };

        internal static Style ATO012 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(108) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FFFAFAFA") },
                new Setter { Property = Label.WidthRequestProperty, Value = 130 },
                new Setter { Property = Label.HeightRequestProperty, Value = 145 },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
                //new Setter { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Center },
                //new Setter { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.End },
                new Setter { Property = Label.TextProperty, Value = ".00" },
                //new Setter { Property = Label.BackgroundColorProperty, Value = Color.Aqua },
            }
        };

        internal static Style ATO011L = new Style(typeof(Label))
        {
            BasedOn = ATO011,
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(150) },
                new Setter { Property = Label.WidthRequestProperty, Value = 532 },
                new Setter { Property = Label.HeightRequestProperty, Value = 200 },
            }
        };

        internal static Style ATO012L = new Style(typeof(Label))
        {
            BasedOn = ATO012,
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(88) },
                new Setter { Property = Label.WidthRequestProperty, Value = 146 - 36 },
                new Setter { Property = Label.HeightRequestProperty, Value = 117 },
            }
        };

        internal static Style ATO013 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(42) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FF000000") },
            }
        };

        internal static Style ATO014 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(42) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FF000000") },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.End },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
            }
        };

        internal static Style ATO015 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(42) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FF3DB9CC") },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.End },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
            }
        };
    }
}
