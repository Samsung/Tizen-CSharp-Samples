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
using Xamarin.Forms.Xaml;

namespace SpeechToText.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClickableCellControl : ViewCell
    {
        /// <summary>
        /// Tap wait time in milliseconds.
        /// </summary>
        private static readonly int TAP_WAIT_TIME = 200;

        /// <summary>
        /// Indicates whether command is executing
        /// </summary>
        private bool _isBusy = false;

        /// <summary>
        /// The main text property definition.
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(IconSubtitleCellControl), "");

        /// <summary>
        /// The main command (cell tapped) property definition.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(IconSubtitleCellControl));

        /// <summary>
        /// The property definition for main command's parameter.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(IconSubtitleCellControl));

        public ClickableCellControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The main control's command.
        /// It is executed when the cell is tapped.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// The parameter for main control's command.
        /// </summary>
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// Main text displayed in the control.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// The callback executed when cell is tapped.
        ///
        /// Shows another icon image (IconPressed) for a specified time.
        /// Executes control's command.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void OnTapped(object sender, EventArgs e)
        {
            if (Command != null && !Command.CanExecute(CommandParameter))
            {
                return;
            }

            if (_isBusy)
                return;

            _isBusy = true;
            // Properties are not properly updated without timer
            Device.StartTimer(TimeSpan.FromMilliseconds(TAP_WAIT_TIME), () =>
            {
                Command?.Execute(CommandParameter);
                // return false to stop the timer
                return false;
            });

            _isBusy = false;
        }

    }
}