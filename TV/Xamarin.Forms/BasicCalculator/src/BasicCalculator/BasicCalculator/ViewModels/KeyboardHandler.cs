/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

namespace BasicCalculator.ViewModels
{
    /// <summary>
    /// Delegate to register key-pressed events.
    /// </summary>
    /// <param name="keyName">Pressed key name.</param>
    public delegate void KeyPressedDelegate(string keyName);

    /// <summary>
    /// Class to handle key-pressed events.
    /// </summary>
    public class KeyboardHandler
    {
        private readonly CalculatorViewModel _viewModel;

        /// <summary>
        /// Method called when a key is pressed.
        /// </summary>
        /// <param name="keyName">Pressed key name.</param>
        public void KeyPressed(string keyName)
        {
            _viewModel.AppendToExpression(keyName);
        }

        /// <summary>
        /// Constructor to ViewModel object reference.
        /// </summary>
        /// <param name="viewModel">CalculatorViewModel object reference.</param>
        public KeyboardHandler(CalculatorViewModel viewModel) => _viewModel = viewModel;
    }
}