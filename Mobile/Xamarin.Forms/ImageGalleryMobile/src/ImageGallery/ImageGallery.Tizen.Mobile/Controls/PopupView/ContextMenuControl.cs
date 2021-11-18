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
using System.Collections.Generic;

namespace ImageGallery.Tizen.Mobile.Controls.PopupView
{
    /// <summary>
    /// ContextMenuControl interface class.
    /// Provides functionality of the ContextMenuControl.
    /// </summary>
    public class ContextMenuControl
    {
        #region fields

        /// <summary>
        /// An instance of ListPopupView class.
        /// </summary>
        private readonly ListPopupView _listPopupView;

        #endregion

        #region properties

        /// <summary>
        /// ItemSelected event.
        /// Notifies about the list item selection.
        /// </summary>
        public event ListItemSelected ItemSelected;

        /// <summary>
        /// Items property storing list of string items.
        /// </summary>
        public IList<string> Items
        {
            get { return _listPopupView.Items; }
            set { _listPopupView.Items = value; }
        }

        #endregion

        #region methods

        /// <summary>
        /// ContextMenuControl class constructor.
        /// Creates an instance of the ListPopupView class.
        /// Attaches handler to the "ItemSelected" event.
        /// </summary>
        public ContextMenuControl()
        {
            _listPopupView = new ListPopupView();
            _listPopupView.ItemSelected += (sender, item) =>
            {
                OnItemSelected(item);
            };
        }

        /// <summary>
        /// Shows context menu.
        /// </summary>
        public void Show()
        {
            _listPopupView.Show();
        }

        /// <summary>
        /// Hides context menu.
        /// </summary>
        public void Hide()
        {
            _listPopupView.Hide();
        }

        /// <summary>
        /// Returns flag indicating whether the context menu is shown or not.
        /// </summary>
        /// <returns>Flag indicating whether the context menu is shown or not.</returns>
        public bool IsShown()
        {
            return _listPopupView.IsShown();
        }

        /// <summary>
        /// Handles "ItemSelected" event of the ListPopupView class.
        /// Invokes "ItemSelected" event.
        /// </summary>
        /// <param name="selectedItemText">Text of the selected item.</param>
        protected virtual void OnItemSelected(string selectedItemText)
        {
            ItemSelected?.Invoke(this, selectedItemText);
        }

        #endregion
    }
}
