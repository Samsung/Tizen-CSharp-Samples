/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

﻿using GestureSensor.Enums;
using GestureSensor.Interfaces;
using GestureSensor.Tizen.Wearable.Services;
using System;
using Tizen.Sensor;
using Dependency = Xamarin.Forms.DependencyAttribute;

[assembly: Dependency(typeof(GestureService))]
namespace GestureSensor.Tizen.Wearable.Services
{
    /// <summary>
    /// Gesture service.
    /// </summary>
    public class GestureService : IGestureService
    {
        /// <summary>
        /// Gesture will be deactivated after this timeout.
        /// </summary>
        private const int GestureTimeout = 2000;

        /// <summary>
        /// WristUp gesture detector.
        /// </summary>
        private WristUpGestureDetector _wristUpGestureDetector;

        /// <summary>
        /// Timer for WristUp gesture.
        /// </summary>
        private CancellableTimer _wristUpTimer;

        /// <summary>
        /// FaceDown gesture detector.
        /// </summary>
        private FaceDownGestureDetector _faceDownGestureDetector;

        /// <summary>
        /// Timer for FaceDown gesture.
        /// </summary>
        private CancellableTimer _faceDownTimer;

        /// <summary>
        /// PickUp gesture detector.
        /// </summary>
        private PickUpGestureDetector _pickUpGestureDetector;

        /// <summary>
        /// Timer for PickUp gesture.
        /// </summary>
        private CancellableTimer _pickUpTimer;

        /// <summary>
        /// Invokes when gesture status has changed.
        /// </summary>
        public event GestureUpdatedDelegate GestureUpdated;

        /// <summary>
        /// Indicates if gestures are supported on the device.
        /// </summary>
        public bool IsSupported => WristUpGestureDetector.IsSupported &&
                                   FaceDownGestureDetector.IsSupported &&
                                   PickUpGestureDetector.IsSupported;

        /// <summary>
        /// Indicates how many times WristUp gesture was detected.
        /// </summary>
        public int WristUpCounter { get; private set; }

        /// <summary>
        /// Indicates how many times FaceDown gesture was detected.
        /// </summary>
        public int FaceDownCounter { get; private set; }

        /// <summary>
        /// Indicates how many times PickUp gesture was detected.
        /// </summary>
        public int PickUpCounter { get; private set; }

        /// <summary>
        /// Initializes <see cref="GestureService"/>
        /// </summary>
        public void Initialize()
        {
            _wristUpGestureDetector = new WristUpGestureDetector();
            _wristUpGestureDetector.DataUpdated += WristUpUpdated;

            _faceDownGestureDetector = new FaceDownGestureDetector();
            _faceDownGestureDetector.DataUpdated += FaceDownUpdated;

            _pickUpGestureDetector = new PickUpGestureDetector();
            _pickUpGestureDetector.DataUpdated += PickUpUpdated;

            _wristUpGestureDetector.Start();
            _faceDownGestureDetector.Start();
            _pickUpGestureDetector.Start();
        }

        /// <summary>
        /// Updates status of WristUp gesture.
        /// </summary>
        /// <param name="sender">Object that invoked event.</param>
        /// <param name="e">Event parameter.</param>
        private void WristUpUpdated(object sender, WristUpGestureDetectorDataUpdatedEventArgs e)
        {
            WristUpCounter++;
            UpdateGesture(ref _wristUpTimer, GestureType.WristUp);
        }

        /// <summary>
        /// Updates status of FaceDown gesture.
        /// </summary>
        /// <param name="sender">Object that invoked event.</param>
        /// <param name="e">Event parameter.</param>
        private void FaceDownUpdated(object sender, FaceDownGestureDetectorDataUpdatedEventArgs e)
        {
            FaceDownCounter++;
            UpdateGesture(ref _faceDownTimer, GestureType.FaceDown);
        }

        /// <summary>
        /// Updates status of PickUp gesture.
        /// </summary>
        /// <param name="sender">Object that invoked event.</param>
        /// <param name="e">Event parameter.</param>
        private void PickUpUpdated(object sender, PickUpGestureDetectorDataUpdatedEventArgs e)
        {
            PickUpCounter++;
            UpdateGesture(ref _pickUpTimer, GestureType.PickUp);
        }

        /// <summary>
        /// Updates gesture. Start timer which deactivates gesture state after certain amount of time.
        /// </summary>
        /// <param name="timer">Timer object.</param>
        /// <param name="type">Gesture type.</param>
        private void UpdateGesture(ref CancellableTimer timer, GestureType type)
        {
            if (timer?.IsRunning == true)
            {
                timer.Stop();
            }

            GestureUpdated?.Invoke(type, true);

            timer = new CancellableTimer(TimeSpan.FromMilliseconds(GestureTimeout), () =>
            {
                GestureUpdated?.Invoke(type, false);
            });

            timer.Start();
        }

        /// <summary>
        /// Disposes used resources.
        /// </summary>
        public void Dispose()
        {
            _wristUpTimer?.Dispose();
            _pickUpTimer?.Dispose();
            _faceDownTimer?.Dispose();
        }
    }
}
