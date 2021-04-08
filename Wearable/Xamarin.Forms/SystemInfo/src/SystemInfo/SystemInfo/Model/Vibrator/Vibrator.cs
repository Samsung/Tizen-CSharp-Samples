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
using Xamarin.Forms;

namespace SystemInfo.Model.Vibrator
{
    /// <summary>
    /// Class that holds information about device's vibrators.
    /// </summary>
    public class Vibrator
    {
        #region fields

        /// <summary>
        /// Service that provides information about device's vibrators.
        /// </summary>
        private readonly IVibrator _service;

        #endregion

        #region properties

        /// <summary>
        /// Gets the number of available vibrators.
        /// </summary>
        public int NumberOfVibrators => _service.NumberOfVibrators;

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public Vibrator()
        {
            _service = DependencyService.Get<IVibrator>();
        }

        #endregion
    }
}