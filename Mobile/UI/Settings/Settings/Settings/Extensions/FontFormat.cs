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
using System.Diagnostics;
using Xamarin.Forms;

namespace Settings
{
    /// <summary>
    /// A enum which provides font weight values.
    /// </summary>
    public enum FontWeight
    {
        Default,
        Normal,
        Thin,
        UltraLight,
        Light,
        Book,
        Medium,
        SemiBold,
        Bold,
        UltraBold,
        Black,
        ExtraBlack
    }

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
        /// <param name="element">element object</param>
        /// <returns>FontWeight value of element</returns>
        public static FontWeight GetFontWeight(BindableObject element)
        {
            return (FontWeight)element.GetValue(FontWeightProperty);
        }

        /// <summary>
        /// Sets the font weight of the bindable element.
        /// </summary>
        /// <param name="element">element object</param>
        /// <param name="weight">font weight</param>
        public static void SetFontWeight(BindableObject element, FontWeight weight)
        {
            element.SetValue(FontWeightProperty, weight);
        }

        static void OnFontWeightChanged(BindableObject bindable, object oldValue, object newValue)
        {
            InternalExtension.InternalPropertyChanged(bindable, FontWeightProperty, () => (FontWeight)newValue == FontWeight.Default, SupportedTypes);
        }
    }
}