/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
using SystemInfo.Model.Vibrator;
using SystemInfo.Tizen.Wearable.Service;
using Vibrator = Tizen.System.Vibrator;

[assembly: Xamarin.Forms.Dependency(typeof(VibratorService))]
namespace SystemInfo.Tizen.Wearable.Service
{
    /// <summary>
    /// Provides methods that allow to obtain information about device's vibrators.
    /// </summary>
    public class VibratorService : IVibrator
    {
        #region properties

        /// <summary>
        /// Number of available vibrators.
        /// </summary>
        public int NumberOfVibrators => Vibrator.NumberOfVibrators;

        #endregion
    }
}
