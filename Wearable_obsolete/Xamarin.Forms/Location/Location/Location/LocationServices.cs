/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd. All rights reserved.
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
using System;
using System.Linq;
using System.Collections.Generic;

using Xamarin.Forms;
using Tizen.Location;
using Tizen.Security;


namespace Location
{
    /// <summary>
    /// Location service page
    /// </summary>
    class LocationServices : ContentPage
    {
        /// <summary>
        /// Locator instance
        /// </summary>
        private static Locator locator = null;

        /// <summary>
        /// GpsSatellite instance
        /// </summary>
        private static GpsSatellite satellite = null;

        /// <summary>
        /// CircleBoundary instance
        /// </summary>
        private static CircleBoundary circle = null;

        private static ServiceState serviceState = ServiceState.Disabled;
        private static Boolean isBound = false;
        private static Boolean isTrack = false;
        private static Boolean isSatellite = false;

        /// <summary>
        /// Text message to indicate current status
        /// </summary>
        static Label textMessage = new Label()
        {
            HorizontalTextAlignment = TextAlignment.Center,
            Text = "Status",
        };

        /// <summary>
        /// Initialize class
        /// </summary>
        public class InitializePage : ContentPage
        {
            /// <summary>
            /// Initialize page
            /// </summary>
            public InitializePage()
            {
                /// Create new menu list
                var menuList = new List<string>
                {
                    "Location Start",
                    "Get Location",
                    "Boundary",
                    "Tracking",
                    "Satellite",
                    "Location Stop",
                };

                /// Create application title
                var title = new Label()
                {
                    HorizontalTextAlignment = TextAlignment.Center,
                    Text = "Location",
                    FontSize = 8,
                };

                /// Create ListView to show menu list
                var menu = new ListView()
                {
                    Header = "",
                    ItemsSource = menuList,
                    Footer = "",
                };

                /// Create navigation items
                menu.ItemTapped += Menu_ItemTapped;

                /// Content view of this page
                Content = new StackLayout
                {
                    Children =
                    {
                        title,
                        menu,
                    },
                };

                /// Check location permission
                PrivilegeCheck();
            }

            /// <summary>
            /// Event : navigation ItemTapped
            /// </summary>
            /// <param name="sender">object</param>
            /// <param name="e">An enumeration of type ItemTappedEventArgs.</param>
            private async void Menu_ItemTapped(object sender, ItemTappedEventArgs e)
            {
                if (e.Item.ToString() == "Location Start")
                {
                    /// An awaitable task, to indicate new page
                    await Navigation.PushModalAsync(new PageStart());
                }
                else if (e.Item.ToString() == "Get Location")
                {
                    /// An awaitable task, to indicate new page
                    await Navigation.PushModalAsync(new PageGetLocation());
                }
                else if (e.Item.ToString() == "Boundary")
                {
                    /// An awaitable task, to indicate new page
                    await Navigation.PushModalAsync(new PageBoundary());
                }
                else if (e.Item.ToString() == "Tracking")
                {
                    /// An awaitable task, to indicate new page
                    await Navigation.PushModalAsync(new PageTracking());
                }
                else if (e.Item.ToString() == "Satellite")
                {
                    /// An awaitable task, to indicate new page
                    await Navigation.PushModalAsync(new PageSatellite());
                }
                else if (e.Item.ToString() == "Location Stop")
                {
                    /// An awaitable task, to indicate new page
                    await Navigation.PushModalAsync(new PageStop());
                }
            }

            /// <summary>
            /// Permission check
            /// </summary>
            public void PrivilegeCheck()
            {
                try
                {
                    /// Check location permission
                    CheckResult result = PrivacyPrivilegeManager.CheckPermission("http://tizen.org/privilege/location");

                    switch (result)
                    {
                        case CheckResult.Allow:
                            break;
                        case CheckResult.Deny:
                            break;
                        case CheckResult.Ask:
                            /// Request to privacy popup
                            PrivacyPrivilegeManager.RequestPermission("http://tizen.org/privilege/location");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    /// Exception handling
                    textMessage.Text = "[Pravacy] Exception \n" + ex.Message;
                }
            }
        }

        /// <summary>
        /// New page to start location service
        /// </summary>
        public class PageStart : ContentPage
        {
            /// <summary>
            /// New page
            /// </summary>
            public PageStart()
            {
                /// Text message to indicate current status
                textMessage.TextColor = Color.White;
                textMessage.Text = "[Start]\nWaiting for GPS signal..";

                /// Content view of this page
                Content = new StackLayout()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        textMessage,
                    }
                };

                if (locator == null)
                {
                    try
                    {
                        /// <summary>
                        /// Create Locator instance, sets LocationType to GPS
                        /// </summary>
                        /// <param name="Hybrid">This method selects the best method available at the moment.</param>
                        /// <param name="Gps">This method uses Global Positioning System.</param>
                        /// <param name="Wps">This method uses WiFi Positioning System.</param>
                        /// <param name="Passive">This method uses passive mode.</param>
                        locator = new Locator(LocationType.Gps);

                        /// Create GpsSatellite instance
                        satellite = new GpsSatellite(locator);

                        if (locator != null)
                        {
                            /// Starts the Locator which has been created using the specified method.
                            locator.Start();

                            /// Add ServiceStateChanged event to receive the event regarding service state
                            locator.ServiceStateChanged += LocatorServiceStateChanged;
                        }
                    }
                    catch (Exception ex)
                    {
                        /// Exception handling
                        textMessage.Text = "[Start] Exception \n" + ex.Message;
                    }
                }
            }

