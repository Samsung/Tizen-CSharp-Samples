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

using System;
using System.ComponentModel;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using AppCommon.ViewModels;

namespace AppCommon.Pages
{
    /// <summary>
    /// Application information page
    /// </summary>
    public class ApplicationInformationPage : ContentPage
    {
        private ApplicationInformationViewModel _viewModel;

        // UI Elements
        private ImageView _icon;
        private TextLabel _applicationName;
        private TextLabel _applicationID;
        private TextLabel _applicationVersion;
        private ImageView _memoryLED;
        private ImageView _batteryLED;
        private TextLabel _language;
        private TextLabel _regionFormat;
        private TextLabel _deviceOrientation;
        private TextLabel _rotationDegree;

        private int _screenWidth;
        private int _screenHeight;
        private bool _isLandscape;

        public ApplicationInformationViewModel ViewModel => _viewModel;

        public ApplicationInformationPage(int screenWidth = 720, int screenHeight = 1280)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _isLandscape = screenWidth > screenHeight;

            // Create ViewModel
            _viewModel = new ApplicationInformationViewModel();

            InitializeComponent();
            SetupDataBinding();
        }

        private void InitializeComponent()
        {
            // Set page properties
            AppBar = new AppBar
            {
                Title = "Application Information"
            };

            // Create main layout with simple background
            var mainLayout = new View
            {
                Layout = new AbsoluteLayout(),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                BackgroundColor = new Color(0.95f, 0.95f, 0.95f, 1.0f) // Light gray background
            };

            // Adaptive layout calculations
            float iconSize = _isLandscape ? 100f : 150f;
            float margin = _isLandscape ? 20f : 30f;
            float topOffset = _isLandscape ? 20f : 80f;
            float ledSize = _isLandscape ? 15f : 20f;

            // Application Icon
            _icon = new ImageView
            {
                Size = new Size(iconSize, iconSize),
                Position = new Position(margin, topOffset)
            };
            mainLayout.Add(_icon);

            // Application Name Label
            var nameLabel = new TextLabel
            {
                Text = "Name:",
                PixelSize = Resources.SmallFontSize,
                TextColor = Resources.Black,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Position = new Position(margin, topOffset + iconSize + 15)
            };
            mainLayout.Add(nameLabel);

            // Application Name
            _applicationName = new TextLabel
            {
                PixelSize = Resources.NormalFontSize,
                TextColor = Resources.DarkGray,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                MultiLine = true,
                Size = new Size(_screenWidth - margin * 2, 40),
                Position = new Position(margin, topOffset + iconSize + 35)
            };
            mainLayout.Add(_applicationName);

            // Application ID Label
            var idLabel = new TextLabel
            {
                Text = "ID:",
                PixelSize = Resources.SmallFontSize,
                TextColor = Resources.Black,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Position = new Position(margin, topOffset + iconSize + 80)
            };
            mainLayout.Add(idLabel);

            // Application ID
            _applicationID = new TextLabel
            {
                PixelSize = Resources.NormalFontSize,
                TextColor = Resources.DarkGray,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                MultiLine = true,
                Size = new Size((_screenWidth - margin * 3) / 2, 50),
                Position = new Position(margin, topOffset + iconSize + 100)
            };
            mainLayout.Add(_applicationID);

            // Application Version Label
            var versionLabel = new TextLabel
            {
                Text = "Version:",
                PixelSize = Resources.SmallFontSize,
                TextColor = Resources.Black,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Position = new Position(_screenWidth / 2 + margin / 2, topOffset + iconSize + 80)
            };
            mainLayout.Add(versionLabel);

            // Application Version
            _applicationVersion = new TextLabel
            {
                PixelSize = Resources.NormalFontSize,
                TextColor = Resources.DarkGray,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                MultiLine = true,
                Size = new Size((_screenWidth - margin * 3) / 2, 40),
                Position = new Position(_screenWidth / 2 + margin / 2, topOffset + iconSize + 100)
            };
            mainLayout.Add(_applicationVersion);

            // Memory LED (simple colored circle)
            float ledYPos = topOffset + iconSize + 170;
            _memoryLED = new ImageView
            {
                Size = new Size(ledSize, ledSize),
                Position = new Position(margin, ledYPos),
                BackgroundColor = Resources.LightGray,
                CornerRadius = ledSize / 2.0f
            };
            mainLayout.Add(_memoryLED);

            // Memory LED Label
            var memoryLabel = new TextLabel
            {
                Text = "Memory:",
                PixelSize = Resources.SmallFontSize,
                TextColor = Resources.DarkGray,
                Position = new Position(margin + ledSize + 10, ledYPos - 5),
                Size = new Size(150, 30)
            };
            mainLayout.Add(memoryLabel);

            // Battery LED (simple colored circle)
            _batteryLED = new ImageView
            {
                Size = new Size(ledSize, ledSize),
                Position = new Position(_screenWidth / 2 + margin / 2, ledYPos),
                BackgroundColor = Resources.LightGray,
                CornerRadius = ledSize / 2.0f
            };
            mainLayout.Add(_batteryLED);

            // Battery LED Label
            var batteryLabel = new TextLabel
            {
                Text = "Battery:",
                PixelSize = Resources.SmallFontSize,
                TextColor = Resources.DarkGray,
                Position = new Position(_screenWidth / 2 + margin / 2 + ledSize + 10, ledYPos - 5),
                Size = new Size(150, 30)
            };
            mainLayout.Add(batteryLabel);

            // Language Label
            float langYPos = ledYPos + 60;
            var languageLabel = new TextLabel
            {
                Text = "Language:",
                PixelSize = Resources.SmallFontSize,
                TextColor = Resources.Black,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Position = new Position(margin, langYPos)
            };
            mainLayout.Add(languageLabel);

            // Language
            _language = new TextLabel
            {
                PixelSize = Resources.NormalFontSize,
                TextColor = Resources.DarkGray,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                MultiLine = true,
                Size = new Size((_screenWidth - margin * 3) / 2, 40),
                Position = new Position(margin, langYPos + 20)
            };
            mainLayout.Add(_language);

            // Region Format Label
            var regionLabel = new TextLabel
            {
                Text = "Region:",
                PixelSize = Resources.SmallFontSize,
                TextColor = Resources.Black,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Position = new Position(_screenWidth / 2 + margin / 2, langYPos)
            };
            mainLayout.Add(regionLabel);

            // Region Format
            _regionFormat = new TextLabel
            {
                PixelSize = Resources.NormalFontSize,
                TextColor = Resources.DarkGray,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                MultiLine = true,
                Size = new Size((_screenWidth - margin * 3) / 2, 40),
                Position = new Position(_screenWidth / 2 + margin / 2, langYPos + 20)
            };
            mainLayout.Add(_regionFormat);

            // Device Orientation Label
            float orientYPos = _isLandscape ? langYPos + 80 : _screenHeight - 120;
            var orientationLabel = new TextLabel
            {
                Text = "Orientation:",
                PixelSize = Resources.SmallFontSize,
                TextColor = Resources.Black,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Position = new Position(margin, orientYPos)
            };
            mainLayout.Add(orientationLabel);

            // Device Orientation
            _deviceOrientation = new TextLabel
            {
                PixelSize = Resources.NormalFontSize,
                TextColor = Resources.DarkGray,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                MultiLine = true,
                Size = new Size((_screenWidth - margin * 3) / 2, 40),
                Position = new Position(margin, orientYPos + 20)
            };
            mainLayout.Add(_deviceOrientation);

            // Rotation Degree Label
            var degreeLabel = new TextLabel
            {
                Text = "Rotation:",
                PixelSize = Resources.SmallFontSize,
                TextColor = Resources.Black,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Position = new Position(_screenWidth / 2 + margin / 2, orientYPos)
            };
            mainLayout.Add(degreeLabel);

            // Rotation Degree
            _rotationDegree = new TextLabel
            {
                PixelSize = _isLandscape ? 80.0f : Resources.LargeFontSize,
                TextColor = Resources.Black,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Size = new Size((_screenWidth - margin * 3) / 2, _isLandscape ? 100 : 150),
                Position = new Position(_screenWidth / 2 + margin / 2, _isLandscape ? orientYPos + 20 : orientYPos - 30)
            };
            mainLayout.Add(_rotationDegree);

            // Set content
            Content = mainLayout;
        }

