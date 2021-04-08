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

using Xamarin.Forms;

namespace VoiceMemo.Effects
{
    public static class VisualAttributes
    {
        public static readonly BindableProperty ThemeStyleProperty = BindableProperty.CreateAttached("ThemeStyle", typeof(string), typeof(VisualAttributes), string.Empty, propertyChanged: OnThemeStylePropertyChanged);
        public static readonly BindableProperty TooltipProperty = BindableProperty.CreateAttached("Tooltip", typeof(string), typeof(VisualAttributes), string.Empty, propertyChanged: OnTooltipPropertyChanged);

        public static string GetThemeStyle(BindableObject element)
        {
            return (string)element.GetValue(ThemeStyleProperty);
        }

        public static void SetThemeStyle(BindableObject element, string themeStyle)
        {
            element.SetValue(ThemeStyleProperty, themeStyle);
        }

        public static string GetTooltip(BindableObject element)
        {
            return (string)element.GetValue(TooltipProperty);
        }

        public static void SetTooltip(BindableObject element, string tooltipText)
        {
            element.SetValue(TooltipProperty, tooltipText);
        }

        static void OnTooltipPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            InternalExtension.InternalPropertyChanged(bindable, TooltipProperty, () => (string)newValue == string.Empty);
        }

        static void OnThemeStylePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            InternalExtension.InternalPropertyChanged(bindable, ThemeStyleProperty, () => (string)newValue == string.Empty);
        }
    }
}
