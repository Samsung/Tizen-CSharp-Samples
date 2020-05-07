using System;
using System.Collections.Generic;
using System.Linq;

namespace SquatCounter.Services
{
    public class SquatCounterService
    {
        private const float Accuracy = 0.030F;
        private const int WindowSize = 10;

        private PressureSensorService _pressureService;
        private Queue<float> _pressureWindow;

        private float _upperThreshold;
        private float _lowerThreshold;
        private bool _valueExceededUpperThreshold;
        private bool _isServiceCalibrated;

        public event EventHandler<int> SquatsUpdated;

        public int SquatsCount { get; private set; }

        public SquatCounterService()
        {
            _pressureWindow = new Queue<float>();

            _pressureService = new PressureSensorService();
            _pressureService.ValueUpdated += PressureSensorUpdated;
        }

        public void Start()
        {
            _pressureService.ValueUpdated += PressureSensorUpdated;
        }

        public void Stop()
        {
            _pressureService.ValueUpdated -= PressureSensorUpdated;
        }

        public void Reset()
        {
            SquatsCount = 0;
            SquatsUpdated.Invoke(this, SquatsCount);
        }

        public void Dispose()
        {
            _pressureService.ValueUpdated -= PressureSensorUpdated;
            _pressureService?.Dispose();
        }

        private void PressureSensorUpdated(object sender, float pressure)
        {
            _pressureWindow.Enqueue(pressure);

            if(_pressureWindow.Count < WindowSize)
            {
                return;
            }
            else if(_pressureWindow.Count == WindowSize && !_isServiceCalibrated)
            {
                CalibrateSerivce();
            }

            AnalyzeNewWindow();

            _pressureWindow.Dequeue();
        }

        private void AnalyzeNewWindow()
        {
            float average = CalculateStableAverage();

            if(average <= _upperThreshold && average >= _lowerThreshold && _valueExceededUpperThreshold)
            {
                _valueExceededUpperThreshold = false;
                SquatsCount++;
                SquatsUpdated.Invoke(this, SquatsCount);
            }
            else if(average > _upperThreshold)
            {
                _valueExceededUpperThreshold = true; 
            }
        }

        private void CalibrateSerivce()
        {
            float average = CalculateStableAverage();
            _upperThreshold = average + Accuracy;
            _lowerThreshold = average - Accuracy;
            _isServiceCalibrated = true;
        }

        private float CalculateStableAverage()
        {
            float minPressure = _pressureWindow.Min();
            float maxPressure = _pressureWindow.Max();
            float sum = _pressureWindow.Sum();

            return (sum - minPressure - maxPressure) / (WindowSize - 2);
        }
    }
}
