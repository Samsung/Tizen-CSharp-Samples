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

namespace Compass.Behaviors
{
    /// <summary>
    /// Image behavior class that animates compass rose rotation.
    /// </summary>
    /// <remarks>This behavior must be connected to one image per instance.</remarks>
    public class CompassRoseBehavior : Behavior<Image>
    {
        #region fields

        /// <summary>
        /// Image the behavior is connected to.
        /// </summary>
        private Image _image;

        #endregion

        #region properties

        /// <summary>
        /// Bindable property that allows to obtain compass deviation.
        /// </summary>
        public static readonly BindableProperty CompassDeviationProperty =
            BindableProperty.Create(nameof(CompassDeviation), typeof(float), typeof(CompassRoseBehavior),
                default(float), propertyChanged: CompassDeviationPropertyChanged);

        /// <summary>
        /// Compass deviation.
        /// </summary>
        public float CompassDeviation
        {
            get => (float)GetValue(CompassDeviationProperty);
            set => SetValue(CompassDeviationProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Called whenever compass deviation is changed.
        /// </summary>
        /// <param name="sender">Object which sent the event.</param>
        /// <param name="oldValue">Previous compass deviation.</param>
        /// <param name="newValue">New compass deviation.</param>
        public static void CompassDeviationPropertyChanged(object sender, object oldValue, object newValue)
        {
            ((CompassRoseBehavior)sender).RotateCompassRose((float)oldValue, (float)newValue);
        }

        /// <summary>
        /// Animates the image rotation.
        /// </summary>
        /// <param name="oldDeviation">Previous compass deviation.</param>
        /// <param name="newDeviation">New compass deviation.</param>
        public void RotateCompassRose(float oldDeviation, float newDeviation)
        {
            if (_image != null)
            {
                new Animation(v => _image.Rotation = v, -oldDeviation, -newDeviation)
                    .Commit(_image, "CompassAnimation", 16, 500, Easing.Linear, null, () => false);
            }
        }

        /// <summary>
        /// Called when behavior is attached to the image.
        /// </summary>
        /// <param name="image">Object to attach behavior to.</param>
        protected override void OnAttachedTo(Image image)
        {
            base.OnAttachedTo(image);
            _image = image;
        }

        /// <summary>
        /// Called when behavior is detached from the image.
        /// </summary>
        /// <param name="image">Object to detach behavior from.</param>
        protected override void OnDetachingFrom(Image image)
        {
            base.OnDetachingFrom(image);
            _image = null;
        }

        #endregion
    }
}
