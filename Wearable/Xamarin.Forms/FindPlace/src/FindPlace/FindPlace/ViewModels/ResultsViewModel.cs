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
using FindPlace.Model;
using System.Collections.Generic;

namespace FindPlace.ViewModels
{
    /// <summary>
    /// Provides results page view abstraction.
    /// </summary>
    public class ResultsViewModel : ViewModelBase
    {
        #region properties

        /// <summary>
        /// List of results.
        /// </summary>
        public IEnumerable<PlaceSearchResult> Results { get; }

        /// <summary>
        /// Type of selected place.
        /// </summary>
        public PlaceType PlaceType { get; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes ResultsViewModel class.
        /// </summary>
        /// <param name="placeType">Type of place.</param>
        /// <param name="results">Results of place search.</param>
        public ResultsViewModel(PlaceType placeType, IEnumerable<PlaceSearchResult> results)
        {
            PlaceType = placeType;
            Results = results;
        }

        #endregion
    }
}
