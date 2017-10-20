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
    /// The style for image used in ring page
    /// </summary>
    class RingStyle
    {
        internal static Style AO003 = new Style(typeof(Image))
        {
            Setters =
            {
                new Setter { Property = Image.BackgroundColorProperty, Value = Color.FromHex("FFE02222") },
            }
        };
        internal static Style AO003P = new Style(typeof(Image))
        {
            Setters =
            {
                new Setter { Property = Image.BackgroundColorProperty, Value = Color.FromHex("FF871414") },
            }
        };
        internal static Style AO004 = new Style(typeof(Image))
        {
            Setters =
            {
                new Setter { Property = Image.BackgroundColorProperty, Value = Color.FromHex("FFFFB300") },
            }
        };
        internal static Style AO004P = new Style(typeof(Image))
        {
            Setters =
            {
                new Setter { Property = Image.BackgroundColorProperty, Value = Color.FromHex("FFA67400") },
            }
        };
    }
}
