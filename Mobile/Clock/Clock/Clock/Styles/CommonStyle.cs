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

using Clock.Interfaces;
using Xamarin.Forms;

namespace Clock.Styles
{
    /// <summary>
    /// The CommonStyle class used for Xamarin.Forms.Button class
    /// </summary>
    public class CommonStyle
    {
        internal static int GetDp(int fontSize)
        {
            int dpi;
            ISystemInfo iSysInfo = DependencyService.Get<ISystemInfo>();
            iSysInfo.TryGetValue<int>("http://tizen.org/feature/screen.dpi", out dpi);
            return fontSize * 160 / dpi;
        }

        /// <summary>
        /// The oneButtonStyle which is used for one bottom-style button
        /// Width = 500, FontSize = 40
        /// </summary>
        internal static Style oneButtonStyle = new Style(typeof(Button))
        {
            Setters =
            {
                new Setter { Property = Button.WidthRequestProperty, Value = 500 },
                new Setter { Property = Button.FontSizeProperty, Value = CommonStyle.GetDp(40) },
                new Setter { Property = Button.HorizontalOptionsProperty, Value = LayoutOptions.CenterAndExpand },
            }
        };

        /// <summary>
        /// The twoButtonStyle which is applied to two bottom-style buttons
        /// /// Width = 300, FontSize = 40
        /// </summary>
        internal static Style twoButtonStyle = new Style(typeof(Button))
        {
            BasedOn = oneButtonStyle,
            Setters =
            {
                new Setter { Property = Button.WidthRequestProperty, Value = 300 },
            }
        };
    }
}
