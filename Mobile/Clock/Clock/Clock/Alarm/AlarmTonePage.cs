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
    /// This class is to define alarm tone setting page
    /// This inherits from ContentPage
    /// Users can select proper alarm tone in this page
    /// </summary>
    public class AlarmTonePage : ContentPage
    {
        // Default tone cell
        AlarmToneTableCell defaultTone;
        // Alarm MP3 tone cell
        AlarmToneTableCell alarmMP3Tone;
        // Alarm ringtone tone cell
        AlarmToneTableCell ringtoneMP3Tone;

        /// <summary>
        /// Constructor for this page
        /// </summary>
        public AlarmTonePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            Draw();
        }

        /// <summary>
        /// This method is called when this page is being shown after creation
        /// </summary>
        internal void Update()
        {
            AlarmToneTypes type = AlarmModel.BindableAlarmRecord.AlarmToneType;
            switch (type)
            {
                case AlarmToneTypes.Default:
                    ((AlarmToneRow)defaultTone.View).toneRadio.IsSelected = true;
                    break;
                case AlarmToneTypes.AlarmMp3:
                    ((AlarmToneRow)alarmMP3Tone.View).toneRadio.IsSelected = true;
                    break;
                case AlarmToneTypes.RingtoneSdk:
                    ((AlarmToneRow)ringtoneMP3Tone.View).toneRadio.IsSelected = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Actual drawing operations are performed in this method
        /// </summary>
        internal void Draw()
        {
            /// Sets title bar
            TitleBar titleBar = new TitleBar();
            /// Sets left button text
            titleBar.LeftButton.Text = "Cancel";
            /// Sets right button text
            titleBar.RightButton.Text = "OK";
            /// Sets title label
            titleBar.TitleLabel.Text = "Select";

            /// Sets event callback when right button is called (update the tone info)
            titleBar.RightButton.Clicked += (s, e) =>
            {
                AlarmModel.BindableAlarmRecord.AlarmToneType = AlarmToneRow.newValue;
                Navigation.PopAsync();
            };

            // Sets default tone table cell
            defaultTone = new AlarmToneTableCell(AlarmToneTypes.Default, this);

            // Sets alarm mp3 tone table cell
            alarmMP3Tone = new AlarmToneTableCell(AlarmToneTypes.AlarmMp3, this);

            // Sets ringtone tone table cell
            ringtoneMP3Tone = new AlarmToneTableCell(AlarmToneTypes.RingtoneSdk, this);

            /// Sets table root to hold all three table cells
            TableRoot root = new TableRoot()
            {
                new TableSection()
                {
                    defaultTone,
                    alarmMP3Tone,
                    ringtoneMP3Tone,
                }
            };

            // TableView to show each tone type
            TableView alarmToneTable = new TableView(root);

            // Main StackLayout
            // titlebar and alarmToneTable are added
            StackLayout mainLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    titleBar,
                    alarmToneTable
                }
            };
            Content = mainLayout;
        }
    }
}
