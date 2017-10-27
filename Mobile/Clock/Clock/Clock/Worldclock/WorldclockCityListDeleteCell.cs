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
using Clock.Data;
using Xamarin.Forms;

namespace Clock.Worldclock
{
    /// <summary>
    /// Custom Cell class for ListView in World clock delete page
    /// It inherits from WorldclockCityListCell class.
    /// Compared to WorldclockCityListCell, the Switch widget is added for delete option.
    /// </summary>
    public class WorldclockCityListDeleteCell : WorldclockCityListCell
    {
        // Switch to represent whether a List's data item will be deleted or not
        public Switch CheckBoxSwitch;

        // Content page which WorldclockCityListDeleteCell's added to
        WorldclockDeletePage currentPage = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public WorldclockCityListDeleteCell() : base()
        {
            // Resizing labels for adding Switch to listview's itemTemplate
            AdjustSize();

            // Add CheckBox to WorldclockCityListCell
            CheckBoxSwitch = new Switch()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            VisualAttributes.SetThemeStyle(CheckBoxSwitch, "CheckBox");
            CheckBoxSwitch.SetBinding(Switch.IsToggledProperty, new Binding("Delete", BindingMode.TwoWay));
            CheckBoxSwitch.Toggled += ItemSwitch_Toggled;
            cityListItemLayout.Children.Add(CheckBoxSwitch,
                Constraint.RelativeToParent((parent) =>
                {
                    return 720 - 104;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return 22 + 93 - 72;
                }));
        }

        /// <summary>
        /// Invoked when the switch is toggled or untoggled.
        /// </summary>
        /// <param name="sender">Checkbox-style Switch</param>
        /// <param name="e">ToggledEventArgs</param>
        private void ItemSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (currentPage == null)
            {
                int currentIndex = (App.Current.MainPage as NavigationPage).Navigation.NavigationStack.Count - 1;
                currentPage = (WorldclockDeletePage)(App.Current.MainPage as NavigationPage).Navigation.NavigationStack[currentIndex];
            }

            // In case that it's called by deleteAllSwitch, there's nothing to do anymore.
            if (currentPage.OnAlldeleted == SwitchType.WholeSwitch)
            {
                return;
            }

            currentPage.OnAlldeleted = SwitchType.SingleSwitch;
            // It's called by turning on/off the "CheckBoxSwitch" switch

            // In case that Switch in ListView's ItemCell is off,
            if (!e.Value && currentPage.deleteAllSwitch.IsToggled)
            {
                currentPage.deleteAllSwitch.IsToggled = false;
            }

            UpdateTitle();
            // In case that Switch is on
            // Need to check all data items' switches are all on
            // If so, deleteAllSwitch switch should be toggled.
            if (e.Value && (currentPage.selectedItems == ((WorldclockInfo)currentPage.BindingContext).CityRecordList.Count))
            {
                currentPage.deleteAllSwitch.IsToggled = true;
            }

            currentPage.OnAlldeleted = SwitchType.None;
        }

        /// <summary>
        /// Update titleBar's Text based on how many delete switches are on.
        /// </summary>
        private void UpdateTitle()
        {
            int count = 0;
            foreach (CityRecord item in ((WorldclockInfo)currentPage.BindingContext).CityRecordList)
            {
                if (item.Delete)
                {
                    count++;
                }
            }

            currentPage.titleBar.TitleLabel.Text = count + " selected";
            currentPage.selectedItems = count;
            return;
        }
    }
}
