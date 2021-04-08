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
    /// <summary>
    /// Main page of device application
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : CirclePage
	{
        /// <summary>
        /// The constructor of MainPage
        /// </summary>
		public MainPage()
		{
			InitializeComponent();
			CreateListView();
		}

        /// <summary>
        /// Create list view of device feature
        /// </summary>
        private void CreateListView()
        {
            // Create device feature list
            // Battery, Display, Haptic, IR, Led & Camera back flash
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

        /// <summary>
        /// Method for tapped event of device feature list item
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            FeatureItem item = (FeatureItem)e.Item;
            if (item.Name.Equals("Battery"))
            {
                // Show battery status
                BatterySample();
            }
            else if (item.Name.Equals("Display"))
            {
                // Show display status
                DisplaySample();
            }
            else if (item.Name.Equals("Haptic"))
            {
                // Vibrate during user input time
                HapticSample();
            }
            else if (item.Name.Equals("IR"))
            {
                // Transmit IR patterns
                IRSample();
            }
            else if (item.Name.Equals("Led"))
            {
                // Play and stop led
                LedSample();
            }
            else if (item.Name.Equals("Camera back flash"))
            {
                // Show camera back flash status
                BackflashSample();
            }
            else
            {
                // Unknown feature
                this.Navigation.PushAsync(new SimpleResult("Wrong operation"));
            }
        }

        /// <summary>
        /// Method for battery feature
        /// </summary>
        private void BatterySample()
        {
            bool value;
            // Checks the battery feature is supported in this device
            var result = Information.TryGetValue<bool>("http://tizen.org/feature/battery", out value);
            if (!result || !value)
            {
                // Battery is not supported
                this.Navigation.PushAsync(new SimpleResult("Wearable doesn't support battery feature"));
                return;
            }

            try
            {
                // Gets the current battery level
                BatteryLevelStatus level = Battery.Level;
                // Gets the battery charge percentage
                int percent = Battery.Percent;
                if (percent < 0 || percent > 100)
                {
                    // Battery percent value is out of bound
                    this.Navigation.PushAsync(new SimpleResult("Failed: Battery\nBattery percent value is invalid"));
                }
                // Gets the current charging state
                bool isCharging = Battery.IsCharging;
                string level_str, isCharing_str;

                // Gets the text of battery level value
                if (level == BatteryLevelStatus.Critical)
                {
                    level_str = "Critical";
                }
                else if (level == BatteryLevelStatus.Empty)
                {
                    level_str = "Empty";
                }
                else if (level == BatteryLevelStatus.Full)
                {
                    level_str = "Full";
                }
                else if (level == BatteryLevelStatus.High)
                {
                    level_str = "High";
                }
                else if (level == BatteryLevelStatus.Low)
                {
                    level_str = "Low";
                }
                else
                {
                    level_str = "Invalid";
                }

                // Gets the text of battery charging state
                if (isCharging)
                {
                    isCharing_str = "charging";
                }
                else
                {
                    isCharing_str = "not charging";
                }

                // Operations are succeed
                this.Navigation.PushAsync(new SimpleResult("Battery level: " + level_str + "\nPercent: " + percent + "\nBattery is " + isCharing_str));
            }
            catch (Exception e)
            {
                // Operations are failed
                this.Navigation.PushAsync(new SimpleResult("Failed: Battery\n" + e.Message));
            }
        }

        /// <summary>
        /// Method for display feature
        /// </summary>
        private void DisplaySample()
        {
            try
            {
                // Gets the number of available display devices
                int numofDisplay = Display.NumberOfDisplays;
                int maxBrightness = -1, old = -1, current = -1;
                foreach (Display dis in Display.Displays)
                {
                    // Gets the maximum brightness value for the specific display
                    maxBrightness = dis.MaxBrightness;
                    // Gets the brightness value of the specific display
                    old = dis.Brightness;
                    // Sets the brightness value of the specific display
                    dis.Brightness = old - 10;
                    current = dis.Brightness;
                    break;
                }

                if (maxBrightness < 0 || current < 0)
                {
                    // Max brightness and current brightness value is out of bound
                    this.Navigation.PushAsync(new SimpleResult("Failed: Display\nGetting brightness is failed"));
                }

                // Sets the display state of the specific display
                Display.State = DisplayState.Normal;
                // Gets the display state of the specific display
                DisplayState state = Display.State;
                // Display state should be Normal
                if (state != DisplayState.Normal)
                {
                    // Operations are failed
                    this.Navigation.PushAsync(new SimpleResult("Failed: Display\nDisplayState has wrong value"));
                }

                // Operations are succeed
                this.Navigation.PushAsync(new SimpleResult("Number of display: " + numofDisplay + "\n Max brightness: " + maxBrightness + "\nOld: " + old + "\nCurrent: " + current + "\nState: Normal"));
            }
            catch (Exception e)
            {
                // Operations are failed
                this.Navigation.PushAsync(new SimpleResult("Failed: Display\n" + e.Message));
            }
        }

        /// <summary>
        /// Method for haptic feature
        /// </summary>
        private void HapticSample()
        {
            bool value;
            // Checks the haptic feature is supported in this device
            var result = Information.TryGetValue<bool>("http://tizen.org/feature/feedback.vibration", out value);
            if (!result || !value)
            {
                // Haptic is not supported
                this.Navigation.PushAsync(new SimpleResult("Wearable doesn't support haptic feature"));
                return;
            }

            // Create haptic page for user input
            this.Navigation.PushAsync(new HapticPage());
        }

        /// <summary>
        /// Method for IR feature
        /// </summary>
        private void IRSample()
        {
            bool value;
            // Checks the IR feature is supported in this device
            var result = Information.TryGetValue<bool>("http://tizen.org/feature/consumer_ir", out value);
            if (!result || !value)
            {
                // IR is not supported
                this.Navigation.PushAsync(new SimpleResult("Wearable doesn't support IR feature"));
                return;
            }

            try
            {
                // Gets the information whether the IR module is available
                result = IR.IsAvailable;
                if (!result)
                {
                    // IR should be available
                    this.Navigation.PushAsync(new SimpleResult("Failed: IR\nIR should be available"));
                }

                List<int> pattern = new List<int>();
                pattern.Add(10);
                pattern.Add(50);
                // Transmits the IR command
                IR.Transmit(10, pattern);

                // Operations are succeed
                this.Navigation.PushAsync(new SimpleResult("Succeed: IR"));
            }
            catch (Exception e)
            {
                // Operations are failed
                this.Navigation.PushAsync(new SimpleResult("Failed: IR\n" + e.Message));
            }
        }

        /// <summary>
        ///  Method for led feature
        /// </summary>
        private void LedSample()
        {
            bool value;
            // Checks the led feature is supported in this device
            var result = Information.TryGetValue<bool>("http://tizen.org/feature/led", out value);
            var t = Task.Run(async delegate
            {
                await Task.Delay(300);
                return;
            });

            if (!result || !value)
            {
                // Led is not supported
                this.Navigation.PushAsync(new SimpleResult("Wearable doesn't support led feature"));
                return;
            }

            try
            {
                // Plays the LED that is located at the front of the device
                Led.Play(500, 200, Tizen.Common.Color.FromRgba(255, 255, 255, 1));
                // Wait 300ms
                t.Wait();
                // Stops the LED
                Led.Stop();

                // Operations are succeed
                this.Navigation.PushAsync(new SimpleResult("Succeed: Led"));
            }
            catch (Exception e)
            {
                // Operations are failed
                this.Navigation.PushAsync(new SimpleResult("Failed: Led\n" + e.Message));
            }
        }

        /// <summary>
        /// Method for camera back flash feature
        /// </summary>
        private void BackflashSample()
        {
            bool value;
            // Checks the camera back flash feature is supported in this device
            var result = Information.TryGetValue<bool>("http://tizen.org/feature/camera.back.flash", out value);
            if (!result || !value)
            {
                // Camera back flash is not supported
                this.Navigation.PushAsync(new SimpleResult("Wearable doesn't support camera back flash feature"));
                return;
            }

            try
            {
                // Gets the maximum brightness value of the LED that is located next to the camera
                var max = Led.MaxBrightness;
                // Gets the current brightness value of the LED that is located next to the camera
                var old_bright = Led.Brightness;

                EventHandler<LedBrightnessChangedEventArgs> handler = null;
                handler = (object sender, LedBrightnessChangedEventArgs args) =>
                {
                    if (Led.Brightness != 50)
                    {
                        // Operations are failed
                        this.Navigation.PushAsync(new SimpleResult("Failed: Camera back flash\nBrightness value is wrong"));
                    }
                    // Removes a handler for brightness changes
                    Led.BrightnessChanged -= handler;

                    // Operations are succeed
                    this.Navigation.PushAsync(new SimpleResult("Succeed: Camera back flash"));
                };
                // Adds a handler for brightness changes
                Led.BrightnessChanged += handler;
                // Gets the current brightness value of the LED that is located next to the camera
                Led.Brightness = 50;
            }
            catch (Exception e)
            {
                // Operations are failed
                this.Navigation.PushAsync(new SimpleResult("Failed: Camera back flash\n" + e.Message));
            }
        }
    }
}
