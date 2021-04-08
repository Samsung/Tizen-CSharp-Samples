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
using System.Windows.Input;
using Xamarin.Forms;

namespace Calculator.Controls
{
    /// <summary>
    /// A element button which is implemented with a custom renderer based on a native image. </summary>
    public partial class ElementButton : Image
    {
        /// <summary>
        /// A color when the button is pressed. </summary>
        public Color BlendingPressedColor
        {
            get;
            set;
        }

        public static readonly BindableProperty CommandProperty =
           BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ElementButton), null, BindingMode.TwoWay);

        /// <summary>
        /// A command will be executed if the button is touched. </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// A command parameter will be passed when the Command is executed. </summary>
        /// <see cref="CommandButton.Command"/>
        public String CommandParameter
        {
            get;
            set;
        }

        public ElementButton()
        {
            BlendingPressedColor = Color.White;
            BackgroundColor = Color.Transparent;
            CommandParameter = "";

            InitializeComponent();
        }
    }
}
