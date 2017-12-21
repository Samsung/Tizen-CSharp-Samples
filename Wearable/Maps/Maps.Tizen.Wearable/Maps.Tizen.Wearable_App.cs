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
using Tizen.Applications;
using System.Linq;
using ElmSharp;
using ElmSharp.Wearable;
using Tizen.Maps;
using Tizen;

namespace Maps.Tizen.Wearable
{
	/// <summary>
	/// Maps application main class.
	/// </summary>
	class App : CoreUIApplication
    {
		/// <summary>
		/// The enum type for view page.
		/// </summary>
		public enum ViewPage
		{
			MAP,
			MAP_IN_PROGRESS,
			ROUTE,
			ROUTE_IN_PROGRESS,
			POI,
			POI_IN_PROGRESS,
		};

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
		public string HERE_KEY = "57l3m35FktUN4sBpDTcJ/ckFerp2t5bCVjouh9v2HCw";

		/// <summary>
		/// The list for the place.
		/// </summary>
		public List<Place> PlaceList = null;

		/// <summary>
		/// The list for the marker.
		/// </summary>
		public List<Pin> MarkerList = new List<Pin>();

		/// <summary>
		/// The categories for the HERE provider.
		/// </summary>
		public string[] HereCategory = { "eat-drink", "transport", "accommodation", "shopping", "leisure-outdoor" };

		/// <summary>
		/// The current view.
		/// </summary>
		public ViewPage view = ViewPage.MAP;

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
		/// The Window object.
		/// </summary>
		public Window window = null;

		/// <summary>
		/// The Naviframe object.
		/// </summary>
		public Naviframe navi = null;

		/// <summary>
		/// The coordinate for the starting position.
		/// </summary>
		public Geocoordinates fromPosition = null;

		/// <summary>
		/// The coordinate for the end position.
		/// </summary>
		public Geocoordinates toPosition = null;

		/// <summary>
		/// Handle when your app creates.
		/// </summary>
		protected override void OnCreate()
        {
            base.OnCreate();
			//Request the user consent
			RequestUserConsent();
        }

		/// <summary>
		/// Request the user consent.
		/// </summary>
		public async void RequestUserConsent()
		{
			// Create the MapService for the HERE provider
			s_maps = new MapService("HERE", HERE_KEY);

			// Request the user consent
			// The user consent popup will be displayed if the value is false
			await s_maps.RequestUserConsent();

			// Check the user's choice in the user consent popup
			if (s_maps.UserConsented != true)
			{
				// Dispose the s_maps
				s_maps.Dispose();
				s_maps = null;

				// Close this app
				Exit();
			}
			else
			{
				// Create a base UI
				Initialize();
			}
		}

		/// <summary>
		/// Create a base UI.
		/// </summary>
		void Initialize()
        {
			// Create a Window
			window = new Window("ElmSharpApp")
			{
				AvailableRotations = DisplayRotation.Degree_0 | DisplayRotation.Degree_180 | DisplayRotation.Degree_270 | DisplayRotation.Degree_90,
				AutoDeletion = true,
            };
            window.Deleted += (s, e) => Exit();
            window.Show();

			// Create a Conformant
			var conf = new Conformant(window)
			{
				WeightX = 1.0,
				WeightY = 1.0,
			};
			window.AddResizeObject(conf);
			conf.Show();

			// Create a Naviframe
			navi = new Naviframe(window);
			conf.SetContent(navi);
			navi.BackButtonPressed += (s, e) => CloseApp();

			CreateMap();
		}

