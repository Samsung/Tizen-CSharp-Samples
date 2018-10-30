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
using RotaryTimer.Interfaces;
using RotaryTimer.Tizen.Wearable.Services;
using System.Threading;
using Tizen.System;

[assembly: Xamarin.Forms.Dependency(typeof(VibrationService))]
namespace RotaryTimer.Tizen.Wearable.Services
{
    /// <summary>
    /// Service which provides platform specific vibration functionality.
    /// </summary>
    class VibrationService : IVibration
    {
        #region methods

        /// <summary>
        /// Starts vibrations.
        /// </summary>
        public void Vibrate()
        {
            if (Vibrator.NumberOfVibrators > 0)
            {
                Vibrator vibrator = Vibrator.Vibrators[0];
                vibrator.Vibrate(2000, 70);
                Thread.Sleep(2000);
            }
        }

        #endregion
    }
}
