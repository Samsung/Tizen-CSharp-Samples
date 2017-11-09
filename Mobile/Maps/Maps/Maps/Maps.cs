/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
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
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using ElmSharp;
using Tizen;
using Tizen.Maps;

namespace Maps
{
    /// <summary>
    /// The enum type for view page.
    /// </summary>
    public enum ViewPage
    {
        MAP,
        ROUTE,
        POI,
		POISELECTED,
    };

    /// <summary>
    /// The Maps application main class.
    /// <list>
    /// <item>Create 4 labels for information. This labels are for functions, errors, geofence status and proximity status.</item>
    /// <item>Create 8 buttons for the general functions of geofence and handlers for each buttons.</item>
    /// </list>
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// The MapService object.
        /// </summary>
        public MapService s_maps = null;

        /// <summary>
        /// The MapView object.
        /// </summary>
        public MapView s_mapview = null;

		/// <summary>
		/// The key of HERE provider. Please get the key from https://developer.here.com/.
		/// You can see the guide about getting the key in https://developer.tizen.org/development/guides/native-application/location-and-sensors/maps-and-maps-service/getting-here-maps-credentials.
		/// </summary>
		public string HERE_KEY = "HERE-KEY";

		/// <summary>
		/// The key of MAPZEN provider. Please get the key from https://mapzen.com/developers.
		/// You can see the guide about getting the key in https://developer.tizen.org/development/guides/native-application/location-and-sensors/maps-and-maps-service/getting-mapzen-api-key.
		/// </summary>
		public string MAPZEN_KEY = "MAPZEN-KEY";

		/// <summary>
		/// The Window object.
		/// </summary>
		public Window window = null;

        /// <summary>
        /// The selected provider.
        /// </summary>
        public string selectedProvider = null;

        /// <summary>
        /// The current view.
        /// </summary>
        public ViewPage view = ViewPage.MAP;

        /// <summary>
        /// The label for the information of the starting position.
        /// </summary>
        public ElmSharp.Label fromLabel = null;

        /// <summary>
        /// The label for the information of the end position.
        /// </summary>
        public ElmSharp.Label toLabel = null;

		/// <summary>
		/// The context popup for the ViewPage.
		/// </summary>
		public ContextPopup viewCtxPopup = null;

		/// <summary>
		/// The context popup for the POI category.
		/// </summary>
		public ContextPopup categoryCtxPopup = null;

		/// <summary>
		/// The list for the place.
		/// </summary>
		public List<Place> PlaceList = null;

        /// <summary>
        /// The list for the marker.
        /// </summary>
        public List<global::Tizen.Maps.Pin> MarkerList = new List<global::Tizen.Maps.Pin>();

        /// <summary>
        /// The categories for the HERE provider.
        /// </summary>
        public string[] HereCategory = { "eat-drink", "transport", "accommodation", "shopping", "leisure-outdoor" };

        /// <summary>
        /// The categories for the MAPZEN provider.
        /// </summary>
        public string[] MapzenCategory = { "food", "transport", "accommodation", "finance", "entertainment" };

        /// <summary>
        /// The latitude value for the seoul.
        /// </summary>
        public double SEOUL_LAT = 37.466548;

        /// <summary>
        /// The longitude value for the seoul.
        /// </summary>
        public double SEOUL_LON = 127.022782;

        /// <summary>
        /// The default latitude value.
        /// </summary>
        public double DEFAULT_LAT = 28.64362;

        /// <summary>
        /// The default longitude value.
        /// </summary>
        public double DEFAULT_LON = 77.19865;

        /// <summary>
        /// The coordinate for the starting position.
        /// </summary>
        public Geocoordinates fromPosition = null;

        /// <summary>
        /// The coordinate for the end position.
        /// </summary>
        public Geocoordinates toPosition = null;

