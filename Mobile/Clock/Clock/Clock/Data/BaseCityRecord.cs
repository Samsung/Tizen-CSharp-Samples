/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.ComponentModel;

namespace Clock.Data
{
    /// <summary>
    /// BaseCityRecord class
    /// It's a base class for CityRecord
    /// And it's used for data that represents information about the current GMT offset related timezone on the world clock map
    /// </summary>
    public class BaseCityRecord : INotifyPropertyChanged
    {
        // Offset
        public int Offset { get; set; }
        // Time
        private string _time;
        public string CityTime
        {
            get { return _time; }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    NotifyPropertyChanged("CityTime");
                }
            }
        }
        // AM or PM
        private string _ampm;
        public string CityAmPm
        {
            get { return _ampm; }
            set
            {
                if (_ampm != value)
                {
                    _ampm = value;
                    NotifyPropertyChanged("CityAmPm");
                }
            }
        }
        // Relative time difference between city and the current local city
        private string _timediff;
        public string RelativeToLocalCountry
        {
            get { return _timediff; }
            set
            {
                if (_timediff != value)
                {
                    _timediff = value;
                    NotifyPropertyChanged("RelativeToLocalCountry");

                }
            }
        }
        // Principal cities
        private string _places;
        public string Cities
        {
            get { return _places; }
            set
            {
                if (_places != value)
                {
                    _places = value;
                    NotifyPropertyChanged("Cities");
                }
            }
        }

        ///<summary>
        ///Event that is raised when the properties of CityRecord change
        ///</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify that a property value has changed
        /// </summary>
        /// <param name="info">string</param>
        protected virtual void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseCityRecord()
        {
        }

        /// <summary>
        /// Return a string that represents the current BaseCityRecord object.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return "GMT-" + Offset + " " + Cities + " - " + CityTime + " " + CityAmPm + " " + RelativeToLocalCountry;
        }
    }
}
