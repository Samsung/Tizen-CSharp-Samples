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

﻿using GestureSensor.Tizen.Wearable.Controls;
using GestureSensor.Tizen.Wearable.Renderers;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(MaskedImage), typeof(MaskedImageRenderer))]
namespace GestureSensor.Tizen.Wearable.Renderers
{
    /// <summary>
    /// Renderer for <see cref="MaskedImage"/>.
    /// </summary>
    public class MaskedImageRenderer : ImageRenderer
    {
        /// <summary>
        /// Updates image color mask when element has changed.
        /// </summary>
        /// <param name="e">Element that changed.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            UpdateColor();
        }

        /// <summary>
        /// Updates image color mask when element's color mask property has changed.
        /// </summary>
        /// <param name="sender">Object that invoked event.</param>
        /// <param name="e">Event arguments.</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            UpdateColor();
        }

        private void UpdateColor()
        {
            var colorImage = Element as MaskedImage;
            if (Control == null || colorImage == null)
            {
                return;
            }

            Control.Color = colorImage.ColorMask.ToNative();
        }
    }
}