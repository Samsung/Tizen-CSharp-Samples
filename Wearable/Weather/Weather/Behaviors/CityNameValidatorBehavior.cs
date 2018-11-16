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

using System;
using Weather.Models.Location;
using Xamarin.Forms;

namespace Weather.Behaviors
{
    /// <summary>
    /// Behavior class that validates user input in city entry.
    /// </summary>
    public class CityNameValidatorBehavior : Behavior<Entry>
    {
        #region properties

        /// <summary>
        /// Bindable property that allows to obtain city object.
        /// </summary>
        public static BindableProperty CityProperty =
            BindableProperty.Create(nameof(City), typeof(City), typeof(CityNameValidatorBehavior));

        /// <summary>
        /// Gets or sets selected city.
        /// </summary>
        public City City
        {
            get => (City)GetValue(CityProperty);
            set => SetValue(CityProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Called when behavior is attached to entry.
        /// </summary>
        /// <param name="bindable">Object to attach behavior.</param>
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += CityNameChanged;
        }

        /// <summary>
        /// Called when behavior is detached from entry.
        /// </summary>
        /// <param name="bindable">Object to detach behavior.</param>
        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= CityNameChanged;
        }

        /// <summary>
        /// Callback method invoked on entry text change.
        /// </summary>
        /// <param name="sender">Object that invoked event.</param>
        /// <param name="textChangedEventArgs">Event arguments.</param>
        private void CityNameChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (sender is Entry entry)
            {
                if (City != null && City.Name == entry.Text)
                {
                    entry.TextColor = Color.FromRgb(128, 128, 128);
                }
                else
                {
                    entry.TextColor = Color.Red;
                }
            }
        }

        #endregion
    }
}