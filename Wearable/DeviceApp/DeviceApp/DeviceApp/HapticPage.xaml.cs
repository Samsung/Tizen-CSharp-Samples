using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.System;
using Tizen.Wearable.CircularUI.Forms;

namespace DeviceApp
{
    /// <summary>
    /// Haptic page of device application
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HapticPage : CirclePage
	{
        /// <summary>
        /// User input to vibrate
        /// </summary>
        HapticInput input = new HapticInput();

        /// <summary>
        /// The constructor of HapticPage
        /// </summary>
        public HapticPage()
        {
            InitializeComponent();
            BindingContext = input;
        }

        /// <summary>
        /// Method for clicked event of OK button
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="args">Event argument</param>
        public void OKClicked(object sender, EventArgs args)
        {
            if (input.Time == null)
            {
                // Haptic duration is null
                this.Navigation.PushAsync(new SimpleResult("Failed: Haptic\nWrong Input"));
            }
            else
            {
                // Vibrate during input time
                HapticOperation((int)input.Time);
            }
        }

        /// <summary>
        /// Method for haptic operation
        /// </summary>
        /// <param name="time">Duration of vibraation</param>
        private async void HapticOperation(int time)
        {
            try
            {
                // Gets the number of the available vibrators
                var numofvib = Vibrator.NumberOfVibrators;
                // Gets the control of the specific vibrator
                Vibrator vib = Vibrator.Vibrators[0];
                int wait_time;

                // Maximum vibration duration is 9999
                if (time < 9999)
                {
                    wait_time = time;
                }
                else
                {
                    wait_time = 9999;
                }

                // Show waiting page during vibration
                await this.Navigation.PushAsync(new SimpleResult("Testing... " + wait_time + "ms"));

                // Wait until vibration is over
                var t = Task.Run(async delegate
                {
                    await Task.Delay(wait_time);
                    return;
                });

                // Vibrates during the user input time with a constant intensity
                vib.Vibrate(time, 100);
                t.Wait();
                // Stops the vibration
                vib.Stop();

                // Operations are succeed
                await this.Navigation.PushAsync(new SimpleResult("Succeed: Haptic(" + wait_time + "ms)"));
            }
            catch (Exception e)
            {
                // Operations are failed
                await this.Navigation.PushAsync(new SimpleResult("Failed: Haptic\n" + e.Message));
            }
        }
	}
}
