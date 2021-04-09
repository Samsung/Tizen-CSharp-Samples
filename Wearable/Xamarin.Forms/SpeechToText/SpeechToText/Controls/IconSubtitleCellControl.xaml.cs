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
    /// The custom control for list cell containing left-aligned icon, main and subtitle text.
    /// The control provides command which is executed when the cell is tapped.
    /// There is a possibility to specify an icon image (IconPressed) for tap animation.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IconSubtitleCellControl : ViewCell
    {
        #region fields

        /// <summary>
        /// Tap animation time in milliseconds.
        /// </summary>
        private static readonly int TAP_ANIMATION_TIME = 200;

        #endregion

        #region properties

        /// <summary>
        /// The main text property definition.
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(IconSubtitleCellControl), "");

        /// <summary>
        /// The subtitle text property definition.
        /// </summary>
        public static readonly BindableProperty SubtitleProperty =
            BindableProperty.Create("Subtitle", typeof(string), typeof(IconSubtitleCellControl), "");

        /// <summary>
        /// The icon image path property definition.
        /// </summary>
        public static readonly BindableProperty IconProperty =
            BindableProperty.Create("Icon", typeof(string), typeof(IconSubtitleCellControl));

        /// <summary>
        /// The icon image path (pressed state) property definition.
        /// </summary>
        public static readonly BindableProperty IconPressedProperty =
            BindableProperty.Create("IconPressed", typeof(string), typeof(IconSubtitleCellControl));

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
        /// Icon image path.
        /// Icon is displayed on the left side of the control.
        /// </summary>
        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        /// <summary>
        /// The icon image path for pressed state of the control (tap).
        /// </summary>
        public string IconPressed
        {
            get => (string)GetValue(IconPressedProperty);
            set => SetValue(IconPressedProperty, value);
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
        /// The control's class constructor.
        /// </summary>
        public IconSubtitleCellControl()
        {
            InitializeComponent();
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

            if (IconImage.IsVisible)
            {
                IconImage.IsVisible = false;
                Device.StartTimer(TimeSpan.FromMilliseconds(TAP_ANIMATION_TIME), () =>
                {
                    IconImage.IsVisible = true;
                    Command?.Execute(CommandParameter);

                    // return false to stop the timer
                    return false;
                });
            }
        }

        #endregion
    }
}