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
using System;
using ElmSharp;
using ErrorCodeConverter.Interfaces;
using ErrorCodeConverter.Tizen.TV;

[assembly: Xamarin.Forms.Dependency(typeof(KeyboardService))]
namespace ErrorCodeConverter.Tizen.TV
{
    /// <summary>
    /// Provides event handlers for pressed "arrow up" and "arrow down" keys.
    /// </summary>
    class KeyboardService : IKeyboard
    {
        #region properties

        /// <summary>
        /// Event invoked on "arrow up" key press.
        /// </summary>
        public event EventHandler ArrowUpKeyDown;

        /// <summary>
        /// Event invoked on "arrow down" key press.
        /// </summary>
        public event EventHandler ArrowDownKeyDown;

        #endregion

        #region methods

        /// <summary>
        /// Initializes new instance of KeyboardService class.
        /// </summary>
        public KeyboardService()
        {
            EcoreEvent<EcoreKeyEventArgs> keyDown = new EcoreEvent<EcoreKeyEventArgs>(
                EcoreEventType.KeyDown, EcoreKeyEventArgs.Create);

            keyDown.On += (s, e) =>
            {
                switch (e.KeyName)
                {
                    case "Down":
                        ArrowDownKeyDown?.Invoke(this, null);
                        break;
                    case "Up":
                        ArrowUpKeyDown?.Invoke(this, null);
                        break;
                }
            };
        }

        #endregion
    }
}
