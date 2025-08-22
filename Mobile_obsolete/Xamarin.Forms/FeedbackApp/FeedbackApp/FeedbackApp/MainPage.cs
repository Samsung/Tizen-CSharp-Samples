using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using System.Threading.Tasks;

using Tizen;
using Tizen.System;

namespace FeedbackApp
{
    /// <summary>
    /// Main page of Feedback application
    /// User can select feedback patterns
    /// </summary>
    class MainPage : ContentPage
    {
        /// <summary>
        /// Asynchronous method for tapped event of pattern item
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="a">Event argument</param>
        private async void Pattern_ItemTappedAsync(object sender, ItemTappedEventArgs a)
        {
            bool support, sound_spt, vib_spt;
            string pattern = a.Item.ToString();
            Feedback feedback = new Feedback();

            try
            {
                // Check whether user selected pattern is supported
                sound_spt = feedback.IsSupportedPattern(FeedbackType.Sound, pattern);
                vib_spt = feedback.IsSupportedPattern(FeedbackType.Vibration, pattern);
                if (!sound_spt && !vib_spt)
                    support = false;
                else
                    support = true;
                // If pattern is supported, play feedback
                if (support)
                    feedback.Play(FeedbackType.All, pattern);

                // Create ResultPage with pattern and supported information
                // If pattern is not supported, there is no feedback
                await this.Navigation.PushAsync(new ResultPage(pattern, support));
            }
            catch (Exception e)
            {
                Log.Debug("FeedbackApp", e.Message);
                // Create ResultPage with pattern
                // When there is exception, feedback play is failed
                await this.Navigation.PushAsync(new ResultPage(pattern, false));
            }

        }

        /// <summary>
        /// The constructor of MainPage class
        /// Feedback patterns are listed
        ///     Tap
        ///     SoftInputPanel
        ///     Key0
        ///     Key1
        ///     Key2
        ///     Key3
        ///     Key4
        ///     Key5
        ///     Key6
        ///     Key7
        ///     Key8
        ///     Key9
        ///     KeyStar
        ///     KeySharp
        ///     KeyBack
        ///     Hold
        ///     HardwareKeyPressed
        ///     HardwareKeyHold
        ///     Message
        ///     Email
        ///     WakeUp
        ///     Schedule
        ///     Timer
        ///     General
        ///     PowerOn
        ///     PowerOff
        ///     ChargerConnected
        ///     ChargingError
        ///     FullyCharged
        ///     LowBattery
        ///     Lock
        ///     UnLock
        ///     VibrationModeAbled
        ///     SilentModeDisabled
        ///     BluetoothDeviceConnected
        ///     BluetoothDeviceDisconnected
        ///     ListReorder
        ///     ListSlider
        ///     VolumeKeyPressed
        /// </summary>
        public MainPage()
        {
            // Title of this page is Patterns
            this.Title = "Patterns";
            var MainListView = new ListView();

            // Create a new ListView of pattern strings
            MainListView.ItemsSource = new string[]
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

            // Add ItemTapped event
            MainListView.ItemTapped += Pattern_ItemTappedAsync;
            this.Content = MainListView;
        }
    }
}
