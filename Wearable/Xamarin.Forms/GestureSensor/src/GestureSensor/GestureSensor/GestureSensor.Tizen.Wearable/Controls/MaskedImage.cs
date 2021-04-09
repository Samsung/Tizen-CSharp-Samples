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

﻿using Xamarin.Forms;

namespace GestureSensor.Tizen.Wearable.Controls
{
    /// <summary>
    /// Image with color mask control.
    /// </summary>
    public class MaskedImage : Image
    {
        /// <summary>
        /// Backing store for the ColorMask bindable property.
        /// </summary>
        public static readonly BindableProperty ColorMaskProperty =
            BindableProperty.Create(nameof(ColorMask), typeof(Color), typeof(MaskedImage), Color.Transparent);

        /// <summary>
        /// Gets or sets image color mask.
        /// </summary>
        public Color ColorMask
        {
            get => (Color)GetValue(ColorMaskProperty);
            set => SetValue(ColorMaskProperty, value);
        }
    }
}