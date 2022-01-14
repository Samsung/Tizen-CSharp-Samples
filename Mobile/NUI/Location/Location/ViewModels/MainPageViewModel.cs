/*
 * Copyright (c) 2022 Samsung Electronics Co., Ltd. All rights reserved.
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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tizen.NUI.Binding;
using System.Windows.Input;
using Tizen.Location;
using Tizen.Security;
using System;
using System.Linq;

namespace Location.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Locator instance
        /// </summary>
        private static Locator locator;

        /// <summary>
        /// GpsSatellite instance
        /// </summary>
        private static GpsSatellite satellite;

        /// <summary>
        /// CircleBoundary instance
        /// </summary>
        private static CircleBoundary circle;

        /// <summary>
        /// Flag indicating is location is trecked
        /// </summary>
        private static bool isTrack;
        private static bool isSatellite;
        private static bool isBound;

        private string textStatus;
        private string textTrack;
        private string textMessage;
        private bool startLocationServiceButtonEnabled;
        private bool stopLocationServiceButtonEnabled;
        private bool satelliteInformationButtonEnabled;
        private bool trackTheRouteButtonEnabled;
        private bool locationBoundaryButtonEnabled;
        private bool getLocationButtonEnabled;
        private string trackTheRouteButtonLabel;
        private string satelliteInformationButtonLabel;
        private string locationBoundaryButtonLabel;
        public ICommand StartLocationService { get; private set; }

        public ICommand GetLocation { get; private set; }

        public ICommand TrackTheRoute { get; private set; }

        public ICommand SatelliteInformation { get; private set; }

        public ICommand LocationBoundary { get; private set; }

        public ICommand StopLocationService { get; private set; }

        public MainPageViewModel()
        {
            locator = null;
            satellite = null;
            circle = null;
            isTrack = false;
            isSatellite = false;
            isBound = false;
            textStatus = "[Status]";
            textTrack = "";
            textMessage = "";
            startLocationServiceButtonEnabled = true;
            stopLocationServiceButtonEnabled = false;
            satelliteInformationButtonEnabled = false;
            trackTheRouteButtonEnabled = false;
            locationBoundaryButtonEnabled = false;
            getLocationButtonEnabled = false;
            trackTheRouteButtonLabel = "Track the route";
            satelliteInformationButtonLabel = "Satellite information";
            locationBoundaryButtonLabel = "Location boundary";
            StartLocationService = new Command(() => ExecuteStartLocationService());
            GetLocation = new Command(() => ExecuteGetLocation());
            TrackTheRoute = new Command(() => ExecuteTrackTheRoute());
            SatelliteInformation = new Command(() => ExecuteSatelliteInformation());
            LocationBoundary = new Command(() => ExecuteLocationBoundary());
            StopLocationService = new Command(() => ExecuteStopLocationService());
            PrivilegeCheck();
        }

        public string TextStatus
        {
            get => textStatus;
            set
            {
                if (textStatus != value)
                {
                    textStatus = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string TextTrack
        {
            get => textTrack;
            set
            {
                if (textTrack != value)
                {
                    textTrack = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string TextMessage
        {
            get => textMessage;
            set
            {
                if (textMessage != value)
                {
                    textMessage = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool StartLocationServiceButtonEnabled
        {
            get => startLocationServiceButtonEnabled;
            set
            {
                if (startLocationServiceButtonEnabled != value)
                {
                    startLocationServiceButtonEnabled = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool StopLocationServiceButtonEnabled
        {
            get => stopLocationServiceButtonEnabled;
            set
            {
                if (stopLocationServiceButtonEnabled != value)
                {
                    stopLocationServiceButtonEnabled = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool SatelliteInformationButtonEnabled
        {
            get => satelliteInformationButtonEnabled;
            set
            {
                if (satelliteInformationButtonEnabled != value)
                {
                    satelliteInformationButtonEnabled = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool TrackTheRouteButtonEnabled
        {
            get => trackTheRouteButtonEnabled;
            set
            {
                if (trackTheRouteButtonEnabled != value)
                {
                    trackTheRouteButtonEnabled = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool LocationBoundaryButtonEnabled
        {
            get => locationBoundaryButtonEnabled;
            set
            {
                if (locationBoundaryButtonEnabled != value)
                {
                    locationBoundaryButtonEnabled = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool GetLocationButtonEnabled
        {
            get => getLocationButtonEnabled;
            set
            {
                if (getLocationButtonEnabled != value)
                {
                    getLocationButtonEnabled = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string TrackTheRouteButtonLabel
        {
            get => trackTheRouteButtonLabel;
            set
            {
                if (trackTheRouteButtonLabel != value)
                {
                    trackTheRouteButtonLabel = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string SatelliteInformationButtonLabel
        {
            get => satelliteInformationButtonLabel;
            set
            {
                if (satelliteInformationButtonLabel != value)
                {
                    satelliteInformationButtonLabel = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string LocationBoundaryButtonLabel
        {
            get => locationBoundaryButtonLabel;
            set
            {
                if (locationBoundaryButtonLabel != value)
                {
                    locationBoundaryButtonLabel = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Button event to start location service
        /// </summary>
        public void ExecuteStartLocationService()
        {
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
                        TextStatus = "[Status] Start location service, GPS searching ...";

                        /// Add ServiceStateChanged event to receive the event regarding service state
                        locator.ServiceStateChanged += Locator_ServiceStateChanged;

                        /// Disable start button to avoid duplicated call.
                        StartLocationServiceButtonEnabled = false;

                        // Enable available buttons
                        SatelliteInformationButtonEnabled = true;
                        StopLocationServiceButtonEnabled = true;
                    }
                    else
                    {
                        /// Locator creation failed
                        TextStatus = "[Status] Location initialize .. Failed";
                    }
                }
                catch (Exception ex)
                {
                    /// Exception handling
                    TextStatus = "[Status] Location Initialize : " + ex.Message;
                }
            }
        }

        /// <summary>
        /// Button event to get current location
        /// </summary>
        public void ExecuteGetLocation()
        {
            try
            {
                /// <summary>
                /// Gets the details of the location.
                /// </summary>
                /// <param name="Latitude">The current latitude [-90.0 ~ 90.0] (degrees).</param>
                /// <param name="Longitude">The current longitude [-180.0 ~ 180.0] (degrees).</param>
                /// <param name="Altitude">The current altitude (meters).</param>
                /// <param name="Speed">The device speed (km/h).</param>
                /// <param name="Direction">The direction and degrees from the north.</param>
                /// <param name="Accuracy">The accuracy.</param>
                /// <param name="Timestamp">The time value when the measurement was done.</param>
                Tizen.Location.Location l = locator.GetLocation();
                TextMessage = "[GetLocation]" + "\n  Timestamp : " + l.Timestamp + "\n  Latitude : " + l.Latitude + "\n  Longitude : " + l.Longitude;
            }
            catch (Exception ex)
            {
                /// Exception when location service is not available
                TextMessage = "[GetLocation]" + "\n  Exception : " + ex.Message;
            }
        }
        /// <summary>
        /// Event : ServiceStateChanged
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">An enumeration of type LocationServiceState.</param>
        private void Locator_ServiceStateChanged(object sender, ServiceStateChangedEventArgs e)
        {
            /// <param name="ServiceState.Disabled">Service is disabled.</param>
            /// <param name="ServiceState.Enabled">Service is enabled.</param>
            if (e.ServiceState == ServiceState.Enabled)
            {
                /// Service state changed to Enabled
                TextStatus = "[Status] Location service enabled";

                /// Update button status
                StartLocationServiceButtonEnabled = false;

                /// Enable buttons to show available method
                TrackTheRouteButtonEnabled = true;
                GetLocationButtonEnabled = true;
                LocationBoundaryButtonEnabled = true;
            }
            else
            {
                /// Service state changed to Disabled
                TextStatus = "[Status] Service disabled, GPS searching ...";
            }
        }

        /// <summary>
        /// Event : LocationChanged
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Object of the Location class.</param>
        private void Locator_LocationChanged(object sender, LocationChangedEventArgs e)
        {
            /// LocationChanged event invoked,
            /// Available values : Latitude, Longitude, Altitude, Speed, Direction, Accuracy, Timestamp
            TextTrack = "[Tracking] " + "\n  Latitude : " + e.Location.Latitude + "\n  Longitude : " + e.Location.Longitude;
        }

        /// <summary>
        /// Remove LocationChanged event
        /// </summary>
        private void CancelTrack()
        {
            /// Remove LocationChanged event
            locator.LocationChanged -= Locator_LocationChanged;
            TrackTheRouteButtonLabel = "Track the route";
            isTrack = false;
        }

        /// <summary>
        /// Button event for location tracking
        /// </summary>
        public void ExecuteTrackTheRoute()
        {
            if (isTrack == true)
            {
                CancelTrack();
                TextTrack = "";
            }
            else
            {
                /// Add LocationChanged event
                locator.LocationChanged += Locator_LocationChanged;
                TrackTheRouteButtonLabel = "Cancel route tracking";
                isTrack = true;
            }
        }

        /// <summary>
        /// Event : SatelliteStatusUpdated
        /// If InViewCount is bigger than 0, SatelliteInformation is available
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">SatelliteStatusChangedEventArgs</param>
        private void Satellite_SatelliteStatusUpdated(object sender, SatelliteStatusChangedEventArgs e)
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
                    TextMessage = "[Satellite]" + "\n  Timestamp : " + e.Timestamp + "\n  Active : " + e.ActiveCount + " , Inview : " + e.InViewCount + "\n  azimuth[" + s.Azimuth + "] elevation[" + s.Elevation + "] prn[" + s.Prn + "]\n  snr[" + s.Snr + "] active[" + s.Active + "]";
                }
                else
                {
                    /// ActiveCount and InViewCount are available when SatelliteSttatusUpdated event invoked.
                    TextMessage = "[Satellite]" + "\n  Timestamp : " + e.Timestamp + "\n  Active : " + e.ActiveCount + " , Inview : " + e.InViewCount;
                }
            }
            catch (Exception ex)
            {
                /// Exception when location service is not available
                TextMessage = "[Satellite]\n  Exception : " + ex.Message;
            }
        }

        /// Cancel satellite information
        private void CancelSatellite()
        {
            /// Remove SatelliteStatusUpdated event
            satellite.SatelliteStatusUpdated -= Satellite_SatelliteStatusUpdated;
            SatelliteInformationButtonLabel = "Satellite information";
            isSatellite = false;
        }

        /// <summary>
        /// Button event to receive satellite information
        /// </summary>
        public void ExecuteSatelliteInformation()
        {
            if (isSatellite == true)
            {
                /// Cancel satellite information
                CancelSatellite();
                TextMessage = "";
            }
            else
            {
                /// Add SatelliteStatusUpdated event
                try
                {
                    satellite.SatelliteStatusUpdated += Satellite_SatelliteStatusUpdated;
                    SatelliteInformationButtonLabel = "Cancel satellite information";
                    isSatellite = true;
                }
                catch (Exception ex)
                {
                    /// Exception handling
                    TextMessage = "[Satellite]\n  Exception : " + ex.Message;
                }
            }
        }

        /// <summary>
        /// Event : ZoneChanged
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ZoneChangedEventArgs</param>
        private void Locator_ZoneChanged(object sender, ZoneChangedEventArgs e)
        {
            /// <param name="BoundState">An enumeration of type BoundaryState.</param>
            /// <param name="Latitude">The latitude value [-90.0 ~ 90.0] (degrees).</param>
            /// <param name="Longitude">The longitude value [-180.0 ~ 180.0] (degrees).</param>
            /// <param name="Altitude">The altitude value.</param>
            /// <param name="Timestamp">The timestamp value.</param>
            TextMessage = "[Boundary]\n  Timestamp : " + e.Timestamp + "\n  BoundState : " + e.BoundState + "\n  Latitude : " + e.Latitude + "\n  Longitude : " + e.Longitude;
        }

        /// Cancel boundary
        private void CancelBoundary()
        {
            /// Remove ZoneChanged event
            locator.ZoneChanged -= Locator_ZoneChanged;
            LocationBoundaryButtonLabel = "Location boundary";
            isBound = false;

            /// RemoveBoundary to remove boundary method
            locator.RemoveBoundary(circle);

            /// Dispose to release circle instance.
            circle.Dispose();
            circle = null;
        }

        /// <summary>
        /// Button event to set location boundary
        /// </summary>
        public void ExecuteLocationBoundary()
        {
            /// Set initial position
            Coordinate center;
            center.Latitude = 10.1234;
            center.Longitude = 20.1234;

            /// Set radius value to detect out of boundary
            double radius = 50.0;

            try
            {
                if (isBound == true)
                {
                    /// Cancel boundary
                    CancelBoundary();
                    TextMessage = "";
                }
                else
                {
                    LocationBoundaryButtonLabel = "Cancel location boundary";
                    isBound = true;

                    /// Create circle boundary
                    /// <param name="center">The coordinates which constitute the center of the circular boundary.</param>
                    /// <param name="radius">The radius value of the circular boundary.</param>
                    circle = new CircleBoundary(center, radius);

                    /// Add circle boundary to detect zone changed
                    locator.AddBoundary(circle);

                    /// Add ZoneChanged event
                    locator.ZoneChanged += Locator_ZoneChanged;
                }
            }
            catch (Exception ex)
            {
                /// Exception handling
                TextMessage = "[Boundary]\n  Exception : " + ex.Message;
            }
        }

        /// <summary>
        /// Button event to stop location service
        /// </summary>
        public void ExecuteStopLocationService()
        {
            try
            {
                if (circle != null)
                {
                    /// Cancel boundary when stop button clicked
                    CancelBoundary();
                }

                if (isTrack == true)
                {
                    /// Cancel tracking when stop button clicked
                    CancelTrack();
                }

                if (isSatellite == true)
                {
                    /// Cancel satellite when stop button clicked
                    CancelSatellite();
                }

                /// Remove text messages
                TextMessage = "";
                TextTrack = "";

                /// Stop to location service
                locator.Stop();

                /// Dispose to release location resource
                locator.Dispose();
                locator = null;
                satellite = null;

                /// Enable location button after Stop() method called.
                StartLocationServiceButtonEnabled = true;

                /// Disable buttons when location in not available.
                TrackTheRouteButtonEnabled = false;
                LocationBoundaryButtonEnabled = false;
                GetLocationButtonEnabled = false;
                SatelliteInformationButtonEnabled = false;
                StopLocationServiceButtonEnabled = false;
                TextStatus = "[Status] Stop location service";
            }
            catch (Exception ex)
            {
                /// Exception handling
                TextMessage = "[Stop]\n  Exception : " + ex.Message;
            }
        }

        /// <summary>
        /// Permission check
        /// </summary>
        private void PrivilegeCheck()
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
                TextStatus = "[Status] Privilege : " + ex.Message;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
