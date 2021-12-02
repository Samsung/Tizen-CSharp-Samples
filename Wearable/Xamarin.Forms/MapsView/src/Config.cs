//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using Xamarin.Forms.Maps;

namespace MapsView
{
    public class Config
    {
        public const string MAPS_SERVICE_PROVIDER = "HERE";
        public const string MAPS_SERVICE_KEY = "<PUT YOUR KEY HERE>";
        // For more information see https://developer.tizen.org/development/guides/native-application/location-and-sensors/maps-and-maps-service/getting-here-maps-credentials

        /// <summary>
        /// How far from above do we start - its a distance in kilometers used for
        /// Distance.FromKilometers() method.
        /// </summary>
        public const double STARTING_ZOOM_LEVEL = 3;

        /// <summary>
        /// What type of Map should be used - we default to Satellite view
        /// </summary>
        public const MapType MAP_TYPE = MapType.Satellite;

        /// <summary>
        /// How often should a zoom happen if the user keeps on rotating the bezel
        /// </summary>
        public const int ZOOM_EVERY_X_MILLISECONDS = 500;
    }
}
