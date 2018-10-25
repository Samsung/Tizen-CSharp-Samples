/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Windows.Input;
using MetalDetector.Models;
using Xamarin.Forms;

namespace MetalDetector.ViewModels
{
    /// <summary>
    /// Main view model class for the application.
    /// Encapsulates presentation logic and state of the application.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Backing field of the Ready property.
        /// </summary>
        private bool _ready = false;

        /// <summary>
        /// Backing field of the IsInRange property.
        /// </summary>
        private bool _isInRange = false;

        /// <summary>
        /// Backing field of the RelativeSignalStrength property.
        /// </summary>
        private int _relativeSignalStrength = 0;

        /// <summary>
        /// Backing field of the SignalStrength property.
        /// </summary>
        private int _signalStrength = 0;

        /// <summary>
        /// Backing field of the Rotation property.
        /// </summary>
        private double _rotation = 0;

        /// <summary>
        /// Metal detector model which allow the application to use the metal detector data.
        /// </summary>
        private MetalDetectorModel _metalDetectorModel;

        #endregion

        #region properties

        /// <summary>
        /// Command which starts metal detector.
        /// </summary>
        public ICommand StartCommand { get; private set; }

        /// <summary>
        /// Command which stops metal detector.
        /// </summary>
        public ICommand StopCommand { get; private set; }

        /// <summary>
        /// Indicates if the metal detector is ready.
        /// </summary>
        public bool Ready
        {
            private set => SetProperty(ref _ready, value);
            get => _ready;
        }

        /// <summary>
        /// Indicates if metal detector detects something in its range.
        /// </summary>
        public bool IsInRange
        {
            set => SetProperty(ref _isInRange, value);
            get => _isInRange;
        }

        /// <summary>
        /// Strength level value (in range from 0 to 9).
        /// </summary>
        public int RelativeSignalStrength
        {
            set => SetProperty(ref _relativeSignalStrength, value);
            get => _relativeSignalStrength;
        }

        /// <summary>
        /// Strength level value.
        /// </summary>
        public int SignalStrength
        {
            set => SetProperty(ref _signalStrength, value);
            get => _signalStrength;
        }

        /// <summary>
        /// Rotation value of the metal detector indicator.
        /// </summary>
        public double Rotation
        {
            set => SetProperty(ref _rotation, value);
            get => _rotation;
        }

        #endregion

        #region methods

        /// <summary>
        /// MainViewModel class constructor.
        /// Initializes the view model.
        /// </summary>
        public MainViewModel()
        {
            _metalDetectorModel = new MetalDetectorModel();

            InitCommands();

            _metalDetectorModel.Updated += ModelOnMetalDetectorDataUpdated;
        }

        /// <summary>
        /// Initializes view model's commands.
        /// </summary>
        private void InitCommands()
        {
            StartCommand = new Command(ExecuteStart);
            StopCommand = new Command(ExecuteStop);
        }

        /// <summary>
        /// Starts the metal detector.
        /// </summary>
        private void ExecuteStart()
        {
            if (_metalDetectorModel.IsSupported())
            {
                _metalDetectorModel.Start();
                Ready = true;
            }
        }

        /// <summary>
        /// Stops the metal detector.
        /// </summary>
        private void ExecuteStop()
        {
            _metalDetectorModel.Stop();
        }

        /// <summary>
        /// Handles "Updated" event of the metal detector model.
        /// Updates properties binded to the application UI.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ModelOnMetalDetectorDataUpdated(object sender, EventArgs e)
        {
            IsInRange = _metalDetectorModel.IsInRange;
            RelativeSignalStrength = _metalDetectorModel.RelativeSignalStrength;
            SignalStrength = _metalDetectorModel.SignalStrength;
            Rotation = _metalDetectorModel.Rotation;
        }

        #endregion
    }
}
