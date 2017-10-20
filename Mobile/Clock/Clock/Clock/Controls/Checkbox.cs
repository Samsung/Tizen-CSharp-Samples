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

using Xamarin.Forms;
using System;

namespace Clock.Controls
{
    /// <summary>
    /// The CheckBox class
    /// </summary>
    public class CheckBox : View
    {
        /// <summary>
        /// Backing store for the IsChecked bindable property.
        /// </summary>
        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create("IsChecked", typeof(bool), typeof(Switch), false, propertyChanged: (bindable, oldValue, newValue) =>
            {
                ((CheckBox)bindable).Checked?.Invoke(bindable, new CheckedEventArgs((bool)newValue));
            },
            defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// Backing store for the IsFavoriteStyle bindable property.
        /// </summary>
        public static readonly BindableProperty IsFavoriteStyleProperty = BindableProperty.Create("IsFavoriteStyle", typeof(bool), typeof(CheckBox), false);

        /// <summary>
        /// Gets or sets whether the CheckBox is checked.
        /// This is a bindable property.
        /// true if the CheckBox is checked; false if the CheckBox is unchecked;
        /// </summary>
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        /// <summary>
        /// Gets or sets whether the CheckBox is favorite style.
        /// This is a bindable property.
        /// true if the style of CheckBox is favorite; false if the style of CheckBox is not favorite;
        /// </summary>
        public bool IsFavoriteStyle
        {
            get { return (bool)GetValue(IsFavoriteStyleProperty); }
            set { SetValue(IsFavoriteStyleProperty, value); }
        }

        ///<summary>
        ///Initializes a new instance of the <see cref="CheckBox"/> class
        ///</summary>
        public CheckBox()
        {
        }

        ///<summary>
        ///Event that is raised when the Chcekbox is checked.
        ///</summary>
        public event EventHandler<CheckedEventArgs> Checked;
    }
}
