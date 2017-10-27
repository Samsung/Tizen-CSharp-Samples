/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
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

namespace Clock.Controls
{
    /// <summary>
    /// FontFormat class
    /// It provides a way to change FontWeight value.
    /// </summary>
    public static class FontFormat
    {
        /// <summary>
        /// BindableProperty. Implements the attached property that represents the font weight.
        /// </summary>
        public static readonly BindableProperty FontWeightProperty = BindableProperty.CreateAttached("FontWeight", typeof(FontWeight), typeof(FontFormat), FontWeight.Default, propertyChanged: OnFontWeightChanged);

        public static IList<Type> SupportedTypes = new List<Type> { typeof(Label), typeof(Entry) };

        /// <summary>
        /// Gets the font weight of the bindable element.
        /// </summary>
        /// <param name="element">BindableObject</param>
        /// <returns>FontWeight</returns>
        public static FontWeight GetFontWeight(BindableObject element)
        {
            return (FontWeight)element.GetValue(FontWeightProperty);
        }

        /// <summary>
        /// Sets the font weight of the bindable element.
        /// </summary>
        /// <param name="element">BindableObject</param>
        /// <param name="weight">FontWeight</param>
        public static void SetFontWeight(BindableObject element, FontWeight weight)
        {
            element.SetValue(FontWeightProperty, weight);
        }

        /// <summary>
        /// Invoked when FontWeight value has been changed
        /// </summary>
        /// <param name="bindable">BindableObject</param>
        /// <param name="oldValue">old FontWeight value</param>
        /// <param name="newValue">new FontWeight value</param>
        static void OnFontWeightChanged(BindableObject bindable, object oldValue, object newValue)
        {
            InternalExtension.InternalPropertyChanged(bindable, FontWeightProperty, () => (FontWeight)newValue == FontWeight.Default, SupportedTypes);
        }
    }
}