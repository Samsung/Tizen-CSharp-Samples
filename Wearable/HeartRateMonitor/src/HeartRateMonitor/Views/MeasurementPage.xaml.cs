
//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using HeartRateMonitor.ViewModels;
using System;
using System.Linq;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartRateMonitor.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MeasurementPage : CirclePage
    {
        /// <summary>
        /// An instance of the MainViewModel class.
        /// </summary>
        private MainViewModel _viewModel;

        /// <summary>
        /// Number representing duration value (in milliseconds) of heart animation.
        /// </summary>
        private const int HEART_ANIMATION_DURATION = 500;

        /// <summary>
        /// MeasurementPage class constructor.
        /// </summary>
        public MeasurementPage()
        {
            InitializeComponent();

            _viewModel = ((App)Application.Current).AppMainViewModel;
            _viewModel.MeasurementStarted += OnMeasurementStarted;
            _viewModel.MeasurementFinished += OnMeasurementFinished;
        }

        /// <summary>
        /// Handles "MeasurementStarted" event of the MainViewModel class.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Arguments passed to the event.</param>
        private void OnMeasurementStarted(object sender, EventArgs e)
        {
            StartHeartAnimation();
        }

        /// <summary>
        /// Starts animation of heart
        /// </summary>
        private async void StartHeartAnimation()
        {
            while (_viewModel.IsMeasuring)
            {
                if (!await heart.ScaleTo(1.15, HEART_ANIMATION_DURATION))
                {
                    await heart.ScaleTo(1, HEART_ANIMATION_DURATION);
                }
            }
        }

        /// <summary>
        /// Stops animation of heart
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Arguments passed to the event.</param>
        private void OnMeasurementFinished(object sender, EventArgs e)
        {
            ViewExtensions.CancelAnimations(heart);
            heart.ScaleTo(1, HEART_ANIMATION_DURATION, Easing.SinOut);
        }


        /// <summary>
        /// Handles binding context change.
        /// Updates context in all found resources (bindable type).
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (this.Resources != null)
            {
                foreach (var resource in this.Resources.Values.OfType<BindableObject>())
                {
                    resource.BindingContext = this.BindingContext;
                }
            }
        }
    }
}