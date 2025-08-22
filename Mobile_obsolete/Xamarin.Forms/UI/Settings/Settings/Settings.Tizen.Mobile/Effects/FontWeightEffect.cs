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
using System.ComponentModel;
using Settings.Tizen.Mobile;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using NativeEntry = Xamarin.Forms.Platform.Tizen.Native.Entry;
using NativeLabel = Xamarin.Forms.Platform.Tizen.Native.Label;
using System.Diagnostics;

[assembly: ResolutionGroupName("Tizen")]
[assembly: ExportEffect(typeof(FontWeightEffect), "FontWeightEffect")]

namespace Settings.Tizen.Mobile
{
    internal class FontWeightEffect : PlatformEffect
    {
        const string DefaultFontWeight = "None";

        /// <summary>
        /// Called when an effect is attached to Forms control
        /// </summary>
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

        /// <summary>
        /// Called when an effect is detached from Forms control.
        /// set default font weight.
        /// </summary>
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

        /// <summary>
        /// Update ElmSharp font weight value using FontWeight property value.
        /// </summary>
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