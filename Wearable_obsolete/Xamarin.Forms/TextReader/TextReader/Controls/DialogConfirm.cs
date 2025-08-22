/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

using System.Windows.Input;
using Xamarin.Forms;

namespace TextReader.Controls
{
    /// <summary>
    /// Confirmation dialog control.
    /// </summary>
    public class DialogConfirm : BindableObject
    {
        #region properties

        /// <summary>
        /// Execute command property definition.
        /// </summary>
        public static readonly BindableProperty ExecuteCommandProperty = BindableProperty.Create(
            nameof(ExecuteCommand), typeof(ICommand), typeof(DialogConfirm), null, BindingMode.OneWayToSource);

        /// <summary>
        /// Confirm command property definition.
        /// </summary>
        public static readonly BindableProperty ConfirmCommandProperty = BindableProperty.Create(
            nameof(ConfirmCommand), typeof(ICommand), typeof(DialogConfirm));

        /// <summary>
        /// Dialog title bindable property definition.
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title), typeof(string), typeof(DialogConfirm), "");

        /// <summary>
        /// Dialog message bindable property definition.
        /// </summary>
        public static readonly BindableProperty MessageProperty = BindableProperty.Create(
            nameof(Message), typeof(string), typeof(DialogConfirm), "");

        /// <summary>
        /// Positive button text bindable property definition.
        /// </summary>
        public static readonly BindableProperty PositiveTextProperty = BindableProperty.Create(
            nameof(PositiveText), typeof(string), typeof(DialogConfirm), "Yes");

        /// <summary>
        /// Negative button text bindable property definition.
        /// </summary>
        public static readonly BindableProperty NegativeTextProperty = BindableProperty.Create(
            nameof(NegativeText), typeof(string), typeof(DialogConfirm), "No");

        /// <summary>
        /// Command which shows the dialog.
        /// </summary>
        public ICommand ExecuteCommand
        {
            get => (ICommand)GetValue(ExecuteCommandProperty);
            set => SetValue(ExecuteCommandProperty, value);
        }

        /// <summary>
        /// Command which is executed when user makes decision (yes, no).
        /// User decision is passed as a command parameter.
        /// </summary>
        public ICommand ConfirmCommand
        {
            get => (ICommand)GetValue(ConfirmCommandProperty);
            set => SetValue(ConfirmCommandProperty, value);
        }

        /// <summary>
        /// Dialog title.
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        /// <summary>
        /// Dialog message.
        /// </summary>
        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        /// <summary>
        /// Positive button text.
        /// </summary>
        public string PositiveText
        {
            get => (string)GetValue(PositiveTextProperty);
            set => SetValue(PositiveTextProperty, value);
        }

        /// <summary>
        /// Negative button text.
        /// </summary>
        public string NegativeText
        {
            get => (string)GetValue(NegativeTextProperty);
            set => SetValue(NegativeTextProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// The control constructor.
        /// </summary>
        public DialogConfirm()
        {
            ExecuteCommand = new Command(Display);
        }

        /// <summary>
        /// Displays confirmation dialog.
        /// Executes confirm command with confirmation result.
        /// </summary>
        public async void Display()
        {
            bool result = await Application.Current.MainPage.DisplayAlert(Title, Message, PositiveText, NegativeText);
            ConfirmCommand?.Execute(result);
        }

        #endregion
    }
}
