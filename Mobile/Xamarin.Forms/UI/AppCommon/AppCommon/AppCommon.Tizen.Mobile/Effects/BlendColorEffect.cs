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
using AppCommon.Tizen.Mobile.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using EColor = ElmSharp.Color;
using EImage = ElmSharp.Image;
using AppCommon.Extensions;

[assembly: ResolutionGroupName("Tizen")]
[assembly: ExportEffect(typeof(BlendColorEffect), "BlendColorEffect")]

namespace AppCommon.Tizen.Mobile.Effects
{
    internal class BlendColorEffect : PlatformEffect
    {
        /// default blend color
        static readonly EColor DefaultBlendColor = EColor.Default;

        /// <summary>
        /// Called when an effect is attached to Forms control
        /// </summary>
        protected override void OnAttached()
        {
            try
            {
                UpdateBlendColor();
            }
            catch (Exception e)
            {
                Log.Error("Cannot set property on attached control. Error: ", e.Message);
            }
        }

        /// <summary>
        /// Called when an effect is detached from Forms control.
        /// </summary>
        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            try
            {
                if (args.PropertyName == ImageAttributes.BlendColorProperty.PropertyName)
                {
                    UpdateBlendColor();
                }
            }
            catch (Exception e)
            {
                Log.Error("Cannot set property on attached control. Error : ", e.Message);
            }
        }

        /// <summary>
        /// Update ElmSharp image color using BlendColor property.
        /// </summary>
        void UpdateBlendColor()
        {
            var blendColor = (Color)Element.GetValue(ImageAttributes.BlendColorProperty);
            if (Control is EImage image)
            {
                image.Color = blendColor == Color.Default ? DefaultBlendColor : blendColor.ToNative();
            }
        }
    }
}