/*
 * Copyright(c) 2025 Samsung Electronics Co., Ltd.
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
 *
 */

using System;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    /// <summary>
    /// Defines an ICommand implementation that wraps a Action.
    /// </summary>
    internal class Command : ICommand
    {
        readonly Func<object, bool> _canExecute;
        readonly Action<object> _execute;

        /// <summary>
        /// Initializes a new instance of the Command class.
        /// </summary>
        /// <param name="execute">An instance to execute when the Command is executed.</param>
        public Command(Action<object> execute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = execute;
        }

        /// <summary>
        /// Initializes a new instance of the Command class.
        /// </summary>
        /// <param name="execute">An Action to execute when the Command is executed.</param>
        public Command(Action execute) : this(o => execute())
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Initializes a new instance of the Command class.
        /// </summary>
        /// <param name="execute">An Action to execute when the Command is executed.</param>
        /// <param name="canExecute">A instance indicating if the Command can be executed.</param>
        public Command(Action<object> execute, Func<object, bool> canExecute) : this(execute)
        {
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));

            _canExecute = canExecute;
        }

        /// <summary>
        /// Initializes a new instance of the Command class.
        /// </summary>
        /// <param name="execute">An Action to execute when the Command is executed.</param>
        /// <param name="canExecute">A instance indicating if the Command can be executed.</param>
        public Command(Action execute, Func<bool> canExecute) : this(o => execute(), o => canExecute())
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));
        }

        /// <summary>
        /// Returns a Boolean indicating if the Command can be executed with the given parameter.
        /// </summary>
        /// <param name="parameter">An Object used as parameter to determine if the Command can be executed.</param>
        /// <returns>true if the Command can be executed, false otherwise.</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute(parameter);

            return true;
        }

        /// <summary>
        /// Occurs when the target of the Command should reevaluate whether or not the Command can be executed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Invokes the execute Action.
        /// </summary>
        /// <param name="parameter">An Object used as parameter for the execute Action.</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// Send a CanExecuteChanged.
        /// </summary>
        public void ChangeCanExecute()
        {
            EventHandler changed = CanExecuteChanged;
            changed?.Invoke(this, EventArgs.Empty);
        }
    }
}
