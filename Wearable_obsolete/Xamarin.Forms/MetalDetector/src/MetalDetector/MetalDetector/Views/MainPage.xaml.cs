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
using System.ComponentModel;
using MetalDetector.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MetalDetector.Views
{
    /// <summary>
    /// Main (and only one) page of the application.
    /// Handles UI logic for the application.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        #region fields

        /// <summary>
        /// Reference to the object of the MainViewModel class.
        /// It allows to listen to the main view model events and execute its commands.
        /// </summary>
        private MainViewModel _mainViewModel;

        /// <summary>
        /// Reference to the object of the Animation class.
        /// It allows to create and start animation of the radar element.
        /// </summary>
        private Animation _radarAnimation;

        #endregion

        #region methods

        /// <summary>
        /// The page constructor.
        /// Creates page structure defined in the XAML file.
        /// Initializes radar animation.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            InitRadarAnimation();
        }

        /// <summary>
        /// Initializes radar animation.
        /// </summary>
        private void InitRadarAnimation()
        {
            _radarAnimation = new Animation(v => radar.Rotation = v, 0, 360);
        }

        /// <summary>
        /// Starts radar animation.
        /// </summary>
        private void StartRadarAnimation()
        {
            _radarAnimation.Commit(this, "RadarAnimation", 16, 2000, Easing.Linear, (v, c) => radar.Rotation = 0, () => true);
        }

        /// <summary>
        /// Handles "PropertyChanged" event of the view model.
        /// Starts radar animation.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_mainViewModel.Ready))
            {
                StartRadarAnimation();
            }
        }

        /// <summary>
        /// Performs action when the main page appears.
        /// Starts metal detector.
        /// </summary>
        protected override void OnAppearing()
        {
            _mainViewModel = (MainViewModel)this.BindingContext;

            _mainViewModel.PropertyChanged += OnPropertyChanged;
            _mainViewModel.StartCommand.Execute(null);
        }

        /// <summary>
        /// Performs action when the main page disappears.
        /// Stops metal detector.
        /// </summary>
        protected override void OnDisappearing()
        {
            _mainViewModel.StopCommand.Execute(null);
        }

        #endregion
    }
}
