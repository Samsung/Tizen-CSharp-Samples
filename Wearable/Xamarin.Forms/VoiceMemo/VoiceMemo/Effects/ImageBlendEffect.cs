/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
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

namespace VoiceMemo.Effects
{
    public class ImageBlendEffect : RoutingEffect
    {
        public static readonly BindableProperty BlendColorProperty =
            BindableProperty.CreateAttached("Color", typeof(Color), typeof(ImageBlendEffect), Color.Default);

        public static Color GetBlendColor(BindableObject element)
        {
            return (Color)element.GetValue(BlendColorProperty);
        }

        public static void SetBlendColor(BindableObject element, Color color)
        {
            element.SetValue(BlendColorProperty, color);
        }

        static void OnBlendColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            InternalExtension.InternalPropertyChanged(bindable, BlendColorProperty, () => (Color)newValue == Color.Default, new List<Type> { typeof(Image) });
        }

        public ImageBlendEffect() : base("SEC.BlendColorEffect")
        {
        }
    }
}
