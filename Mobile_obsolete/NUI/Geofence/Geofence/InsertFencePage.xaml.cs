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
using System;
using Tizen.Location.Geofence;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using static Tizen.NUI.BaseComponents.TextField;

namespace Geofence
{
    /// <summary>
    /// Class representing Insert Fence Page
    /// </summary>
    public partial class InsertFencePage : ContentPage
    {
        /// <summary>
        /// Latitude
        /// </summary>
        private string latitude = "";

        /// <summary>
        /// Longitude
        /// </summary>
        private string longitude = "";

        /// <summary>
        /// VirtualPerimeter object.
        /// </summary>
        private VirtualPerimeter perimeter = null;

        public InsertFencePage(string title, int placeID, FenceType fenceType)
        {
            InitializeComponent();

            if (Program.Perimeter != null)
            {
                perimeter = Program.Perimeter;
            }

            AppBar.Title = title;

            LongitudeTextFiled.TextChanged += Longitude_TextChanged;
            LatitudeTextFiled.TextChanged += Latitude_TextChanged;

            if (fenceType == FenceType.GeoPoint)
            {
                LatitudeTextFiled.PlaceholderText = "Latitude";
            }
            else
            {
                LatitudeTextFiled.PlaceholderText = "BSSID";
            }

            //PalceNameTextFiled.TextChanged += PlaceName_TextChanged;
            CancelButton.Clicked += (o, e) => Navigator?.Pop();
            DoneButton.Clicked += (o, e) =>
            {
                if (!longitude.Equals("") && !latitude.Equals(""))
                {
                    Fence fence = null;
                    switch (fenceType)
                    {
                        case FenceType.GeoPoint:
                            // Check the value of the second entry
                            if (string.IsNullOrEmpty(longitude))
                            {
                                // Throw an argument exception with message
                                throw new ArgumentException("Content cannot be null or empty");
                            }

                            // Create a gps fence with inserted information
                            fence = Fence.CreateGPSFence(placeID, double.Parse(latitude), double.Parse(latitude), 100, "TestAddress");
                            break;
                        case FenceType.Wifi:
                            // Create a wifi fence with inserted information
                            fence = Fence.CreateWifiFence(placeID, latitude, "TestAddress");
                            break;
                        case FenceType.Bluetooth:
                            // Create a bt fence with inserted information
                            fence = Fence.CreateBTFence(placeID, latitude, "TestAddress");
                            break;
                        default:
                            break;
                    }

                    if (fence != null)
                    {
                        // Add the fence
                        perimeter.AddGeofence(fence);
                    }

                    Navigator?.RemoveAt(Navigator.PageCount - 2);
                    Navigator?.RemoveAt(Navigator.PageCount - 2);
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
                    DialogPage.ShowAlertDialog("Error!", "Please Input Latitude And Longitude Correctly", button);
                }
            };
        }

        /// <summary>
        /// Set Latitude received user input
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event data of ID text changed </param>
        private void Latitude_TextChanged(object sender, TextChangedEventArgs e) => latitude = e.TextField.Text;

        /// <summary>
        /// Set Longitude received user input
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event data of ID text changed </param>
        private void Longitude_TextChanged(object sender, TextChangedEventArgs e) => longitude = e.TextField.Text;

        protected override void Dispose(DisposeTypes type)
        {
            if (Disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                RemoveAllChildren(true);
            }

            base.Dispose(type);
        }

        private void RemoveAllChildren(bool dispose = false)
        {
            RecursiveRemoveChildren(this, dispose);
        }

        private void RecursiveRemoveChildren(View parent, bool dispose)
        {
            if (parent == null)
            {
                return;
            }

            int maxChild = (int)parent.ChildCount;
            for (int i = maxChild - 1; i >= 0; --i)
            {
                View child = parent.GetChildAt((uint)i);
                if (child == null)
                {
                    continue;
                }

                RecursiveRemoveChildren(child, dispose);
                parent.Remove(child);
                if (dispose)
                {
                    child.Dispose();
                }
            }
        }
    }
}
