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
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HapticPage : CirclePage
	{
        HapticInput input = new HapticInput();
        public HapticPage()
        {
            InitializeComponent();
            BindingContext = input;
        }

        public void OKClicked(object sender, EventArgs args)
        {
            if (input.Time == null)
                this.Navigation.PushAsync(new SimpleResult("Failed: Haptic\nWrong Input"));
            else
                HapticOperation((int)input.Time);
        }


        private async void HapticOperation(int time)
        {
            try
            {
                await this.Navigation.PushAsync(new SimpleResult("Testing..."));

                var numofvib = Vibrator.NumberOfVibrators;
                Vibrator vib = Vibrator.Vibrators[0];
                int wait_time;

                if (time < 9999)

                    wait_time = time;
                else
                    wait_time = 9999;

                var t = Task.Run(async delegate
                {
                    await Task.Delay(wait_time);
                    return;
                });

                vib.Vibrate(time, 100);
                t.Wait();
                vib.Stop();

                await this.Navigation.PushAsync(new SimpleResult("Succeed: Haptic(" + wait_time + "ms)"));
            }
            catch (Exception e)
            {
                await this.Navigation.PushAsync(new SimpleResult("Failed: Haptic\n" + e.Message));
            }
        }
	}
}