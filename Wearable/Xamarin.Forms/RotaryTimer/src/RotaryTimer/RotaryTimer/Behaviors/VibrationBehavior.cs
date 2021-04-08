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
using RotaryTimer.Interfaces;
using Xamarin.Forms;

namespace RotaryTimer.Behaviors
{
    /// <summary>
    /// View behavior class that starts vibration on timer complete.
    /// </summary>
    /// <remarks>This behavior must be connected to one view per instance.</remarks>
    public class VibrationBehavior : Behavior<View>
    {
        #region properties

        /// <summary>
        /// Bindable property that allows to obtain timer status.
        /// </summary>
        public static readonly BindableProperty HasEndedProperty =
            BindableProperty.Create(nameof(HasEnded), typeof(bool), typeof(VibrationBehavior),
                false, propertyChanged: HasEndedPropertyChange);

        /// <summary>
        /// Flag indicating whether timer has ended or not.
        /// </summary>
        public bool HasEnded
        {
            get => (bool)GetValue(HasEndedProperty);
        }

        #endregion

        #region methods

        /// <summary>
        /// Handles HasEndedProperty change.
        /// </summary>
        /// <param name="sender">Instance triggering change.</param>
        /// <param name="oldValue">Old value.</param>
        /// <param name="newValue">New value.</param>
        private static void HasEndedPropertyChange(object sender, object oldValue, object newValue)
        {
            ((VibrationBehavior)sender).Vibrate((bool)newValue);
        }


        /// <summary>
        /// Starts vibrations.
        /// </summary>
        /// <param name="completed">Ended flag.</param>
        private void Vibrate(bool completed)
        {
            if (completed)
            {
                DependencyService.Get<IVibration>().Vibrate();
            }
        }

        #endregion
    }
}