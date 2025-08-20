/*
* Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using Sensor.Tizen.Mobile;
using Sensor.Tizen.Mobile.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tizen.Sensor;

[assembly: Xamarin.Forms.Dependency(implementorType: typeof(SensorManager))]

namespace Sensor.Tizen.Mobile
{
    /// <summary>
    /// SensorManager Implementation class
    /// </summary>
    public class SensorManager : ISensorManager
    {
        private static Dictionary<string, ISensorAdapter> sensorList = new Dictionary<string, ISensorAdapter>();

        /// <summary>
        /// Initialize sensor list.
        /// </summary>
        private void Initialize()
        {
            if (Accelerometer.IsSupported)
            {
                sensorList.Add(SensorTypes.AccelerometerType, new AccelerometerAdapter());
            }

            if (FaceDownGestureDetector.IsSupported)
            {
                sensorList.Add(SensorTypes.FaceDownGestureDetectorType, new FaceDownGestureDetectorAdapter());
            }

            if (GravitySensor.IsSupported)
            {
                sensorList.Add(SensorTypes.GravitySensorType, new GravitySensorAdapter());
            }

            if (Gyroscope.IsSupported)
            {
                sensorList.Add(SensorTypes.GyroscopeType, new GyroscopeAdapter());
            }

            if (GyroscopeRotationVectorSensor.IsSupported)
            {
                sensorList.Add(SensorTypes.GyroscopeRotationVectorType, new GyroscopeRotationVectorSensorAdapter());
            }

            if (HeartRateMonitor.IsSupported)
            {
                sensorList.Add(SensorTypes.HeartRateMonitorType, new HeartRateMonitorAdapter());
            }

            if (HumiditySensor.IsSupported)
            {
                sensorList.Add(SensorTypes.HumiditySensorType, new HumiditySensorAdapter());
            }

            if (InVehicleActivityDetector.IsSupported)
            {
                sensorList.Add(SensorTypes.InVehicleActivityDetectorType, new InVehicleActivityDetectorAdapter());
            }

            if (LightSensor.IsSupported)
            {
                sensorList.Add(SensorTypes.LightSensorType, new LightSensorAdapter());
            }

            if (LinearAccelerationSensor.IsSupported)
            {
                sensorList.Add(SensorTypes.LinearAccelerationSensorType, new LinearAccelerationSensorAdapter());
            }

            if (Magnetometer.IsSupported)
            {
                sensorList.Add(SensorTypes.MagnetometerType, new MagnetometerAdapter());
            }

            if (MagnetometerRotationVectorSensor.IsSupported)
            {
                sensorList.Add(SensorTypes.MagnetometerRotationVectorType, new MagnetometerRotationVectorSensorAdapter());
            }

            if (OrientationSensor.IsSupported)
            {
                sensorList.Add(SensorTypes.OrientationSensorType, new OrientationSensorAdapter());
            }

            if (Pedometer.IsSupported)
            {
                sensorList.Add(SensorTypes.PedometerType, new PedometerAdapter());
            }

            if (PickUpGestureDetector.IsSupported)
            {
                sensorList.Add(SensorTypes.PickUpGestureDetectorType, new PickUpGestureDetectorAdapter());
            }

            if (PressureSensor.IsSupported)
            {
                sensorList.Add(SensorTypes.PressureSensorType, new PressureSensorAdapter());
            }

            if (ProximitySensor.IsSupported)
            {
                sensorList.Add(SensorTypes.ProximitySensorType, new ProximitySensorAdapter());
            }

            if (RotationVectorSensor.IsSupported)
            {
                sensorList.Add(SensorTypes.RotationVectorSensorType, new RotationVectorSensorAdapter());
            }

            if (RunningActivityDetector.IsSupported)
            {
                sensorList.Add(SensorTypes.RunningActivityDetectorType, new RunningActivityDetectorAdapter());
            }

            if (SleepMonitor.IsSupported)
            {
                sensorList.Add(SensorTypes.SleepMonitorType, new SleepMonitorAdapter());
            }

            if (StationaryActivityDetector.IsSupported)
            {
                sensorList.Add(SensorTypes.StationaryActivityDetectorType, new StationaryActivityDetectorAdapter());
            }

            if (TemperatureSensor.IsSupported)
            {
                sensorList.Add(SensorTypes.TemperatureSensorType, new TemperatureSensorAdapter());
            }

            if (UltravioletSensor.IsSupported)
            {
                sensorList.Add(SensorTypes.UltravioletSensorType, new UltravioletSensorAdapter());
            }

            if (UncalibratedGyroscope.IsSupported)
            {
                sensorList.Add(SensorTypes.UncalibratedGyroscopeType, new UncalibratedGyroscopeAdapter());
            }

            if (UncalibratedMagnetometer.IsSupported)
            {
                sensorList.Add(SensorTypes.UncalibratedMagnetometerType, new UncalibratedMagnetometerAdapter());
            }

            if (WalkingActivityDetector.IsSupported)
            {
                sensorList.Add(SensorTypes.WalkingActivityDetectorType, new WalkingActivityDetectorAdapter());
            }

            if (WristUpGestureDetector.IsSupported)
            {
                sensorList.Add(SensorTypes.WristUpGestureDetectorType, new WristUpGestureDetectorAdapter());
            }
        }

        /// <summary>
        /// Gets supported sensor types on the current device.
        /// </summary>
        /// <returns>Sensor types</returns>
        public List<string> GetSensorTypeList()
        {
            List<string> sensorTypeList = new List<string>();

            PrivacyChecker.CheckPermission("http://tizen.org/privilege/healthinfo");

            Initialize();

            foreach (var sensor in sensorList)
            {
                sensorTypeList.Add(sensor.Key);
            }

            return sensorTypeList;
        }

        /// <summary>
        /// Gets sensor information.
        /// </summary>
        /// <param name="type">Sensor type</param>
        /// <returns>Sensor Information</returns>
        public SensorInfo GetSensorInfo(string type)
        {
            return sensorList[type].GetSensorInfo();
        }

        /// <summary>
        /// Start sensor.
        /// </summary>
        /// <param name="type">Sensor type</param>
        /// <param name="listener">Event handler</param>
        public void StartSensor(string type, EventHandler<SensorEventArgs> listener)
        {
            sensorList[type].Start(listener);
        }

        /// <summary>
        /// Stop sensor.
        /// </summary>
        /// <param name="type">Sensor type</param>
        /// <param name="listener">Event handler</param>
        public void StopSensor(string type, EventHandler<SensorEventArgs> listener)
        {
            sensorList[type].Stop(listener);
        }
    }
}
