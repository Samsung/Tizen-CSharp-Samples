/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

using Xamarin.Forms;

namespace NotificationManager.Controls.MenuItem
{
    /// <summary>
    /// MenuTextStatusSwitchItem class.
    /// Provides logic for MenuTextStatusSwitchItem.
    /// This class defines a list item which contains a label at the top (title),
    /// a label at the bottom (status) and a switch in the right corner.
    /// </summary>
    public partial class MenuTextStatusSwitchItem
    {
        #region fields

        /// <summary>
        /// Describes the state when the switch is 'On'.
        /// </summary>
        private const string ON_MSG = "On";

        /// <summary>
        /// Describes the state when the switch is 'Off'.
        /// </summary>
        private const string OFF_MSG = "Off";

        #endregion

        #region properties

        /// <summary>
        /// Status on bindable property.
        /// </summary>
        public static readonly BindableProperty StatusOnProperty =
            BindableProperty.Create(nameof(StatusOn), typeof(bool), typeof(MenuTextStatusSwitchItem),
                default(bool), BindingMode.TwoWay, propertyChanged: OnStatusOnPropertyChanged);

        /// <summary>
        /// Status on property.
        /// Status of the MenuTextStatusSwitchItem which describes if MenuTextStatusSwitchItem
        /// is activated ('On') or deactivated ('Off').
        /// </summary>
        public bool StatusOn
        {
            set => SetValue(StatusOnProperty, value);
            get => (bool)GetValue(StatusOnProperty);
        }

        /// <summary>
        /// Text property.
        /// Text displayed in MenuTextStatusSwitchItem's TextLabel.
        /// </summary>
        public string Text
        {
            set => TextLabel.Text = value;
            get => TextLabel.Text;
        }

        #endregion

        #region methods

        /// <summary>
        /// MenuTextStatusSwitchItem class constructor.
        /// Initializes component, properties and registers an event handler
        /// to respond to switch's 'Toggled' event.
        /// </summary>
        public MenuTextStatusSwitchItem()
        {
            InitializeComponent();

            StatusLabel.Text = OFF_MSG;
            StatusOn = false;

            StatusSwitch.Toggled += (sender, e) =>
            {
                if (e.Value)
                {
                    StatusLabel.Text = ON_MSG;
                    StatusOn = true;
                }
                else
                {
                    StatusLabel.Text = OFF_MSG;
                    StatusOn = false;
                }
            };
        }

        /// <summary>
        /// Sets MenuTextStatusSwitchItem' state to 'On' or 'Off'.
        /// </summary>
        /// <param name="statusOn">True if the state should be 'On', false otherwise.</param>
        private void SetStatusOn(bool? statusOn)
        {
            StatusSwitch.IsToggled = statusOn ?? StatusSwitch.IsToggled;
        }

        /// <summary>
        /// Handles 'StatusOnProperty' property change event.
        /// </summary>
        /// <param name="bindable">The bindable object of type MenuTextStatusSwitchItem.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void OnStatusOnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MenuTextStatusSwitchItem button)
            {
                button.SetStatusOn(newValue as bool?);
            }
        }

        #endregion
    }
}