/*
 * Copyright (c) 2025 Samsung Electronics Co., Ltd
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

using Calculator.Tizen;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Binding;

namespace Calculator.controls
{
    static class RadTextLabelBindings
    {
        public static BindingProperty<RadTextLabel, bool> IsVisibleProperty { get; } = new BindingProperty<RadTextLabel, bool>
        {
            Setter = (v, value) => v.IsVisible = value,
        };
    }

    internal class RadTextLabel : TextLabel
    {
        public RadTextLabel(TextLabelStyle style) : base(style)
        {
        }

        public bool IsVisible
        {
            set
            {
                if (value)
                {
                    Text = "Rad";
                }
                else
                {
                    Text = "";
                }
            }
        }
    }
}
