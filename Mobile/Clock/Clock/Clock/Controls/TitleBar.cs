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

using System;
using Xamarin.Forms;

namespace Clock.Controls
{
    /// <summary>
    /// This class is to define common title bar area
    /// This title bar has left and right buttons with title text.
    /// Proper event handler can be added to left and right button
    /// </summary>
    public class TitleBar : RelativeLayout
    {
        /// <summary>
        /// Left button to show on top of the page
        /// </summary>
        private Button leftButton;

        /// <summary>
        /// Left button field to show on top of the page
        /// </summary>
        private Button rightButton;

        /// <summary>
        /// Right button field to show on top of the page
        /// </summary>
        private Label titleLabel;

        /// <summary>
        /// Left button property to show on top of the page
        /// </summary>
        public Button LeftButton
        {
            get
            {
                return leftButton;
            }

            set
            {
                leftButton = value;
            }
        }

        /// <summary>
        /// Right button property to show on top of the page
        /// </summary>
        public Button RightButton
        {
            get
            {
                return rightButton;
            }

            set
            {
                rightButton = value;
            }
        }

        /// <summary>
        /// Title label on top of the page
        /// </summary>
        public Label TitleLabel
        {
            get
            {
                return titleLabel;
            }

            set
            {
                titleLabel = value;
            }
        }

        /// <summary>
        /// Constructor for this class.
        /// It defines left button-title-right button on top
        /// </summary>
        public TitleBar()
        {
            /// Sets height to 110 according to UX guide
            HeightRequest = 110;
            /// Fills horizontally
            HorizontalOptions = LayoutOptions.FillAndExpand;
            /// Needs to only take as much as HeightRequest
            /// Do not use FillAndExpand here to keep the HeightRequest
            VerticalOptions = LayoutOptions.Start;

            /// Create a new left button
            leftButton = new Button
            {
                WidthRequest = 167,
                HeightRequest = 94,
                /// This style is based on UX guide
                /// This style is not available in other platforms so if you want to run
                /// this app on other than Tizen, this should be removed
            };
            VisualAttributes.SetThemeStyle(leftButton, "naviframe/title_left");

            /// Add button clicked event to left
            leftButton.Clicked += LeftButton_Clicked;

            /// Create a new right button
            rightButton = new Button
            {
                WidthRequest = 167,
                HeightRequest = 94,
                /// This style is based on UX guide
                /// This style is not available in other platforms so if you want to run
                /// this app on other than Tizen, this should be removed
            };
            VisualAttributes.SetThemeStyle(rightButton, "naviframe/title_right");

            /// Create a title label
            titleLabel = new Label
            {
                WidthRequest = 720 - (32 + 130 + 14) * 2,
                // TODO-CHECK:
                HeightRequest = 94/*67*/,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 30,
                TextColor = Color.FromHex("FFFAFAFA"),
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(titleLabel, FontWeight.Light);

            /// Add it to the layout
            Children.Add(leftButton,
                Constraint.RelativeToParent((parent) => { return 8; }),
                Constraint.RelativeToParent((parent) => { return (110 - 94) / 2; }));

            /// Add it to the layout
            Children.Add(rightButton,
                Constraint.RelativeToParent((parent) => { return 720 - (167 + 8); }),
                Constraint.RelativeToParent((parent) => { return (110 - 94) / 2; }));

            /// Add it to the layout
            Children.Add(titleLabel,
                Constraint.RelativeToParent((parent) => { return 32 + 130 + 14; }),
                Constraint.RelativeToParent((parent) => { return (110 - 94) / 2; }));
        }

        /// <summary>
        /// Default event behavior to be invoked when the left button clicked
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event argument</param>
        private void LeftButton_Clicked(object sender, EventArgs e)
        {
            // Go back to the previous page
            Navigation.PopAsync();
        }
    }
}
