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

using Xamarin.Forms;
using AppCommon.Interfaces;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AppCommon
{
    /// <summary>
    /// The enumeration for LowBatteryStatus
    /// </summary>
    public enum LowBatteryStatus
    {
        None = 0,
        PowerOff = 1,
        CriticalLow = 2
    }

    /// <summary>
    /// The enumeration for LowMemoryStatus
    /// </summary>
    public enum LowMemoryStatus
    {
        None = 0,
        Normal = 1,
        SoftWarning = 2,
        HardWarning = 4
    }

    /// <summary>
    /// The enumeration for DeviceOrientationStatus
    /// </summary>
    public enum DeviceOrientationStatus
    {
        Orientation_0 = 0,
        Orientation_90 = 90,
        Orientation_180 = 180,
        Orientation_270 = 270
    }

    /// <summary>
    /// A class about Application Information Data
    /// </summary>
    public class ApplicationInformationViewModel : INotifyPropertyChanged
    {
        private readonly string degree ="\u00B0";

        private Color _LowBatteryLEDColor;
        private Color _LowMemoryLEDColor;
        private string _deviceOrientation;
        private string _RotationDegree;
        private string _Language;
        private string _RegionFormat;

        /// <summary>
        /// A constructor for ApplicationInformationModel
        /// </summary>
        public ApplicationInformationViewModel()
        {
            Initialize();
        }

        /// <summary>
        /// An application ID
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// An application name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// An application icon path
        /// </summary>
        public string IconPath { get; private set; }

        /// <summary>
        /// An application version
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// The orientation of the device
        /// </summary>
        public string DeviceOrientation
        {
            get
            {
                return _deviceOrientation;
            }

            private set
            {
                if (_deviceOrientation == value)
                {
                    return;
                }

                _deviceOrientation = value;
                OnPropertyChanged("DeviceOrientation");
            }
        }

        /// <summary>
        /// The rotation degree of the device
        /// </summary>
        public string RotationDegree
        {
            get
            {
                return _RotationDegree;
            }

            private set
            {
                if (_RotationDegree == value)
                {
                    return;
                }

                _RotationDegree = value;
                OnPropertyChanged("RotationDegree");
            }
        }

        /// <summary>
        /// A color to represent a state of the device battery
        /// </summary>
        public Color LowBatteryLEDColor
        {
            get
            {
                return _LowBatteryLEDColor;
            }

            set
            {
                if (_LowBatteryLEDColor == value)
                {
                    return;
                }

                _LowBatteryLEDColor = value;
                OnPropertyChanged("LowBatteryLEDColor");
            }
        }

        /// <summary>
        /// A color to represent a state of the device memory
        /// </summary>
        public Color LowMemoryLEDColor
        {
            get
            {
                return _LowMemoryLEDColor;
            }

            set
            {
                if (_LowMemoryLEDColor == value)
                {
                    return;
                }

                _LowMemoryLEDColor = value;
                OnPropertyChanged("LowMemoryLEDColor");
            }
        }

        /// <summary>
        /// The language setting on the device
        /// </summary>
        public string Language
        {
            get
            {
                return _Language;
            }

            set
            {
                if (_Language == value)
                {
                    return;
                }

                _Language = value;
                OnPropertyChanged("Language");
            }
        }

        /// <summary>
        /// The region format setting on the device
        /// </summary>
        public string RegionFormat
        {
            get
            {
                return _RegionFormat;
            }

            set
            {
                if (_RegionFormat == value)
                {
                    return;
                }

                _RegionFormat = value;
                OnPropertyChanged("RegionFormat");
            }
        }

        /// <summary>
        /// Update the color for the LowBatterLED
        /// </summary>
        /// <param name="status">low batter status</param>
        public void UpdateLowBatteryLEDColor(LowBatteryStatus status)
        {
            switch (status)
            {
                case LowBatteryStatus.PowerOff:
                    {
                        LowBatteryLEDColor = Color.FromRgb(236, 13, 13);
                        return;
                    }

                case LowBatteryStatus.CriticalLow:
                    {
                        LowBatteryLEDColor = Color.FromRgb(220, 197, 0);
                        return;
                    }

                case LowBatteryStatus.None:
                default:
                    {
                        LowBatteryLEDColor = Color.FromRgb(223, 223, 223);
                        return;
                    }
            }
        }

        /// <summary>
        /// Update the color for the LowMemoryLED
        /// </summary>
        /// <param name="status">low memory status</param>
        public void UpdateLowMemoryLEDColor(LowMemoryStatus status)
        {
            switch (status)
            {
                case LowMemoryStatus.Normal:
                case LowMemoryStatus.SoftWarning:
                    {
                        LowMemoryLEDColor = Color.FromRgb(220, 197, 0);
                        Device.StartTimer(System.TimeSpan.FromMilliseconds(1000), () =>
                        {
                            UpdateLowMemoryLEDColor(LowMemoryStatus.None);
                            return false;
                        });
                        return;
                    }

                case LowMemoryStatus.HardWarning:
                    {
                        LowMemoryLEDColor = Color.FromRgb(236, 13, 13);
                        Device.StartTimer(System.TimeSpan.FromMilliseconds(1000), () =>
                        {
                            UpdateLowMemoryLEDColor(LowMemoryStatus.None);
                            return false;
                        });
                        return;
                    }

                case LowMemoryStatus.None:
                default:
                    {
                        LowMemoryLEDColor = Color.FromRgb(223, 223, 223);
                        return;
                    }
            }
        }

        /// <summary>
        /// To update the latest orientation of the device
        /// </summary>
        /// <param name="orientation">the orientation</param>
        public void UpdateDeviceOrientation(DeviceOrientationStatus orientation)
        {
            switch (orientation)
            {
                case DeviceOrientationStatus.Orientation_0:
                    {
                        DeviceOrientation = "Natural position";
                        RotationDegree = "0" + degree;
                        return;
                    }

                case DeviceOrientationStatus.Orientation_90:
                    {
                        DeviceOrientation = "Right-side up";
                        RotationDegree = "90" + degree;
                        return;
                    }

                case DeviceOrientationStatus.Orientation_180:
                    {
                        DeviceOrientation = "Up-side down";
                        RotationDegree = "180" + degree;
                        return;
                    }

                case DeviceOrientationStatus.Orientation_270:
                    {
                        DeviceOrientation = "Left-side up";
                        RotationDegree = "270" + degree;
                        return;
                    }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called to notify that a change of property happened
        /// </summary>
        /// <param name="propertyName">A property name that need to update to the value</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// To initialize fields when the class is instantiated
        /// </summary>
        void Initialize()
        {
            var appInfo = DependencyService.Get<IAppInformation>();
            var systemSettings = DependencyService.Get<ISystemSettings>();
            var batteryInfo = DependencyService.Get<IBatteryInformation>();

            batteryInfo.LevelChanged += (s, e) =>
            {
                UpdateLowBatteryLEDColor(e.LowBatteryStatus);
            };

            ID = appInfo.ID;
            Name = appInfo.Name;
            IconPath = appInfo.IconPath;

            Version = "0.0.0.1";
            LowBatteryLEDColor = Color.FromRgb(223, 223, 223);
            LowMemoryLEDColor = Color.FromRgb(223, 223, 223);
            Language = systemSettings.Language;
            RegionFormat = systemSettings.Language;

            DeviceOrientation = "Natural position";
            RotationDegree = "0" + degree;
        }
    }
}