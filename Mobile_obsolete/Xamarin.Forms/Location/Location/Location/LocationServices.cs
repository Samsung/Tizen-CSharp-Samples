using System;
using System.Linq;

using Xamarin.Forms;
using Tizen.Location;
using Tizen.Security;


namespace Location
{
    /// <summary>
    /// Location service page
    /// </summary>
    public class LocationServices : ContentPage
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

        private static Button buttonStart;
        private static Button buttonGetLocation;
        private static Button buttonBoundary;
        private static Button buttonTrack;
        private static Button buttonSatellite;
        private static Button buttonStop;
        private static Label textStatus;
        private static Label textTrack;
        private static Label textMessage;
        private static Boolean isTrack = false;
        private static Boolean isSatellite = false;
        private static Boolean isBound = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public LocationServices()
        {
        }

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
                /// Create new Button to start location service
                buttonStart = new Button
                {
                    Text = "Start location service"
                };

                /// Create new Button for getting location
                buttonGetLocation = new Button
                {
                    Text = "Get Location"
                };

                /// Create new Button for boundary
                buttonBoundary = new Button
                {
                    Text = "Location boundary"
                };

                /// Create new Button for route tracking
                buttonTrack = new Button
                {
                    Text = "Track the route"
                };

                /// Create new Button for satellite
                buttonSatellite = new Button
                {
                    Text = "Satellite information"
                };

                /// Create new Button to stop location service
                buttonStop = new Button
                {
                    Text = "Stop location service"
                };

                /// Create new Label to show current status
                textStatus = new Label();
                textTrack = new Label();
                textMessage = new Label();
                textStatus.Text = "[Status]";

                /// Content view of this page
                Content = new StackLayout
                {
                    /// Set padding and orientation
                    Padding = new Thickness(20, 20),
                    Orientation = StackOrientation.Vertical,

                    Children =
                    {
                        /// <summary>
                        /// Application title
                        /// </summary>
                        new Label
                        {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Location",
                            FontSize = 20,
                        },

                        /// Add buttons to the StackLayout
                        buttonStart,
                        buttonGetLocation,
                        buttonBoundary,
                        buttonTrack,
                        buttonSatellite,
                        buttonStop,

                        /// Add Label to show status message
                        textStatus,
                        textMessage,
                        textTrack,
                    }
                };

                /// Add button clicked event
                buttonStart.Clicked += ButtonStartClicked;
                buttonGetLocation.Clicked += ButtonGetLocationClicked;
                buttonBoundary.Clicked += ButtonBoundaryClicked;
                buttonTrack.Clicked += ButtonTrackClicked;
                buttonSatellite.Clicked += ButtonSatelliteClicked;
                buttonStop.Clicked += ButtonStopClicked;

                /// Set initial state of button enabled
                buttonGetLocation.IsEnabled = false;
                buttonBoundary.IsEnabled = false;
                buttonTrack.IsEnabled = false;
                buttonSatellite.IsEnabled = false;
                buttonStop.IsEnabled = false;

