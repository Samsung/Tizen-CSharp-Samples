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
    /// This class defines alarm type row UIs.
    /// This row inhertis from RelativeLayout.
    /// </summary>
    class AlarmTypeRow : RelativeLayout
    {
        /// <summary>
        /// The main text label
        /// </summary>
        private Label mainLabel;

        /// <summary>
        /// The type selection radio button
        /// </summary>
        internal RadioButton typeRadio;
        
        /// <summary>
        /// The main label string
        /// </summary>
        string mainStr;
        
        /// <summary>
        /// The new alarm type value
        /// </summary>
        internal static AlarmTypes newValue;

        /// <summary>
        /// The old alarm type value
        /// </summary>
        internal static AlarmTypes oldValue;

        /// <summary>
        /// Constructor for this class.
        /// Place UI controls and text according to given alarm type
        /// </summary>
        /// <param name="type">The alarm type to show in this row</param>
        /// <seealso cref="AlarmTypes">
        public AlarmTypeRow(AlarmTypes type)
        {
            HeightRequest = 120;
            switch (type)
            {
                case AlarmTypes.Sound:
                    mainStr = "Sound";
                    break;
                case AlarmTypes.Vibration:
                    mainStr = "Vibration";
                    break;
                case AlarmTypes.SoundVibration:
                    mainStr = "Vibration and sound";
                    break;
                default:
                    mainStr = "";
                    break;
            }

            if (mainStr != null)
            {
                if (mainLabel == null)
                {
                    mainLabel = new Label
                    {
                        HeightRequest = 54,
                        Style = AlarmStyle.T033,
                        Text = mainStr,
                    };
                    // to meet To meet thin attribute for font, need to use custom feature
                    FontFormat.SetFontWeight(mainLabel, FontWeight.Light);
                }

                Children.Add(mainLabel,
                    Constraint.RelativeToParent((parent) => { return 32; }),
                    Constraint.RelativeToParent((parent) => { return (120 - 72) / 2; }));
            }

            typeRadio = new RadioButton
            {
                Text = type.ToString(),
                GroupName = "AlarmType",
                HeightRequest = 80,
                WidthRequest = 80,
            };

            if (AlarmModel.BindableAlarmRecord.AlarmType == type)
            {
                typeRadio.IsSelected = true;
                oldValue = newValue = type;
            }

            typeRadio.Selected += TypeRadio_Selected;

            Children.Add(typeRadio,
                Constraint.RelativeToParent((parent) => (parent.X + parent.Width - (80 + 32))),
                Constraint.RelativeToParent((parent) => { return (120 - 80) / 2; }));
        }

        /// <summary>
        /// Event callback to set proper alarm type value
        /// </summary>
        /// <param name="sender">Event object sender</param>
        /// <param name="e">Event argument</param>
        private void TypeRadio_Selected(object sender, SelectedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
            if (button.IsSelected)
            {
                switch (button.Text)
                {
                    case "Sound":
                        newValue = AlarmTypes.Sound;
                        break;
                    case "SoundVibration":
                        newValue = AlarmTypes.SoundVibration;
                        break;
                    case "Vibration":
                        newValue = AlarmTypes.Vibration;
                        break;
                    default:
                        newValue = AlarmTypes.Sound;
                        break;
                }
            }

        }
    }
}
