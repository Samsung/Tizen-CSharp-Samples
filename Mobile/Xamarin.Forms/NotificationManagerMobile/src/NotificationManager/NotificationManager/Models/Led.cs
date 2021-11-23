/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

using Xamarin.Forms;

namespace NotificationManager.Models
{
    /// <summary>
    /// LED class.
    /// Provides property that allows to indicate if LED is supported.
    /// </summary>
    public static class Led
    {
        #region properties

        /// <summary>
        /// Indicates if LED is available.
        /// </summary>
        public static bool IsLedAvailable => DependencyService.Get<ILed>().IsAvailable;

        #endregion
    }
}