		/// <summary>
		/// Create a MapView object.
		/// </summary>
		public void CreateMap()
		{
			// Create the MapView
			var layout = new Layout(navi);
			s_mapview = new MapView(layout, s_maps);
			// Move the MapView
			s_mapview.Move(0, 0);
			// Resize the MapView
			s_mapview.Resize(360, 360);
			// Show the MapView
			s_mapview.Show();
			// Set the latitude and longitude for the center position of the MapView
			//s_mapview.Center = new Geocoordinates(SEOUL_LAT, SEOUL_LON);
			s_mapview.Center = new Geocoordinates(DEFAULT_LAT, DEFAULT_LON);
			// Set the zoom level
			s_mapview.ZoomLevel = 9;
			// Add the handler for the longpress event on MapView
			s_mapview.LongPressed += MapViewLongPressed;

			// Create the MoreOption
			var viewOption = new MoreOption(window)
			{
				AlignmentX = -1,
				AlignmentY = -1,
				WeightX = 1,
				WeightY = 1,
				Direction = MoreOptionDirection.Right
			};
			// Move the viewOption
			viewOption.Move(180, 180);
			// Show the viewOption
			viewOption.Show();
			viewOption.Clicked += ViewOptionSelected;

			// Create and add items of the MoreOption
			viewOption.Items.Add(new MoreOptionItem() { MainText = "Map" });
			viewOption.Items.Add(new MoreOptionItem() { MainText = "POI" });
			viewOption.Items.Add(new MoreOptionItem() { MainText = "Route" });

			RotaryEventManager.Rotated += (e) =>
			{
				if (viewOption.IsOpened == false)
				{
					if (e.IsClockwise)
					{
						s_mapview.ZoomLevel += 1;
					}
					else
					{
						s_mapview.ZoomLevel -= 1;
					}
				}
			};

			var mapPage = navi.Push(layout, null, "empty");
			navi.Popped += (s, e) => { viewOption.Unrealize(); };
		}

		/// <summary>
		/// Handle the event of the longpress on the MapView.
		/// </summary>
		/// <param name="sender">Specifies the sender object</param>
		/// <param name="oe">Specifies the occured event</param>
		private void ViewOptionSelected(object sender, MoreOptionItemEventArgs oe)
		{
			// Remove the used data
			ClearData();

			if (oe.Item.MainText == "Map")
			{
				view = ViewPage.MAP;
				((MoreOption)sender).IsOpened = false;
			}
			else if (oe.Item.MainText == "POI")
			{
				view = ViewPage.POI;

				// Create the RotarySelector for the category
				var poiSelector = new RotarySelector(window)
				{
					AlignmentX = -1,
					AlignmentY = -1,
					WeightX = 1,
					WeightY = 1
				};
				foreach (string category in HereCategory)
				{
					poiSelector.Items.Add(new RotarySelectorItem() { MainText = category });
				}

				poiSelector.Show();

				poiSelector.Clicked += (s, e) => 
				{
					// Request the pois with the center position
					RequestPOI(new Geocoordinates(s_mapview.Center.Latitude, s_mapview.Center.Longitude), e.Item.MainText);

					poiSelector.Unrealize();

					((MoreOption)sender).IsOpened = false;
				};
				poiSelector.BackButtonPressed += (s, e) => { poiSelector.Unrealize(); };
			}
			else if (oe.Item.MainText == "Route")
			{
				view = ViewPage.ROUTE;
				((MoreOption)sender).IsOpened = false;
			}
		}

