using System;
using System.Collections.Generic;
using Tizen.Location.Geofence;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using static Tizen.NUI.BaseComponents.TextField;

namespace Geofence
{
    public partial class InsertInfoPage : ContentPage
    {
        /// <summary>
        /// Place Name
        /// </summary>
        private string placeName = "";

        public InsertInfoPage(VirtualPerimeter perimeter, string title)
        {
            InitializeComponent();

            AppBar.Title = title;

            PalceNameTextFiled.TextChanged += PlaceName_TextChanged;
            CancelButton.Clicked += (o, e) => Navigator?.Pop();
            DoneButton.Clicked += (o, e) =>
            {
                if (!placeName.Equals(""))
                {
                    perimeter.AddPlaceName(placeName);
                    Navigator?.Pop();
                }
                else
                {
                    // Check wrong Input parameter
                    var button = new Button()
                    {
                        Text = "OK",
                    };
                    button.Clicked += (object s, ClickedEventArgs a) =>
                    {
                        Navigator?.Pop();
                    };
                    DialogPage.ShowAlertDialog("Error!", "Please Input Place Name Correctly", button);
                }
            };
        }

        /// <summary>
        /// Set user id received user input
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event data of ID text changed </param>
        private void PlaceName_TextChanged(object sender, TextChangedEventArgs e) => placeName = e.TextField.Text;
    }
}
