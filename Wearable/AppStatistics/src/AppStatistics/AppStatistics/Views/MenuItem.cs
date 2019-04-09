/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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

namespace AppStatistics.Views
{
    /// <summary>
    /// Menu item class.
    /// Used as one element of the menu list.
    /// </summary>
    class MenuItem : BindableObject
    {
        #region properties

        /// <summary>
        /// Bindable property definition for menu item's image.
        /// </summary>
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create("Image", typeof(string), typeof(MenuItem), default(string));

        /// <summary>
        /// Bindable property definition for menu item's action command.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(MenuItem), default(ICommand));

        /// <summary>
        /// Bindable property definition for menu item's action command parameter.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(MenuItem), default(object));

        /// <summary>
        /// Current value of the menu item's title.
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Current value of the menu item's image.
        /// </summary>
        public string Image
        {
            get => (string)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        /// <summary>
        /// Menu item's command.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Menu item's command parameter.
        /// </summary>
        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        #endregion
    }
}
