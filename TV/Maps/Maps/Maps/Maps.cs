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
        POI
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
        public Window win = null;

        /// <summary>
        /// The selected provider.
        /// </summary>
        public string selectedProvider = null;

        /// <summary>
        /// The current view.
        /// </summary>
        public ViewPage view = ViewPage.MAP;

        /// <summary>
        /// The button for the information of the starting position.
        /// </summary>
        public ElmSharp.Button fromBtn = null;

        /// <summary>
        /// The button for the information of the end position.
        /// </summary>
        public ElmSharp.Button toBtn = null;

        /// <summary>
        /// The button for the map view.
        /// </summary>
        public ElmSharp.Button MapBtn = null;

        /// <summary>
        /// The button for the route view.
        /// </summary>
        public ElmSharp.Button RouteBtn = null;

        /// <summary>
        /// The button for the poi view.
        /// </summary>
        public ElmSharp.Button PoiBtn = null;

        /// <summary>
        /// The list for the poi button.
        /// </summary>
        public List<ElmSharp.Button> POIBtnList = null;

        /// <summary>
        /// The list for the direction button.
        /// </summary>
        public List<ElmSharp.Button> DirectionBtnList = null;

        /// <summary>
        /// The list for the place.
        /// </summary>
        public List<Place> PlaceList = null;

        /// <summary>
        /// The list for the marker.
        /// </summary>
        public List<global::Tizen.Maps.Pin> MarkerList = new List<global::Tizen.Maps.Pin>();

        /// <summary>
        /// The index for the current marker.
        /// </summary>
        public int CurrentMarker = 0;

        /// <summary>
        /// The categories for the HERE provider.
        /// </summary>
        public string[] HereCategory = { "eat-drink", "transport", "accommodation", "shopping", "leisure-outdoor" };

        /// <summary>
        /// The categories for the MAPZEN provider.
        /// </summary>
        public string[] MapzenCategory = { "food", "transport", "accommodation", "finance", "entertainment" };

        /// <summary>
        /// The directions.
        /// </summary>
        public string[] DirectionText = { "£«", "£­", "¡ã", "¡å", "¢¸", "¢º", "¢Â" };

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
        /// <param name="provider">Specifies the provider</param>
        public async void CreateMapService(string provider)
        {
            // Set the provider to the selected provider
            selectedProvider = provider;

            // check the provider
            if (provider == "HERE")
            {
                // Create the MapService for the HERE provider
                s_maps = new MapService(provider, HERE_KEY);
            }
            else if (provider == "MAPZEN")
            {
                // Create the MapService for the MAPZEN provider
                s_maps = new MapService(provider, MAPZEN_KEY);
            }

            // Request the user consent
            // The user consent popup will be displayed if the value is false
            await s_maps.RequestUserConsent();

            // Check the user's choice in the user consent popup
            if (s_maps.UserConsented != true)
            {
                // Terminate this application if user doesn't agree the user consent popup
                CloseApp(this.MainPage);
            }

            // Create the window
            win = new Window("MapView");
            win.KeyGrab(EvasKeyEventArgs.PlatformBackButtonName, false);
            win.KeyUp += (s, e) =>
            {
                if (e.KeyName == EvasKeyEventArgs.PlatformBackButtonName)
                {
                    // Terminate this application if the back key is selected
                    CloseApp(this.MainPage);
                }
            };
            // Show the window
            win.Show();

            // Create the MapView
            s_mapview = new MapView(win, s_maps);
            // Move the MapView
            s_mapview.Move(330, 0);
            // Resize the MapView
            s_mapview.Resize(1580, 1070);
            // Set the latitude and longitude for the center position of the MapView
            //s_mapview.Center = new Geocoordinates(SEOUL_LAT, SEOUL_LON);
            s_mapview.Center = new Geocoordinates(DEFAULT_LAT, DEFAULT_LON);
            // Set the zoom level
            s_mapview.ZoomLevel = 10;
            // Show the MapView
            s_mapview.Show();

            CreateButton();
        }

        /// <summary>
        /// Request an address with the latitude and longitude.
        /// </summary>
        /// <param name="btn">Specifies the button for displaying the result</param>
        /// <param name="latitude">Specifies the latitude for getting the address</param>
        /// <param name="longitude">Specifies the longitude for getting the address</param>
        public async void RequestAddress(ElmSharp.Button btn, double latitude, double longitude)
        {
            try
            {
                // Request an address with the latitude and longitude
                var response = await s_maps.CreateReverseGeocodeRequest(latitude, longitude).GetResponseAsync();
                // Set the address to the button
                btn.Text = response.First().Building.ToString() + " " + response.First().Street + " " + response.First().City + " " + response.First().State + " " + response.First().Country;
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
                CurrentMarker = 0;
                for (int i = 0; i < PlaceList.Count; i++)
                {
                    // Create pins with the places and add it to marker list
                    MarkerList.Add(new global::Tizen.Maps.Pin(PlaceList[i].Coordinates));
                    // Show the markers
                    s_mapview.Add(MarkerList[i]);
                }

                // Set the current marker
                SetCurrentMarker(CurrentMarker);
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
        /// <param name="index">Specifies the index of the current marker</param>
        public void SetCurrentMarker(int index)
        {
            for (int i = 0; i < PlaceList.Count; i++)
            {
                // Set the zorder of the other markers
                MarkerList[i].ZOrder = 0;
                // Set the size of the other markers
                MarkerList[i].Resize(new ElmSharp.Size(30, 30));
            }

            // Set the zorder of the current marker
            MarkerList[index].ZOrder = 100;
            // Set the size of the current marker
            MarkerList[index].Resize(new ElmSharp.Size(60, 60));
            // Set the center position to the current marker's position
            s_mapview.Center = PlaceList[index].Coordinates;
            // Display the current marker's name to the button
            fromBtn.Text = PlaceList[index].Name;
        }

        /// <summary>
        /// Display the buttons.
        /// </summary>
        public void CreateButton()
        {
            // Create the button for the starting position
            fromBtn = new ElmSharp.Button(win)
            {
                // Set the default text
                Text = "",
                // Set the default color
                Color = ElmSharp.Color.Red,
                // Set the default background color
                BackgroundColor = ElmSharp.Color.Gray
            };
            // Move the button
            fromBtn.Move(580, 45);
            // Resize the button
            fromBtn.Resize(1300, 80);
            // Show the button
            fromBtn.Show();
            // Set the handler for the button. Remove the focus from the button when it is clocked
            fromBtn.Clicked += (sender, e) => { ((ElmSharp.Button)sender).SetFocus(false); };

            // Create the button for the end position
            toBtn = new ElmSharp.Button(win)
            {
                // Set the default text
                Text = "",
                // Set the default color
                Color = ElmSharp.Color.Red,
                // Set the default background color
                BackgroundColor = ElmSharp.Color.Gray
            };
            // Move the button
            toBtn.Move(580, 130);
            // Resize the button
            toBtn.Resize(1300, 80);
            // Add the handler for this button. The focus of the button will be removed if it is clicked
            toBtn.Clicked += (sender, e) => { ((ElmSharp.Button)sender).SetFocus(false); };

            // Create the button for the map viewpage
            MapBtn = new ElmSharp.Button(win)
            {
                // Set the default text
                Text = "Map",
                // Set the default color
                Color = ElmSharp.Color.White,
                // Set the default background color
                BackgroundColor = ElmSharp.Color.Gray
            };
            // Move the button
            MapBtn.Move(50, 100);
            // Resize the button
            MapBtn.Resize(250, 100);
            // Show the button
            MapBtn.Show();
            // Focus on this button
            MapBtn.SetFocus(true);

            // Create the button for the route viewpage
            RouteBtn = new ElmSharp.Button(win)
            {
                // Set the default text
                Text = "Route",
                // Set the default color
                Color = ElmSharp.Color.White,
                // Set the default background color
                BackgroundColor = ElmSharp.Color.Gray
            };
            // Move the button
            RouteBtn.Move(50, 200);
            // Resize the button
            RouteBtn.Resize(250, 100);
            // Show the button
            RouteBtn.Show();

            // Create the button for the poi viewpage
            PoiBtn = new ElmSharp.Button(win)
            {
                // Set the default text
                Text = "POI",
                // Set the default color
                Color = ElmSharp.Color.White,
                // Set the default background color
                BackgroundColor = ElmSharp.Color.Gray
            };
            // Move the button
            PoiBtn.Move(50, 300);
            // Resize the button
            PoiBtn.Resize(250, 100);
            // Show the button
            PoiBtn.Show();

            // Create a list for the the button of the poi's category
            POIBtnList = new List<ElmSharp.Button>();
            // Check the selected provider
            if (selectedProvider == "HERE")
            {
                // Get the categories of the here provider
                foreach (string category in HereCategory)
                {
                    // Add the category's button to the list
                    POIBtnList.Add(new ElmSharp.Button(win) { Text = category });
                }
            }
            else if (selectedProvider == "MAPZEN")
            {
                // Get the categories of the mapzen provider
                foreach (string category in MapzenCategory)
                {
                    // Add the category's button to the list
                    POIBtnList.Add(new ElmSharp.Button(win) { Text = category });
                }
            }

            for (int i = 0; i < POIBtnList.Count; i++)
            {
                // Set the default color for the category buttons
                POIBtnList[i].Color = ElmSharp.Color.White;
                // Move the category buttons
                POIBtnList[i].Move(50, 400 + (i * 80));
                // Resize the category buttons
                POIBtnList[i].Resize(250, 80);
                // Add the handler for the category buttons
                POIBtnList[i].Clicked += (sender, e) =>
                {
                    // Remove the used data
                    ClearData();
                    // Hide the button for the end position
                    toBtn.Hide();
                    // Request the pois with the center position
                    RequestPOI(new Geocoordinates(s_mapview.Center.Latitude, s_mapview.Center.Longitude), ((ElmSharp.Button)sender).Text);
                    // Set the viewpage
                    view = ViewPage.POI;
                };
            }

            // Add the handler for the map button
            MapBtn.Clicked += (sender, e) =>
            {
                // Remove the used data
                ClearData();
                // Hide the button for the end position
                toBtn.Hide();
                for (int i = 0; i < POIBtnList.Count; i++)
                {
                    // Hide the category buttons
                    POIBtnList[i].Hide();
                }
                // Set the viewpage
                view = ViewPage.MAP;
            };

            // Add the handler for the route button
            RouteBtn.Clicked += (sender, e) =>
            {
                // Remove the used data
                ClearData();
                // Show the button for the end position
                toBtn.Show();
                for (int i = 0; i < POIBtnList.Count; i++)
                {
                    // Hide the category buttons
                    POIBtnList[i].Hide();
                }
                // Set the viewpage
                view = ViewPage.ROUTE;
            };

            // Add the handler for the poi button
            PoiBtn.Clicked += (sender, e) =>
            {
                // Remove the used data
                ClearData();
                // Hide the button for the end position
                toBtn.Hide();
                for (int i = 0; i < POIBtnList.Count; i++)
                {
                    // Show the category buttons
                    POIBtnList[i].Show();
                }
            };

            // Create the list for the direction buttons
            DirectionBtnList = new List<ElmSharp.Button>();
            foreach (string direction in DirectionText)
            {
                // Create buttons and add it to the list
                DirectionBtnList.Add(new ElmSharp.Button(win)
                {
                    // Set the default text
                    Text = direction,
                    // Set the default color
                    Color = ElmSharp.Color.White,
                    // Set the default background color
                    BackgroundColor = ElmSharp.Color.Gray
                });
            }

            for (int i = 0; i < DirectionBtnList.Count; i++)
            {
                if (i % 2 == 0)
                {
                    // Move the direction buttons
                    DirectionBtnList[i].Move(380, 100 + 100 * (i / 2));
                }
                else
                {
                    // Move the direction buttons
                    DirectionBtnList[i].Move(470, 100 + 100 * (i / 2));
                }

                // Resize the direction buttons
                DirectionBtnList[i].Resize(80,80);
                // Show the direction buttons
                DirectionBtnList[i].Show();
                // Add the handler for the direction buttons
                DirectionBtnList[i].Clicked += (sender, e) =>
                {
                    ElmSharp.Button selectedBtn = ((ElmSharp.Button)sender);
                    // Check the clicked button
                    if (selectedBtn == DirectionBtnList[0])
                    {
                        // Zoom in
                        s_mapview.ZoomLevel = s_mapview.ZoomLevel + 1;
                    }
                    else if (selectedBtn == DirectionBtnList[1])
                    {
                        // Zoom out
                        s_mapview.ZoomLevel = s_mapview.ZoomLevel - 1;
                    }
                    else if (selectedBtn == DirectionBtnList[6])
                    {
                        // Check the viewpage
                        if (view == ViewPage.MAP)
                        {
                            // Remove all the map object from the map view
                            s_mapview.RemoveAll();
                            // Add the pin to the center positon of the map view
                            s_mapview.Add(new global::Tizen.Maps.Pin(s_mapview.Center));
                            // Request the address by the center position of the map view and display the address to the button of the starting position
                            RequestAddress(fromBtn, s_mapview.Center.Latitude, s_mapview.Center.Longitude);
                        }
                        else if (view == ViewPage.ROUTE)
                        {
                            // Check the text of the from button and the to button. The route is being displayed if the text of the buttons is not empty.
                            if (fromBtn.Text != "" && toBtn.Text != "")
                            {
                                // Remove all the map object from the map view
                                s_mapview.RemoveAll();
                                // Remove the text of the from button
                                fromBtn.Text = "";
                                // Remove the text of the to button
                                toBtn.Text = "";
                            }

                            // Check the text of the from button
                            if (fromBtn.Text == "")
                            {
                                // Add a marker to the center position
                                s_mapview.Add(new global::Tizen.Maps.Pin(s_mapview.Center));
                                // Request an address with the center position
                                RequestAddress(fromBtn, s_mapview.Center.Latitude, s_mapview.Center.Longitude);
                                // Create the Geocoordinates from the center position
                                fromPosition = new Geocoordinates(s_mapview.Center.Latitude, s_mapview.Center.Longitude);
                            }
                            else
                            {
                                // Add a marker to the center position
                                s_mapview.Add(new global::Tizen.Maps.Sticker(s_mapview.Center));
                                // Request an address with the center position
                                RequestAddress(toBtn, s_mapview.Center.Latitude, s_mapview.Center.Longitude);
                                // Create the Geocoordinates from the center position
                                toPosition = new Geocoordinates(s_mapview.Center.Latitude, s_mapview.Center.Longitude);
                                // Request a route with the fromPosition and the toPosition
                                RequestRoute(fromPosition, toPosition);
                            }
                        }
                        else if (view == ViewPage.POI)
                        {
                            // Increase the index of the current marker
                            CurrentMarker = CurrentMarker + 1;
                            // Check the index of the current marker
                            if (CurrentMarker >= PlaceList.Count)
                            {
                                // Set 0 to the index if the index of the current marker is greater than the maximum value
                                CurrentMarker = 0;
                            }

                            // Display the current marker
                            SetCurrentMarker(CurrentMarker);
                        }
                    }
                    else
                    {
                        double variable = 0.0;
                        // Check the current zoom level
                        if (s_mapview.ZoomLevel >= 15)
                        {
                            // Set the value to move as the current zoom level
                            variable = 0.01;
                        }
                        else if (s_mapview.ZoomLevel >= 10)
                        {
                            // Set the value to move as the current zoom level
                            variable = 0.1;
                        }
                        else
                        {
                            // Set the value to move as the current zoom level
                            variable = 1.0;
                        }

                        // Check the selected button to determine the direction to move
                        if (selectedBtn == DirectionBtnList[3] || selectedBtn == DirectionBtnList[4])
                        {
                            // Set the value for the reverse direction
                            variable = -variable;
                        }

                        // Check the selected button to determine the direction to move
                        if (selectedBtn == DirectionBtnList[2] || selectedBtn == DirectionBtnList[3])
                        {
                            // Set the center position after creating the Geocoordinates with the calculated value
                            s_mapview.Center = new Geocoordinates(s_mapview.Center.Latitude + variable, s_mapview.Center.Longitude);
                        }
                        else
                        {
                            // Set the center position after creating the Geocoordinates with the calculated value
                            s_mapview.Center = new Geocoordinates(s_mapview.Center.Latitude, s_mapview.Center.Longitude + variable);
                        }
                    }
                };
            }
        }

        /// <summary>
        /// Remove the used resource.
        /// </summary>
        public void ClearData()
        {
            // Remove the index of the current marker
            CurrentMarker = 0;
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
            // Clear the button's text
			if (fromBtn != null)
				fromBtn.Text = "";
			if (toBtn != null)
				toBtn.Text = "";
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
                tc.Command = new Command(() => { CreateMapService(tc.Text); });
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
        /// Remove the used resource and terminate this application.
        /// </summary>
        /// <param name="MainPage">Specifies the main page</param>
        public void CloseApp(Page MainPage)
        {
            // Remove the used resource
            ClearData();

            if (win != null)
            {
                // Unrealize the Window object
                win.Unrealize();
                win = null;
            }

            // Quit the main loop for terminating this application
            EcoreMainloop.Quit();
        }
    }
}
