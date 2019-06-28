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
using FindPlace.Interfaces;
using FindPlace.Model;
using FindPlace.Tizen.Wearable.Services;
using FindPlace.Tizen.Wearable.Services.Privilege;
using FindPlace.Utils;
using System;
using System.Threading.Tasks;
using Tizen.Location;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocationService))]

namespace FindPlace.Tizen.Wearable.Services
{
    /// <summary>
    /// Service providing current device location.
    /// </summary>
    public class LocationService : ILocationService
    {
        #region fields

        /// <summary>
        /// Logger service.
        /// </summary>
        private readonly ILoggerService _loggerService;

        #endregion

        #region methods

        /// <summary>
        /// Initializes service.
        /// </summary>
        public LocationService()
        {
            _loggerService = DependencyService.Get<ILoggerService>();
        }

        /// <summary>
        /// Creates request to a location service.
        /// </summary>
        /// <returns>Returns task with lovation service response.</returns>
        public async Task<LocationServiceResponse> GetLocationAsync()
        {
            if (PrivilegeManager.AllPermissionsGranted())
            {
                var taskCompletionSource = new TaskCompletionSource<Location>();

                Locator locator = null;

                try
                {
                    locator = new Locator(LocationType.Hybrid);

                    void OnLocatorServiceStateChanged(object sender, ServiceStateChangedEventArgs e)
                    {
                        if (e.ServiceState == ServiceState.Enabled)
                        {
                            locator.ServiceStateChanged -= OnLocatorServiceStateChanged;
                            var receivedLocation = locator.GetLocation();
                            taskCompletionSource.SetResult(receivedLocation);
                        }
                    }

                    locator.ServiceStateChanged += OnLocatorServiceStateChanged;
                    locator.Start();

                    var location = await taskCompletionSource.Task;
                    _loggerService.Verbose($"{location.Latitude}, {location.Longitude}");
                    return new LocationServiceResponse(true, true, new Geocoordinates(location.Latitude, location.Longitude));
                }
                catch (Exception ex)
                {
                    _loggerService.Error(ex.Message);
                    _loggerService.Verbose("GPS not available.");
                    return new LocationServiceResponse(true);
                }
                finally
                {
                    locator?.Dispose();
                }
            }
            else
            {
                _loggerService.Verbose("GPS access not granted.");
                return new LocationServiceResponse(false);
            }
        }

        #endregion
    }
}
