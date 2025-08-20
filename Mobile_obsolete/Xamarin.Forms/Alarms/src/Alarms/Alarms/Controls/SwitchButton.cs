/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
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
using Xamarin.Forms;

namespace Alarms.Controls
{
    /// <summary>
    /// SwitchButton implementation of Button that allows button to be in pressed/unpressed state.
    /// </summary>
    public class SwitchButton : Button
    {
        #region properties

        private readonly Color NORMAL_BUTTON_BACKGROUND = Color.FromRgb(82, 199, 217);
        private readonly Color DIMMED_BUTTON_BACKGROUND = Color.FromRgb(57, 139, 151);

        /// <summary>
        /// Bindable property indicating if the button is currently in "Pressed" state.
        /// </summary>
        public static BindableProperty IsPressedProperty = BindableProperty.Create(nameof(IsPressed), typeof(bool),
            typeof(SwitchButton), default(bool), propertyChanged: OnPressedPropertyChanged);

        #endregion

        #region methods

        /// <summary>
        /// Overloaded OnPressedPropertyChanged method called on every property modification.
        /// </summary>
        /// <param name="target">Target object of property modification.</param>
        /// <param name="oldValue">Old object value.</param>
        /// <param name="newValue">New object value.</param>
        private static void OnPressedPropertyChanged(BindableObject target, object oldValue, object newValue)
        {
            ((SwitchButton)target).BackgroundColorUpdate();
        }

        /// <summary>
        /// Property that indicates current "Pressed" status.
        /// </summary>
        public bool IsPressed
        {
            get => (bool)GetValue(IsPressedProperty);
            set => SetValue(IsPressedProperty, value);
        }

        /// <summary>
        /// Updates button color according to current "Pressed" status.
        /// </summary>
        public void BackgroundColorUpdate()
        {
            this.BackgroundColor = IsPressed ? DIMMED_BUTTON_BACKGROUND : NORMAL_BUTTON_BACKGROUND;
        }

        /// <summary>
        /// Constructor. Registers "Clicked" event.
        /// </summary>
        public SwitchButton()
        {
            this.Clicked += OnClicked;
        }

        /// <summary>
        /// Clicked event maintenance. Toggles button "Pressed" status.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void OnClicked(object sender, EventArgs eventArgs)
        {
            IsPressed = !IsPressed;
        }

        #endregion
    }
}
