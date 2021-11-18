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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace ImageGallery.Tizen.Mobile.Controls.PopupView
{
    public delegate void ListItemSelected(object sender, string item);

    /// <summary>
    /// ListPopupView class.
    /// Provides functionality of the ListPopupView.
    /// </summary>
    public class ListPopupView : PopupView
    {
        #region fields

        /// <summary>
        /// An instance of the ElmSharp.List class.
        /// </summary>
        private readonly ElmSharp.List _list;

        /// <summary>
        /// List of string items.
        /// </summary>
        private readonly IList<string> _items;

        /// <summary>
        /// An instance of selected item.
        /// </summary>
        private ListItem _selectedItem = null;

        /// <summary>
        /// Sets the selected state of given item parameter.
        /// </summary>
        /// <param name="item">The list item.</param>
        /// <param name="value">Boolean value which enables/disables the item selected state.</param>
        /// <returns>Integer pointer.</returns>
        [DllImport("libelementary.so.1")]
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter")]
        internal static extern IntPtr elm_list_item_selected_set(IntPtr item, bool value);

        #endregion

        #region properties

        /// <summary>
        /// ItemSelected event.
        /// Notifies about the list item selection.
        /// </summary>
        public event ListItemSelected ItemSelected;

        /// <summary>
        /// List property storing an instance of the ElmSharp.List class.
        /// </summary>
        protected ElmSharp.List List => _list;

        /// <summary>
        /// Items property storing list of string items.
        /// </summary>
        public IList<string> Items
        {
            get => _items;
            set
            {
                List.Clear();
                _items.Clear();

                if (value == null)
                {
                    return;
                }

                foreach (string item in value)
                {
                    if (!String.IsNullOrEmpty(item))
                    {
                        List.Append(item);
                        _items.Add(item);
                    }
                }
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// ListPopupView class constructor.
        /// Creates an instance of the ElmSharp.List class.
        /// Creates ItemSelected list event handler.
        /// Creates PopupDismissed popup event handler.
        /// </summary>
        public ListPopupView()
        {
            _list = new ElmSharp.List(Popup)
            {
                Mode = ListMode.Expand
            };
            _list.SetWeight(1.0, 1.0);
            _items = new List<string>();
            _list.ItemSelected += (sender, args) =>
            {
                elm_list_item_selected_set(args.Item, false);
                _selectedItem = args.Item;
                Hide();
            };
            _list.Show();

            this.PopupDismissed += (sender, args) =>
            {
                if (_selectedItem != null)
                {
                    NotifyAboutSelection(_selectedItem.Text);
                    _selectedItem = null;
                }
            };

            Popup.SetContent(_list);
        }

        /// <summary>
        /// Returns an instance of ListPopupView class.
        /// </summary>
        /// <returns>Instance of ListPopupView class.</returns>
        public ListPopupView Create()
        {
            return new ListPopupView();
        }

        /// <summary>
        /// Returns flag indicating whether the popup is shown or not.
        /// </summary>
        /// <returns>Flag indicating whether the popup is shown or not.</returns>
        public bool IsShown()
        {
            return _isShown;
        }

        /// <summary>
        /// Notifies about the list item selection.
        /// Invokes "ItemSelected" event.
        /// </summary>
        /// <param name="selectedItemText">Text of the selected item.</param>
        protected virtual void NotifyAboutSelection(string selectedItemText)
        {
            ItemSelected?.Invoke(this, selectedItemText);
        }

        #endregion
    }
}
