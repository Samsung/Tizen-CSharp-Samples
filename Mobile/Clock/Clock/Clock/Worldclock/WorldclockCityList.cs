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

using Clock.Converters;
using Clock.Styles;
using Xamarin.Forms;

namespace Clock.Worldclock
{
    /// <summary>
    /// WorldclockCityList  class
    /// </summary>
    public class WorldclockCityList
    {
        // the maximum number of the items of list
        public const int MAX_ITEMS_LIMIT = 20;

        // StackLayout when an app user adds cities
        public WorldclockCityListUI cityListUI;
        // StackLayout when there's no cities that an app user adds to a list
        public StackLayout emptyListAreaLayout;

        /// <summary>
        /// Constructor
        /// </summary>
        public WorldclockCityList()
        {
            CreateEmptyListPage();
            cityListUI = new WorldclockCityListUI();
        }

        /// <summary>
        /// Create the emptyListAreaLayout
        /// </summary>
        private void CreateEmptyListPage()
        {
            if (emptyListAreaLayout == null)
            {
                emptyListAreaLayout = new StackLayout()
                {
                    Spacing = 0,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    //HeightRequest = 1113 - 679,
                    WidthRequest = 720,
                    BackgroundColor = Color.White,
                };
                emptyListAreaLayout.SetBinding(StackLayout.IsVisibleProperty, new Binding("CityRecordList.Count", BindingMode.Default, new ItemCountToVisibilityConverter(), true));

                Label emptyListLabel = new Label
                {
                    Text = "After you add cities, they\r\nwill be shown here.",
                    //BackgroundColor = Color.FromHex("#0099aa"),
                    WidthRequest = 720 - 181 - 181,
                    HeightRequest = 43 + 43,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    // TODO: THERE IS NO GUIDELINE FOR FONT
                    // From native clock app, base: "font=Tizen:style=Regular color=#808080aa font_size=34 align=center"; 
                    TextColor = Color.FromHex("#808080aa"),
                    FontSize = CommonStyle.GetDp(31),
                    Margin = new Thickness(181, 452 / 2 - 43, 181, 452 / 2 - 43),
                };

                emptyListAreaLayout.Children.Add(emptyListLabel);
            }
        }
    }
}
