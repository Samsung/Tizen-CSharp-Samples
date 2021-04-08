/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
using System.Windows.Input;
using Xamarin.Forms;

namespace QRCodeGenerator.Views
{
    /// <summary>
    /// Settings item class.
    /// Used as one item for list item source.
    /// </summary>
    public class SettingsItem : BindableObject
    {
        #region properties

        /// <summary>
        /// Value property definition.
        /// It defines bindable property which value contains current value of settings item.
        /// </summary>
        public static readonly BindableProperty TitleValueProperty =
            BindableProperty.Create("TitleValue", typeof(string), typeof(SettingsItem), default(string));

        /// <summary>
        /// Command property definition.
        /// It defines bindable property which value contains main item's action command.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(SettingsItem), default(ICommand));

        /// <summary>
        /// Disabled property definition.
        /// It defines bindable property which value determines if settings item is disabled.
        /// </summary>
        public static readonly BindableProperty DisabledProperty =
            BindableProperty.Create("Disabled", typeof(bool), typeof(SettingsItem), false);

        /// <summary>
        /// Item title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Current value of the settings item.
        /// </summary>
        public string TitleValue
        {
            get => (string)GetValue(TitleValueProperty);
            set => SetValue(TitleValueProperty, value);
        }

        /// <summary>
        /// Item command.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Current state of settings item Disabled property.
        /// </summary>
        public bool Disabled
        {
            get => (bool)GetValue(DisabledProperty);
            set => SetValue(DisabledProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Override of OnPropertyChanged method.
        /// Checks command CanExecute value and sets Disabled state.
        /// </summary>
        /// <param name="propertyName">Name of changed property.</param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == CommandProperty.PropertyName)
            {
                if (Command != null)
                {
                    Disabled = !Command.CanExecute(null);
                    Command.CanExecuteChanged += CommandOnCanExecuteChanged;
                }
                else
                {
                    Disabled = false;
                }
            }
        }

        /// <summary>
        /// Handles command CanExecuteChanged event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void CommandOnCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            Disabled = !Command.CanExecute(null);
        }

        #endregion
    }
}