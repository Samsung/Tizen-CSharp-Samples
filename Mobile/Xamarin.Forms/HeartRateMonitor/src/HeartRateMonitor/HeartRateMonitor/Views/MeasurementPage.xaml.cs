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
using System.Threading.Tasks;
using HeartRateMonitor.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace HeartRateMonitor.Views
{
    /// <summary>
    /// MeasurementPage class.
    /// Provides logic for UI MeasurementPage.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeasurementPage : ContentPage
    {
        #region fields

        /// <summary>
        /// An instance of the MainViewModel class.
        /// </summary>
        private MainViewModel _viewModel;

        /// <summary>
        /// Number representing duration value (in milliseconds) of the chart animation.
        /// </summary>
        private const int CHART_ANIMATION_DURATION = 1000;

        /// <summary>
        /// Number representing duration value (in milliseconds) of hearts animation.
        /// </summary>
        private const int HEART_ANIMATION_DURATION = 500;

        /// <summary>
        /// Number representing delay value (in milliseconds) of big hearts animation.
        /// </summary>
        private const int BIG_HEARTS_ANIMATION_DELAY = 500;

        /// <summary>
        /// Number representing duration value (in milliseconds) of the stop animation.
        /// </summary>
        private const int STOP_ANIMATION_DURATION = 300;

        /// <summary>
        /// Number representing length (in pixels) of the one chart wave.
        /// </summary>
        private const int CHART_WAVE_LENGTH = 240;

        #endregion

        #region methods

        /// <summary>
        /// MeasurementPage class constructor.
        /// </summary>
        public MeasurementPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles "MeasurementStarted" event of the MainViewModel class.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Arguments passed to the event.</param>
        private void OnMeasurementStarted(object sender, EventArgs e)
        {
            StartChartAnimation();
            StartHeartAnimation(heart1, 0);
            StartHeartAnimation(heart2, BIG_HEARTS_ANIMATION_DELAY);
            StartHeartAnimation(heart3, BIG_HEARTS_ANIMATION_DELAY);
        }

        /// <summary>
        /// Handles "MeasurementFinished" event of the MainViewModel class.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Arguments passed to the event.</param>
        private void OnMeasurementFinished(object sender, EventArgs e)
        {
            StopChartAnimation();
            StopHeartAnimation(heart1);
            StopHeartAnimation(heart2);
            StopHeartAnimation(heart3);
        }

        /// <summary>
        /// Starts chart animation.
        /// </summary>
        private async void StartChartAnimation()
        {
            while (_viewModel.IsMeasuring)
            {
                ResetChartPosition();
                await chart.TranslateTo(-CHART_WAVE_LENGTH, 0, CHART_ANIMATION_DURATION);
            }
        }


        /// <summary>
        /// Starts animation of heart given as first parameter
        /// with delay given as second parameter.
        /// </summary>
        /// <param name="heartImage">Heart image object.</param>
        /// <param name="delay">Animation delay (in milliseconds).</param>
        private async void StartHeartAnimation(Image heartImage, int delay)
        {
            await Task.Delay(delay);

            while (_viewModel.IsMeasuring)
            {
                if (!await heartImage.ScaleTo(1.1, HEART_ANIMATION_DURATION))
                {
                    await heartImage.ScaleTo(1, HEART_ANIMATION_DURATION);
                }
            }
        }

        /// <summary>
        /// Stops chart animation.
        /// </summary>
        private void StopChartAnimation()
        {
            ViewExtensions.CancelAnimations(chart);
            chart.TranslateTo(-CHART_WAVE_LENGTH, 0, STOP_ANIMATION_DURATION, Easing.SinOut);
        }

        /// <summary>
        /// Stops animation of heart given as parameter.
        /// </summary>
        /// <param name="heartImage">Heart image object.</param>
        private void StopHeartAnimation(Image heartImage)
        {
            ViewExtensions.CancelAnimations(heartImage);
            heartImage.ScaleTo(1, STOP_ANIMATION_DURATION, Easing.SinOut);
        }

        /// <summary>
        /// Initializes MeasurementPage class.
        /// Assigns handlers to the MeasurementStarted and MeasurementFinished events
        /// of the MainViewModel class.
        /// </summary>
        public void Init()
        {
            _viewModel = ((App)Application.Current).AppMainViewModel;

            _viewModel.MeasurementStarted += OnMeasurementStarted;
            _viewModel.MeasurementFinished += OnMeasurementFinished;
        }

        /// <summary>
        /// Performs action when the measurement page disappears.
        /// Restores initial position of the chart element.
        /// </summary>
        protected override void OnDisappearing()
        {
            ResetChartPosition();
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

        /// <summary>
        /// Restores initial position of the chart element.
        /// </summary>
        private void ResetChartPosition()
        {
            chart.TranslationX = 0;
        }

        #endregion
    }
}