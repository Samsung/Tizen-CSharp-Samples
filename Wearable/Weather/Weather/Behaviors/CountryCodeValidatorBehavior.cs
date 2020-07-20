//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Models.Location;
using Weather.Service;
using Weather.Utils;
using Xamarin.Forms;

namespace Weather.Behaviors
{
    /// <summary>
    /// Behavior class that validates user input in country entry.
    /// </summary>
    public class CountryCodeValidatorBehavior : Behavior<Entry>
    {
        #region fields

        /// <summary>
        /// Task that creates and initializes a CountryProvider.
        /// </summary>
        private readonly Task<CountryProvider> _countryProviderTask;

        #endregion

        #region properties

        /// <summary>
        /// Maximum length of user input.
        /// </summary>
        public int MaxLength { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public CountryCodeValidatorBehavior()
        {
            _countryProviderTask = LoadCountryList();
        }

        /// <summary>
        /// Called when behavior is attached to entry.
        /// </summary>
        /// <param name="bindable">Object to attach behavior.</param>
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += EntryTextChanged;
        }

        /// <summary>
        /// Called when behavior is detached from entry.
        /// </summary>
        /// <param name="bindable">Object to detach behavior.</param>
        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= EntryTextChanged;
        }

        /// <summary>
        /// Callback method, invoked when entry text is changed.
        /// Changes user input to uppercase and forbids to enter more than "MaxLength" number of characters.
        /// </summary>
        /// <param name="sender">Object that invoked event.</param>
        /// <param name="textChangedEventArgs">Event arguments.</param>
        private async void EntryTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (!(sender is Entry entry))
            {
                return;
            }

            entry.Text = entry.Text.Length > MaxLength
                ? textChangedEventArgs.OldTextValue.ToUpper()
                : textChangedEventArgs.NewTextValue.ToUpper();

            var provider = await _countryProviderTask;
            entry.TextColor = provider.Validate(entry.Text) ? Color.Gray : Color.Red;
        }

        /// <summary>
        /// Loads supported city list from file.
        /// </summary>
        private Task<CountryProvider> LoadCountryList()
        {
            return Task.Run(() =>
            {
                var jsonFileReader = new JsonFileReader<IList<Country>>("Weather.Data.", "country.list.json");
                jsonFileReader.Read();
                return new CountryProvider(jsonFileReader.Result.AsQueryable());
            });
        }

        #endregion
    }
}