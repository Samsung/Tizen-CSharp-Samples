/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System.Windows.Input;
using Xamarin.Forms;

namespace VoiceRecorder.Control
{
    /// <summary>
    /// Dialog control with Yes and No buttons.
    /// </summary>
    public class DialogYesNo : BindableObject
    {
        #region properties

        /// <summary>
        /// Execute command property definition.
        /// </summary>
        public static readonly BindableProperty ExecuteCommandProperty = BindableProperty.Create(
            nameof(ExecuteCommand), typeof(ICommand), typeof(DialogYesNo), null, BindingMode.OneWayToSource);

        /// <summary>
        /// Cancel command property definition.
        /// </summary>
        public static readonly BindableProperty CancelCommandProperty = BindableProperty.Create(
            nameof(CancelCommand), typeof(ICommand), typeof(DialogYesNo));

        /// <summary>
        /// Confirm command property definition.
        /// </summary>
        public static readonly BindableProperty ConfirmCommandProperty = BindableProperty.Create(
            nameof(ConfirmCommand), typeof(ICommand), typeof(DialogYesNo));

        /// <summary>
        /// Dialog title bindable property definition.
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title), typeof(string), typeof(DialogYesNo), "");

        /// <summary>
        /// Dialog message bindable property definition.
        /// </summary>
        public static readonly BindableProperty MessageProperty = BindableProperty.Create(
            nameof(Message), typeof(string), typeof(DialogYesNo), "");

        /// <summary>
        /// Command which shows the dialog.
        /// </summary>
        public ICommand ExecuteCommand
        {
            get => (ICommand)GetValue(ExecuteCommandProperty);
            set => SetValue(ExecuteCommandProperty, value);
        }

        /// <summary>
        /// Command which is executed when user taps "No" button.
        /// </summary>
        public ICommand CancelCommand
        {
            get => (ICommand)GetValue(CancelCommandProperty);
            set => SetValue(CancelCommandProperty, value);
        }

        /// <summary>
        /// Command which is executed when user taps "Yes" button.
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

        #endregion

        #region methods

        /// <summary>
        /// The control constructor.
        /// </summary>
        public DialogYesNo()
        {
            ExecuteCommand = new Command(Display);
        }

        /// <summary>
        /// Displays Yes-No dialog with title from the Title property and message text from the Message property.
        /// </summary>
        public async void Display()
        {
            var answer = await Application.Current.MainPage.DisplayAlert(Title, Message, "Delete", "Cancel");

            if (answer)
            {
                ConfirmCommand?.Execute(null);
                return;
            }

            CancelCommand?.Execute(null);
        }

        #endregion
    }
}