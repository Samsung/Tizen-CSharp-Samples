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
    public static class ProgressAttributes
    {
        public static readonly BindableProperty PulsingStatusProperty =
            BindableProperty.CreateAttached("PulsingStatus", typeof(bool), typeof(ProgressAttributes), false, propertyChanged: OnPulsingStatusPropertyChanged);

        public static bool GetPulsingStatus(BindableObject element)
        {
            return (bool)element.GetValue(PulsingStatusProperty);
        }

        public static void SetPulsingStatus(BindableObject element, bool isPulsing)
        {
            string style = VisualAttributes.GetThemeStyle(element);
            if (style == "pending")
            {
                element.SetValue(PulsingStatusProperty, isPulsing);
            }
        }

        static void OnPulsingStatusPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            InternalExtension.InternalPropertyChanged(bindable, PulsingStatusProperty, () => (bool)newValue == false, new List<Type> { typeof(ProgressBar) });
        }
    }
}