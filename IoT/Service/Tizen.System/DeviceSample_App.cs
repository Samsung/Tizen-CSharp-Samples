using Tizen;
using Tizen.Applications;

namespace DeviceSample
{
    class App : ServiceApplication
    {
        private const string LogTag = "DeviceSample";

        protected override void OnCreate()
        {
            base.OnCreate();
            Log.Info(LogTag, "[Main] Starting Device Sample Application");
            
            BatterySample.Run();
            DisplaySample.Run();
            HapticSample.Run();
            IrSample.Run();
            LedSample.Run();
            PowerSample.Run();
            
            Log.Info(LogTag, "[Main] All samples completed");
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
        }

        static void Main(string[] args)
        {
            App app = new App();
            app.Run(args);
        }
    }
}
