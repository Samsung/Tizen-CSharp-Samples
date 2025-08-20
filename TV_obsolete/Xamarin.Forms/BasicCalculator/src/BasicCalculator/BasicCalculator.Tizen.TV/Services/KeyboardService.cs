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

using BasicCalculator.Tizen.TV.Services;
using ElmSharp;
using Xamarin.Forms;
using BasicCalculator.ViewModels;

[assembly: Xamarin.Forms.Dependency(typeof(KeyboardService))]

namespace BasicCalculator.Tizen.TV.Services
{
    /// <summary>
    /// Keyboard listener and event dispatcher.
    /// </summary>
    internal class KeyboardService : IKeyboardService
    {
        #region fields

        /// <summary>
        /// Delegate used by <see cref="KeyEvent(object, EvasKeyEventArgs)"/> to maintain
        /// Event registered methods.
        /// </summary>
        private KeyPressedDelegate _keyPressedDelegate;

        #endregion fields

        #region methods

        /// <summary>
        /// Event fired when any key is received by the application's MainWindow.
        /// </summary>
        /// <param name="sender">Sending object's reference.</param>
        /// <param name="keyEventArgs">Key event arguments.</param>
        private void KeyEvent(object sender, EvasKeyEventArgs keyEventArgs)
        {
            _keyPressedDelegate?.Invoke(keyEventArgs.KeyName);
        }

        /// <summary>
        /// Registers KeyUp Event for keys passed as list in the first argument to execute
        /// delegate passed in the second argument.
        /// </summary>
        /// <param name="keyList">List of keys names.</param>
        /// <param name="keyPressedDelegate">Delegate to be executed on KeyUp event.</param>
        public void RegisterKeys(string[] keyList, KeyPressedDelegate keyPressedDelegate)
        {
            _keyPressedDelegate += keyPressedDelegate;
            Forms.NativeParent.KeyUp += KeyEvent;
            foreach (var key in keyList)
            {
                Forms.NativeParent.KeyGrab(key, true);
            }
        }

        #endregion methods
    }
}
