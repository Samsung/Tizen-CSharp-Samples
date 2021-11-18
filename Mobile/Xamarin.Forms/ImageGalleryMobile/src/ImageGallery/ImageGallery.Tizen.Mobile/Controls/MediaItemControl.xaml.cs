/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
using Xamarin.Forms.Xaml;

namespace ImageGallery.Tizen.Mobile.Controls
{
    /// <summary>
    /// ThumbnailViewControl class.
    /// Provides logic for the ThumbnailViewControl.
    /// </summary>

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MediaItemControl : ContentView
    {
        #region properties

        /// <summary>
        /// "Favorite" bindable property definition.
        /// </summary>
        public static BindableProperty FavoriteProperty = BindableProperty.Create("Favorite", typeof(bool),
            typeof(MediaItemControl), default(bool));

        /// <summary>
        /// Indicates if media item is marked as favorite.
        /// </summary>
        public bool Favorite
        {
            set => SetValue(FavoriteProperty, value);
            get => (bool)GetValue(FavoriteProperty);
        }

        /// <summary>
        /// Thumbnail path bindable property definition.
        /// </summary>
        public static BindableProperty ThumbnailPathProperty = BindableProperty.Create("ThumbnailPath", typeof(string),
            typeof(MediaItemControl), default(string));

        /// <summary>
        /// Path to the thumbnail image of media item.
        /// </summary>
        public string ThumbnailPath
        {
            set => SetValue(ThumbnailPathProperty, value);
            get => (string)GetValue(ThumbnailPathProperty);
        }

        /// <summary>
        /// Select mode bindable property definition.
        /// </summary>
        public static BindableProperty SelectModeProperty = BindableProperty.Create("SelectMode", typeof(bool),
            typeof(MediaItemControl), false);

        /// <summary>
        /// Indicates if control is in select mode.
        /// In select mode the control shows checkbox which allows user to select the media item.
        /// </summary>
        public bool SelectMode
        {
            set => SetValue(SelectModeProperty, value);
            get => (bool)GetValue(SelectModeProperty);
        }

        /// <summary>
        /// "Selected" bindable property definition.
        /// </summary>
        public static BindableProperty SelectedProperty = BindableProperty.Create("Selected", typeof(bool),
            typeof(MediaItemControl), false, BindingMode.TwoWay);

        /// <summary>
        /// Indicates if media item is selected.
        /// User can select item if the control is in select mode (shows check-box).
        /// </summary>
        public bool Selected
        {
            set => SetValue(SelectedProperty, value);
            get => (bool)GetValue(SelectedProperty);
        }

        /// <summary>
        /// Default control's command bindable property definition.
        /// </summary>
        public static BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand),
            typeof(MediaItemControl), default(ICommand));

        /// <summary>
        /// Default control's command.
        /// Executed when the control is tapped.
        /// </summary>
        public ICommand Command
        {
            set => SetValue(CommandProperty, value);
            get => (ICommand)GetValue(CommandProperty);
        }

        /// <summary>
        /// Command parameter bindable property definition.
        /// </summary>
        public static BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object),
            typeof(MediaItemControl), default(object));

        /// <summary>
        /// Parameter for control's default command.
        /// </summary>
        public object CommandParameter
        {
            set => SetValue(CommandParameterProperty, value);
            get => GetValue(CommandParameterProperty);
        }

        #endregion

        #region methods

        /// <summary>
        /// The control constructor.
        /// </summary>
        public MediaItemControl()
        {
            InitializeComponent();

            Overlay.GestureRecognizers.Add(
                new TapGestureRecognizer
                {
                    Command = new Command(OnTapped)
                });
        }

        /// <summary>
        /// Handles tap on the control.
        /// </summary>
        private void OnTapped()
        {
            if (SelectMode)
            {
                Selected = !Selected;
            }
            else
            {
                if (Command != null && Command.CanExecute(CommandParameter))
                {
                    Command.Execute(CommandParameter);
                }
            }
        }

        #endregion
    }
}