		/// <summary>
		/// Handle the event of the longpress on the MapView.
		/// </summary>
		/// <param name="sender">Specifies the sender object</param>
		/// <param name="e">Specifies the occured event</param>
		private void MapViewLongPressed(object sender, MapGestureEventArgs e)
		{
			// Set the zoom level
			if (s_mapview.ZoomLevel < 13)
			{
				s_mapview.ZoomLevel = 13;
			}

			// Check the viewpage
			if (view == ViewPage.MAP)
			{
				// Remove the used data
				ClearData();
				// Move to the longpressed position
				s_mapview.Center = e.Geocoordinates;
				// Add the pin to the center positon of the map view
				s_mapview.Add(new Pin(s_mapview.Center));
				// Request the address by the center position of the map view and display the address to the label of the starting position
				RequestAddress(s_mapview.Center.Latitude, s_mapview.Center.Longitude);
			}
			else if (view == ViewPage.ROUTE)
			{
				if (fromPosition != null && toPosition != null)
				{
					// Remove the used data
					ClearData();
				}

				// Move to the longpressed position
				s_mapview.Center = e.Geocoordinates;

				// Check the text of the from the label
				if (fromPosition == null)
				{
					// Add a marker to the center position
					s_mapview.Add(new Pin(s_mapview.Center));
					// Create the Geocoordinates from the center position
					fromPosition = new Geocoordinates(s_mapview.Center.Latitude, s_mapview.Center.Longitude);
				}
				else
				{
					// Add a marker to the center position
					s_mapview.Add(new Sticker(s_mapview.Center));
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
		/// <param name="latitude">Specifies the latitude for getting the address</param>
		/// <param name="longitude">Specifies the longitude for getting the address</param>
		public async void RequestAddress(double latitude, double longitude)
		{
			try
			{
				view = ViewPage.MAP_IN_PROGRESS;
				// Request an address with the latitude and longitude
				var response = await s_maps.CreateReverseGeocodeRequest(latitude, longitude).GetResponseAsync();
				// Set the address to the popup
				if (view == ViewPage.MAP_IN_PROGRESS)
				{
					CreatePopup(response.First().Building.ToString() + " " + response.First().Street + " " + response.First().City + " " + response.First().State + " " + response.First().Country);
					view = ViewPage.MAP;
				}
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
				view = ViewPage.ROUTE_IN_PROGRESS;
				// Request a route with between two positions
				var response = await s_maps.CreateRouteSearchRequest(from, to).GetResponseAsync();
				// Get the route
				IEnumerator<Route> route = response.GetEnumerator();

				route.MoveNext();

				if (view == ViewPage.ROUTE_IN_PROGRESS)
				{
					// Display the polylines after making it from the path of the route
					s_mapview.Add(new Polyline((List<Geocoordinates>)route.Current.Path, ElmSharp.Color.Red, 5));

					string distance;
					if (route.Current.Unit == DistanceUnit.Kilometer)
					{
						distance = string.Format("{0,10:N2}", route.Current.Distance);
					}
					else
					{
						distance = string.Format("{0,10:N2}", route.Current.Distance / 1000);
					}

					string duration = string.Format("{0,10:N2}", route.Current.Duration / 60);

					CreatePopup(distance + " km / " + duration + " min by car");

					view = ViewPage.ROUTE;
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
				view = ViewPage.POI_IN_PROGRESS;
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
				if (view == ViewPage.POI_IN_PROGRESS)
				{
					for (int i = 0; i < PlaceList.Count; i++)
					{
						// Create pins with the places and add it to marker list
						MarkerList.Add(new Pin(PlaceList[i].Coordinates));
						// Show the markers
						s_mapview.Add(MarkerList[i]);
						// Add a handler to click the marker
						MarkerList[i].Clicked += (sender, e) => { SetCurrentMarker((Marker)sender); };
					}

					// Set the current marker
					SetCurrentMarker(MarkerList[0]);

					view = ViewPage.POI;
				}
			}
			catch (Exception e)
			{
				// Display logs with the error message
				Log.Debug("Map", e.Message.ToString());
			}
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
			// Display the current marker's name to the popup
			CreatePopup(PlaceList[currentIndex].Name);
		}

		/// <summary>
		/// Create the toast popup
		/// </summary>
		/// <param name="text">Specifies the text</param>
		private void CreatePopup(string text)
		{
			// Create a popup
			Popup popup = new Popup(window)
			{
				WeightX = 1,
				WeightY = 1,
				Style = "toast/circle",
				Timeout = 2.0,
				Text = text,
			};
			popup.Show();

			popup.BackButtonPressed += PopupDismiss;
			popup.TimedOut += PopupDismiss;
		}

		/// <summary>
		/// Dismiss the toast popup
		/// </summary>
		/// <param name="sender">Specifies the object</param>
		/// <param name="e">Specifies the EventArgs</param>
		private void PopupDismiss(object sender, EventArgs e)
		{
			((Popup)sender).Dismiss();
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
			{
				s_mapview.RemoveAll();
			}

			fromPosition = null;
			toPosition = null;
		}

		/// <summary>
		/// Remove the used resource and terminate this application.
		/// </summary>
		public void CloseApp()
		{
			// Remove the used data
			ClearData();

			// Remove all the map object from the map view
			if (s_mapview != null)
			{
				s_mapview.RemoveAll();
				s_mapview.LongPressed -= MapViewLongPressed;
				s_mapview.Dispose();
				s_mapview = null;
			}

			// Remove all the map object from the map view
			if (s_maps != null)
			{
				s_maps.Dispose();
				s_mapview = null;
			}

			Exit();
		}

		/// <summary>
		/// The entry point for the application.
		/// </summary>
		/// <param name="args"> A list of command line arguments.</param>
		static void Main(string[] args)
        {
            Elementary.Initialize();
            Elementary.ThemeOverlay();
            App app = new App();
            app.Run(args);
        }
    }
}
