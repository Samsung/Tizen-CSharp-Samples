
//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;

using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

using Tizen;
using Tizen.System;

namespace FeedbackApp
{
    class PatternListPage : CirclePage
    {
        /// <summary>
        /// Method is executed after ListView item has been tapped
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="a">Event argument</param>
        private void PatternItemTapped(object sender, ItemTappedEventArgs a)
        {
            string pattern = a.Item.ToString();
            Feedback feedback = new Feedback();

            try
            {
                // Check whether user selected pattern is supported
                bool soundSupported = feedback.IsSupportedPattern(FeedbackType.Sound, pattern);
                bool vibrationSupproted = feedback.IsSupportedPattern(FeedbackType.Vibration, pattern);

                bool supported = soundSupported || vibrationSupproted;

                // If any feedback type for pattern is supported, play all supported feedback types
                if (supported)
                {
                    feedback.Play(FeedbackType.All, pattern);
                }

                // Create ResultPage displaying pattern name and information whether it is supported
                this.Navigation.PushAsync(new ResultPage(pattern, supported));
            }
            catch (Exception e)
            {
                Log.Debug("FeedbackApp", e.Message);
                // Create ResultPage displaying pattern name and information that it is not supported
                // When there is exception, feedback play failed
                this.Navigation.PushAsync(new ResultPage(pattern, false));
            }

        }

        /// <summary>
        /// The constructor of PatternListPage class
        /// </summary>
        public PatternListPage()
        {
            // Set title for page
            this.Title = "Patterns";

            // Create list of pattern names
            var patterns = new string[]
            {
                "Tap",
                "SoftInputPanel",
                "Key0",
                "Key1",
                "Key2",
                "Key3",
                "Key4",
                "Key5",
                "Key6",
                "Key7",
                "Key8",
                "Key9",
                "KeyStar",
                "KeySharp",
                "KeyBack",
                "Hold",
                "HardwareKeyPressed",
                "HardwareKeyHold",
                "Message",
                "Email",
                "WakeUp",
                "Schedule",
                "Timer",
                "General",
                "PowerOn",
                "PowerOff",
                "ChargerConnected",
                "ChargingError",
                "FullyCharged",
                "LowBattery",
                "Lock",
                "UnLock",
                "VibrationModeAbled",
                "SilentModeDisabled",
                "BluetoothDeviceConnected",
                "BluetoothDeviceDisconnected",
                "ListReorder",
                "ListSlider",
                "VolumeKeyPressed",
            };

            // Create ListView displaying pattern names
            var MainListView = new CircleListView()
            {
                ItemsSource = patterns,
            };

            // Add ItemTapped event handler
            MainListView.ItemTapped += PatternItemTapped;
            this.Content = MainListView;
        }
    }
}
