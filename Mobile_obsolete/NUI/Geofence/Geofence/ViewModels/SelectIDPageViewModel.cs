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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Tizen.Location.Geofence;

namespace Geofence.ViewModels
{
    public enum DataType 
    { 
        PLACE, 
        FENCE
    }

    /// <summary>
    /// Class representing Select ID Page View Model
    /// </summary>
    public class SelectIDPageViewModel
    {
        public ObservableCollection<ListIteam> Source { get; private set; } = new ObservableCollection<ListIteam>();

        public SelectIDPageViewModel(VirtualPerimeter perimeter, DataType type)
        {
            switch (type)
            {
                case DataType.FENCE:
                    // Get the information for all the fences
                    foreach (var data in perimeter.GetFenceDataList())
                    {
                        // Add the geofence id to the list
                        Source.Add(new ListIteam(data.GeofenceId.ToString() + " " + data.Fence));
                    }

                    break;
                case DataType.PLACE:
                    // Get the information for all the places
                    foreach (var data in perimeter.GetPlaceDataList())
                    {
                        // Add the place id to the list
                        Source.Add(new ListIteam(data.PlaceId.ToString() + " " + data.Name));
                    }

                    break;
            }
        }
    }

    /// <summary>
    /// Class representing Iteam of Select ID Page Lists
    /// </summary>
    public class ListIteam : INotifyPropertyChanged
    {
        private string item;
        public string Item
        {
            get => item;
            set
            {
                if (item != value)
                {
                    item = value;
                    OnPropertyChanged("Item");
                }
            }
        }

        public ListIteam(string item)
        {
            Item = item;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) 
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        }
    }
}
