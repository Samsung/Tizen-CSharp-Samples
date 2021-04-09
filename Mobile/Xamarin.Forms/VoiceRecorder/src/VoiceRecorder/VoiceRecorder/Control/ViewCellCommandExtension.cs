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
    /// ViewCellCommandExtension class.
    /// Extends View Cell with Command property.
    /// </summary>
    public class ViewCellCommandExtension : ViewCell
    {
        #region properties

        /// <summary>
        /// Bindable property for Command.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(ViewCellCommandExtension));

        /// <summary>
        /// Bindable property for CommandParameter.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(ViewCellCommandExtension));

        /// <summary>
        /// Bindable property for CellContext.
        /// </summary>
        public static readonly BindableProperty CellContextProperty =
            BindableProperty.Create(nameof(CellContext), typeof(Xamarin.Forms.View), typeof(ViewCellCommandExtension));

        /// <summary>
        /// Visualization of the ViewCellCommandExtension.
        /// </summary>
        public Xamarin.Forms.View CellContext
        {
            get => (Xamarin.Forms.View)GetValue(CellContextProperty);
            set => SetValue(CellContextProperty, value);
        }

        /// <summary>
        /// Command to execute on tap.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Object specified by a binding source.
        /// </summary>
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Executes the ViewCellCommandExtension Command property.
        /// </summary>
        protected override void OnTapped()
        {
            Command?.Execute(CommandParameter);
        }

        #endregion
    }
}
