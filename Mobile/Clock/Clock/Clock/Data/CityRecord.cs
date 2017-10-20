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

namespace Clock.Data
{
    /// <summary>
    /// CityRecord class
    /// </summary>
    public class CityRecord : BaseCityRecord
    {
        // Date
        private string _date;
        public string CityDate
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    NotifyPropertyChanged("CityDate");
                }
            }
        }

        // to be deleted or not
        private bool _delete;
        public bool Delete
        {
            get { return _delete; }
            set
            {
                if (_delete != value)
                {
                    _delete = value;
                    NotifyPropertyChanged("Delete");
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CityRecord() : base()
        {
        }

        /// <summary>
        /// Notify that a property value has changed
        /// </summary>
        /// <param name="info">string</param>
        protected override void NotifyPropertyChanged(string info)
        {
            base.NotifyPropertyChanged(info);
        }

        /// <summary>
        /// Return a string that represents the current CityRecord object.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return Cities + " - " + CityTime + " " + CityAmPm + " " + CityDate;
        }
    }
}
