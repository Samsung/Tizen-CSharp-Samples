/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
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

using Xamarin.Forms;

namespace Alarms.Controls
{
    /// <summary>
    /// Dialog control with Yes and No buttons.
    /// </summary>
    public class DialogYesNo : DialogBase
    {
        #region properties

        /// <summary>
        /// Bindable property for dialog result.
        /// </summary>
        public static readonly BindableProperty ResultProperty = BindableProperty.Create(nameof(Result),
            typeof(bool), typeof(DialogYesNo), false, BindingMode.OneWayToSource);

        /// <summary>
        /// Bindable property for Command executed on received result.
        /// </summary>
        public static readonly BindableProperty ResultReceivedProperty = BindableProperty.Create(nameof(ExecuteCommand),
            typeof(Command), typeof(DialogYesNo), null);

        /// <summary>
        /// Result of user selection from dialog box.
        /// </summary>
        public bool Result
        {
            get => (bool)GetValue(ResultProperty);
            set => SetValue(ResultProperty, value);
        }

        /// <summary>
        /// Command executed on dialog result change.
        /// </summary>
        public Command ResultReceived
        {
            get => (Command)GetValue(ResultReceivedProperty);
            set => SetValue(ResultReceivedProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Displays Yes/No alert dialog with title from the Title property and message text from the Message property.
        /// </summary>
        protected override async void DisplayAlert()
        {
            Result = await App.Current.MainPage.DisplayAlert(Title, Message, "Yes", "No");
            ResultReceived?.Execute(null);
        }

        #endregion
    }
}