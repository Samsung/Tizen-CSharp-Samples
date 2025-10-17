using System;
using System.Linq;
using Tizen.Applications;
using Tizen.System;
using Tizen;

namespace FeedbackSample
{
    class App : ServiceApplication
    {
        string[] patterns = new string[39] { "Tap", "SoftInputPanel", "Key0", "Key1", "Key2", "Key3", "Key4", "Key5", "Key6", "Key7",
            "Key8", "Key9", "KeyStar", "KeySharp", "KeyBack", "Hold", "HardwareKeyPressed", "HardwareKeyHold", "Message", "Email",
            "WakeUp", "Schedule", "Timer", "General", "PowerOn", "PowerOff", "ChargerConnected", "ChargingError", "FullyCharged", "LowBattery",
            "Lock", "UnLock", "VibrationModeAbled", "SilentModeDisabled", "BluetoothDeviceConnected", "BluetoothDeviceDisconnected", "ListReorder", "ListSlider", "VolumeKeyPressed"};

	private void SamplePlayStopTest()
	{
            Feedback feedback = new Feedback();
            bool support;
	    bool support_vibration;
	    bool ret;
	    bool value;

	    Log.Info("FEEDBACK_SAMPLE", "====Play & Stop Vibratoin Test====");
            Log.Info("FEEDBACK_SAMPLE", "");

            ret = Information.TryGetValue<bool>("http://tizen.org/feature/feedback.vibration", out value);

	    if (ret && value)
		support_vibration = true;
	    else
		support_vibration = false;

	    if (!support_vibration)
	    {
		Log.Warn("FEEDBACK_SAMPLE", "Vibration is not supported, so stop the test");
		return;
	    }

	    try
	    {
                foreach (FeedbackType type in Enum.GetValues(typeof(FeedbackType)))
                {
                    if (type == FeedbackType.All)
                        continue;

                    foreach (string pattern in patterns)
                    {
                        support = feedback.IsSupportedPattern(type, pattern);

                        if (support) {
			    Log.Info("FEEDBACK_SAMPLE", $"Play: {pattern}");
                            feedback.Play(type, pattern);
			}
                    }
                    
		    Log.Info("FEEDBACK_SAMPLE", "Stop all patterns");
		    feedback.Stop();
		}
	    }
	    catch (Exception ex)
	    {
	        Log.Error("FEEDBACK_SAMPLE", $"Error: {ex.Message}"); 
	    }
            
	    Log.Info("FEEDBACK_SAMPLE", "");
	}

        private void SampleTest()
        {
	    SamplePlayStopTest();
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            Log.Info("FEEDBACK_SAMPLE", "App: Created");

            SampleTest();
        }

        protected override void OnAppControlReceived(AppControlReceivedEventArgs e)
        {
            base.OnAppControlReceived(e);
        }

        protected override void OnDeviceOrientationChanged(DeviceOrientationEventArgs e)
        {
            base.OnDeviceOrientationChanged(e);
        }

        protected override void OnLocaleChanged(LocaleChangedEventArgs e)
        {
            base.OnLocaleChanged(e);
        }

        protected override void OnLowBattery(LowBatteryEventArgs e)
        {
            base.OnLowBattery(e);
        }

        protected override void OnLowMemory(LowMemoryEventArgs e)
        {
            base.OnLowMemory(e);
        }

        protected override void OnRegionFormatChanged(RegionFormatChangedEventArgs e)
        {
            base.OnRegionFormatChanged(e);
        }

        protected override void OnTerminate()
        {
            base.OnTerminate();
            Log.Info("FEEDBACK_SAMPLE", "App: Terminated");
        }

        static void Main(string[] args)
        {
            App app = new App();
            app.Run(args);
        }
    }
}
