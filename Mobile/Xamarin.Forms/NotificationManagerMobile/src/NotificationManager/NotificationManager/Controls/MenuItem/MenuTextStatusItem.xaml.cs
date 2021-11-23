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

using System.Windows.Input;
using Xamarin.Forms;

namespace NotificationManager.Controls.MenuItem
{
    /// <summary>
    /// MenuTextStatusItem class.
    /// Provides logic for MenuTextStatusItem.
    /// This class defines a list item which contains a label at the top (title),
    /// and a label at the bottom (status).
    /// </summary>
    public partial class MenuTextStatusItem
    {
        #region fields

        /// <summary>
        /// Number of characters per line.
        /// </summary>
        private const int CHARS_PER_LINE = 40;

        /// <summary>
        /// Height of a single non-title line.
        /// </summary>
        private const int LINE_HEIGHT = 40;

        /// <summary>
        /// Height of the title line.
        /// </summary>
        private const int TITLE_HEIGHT = 100;

        #endregion

        #region properties

        /// <summary>
        /// Command bindable property.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MenuTextStatusItem), null,
                propertyChanged: OnCommandPropertyChanged);

        /// <summary>
        /// Command parameter bindable property.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MenuTextStatusItem), null,
                propertyChanged: OnCommandParameterPropertyChanged);

        /// <summary>
        /// Status bindable property.
        /// </summary>
        public static readonly BindableProperty StatusProperty =
            BindableProperty.Create(nameof(Status), typeof(string), typeof(MenuTextStatusItem), null,
                propertyChanged: OnStatusPropertyChanged);

        /// <summary>
        /// Command property.
        /// Command executed after MenuTextStatusItem is tapped.
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
        /// Text displayed in MenuTextStatusItem's TextLabel.
        /// </summary>
        public string Text
        {
            get => TextLabel.Text;
            set => TextLabel.Text = value;
        }

        /// <summary>
        /// Status property.
        /// Status of the MenuTextStatusItem which consists of additional information.
        /// </summary>
        public string Status
        {
            get => (string)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// MenuTextStatusItem class constructor.
        /// </summary>
        public MenuTextStatusItem()
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
        /// Sets MenuTextStatusItem status property.
        /// </summary>
        /// <param name="status">Text which is to be set in the status label.</param>
        private void SetStatus(string status)
        {
            StatusLabel.Text = status;

            int lines = status.Length / CHARS_PER_LINE;

            if (status.Length % CHARS_PER_LINE != 0)
            {
                ++lines;
            }

            Height = TITLE_HEIGHT + lines * LINE_HEIGHT;
        }

        /// <summary>
        /// Handles 'CommandProperty' property change event.
        /// </summary>
        /// <param name="bindable">The bindable object of type MenuTextStatusItem.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void OnCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MenuTextStatusItem button)
            {
                button.SetCommand(newValue as ICommand);
            }
        }

        /// <summary>
        /// Handles 'CommandParameterProperty' property change event.
        /// </summary>
        /// <param name="bindable">The bindable object of type MenuTextStatusItem.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void OnCommandParameterPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MenuTextStatusItem button)
            {
                button.SetCommandParameter(newValue);
            }
        }

        /// <summary>
        /// Handles 'StatusProperty' property change event.
        /// </summary>
        /// <param name="bindable">The bindable object of type MenuTextStatusItem.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void OnStatusPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MenuTextStatusItem button)
            {
                button.SetStatus(newValue as string);
            }
        }

        #endregion
    }
}