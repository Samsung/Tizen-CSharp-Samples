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

namespace RotaryTimer.Behaviors
{
    /// <summary>
    /// View behavior class that animates the timer progress.
    /// </summary>
    /// <remarks>This behavior must be connected to one view per instance.</remarks>
    public class ProgressAnimationBehavior : Behavior<View>
    {
        #region fields

        /// <summary>
        /// The element to which behavior is attached.
        /// </summary>
        private View _view;

        /// <summary>
        /// Animation name.
        /// </summary>
        private const string ANIMATION_NAME = "animation";

        #endregion

        #region properties

        /// <summary>
        /// Bindable property that allows to obtain timer setting.
        /// </summary>
        public static readonly BindableProperty TimerSettingProperty =
            BindableProperty.Create(nameof(TimerSetting), typeof(int), typeof(ProgressAnimationBehavior),
                default(int));

        /// <summary>
        /// Timer setting.
        /// </summary>
        public int TimerSetting
        {
            get => (int)GetValue(TimerSettingProperty);
        }

        /// <summary>
        /// Bindable property that allows to obtain timer state.
        /// </summary>
        public static readonly BindableProperty IsRunningProperty =
           BindableProperty.Create(nameof(IsRunning), typeof(bool), typeof(ProgressAnimationBehavior),
                true, propertyChanged: IsRunningPropertyChange);

        /// <summary>
        /// Timer state.
        /// </summary>
        public bool IsRunning
        {
            get => (bool)GetValue(IsRunningProperty);
        }

        /// <summary>
        /// Timer running state.
        /// </summary>
        /// <param name="sender">Instance triggering change.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void IsRunningPropertyChange(object sender, object oldValue, object newValue)
        {
            ((ProgressAnimationBehavior)sender).StopAnimation((bool)newValue);
        }

        /// <summary>
        /// Stops animation.
        /// </summary>
        /// <param name="newValue">Stopping flag.</param>
        private void StopAnimation(bool newValue)
        {
            if (!newValue)
            {
                Device.BeginInvokeOnMainThread(() => _view.AbortAnimation(ANIMATION_NAME));
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Called when behavior is attached to the view.
        /// </summary>
        /// <param name="bindable">Object to attach behavior to.</param>
        protected override void OnAttachedTo(View bindable)
        {
            _view = bindable;

            base.OnAttachedTo(bindable);
            AnimateProgess(bindable);
        }

        /// <summary>
        /// Animates the timer progress.
        /// </summary>
        /// <param name="view">The view to animate.</param>
        private void AnimateProgess(View view)
        {
            if (view != null)
            {
                Device.BeginInvokeOnMainThread(() => new Animation(v => view.Rotation = v, 0, -180)
                    .Commit(view, ANIMATION_NAME, 16, (uint)TimerSetting, Easing.Linear, null, () => false));
            }
        }

        #endregion
    }
}