		/// <summary>
		/// Create a MapService, MapView and Window object.
		/// </summary>
		/// <param name="selectedTC">The selected TextCell</param>
		public async void CreateMapService(TextCell selectedTC)
		{
			// Set the disable for the selected TextCell
			selectedTC.IsEnabled = false;
			// Set the provider to the selected provider
			selectedProvider = selectedTC.Text;

			// check the provider
			if (selectedProvider == "HERE")
            {
                // Create the MapService for the HERE provider
                s_maps = new MapService(selectedProvider, HERE_KEY);
            }
            else if (selectedProvider == "MAPZEN")
            {
                // Create the MapService for the MAPZEN provider
                s_maps = new MapService(selectedProvider, MAPZEN_KEY);
            }

            // Request the user consent
            // The user consent popup will be displayed if the value is false
            await s_maps.RequestUserConsent();

			// Set the enable for the selected TextCell
			selectedTC.IsEnabled = true;

			// Check the user's choice in the user consent popup
			if (s_maps.UserConsented != true)
            {
				// Dispose the MapService
				s_maps.Dispose();
				s_maps = null;

				// return to the view for the provider selection
				return;
			}

			// Create the window
			window = new Window("MapView");
			window.KeyGrab(EvasKeyEventArgs.PlatformBackButtonName, false);
			window.KeyUp += (s, e) =>
            {
                if (e.KeyName == EvasKeyEventArgs.PlatformBackButtonName)
                {
                    // Close the map if the back key is selected
                    CloseMap();
                }
				else if (e.KeyName == EvasKeyEventArgs.PlatformMenuButtonName)
				{
					// Create the ContextPopup for the ViewPage selection
					viewCtxPopup = new ContextPopup(window)
					{
						// Set the AutoHide value
						AutoHide = true,
					};
					// Append items for the ContextPopup and add event handlers
					(viewCtxPopup.Append("Map")).Selected += ViewPageSelected;
					(viewCtxPopup.Append("POI")).Selected += ViewPageSelected;
					(viewCtxPopup.Append("Route")).Selected += ViewPageSelected;
					// Move the ContextPopup
					viewCtxPopup.Move(0, window.ScreenSize.Height);
					// Show the ContextPopup
					viewCtxPopup.Show();
				}
            };
			// Show the window
			window.Show();

			var box = new Box(window)
			{
				AlignmentX = -1,
				AlignmentY = -1,
				WeightX = 1,
				WeightY = 1,
			};
			box.Show();

			var bg = new Background(window)
			{
				Color = ElmSharp.Color.White
			};
			bg.SetContent(box);

			// Create the MapView
			s_mapview = new MapView(window, s_maps);
			// Move the MapView
			s_mapview.Move(0, 0);
			// Resize the MapView
			s_mapview.Resize(window.ScreenSize.Width, window.ScreenSize.Height);
			// Show the MapView
			s_mapview.Show();
			// Set the latitude and longitude for the center position of the MapView
			//s_mapview.Center = new Geocoordinates(SEOUL_LAT, SEOUL_LON);
			s_mapview.Center = new Geocoordinates(DEFAULT_LAT, DEFAULT_LON);
			// Set the zoom level
			s_mapview.ZoomLevel = 9;
			// Add the handler for the longpress event on MapView
			s_mapview.LongPressed += MapViewLongPressed;

			// Create the labels
			CreateLabel();
        }

