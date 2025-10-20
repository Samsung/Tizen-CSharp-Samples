using System;
using Tizen;
using Tizen.Applications;
using Tizen.System;
using Tizen.Sensor;

namespace SensorSample
{
    class App : ServiceApplication
    {
        public const string LOGTAG = "SENSOR_SAMPLE";
        private Accelerometer SensorAccelerometer = null;
        EventHandler<AccelerometerDataUpdatedEventArgs> SensorAccelerometerHandler = null;

        private void StartSensorListen()
        {
            try
            {
                SensorAccelerometer = new Accelerometer();
            }
            catch (NotSupportedException)
            {
                Log.Info(LOGTAG, "Accelerometer is not supported");
                SensorAccelerometer = null;
                return;
            }

            SensorAccelerometerHandler = (sender, e) =>
            {
                Log.Info(LOGTAG, "Accelerometer: (" + e.X + ", " + e.Y + ", " + e.Z + ")");
            };

            SensorAccelerometer.DataUpdated += SensorAccelerometerHandler;

            SensorAccelerometer.Start();

            Log.Info(LOGTAG, "Sensor started");
        }

        private void StopSensorListen()
        {
            if (SensorAccelerometer == null)
            {
                return;
            }

            SensorAccelerometer.Stop();
            SensorAccelerometer.DataUpdated -= SensorAccelerometerHandler;
            SensorAccelerometer.Dispose();
            SensorAccelerometer = null;

            Log.Info(LOGTAG, "Sensor stopped");
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            StartSensorListen();
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
            StopSensorListen();

            base.OnTerminate();
        }

        static void Main(string[] args)
        {
            App app = new App();
            app.Run(args);
        }
    }
}
