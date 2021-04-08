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
using System;

namespace GestureSensor.Interfaces
{
    /// <summary>
    /// Function template for gesture updating.
    /// </summary>
    /// <param name="type">Type of the gesture.</param>
    /// <param name="isDetected">Indicates if gesture is detected.</param>
    public delegate void GestureUpdatedDelegate(GestureType type, bool isDetected);

    /// <summary>
    /// Provides methods associated with gesture recognition.
    /// </summary>
    public interface IGestureService : IDisposable
    {
        /// <summary>
        /// Invoked when gesture was updated.
        /// </summary>
        event GestureUpdatedDelegate GestureUpdated;

        /// <summary>
        /// Indicates if gestures are supported on the device.
        /// </summary>
        bool IsSupported { get; }

        /// <summary>
        /// Initializes gestures.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Indicates how many times WristUp gesture was detected.
        /// </summary>
        int WristUpCounter { get; }

        /// <summary>
        /// Indicates how many times FaceDown gesture was detected.
        /// </summary>
        int FaceDownCounter { get; }

        /// <summary>
        /// Indicates how many times PickUp gesture was detected.
        /// </summary>
        int PickUpCounter { get; }
    }
}