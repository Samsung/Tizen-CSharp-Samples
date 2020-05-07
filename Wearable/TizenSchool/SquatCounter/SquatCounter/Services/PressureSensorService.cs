using Tizen.Sensor;
using System;

namespace SquatCounter.Services
{
    public class PressureSensorService
    {
        private readonly PressureSensor _pressureSensor;

        private const string NotSupportedMsg = "Pressure sensor is not supported on your device.";

        public event EventHandler<float> ValueUpdated;

        private const int Interval = 10;

        public PressureSensorService()
        {
            if(!PressureSensor.IsSupported)
            {
                throw new NotSupportedException(NotSupportedMsg);
            }

            _pressureSensor = new PressureSensor();
            _pressureSensor.DataUpdated += PressureSensorUpdated;
            _pressureSensor.Interval = Interval;
            _pressureSensor.Start();
        }

        private void PressureSensorUpdated(object sender, PressureSensorDataUpdatedEventArgs e)
        {
            ValueUpdated?.Invoke(this, e.Pressure);
        }

        public void Dispose()
        {
            _pressureSensor?.Stop();
            _pressureSensor.DataUpdated -= PressureSensorUpdated;
            _pressureSensor?.Dispose();
        }
    }
}