                /// Check location permission
                PrivilegeCheck();
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
                    textStatus.Text = "[Status] Privilege : " + ex.Message;
                }
            }

            /// <summary>
            /// Button event to start location service
            /// </summary>
            /// <param name="sender">object</param>
            /// <param name="e">EventArgs</param>
            private void ButtonStartClicked(object sender, EventArgs e)
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
                            textStatus.Text = "[Status] Start location service, GPS searching ...";

                            /// Add ServiceStateChanged event to receive the event regarding service state
                            locator.ServiceStateChanged += Locator_ServiceStateChanged;

                            /// Disable start button to avoid duplicated call.
                            buttonStart.IsEnabled = false;

                            // Enable available buttons
                            buttonSatellite.IsEnabled = true;
                            buttonStop.IsEnabled = true;
                        }
                        else
                        {
                            /// Locator creation failed
                            textStatus.Text = "[Status] Location initialize .. Failed";
                        }
                    }
                    catch (Exception ex)
                    {
                        /// Exception handling
                        textStatus.Text = "[Status] Location Initialize : " + ex.Message;
                    }
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
                    textStatus.Text = "[Status] Location service enabled";

                    /// Update button status
                    buttonStart.IsEnabled = false;

                    /// Enable buttons to show available method
                    buttonTrack.IsEnabled = true;
                    buttonGetLocation.IsEnabled = true;
                    buttonBoundary.IsEnabled = true;
                }
                else
                {
                    /// Service state changed to Disabled
                    textStatus.Text = "[Status] Service disabled, GPS searching ...";
                }
            }

            /// <summary>
            /// Button event to get current location
            /// </summary>
            /// <param name="sender">object</param>
            /// <param name="e">EventArgs</param>
            private void ButtonGetLocationClicked(object sender, EventArgs e)
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
                    textMessage.Text = "[GetLocation]" + "\n  Timestamp : " + l.Timestamp + "\n  Latitude : " + l.Latitude + "\n  Longitude : " + l.Longitude;
                }
                catch (Exception ex)
                {
                    /// Exception when location service is not available
                    textMessage.Text = "[GetLocation]" + "\n  Exception : " + ex.Message;
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
                textTrack.Text = "[Tracking] " + "\n  Latitude : " + e.Location.Latitude + "\n  Longitude : " + e.Location.Longitude;
            }

            /// <summary>
            /// Remove LocationChanged event
            /// </summary>
            private void CancelTrack()
            {
                /// Remove LocationChanged event
                locator.LocationChanged -= Locator_LocationChanged;
                buttonTrack.Text = "Track the route";
                isTrack = false;
            }

            /// <summary>
            /// Button event for location tracking
            /// </summary>
            /// <param name="sender">object</param>
            /// <param name="e">EventArgs</param>
            private void ButtonTrackClicked(object sender, EventArgs e)
            {
                if (isTrack == true)
                {
                    CancelTrack();
                    textTrack.Text = "";
                }
                else
                {
                    /// Add LocationChanged event
                    locator.LocationChanged += Locator_LocationChanged;
                    buttonTrack.Text = "Cancel route tracking";
                    isTrack = true;
                }
            }

            /// Cancel satellite information
            private void CancelSatellite()
            {
                /// Remove SatelliteStatusUpdated event
                satellite.SatelliteStatusUpdated -= Satellite_SatelliteStatusUpdated;
                buttonSatellite.Text = "Satellite information";
                isSatellite = false;
            }

            /// <summary>
            /// Button event to receive satellite information
            /// </summary>
            /// <param name="sender">object</param>
            /// <param name="e">EventArgs</param>
            private void ButtonSatelliteClicked(object sender, EventArgs e)
            {
                if (isSatellite == true)
                {
                    /// Cancel satellite information
                    CancelSatellite();
                    textMessage.Text = "";
                }
                else
                {
                    /// Add SatelliteStatusUpdated event
                    try
                    {
                        satellite.SatelliteStatusUpdated += Satellite_SatelliteStatusUpdated;
                        buttonSatellite.Text = "Cancel satellite information";
                        isSatellite = true;
                    }
                    catch (Exception ex)
                    {
                        /// Exception handling
                        textMessage.Text = "[Satellite]\n  Exception : " + ex.Message;
                    }
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
                        textMessage.Text = "[Satellite]" + "\n  Timestamp : " + e.Timestamp + "\n  Active : " + e.ActiveCount + " , Inview : " + e.InViewCount + "\n  azimuth[" + s.Azimuth + "] elevation[" + s.Elevation + "] prn[" + s.Prn + "]\n  snr[" + s.Snr + "] active[" + s.Active + "]";
                    }
                    else
                    {
                        /// ActiveCount and InViewCount are available when SatelliteSttatusUpdated event invoked.
                        textMessage.Text = "[Satellite]" + "\n  Timestamp : " + e.Timestamp + "\n  Active : " + e.ActiveCount + " , Inview : " + e.InViewCount;
                    }
                }
                catch (Exception ex)
                {
                    /// Exception when location service is not available
                    textMessage.Text = "[Satellite]\n  Exception : " + ex.Message;
                }
            }

            /// Cancel boundary
            private void CancelBoundary()
            {
                /// Remove ZoneChanged event
                locator.ZoneChanged -= Locator_ZoneChanged;
                buttonBoundary.Text = "Location boundary";
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
            /// <param name="sender">object</param>
            /// <param name="e">EventArgs</param>
            private void ButtonBoundaryClicked(object sender, EventArgs e)
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
                        textMessage.Text = "";
                    }
                    else
                    {
                        buttonBoundary.Text = "Cancel location boundary";
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
                    textMessage.Text = "[Boundary]\n  Exception : " + ex.Message;
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
                textMessage.Text = "[Boundary]\n  Timestamp : " + e.Timestamp + "\n  BoundState : " + e.BoundState + "\n  Latitude : " + e.Latitude + "\n  Longitude : " + e.Longitude;
            }

            /// <summary>
            /// Button event to stop location service
            /// </summary>
            /// <param name="sender">object</param>
            /// <param name="e">EventArgs</param>
            private void ButtonStopClicked(object sender, EventArgs e)
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
                    textMessage.Text = "";
                    textTrack.Text = "";

                    /// Stop to location service
                    locator.Stop();

                    /// Dispose to release location resource
                    locator.Dispose();
                    locator = null;
                    satellite = null;

                    /// Enable location button after Stop() method called.
                    buttonStart.IsEnabled = true;

                    /// Disable buttons when location in not available.
                    buttonTrack.IsEnabled = false;
                    buttonBoundary.IsEnabled = false;
                    buttonGetLocation.IsEnabled = false;
                    buttonSatellite.IsEnabled = false;
                    buttonStop.IsEnabled = false;
                    textStatus.Text = "[Status] Stop location service";
                }
                catch (Exception ex)
                {
                    /// Exception handling
                    textMessage.Text = "[Stop]\n  Exception : " + ex.Message;
                }
            }
        }
    }
}
