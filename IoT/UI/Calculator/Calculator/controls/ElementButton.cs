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

using System.Windows.Input;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Binding;

namespace Calculator.controls
{
    static class ElementButtonBindings
    {
        public static BindingProperty<ElementButton, ICommand> CommandProperty { get; } = new BindingProperty<ElementButton, ICommand>
        {
            Setter = (v, value) => v.Command = value,
        };
    }

    internal class ElementButton : ImageView
    {
        public ElementButton()
        {
            BlendingPressedColor = Color.White;
            BackgroundColor = Color.Transparent;
            CommandParameter = "";
            FittingMode = FittingModeType.Center;

            TouchEvent += OnTouchEvent;
        }

        /// <summary>
        /// A color when the button is pressed. </summary>
        public Color BlendingPressedColor
        {
            get;
            set;
        }

        /// <summary>
        /// A color when the button is unpressed. </summary>
        public Color NormalBackgroundColor
        {
            get
            {
                return normalBackgroundColor;
            }
            set
            {
                ImageColor = BlendingColor;
                normalBackgroundColor = value;
                BackgroundColor = value;
            }
        }

        /// <summary>
        /// A command will be executed if the button is touched. </summary>
        public ICommand Command
        {
            get;
            set;
        }

        /// <summary>
        /// A command parameter will be passed when the Command is executed. </summary>
        /// <see cref="CommandButton.Command"/>
        public string CommandParameter
        {
            get;
            set;
        }

        private bool OnTouchEvent(object s, TouchEventArgs e)
        {
            switch (e.Touch.GetState(0))
            {
                case PointStateType.Down:
                    KeyDown();
                    break;
                case PointStateType.Motion:
                case PointStateType.Up:
                    KeyUp();
                    break;
            }

            return false;
        }

        /// <summary>
        /// A Action delegate which is restore button image as default
        /// and execute button's Command with CommandParameter. </summary>
        private void KeyUp()
        {
            ImageColor = BlendingColor;
            BackgroundColor = NormalBackgroundColor;

            if (isTouched)
            {
                Command?.Execute(CommandParameter);
            }

            isTouched = false;
        }

        /// <summary>
        /// A Action delegate which is restore button image as pressed situation. </summary>
        private void KeyDown()
        {
            isTouched = true;
            ImageColor = BlendingPressedColor;
            BackgroundColor = BackgroundPressedColor;
        }

        /// <summary>
        /// A flags that indicates whether the touch is handled or not.
        /// </summary>
        private volatile bool isTouched;

        /// <summary>
        /// A button's blending color
        /// </summary>
        private static Color BlendingColor = new Color((float)0xFA / 0xFF, (float)0xFA / 0xFF, (float)0xFA / 0xFF, 1.0f);

        /// <summary>
        /// A button's background color if it is touched.
        /// </summary>
        private static Color BackgroundPressedColor = new Color((float)0xFA / 0xFF, (float)0xFA / 0xFF, (float)0xFA / 0xFF, (float)0xAA / 0xFF);

        /// <summary>
        /// A button's background color if it is normal.
        /// </summary>
        private Color normalBackgroundColor;
    }
}
