using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.System;

namespace DeviceApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : CirclePage
	{
		public MainPage ()
		{
			InitializeComponent ();

            CreateListView();
        }

        private void CreateListView()
        {
            List<FeatureItem> featureItems = new List<FeatureItem>
            {
                new FeatureItem("Battery"),
                new FeatureItem("Display"),
                new FeatureItem("Haptic"),
                new FeatureItem("IR"),
                new FeatureItem("Led"),
                new FeatureItem("Camera back flash"),
            };
            DeviceFeatureList.ItemsSource = featureItems;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            FeatureItem item = (FeatureItem) e.Item;
            if (item.Name.Equals("Battery"))
                BatterySample();
            else if (item.Name.Equals("Display"))
                DisplaySample();
            else if (item.Name.Equals("Haptic"))
                HapticSample();
            else if (item.Name.Equals("IR"))
                IRSample();
            else if (item.Name.Equals("Led"))
                LedSample();
            else if (item.Name.Equals("Camera back flash"))
                BackflashSample();
            else
                this.Navigation.PushAsync(new SimpleResult("Wrong operation"));
        }

        private void BatterySample()
        {
            bool value;
            var result = Information.TryGetValue<bool>("http://tizen.org/feature/battery", out value);
            if (!result || !value)
            {
                this.Navigation.PushAsync(new SimpleResult("Wearable doesn't support battery feature"));
                return;
            }

            try
            {
                BatteryLevelStatus level = Battery.Level;
                int percent = Battery.Percent;
                if (percent < 0 || percent > 100)
                    this.Navigation.PushAsync(new SimpleResult("Failed: Battery\nBattery percent value is invalid"));
                bool isCharging = Battery.IsCharging;
                string level_str, isCharing_str;

                if (level == BatteryLevelStatus.Critical)
                    level_str = "Critical";
                else if (level == BatteryLevelStatus.Empty)
                    level_str = "Empty";
                else if (level == BatteryLevelStatus.Full)
                    level_str = "Full";
                else if (level == BatteryLevelStatus.High)
                    level_str = "High";
                else if (level == BatteryLevelStatus.Low)
                    level_str = "Low";
                else
                    level_str = "Invalid";

                if (isCharging)
                    isCharing_str = "charging";
                else
                    isCharing_str = "not charging";

                this.Navigation.PushAsync(new SimpleResult("Battery level: " + level_str + "\nPercent: " + percent + "\nBattery is " + isCharing_str));
            }
            catch (Exception e)
            {
                this.Navigation.PushAsync(new SimpleResult("Failed: Battery\n" + e.Message));
            }
        }

        private void DisplaySample()
        {
            try
            {
                int numofDisplay = Display.NumberOfDisplays;
                int maxBrightness = -1, old = -1, current = -1;
                foreach (Display dis in Display.Displays)
                {
                    maxBrightness = dis.MaxBrightness;
                    old = dis.Brightness;
                    dis.Brightness = old - 10;
                    current = dis.Brightness;
                    break;
                }
                if (maxBrightness < 0 || current < 0)
                    this.Navigation.PushAsync(new SimpleResult("Failed: Display\nGetting brightness is failed"));

                Display.State = DisplayState.Normal;
                DisplayState state = Display.State;
                if (state != DisplayState.Normal)
                    this.Navigation.PushAsync(new SimpleResult("Failed: Display\nDisplayState has wrong value"));

                this.Navigation.PushAsync(new SimpleResult("Number of display: " + numofDisplay + "\n Max brightness: " + maxBrightness + "\nOld: " + old + "\nCurrent: " + current + "\nState: Normal"));
            }
            catch (Exception e)
            {
                this.Navigation.PushAsync(new SimpleResult("Failed: Display\n" + e.Message));
            }
        }

        private void HapticSample()
        {
            bool value;
            var result = Information.TryGetValue<bool>("http://tizen.org/feature/feedback.vibration", out value);
            if (!result || !value)
            {
                this.Navigation.PushAsync(new SimpleResult("Wearable doesn't support haptic feature"));
                return;
            }

            this.Navigation.PushAsync(new HapticPage());
        }

        private void IRSample()
        {
            bool value;
            var result = Information.TryGetValue<bool>("http://tizen.org/feature/consumer_ir", out value);
            if (!result || !value)
            {
                this.Navigation.PushAsync(new SimpleResult("Wearable doesn't support IR feature"));
                return;
            }

            try
            {
                result = IR.IsAvailable;
                if (!result)
                    this.Navigation.PushAsync(new SimpleResult("Failed: IR\nIR should be available"));

                List<int> pattern = new List<int>();
                pattern.Add(10);
                pattern.Add(50);
                IR.Transmit(10, pattern);

                this.Navigation.PushAsync(new SimpleResult("Succeed: IR"));
            }
            catch (Exception e)
            {
                this.Navigation.PushAsync(new SimpleResult("Failed: IR\n" + e.Message));
            }
        }

        private void LedSample()
        {
            bool value;
            var result = Information.TryGetValue<bool>("http://tizen.org/feature/led", out value);
            var t = Task.Run(async delegate
            {
                await Task.Delay(300);
                return;
            });

            if (!result || !value)
            {
                this.Navigation.PushAsync(new SimpleResult("Wearable doesn't support led feature"));
                return;
            }

            try
            {
                Led.Play(500, 200, Tizen.Common.Color.FromRgba(255, 255, 255, 1));
                t.Wait();
                Led.Stop();
            }
            catch (Exception e)
            {
                this.Navigation.PushAsync(new SimpleResult("Failed: Led\n" + e.Message));
            }
        }

        private void BackflashSample()
        {
            bool value;
            var result = Information.TryGetValue<bool>("http://tizen.org/feature/camera.back.flash", out value);
            if (!result || !value)
            {
                this.Navigation.PushAsync(new SimpleResult("Wearable doesn't support camera back flash feature"));
                return;
            }

            try
            {
                var max = Led.MaxBrightness;
                var old_bright = Led.Brightness;

                EventHandler<LedBrightnessChangedEventArgs> handler = null;
                handler = (object sender, LedBrightnessChangedEventArgs args) =>
                {
                    if (Led.Brightness != 50)
                        this.Navigation.PushAsync(new SimpleResult("Failed: Camera back flash\nBrightness value is wrong"));
                    Led.BrightnessChanged -= handler;

                    this.Navigation.PushAsync(new SimpleResult("testtest"));
                };
                Led.BrightnessChanged += handler;
                Led.Brightness = 50;
            }
            catch (Exception e)
            {
                this.Navigation.PushAsync(new SimpleResult("Failed: Camera back flash\n" + e.Message));
            }
        }
    }
}