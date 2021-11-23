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

using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace NotificationManager.Controls.MenuItem
{
    /// <summary>
    /// MenuTextItem class.
    /// Provides logic for MenuTextItem.
    /// This class defines a list item which contains a label aligned to the center (title).
    /// </summary>
    public partial class MenuTextItem
    {
        #region properties

        /// <summary>
        /// Command bindable property.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MenuTextItem),
                propertyChanged: OnCommandPropertyChanged);

        /// <summary>
        /// Command parameter bindable property.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MenuTextItem),
                propertyChanged: OnCommandParameterPropertyChanged);

        /// <summary>
        /// Command property.
        /// Command executed after MenuTextItem is tapped.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Command parameter property.
        /// Command parameter used by 'Command' during execution.
        /// </summary>
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// Text property.
        /// Text displayed in MenuTextItem's TextLabel.
        /// </summary>
        public string Text
        {
            set => TextLabel.Text = value;
            get => TextLabel.Text;
        }

        /// <summary>
        /// Tapped property.
        /// Event handler executed after MenuTextItem is tapped.
        /// </summary>
        public new EventHandler Tapped
        {
            set => GestureRecognizer.Tapped += value;
        }

        #endregion

        #region methods

        /// <summary>
        /// MenuTextItem class constructor.
        /// </summary>
        public MenuTextItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets command for the tap gesture recognizer.
        /// </summary>
        /// <param name="command">Command to be set.</param>
        private void SetCommand(ICommand command)
        {
            GestureRecognizer.Command = command;
        }

        /// <summary>
        /// Sets command parameter for the tap gesture recognizer.
        /// </summary>
        /// <param name="commandParameter">Command parameter to be set.</param>
        private void SetCommandParameter(object commandParameter)
        {
            GestureRecognizer.CommandParameter = commandParameter;
        }

        /// <summary>
        /// Handles 'CommandProperty' property change event.
        /// </summary>
        /// <param name="bindable">The bindable object of type MenuTextItem.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void OnCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MenuTextItem button)
            {
                button.SetCommand(newValue as ICommand);
            }
        }

        /// <summary>
        /// Handles 'CommandParameterProperty' property change event.
        /// </summary>
        /// <param name="bindable">The bindable object of type MenuTextItem.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void OnCommandParameterPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MenuTextItem button)
            {
                button.SetCommandParameter(newValue);
            }
        }

        #endregion
    }
}