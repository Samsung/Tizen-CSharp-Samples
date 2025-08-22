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
using Xamarin.Forms;
using RotaryTimer.Interfaces;
using System.Windows.Input;

namespace RotaryTimer.Views
{
    /// <summary>
    /// Setting timer view class.
    /// </summary>
    public partial class SetTimeView : ContentPage
    {
        #region properties

        /// <summary>
        /// Command setting start button image.
        /// </summary>
        public ICommand SetStartButtonPressed { get; }

        /// <summary>
        /// Command updating time value.
        /// </summary>
        private ICommand UpdateValueCommand
        {
            get => (ICommand)GetValue(UpdateValueCommandProperty);
            set => SetValue(UpdateValueCommandProperty, value);
        }

        /// <summary>
        /// Bindable property for <see cref="UpdateValueCommand">UpdateValueCommand</see>
        /// </summary>
        public static BindableProperty UpdateValueCommandProperty =
            BindableProperty.Create(nameof(UpdateValueCommand), typeof(ICommand), typeof(SetTimeView));

        #endregion

        #region methods

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public SetTimeView()
        {
            SetStartButtonPressed = new Command<bool>((enabled) =>
            {
                if (enabled)
                {
                    StartButton.Source = "button_start_pressed.png";
                }
            });

            DependencyService.Get<IRotaryService>().RotationChanged += OnRotationChanged;

            InitializeComponent();
        }


        /// <summary>
        /// Handles rotation change.
        /// Updates time value.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="clockwise">Rotation direction. True if clockwise.</param>
        private void OnRotationChanged(object sender, bool clockwise)
        {
            UpdateValueCommand.Execute(clockwise ? 1 : -1);
        }

        /// <summary>
        /// Executed on page disappearing.
        /// Sets initial source for start button image.
        /// </summary>
        protected override void OnDisappearing()
        {
            StartButton.Source = "button_start.png";
        }

        #endregion
    }
}
