/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ApplicationControl.Extensions
{
    public static class ImageAttributes
    {
        public static readonly BindableProperty BlendColorProperty = BindableProperty.CreateAttached("BlendColor", typeof(Color), typeof(ImageAttributes), Color.Default, propertyChanged: OnBlendColorPropertyChanged);

        /// <summary>
        /// Gets the blend color of the bindable element.
        /// </summary>
        /// <param name="element">element object</param>
        /// <returns>color for blend</returns>
        public static Color GetBlendColor(BindableObject element)
        {
            return (Color)element.GetValue(BlendColorProperty);
        }

        /// <summary>
        /// Sets the blend color of the bindable element.
        /// </summary>
        /// <param name="element">element object</param>
        /// <param name="color">color for blend</param>
        public static void SetBlendColor(BindableObject element, Color color)
        {
            element.SetValue(BlendColorProperty, color);
        }

        static void OnBlendColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            InternalExtension.InternalPropertyChanged(bindable, BlendColorProperty, () => (Color)newValue == Color.Default, new List<Type> { typeof(Image), typeof(CustomImageButton) });
        }
    }
}