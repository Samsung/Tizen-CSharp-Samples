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
using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// Alarm type page shows which alarm type (vibration, sound, sound and vibration)
    /// This class inherits from ContentPage.
    /// Users can select alarm type for this alarm
    /// </summary>
    public class AlarmTypePage : ContentPage
    {
        /// <summary>
        /// Main stack laout to place UI controls
        /// </summary>
        StackLayout mainLayout;

        /// <summary>
        /// TableView to show alarm type
        /// </summary>
        private TableView alarmTypeTable;

        /// <summary>
        /// TitleBar to be shown on the top of the page
        /// </summary>
        private TitleBar titleBar;

        /// <summary>
        /// Type table view cell for sound
        /// </summary>
        AlarmTypeTableCell sound;

        /// <summary>
        /// Type table view cell for vibration
        /// </summary>
        AlarmTypeTableCell vibration;

        /// <summary>
        /// Type table view cell for sound and vibration
        /// </summary>
        AlarmTypeTableCell soundVibration;

        /// <summary>
        /// Constructor for this class
        /// </summary>
        public AlarmTypePage()
        {
            /// Needs to set navigation bar hidden
            NavigationPage.SetHasNavigationBar(this, false);
            Draw();
        }

        /// <summary>
        /// Draws UI controls in this page
        /// </summary>
        internal void Draw()
        {
            if (titleBar == null)
            {
                titleBar = new TitleBar();
                titleBar.LeftButton.Text = "Cancel";
                titleBar.RightButton.Text = "OK";
                titleBar.TitleLabel.Text = "Select";
                titleBar.RightButton.Clicked += (s, e) =>
                {
                    AlarmModel.BindableAlarmRecord.AlarmType = AlarmTypeRow.newValue;
                    Navigation.PopAsync();
                };
            }

            /// Creates table cell for sound
            sound = new AlarmTypeTableCell(AlarmTypes.Sound, this);
            /// Creates table cell for vibration
            vibration = new AlarmTypeTableCell(AlarmTypes.Vibration, this);
            /// Creates table cell for sound and vibration
            soundVibration = new AlarmTypeTableCell(AlarmTypes.SoundVibration, this);
            /// Sets root for tableview
            TableRoot root = new TableRoot()
            {
                new TableSection()
                {
                    sound,
                    vibration,
                    soundVibration,
                }
            };
            /// Creates alarm type table
            alarmTypeTable = new TableView(root);
            /// Sets main layout
            mainLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    titleBar,
                    alarmTypeTable
                }
            };
            Content = mainLayout;
        }

        /// <summary>
        /// This method is called when this page is on appearing event
        /// </summary>
        internal void Update()
        {
            Draw();
        }
    }
}
