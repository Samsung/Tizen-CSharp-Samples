/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Clock.Controls
{
    /// <summary>
    /// A custom label that reacts to touch events.
    /// </summary>
    public class MoreMenuItem : Label
    {
        /// <summary>
        /// BindableProperty. Backing store for the Command bindable property
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(MoreMenuItem), null, propertyChanged: (bo, o, n) => ((MoreMenuItem)bo).OnCommandChanged());

        /// <summary>
        /// BindableProperty. Backing store for the CommandParameter bindable property
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(Button), null,
            propertyChanged: (bindable, oldvalue, newvalue) => ((MoreMenuItem)bindable).CommandCanExecuteChanged(bindable, EventArgs.Empty));

        /// <summary>
        /// ICommand Gets or sets the command to invoke when the label is pressed.
        /// It's a bindable property
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the parameter to pass to the Command property
        /// It's a bindable property
        /// </summary>
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        bool IsEnabledCore
        {
            set { SetValueCore(IsEnabledProperty, value); }
        }

        void OnCommandChanged()
        {
            if (Command != null)
            {
                Command.CanExecuteChanged += CommandCanExecuteChanged;
                CommandCanExecuteChanged(this, EventArgs.Empty);
            }
            else
            {
                IsEnabledCore = true;
            }
        }

        void CommandCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            ICommand cmd = Command;
            if (cmd != null)
            {
                IsEnabledCore = cmd.CanExecute(CommandParameter);
            }
        }

        /// <summary>
        /// Called when a property changes
        /// </summary>
        /// <param name="propertyName">string</param>
        protected override void OnPropertyChanging(string propertyName = null)
        {
            if (propertyName == CommandProperty.PropertyName)
            {
                ICommand cmd = Command;
                if (cmd != null)
                {
                    cmd.CanExecuteChanged -= CommandCanExecuteChanged;
                }
            }

            base.OnPropertyChanging(propertyName);
        }

        /// <summary>
        /// Initializes a new instance of the MoreMenuItem class.
        /// </summary>
        public MoreMenuItem() : base()
        {
            // Margin for MoreMenuItem Label
            Margin = new Thickness(20, 38, 20, 38);
            TextColor = Color.Black;
            FontSize = 25;
        }
    }
}
