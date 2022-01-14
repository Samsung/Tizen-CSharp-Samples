/*
 * Copyright (c) 2022 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Tizen.NUI.Binding;
using Tizen.NUI.Components;

namespace Location.Behaviors
{
    public static class TextSetter
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.CreateAttached(
                "Text",
                typeof(string),
                typeof(TextSetter),
                "",
                propertyChanged: OnTextChanged);

        public static string GetText(BindableObject button) => (string)button.GetValue(TextProperty);

        public static void SetText(BindableObject button, string value) => button.SetValue(TextProperty, value);

        public static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is string text && bindable is Button button)
            {
                button.Text = text;
            }
        }
    }
}
