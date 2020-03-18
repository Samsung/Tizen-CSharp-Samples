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
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using VoiceRecorder.Tizen.Mobile.Control;
using VoiceRecorder.Tizen.Mobile.Renderer;
using EContextPopup = ElmSharp.ContextPopup;
using EContextPopupDirection = ElmSharp.ContextPopupDirection;
using EContextPopupItem = ElmSharp.ContextPopupItem;
using EIcon = ElmSharp.Icon;
using Xamarin.Forms;
using XFPlatformTizen = Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen;

[assembly: Xamarin.Forms.Dependency(typeof(ContextPopupRenderer))]

namespace VoiceRecorder.Tizen.Mobile.Renderer
{
    /// <summary>
    /// ContextPopup control Tizen renderer.
    /// Provides methods to control ContextPopup.
    /// Implements IContextPopup interface.
    /// </summary>
    internal class ContextPopupRenderer : IContextPopup, INotifyPropertyChanged, IDisposable
    {
        #region fields

        /// <summary>
        /// Instance of ContextPopup class.
        /// </summary>
        private EContextPopup _popup;

        /// <summary>
        /// Dictionary with ContextPopup items.
        /// </summary>
        private IDictionary<ContextPopupItem, EContextPopupItem> _items;

        /// <summary>
        /// Backing field of IsAutoHidingEnabled property.
        /// </summary>
        private bool _isAutoHidingEnabled = true;

        /// <summary>
        /// Backing field of DirectionPriorities property.
        /// </summary>
        private ContextPopupDirectionPriorities _priorities =
            new ContextPopupDirectionPriorities(ContextPopupDirection.Up, ContextPopupDirection.Left, ContextPopupDirection.Right, ContextPopupDirection.Down);

        /// <summary>
        /// Backing field of SelectedItem property.
        /// </summary>
        private ContextPopupItem _selectedItem = null;

        /// <summary>
        /// Flag indicating whether the ContextPopup is diasposed or not.
        /// </summary>
        private bool _isDisposed;

        #endregion

        #region properties

        /// <summary>
        /// Occurs when a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when a ContextPopupItem is selected.
        /// </summary>
        public event EventHandler ItemSelected;

        /// <summary>
        /// Occurs when the ContextPopup is dismissed.
        /// </summary>
        public event EventHandler Dismissed;