        /// <summary>
        /// Handle the event of the longpress on the MapView.
        /// </summary>
        /// <param name="sender">Specifies the sender object</param>
        /// <param name="e">Specifies the occured event</param>
        private void MapViewLongPressed(object sender, MapGestureEventArgs e)
        {
            // Check the viewpage
            if (view == ViewPage.MAP)
            {
                // Remove all the map object from the map view
                s_mapview.RemoveAll();
				// Set the zoom level
				s_mapview.ZoomLevel = 13;
				// Move to the longpressed position
				s_mapview.Center = e.Geocoordinates;
                // Add the pin to the center positon of the map view
                s_mapview.Add(new global::Tizen.Maps.Pin(s_mapview.Center));
                // Request the address by the center position of the map view and display the address to the label of the starting position
                RequestAddress(fromLabel, s_mapview.Center.Latitude, s_mapview.Center.Longitude);
            }
            else if (view == ViewPage.ROUTE)
            {
				// Set the zoom level
				s_mapview.ZoomLevel = 13;
				// Check the text of the fromLabel and the toLabel. The route is being displayed if the text of the labels is not empty.
				if ((fromLabel.Text != "" && toLabel.Text != "") || (fromLabel.Text == "" && toLabel.Text == ""))
				{
					// Remove the used resource
					ClearData();
				}

                // Move to the longpressed position
                s_mapview.Center = e.Geocoordinates;

                // Check the text of the from the label
                if (fromPosition == null)
                {
                    // Add a marker to the center position
                    s_mapview.Add(new global::Tizen.Maps.Pin(s_mapview.Center));
                    // Request an address with the center position
                    RequestAddress(fromLabel, s_mapview.Center.Latitude, s_mapview.Center.Longitude);
                    // Create the Geocoordinates from the center position
                    fromPosition = new Geocoordinates(s_mapview.Center.Latitude, s_mapview.Center.Longitude);
                }
                else if (fromLabel.Text != "" && toPosition == null)
				{
                    // Add a marker to the center position
                    s_mapview.Add(new global::Tizen.Maps.Sticker(s_mapview.Center));
                    // Request an address with the center position
                    RequestAddress(toLabel, s_mapview.Center.Latitude, s_mapview.Center.Longitude);
                    // Create the Geocoordinates from the center position
                    toPosition = new Geocoordinates(s_mapview.Center.Latitude, s_mapview.Center.Longitude);
                    // Request a route with the fromPosition and the toPosition
                    RequestRoute(fromPosition, toPosition);
                }
            }
        }

        /// <summary>
        /// Request an address with the latitude and longitude.
        /// </summary>
        /// <param name="label">Specifies the label for displaying the result</param>
        /// <param name="latitude">Specifies the latitude for getting the address</param>
        /// <param name="longitude">Specifies the longitude for getting the address</param>
        public async void RequestAddress(ElmSharp.Label label, double latitude, double longitude)
        {
            try
            {
				// clear the text
				label.Text = "";
                // Request an address with the latitude and longitude
                var response = await s_maps.CreateReverseGeocodeRequest(latitude, longitude).GetResponseAsync();
                // Set the address to the label
                label.Text = response.First().Building.ToString() + " " + response.First().Street + " " + response.First().City + " " + response.First().State + " " + response.First().Country;
            }
            catch (Exception e)
            {
                // Display logs with the error message
                Log.Debug("Map", e.Message.ToString());
            }
        }

        /// <summary>
        /// Request a route between two positions.
        /// </summary>
        /// <param name="from">Specifies the starting position</param>
        /// <param name="to">Specifies the end position</param>
        public async void RequestRoute(Geocoordinates from, Geocoordinates to)
        {
            try
            {
                // Request a route with between two positions
                var response = await s_maps.CreateRouteSearchRequest(from, to).GetResponseAsync();
                // Get the route
                IEnumerator<Route> route = response.GetEnumerator();
                while (route.MoveNext())
                {
                    // Display the polylines after making it from the path of the route
					if (view == ViewPage.ROUTE)
						s_mapview.Add(new Polyline((List<Geocoordinates>)route.Current.Path, ElmSharp.Color.Red, 5));
                }
            }
            catch (Exception e)
            {
                // Display logs with the error message
                Log.Debug("Map", e.Message.ToString());
            }
        }

        /// <summary>
        /// Request pois with the position and the category.
        /// </summary>
        /// <param name="coordinate">Specifies the starting position</param>
        /// <param name="Category">Specifies the end position</param>
        public async void RequestPOI(Geocoordinates coordinate, string Category)
        {
			try
            {
                // Set the category
                s_maps.PlaceSearchFilter.Category.Id = Category;
                /// Request pois with the position and the category.
                var response = await s_maps.CreatePlaceSearchRequest(coordinate, 500).GetResponseAsync();
                // Get the places
                IEnumerator<Place> place = response.GetEnumerator();
                // Create the list for places
                PlaceList = new List<Place>();
                while (place.MoveNext())
                {
                    // Add the place to the list
                    PlaceList.Add(place.Current);
                }

                // Sort by distance
                PlaceList = PlaceList.OrderBy(x => x.Distance).ToList();
                for (int i = 0; i < PlaceList.Count; i++)
                {
                    // Create pins with the places and add it to marker list
                    MarkerList.Add(new global::Tizen.Maps.Pin(PlaceList[i].Coordinates));
                    // Show the markers
					if (view == ViewPage.POI)
						s_mapview.Add(MarkerList[i]);
                    // Add a handler to click the marker
                    MarkerList[i].Clicked += (sender, e) => { SetCurrentMarker((Marker)sender); };
                }

                // Set the current marker
				if (view == ViewPage.POI)
                    SetCurrentMarker(MarkerList[0]);
            }
            catch (Exception e)
            {
                // Display logs with the error message
                Log.Debug("Map", e.Message.ToString());
            }
			// Set the viewpage
			if (view == ViewPage.POI)
				view = ViewPage.POISELECTED;
        }