        private void SetupDataBinding()
        {
            // Subscribe to PropertyChanged events for manual data binding
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;

            // Initialize UI with current ViewModel values
            UpdateUI();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Update UI directly (NUI is already on UI thread)
            switch (e.PropertyName)
            {
                case nameof(_viewModel.IconPath):
                    _icon.ResourceUrl = _viewModel.IconPath;
                    break;
                case nameof(_viewModel.Name):
                    _applicationName.Text = _viewModel.Name;
                    break;
                case nameof(_viewModel.ID):
                    _applicationID.Text = _viewModel.ID;
                    break;
                case nameof(_viewModel.Version):
                    _applicationVersion.Text = _viewModel.Version;
                    break;
                case nameof(_viewModel.LowMemoryLEDColor):
                    _memoryLED.BackgroundColor = _viewModel.LowMemoryLEDColor;
                    break;
                case nameof(_viewModel.LowBatteryLEDColor):
                    _batteryLED.BackgroundColor = _viewModel.LowBatteryLEDColor;
                    break;
                case nameof(_viewModel.Language):
                    _language.Text = _viewModel.Language;
                    break;
                case nameof(_viewModel.RegionFormat):
                    _regionFormat.Text = _viewModel.RegionFormat;
                    break;
                case nameof(_viewModel.DeviceOrientation):
                    _deviceOrientation.Text = _viewModel.DeviceOrientation;
                    break;
                case nameof(_viewModel.RotationDegree):
                    _rotationDegree.Text = _viewModel.RotationDegree;
                    break;
            }
        }

        private void UpdateUI()
        {
            _icon.ResourceUrl = _viewModel.IconPath;
            _applicationName.Text = _viewModel.Name;
            _applicationID.Text = _viewModel.ID;
            _applicationVersion.Text = _viewModel.Version;
            _memoryLED.BackgroundColor = _viewModel.LowMemoryLEDColor;
            _batteryLED.BackgroundColor = _viewModel.LowBatteryLEDColor;
            _language.Text = _viewModel.Language;
            _regionFormat.Text = _viewModel.RegionFormat;
            _deviceOrientation.Text = _viewModel.DeviceOrientation;
            _rotationDegree.Text = _viewModel.RotationDegree;
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (type == DisposeTypes.Explicit)
            {
                // Unsubscribe from events
                if (_viewModel != null)
                {
                    _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
                }
            }
            base.Dispose(type);
        }
    }
}
