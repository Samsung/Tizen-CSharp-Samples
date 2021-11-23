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
using System;
using ElmSharp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

namespace ImageGallery.Tizen.Mobile.Controls.PopupView
{
    /// <summary>
    /// PopupView class.
    /// Provides base functionality of the PopupView.
    /// </summary>
    public class PopupView
    {
        #region fields

        /// <summary>
        /// Flag indicating whether the popup is shown or not.
        /// </summary>
        protected bool _isShown = false;

        #endregion

        #region properties

        /// <summary>
        /// Popup property storing an instance of the ElmSharp.Popup class.
        /// </summary>
        protected Popup Popup { get; private set; }

        /// <summary>
        /// PopupDismissed event.
        /// Notifies about the popup dismission.
        /// </summary>
        public event EventHandler PopupDismissed;

        #endregion

        #region methods

        /// <summary>
        /// PopupView class constructor.
        /// Creates an instance of the ElmSharp.Popup class.
        /// </summary>
        public PopupView()
        {
            Popup = new Popup(Forms.NativeParent);
            Popup.SetAlignment(-1.0, 1.0);
            Popup.SetWeight(1.0, 1.0);
        }

        /// <summary>
        /// Displays popup.
        /// Attaches handler of OutsideClicked event.
        /// </summary>
        public void Show()
        {
            if (_isShown)
            {
                return;
            }

            Popup.OutsideClicked += OnOutsideClicked;
            Popup.Dismissed += OnPopupDismissed;
            Popup.Show();
            _isShown = true;
        }

        /// <summary>
        /// Hides popup.
        /// Detaches handler of OutsideClicked event.
        /// </summary>
        public void Hide()
        {
            if (!_isShown)
            {
                return;
            }

            Popup.OutsideClicked -= OnOutsideClicked;
            Popup.Hide();
            _isShown = false;
        }

        /// <summary>
        /// Handles "OutsideClicked" event of the ElmSharp.Popup.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">An instance of the EventArgs class providing detailed information about the event.</param>
        private void OnOutsideClicked(object sender, EventArgs e)
        {
            Hide();
        }

        /// <summary>
        /// Handles "Dismissed" event of the ElmSharp.Popup.
        /// Notifies about the popup dismission.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">An instance of the EventArgs class providing detailed information about the event.</param>
        private void OnPopupDismissed(object sender, EventArgs e)
        {
            PopupDismissed?.Invoke(this, e);
            Popup.Dismissed -= OnPopupDismissed;
        }

        #endregion
    }
}
