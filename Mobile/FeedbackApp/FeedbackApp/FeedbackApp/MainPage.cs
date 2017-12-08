using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using System.Threading.Tasks;

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
            // Create SecondPage with user selected pattern
            await this.Navigation.PushAsync(new SecondPage(a.Item.ToString()));
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
