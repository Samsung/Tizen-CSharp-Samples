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
    /// MenuTextStatusImageItem class.
    /// Provides logic for MenuTextStatusImageItem.
    /// This class defines a list item which contains a label at the top (title),
    /// a label at the bottom (status) and an image in the right corner.
    /// </summary>
    public partial class MenuTextStatusImageItem
    {
        #region properties

        /// <summary>
        /// Command bindable property.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MenuTextStatusImageItem),
                propertyChanged: OnCommandPropertyChanged);

        /// <summary>
        /// Command parameter bindable property.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MenuTextStatusImageItem),
                propertyChanged: OnCommandParameterPropertyChanged);

        /// <summary>
        /// Image path bindable property.
        /// </summary>
        public static readonly BindableProperty ImagePathProperty =
            BindableProperty.Create(nameof(ImagePath), typeof(string), typeof(MenuTextStatusImageItem), default(string),
                propertyChanged: OnImagePathPropertyChanged);

        /// <summary>
        /// Image color bindable property.
        /// </summary>
        public static readonly BindableProperty ImageColorProperty =
            BindableProperty.Create(nameof(ImageColor), typeof(Color), typeof(MenuTextStatusImageItem), default(Color),
                propertyChanged: OnImageColorPropertyChanged);

        /// <summary>
        /// Status bindable property.
        /// </summary>
        public static readonly BindableProperty StatusProperty =
            BindableProperty.Create(nameof(Status), typeof(string), typeof(MenuTextStatusImageItem), default(string),
                propertyChanged: OnStatusPropertyChanged);

        /// <summary>
        /// Command property.
        /// Command executed after MenuTextStatusImageItem is tapped.
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
        /// Image path property.
        /// Path to the image displayed in MenuTextStatusImageItem's Image.
        /// </summary>
        public string ImagePath
        {
            get => (string)GetValue(ImagePathProperty);
            set => SetValue(ImagePathProperty, value);
        }

        /// <summary>
        /// Image color property.
        /// Color of the image displayed in MenuTextStatusImageItem's Image.
        /// </summary>
        public Color ImageColor
        {
            get => (Color)GetValue(ImageColorProperty);
            set => SetValue(ImageColorProperty, value);
        }

        /// <summary>
        /// Text property.
        /// Text displayed in MenuTextStatusImageItem's TextLabel.
        /// </summary>
        public string Text
        {
            get => TextLabel.Text;
            set => TextLabel.Text = value;
        }

        /// <summary>
        /// Status property.
        /// Status of the MenuTextStatusImageItem which consists of additional information.
        /// </summary>
        public string Status
        {
            get => (string)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// MenuTextStatusImageItem class constructor.
        /// </summary>
        public MenuTextStatusImageItem()
        {
            InitializeComponent();

            ImageItem.BackgroundColor = ImageColor;
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
        /// Sets the image path.
        /// </summary>
        /// <param name="imagePath">Image's path.</param>
        private void SetImagePath(string imagePath)
        {
            ImageItem.Source = imagePath;
        }

        /// <summary>
        /// Handles 'CommandProperty' property change event.
        /// </summary>
        /// <param name="bindable">The bindable object of type MenuTextStatusImageItem.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void OnCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MenuTextStatusImageItem button)
            {
                button.SetCommand(newValue as ICommand);
            }
        }

        /// <summary>
        /// Handles 'CommandParameterProperty' property change event.
        /// </summary>
        /// <param name="bindable">The bindable object of type MenuTextStatusImageItem.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void OnCommandParameterPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MenuTextStatusImageItem button)
            {
                button.SetCommandParameter(newValue);
            }
        }

        /// <summary>
        /// Sets Image's BackgroundColor property.
        /// </summary>
        /// <param name="imageColor">Color of the image which is to be set.</param>
        private void SetImageColor(Color imageColor)
        {
            ImageItem.BackgroundColor = imageColor;
        }

        /// <summary>
        /// Sets status.
        /// </summary>
        /// <param name="status">Text which is to be set in the status label.</param>
        private void SetStatus(string status)
        {
            StatusLabel.Text = status;
        }

        /// <summary>
        /// Handles 'ImagePathProperty' property change event.
        /// </summary>
        /// <param name="bindable">The bindable object of type MenuTextStatusImageItem.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void OnImagePathPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MenuTextStatusImageItem button)
            {
                button.SetImagePath(newValue as string);
            }
        }

        /// <summary>
        /// Handles 'ImageColorProperty' property change event.
        /// </summary>
        /// <param name="bindable">The bindable object of type MenuTextStatusImageItem.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void OnImageColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MenuTextStatusImageItem button)
            {
                button.SetImageColor((Color)newValue);
            }
        }

        /// <summary>
        /// Handles 'StatusProperty' property change event.
        /// </summary>
        /// <param name="bindable">The bindable object of type MenuTextStatusImageItem.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void OnStatusPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MenuTextStatusImageItem button)
            {
                button.SetStatus(newValue as string);
            }
        }

        #endregion
    }
}