        /// <summary>
        /// Display the current marker.
        /// </summary>
        /// <param name="marker">Specifies the current marker</param>
        public void SetCurrentMarker(Marker marker)
        {
            int currentIndex = 0;

            for (int i = 0; i < PlaceList.Count; i++)
            {
                // Set the zorder of the other markers
                MarkerList[i].ZOrder = 0;
                // Set the size of the other markers
                MarkerList[i].Resize(new ElmSharp.Size(30, 30));

                if (MarkerList[i] == marker)
                {
                    currentIndex = i;
                }
            }

            // Set the zorder of the current marker
            marker.ZOrder = 100;
            // Set the size of the current marker
            marker.Resize(new ElmSharp.Size(60, 60));
            // Set the center position to the current marker's position
            s_mapview.Center = marker.Coordinates;
            // Display the current marker's name to the label
            fromLabel.Text = PlaceList[currentIndex].Name;
        }

        /// <summary>
        /// Create the labels.
        /// </summary>  
        public void CreateLabel()
        {
            // Create the label for the starting position
            fromLabel = new ElmSharp.Label(window)
            {
                // Set the default text
                Text = "",
                // Set the default color
                Color = ElmSharp.Color.Silver,
                // Set the default background color
                BackgroundColor = ElmSharp.Color.White
            };
            // Move the label
            fromLabel.Move(5, 5);
            // Resize the label
            fromLabel.Resize(710, 70);
            // Show the label
            fromLabel.Show();
            // Set the handler for the label. Remove the focus from the label when it is focused
            fromLabel.Focused += (sender, e) => { ((ElmSharp.Label)sender).SetFocus(false); };

            // Create the label for the end position
            toLabel = new ElmSharp.Label(window)
            {
                // Set the default text
                Text = "",
                // Set the default color
                Color = ElmSharp.Color.Silver,
                // Set the default background color
                BackgroundColor = ElmSharp.Color.White
            };
            // Move the label
            toLabel.Move(5, 80);
            // Resize the label
            toLabel.Resize(710, 70);
            // Add the handler for this label. The focus of the label will be removed if it is clicked
            toLabel.Focused += (sender, e) => { ((ElmSharp.Label)sender).SetFocus(false); };
        }

		/// <summary>
		/// Handle the event of the view selection in the ContextPopup.
		/// </summary>
		/// <param name="sender">Specifies the sender object</param>
		/// <param name="e">Specifies the occured event</param>
		private void ViewPageSelected(object sender, EventArgs e)
        {
			
            // Remove the used data
            ClearData();

			if (((ContextPopupItem)sender).Text == "Map")
			{
				// Hide the label for the end position
				toLabel.Hide();
				// Set the viewpage
				view = ViewPage.MAP;
			}
			else if (((ContextPopupItem)sender).Text == "POI")
			{
                // Hide the label for the end position
                toLabel.Hide();
                // Set the viewpage
                view = ViewPage.POI;

				// Create the ContextPopup for the category selection
				categoryCtxPopup = new ContextPopup(window)
				{
					// Set the AutoHide value
					AutoHide = true,
				};

				// Check the selected provider
				if (selectedProvider == "HERE")
				{
					// Get the categories of the here provider
					foreach (string category in HereCategory)
					{
						// Append items for the ContextPopup and add event handlers
						(categoryCtxPopup.Append(category)).Selected += CategorySelected;
					}
				}
				else if (selectedProvider == "MAPZEN")
				{
					// Get the categories of the mapzen provider
					foreach (string category in MapzenCategory)
					{
						// Append items for the ContextPopup and add event handlers
						(categoryCtxPopup.Append(category)).Selected += CategorySelected;
					}
				}
				// Move the ContextPopup
				categoryCtxPopup.Move(0, window.ScreenSize.Height);
				// Show the ContextPopup
				categoryCtxPopup.Show();
			}
			else if (((ContextPopupItem)sender).Text == "Route")
			{
				// Show the label for the end position
				toLabel.Show();
				// Set the viewpage
				view = ViewPage.ROUTE;
			}

			if (viewCtxPopup != null)
			{
				// Dismiss the ContextPopup object
				viewCtxPopup.Dismiss();
				viewCtxPopup = null;
			}
		}

