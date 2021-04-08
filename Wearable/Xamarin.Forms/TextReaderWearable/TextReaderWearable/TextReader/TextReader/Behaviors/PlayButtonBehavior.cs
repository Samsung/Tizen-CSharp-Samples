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

using System.Threading.Tasks;
using TextReader.Models;
using Xamarin.Forms;

namespace TextReader.Behaviors
{
    /// <summary>
    /// Grid behavior class that manipulates the parameters of the text reader play button.
    /// </summary>
    /// <remarks>This behavior must be connected to one grid per instance.</remarks>
    public class PlayButtonBehavior : Behavior<Grid>
    {
        #region fields

        /// <summary>
        /// Model class instance used to synthesize text into speech.
        /// </summary>
        private TextToSpeechModel _ttsModel;

        /// <summary>
        /// Grid the behavior is attached to.
        /// </summary>
        private Grid _grid;

        /// <summary>
        /// Animation name.
        /// </summary>
        private const string AnimationName = "animation";

        /// <summary>
        /// Delay to postpone making grid visible.
        /// </summary>
        private const int DataTriggerDelay = 20;

        /// <summary>
        /// Time when button is visible.
        /// </summary>
        private const int ButtonInvisibleDuration = 300;

        /// <summary>
        /// Time when button is fading out.
        /// </summary>
        private const int ButtonFadeOutDuration = 600;

        #endregion

        #region properties

        /// <summary>
        /// Bindable property for IsPlaying property.
        /// </summary>
        public static readonly BindableProperty IsPlayingProperty =
            BindableProperty.Create(nameof(IsPlaying), typeof(bool), typeof(PlayButtonBehavior),
                default(bool), propertyChanged: IsPlayingPropertyChanged);

        /// <summary>
        /// Flag indicating if the text is being read.
        /// </summary>
        public bool IsPlaying
        {
            get => (bool)GetValue(IsPlayingProperty);
            set => SetValue(IsPlayingProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Called whenever playback state is changed.
        /// </summary>
        /// <param name="sender">Object which sent the event.</param>
        /// <param name="oldValue">Previous playback state.</param>
        /// <param name="newValue">New playback state.</param>
        public static async void IsPlayingPropertyChanged(object sender, object oldValue, object newValue)
        {
            PlayButtonBehavior playButtonBehavior = sender as PlayButtonBehavior;
            await playButtonBehavior?.ShowPlayButtonAsync();
        }

        /// <summary>
        /// Manipulates the visibility of the grid.
        /// </summary>
        /// <returns>This method is asynchronous.</returns>
        private async Task ShowPlayButtonAsync()
        {
            if (_grid == null || _grid.IsVisible)
            {
                return;
            }

            _ttsModel.AnimationRunning = true;

            await Task.Delay(DataTriggerDelay);
            _grid.IsVisible = true;
            await Task.Delay(ButtonInvisibleDuration);

            void OnAnimate(double opacity)
            {
                _grid.Opacity = opacity;

                if (opacity == 0)
                {
                    _grid.IsVisible = false;
                    _grid.Opacity = 1;
                }
            }

            var fadeOpacityAnimation = new Animation(OnAnimate, 1, 0);

            Device.BeginInvokeOnMainThread(() => fadeOpacityAnimation
                .Commit(_grid, AnimationName, 16, ButtonFadeOutDuration,
                Easing.Linear, OnAnimationCompleted, () => false));
        }

        /// <summary>
        /// Executes after animation is finished.
        /// </summary>
        /// <param name="param1">Double parameter for animation completed callback.</param>
        /// <param name="param2">Bool parameter for animation completed callback.</param>
        private void OnAnimationCompleted(double param1, bool param2)
        {
            _ttsModel.AnimationRunning = false;
        }

        /// <summary>
        /// Called when behavior is attached to a grid.
        /// </summary>
        /// <param name="grid">Object to attach behavior to.</param>
        protected override void OnAttachedTo(Grid grid)
        {
            base.OnAttachedTo(grid);
            _grid = grid;
            _ttsModel = TextToSpeechModel.Instance;
        }

        /// <summary>
        /// Called when behavior is detached from a grid.
        /// </summary>
        /// <param name="grid">Object to detach behavior from.</param>
        protected override void OnDetachingFrom(Grid grid)
        {
            base.OnDetachingFrom(grid);
            _grid = null;
        }

        #endregion
    }
}
