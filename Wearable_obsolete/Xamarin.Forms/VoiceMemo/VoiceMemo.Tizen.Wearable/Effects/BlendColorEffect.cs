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

using EColor = ElmSharp.Color;
using EImage = ElmSharp.Image;
using System;
using System.ComponentModel;
using VoiceMemo.Tizen.Wearable.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using VoiceMemo.Effects;

[assembly: ResolutionGroupName("SEC")]
[assembly: ExportEffect(typeof(BlendColorEffect), "BlendColorEffect")]

namespace VoiceMemo.Tizen.Wearable.Effects
{
    internal class BlendColorEffect : PlatformEffect
    {
        static readonly EColor DefaultBlendColor = EColor.Default;

        public object ImageAtctributes { get; private set; }

        protected override void OnAttached()
        {
            try
            {
                if (Control is EImage nativeControl)
                {
                    nativeControl.LoadingCompleted += OnNativeImageLoadingCompleted;
                }

                UpdateBlendColor();
            }
            catch (Exception e)
            {
                Log.Error("Cannot set property on attached control. Error: ", e.Message);
            }
        }

        protected override void OnDetached()
        {
            if (Control is EImage nativeControl)
            {
                nativeControl.LoadingCompleted -= OnNativeImageLoadingCompleted;
                nativeControl.Color = DefaultBlendColor;
            }
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
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

            base.OnElementPropertyChanged(args);
        }

        void OnNativeImageLoadingCompleted(object sender, EventArgs e)
        {
            UpdateBlendColor();
        }

        void UpdateBlendColor()
        {
            if ((Element as Image).IsLoading)
            {
                Device.StartTimer(TimeSpan.Zero, () =>
                {
                    InternalBlendColorUpdate();
                    return false;
                });
            }
            else
            {
                InternalBlendColorUpdate();
            }
        }

        void InternalBlendColorUpdate()
        {
            if (Control is EImage image)
            {
                var blendColor = (Color)Element.GetValue(ImageAttributes.BlendColorProperty);
                image.Color = blendColor == Color.Default ? DefaultBlendColor : blendColor.ToNative();
            }
        }
    }
}
