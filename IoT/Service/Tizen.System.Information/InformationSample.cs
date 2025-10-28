using Tizen;
using Tizen.Applications;
using Tizen.System;
using System;


namespace InformationSample
{
    class App : ServiceApplication
    {
        private const string LogTag = "InformationSample";

        private void SystemInformationSample()
		{
			string key;

			key = "http://tizen.org/system/build.id";
			if (Information.TryGetValue(key, out string string_value))
			{
				Log.Info(LogTag, $"Key: {key}");
				Log.Info(LogTag, $"Value: {string_value}");
				Log.Info(LogTag, "---");
			}

			key = "http://tizen.org/feature/camera.back.flash";
			if (Information.TryGetValue(key, out bool boolean_value))
			{
				Log.Info(LogTag, $"Key: {key}");
				Log.Info(LogTag, $"Value: {boolean_value}");
				Log.Info(LogTag, "---");
			}

			key = "http://tizen.org/system/sound.ringtone.volume.resolution.max";
			if (Information.TryGetValue(key, out int integer_value))
			{
				Log.Info(LogTag, $"Key: {key}");
				Log.Info(LogTag, $"Value: {integer_value}");
				Log.Info(LogTag, "---");
			}
		}

        private void MemoryUsageSample()
		{
			SystemMemoryUsage usage = new SystemMemoryUsage();

			usage.Update();

			int total = usage.Total;
			int used = usage.Used;
			int free = usage.Free;
			int cache = usage.Cache;
			int swap = usage.Swap;

			Log.Info(LogTag, $"Total: {total} KiB");
			Log.Info(LogTag, $"Used: {used} KiB");
			Log.Info(LogTag, $"Free: {free} KiB");
			Log.Info(LogTag, $"Cache: {cache} KiB");
			Log.Info(LogTag, $"Swap: {swap} KiB");
		}

        private void CpuUsageSample()
		{
			SystemCpuUsage usage = new SystemCpuUsage();

			usage.Update();

			double user = usage.User;
			double system = usage.System;
			double nice = usage.Nice;
			double iowait = usage.IoWait;
			int processor_count = usage.ProcessorCount;
			int frequency_core0 = usage.GetCurrentFrequency(0);
			int max_frequency_core0 = usage.GetMaxFrequency(0);

			Log.Info(LogTag, $"User: {user}%");
			Log.Info(LogTag, $"System: {system}%");
			Log.Info(LogTag, $"Nice: {nice}%");
			Log.Info(LogTag, $"IoWait: {iowait}%");
			Log.Info(LogTag, $"Processor Count: {processor_count}");
			Log.Info(LogTag, $"Frequency (Core 0): {frequency_core0} MHz");
			Log.Info(LogTag, $"Max Frequency (Core 0): {max_frequency_core0} MHz");
		}

		private void AudioJackConnectedCallback(object sender, RuntimeFeatureStatusChangedEventArgs args)
		{
			if (Information.TryGetValue(args.Key, out bool isConnected))
			{
				Log.Info(LogTag, $"AudioJack Status Changed - Key: {args.Key}, Connected: {isConnected}");
				
				if (isConnected)
				{
					Log.Info(LogTag, "AudioJack is now connected");
					
					string audioJackTypeKey = "http://tizen.org/runtimefeature/audiojack.type";
					if (Information.TryGetValue(audioJackTypeKey, out string jackType))
					{
						Log.Info(LogTag, $"AudioJack Type: {jackType}");
					}
				}
				else
				{
					Log.Info(LogTag, "AudioJack is now disconnected");
				}
			}
			else
			{
				Log.Error(LogTag, $"Failed to get current value for {args.Key}");
			}
		}

		private void RuntimeFeatureMonitorStart()
		{
			string key = "http://tizen.org/runtimefeature/audiojack.connected";

			Information.SetCallback(key, AudioJackConnectedCallback);
		}

		private void RuntimeFeatureMonitorStop()
		{
			string key = "http://tizen.org/runtimefeature/audiojack.connected";

			Information.UnsetCallback(key, AudioJackConnectedCallback);
		}

        protected override void OnCreate()
        {
            base.OnCreate();

			Log.Info(LogTag, "SystemInformationSample");
			SystemInformationSample();

			Log.Info(LogTag, "MemoryUsageSample");
			MemoryUsageSample();

			Log.Info(LogTag, "CpuUsageSample");
			CpuUsageSample();

			Log.Info(LogTag, "RuntimeFeatureMonitorStart");
			RuntimeFeatureMonitorStart();
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
            RuntimeFeatureMonitorStop();

            base.OnTerminate();
        }

        static void Main(string[] args)
        {
            App app = new App();
            app.Run(args);
        }
    }
}
