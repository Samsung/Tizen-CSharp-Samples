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
using ElmSharp;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportEffect(typeof(TooltipEffect), "TooltipEffect")]

namespace Clock.Tizen.Mobile.Renderers
{
    internal class TooltipEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                UpdateTooltip();
            }
            catch (Exception e)
            {
                Log.Error("Cannot set property on attached control. Error: ", e.Message);
            }
        }

        protected override void OnDetached()
        {
            if (Control is Widget nativeControl)
            {
                nativeControl.UnsetTooltip();
            }
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            try
            {
                if (args.PropertyName == VisualAttributes.TooltipProperty.PropertyName)
                {
                    UpdateTooltip();
                }
            }
            catch (Exception e)
            {
                Log.Error("Cannot set property on attached control. Error : ", e.Message);
            }
        }

        void UpdateTooltip()
        {
            var tooltipText = (string)Element.GetValue(VisualAttributes.TooltipProperty);
            if (Control is Widget nativeControl)
            {
                nativeControl.SetTooltipText(tooltipText);
            }
        }
    }
}