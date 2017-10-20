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
using Tizen.Xamarin.Forms.Extension;
using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// Alarm tone row class.
    /// This class defines layout of alarm tone row
    /// </summary>
    class AlarmToneRow : RelativeLayout
    {
        /// <summary>
        /// Main label
        /// </summary>
        private Label mainLabel;
        /// <summary>
        /// Main string
        /// </summary>
        string mainStr;
        /// <summary>
        /// Tone radio button
        /// </summary>
        internal RadioButton toneRadio;
        /// <summary>
        /// Tone radio button new value
        /// </summary>
        internal static AlarmToneTypes newValue;
        /// <summary>
        /// Tone radio button old value
        /// </summary>
        internal static AlarmToneTypes oldValue;

        /// <summary>
        /// Construct alarm tone row UI
        /// </summary>
        /// <param name="editType">type indicator</param>
        /// <seealso cref="AlarmToneTypes">
        public AlarmToneRow(AlarmToneTypes type)
        {
            /// Sets each row height to 120 according UX guide
            HeightRequest = 120;

            /// Checks type and set mainStr
            switch (type)
            {
                case AlarmToneTypes.AlarmMp3:
                    mainStr = "alarm.mp3";
                    break;
                case AlarmToneTypes.Default:
                    mainStr = "Default";
                    break;
                case AlarmToneTypes.RingtoneSdk:
                    mainStr = "ringtone_sdk.mp3";
                    break;
                default:
                    mainStr = "";
                    break;
            }

            /// If mainStr is not null, create a new main Label
            if (mainStr != null)
            {
                if (mainLabel == null)
                {
                    mainLabel = new Label
                    {
                        HeightRequest = 54,
                        Style = AlarmStyle.T033,
                    };
                    // to meet To meet thin attribute for font, need to use custom feature
                    FontFormat.SetFontWeight(mainLabel, FontWeight.Light);
                }

                /// Sets main Label with mainStr
                mainLabel.Text = mainStr;
                /// Add to layout
                Children.Add(mainLabel,
                    Constraint.RelativeToParent((parent) => { return 32; }),
                    Constraint.RelativeToParent((parent) => { return (120 - 72) / 2; }));
            }

            /// Create a new radio button for this row
            toneRadio = new RadioButton
            {
                Text = type.ToString(),
                HeightRequest = 80,
                WidthRequest = 80,
                /// Group name should be set same for all radio button group elements
                GroupName = "AlarmTone",
            };

            if (AlarmModel.BindableAlarmRecord.AlarmToneType == type)
            {
                toneRadio.IsSelected = true;
                oldValue = newValue = type;
            }

            toneRadio.Selected += ToneRadio_Selected;

            Children.Add(toneRadio,
                Constraint.RelativeToParent((parent) => (parent.X + parent.Width - (80 + 32))),
                Constraint.RelativeToParent((parent) => { return (120 - 80) / 2; }));
        }

        /// <summary>
        /// Event callback to set proper alarm tone value
        /// </summary>
        /// <param name="sender">Event object sender</param>
        /// <param name="e">Event argument</param>
        private void ToneRadio_Selected(object sender, SelectedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button.IsSelected)
            {
                switch (button.Text)
                {
                    case "Default":
                        newValue = AlarmToneTypes.Default;
                        break;
                    case "AlarmMp3":
                        newValue = AlarmToneTypes.AlarmMp3;
                        break;
                    case "RingtoneSdk":
                        newValue = AlarmToneTypes.RingtoneSdk;
                        break;
                    default:
                        newValue = AlarmToneTypes.Default;
                        break;
                }
            }
        }
    }
}
