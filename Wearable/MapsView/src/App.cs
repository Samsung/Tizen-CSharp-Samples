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

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Tizen.Wearable.CircularUI.Forms;

namespace MapsView
{
    public class App : Application
    {
        public App()
        {
            PositionToStringValueConverter positionConverter = new PositionToStringValueConverter();

            // This app main page is a Circle Page
            var page = new CirclePage();

            // With a Circle List View as the only component
            var circleListView = new CircleListView
            {
                // Items on this list view are coming from a predefined pins list (check Pins.cs)
                ItemsSource = Pins.Predefined,
                // Each item on the list has two visual elements:
                ItemTemplate = new DataTemplate(() =>
                {                   
                    var cell = new TextCell();
                    // a Label (e.g. Rome)
                    cell.SetBinding(TextCell.TextProperty, "Label");
                    // and its coordinates.                    
                    cell.SetBinding(TextCell.DetailProperty, "Position", converter: positionConverter);
                    return cell;
                }),
            };
          
            circleListView.ItemTapped += CircleListView_ItemTapped;
            page.Content = circleListView;            
            page.RotaryFocusObject = circleListView;
            NavigationPage.SetHasNavigationBar(page, false);           
            MainPage = new NavigationPage();           
            MainPage.Navigation.PushAsync(page, false);
        }

        /// <summary>
        /// Handler for circleListView ItemTapped event
        /// It creates a new MapPage, prepares a MapSpan for MapPage to start with and navigates to it
        /// </summary>
        private void CircleListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Pin pin = (Pin)((CircleListView)sender).SelectedItem;
            MapSpan startingRegion = MapSpan.FromCenterAndRadius(pin.Position, Distance.FromKilometers(Config.STARTING_ZOOM_LEVEL));
            MapPage mapPage = new MapPage();
            mapPage.BindingContext = startingRegion;
            MainPage.Navigation.PushAsync(mapPage, false);               
        }

    }
}
