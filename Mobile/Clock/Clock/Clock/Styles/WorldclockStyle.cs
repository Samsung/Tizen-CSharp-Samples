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
    /// The style for label used in Worldclock page
    /// </summary>
    class WorldclockStyle
    {
        internal static Style ATO017 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(70) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("#FFFAFAFA") },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
            }
        };

        internal static Style ATO018 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(36) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("#FFFAFAFA") },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Start },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
            }
        };

        internal static Style ATO016 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(36) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("#FFFAFAFA") },
                new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center },
                new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
            }
        };


        internal static Style ATO040 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(50) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("#FF000000") },
            }
        };

        internal static Style ATO041 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(30) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("#FF000000") },
            }
        };

        internal static Style ATO042 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(30) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("#FF808080") },
            }
        };

        internal static Style ATO043 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(40) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("#FF000000") },
            }
        };

        internal static Style ATO044 = new Style(typeof(Label))
        {
            Setters =
            {
                new Setter { Property = Label.FontSizeProperty, Value = CommonStyle.GetDp(32) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("#FF808080") },
            }
        };

    }
}
