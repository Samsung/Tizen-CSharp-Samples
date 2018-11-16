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

namespace VoiceRecorder.Tizen.Mobile.Control
{
    /// <summary>
    /// Toast control.
    /// </summary>
    public class TizenToast : BindableObject
    {
        #region properties

        /// <summary>
        /// Execute command property definition.
        /// </summary>
        public static readonly BindableProperty ExecuteCommandProperty = BindableProperty.Create(
            nameof(ExecuteCommand), typeof(ICommand), typeof(TizenToast), null, BindingMode.OneWayToSource);

        /// <summary>
        /// Toast text bindable property definition.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text), typeof(string), typeof(TizenToast), "");

        /// <summary>
        /// Command which shows the toast.
        /// </summary>
        public ICommand ExecuteCommand
        {
            get => (ICommand)GetValue(ExecuteCommandProperty);
            set => SetValue(ExecuteCommandProperty, value);
        }

        /// <summary>
        /// Toast text.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// The control constructor.
        /// </summary>
        public TizenToast()
        {
            ExecuteCommand = new Command(Display);
        }

        /// <summary>
        /// Displays toast with text from the Text property.
        /// </summary>
        public void Display()
        {
            Toast.DisplayText(Text);
        }

        #endregion
    }
}
