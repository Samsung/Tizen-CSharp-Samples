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
using ElmSharp;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

namespace ImageGallery.Tizen.Mobile.Services
{
    class KeyEventService
    {
        #region fields

        /// <summary>
        /// Key code for hardware menu key.
        /// </summary>
        private readonly string _menuKey = "XF86Menu";

        #endregion

        #region properties

        /// <summary>
        /// KeyPressed event.
        /// It is used to notify about pressing hardware menu key.
        /// </summary>
        public event EventHandler MenuKeyPressed;

        #endregion

        #region methods

        /// <summary>
        /// KeyEventService class constructor.
        /// </summary>
        public KeyEventService()
        {
            RegisterMenuKey();
        }

        /// <summary>
        /// Handles "KeyUp" event of the Forms.Context.MainWindow object.
        /// Invokes "MenuKeyPressed" event.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">An instance of the EvasKeyEventArgs class providing detailed information about the event.</param>
        private void OnKeyUp(object sender, EvasKeyEventArgs e)
        {
            if (e.KeyName.Equals(_menuKey))
            {
                MenuKeyPressed?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Registers event for hardware menu key.
        /// </summary>
        private void RegisterMenuKey()
        {
            Forms.NativeParent.KeyUp += OnKeyUp;
            Forms.NativeParent.KeyGrab(_menuKey, true);
        }

        #endregion
    }
}
