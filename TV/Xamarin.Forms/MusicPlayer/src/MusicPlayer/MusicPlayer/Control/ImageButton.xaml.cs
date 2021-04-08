/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
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
using Xamarin.Forms.Xaml;

namespace MusicPlayer.Control
{
    /// <summary>
    /// Button which allows to set images for its states.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageButton : ContentView
    {
        #region fields

        /// <summary>
        /// Duration for pressed state of button in milliseconds.
        /// </summary>
        private const int PRESSED_TIME = 200;

        /// <summary>
        /// Gives information if button is focused.
        /// </summary>
        private bool _isFocused;

        /// <summary>
        /// Path of currently displayed image.
        /// </summary>
        private string _displayedImage;

        #endregion

        #region properties

        /// <summary>
        /// Gets the currently displayed image.
        /// </summary>
        public string DisplayedImage
        {
            get => _displayedImage;
            private set
            {
                _displayedImage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Allows to set the image of default button.
        /// </summary>
        public static readonly BindableProperty DefaultImageProperty = BindableProperty.Create(
                                                nameof(DefaultImage),
                                                typeof(string),
                                                typeof(ImageButton),
                                                null,
                                                propertyChanged: OnDefaultImageChanged);

        /// <summary>
        /// Gets or sets the default image.
        /// </summary>
        public string DefaultImage
        {
            get => (string)GetValue(DefaultImageProperty);
            set => SetValue(DefaultImageProperty, value);
        }

        /// <summary>
        /// Allows to set the image of second state default button.
        /// </summary>
        public static readonly BindableProperty SecondStateDefaultImageProperty = BindableProperty.Create(
                                                nameof(SecondStateDefaultImage),
                                                typeof(string),
                                                typeof(ImageButton),
                                                null);

        /// <summary>
        /// Gets or sets the second state default image.
        /// </summary>
        public string SecondStateDefaultImage
        {
            get => (string)GetValue(SecondStateDefaultImageProperty);
            set => SetValue(SecondStateDefaultImageProperty, value);
        }

        /// <summary>
        /// Allows to set the image of focused button.
        /// </summary>
        public static readonly BindableProperty FocusedImageProperty = BindableProperty.Create(
                                                nameof(FocusedImage),
                                                typeof(string),
                                                typeof(ImageButton),
                                                null);

        /// <summary>
        /// Gets or sets the focused state image.
        /// </summary>
        public string FocusedImage
        {
            get => (string)GetValue(FocusedImageProperty);
            set => SetValue(FocusedImageProperty, value);
        }

        /// <summary>
        /// Allows to set the image of second state focused button.
        /// </summary>
        public static readonly BindableProperty SecondStateFocusedImageProperty = BindableProperty.Create(
                                                nameof(SecondStateFocusedImage),
                                                typeof(string),
                                                typeof(ImageButton),
                                                null);

        /// <summary>
        /// Gets or sets the second state focused image.
        /// </summary>
        public string SecondStateFocusedImage
        {
            get => (string)GetValue(SecondStateFocusedImageProperty);
            set => SetValue(SecondStateFocusedImageProperty, value);
        }

        /// <summary>
        /// Allows to set the image of pressed button.
        /// </summary>
        public static readonly BindableProperty PressedImageProperty = BindableProperty.Create(
                                                nameof(PressedImage),
                                                typeof(string),
                                                typeof(ImageButton),
                                                null);

        /// <summary>
        /// Gets or sets the pressed state image.
        /// </summary>
        public string PressedImage
        {
            get => (string)GetValue(PressedImageProperty);
            set => SetValue(PressedImageProperty, value);
        }

        /// <summary>
        /// Allows to set the image of second state pressed button.
        /// </summary>
        public static readonly BindableProperty SecondStatePressedImageProperty = BindableProperty.Create(
                                                nameof(SecondStatePressedImage),
                                                typeof(string),
                                                typeof(ImageButton),
                                                null);

        /// <summary>
        /// Gets or sets the second state pressed image.
        /// </summary>
        public string SecondStatePressedImage
        {
            get => (string)GetValue(SecondStatePressedImageProperty);
            set => SetValue(SecondStatePressedImageProperty, value);
        }

        /// <summary>
        /// Allows to set command to invoke when the button is pressed.
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
                                                nameof(Command),
                                                typeof(ICommand),
                                                typeof(ImageButton),
                                                null);

        /// <summary>
        /// The command to invoke when the button is pressed.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Allows to define if button is in second state.
        /// </summary>
        public static readonly BindableProperty IsInSecondStateProperty = BindableProperty.Create(
                                                nameof(IsInSecondState),
                                                typeof(bool),
                                                typeof(ImageButton),
                                                false,
                                                propertyChanged: OnIsInSecondStateChanged);

        /// <summary>
        /// Gives information if button is in second state.
        /// </summary>
        public bool IsInSecondState
        {
            get => (bool)GetValue(IsInSecondStateProperty);
            set => SetValue(IsInSecondStateProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public ImageButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handler for focusing the button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Focus event arguments.</param>
        private void OnFocused(object sender, FocusEventArgs e)
        {
            DisplayedImage = IsInSecondState ? SecondStateFocusedImage : FocusedImage;
            _isFocused = true;
        }

        /// <summary>
        /// Event handler for unfocusing the button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Focus event arguments.</param>
        private void OnUnfocused(object sender, FocusEventArgs e)
        {
            DisplayedImage = IsInSecondState ? SecondStateDefaultImage : DefaultImage;
            _isFocused = false;
        }

        /// <summary>
        /// Event handler for clicking the button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Clicked event arguments.</param>
        private void OnClicked(object sender, EventArgs e)
        {
            DisplayedImage = IsInSecondState ? SecondStatePressedImage : PressedImage;
            Device.StartTimer(TimeSpan.FromMilliseconds(PRESSED_TIME), () =>
            {
                if (SecondStateDefaultImage == null)
                {
                    DisplayedImage = FocusedImage;
                }

                return false;
            });
        }

        /// <summary>
        /// Handler for changing the value of IsInSecondState property.
        /// </summary>
        /// <param name="bindable">BindableObject which raised the event.</param>
        /// <param name="oldvalue">Old value of property.</param>
        /// <param name="newvalue">New value of property</param>
        private static void OnIsInSecondStateChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ImageButton myself = (ImageButton)bindable;
            if (myself._isFocused)
            {
                myself.DisplayedImage = (bool)newvalue ? myself.SecondStateFocusedImage : myself.FocusedImage;
            }
            else
            {
                myself.DisplayedImage = (bool)newvalue ? myself.SecondStateDefaultImage : myself.DefaultImage;
            }
        }

        /// <summary>
        /// Handler for changing the value of DefaultImage property.
        /// </summary>
        /// <param name="bindable">BindableObject which raised the event.</param>
        /// <param name="oldvalue">Old value of property.</param>
        /// <param name="newvalue">New value of property</param>
        private static void OnDefaultImageChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ImageButton myself = (ImageButton)bindable;
            myself.DisplayedImage = (string)newvalue;
        }

        #endregion
    }
}