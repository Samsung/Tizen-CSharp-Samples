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

using Clock.Converters;
using Clock.Data;
using Clock.Utils;
using Xamarin.Forms;

namespace Clock.Worldclock
{
    /// <summary>
    /// The customized StackLayout for containing ListView
    /// </summary>
    public class WorldclockCityListUI : StackLayout
    {
        private ListView cityListView;

        /// <summary>
        /// WorldclockCityListUI constructor
        /// </summary>
        public WorldclockCityListUI()
        {
            if (cityListView == null)
            {
                var customCell = new DataTemplate(typeof(WorldclockCityListCell));
                cityListView = new ListView
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HasUnevenRows = false,
                    //RowHeight = 22 + 53 + 43 + 26,
                    ItemsSource = App.ClockInfo.CityRecordList, // this time it would be null
                    ItemTemplate = customCell,
                    //BackgroundColor = Color.Yellow,
                };
                Children.Add(cityListView);
                cityListView.ItemTapped += CityListView_ItemTapped;
                cityListView.ItemSelected += CityListView_ItemSelected;
                SetBinding(StackLayout.IsVisibleProperty, new Binding("CityRecordList.Count", BindingMode.Default, new ItemCountToVisibilityConverter(), false));
            }
        }

        /// <summary>
        /// Called when an item is selected
        /// </summary>
        /// <param name="sender">ListView's data item</param>
        /// <param name="e">SelectedItemChangedEventArgs</param>
        private void CityListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //CityListView_ItemSelected() is called on deselection, which results in SelectedItem being set to null
            if (e.SelectedItem == null)
            {
                return;
            }
            // make it deselected
            ((ListView)sender).SelectedItem = null;
        }

        /// <summary>
        /// Called when an item is tapped
        /// </summary>
        /// <param name="sender">cityListView(Xamarin.Forms.ListView) object</param>
        /// <param name="e">ItemTappedEventArgs</param>
        private void CityListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            CityRecord cityRecord = e.Item as CityRecord;
            TimezoneUtility.SetCurrentTimezone(TimezoneUtility.GetTimezoneByOffset(cityRecord.Offset));
            Worldclock.WorldclockPage.GetInstance().OnMapViewUpdateRequest();
        }
    }
}
