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
using Xamarin.Forms;

namespace SpeechToText.Views
{
    /// <summary>
    /// Settings item class.
    /// Used as one item for list item source.
    /// </summary>
    class SettingsItem : BindableObject
    {
        #region properties

        /// <summary>
        /// Value property definition.
        /// It defines bindable property which value contains current value of settings item.
        /// </summary>
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create("Value", typeof(string), typeof(SettingsItem));

        /// <summary>
        /// Command property definition.
        /// It defines bindable property which value contains main item's action command.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(SettingsItem));

        /// <summary>
        /// Item title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Item icon image path.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Item command.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Item icon image path (pressed state).
        /// </summary>
        public string IconPressed { get; set; }

        /// <summary>
        /// Item command parameter.
        /// </summary>
        public object CommandParameter { get; set; }

        /// <summary>
        /// Current value of the settings item.
        /// </summary>
        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        #endregion
    }
}
