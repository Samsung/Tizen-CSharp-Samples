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

namespace SystemInfo.Tizen.Wearable.Control
{
    /// <summary>
    /// TizenTextCell class providing listview items containing main text and sub text.
    /// </summary>
    public class TizenTextCell : ViewCell
    {
        #region properties

        /// <summary>
        /// Text bindable property.
        /// </summary>
        public static BindableProperty TextProperty =
            BindableProperty.Create(
                "Text",
                typeof(string),
                typeof(TizenTextCell),
                default(string));

        /// <summary>
        /// Text property providing value for the main text of the control.
        /// </summary>
        public string Text
        {
            set { SetValue(TextProperty, value); }
            get { return (string)GetValue(TextProperty); }
        }

        /// <summary>
        /// Detail bindable property.
        /// </summary>
        public static BindableProperty DetailProperty =
            BindableProperty.Create(
                "Detail",
                typeof(string),
                typeof(TizenTextCell),
                default(string));

        /// <summary>
        /// Detail property providing value for the sub text of the control.
        /// </summary>
        public string Detail
        {
            set { SetValue(DetailProperty, value); }
            get { return (string)GetValue(DetailProperty); }
        }

        /// <summary>
        /// Command bindable property.
        /// </summary>
        public static BindableProperty CommandProperty =
            BindableProperty.Create(
                "Command",
                typeof(ICommand),
                typeof(TizenTextCell),
                null);

        /// <summary>
        /// Command property.
        /// </summary>
        public ICommand Command
        {
            set { SetValue(CommandProperty, value); }
            get { return (ICommand)GetValue(CommandProperty); }
        }

        #endregion

        /// <summary>
        /// Overrides OnTapped method.
        /// </summary>
        protected override void OnTapped()
        {
            base.OnTapped();
            if (Command != null)
            {
                Command.Execute(null);
            }
        }
    }
}
