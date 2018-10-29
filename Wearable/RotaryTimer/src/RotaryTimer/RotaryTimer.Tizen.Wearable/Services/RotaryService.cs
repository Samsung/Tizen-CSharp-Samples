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
using ElmSharp.Wearable;
using RotaryTimer.Tizen.Wearable.Services;
using System;
using RotaryTimer.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(RotaryService))]
namespace RotaryTimer.Tizen.Wearable.Services
{
    /// <summary>
    /// Service which provides event handler subscription for Tizen rotary change.
    /// </summary>
    class RotaryService : IRotaryService
    {
        #region properties

        /// <summary>
        /// Event dispatched on rotary change.
        /// </summary>
        public event EventHandler<bool> RotationChanged;

        #endregion

        #region methods

        /// <summary>
        /// Invokes methods attached to RotationChanged event.
        /// </summary>
        /// <param name="args">Rotary arguments.</param>
        private void OnRotaryChange(RotaryEventArgs args)
        {
            RotationChanged?.Invoke(null, args.IsClockwise);
        }

        /// <summary>
        /// Subscribes for rotation change.
        /// </summary>
        public RotaryService()
        {
            RotaryEventManager.Rotated += OnRotaryChange;
        }

        #endregion
    }
}
