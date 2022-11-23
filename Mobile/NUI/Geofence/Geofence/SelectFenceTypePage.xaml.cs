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
    /// Class representing Select Fence Type Page
    /// </summary>
    public partial class SelectFenceTypePage : ContentPage
    {
        /// <summary>
        /// VirtualPerimeter object.
        /// </summary>
        private int placeID = 0;

        public SelectFenceTypePage(string title, int _placeID)
        {
            InitializeComponent();

            BindingContext = new SelectFenceTypeViewModel();

            AppBar.Title = title;

            placeID = _placeID;

            ColView.ItemTemplate = new Tizen.NUI.Binding.DataTemplate(() =>
            {
                var item = new RecyclerViewItem()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                };

                item.Clicked += OnClicked;

                var label = new TextLabel()
                {
                    WidthSpecification = LayoutParamPolicies.MatchParent,
                };
                label.SetBinding(TextLabel.TextProperty, "Type");
                item.Add(label);
                return item;
            });
            
        }

        /// <summary>
        /// Event for selecting item from list
        /// </summary>
        /// <param name="sender"> Parameter about which object is invoked the current event. </param>
        /// <param name="e"> Event arguments</param>
        private void OnClicked(object sender, ClickedEventArgs e)
        {
            FenceType fenceType = FenceType.GeoPoint;
            switch (((sender as RecyclerViewItem).Children.First() as TextLabel).Text)
            {
                case "GeoPoint":
                    fenceType = FenceType.GeoPoint;
                    break;
                case "Wifi":
                    fenceType = FenceType.Wifi;
                    break;
                case "Bluetooth":
                    fenceType = FenceType.Bluetooth;
                    break;
            }

            Navigator?.PushWithTransition(new InsertFencePage(AppBar.Title, placeID, fenceType));
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
