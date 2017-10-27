/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Clock.Data;
using Clock.Interfaces;
using Clock.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tizen.Xamarin.Forms.Extension;
using Xamarin.Forms;

namespace Clock.Worldclock
{
    public class WorldclockInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private WorldclockPage page;
        public ObservableCollection<CityRecord> CityRecordList { get; }
        public LocationObservableCollection userLocations;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_page">WorldclockPage</param>
        public WorldclockInfo(WorldclockPage _page)
        {
            CityRecordList = new ObservableCollection<CityRecord>();
            userLocations = new LocationObservableCollection();
            page = _page;
            LoadItemsList();
            TimezoneUtility.UpdateLocalTime();
        }

        /// <summary>
        /// Notify that a property value has changed
        /// </summary>
        /// <param name="propertyName">string</param>
        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            SaveItemsList();
            DependencyService.Get<IPreference>().SetInt("DOTNET_WORLDCLOCK_MAP_CURRENT_TIMEZONE", TimezoneUtility.GetCurrentTimezoneNo());
        }

        private bool Exist(Location l)
        {
            if (userLocations.Count != 0)
            {
                foreach (Location item in userLocations)
                {
                    if ((l.Name == item.Name) && (l.Country == item.Country))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Called after a city is selected
        /// The information about the city is added to ListView.
        /// </summary>
        /// <param name="l">Location</param>
        public void OnItemAdded(Location l)
        {
            // check if the city is already added to listView or not
            if (Exist(l))
            {
                Toast.DisplayText(l.Name + " already exists.");
                return;
            }

            // Add location information to userLocations
            userLocations.Add(l);
            Timezone tz = TimezoneUtility.GetTimezoneByOffset(l.GmtOffset);
            TimezoneUtility.SetCurrentTimezone(tz);
            page.OnMapViewUpdateRequest();
            AppendItemToCustomList(l);
        }

        /// <summary>
        /// Location information is added to ListView by adding CityRecord to ListView's ItemSources
        /// </summary>
        /// <param name="item">Location</param>
        public void AppendItemToCustomList(Location item)
        {
            CityRecord cityRecord = CityRecordUtility.GenerateCityRecord(item.GmtOffset);
            cityRecord.Cities = item.Name + ", " + item.Country;
            CityRecordList.Add(cityRecord);
        }

        /// <summary>
        /// Save information about the added cities before the application is terminated
        /// </summary>
        private void SaveItemsList()
        {
            DependencyService.Get<IPreference>().Save(userLocations);
        }

        /// <summary>
        /// Restore all cities the user adds to userLocations(LocationObservableCollection)
        /// </summary>
        private void LoadItemsList()
        {
            DependencyService.Get<IPreference>().Load(ref userLocations);
        }
    }
}