		/// <summary>
		/// Handle the event of the category selection in the ContextPopup.
		/// </summary>
		/// <param name="sender">Specifies the sender object</param>
		/// <param name="e">Specifies the occured event</param>
		private void CategorySelected(object sender, EventArgs e)
		{
			// Remove the used data
			ClearData();
			// Request the pois with the center position
			RequestPOI(new Geocoordinates(s_mapview.Center.Latitude, s_mapview.Center.Longitude), ((ContextPopupItem)sender).Text);

			if (categoryCtxPopup != null)
			{
				// Dismiss the ContextPopup object
				categoryCtxPopup.Dismiss();
				categoryCtxPopup = null;
			}
		}

		/// <summary>
		/// Remove the used resource.
		/// </summary>
		public void ClearData()
        {
            // Clear the marker list
            MarkerList.Clear();

            if (PlaceList != null)
            {
                // Clear the place list
                PlaceList.Clear();
            }

            PlaceList = null;

            // Remove all the map object from the map view
			if (s_mapview != null)
				s_mapview.RemoveAll();
            fromPosition = null;
            toPosition = null;
            // Clear the label's text
			if (fromLabel != null)
				fromLabel.Text = "";
			if (toLabel != null)
				toLabel.Text = "";
        }

        /// <summary>
        /// Create the main page and add event handlers
        /// </summary>
        public App()
        {
			// Create a TableSelection
			TableSection selection = new TableSection("PROVIDERS") { };
			for (int i = 0; i < MapService.Providers.Count(); i++)
            {
                // Create a TextCell
                TextCell tc = new TextCell()
                {
                    // Set the Text to the provider
                    Text = MapService.Providers.ElementAt(i),
                };
				// Create a Command for the selection of the TextCell
				tc.Command = new Command(() => { CreateMapService(tc); });
				// Add the TextCell to the TableSelection
				selection.Add(tc);
            }

            // Create a ContentPage for the MainPage
            MainPage = new ContentPage
            {
                // Create a TableView for the content of the MainPage
                Content = new TableView { Root = new TableRoot { selection } }
            };
        }

        /// <summary>
        /// Handle when your app starts.
        /// </summary>
        protected override void OnStart()
        {
        }

        /// <summary>
        /// Handle when your app sleeps.
        /// </summary>
        protected override void OnSleep()
        {
        }

        /// <summary>
        /// Handle when your app resumes.
        /// </summary>
        protected override void OnResume()
        {
        }

        /// <summary>
        /// Remove the used resource and close the map.
        /// </summary>
        public void CloseMap()
        {
            // Remove the used resource
            ClearData();

			// check the viewCtxPopup
			if (viewCtxPopup != null)
			{
				// Dismiss the ContextPopup object
				viewCtxPopup.Dismiss();
				viewCtxPopup = null;
			}

			// check the categoryCtxPopup
			if (categoryCtxPopup != null)
			{
				// Dismiss the ContextPopup object
				categoryCtxPopup.Dismiss();
				categoryCtxPopup = null;
			}

			// check the window
			if (window != null)
            {
				// Unrealize the Window object
				window.Unrealize();
				window = null;
            }

			// check the s_mapview
			if (s_mapview != null)
			{
				// Dispose the MapView
				s_mapview.Dispose();
				s_mapview = null;
			}

			// check the s_maps
			if (s_maps != null)
			{
				// Dispose the MapService
				s_maps.Dispose();
				s_maps = null;
			}
		}
    }
}
