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
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using static Tizen.NUI.BaseComponents.TextField;

namespace Geofence
{
    /// <summary>
    /// Class representing Inert Info Page
    /// </summary>
    public partial class InsertInfoPage : ContentPage
    {
        /// <summary>
        /// Place Name
        /// </summary>
        private string placeName = "";

        public InsertInfoPage(string title, int placeID)
        {
            InitializeComponent();

            AppBar.Title = title;

            PalceNameTextFiled.TextChanged += PlaceName_TextChanged;
            CancelButton.Clicked += (o, e) => Navigator?.Pop();
            DoneButton.Clicked += (o, e) =>
            {
                if (!placeName.Equals(""))
                {
                    if (placeID == -1)
                    {
                        Program.Perimeter.AddPlaceName(placeName);
                    }
                    else
                    {
                        Program.Perimeter.UpdatePlace(placeID, placeName);
                        Navigator?.RemoveAt(Navigator.PageCount - 2);
                    }

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