        /// <summary>
        /// Gets or sets whether ContextPopup should be hidden automatically
        /// or not when parent of ContextPopup is resized.
        /// </summary>
        public bool IsAutoHidingEnabled
        {
            get
            {
                return _isAutoHidingEnabled;
            }

            set
            {
                _isAutoHidingEnabled = value;
                UpdateIsAutoHidingEnabled();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the direction priorities for the ContextPopup.
        /// </summary>
        public ContextPopupDirectionPriorities DirectionPriorities
        {
            get
            {
                return _priorities;
            }

            set
            {
                _priorities = value;
                UpdateDirectionPriorities();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the selected item of the ContextPopup.
        /// </summary>
        public ContextPopupItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region methods

        /// <summary>
        /// ContextPopupRenderer class constructor.
        /// </summary>
        public ContextPopupRenderer()
        {
            _popup = new EContextPopup(Forms.NativeParent);

            _popup.BackButtonPressed += (s, e) =>
            {
                _popup.Dismiss();
            };

            _popup.Dismissed += (s, e) =>
            {
                Dismissed?.Invoke(this, EventArgs.Empty);
            };

            _items = new Dictionary<ContextPopupItem, EContextPopupItem>();
        }

        /// <summary>
        /// ContextPopupRenderer class destructor.
        /// </summary>
        ~ContextPopupRenderer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dismisses the ContextPopup.
        /// </summary>
        public void Dismiss()
        {
            _popup.Dismiss();
        }

        /// <summary>
        /// Adds items to items dictionary.
        /// </summary>
        /// <param name="items">List of items to add.</param>
        public void AddItems(IEnumerable<ContextPopupItem> items)
        {
            foreach (var item in items)
            {
                item.PropertyChanged += ContextPopupItemPropertyChanged;
                AddItem(item);
            }
        }

        /// <summary>
        /// Removes items from items dictionary.
        /// </summary>
        /// <param name="items">List of items to remove.</param>
        public void RemoveItems(IEnumerable<ContextPopupItem> items)
        {
            foreach (var item in items)
            {
                item.PropertyChanged -= ContextPopupItemPropertyChanged;
                if (_items.ContainsKey(item))
                {
                    var nativeItem = _items[item];
                    nativeItem.Delete();
                    _items.Remove(item);
                }
            }
        }

        /// <summary>
        /// Clears items dictionary.
        /// </summary>
        public void ClearItems()
        {
            foreach (var item in _items.Keys)
            {
                item.PropertyChanged -= ContextPopupItemPropertyChanged;
            }

            _items.Clear();
            _popup.Clear();
        }

        /// <summary>
        /// Shows the ContextPopup. The ContextPopup is positioned at the horizontal and the vertical position
        /// of a specific anchor with offsets.
        /// </summary>
        /// <param name="anchor">The view to which the popup should be anchored.</param>
        /// <param name="xAnchorOffset">The horizontal offset from the anchor.</param>
        /// <param name="yAnchorOffset">The vertical offset from the anchor.</param>
        public void Show(Xamarin.Forms.View anchor, int xAnchorOffset, int yAnchorOffset)
        {
            var geometry = XFPlatformTizen.Platform.GetRenderer(anchor).NativeView.Geometry;
            _popup.Move(geometry.X + xAnchorOffset, geometry.Y + yAnchorOffset);
            _popup.Show();
        }

        /// <summary>
        /// Gets the direction of the ContextPopup if it is shown.
        /// This method returns false if it is not shown and the output argument is a default value.
        /// </summary>
        /// <param name="direction">The direction of the ContextPopup.</param>
        /// <returns>True if the ContextPopup is shown, false otherwise.</returns>
        public bool TryGetContextPopupDirection(out ContextPopupDirection direction)
        {
            var nativeDirection = _popup.Direction;
            if (nativeDirection != EContextPopupDirection.Unknown)
            {
                direction = (ContextPopupDirection)nativeDirection;
                return true;
            }
            else
            {
                direction = default(ContextPopupDirection);
                return false;
            }
        }

        /// <summary>
        /// Updates label of a given item.
        /// </summary>
        /// <param name="item">Item with a new label.</param>
        public void UpdateContextPopupItemLabel(ContextPopupItem item)
        {
            EContextPopupItem nativeItem = _items[item];
            nativeItem.SetPartText("default", item.Label);
        }

        /// <summary>
        /// Updates icon of a given item.
        /// </summary>
        /// <param name="item">Item with a new icon.</param>
        public void UpdateContextPopupItemIcon(ContextPopupItem item)
        {
            if (string.IsNullOrEmpty(item.Icon))
            {
                _items[item]?.SetPartContent("icon", null);
            }
            else
            {
                AppendOrModifyItemWithIcon(item);
            }
        }

        /// <summary>
        /// Calls method to release unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged resources used by ContextPopup.
        /// </summary>
        /// <param name="isDisposing">Flag indicating whether process of disposing lasts or not.</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (isDisposing)
            {
                if (_popup != null)
                {
                    _popup.Unrealize();
                    _popup = null;
                }
            }

            _isDisposed = true;
        }

        /// <summary>
        /// Invokes PropertyChanged with automatically obtained property name.
        /// </summary>
        /// <param name="propertyName">Name of the changed property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Updates label or icon of an item in ContextPopup items list.
        /// </summary>
        /// <param name="sender">Instance of the object which invokes event.</param>
        /// <param name="e">PropertyChanged event arguments.</param>
        void ContextPopupItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = sender as ContextPopupItem;

            if (e.PropertyName == nameof(ContextPopupItem.Label))
            {
                // If the native item already has a label
                UpdateContextPopupItemLabel(item);
            }
            else if (e.PropertyName == nameof(ContextPopupItem.Icon))
            {
                // If the native item already has an icon
                UpdateContextPopupItemIcon(item);
            }
        }

        /// <summary>
        /// Updates directions of the ContextPopup.
        /// </summary>
        void UpdateDirectionPriorities()
        {
            _popup.SetDirectionPriorty(
                (EContextPopupDirection)_priorities.First,
                (EContextPopupDirection)_priorities.Second,
                (EContextPopupDirection)_priorities.Third,
                (EContextPopupDirection)_priorities.Fourth);
        }

        /// <summary>
        /// Updates option of auto hide of the ContextPopup.
        /// </summary>
        void UpdateIsAutoHidingEnabled()
        {
            _popup.AutoHide = IsAutoHidingEnabled;
        }

        /// <summary>
        /// Adds ContextPopupItem to items dictionary.
        /// </summary>
        /// <param name="item">Item to add to items dictionary.</param>
        void AddItem(ContextPopupItem item)
        {
            if (_items.ContainsKey(item))
            {
                return;
            }

            EContextPopupItem nativeItem;
            if (string.IsNullOrEmpty(item.Icon))
            {
                nativeItem = _popup.Append(item.Label);
            }
            else
            {
                nativeItem = AppendOrModifyItemWithIcon(item);
            }

            _items.Add(item, nativeItem);

            nativeItem.Selected += (s, e) =>
            {
                SelectedItem = item; // This will invoke SelectedIndexChanged if the index has changed
                ItemSelected?.Invoke(this, EventArgs.Empty);
            };
        }

        /// <summary>
        /// Creates new item with icon.
        /// </summary>
        /// <param name="item">New item to set.</param>
        /// <returns>Item with updated values.</returns>
        EContextPopupItem AppendOrModifyItemWithIcon(ContextPopupItem item)
        {
            EContextPopupItem nativeItem = null;
            EIcon icon = new EIcon(_popup);
            icon.StandardIconName = item.Icon;
            if (!string.IsNullOrEmpty(icon.StandardIconName))
            {
                if (!_items.ContainsKey(item))
                {
                    nativeItem = _popup.Append(item.Label, icon);
                }
                else
                {
                    _items[item].SetPartContent("icon", icon);
                }
            }
            else
            {
                //Not a standard icon
                XFPlatformTizen.Native.Image iconImage = new XFPlatformTizen.Native.Image(_popup);
                var task = iconImage.LoadFromImageSourceAsync(item.Icon);
                if (!_items.ContainsKey(item))
                {
                    nativeItem = _popup.Append(item.Label, iconImage);
                }
                else
                {
                    _items[item].SetPartContent("icon", iconImage);
                }
            }

            return nativeItem;
        }

        #endregion
    }
}