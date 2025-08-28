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

using System.Collections.Generic;
using System.Collections.ObjectModel;

using Tizen.NUI;
using Tizen.NUI.Components;
using Sensor.Tizen.Mobile;
using System.ComponentModel;

namespace Sensor.Pages
{
    public class ItemClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ItemClass(string type, Color itemColor)
        {
            Text = type;
            BgColor = itemColor;
        }

        string text;
        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        Color color;
        public Color BgColor
        {
            get => color;
            set { color = value; OnPropertyChanged("BgColor"); }
        }
    }

    public class ViewModel
    {
        public ObservableCollection<ItemClass> Items { get; private set; }
        public ViewModel()
        {
            Items = new ObservableCollection<ItemClass>();

            List<string> types = new SensorManager().GetSensorTypeList();
            global::Tizen.Log.Info("Sensor", "types count : " + types.Count);

            int i = 0;
            foreach (var type in types)
            {
                global::Tizen.Log.Info("Sensor", "type : " + type);

                if (i % 2 == 0)
                {
                    Items.Add(new ItemClass(type, new Color(0.0f, 0.0f, 0.0f, 0.0f)));
                }
                else
                {
                    Items.Add(new ItemClass(type, new Color(0.0f, 0.0f, 0.0f, 0.02f)));
                }

                i++;
            }
        }
    }

    /// <summary>
    /// Main Content Page with the supported sensor list on the current device.
    /// </summary>
    public partial class SensorMain : ContentPage
    {
        SensorManager sensorManager = new SensorManager();

        public ObservableCollection<string> Items { get; set; }

        /// <summary>
        /// The constructor of SensorMain.
        /// </summary>
        public SensorMain()
        {
            InitializeComponent();
            BindingContext = new ViewModel();
        }

        /// <summary>
        /// This method will be called when a list item is selected.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            List<object> cur = new List<object>(e.CurrentSelection);

            foreach (ItemClass item in e.PreviousSelection)
            {
                if (cur.Contains(item)) continue;
                
            }
            foreach (ItemClass item in cur)
            {
                SensorInfo info = sensorManager.GetSensorInfo((cur[0] as ItemClass).Text);
                OpenSensorPage(info);
            }
        }

        /// <summary>
        /// Opens specific sensor page.
        /// </summary>
        /// <param name="info">Sensor information</param>
        public void OpenSensorPage(SensorInfo info)
        {
            if (info.Type == SensorTypes.AccelerometerType)
            {
                Program.navigator.Push(new AccelerometerPage(info));
            }

            if (info.Type == SensorTypes.FaceDownGestureDetectorType)
            {
                Program.navigator.Push(new FaceDownGestureDetectorPage(info));
            }

            if (info.Type == SensorTypes.GravitySensorType)
            {
                Program.navigator.Push(new GravitySensorPage(info));
            }

            if (info.Type == SensorTypes.GyroscopeType)
            {
                Program.navigator.Push(new GyroscopePage(info));
            }

            if (info.Type == SensorTypes.GyroscopeRotationVectorType)
            {
                Program.navigator.Push(new GyroscopeRotationVectorSensorPage(info));
            }

            if (info.Type == SensorTypes.HeartRateMonitorType)
            {
                 Program.navigator.Push(new HeartRateMonitorPage(info));
            }

            if (info.Type == SensorTypes.HumiditySensorType)
            {
                Program.navigator.Push(new HumiditySensorPage(info));
            }

            if (info.Type == SensorTypes.InVehicleActivityDetectorType)
            {
                Program.navigator.Push(new InVehicleActivityDetectorPage(info));
            }

            if (info.Type == SensorTypes.LightSensorType)
            {
                Program.navigator.Push(new LightSensorPage(info));
            }

            if (info.Type == SensorTypes.LinearAccelerationSensorType)
            {
                Program.navigator.Push(new LinearAccelerationSensorPage(info));
            }

            if (info.Type == SensorTypes.MagnetometerType)
            {
                Program.navigator.Push(new MagnetometerPage(info));
            }

            if (info.Type == SensorTypes.MagnetometerRotationVectorType)
            {
                Program.navigator.Push(new MagnetometerRotationVectorSensorPage(info));
            }

            if (info.Type == SensorTypes.OrientationSensorType)
            {
                Program.navigator.Push(new OrientationSensorPage(info));
            }

            if (info.Type == SensorTypes.PedometerType)
            {
                Program.navigator.Push(new PedometerPage(info));
            }

            if (info.Type == SensorTypes.PickUpGestureDetectorType)
            {
                Program.navigator.Push(new PickUpGestureDetectorPage(info));
            }

            if (info.Type == SensorTypes.PressureSensorType)
            {
                Program.navigator.Push(new PressureSensorPage(info));
            }

            if (info.Type == SensorTypes.ProximitySensorType)
            {
                Program.navigator.Push(new ProximitySensorPage(info));
            }

            if (info.Type == SensorTypes.RotationVectorSensorType)
            {
                Program.navigator.Push(new RotationVectorSensorPage(info));
            }

            if (info.Type == SensorTypes.RunningActivityDetectorType)
            {
                Program.navigator.Push(new RunningActivityDetectorPage(info));
            }

            if (info.Type == SensorTypes.SleepMonitorType)
            {
                Program.navigator.Push(new SleepMonitorPage(info));
            }

            if (info.Type == SensorTypes.StationaryActivityDetectorType)
            {
                Program.navigator.Push(new StationaryActivityDetectorPage(info));
            }

            if (info.Type == SensorTypes.TemperatureSensorType)
            {
                Program.navigator.Push(new TemperatureSensorPage(info));
            }

            if (info.Type == SensorTypes.UltravioletSensorType)
            {
                Program.navigator.Push(new UltravioletSensorPage(info));
            }

            if (info.Type == SensorTypes.UncalibratedGyroscopeType)
            {
                Program.navigator.Push(new UncalibratedGyroscopePage(info));
            }

            if (info.Type == SensorTypes.UncalibratedMagnetometerType)
            {
                Program.navigator.Push(new UncalibratedMagnetometerPage(info));
            }

            if (info.Type == SensorTypes.WalkingActivityDetectorType)
            {
                Program.navigator.Push(new WalkingActivityDetectorPage(info));
            }

            if (info.Type == SensorTypes.WristUpGestureDetectorType)
            {
                Program.navigator.Push(new WristUpGestureDetectorPage(info));
            }
        }
    }
}
