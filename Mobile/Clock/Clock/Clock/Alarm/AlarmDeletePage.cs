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
using Clock.Interfaces;
using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// The flag to distinguish
    ///   - turn on/of switch to delete all items of list
    ///   - enable deleting all items of list by turning on a item which is the last unchecked one
    /// </summary>
    public enum SwitchType
    {
        // no type
        None,
        // in case of turning on a switch to delete all items
        WholeSwitch,
        // in case of turning on a switch in one of items in a ListView
        SingleSwitch,
    }

    /// <summary>
    /// Alarm Delete Page
    /// </summary>
    public class AlarmDeletePage : ContentPage
    {
        /// <summary>
        /// Main StackLayout
        /// </summary>
        StackLayout mainLayout;

        /// <summary>
        /// Title bar
        /// </summary>
        public TitleBar titleBar;

        /// <summary>
        /// Switch type
        /// </summary>
        public SwitchType OnAlldeleted = SwitchType.None;

        /// <summary>
        /// Delete all switches
        /// </summary>
        public Switch deleteAllSwitch;

        /// <summary>
        /// Count items selected
        /// </summary>
        public int selectedItems = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public AlarmDeletePage()
        {
            Content = CreateDeletePage();
        }

        /// <summary>
        /// Create UI for Alarm Delete Page
        /// </summary>
        /// <returns>StackLayout</returns>
        private StackLayout CreateDeletePage()
        {
            if (mainLayout == null)
            {
                // Hide Navigation Bar
                NavigationPage.SetHasNavigationBar(this, false);

                //TitleBar for Alarm Delete Page
                titleBar = new TitleBar();
                titleBar.LeftButton.Text = "CANCEL";
                titleBar.RightButton.Text = "DELETE";
                titleBar.TitleLabel.Text = "0 selected";
                titleBar.RightButton.Clicked += TitleBar_RightDeleteButton_Clicked;

                // Header of ListView
                RelativeLayout headerLayout = CreateListViewHeader();

                // ListView for Alarm delete page
                ListView AlarmDeleteListview = new ListView
                {
                    BackgroundColor = Color.Gray,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HasUnevenRows = false,
                    Header = headerLayout,
                    // Source of data items.
                    ItemsSource = AlarmModel.ObservableAlarmList,

                    // Define template for displaying each item.
                    // (Argument of DataTemplate constructor is called for
                    //      each item; it must return a Cell derivative.)
                    ItemTemplate = new DataTemplate(typeof(AlarmDeleteListCell))
                };
                AlarmDeleteListview.ItemSelected += AlarmDeleteListview_ItemSelected;

                mainLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children =
                    {
                        titleBar,
                        AlarmDeleteListview
                    }
                };
            }

            return mainLayout;
        }

        /// <summary>
        /// Create UI layout that will be displayed at the top of the ListView
        /// </summary>
        /// <returns>RelativeLayout for ListView's Header</returns>
        private RelativeLayout CreateListViewHeader()
        {
            // The View for Header of ListView
            RelativeLayout headerLayout = new RelativeLayout
            {
                HeightRequest = 93
            };

            // Title label of ListView's header
            Label headerTitleLabel = new Label()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                Text = "Select all",
                TextColor = Color.Black,
                FontSize = 20,
            };
            FontFormat.SetFontWeight(headerTitleLabel, FontWeight.Light);

            headerLayout.Children.Add(headerTitleLabel,
                Constraint.RelativeToParent((parent) =>
                {
                    return 32 + 10;
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return (22 + 93 - 72);
                }));

            // Switch to give an app user an opportunity to delete all data items of List
            deleteAllSwitch = new Switch
            {
                HorizontalOptions = LayoutOptions.End,
            };
            VisualAttributes.SetThemeStyle(deleteAllSwitch, "CheckBox");
            deleteAllSwitch.Toggled += DeleteAllSwitch_Toggled;
            headerLayout.Children.Add(deleteAllSwitch,
                Constraint.RelativeToParent((parent) =>
                {
                    return (720 - 104);
                }),
                Constraint.RelativeToParent((parent) =>
                {
                    return (22 + 93 - 72);
                }));
            return headerLayout;
        }

        /// <summary>
        /// Invoked when the alarm delete page is disappearing
        /// At that time, restore the default value.
        /// </summary>
        protected override void OnDisappearing()
        {
            for (int i = AlarmModel.ObservableAlarmList.Count - 1; i >= 0; i--)
            {
                // check if it is checked to remove
                AlarmModel.ObservableAlarmList[i].Delete = false;
            }
        }

        /// <summary>
        /// Called when "DELETE" button is pressed in title bar of alarm Delete Page.
        ///  - Remove selected data items from list
        ///  - Go back to the previous page(alarm main page)
        /// </summary>
        /// <param name="sender">Titlebar's RightButton(DELETE button)</param>
        /// <param name="e">System.EventArgs</param>
        private void TitleBar_RightDeleteButton_Clicked(object sender, System.EventArgs e)
        {
            for (int i = AlarmModel.ObservableAlarmList.Count - 1; i >= 0; i--)
            {
                // check if it is checked to remove
                if (AlarmModel.ObservableAlarmList[i].Delete)
                {
                    AlarmRecord record = AlarmModel.ObservableAlarmList[i];
                    //var obj = record;
                    DependencyService.Get<IAlarm>().DeleteAlarm(record);
                    // Since it has been removed in the native alarm list, remove from alarm record dictionary also
                    AlarmModel.AlarmRecordDictionary.Remove(record.GetUniqueIdentifier());
                    // remove it from list
                    AlarmModel.ObservableAlarmList.RemoveAt(i);
                }
            }

            AlarmModel.SaveDictionary();
            // Go back to alarm Main Page from alarm Delete Page.
            Navigation.PopAsync();
        }

        /// <summary>
        /// Called when "deleteAllSwitch" switch is toggled or untoggled.
        /// </summary>
        /// <param name="sender">Switch</param>
        /// <param name="e">ToggledEventArgs</param>
        private void DeleteAllSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (OnAlldeleted == SwitchType.SingleSwitch)
            {
                return;
            }

            OnAlldeleted = SwitchType.WholeSwitch;
            // According to the value to which 'deleteAllSwitch' switch is toggled, make all data items' Delete value updated.
            foreach (AlarmRecord record in AlarmModel.ObservableAlarmList)
            {
                record.Delete = e.Value;
            }
            // Update titleBar's Text based on how many delete switches are on.
            if (e.Value)
            {
                titleBar.TitleLabel.Text = AlarmModel.ObservableAlarmList.Count + " selected";
                selectedItems = AlarmModel.ObservableAlarmList.Count;
            }
            else
            {
                titleBar.TitleLabel.Text = "0 selected";
                selectedItems = 0;
            }

            OnAlldeleted = SwitchType.None;
        }

        /// <summary>
        /// Called when a data item is selected or unselected
        /// </summary>
        /// <param name="sender">ListView's data item</param>
        /// <param name="e">SelectedItemChangedEventArgs</param>
        private void AlarmDeleteListview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            AlarmRecord item = (AlarmRecord)e.SelectedItem;
            item.Delete = !item.Delete;

            // make it deselected
            ((ListView)sender).SelectedItem = null;
        }
    }
}
