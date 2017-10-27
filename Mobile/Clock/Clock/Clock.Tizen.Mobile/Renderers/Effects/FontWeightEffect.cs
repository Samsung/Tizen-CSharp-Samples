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

using Clock.Controls;
using Clock.Tizen.Mobile.Renderers;
using NativeEntry = Xamarin.Forms.Platform.Tizen.Native.Entry;
using NativeLabel = Xamarin.Forms.Platform.Tizen.Native.Label;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportEffect(typeof(FontWeightEffect), "FontWeightEffect")]

namespace Clock.Tizen.Mobile.Renderers
{
    internal class FontWeightEffect : PlatformEffect
    {
        const string DefaultFontWeight = "None";

        protected override void OnAttached()
        {
            try
            {
                UpdateFontWeight();
            }
            catch (Exception e)
            {
                Log.Error("Cannot set property on attached control. Error: ", e.Message);
            }
        }

        protected override void OnDetached()
        {
            if (Control is NativeEntry entry)
            {
                entry.FontWeight = DefaultFontWeight;
            }
            else if (Control is NativeLabel label)
            {
                label.FontWeight = DefaultFontWeight;
            }
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            try
            {
                if (args.PropertyName == FontFormat.FontWeightProperty.PropertyName)
                {
                    UpdateFontWeight();
                }
            }
            catch (Exception e)
            {
                Log.Error("Cannot set property on attached control. Error : ", e.Message);
            }
        }

        void UpdateFontWeight()
        {
            var fontWeight = (FontWeight)Element.GetValue(FontFormat.FontWeightProperty);
            if (Control is NativeEntry entry)
            {
                entry.FontWeight = fontWeight == FontWeight.Default ? DefaultFontWeight : fontWeight.ToString();
            }
            else if (Control is NativeLabel label)
            {
                label.FontWeight = fontWeight == FontWeight.Default ? DefaultFontWeight : fontWeight.ToString();
            }
        }
    }
}