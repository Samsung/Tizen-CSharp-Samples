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
using Compass.Models;
using Compass.Utils;

namespace Compass.ViewModels
{
    /// <summary>
    /// Provides methods responsible for application view model state.
    /// </summary>
    public class CompassViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Backing field of the CompassDeviation property.
        /// </summary>
        private float _compassDeviation;

        /// <summary>
        /// Backing field of the CompassDirection property.
        /// </summary>
        private CompassDirections _compassDirection;

        /// <summary>
        /// Provides methods to obtain the compass data.
        /// </summary>
        private CompassModel _compassModel = new CompassModel();

        #endregion

        #region properties

        /// <summary>
        /// Current compass deviation.
        /// </summary>
        public float CompassDeviation
        {
            get => _compassDeviation;
            private set => SetProperty(ref _compassDeviation, value);
        }

        /// <summary>
        /// Current compass direction.
        /// </summary>
        public CompassDirections CompassDirection
        {
            get => _compassDirection;
            private set => SetProperty(ref _compassDirection, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        public CompassViewModel()
        {
            if (_compassModel != null)
                _compassModel.CompassDataUpdated += OnCompassDataUpdated;
            _compassModel?.Start();
        }

        /// <summary>
        /// Handles "CompassDataUpdated" event of the CompassModel class.
        /// Updates value of CompassDeviation and CompassDirection properties.
        /// </summary>
        /// <param name="sender">Instance of the object which invoked the event.</param>
        /// <param name="compassDeviation">Compass deviation.</param>
        /// <param name="compassDirection">Compass direction.</param>
        private void OnCompassDataUpdated(object sender, float compassDeviation, CompassDirections compassDirection)
        {
            CompassDeviation = compassDeviation;
            CompassDirection = compassDirection;
        }

        #endregion
    }
}
