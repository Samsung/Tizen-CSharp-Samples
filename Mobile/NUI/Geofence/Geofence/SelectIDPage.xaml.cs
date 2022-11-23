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
using Tizen.Location.Geofence;
using Tizen.NUI;
using Tizen.NUI.Binding;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Geofence.ViewModels;
using static Tizen.NUI.BaseComponents.TextField;
using System.Linq;

namespace Geofence
{
    /// <summary>
    /// Class representing Select ID Page
    /// </summary>
    public partial class SelectIDPage : ContentPage
    {
        /// <summary>
        /// VirtualPerimeter object.
        /// </summary>
        private VirtualPerimeter perimeter = null;

        MainPage mainPage;

        System.EventHandler<ClickedEventArgs> onClicked = null;

        public SelectIDPage(string title, SelectIDPageViewModel list, MainPage main, Button button)
        {
            InitializeComponent();
            if (Program.Perimeter != null)
            {
                perimeter = Program.Perimeter;
            }

            BindingContext = list;

            mainPage = main;

            AppBar.Title = title;

            OnClicked(button);

            ColView.ItemTemplate = new Tizen.NUI.Binding.DataTemplate(() =>
            {
                var item = new RecyclerViewItem()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                };

                item.Clicked += onClicked;

                var label = new TextLabel()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                };
                label.SetBinding(TextLabel.TextProperty, "Item");
                item.Add(label);
                return item;
            });
            
        }

        /// <summary>
        /// Event for selecting item from list
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClickedRemovePlace(object sender, ClickedEventArgs e)
        {
            if (perimeter != null)
            {
                perimeter.RemovePlace(int.Parse(((sender as RecyclerViewItem).Children.First() as TextLabel).Text.Split(' ').First()));
            }

            Navigator?.RemoveAt(Navigator.PageCount - 1);
        }

        /// <summary>
        /// Event for selecting item from list
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClickedRemoveFence(object sender, ClickedEventArgs e)
        {
            if (perimeter != null)
            {
                perimeter.RemoveGeofence(int.Parse(((sender as RecyclerViewItem).Children.First() as TextLabel).Text.Split(' ').First()));
            }

            Navigator?.RemoveAt(Navigator.PageCount - 1);
        }

        /// <summary>
        /// Event for selecting item from list
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClickedFenceStatus(object sender, ClickedEventArgs e)
        {
            int argument = int.Parse(((sender as RecyclerViewItem).Children.First() as TextLabel).Text.Split(' ').First());
            // Create a fence status for sthe elected fence
            FenceStatus status = new FenceStatus(argument);

            // Set the value to label about the fence status
            mainPage.GetInfoLabelList[2].Text = "Fence ID: " + argument + ", GeofenceState: ";
            switch (status.State)
            {
                case GeofenceState.In:
                    mainPage.GetInfoLabelList[2].Text += "In";
                    break;
                case GeofenceState.Out:
                    mainPage.GetInfoLabelList[2].Text += "Out";
                    break;
                default:
                    mainPage.GetInfoLabelList[2].Text += "Uncertain";
                    break;
            }

            // Add the duration to the label
            mainPage.GetInfoLabelList[2].Text += ", Duration: " + status.Duration.ToString();
            Navigator?.RemoveAt(Navigator.PageCount - 1);
        }

        /// <summary>
        /// Event for selecting item from list
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClickedUpdatePlace(object sender, ClickedEventArgs e)
        {
            int placeID = int.Parse(((sender as RecyclerViewItem).Children.First() as TextLabel).Text.Split(' ').First());
            Navigator?.PushWithTransition(new InsertInfoPage(AppBar.Title, placeID));
        }

        /// <summary>
        /// Event for selecting item from list
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClickedCreateFence(object sender, ClickedEventArgs e)
        {
            int placeID = int.Parse(((sender as RecyclerViewItem).Children.First() as TextLabel).Text.Split(' ').First());
            Navigator?.PushWithTransition(new SelectFenceTypePage(AppBar.Title, placeID));
        }

        /// <summary>
        /// Event for selecting item from list
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClickedStartFence(object sender, ClickedEventArgs e)
        {
            // Start a selected fence
            MainPage.FenceID = int.Parse(((sender as RecyclerViewItem).Children.First() as TextLabel).Text.Split(' ').First());
            Program.Geofence.Start(MainPage.FenceID); 
            mainPage.StopButton.IsEnabled = true;
            Navigator?.Pop();
        }

        private void OnClicked(Button button)
        {
            if (mainPage.GetButtonList[1].Equals(button))
            {
                onClicked = OnClickedRemovePlace;
            }
            else if (mainPage.GetButtonList[2].Equals(button))
            {
                onClicked = OnClickedCreateFence;
            }
            else if (mainPage.GetButtonList[3].Equals(button))
            {
                onClicked = OnClickedRemoveFence;
            }
            else if (mainPage.GetButtonList[4].Equals(button))
            {
                onClicked = OnClickedStartFence;
            }
            else if (mainPage.GetButtonList[6].Equals(button))
            {
                onClicked = OnClickedUpdatePlace;
            }
            else if (mainPage.GetButtonList[7].Equals(button))
            {
                onClicked = OnClickedFenceStatus;
            }
        }

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
