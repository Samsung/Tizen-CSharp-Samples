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
    /// Base class for dialog controls.
    /// </summary>
    public abstract class DialogBase : BindableObject
    {
        #region properties

        /// <summary>
        /// BindableProperty for showing dialog Command execution.
        /// </summary>
        public static readonly BindableProperty ExecuteCommandProperty = BindableProperty.Create(nameof(ExecuteCommand),
            typeof(Command), typeof(DialogBase), null, BindingMode.OneWayToSource);

        #endregion

        #region methods

        /// <summary>
        /// Constructor of the control object.
        /// </summary>
        protected DialogBase()
        {
            ExecuteCommand = new Command(DisplayAlert);
        }

        /// <summary>
        /// Property for command which is executed to show the dialog.
        /// </summary>
        public Command ExecuteCommand
        {
            get => (Command)GetValue(ExecuteCommandProperty);
            set => SetValue(ExecuteCommandProperty, value);
        }

        /// <summary>
        /// Dialog box title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Dialog box message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Abstract method which should display the alert dialog with title from the <see cref="Title"/> property
        /// and message text from the <see cref="Message"/> property.
        /// </summary>
        protected abstract void DisplayAlert();

        #endregion
    }
}