            /// <summary>
            /// Event : ServiceStateChanged
            /// </summary>
            /// <param name="sender">object</param>
            /// <param name="e">An enumeration of type LocationServiceState.</param>
            public void LocatorServiceStateChanged(object sender, ServiceStateChangedEventArgs e)
            {
                if (e.ServiceState == ServiceState.Enabled)
                {
                    /// Service is enabled
                    textMessage.TextColor = Color.YellowGreen;
                    textMessage.Text = "[Status] Success \n Service enabled";
                    serviceState = ServiceState.Enabled;
                }
                else
                {
                    /// Service is disabled
                    textMessage.TextColor = Color.White;
                    textMessage.Text = "[Status] \n Service disabled";
                    serviceState = ServiceState.Disabled;
                }
            }
        }

        /// <summary>
        /// New page to getting current location
        /// </summary>
        public class PageGetLocation : ContentPage
        {
            /// <summary>
            /// New page
            /// </summary>
            public PageGetLocation()
            {
                /// Text message to indicate current status
                textMessage.TextColor = Color.White;
                textMessage.Text = "[GetLocation]";

                /// Content view of this page
                Content = new StackLayout()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        textMessage,
                    }
                };

                try
                {
                    /// Gets the details of the location.
                    Tizen.Location.Location l = locator.GetLocation();

                    /// Status message about current location
                    textMessage.TextColor = Color.YellowGreen;
                    textMessage.Text = "[GetLocation] Success\nlatitude : " + l.Latitude + "\nlongitude : " + l.Longitude;
                }
                catch (InvalidOperationException)
                {
                    /// Exception handling
                    textMessage.Text = "[GetLocation]\nService temporarily unavailable.\nPlease retry after service enabled";
                }
                catch (Exception ex)
                {
                    /// Exception handling
                    textMessage.Text = "[GetLocation] Exception \n" + ex.Message;
                }
            }
        }

        /// <summary>
        /// New page for location boundary
        /// </summary>
        public class PageBoundary : ContentPage
        {
            /// <summary>
            /// New page
            /// </summary>
            public PageBoundary()
            {
                /// Text message to indicate current status
                textMessage.TextColor = Color.White;
                textMessage.Text = "[Boundary]";

                /// Content view of this page
                Content = new StackLayout()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        textMessage,
                    }
                };

                try
                {
                    if (isBound != true)
                    {
                        if (serviceState == ServiceState.Enabled)
                        {
                            /// Set initial position
                            Coordinate center;
                            center.Latitude = 10.1234;
                            center.Longitude = 20.1234;

                            /// Set radius value to detect out of boundary
                            double radius = 50.0;

                            /// Create circle boundary
                            /// <param name="center">The coordinates which constitute the center of the circular boundary.</param>
                            /// <param name="radius">The radius value of the circular boundary.</param>
                            circle = new CircleBoundary(center, radius);

                            /// Add circle boundary to detect zone changed
                            locator.AddBoundary(circle);

                            /// Add ZoneChanged event
                            locator.ZoneChanged += LocatorZoneChanged;
                            isBound = true;
                        }
                        else
                        {
                            textMessage.Text = "[Boundary]\nService temporarily unavailable.\nPlease retry after service enabled";
                        }
                    }
                }
                catch (Exception ex)
                {
                    /// Exception handling
                    textMessage.Text = "[Boundary] Exception \n" + ex.Message;
                }
            }
        }

        /// <summary>
        /// Event : ZoneChanged
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ZoneChangedEventArgs</param>
        public static void LocatorZoneChanged(object sender, ZoneChangedEventArgs e)
        {
            /// <param name="BoundState">An enumeration of type BoundaryState.</param>
            /// <param name="Latitude">The latitude value [-90.0 ~ 90.0] (degrees).</param>
            /// <param name="Longitude">The longitude value [-180.0 ~ 180.0] (degrees).</param>
            /// <param name="Altitude">The altitude value.</param>
            /// <param name="Timestamp">The timestamp value.</param>
            textMessage.TextColor = Color.YellowGreen;
            textMessage.Text = "[Boundary : " + e.BoundState + "] Success\nlatitude : " + e.Latitude + "\nlongitude : " + e.Longitude;
        }

        /// <summary>
        /// New page to tracking current location
        /// </summary>
        public class PageTracking : ContentPage
        {
            /// <summary>
            /// New page
            /// </summary>
            public PageTracking()
            {
                /// Text message to indicate current status
                textMessage.TextColor = Color.White;
                textMessage.Text = "[Tracking]";

                /// Content view of this page
                Content = new StackLayout()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        textMessage,
                    }
                };

                try
                {
                    if (isTrack != true)
                    {
                        /// Add LocationChanged event
                        locator.LocationChanged += LocatorLocationChanged;
                        isTrack = true;
                    }
                }
                catch (Exception ex)
                {
                    /// Exception handling
                    textMessage.Text = "[Tracking] Exception \n" + ex.Message;
                }
            }
        }

        /// <summary>
        /// Event : LocationChanged
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Object of the Location class.</param>
        public static void LocatorLocationChanged(object sender, LocationChangedEventArgs e)
        {
            /// LocationChanged event invoked,
            /// Available values : Latitude, Longitude, Altitude, Speed, Direction, Accuracy, Timestamp
            textMessage.TextColor = Color.YellowGreen;
            textMessage.Text = "[Tracking] Success\nlatitude : " + e.Location.Latitude + "\nlongitude : " + e.Location.Longitude;
        }

        /// <summary>
        /// New page for satellite information
        /// </summary>
        public class PageSatellite : ContentPage
        {
            /// <summary>
            /// New page
            /// </summary>
            public PageSatellite()
            {
                /// Text message to indicate current status
                textMessage.TextColor = Color.White;
                textMessage.Text = "[Satellite]";

                /// Content view of this page
                Content = new StackLayout()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        textMessage,
                    }
                };

                try
                {
                    if (isTrack == true)
                    {
                        /// Remove LocationChanged event
                        locator.LocationChanged -= LocatorLocationChanged;
                        isTrack = false;
                    }

                    if (isSatellite != true)
                    {
                        /// Add SatelliteStatusUpdated event
                        satellite.SatelliteStatusUpdated += SatelliteSatelliteStatusUpdated;
                    }
                }
                catch (Exception ex)
                {
                    /// Exception handling
                    textMessage.Text = "[Satellite] Exception \n" + ex.Message;
                }
            }
        }

        /// <summary>
        /// Event : SatelliteStatusUpdated
        /// If InViewCount is bigger than 0, SatelliteInformation is available
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">SatelliteStatusChangedEventArgs</param>
        public static void SatelliteSatelliteStatusUpdated(object sender, SatelliteStatusChangedEventArgs e)
        {
            try
            {
                /// <param name="ActiveCount">The number of active satellites.</param>
                /// <param name="InViewCount">The number of satellites in view.</param>
                /// <param name="Timestamp">The time at which the data has been extracted.</param>
                if (e.InViewCount > 0)
                {
                    /// <summary>
                    /// Satellites returns <IList> values
                    /// ElementAt<SatelliteInformation>(0) used to check one of SatelliteInformation
                    /// </summary>
                    /// <param name="Azimuth">The azimuth value of the satellite in degrees.</param>
                    /// <param name="Elevation">The elevation of the satellite in meters.</param>
                    /// <param name="Prn">The PRN value of the satellite.</param>
                    /// <param name="Snr">The SNR value of the satellite in dB.</param>
                    /// <param name="Active">The flag signaling if the satellite is in use.</param>
                    SatelliteInformation s = satellite.Satellites.ElementAt<SatelliteInformation>(0);
                    textMessage.TextColor = Color.YellowGreen;
                    textMessage.Text = "[Satellite] Success\n InViewCount : " + e.InViewCount + "\n Active : " + s.Active;
                }
            }
            catch (Exception ex)
            {
                /// Exception handling
                textMessage.Text = "[Satellite] Exception \n" + ex.Message;
            }
        }

        /// <summary>
        /// New page to stop location service
        /// </summary>
        public class PageStop : ContentPage
        {
            /// <summary>
            /// New page
            /// </summary>
            public PageStop()
            {
                /// Text message to indicate current status
                textMessage.TextColor = Color.White;
                textMessage.Text = "[Stop]";

                /// Content view of this page
                Content = new StackLayout()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        textMessage,
                    }
                };

                try
                {
                    if (circle != null)
                    {
                        /// Remove ZoneChanged event
                        locator.ZoneChanged -= LocatorZoneChanged;
                        isBound = false;

                        /// RemoveBoundary to remove boundary method
                        locator.RemoveBoundary(circle);

                        /// Dispose to release circle instance.
                        circle.Dispose();
                        circle = null;
                    }

                    if (isTrack == true)
                    {
                        /// Remove LocationChanged event
                        locator.LocationChanged -= LocatorLocationChanged;
                        isTrack = false;
                    }

                    if (isSatellite == true)
                    {
                        /// Remove SatelliteStatusUpdated event
                        satellite.SatelliteStatusUpdated -= SatelliteSatelliteStatusUpdated;
                        isSatellite = false;
                    }

                    /// Stop to location service
                    locator.Stop();

                    /// Dispose to release location resource
                    locator.Dispose();
                    locator = null;
                    satellite = null;

                    /// Indicate current status
                    textMessage.TextColor = Color.YellowGreen;
                    textMessage.Text = "[Stop] Success ";
                }
                catch (Exception ex)
                {
                    /// Exception handling
                    textMessage.Text = "[Stop] Exception \n" + ex.Message;
                }
            }
        }
    }
}
