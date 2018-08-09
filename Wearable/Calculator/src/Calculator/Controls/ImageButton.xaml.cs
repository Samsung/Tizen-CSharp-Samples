
//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;

using System.Windows.Input;
using Xamarin.Forms;

namespace Calculator.Controls
{
    /// <summary>
    /// A command button which is implemented with a custom renderer based on a native image. 
    /// </summary>
    public partial class ImageButton : Image
    {
        public static readonly BindableProperty CommandProperty =
           BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ImageButton), null, BindingMode.TwoWay);

        /// <summary>
        /// A command that will be executed if the button is touched. 
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// A command parameter that will be passed when the Command is executed. 
        /// </summary>
        /// <see cref="ImageButton.Command"/>
        public String CommandParameter
        {
            get;
            set;
        }

        public ImageButton()
        {
            CommandParameter = "";
            InitializeComponent();
        }
    }
}
