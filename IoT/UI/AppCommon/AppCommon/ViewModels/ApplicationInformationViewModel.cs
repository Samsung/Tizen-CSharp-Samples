/*
 * Copyright (c) 2025 Samsung Electronics Co., Ltd All Rights Reserved
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

using System.ComponentModel;
using Tizen.NUI;
using AppCommon.Models;
using AppCommon.Services;

namespace AppCommon.ViewModels
{
    /// <summary>
    /// ViewModel for application information page
    /// </summary>
    public class ApplicationInformationViewModel : INotifyPropertyChanged
    {
        private const string degree = "\u00b0";

        private string _id;
        private string _name;
        private string _iconPath;
        private string _version;
        private Color _lowBatteryLEDColor;
        private Color _lowMemoryLEDColor;
        private string _language;
        private string _regionFormat;
        private string _deviceOrientation;
        private string _rotationDegree;

        public ApplicationInformationViewModel()
        {
            Initialize();
        }

        public string ID
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string IconPath
        {
            get => _iconPath;
            set
            {
                if (_iconPath != value)
                {
                    _iconPath = value;
                    OnPropertyChanged(nameof(IconPath));
                }
            }
        }

        public string Version
        {
            get => _version;
            set
            {
                if (_version != value)
                {
                    _version = value;
                    OnPropertyChanged(nameof(Version));
                }
            }
        }

        public Color LowBatteryLEDColor
        {
            get => _lowBatteryLEDColor;
            set
            {
                if (_lowBatteryLEDColor != value)
                {
                    _lowBatteryLEDColor = value;
                    OnPropertyChanged(nameof(LowBatteryLEDColor));
                }
            }
        }

        public Color LowMemoryLEDColor
        {
            get => _lowMemoryLEDColor;
            set
            {
                if (_lowMemoryLEDColor != value)
                {
                    _lowMemoryLEDColor = value;
                    OnPropertyChanged(nameof(LowMemoryLEDColor));
                }
            }
        }

        public string Language
        {
            get => _language;
            set
            {
                if (_language != value)
                {
                    _language = value;
                    OnPropertyChanged(nameof(Language));
                }
            }
        }

        public string RegionFormat
        {
            get => _regionFormat;
            set
            {
                if (_regionFormat != value)
                {
                    _regionFormat = value;
                    OnPropertyChanged(nameof(RegionFormat));
                }
            }
        }

        public string DeviceOrientation
        {
            get => _deviceOrientation;
            set
            {
                if (_deviceOrientation != value)
                {
                    _deviceOrientation = value;
                    OnPropertyChanged(nameof(DeviceOrientation));
                }
            }
        }

        public string RotationDegree
        {
            get => _rotationDegree;
            set
            {
                if (_rotationDegree != value)
                {
                    _rotationDegree = value;
                    OnPropertyChanged(nameof(RotationDegree));
                }
            }
        }

        public void UpdateLowBatteryLEDColor(LowBatteryStatus status)
        {
            switch (status)
            {
                case LowBatteryStatus.PowerOff:
                    LowBatteryLEDColor = Resources.Red;
                    return;
                case LowBatteryStatus.CriticalLow:
                    LowBatteryLEDColor = Resources.Yellow;
                    return;
                case LowBatteryStatus.None:
                default:
                    LowBatteryLEDColor = Resources.LightGray;
                    return;
            }
        }

        public void UpdateLowMemoryLEDColor(LowMemoryStatus status)
        {
            switch (status)
            {
                case LowMemoryStatus.HardWarning:
                    LowMemoryLEDColor = Resources.Red;
                    return;
                case LowMemoryStatus.SoftWarning:
                    LowMemoryLEDColor = Resources.Yellow;
                    return;
                case LowMemoryStatus.Normal:
                    LowMemoryLEDColor = Resources.Green;
                    return;
                case LowMemoryStatus.None:
                default:
                    LowMemoryLEDColor = Resources.LightGray;
                    return;
            }
        }

        public void UpdateDeviceOrientation(DeviceOrientationStatus orientation)
        {
            switch (orientation)
            {
                case DeviceOrientationStatus.Orientation_0:
                    DeviceOrientation = "Natural position";
                    RotationDegree = "0" + degree;
                    return;
                case DeviceOrientationStatus.Orientation_90:
                    DeviceOrientation = "Right-side up";
                    RotationDegree = "90" + degree;
                    return;
                case DeviceOrientationStatus.Orientation_180:
                    DeviceOrientation = "Up-side down";
                    RotationDegree = "180" + degree;
                    return;
                case DeviceOrientationStatus.Orientation_270:
                    DeviceOrientation = "Left-side up";
                    RotationDegree = "270" + degree;
                    return;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void Initialize()
        {
            var appInfo = ServiceLocator.Get<IAppInformationService>();
            var systemSettings = ServiceLocator.Get<ISystemSettingsService>();
            var batteryInfo = ServiceLocator.Get<IBatteryInformationService>();

            batteryInfo.LevelChanged += (s, e) =>
            {
                UpdateLowBatteryLEDColor(e.LowBatteryStatus);
            };

            ID = appInfo.ID;
            Name = appInfo.Name;
            IconPath = appInfo.IconPath;

            Version = "0.0.0.1";
            LowBatteryLEDColor = Resources.LightGray;
            LowMemoryLEDColor = Resources.LightGray;
            Language = systemSettings.Language;
            RegionFormat = systemSettings.Language;

            DeviceOrientation = "Natural position";
            RotationDegree = "0" + degree;
        }
    }
}
