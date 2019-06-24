/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
using FindPlace.Enums;

namespace FindPlace.ViewModels
{
    /// <summary>
    /// Provides type of place selection abstraction.
    /// </summary>
    public class PlaceTypeViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Backing field of Place property.
        /// </summary>
        private PlaceType _placeType;

        #endregion

        #region properties

        /// <summary>
        /// Type of place.
        /// </summary>
        public PlaceType PlaceType
        {
            get => _placeType;
            set => SetProperty(ref _placeType, value);
        }

        /// <summary>
        /// Represents the empty PlaceTypeViewModel.
        /// </summary>
        public static PlaceTypeViewModel Empty { get; } = new PlaceTypeViewModel();

        /// <summary>
        /// Distincts empty PlaceTypeViewModel.
        /// </summary>
        public bool IsEmpty { get; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes PlaceTypeViewModel class.
        /// </summary>
        /// <param name="placeType">Place type.</param>
        public PlaceTypeViewModel(PlaceType placeType)
        {
            PlaceType = placeType;
            IsEmpty = false;
        }

        /// <summary>
        /// Initializes PlaceTypeViewModel class.
        /// </summary>
        private PlaceTypeViewModel()
        {
            IsEmpty = true;
        }

        #endregion
    }
}
