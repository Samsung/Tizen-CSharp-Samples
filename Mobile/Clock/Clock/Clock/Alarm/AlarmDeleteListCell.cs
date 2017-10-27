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
using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// This class defines list cell for deleting Alarm.
    /// This inherits from AlarmListCell which provides custom UI for a ListView
    /// It is commonly used for any cell for this AlarmDeleteListView
    /// </summary>
    public class AlarmDeleteListCell : AlarmListCell
    {
        // Checkbox-styled Switch
        public Switch CheckBoxSwitch;

        // Content page which WorldclockCityListDeleteCell's added to
        AlarmDeletePage currentPage = null;

        // Constructor
        public AlarmDeleteListCell() : base()
        {
            // Set current page
            currentPage = ((AlarmDeletePage)AlarmPageController.GetInstance(AlarmPages.DeletePage));
        }

        // Construct alarm list cell's UI
        protected override RelativeLayout Draw()
        {
            RelativeLayout alarmItemDeleteLayout = base.Draw();
            switchObj.IsVisible = false;
            CheckBoxSwitch = new Switch()
            {
                HeightRequest = 72,
                WidthRequest = 72,
            };
            VisualAttributes.SetThemeStyle(CheckBoxSwitch, "CheckBox");
            CheckBoxSwitch.SetBinding(Switch.IsToggledProperty, new Binding("Delete", BindingMode.TwoWay));
            CheckBoxSwitch.Toggled += ItemSwitch_Toggled;
            alarmItemLayout.Children.Add(CheckBoxSwitch,
                    Constraint.RelativeToParent((parent) =>
                    {
                        return (720 - 104);
                    }),
                    Constraint.RelativeToParent((parent) =>
                    {
                        return (22 + 93 - 72);
                    }));
            return alarmItemDeleteLayout;
        }

        /// <summary>
        /// Invoked when the switch is toggled or untoggled.
        /// </summary>
        /// <param name="sender">Data item's checkBox-style Switch object</param>
        /// <param name="e">ToggledEventArgs</param>
        private void ItemSwitch_Toggled(object sender, ToggledEventArgs e)
        {
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
                // Set delete to false
                currentPage.deleteAllSwitch.IsToggled = false;
            }

            UpdateTitle();
            // In case that Switch is on
            // Need to check all data items' switches are all on
            // If so, deleteAllSwitch switch should be toggled.
            if (e.Value && (currentPage.selectedItems == AlarmModel.ObservableAlarmList.Count))
            {
                currentPage.deleteAllSwitch.IsToggled = true;
            }
            // Set all deleted none
            currentPage.OnAlldeleted = SwitchType.None;
        }

        /// <summary>
        /// Update titleBar's Text based on how many delete switches are on.
        /// </summary>
        private void UpdateTitle()
        {
            int count = 0;
            // Iterate items to count marked deleted
            foreach (AlarmRecord item in AlarmModel.ObservableAlarmList)
            {
                if (item.Delete)
                {
                    count++;
                }
            }
            // Change title label text
            currentPage.titleBar.TitleLabel.Text = count + " selected";
            currentPage.selectedItems = count;
            return;
        }

        /// <summary>
        /// Binding context callback
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext == null)
            {
                return;
            }
            else
            {
                View = Draw();
            }
        }
    }
}
