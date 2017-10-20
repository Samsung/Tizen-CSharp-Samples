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

namespace Clock.Worldclock
{
    /// <summary>
    /// Custom Cell class for ListView in World clock page
    /// </summary>
    public class WorldclockCityListCell : ViewCell
    {
        private Label timeLabel;
        private Label amPmLabel;
        private Label dateLabel;
        private Label citiesLabel;
        private Label relativeToLocalLabel;
        protected RelativeLayout cityListItemLayout;
        private const int ADJUST_WIDTH_OF_LABEL = 333;

        /// <summary>
        /// Constructor
        /// </summary>
        public WorldclockCityListCell()
        {
            View = CreateCityList();
        }

        /// <summary>
        /// Create a RelativeLayout for custom cells
        /// </summary>
        /// <returns>RelativeLayout</returns>
        private RelativeLayout CreateCityList()
        {
            if (cityListItemLayout == null)
            {
                cityListItemLayout = new RelativeLayout
                {
                    WidthRequest = 720,
                    HeightRequest = 22 + 53 + 43 + 26,
                };

                timeLabel = new Label
                {
                    Style = WorldclockStyle.ATO040,
                    WidthRequest = 113,
                    HeightRequest = 61, /*67*/// GUI GUIDE : 67
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                // to meet To meet thin attribute for font, need to use custom feature
                FontFormat.SetFontWeight(timeLabel, FontWeight.Light);
                timeLabel.SetBinding(Label.TextProperty, "CityTime");

                cityListItemLayout.Children.Add(timeLabel,
                    Constraint.RelativeToParent((parent) =>
                    {
                        return 32;
                    }),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return 17;
                    }));

                amPmLabel = new Label
                {
                    Style = WorldclockStyle.ATO041,
                    WidthRequest = 52,
                    HeightRequest = 40,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    //BackgroundColor = Color.Violet,
                };
                // to meet To meet thin attribute for font, need to use custom feature
                FontFormat.SetFontWeight(amPmLabel, FontWeight.Medium);
                amPmLabel.SetBinding(Label.TextProperty, "CityAmPm");
                cityListItemLayout.Children.Add(amPmLabel,
                    Constraint.RelativeToParent((parent) =>
                    {
                        return 32 + 113 + 8;
                    }),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return 17 + 21;
                    }));

                dateLabel = new Label
                {
                    Style = WorldclockStyle.ATO042,
                    Text = "Wed, 20 Mar",
                    WidthRequest = 173,
                    HeightRequest = 40,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    //BackgroundColor = Color.Beige,
                };
                // to meet To meet thin attribute for font, need to use custom feature
                FontFormat.SetFontWeight(dateLabel, FontWeight.Normal);
                dateLabel.SetBinding(Label.TextProperty, "CityDate");
                cityListItemLayout.Children.Add(dateLabel,
                    Constraint.RelativeToParent((parent) =>
                    {
                        return 32;
                    }),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return 17 + 61;
                    }));

                citiesLabel = new Label
                {
                    Style = WorldclockStyle.ATO043,
                    Text = "Beijing, China",
                    WidthRequest = 405,
                    HeightRequest = 53,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    //BackgroundColor = Color.SpringGreen,
                    LineBreakMode = LineBreakMode.TailTruncation,
                };
                // to meet To meet thin attribute for font, need to use custom feature
                FontFormat.SetFontWeight(citiesLabel, FontWeight.Normal);
                citiesLabel.SetBinding(Label.TextProperty, "Cities");
                cityListItemLayout.Children.Add(citiesLabel,
                    Constraint.RelativeToParent((parent) =>
                    {
                        return 32 + 173 + 82;
                    }),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return 22;
                    }));

                relativeToLocalLabel = new Label
                {
                    Style = WorldclockStyle.ATO044,
                    Text = "Same as local time",
                    WidthRequest = 405,
                    HeightRequest = 43,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start,
                    //BackgroundColor = Color.Black,
                };
                // to meet To meet thin attribute for font, need to use custom feature
                FontFormat.SetFontWeight(relativeToLocalLabel, FontWeight.Normal);
                relativeToLocalLabel.SetBinding(Label.TextProperty, "RelativeToLocalCountry");
                cityListItemLayout.Children.Add(relativeToLocalLabel,
                    Constraint.RelativeToParent((parent) =>
                    {
                        return 32 + 173 + 82;
                    }),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return 22 + 53;
                    }));
            }

            return cityListItemLayout;
        }

        /// <summary>
        /// Resize of labels to secure spare space when the Switch for delete option is included in this custom cell
        /// </summary>
        protected void AdjustSize()
        {
            citiesLabel.WidthRequest = ADJUST_WIDTH_OF_LABEL;
            relativeToLocalLabel.WidthRequest = ADJUST_WIDTH_OF_LABEL;
        }
    }
}
