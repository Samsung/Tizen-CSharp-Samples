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

using Clock.Controls;
using Clock.Styles;
using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// This class defines an empty page to show when no alarm list previously added
    /// When an alarm is added, this page should not be shown
    /// </summary>
    internal class AlarmEmptyPageLayout : RelativeLayout
    {
        private Label _mainLabel;
        private Label _subLabel1;
        private Label _subLabel2;

        /// <summary>
        /// Main title 
        /// This label shows "No alarm"
        /// </summary>
        public string MainTitle
        {
            get
            {
                return _mainLabel.Text;
            }

            set
            {
                _mainLabel.Text = value;
            }
        }

        /// <summary>
        /// Sub line
        /// First line of description
        /// </summary>
        public string Subline1
        {
            get
            {
                return _subLabel1.Text;
            }

            set
            {
                _subLabel1.Text = value;
            }
        }

        /// <summary>
        /// Sub line
        /// Second line of description
        /// </summary>
        public string Subline2
        {
            get
            {
                return _subLabel2.Text;
            }

            set
            {
                _subLabel2.Text = value;
            }
        }

        /// <summary>
        /// Constructor for this class
        /// </summary>
        public AlarmEmptyPageLayout()
        {
            WidthRequest = 720;
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.White;
            /// set main label properties
            _mainLabel = new Label
            {
                WidthRequest = 720 - 32 * 2,
                HeightRequest = 54,
                Style = Styles.AlarmStyle.T020,
                TextColor = Color.Gray,
                FontSize = CommonStyle.GetDp(50),
                HorizontalTextAlignment = TextAlignment.Center,
            };
            FontFormat.SetFontWeight(_mainLabel, FontWeight.Light);

            /// setting sublabel properties
            _subLabel1 = new Label
            {
                WidthRequest = 720 - 32 * 2,
                HeightRequest = 43,
                HorizontalTextAlignment = TextAlignment.Center,
                //Style = Styles.AlarmStyle.T033,
                TextColor = Color.Gray,
                FontSize = CommonStyle.GetDp(36),
            };
            FontFormat.SetFontWeight(_subLabel1, FontWeight.Light);

            /// setting sublabel properties
            _subLabel2 = new Label
            {
                WidthRequest = 720 - 32 * 2,
                HeightRequest = 43,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Gray,
                FontSize = CommonStyle.GetDp(36),
                //Style = Styles.AlarmStyle.T033
            };
            FontFormat.SetFontWeight(_subLabel2, FontWeight.Light);

            /// add to layout
            Children.Add(_mainLabel, Constraint.RelativeToParent((parent) =>
                {
                    return 32;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return (((VisualElement)Parent).Height - 194) / 2; /*((((VisualElement)Parent).Height - 254) - (54 + 54 + 43 + 43)) / 2*/;
                }));

            /// add to layout
            Children.Add(_subLabel1, Constraint.RelativeToView(_mainLabel, (parent, sibling) =>
                {
                    return sibling.X;
                }),
                Constraint.RelativeToView(_mainLabel, (parent, sibling) =>
                {
                    return sibling.Y + sibling.Height + 54;
                }));
            /// add to layout
            Children.Add(_subLabel2, Constraint.RelativeToView(_subLabel1, (parent, sibling) =>
                {
                    return sibling.X;
                }),
                Constraint.RelativeToView(_subLabel1, (parent, sibling) =>
                {
                    return sibling.Y + sibling.Height;
                }));
        }
    }
}
