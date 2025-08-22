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

using Xamarin.Forms;

namespace Weather.Utils
{
    /// <summary>
    /// Helper class that allows to bind string value.
    /// </summary>
    public class BindableString : BindableObject
    {
        #region properties

        /// <summary>
        /// Bindable property that allows to set value of string.
        /// </summary>
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(string), typeof(BindableString), string.Empty);

        /// <summary>
        /// Gets or sets value of string.
        /// </summary>
        public string Value
        {
            get => GetValue(ValueProperty).ToString();
            set => SetValue(ValueProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Returns string value instead of class string representation.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            return Value;
        }

        #endregion
    }
}