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
    /// <summary>
    /// The custom control for list cell containing main and subtitle text.
    /// The control provides command which is executed when the cell is tapped.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubtitleCell : ViewCell
    {
        #region properties

        /// <summary>
        /// The main text property definition.
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(SubtitleCell), "");

        /// <summary>
        /// The subtitle text property definition.
        /// </summary>
        public static readonly BindableProperty SubtitleProperty =
            BindableProperty.Create("Subtitle", typeof(string), typeof(SubtitleCell), "");

        /// <summary>
        /// The main command (cell tapped) property definition.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(SubtitleCell));

        /// <summary>
        /// The property definition for main command's parameter.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(SubtitleCell));

        /// <summary>
        /// Main text displayed in the control.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// Subtitle text displayed below the main text.
        /// </summary>
        public string Subtitle
        {
            get => (string)GetValue(SubtitleProperty);
            set => SetValue(SubtitleProperty, value);
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
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// The control constructor.
        /// </summary>
        public SubtitleCell()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The callback executed when cell is tapped.
        /// Executes control's command.
        /// </summary>
        /// <param name="sender">Tapped cell.</param>
        /// <param name="e">Event arguments.</param>
        private void OnTapped(object sender, EventArgs e)
        {
            if (Command != null && !Command.CanExecute(CommandParameter))
            {
                return;
            }

            Command?.Execute(CommandParameter);
        }

        #endregion
    }
}