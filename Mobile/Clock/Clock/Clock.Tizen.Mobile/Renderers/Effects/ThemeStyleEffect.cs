/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
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

[assembly: ResolutionGroupName("Tizen")]
[assembly: ExportEffect(typeof(ThemeStyleEffect), "ThemeStyleEffect")]

namespace Clock.Tizen.Mobile.Renderers
{
    internal class ThemeStyleEffect : PlatformEffect
    {
        static readonly string DefaultThemeStyle = "default";

        protected override void OnAttached()
        {
            try
            {
                UpdateThemeStyle();
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
                nativeControl.Style = DefaultThemeStyle;
            }
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            try
            {
                if (args.PropertyName == VisualAttributes.ThemeStyleProperty.PropertyName)
                {
                    UpdateThemeStyle();
                }
            }
            catch (Exception e)
            {
                Log.Error("Cannot set property on attached control. Error : ", e.Message);
            }
        }

        void UpdateThemeStyle()
        {
            var theme = (string)Element.GetValue(VisualAttributes.ThemeStyleProperty);
            if (Control is Widget nativeControl)
            {
                nativeControl.Style = string.IsNullOrEmpty(theme) ? DefaultThemeStyle : theme;
            }
        }
    }
}