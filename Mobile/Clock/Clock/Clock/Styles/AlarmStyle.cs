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

using Xamarin.Forms;

namespace Clock.Styles
{
    /// <summary>
    /// The style for Label used in Alarm page
    /// </summary>
    class AlarmStyle
    {
        internal static Style ATO001 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.HeightRequestProperty, Value = 93 },
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(70) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FF000000") },
                new Setter { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.Start },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Start },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
            }
        };

        internal static Style ATO001D = new Style(typeof(Label))
        {
            BasedOn = ATO001,
            Setters =
            {
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("66000000") },
            }
        };

        internal static Style ATO002 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.WidthRequestProperty, Value = 52 },
                new Setter { Property = Label.HeightRequestProperty, Value = 48 },
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(36) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FF000000") },
                new Setter { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.FillAndExpand },
                new Setter { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.FillAndExpand },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Start },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
            }
        };

        internal static Style ATO002D = new Style(typeof(Label))
        {
            BasedOn = ATO002,
            Setters =
            {
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("66000000") },
            }
        };

        internal static Style ATO003 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.WidthRequestProperty, Value = (268 - 43) },
                new Setter { Property = Label.HeightRequestProperty, Value = 43 },
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(32) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FF000000") },
                new Setter { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.End },
                new Setter { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.Center },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Start },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
                new Setter { Property = Label.LineBreakModeProperty, Value = LineBreakMode.TailTruncation }
            }
        };

        internal static Style ATO003D = new Style(typeof(Label))
        {
            BasedOn = ATO003,
            Setters =
            {
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("66000000") },
            }
        };

        internal static Style ATO004 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.WidthRequestProperty, Value = 268 },
                new Setter { Property = Label.HeightRequestProperty, Value = 43 },
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(32) },
                new Setter { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.End },
                new Setter { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.Center },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Start },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FF59B03A") },
            }
        };

        internal static Style ATO004D = new Style(typeof(Label))
        {
            BasedOn = ATO004,
            Setters =
            {
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("6659B03A") },
            }
        };

        internal static Style ATO044 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.WidthRequestProperty, Value = 268 },
                new Setter { Property = Label.HeightRequestProperty, Value = 86 },
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(55) },
                new Setter { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.End },
                new Setter { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.Center },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Start },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FF00DD00") },
            }
        };

        internal static Style ATO044D = new Style(typeof(Label))
        {
            BasedOn = ATO044,
            Setters =
            {
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("6659B03A") },
            }
        };

        internal static Style ATO005 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(32) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FFB3B3B3") },
            }
        };

        internal static Style ATO005D = new Style(typeof(Label))
        {
            BasedOn = ATO005,
            Setters =
            {
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("66B3B3B3") },
            }
        };


        internal static Style ATO006 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(52) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FFFAFAFA") },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
            }
        };

        internal static Style ATO007 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(183) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FFFAFAFA") },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
            }
        };

        internal static Style ATO008 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(48) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FFFAFAFA") },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
            }
        };


        internal static Style ATO0021 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(96) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FF000000") },
            }
        };


        internal static Style T023 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(50/*40*/) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FF000000") },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center  },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Start  }
            }
        };

        internal static Style T024 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(32) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FF737373") },
            }
        };

        internal static Style T033 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(40) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FF000000") },
            }
        };

        internal static Style T020 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(50) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("FFFAFAFA") },
            }
        };
    }
}
