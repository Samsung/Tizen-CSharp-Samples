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
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace FindPlace.Tizen.Wearable.Behaviors
{
    /// <summary>
    /// Class that provides functionality for rotary events.
    /// </summary>
    public class RotaryBehavior : Behavior<CirclePage>
    {
        #region fields

        /// <summary>
        /// Default zoom level.
        /// </summary>
        private const int DefaultZoomLevel = 14;

        #endregion

        #region properties

        /// <summary>
        /// Property for zoom level.
        /// </summary>
        public static readonly BindableProperty ZoomLevelProperty =
            BindableProperty.Create(nameof(ZoomLevel), typeof(int), typeof(RotaryBehavior), DefaultZoomLevel);

        /// <summary>
        /// Zoom level property.
        /// </summary>
        public int ZoomLevel
        {
            get { return (int)GetValue(ZoomLevelProperty); }
            set { SetValue(ZoomLevelProperty, value); }
        }

        #endregion

        #region methods

        /// <summary>
        /// Overridden OnAttachedTo method which subscribes to the RotaryEventManager event.
        /// </summary>
        /// <param name="bindable">Bindable object.</param>
        protected override void OnAttachedTo(CirclePage bindable)
        {
            base.OnAttachedTo(bindable);
            ElmSharp.Wearable.RotaryEventManager.Rotated += OnRotated;
        }

        /// <summary>
        /// Overridden OnDetachingFrom method which unsubscribes to the RotaryEventManager event.
        /// </summary>
        /// <param name="bindable">Bindable object.</param>
        protected override void OnDetachingFrom(CirclePage bindable)
        {
            ElmSharp.Wearable.RotaryEventManager.Rotated -= OnRotated;
            base.OnDetachingFrom(bindable);
        }

        /// <summary>
        /// Rotated event handler
        /// </summary>
        /// <param name="args">Rotary event arguments.</param>
        private void OnRotated(ElmSharp.Wearable.RotaryEventArgs args)
        {
            if (args.IsClockwise)
            {
                ZoomLevel++;
            }
            else
            {
                ZoomLevel--;
            }
        }

        #endregion
    }
}
