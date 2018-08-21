
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

using System.Windows.Input;
using Tizen;
using Xamarin.Forms;

namespace HeartRateMonitor.Controls
{
    /// <summary>
    /// Dialog control with OK button.
    /// </summary>
    public class DialogOK : BindableObject
    {
        #region properties

        /// <summary>
        /// Execute command property definition.
        /// </summary>
        public static readonly BindableProperty ExecuteCommandProperty = BindableProperty.Create(
            nameof(ExecuteCommand), typeof(ICommand), typeof(DialogOK), null, BindingMode.OneWayToSource);

        /// <summary>
        /// Confirm command property definition.
        /// </summary>
        public static readonly BindableProperty ConfirmCommandProperty = BindableProperty.Create(
            nameof(ConfirmCommand), typeof(ICommand), typeof(DialogOK));

        /// <summary>
        /// Dialog title bindable property definition.
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title), typeof(string), typeof(DialogOK), "");

        /// <summary>
        /// Dialog message bindable property definition.
        /// </summary>
        public static readonly BindableProperty MessageProperty = BindableProperty.Create(
            nameof(Message), typeof(string), typeof(DialogOK), "");

        /// <summary>
        /// Command which shows the dialog.
        /// </summary>
        public ICommand ExecuteCommand
        {
            get => (ICommand)GetValue(ExecuteCommandProperty);
            set => SetValue(ExecuteCommandProperty, value);
        }

        /// <summary>
        /// Command which is executed when user confirms the message (taps OK button).
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
        public DialogOK()
        {
            ExecuteCommand = new Command(Display);
        }

        /// <summary>
        /// Displays OK dialog with title from the Title property and message text from the Message property.
        /// </summary>
        public async void Display()
        {
            Log.Debug("HRM", "oh boy");
            await Application.Current.MainPage.DisplayAlert(Title, Message, "OK");

            Log.Debug("HRM", "sure");
            ConfirmCommand?.Execute(null);
        }

        #endregion
    }
}
