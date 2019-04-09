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
namespace Geocoding.ViewModels
{
    /// <summary>
    /// IGeocodingCoordinatesArgs interface.
    /// It defines methods that must be implemented by class used to notifying about geocoding coordinates.
    /// </summary>
    public interface IGeocodingCoordinatesArgs
    {
        #region properties

        /// <summary>
        /// Latitude value.
        /// </summary>
        double Latitude { get; }

        /// <summary>
        /// Longitude value.
        /// </summary>
        double Longitude { get; }

        #endregion
    }
}
