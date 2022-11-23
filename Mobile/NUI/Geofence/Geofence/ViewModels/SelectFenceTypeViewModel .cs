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
using System.Collections.ObjectModel;
using System.ComponentModel;
using Tizen.Location.Geofence;

namespace Geofence.ViewModels
{
    /// <summary>
    /// Class representing Select Fence Type Page View Model
    /// </summary>
    public class SelectFenceTypeViewModel
    {
        public ObservableCollection<FenceListIteam> Source { get; private set; } = new ObservableCollection<FenceListIteam>();

        public SelectFenceTypeViewModel()
        {
            Source.Add(new FenceListIteam(FenceType.GeoPoint));
            Source.Add(new FenceListIteam(FenceType.Wifi));
            Source.Add(new FenceListIteam(FenceType.Bluetooth));
        }
    }

    public class FenceListIteam : INotifyPropertyChanged
    {
        private FenceType type;
        public string Type
        {
            get => type.ToString();
        }

        public FenceListIteam(FenceType _type)
        {
            type = _type;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) 
        { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        }
    }
}
