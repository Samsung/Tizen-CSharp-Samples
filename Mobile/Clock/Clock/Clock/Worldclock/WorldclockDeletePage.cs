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
using Clock.Data;
using Xamarin.Forms;

namespace Clock.Worldclock
{
    /// <summary>
    /// The flag to distinguish
    ///   - turn on/of switch to delete all items of list
    ///   - enable deleting all items of list by turning on a item which is the last unchecked one
    /// </summary>
    public enum SwitchType
    {
        None,
        // in case of turning on a switch to delete all items
        WholeSwitch,
        // in case of turning on a switch in one of items in a ListView
        SingleSwitch,
    }

    /// <summary>
    /// World clock Delete Page
    /// It provides delete feature
    /// </summary>
    public class WorldclockDeletePage : ContentPage
    {
        /// <summary>
        /// Singleton
        /// </summary>
        private static WorldclockDeletePage DeletePage = null;

        /// <summary>
        /// Main StackLayout
        /// </summary>
        StackLayout mainLayout;

        public TitleBar titleBar;

        public SwitchType OnAlldeleted = SwitchType.None;

        WorldclockInfo info;
        public Switch deleteAllSwitch;

        public int selectedItems = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_info">WorldclockInfo</param>
        public WorldclockDeletePage(WorldclockInfo _info)
        {
            info = _info;
            BindingContext = info;
            Content = CreateDeletePage();
        }

        /// <summary>
        /// Return Worldclock delete page
        /// </summary>
        /// <param name="_info">WorldclockInfo</param>
        /// <returns>WorldclockDeletePage</returns>
        public static WorldclockDeletePage GetInstance(WorldclockInfo _info)
        {
            return DeletePage ?? (DeletePage = new WorldclockDeletePage(_info));
        }

        private StackLayout CreateDeletePage()
        {
            if (mainLayout == null)
            {
                // Hide Navigation Bar
                NavigationPage.SetHasNavigationBar(this, false);

                //TitleBar for World clock Delete Page
                titleBar = new TitleBar();
                titleBar.LeftButton.Text = "CANCEL";
                titleBar.RightButton.Text = "DELETE";
                titleBar.TitleLabel.Text = "Select cities";
                titleBar.RightButton.Clicked += TitleBar_RightDeleteButton_Clicked;

                // Header of ListView
                RelativeLayout headerLayout = CreateListViewHeader();

                // ListView for World clock delete page
                ListView worldclockDeleteListview = new ListView
                {
                    BackgroundColor = Color.Gray,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HasUnevenRows = false,
                    Header = headerLayout,
                    // Source of data items.
                    ItemsSource = info.CityRecordList,

                    // Define template for displaying each item.
                    // (Argument of DataTemplate constructor is called for
                    //      each item; it must return a Cell derivative.)
                    ItemTemplate = new DataTemplate(typeof(WorldclockCityListDeleteCell))
                };
                worldclockDeleteListview.ItemSelected += WorldclockDeleteListview_ItemSelected;

                mainLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children =
                    {
                        titleBar,
                        worldclockDeleteListview
                    }
                };
            }

            return mainLayout;
        }

        /// <summary>
        /// Create UI layout that will be displayed at the top of the ListView
        /// </summary>
        /// <returns>RelativeLayout</returns>
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
            // to meet To meet thin attribute for font, need to use custom feature
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
        /// Invoked when the worldclock delete page is disappearing
        /// At that time, restore the default value.
        /// </summary>
        protected override void OnDisappearing()
        {
            for (int i = info.CityRecordList.Count - 1; i >= 0; i--)
            {
                // check if it is checked to remove
                info.CityRecordList[i].Delete = false;
            }
        }

        /// <summary>
        /// Called when "DELETE" button is pressed in title bar of World clock Delete Page.
        ///  - Remove selected data items from list
        ///  - Go back to the previous page(World clock main page)
        /// </summary>
        /// <param name="sender">Titlebar's right button(DELETE button)</param>
        /// <param name="e">EventArgs</param>
        private void TitleBar_RightDeleteButton_Clicked(object sender, System.EventArgs e)
        {
            for (int i = info.CityRecordList.Count - 1; i >= 0; i--)
            {
                // check if it is checked to remove
                if (info.CityRecordList[i].Delete)
                {
                    // remove it from list
                    info.userLocations.RemoveAt(i);
                    info.CityRecordList.RemoveAt(i);
                }
            }
            // Go back to World clock Main Page from World clock Delete Page.
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
            foreach (CityRecord record in info.CityRecordList)
            {
                record.Delete = e.Value;
            }
            // Update titleBar's Text based on how many delete switches are on.
            if (e.Value)
            {
                titleBar.TitleLabel.Text = info.CityRecordList.Count + " selected";
                selectedItems = info.CityRecordList.Count;
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
        private void WorldclockDeleteListview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            CityRecord item = (CityRecord)e.SelectedItem;
            item.Delete = !item.Delete;

            // make it deselected
            ((ListView)sender).SelectedItem = null;
        }
    }
}
