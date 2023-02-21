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


using System;

namespace BasicCalculator.ViewModels
{
    /// <summary>
    /// Interface to maintain Keyboard.
    /// </summary>
    public interface IKeyboardService
    {
        /// <summary>
        /// Receives keyboard events.
        /// <param name="sender">Sender.</param>
        /// <param name="keyEventArgs">Key events arguments.</param>
        void KeyEvent(object sender, EventArgs keyEventArgs);

        /// <summary>
        /// Register for keyboard events for keys from the list.
        /// </summary>
        /// <param name="keyList">List of keys to register.</param>
        /// <param name="keyPressedDelegate">Delegate to call on key event.</param>
        void RegisterKeys(string[] keyList, KeyPressedDelegate keyPressedDelegate);
    }
}
