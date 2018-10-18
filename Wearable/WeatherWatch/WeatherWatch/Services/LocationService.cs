/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Threading.Tasks;
using Tizen.Location;
using Tizen.System;
using WeatherWatch.PageModels;
using Xamarin.Forms;

namespace WeatherWatch.Services
{
    /// <summary>
    /// LocationService class
    /// </summary>
    public class LocationService
    {
        public bool LocationSupported;
        public bool LocationEnabled;
        private Locator locator;

        UserPermission permission;
        Location Position;
        WeatherWatchPageModel _viewModel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_vm">WeatherWatchPageModel</param>
        public LocationService(WeatherWatchPageModel _vm)
        {
            _viewModel = _vm;
            Information.TryGetValue("http://tizen.org/feature/location", out LocationSupported);
            LocationEnabled = false;

            if (LocationSupported)
            {
                SubscribeUserPemission();
                RequestPermission();
            }
        }

        async public Task<Location> GetCurrentPosition()
        {
            if (locator == null)
            {
                bool ret = await StartService();
                if (!ret)
                {
                    return null;
                }
            }

            Location l = null;
            try
            {
                l = locator.GetLocation();
            }
            catch (Exception e)
            {
                Console.WriteLine("\n[GetCurrentPosition] GetLocation()--> Exception : " + e.Message + ", " + e.StackTrace + ", " + e.InnerException);
            }

            return l;
        }

        async void RequestPermission()
        {
            permission = new UserPermission();
            bool userPerm = await permission.GetPermission("http://tizen.org/privilege/location");
            if (userPerm)
            {
                MessagingCenter.Send<LocationService, bool>(this, "UserConsentGained", true);
            }
        }

        Task<bool> StartService()
        {
            //bool result = true;
            //var NonUItask = Task.Run(() =>
            return Task.Run(() =>
            {
                Console.WriteLine("[LocationService.StartService()]  start");
                try
                {
                    locator = new Locator(LocationType.Hybrid);
                    locator.ServiceStateChanged += Locator_ServiceStateChanged;
                    
                    locator.Start();
                }
                catch (Exception ee)
                {
                    Console.WriteLine("\n\n\n\n\n LocationService Exception : " + ee.Message + ", " + ee.StackTrace + ", " + ee.InnerException);
                    if (locator != null)
                    {
                        locator.ServiceStateChanged -= Locator_ServiceStateChanged;
                        locator.Dispose();
                        locator = null;
                    }

                    return false;
                }

                Console.WriteLine("[LocationService.StartService()]  end    locator: " + locator);
                return true;
            });
        }

        void SubscribeUserPemission()
        {
            MessagingCenter.Subscribe<LocationService, bool>(this, "UserConsentGained", (obj, item) =>
            {
                Console.WriteLine("   ## MainPageViewModel get notified about GetUserPermission");
                StartService();
            });
        }

        private void Locator_ServiceStateChanged(object sender, ServiceStateChangedEventArgs e)
        {
            LocationEnabled = e.ServiceState == ServiceState.Disabled ? false : true;

            if (LocationEnabled)
            {
                Position = locator.GetLocation();
                Console.WriteLine("[Locator_ServiceStateChanged] Location Service is Enabled. Position : " + Position.Latitude + ", " + Position.Longitude);
            }
            else
            {
                Position = null;
                Console.WriteLine("[Locator_ServiceStateChanged] Location Service is Disabled.");
            }

            _viewModel.UpdateInformation(Position);
        }
    }
}
