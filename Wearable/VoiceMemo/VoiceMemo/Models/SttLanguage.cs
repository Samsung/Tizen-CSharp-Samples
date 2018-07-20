/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
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

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VoiceMemo.Models
{
    /// <summary>
    /// Class SttLanguage
    /// It presents language (locale) information for Speech-to-Text service
    /// </summary>
    public class SttLanguage : INotifyPropertyChanged
    {
        bool _isOn;
        string _lang, _combo, _name, _country;

        public SttLanguage(string lang, string name, string country)
        {
            Lang = lang;
            Name = name;
            Country = country;
            Combo = name + "----" + country;
            IsOn = false;
        }

        /// <summary>
        /// Language
        /// </summary>
        public string Lang
        {
            set { SetProperty(ref _lang, value, "Lang"); }
            get { return _lang; }
        }

        /// <summary>
        /// combination of the full localized culture name and the full name of the country/region in English
        /// It's used to figure out which language is selected for Speech-to-Text service 
        /// </summary>
        public string Combo
        {
            set { SetProperty(ref _combo, value, "Combo"); }
            get { return _combo; }
        }

        /// <summary>
        /// Name
        /// It's CultureInfo.DisplayName property and the full localized culture name
        /// </summary>
        public string Name
        {
            set { SetProperty(ref _name, value, "Name"); }
            get { return _name; }
        }

        /// <summary>
        /// Country name
        /// It's RegionInfo.EnglishName property and the full name of the country/region in English.
        /// </summary>
        public string Country
        {
            set
            {
                SetProperty(ref _country, value, "Country");
            }

            get { return _country; }
        }

        // Indicate that this language is set for Stt service.
        public bool IsOn
        {
            get { return _isOn; }
            set
            {
                SetProperty(ref _isOn, value, "IsOn");
            }
        }

        public override string ToString()
        {
            return "SttLanguage Name:" + Name + ", Country:" + Country + ", IsOn: " + IsOn;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value,
                                      [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Called to notify that a change of property happened
        /// </summary>
        /// <param name="propertyName">The name of the property that changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
