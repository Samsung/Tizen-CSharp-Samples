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
	public partial class PowerLockPage : CirclePage
	{
		public PowerLockPage ()
		{
			InitializeComponent ();
		}

        public void NormalClicked(object sender, EventArgs args)
        {
            var t = Task.Run(async delegate
            {
                await Task.Delay(1000);
                return;
            });
            try
            {
                Power.RequestLock(PowerLock.DisplayNormal, 1000);
                t.Wait();
                Power.ReleaseLock(PowerLock.DisplayNormal);
                this.Navigation.PushAsync(new SimpleResult("Succeed: Power Lock"));
            }
            catch (Exception e)
            {
                this.Navigation.PushAsync(new SimpleResult("Failed: Power\n" + e.Message));
            }
        }

        public void DimClicked(object sender, EventArgs args)
        {
            var t = Task.Run(async delegate
            {
                await Task.Delay(1000);
                return;
            });
            try
            {
                Power.RequestLock(PowerLock.DisplayDim, 1000);
                t.Wait();
                Power.ReleaseLock(PowerLock.DisplayDim);
                this.Navigation.PushAsync(new SimpleResult("Succeed: Power Lock"));
            }
            catch (Exception e)
            {
                this.Navigation.PushAsync(new SimpleResult("Failed: Power\n" + e.Message));
            }
        }
	}
}