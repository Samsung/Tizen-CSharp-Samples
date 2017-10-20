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
using Clock.Converters;
using Clock.Interfaces;
using Clock.Utils;
using System;
using Tizen.Xamarin.Forms.Extension;
using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// This class defines alarm list page which shows all alarms in ListView
    /// This inherits from floating button enabled content page
    /// Floating button enabled content page is a Tizen custom feature.
    /// So for other platforms need to implement this feature to run on those platforms
    /// </summary>
    public class AlarmListPage : ContentPage
    {
        public AlarmModel alarmModel;
        /// <summary>
        /// stack layout to show alarm ListView
        /// </summary>
        public AlarmListUI alarmListUI;

        /// <summary>
        /// blank relative layout to show when no alarm available
        /// </summary>
        internal AlarmEmptyPageLayout blankLayout;

        // Dialog that is shown when an app user presses HW menu key
        private MoreMenuDialog dialog;

        // the maximum number of the items of list
        public const int MAX_ITEMS_LIMIT = 20;

        /// <summary>
        /// blank layout to show when no alarm available
        /// </summary>
        public AlarmListPage()
        {
            /// Title for this page
            Title = "Alarm";
            /// Icon to be shown in the main tab
            Icon = "maintabbed/clock_tabs_ic_alarm.png";

            alarmModel = new AlarmModel();
            BindingContext = alarmModel;

            // Create alarmList UI whether or not there is a saved alarm record
            alarmListUI = new AlarmListUI();
            // Create Blank page
            blankLayout = CreateBlankStack();

            AlarmModel.PrintAll("In AlarmListPage(), Dictionary and list are loaded.");

            /// Needs to check alarm record dictionary availability
            blankLayout.IsVisible = (AlarmModel.ObservableAlarmList.Count == 0) ? true : false;
            alarmListUI.IsVisible = (AlarmModel.ObservableAlarmList.Count == 0) ? false : true;

            blankLayout.SetBinding(RelativeLayout.IsVisibleProperty, new Binding("Count", BindingMode.Default, new ItemCountToVisibilityConverter(), true, source: AlarmModel.ObservableAlarmList));
            alarmListUI.SetBinding(StackLayout.IsVisibleProperty, new Binding("Count", BindingMode.Default, new ItemCountToVisibilityConverter(), false, source: AlarmModel.ObservableAlarmList));

            StackLayout mainLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 0,
                Children =
                {
                    blankLayout,
                    alarmListUI,
                }
            };
            Content = mainLayout;
        }

        /// <summary>
        /// Called when the floating button has been clicked
        /// Show a page for editing an alarm
        /// When the floating button is clicked (new alarm request), new alarm will be created and call
        /// Alarm Edit Page with the created alarm record
        /// </summary>
        /// <param name="sender">floating button object</param>
        /// <seealso cref="System.object">
        /// <param name="e">Event argument for event of floating button.</param>
        /// <seealso cref="System.EventArgs">
        public void OnFloatingButtonClicked(object sender, EventArgs e)
        {
            if (AlarmModel.ObservableAlarmList.Count >= MAX_ITEMS_LIMIT)
            {
                Toast.DisplayText("Maximum number of alarms(" + MAX_ITEMS_LIMIT + ") reached.");
                return;
            }

            /// Creates default alarm record
            AlarmRecord defaultAlarmRecord = new AlarmRecord();
            defaultAlarmRecord.SetDefault();
            /// Call via alarm page controller which instantiates page once
            Navigation.PushAsync(AlarmPageController.GetInstance(AlarmPages.EditPage, defaultAlarmRecord), false);
        }

        /// <summary>
        /// Each time this page is appearing, alarm count should be checked and if no alarm, 
        /// then use the blank page instead of alarm list view.
        /// In other case, just show alarm list UI.
        /// </summary>
        protected override void OnAppearing()
        {
            // When this page is shown, a floating button should be visible for an app user to add a new alarm
            ((App)Application.Current).ShowFloatingButton(Title);
            // Start listening for key events
            MenuKeyListener.Start(this, MenuKeyPressed);
        }

        /// <summary>
        /// Each time this page is disappearing, a floating button should be hidden.
        /// </summary>
        protected override void OnDisappearing()
        {
            // Stop listening for key events
            MenuKeyListener.Stop(this);
            // Make a floating button hidden
            ((App)Application.Current).HideFloatingButton(Title);
        }

        /// <summary>
        /// Blank stack looks trivial but still needs to align text in the middle of the page
        /// So you can't just put a big label to show this message since labels should be
        /// located into proper location
        /// </summary>
        /// <returns>AlarmEmptyPageLayout</returns>
        private AlarmEmptyPageLayout CreateBlankStack()
        {
            /// Create a blan page layout
            blankLayout = new AlarmEmptyPageLayout();
            /// Sets proper texts
            blankLayout.MainTitle = "No alarms";
            blankLayout.Subline1 = "After you create alarms, they will";
            blankLayout.Subline2 = "be shown here";
            return blankLayout;
        }

        /// <summary>
        /// Invoked when "Delete" more menu is choosed
        /// </summary>
        /// <param name="menu">string</param>
        private void ShowDeletePage(string menu)
        {
            // Make MoreMenuDialog invisible
            dialog.Hide();
            // Make WorldclockDeletePage visible
            Navigation.PushAsync(AlarmPageController.GetInstance(AlarmPages.DeletePage), false);
        }

        /// <summary>
        /// Called when HW menu key button is pressed in World clock page.
        /// </summary>
        /// <param name="sender">KeyEventSender</param>
        /// <param name="key">HW menu key("XF86Menu")</param>
        private void MenuKeyPressed(IKeyEventSender sender, string key)
        {
            // Ignore menu key handling when no data item is added to List
            // because there's no item to delete or reorder
            if (AlarmModel.ObservableAlarmList.Count == 0)
            {
                return;
            }
            // If one data item is added to a list, only "DELETE" more menu will be provided.
            dialog = new MoreMenuDialog(MORE_MENU_OPTION.MORE_MENU_DELETE, ShowDeletePage);

            dialog.BackButtonPressed += Dialog_Canceled;
            dialog.OutsideClicked += Dialog_Canceled;
            dialog.Shown += Dialog_Shown;
            // Make MoreMenuDialog visible
            dialog.Show();
        }

        private void Dialog_Shown(object sender, EventArgs e)
        {
            // Disable handling HW menu key while MoreMenuDialog is shown
            MenuKeyListener.Stop(this);
        }

        private void Dialog_Canceled(object sender, EventArgs e)
        {
            // Make MoreMenuDialog hidden
            ((MoreMenuDialog)sender).Hide();
            // Restart listening HW Menu key and subscribing from messages about pressing HW meny key
            MenuKeyListener.Start(this, MenuKeyPressed);
        }
    }
}
