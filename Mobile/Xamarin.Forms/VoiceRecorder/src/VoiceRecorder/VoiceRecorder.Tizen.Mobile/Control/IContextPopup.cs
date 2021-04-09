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

namespace VoiceRecorder.Tizen.Mobile.Control
{
    /// <summary>
    /// The ContextPopup interface.
    /// </summary>
    public interface IContextPopup
    {
        #region properties

        /// <summary>
        /// Occurs when a ContextPopupItem is selected.
        /// </summary>
        event EventHandler ItemSelected;

        /// <summary>
        /// Occurs when the ContextPopup is dismissed.
        /// </summary>
        event EventHandler Dismissed;

        /// <summary>
        /// Gets or sets the selected item of the ContextPopup.
        /// </summary>
        ContextPopupItem SelectedItem { get; set; }

        /// <summary>
        /// Gets or sets the direction priorities for the ContextPopup.
        /// </summary>
        ContextPopupDirectionPriorities DirectionPriorities { get; set; }

        /// <summary>
        /// Gets or sets whether ContextPopup should be hidden automatically
        /// or not when parent of ContextPopup is resized.
        /// </summary>
        bool IsAutoHidingEnabled { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Adds items.
        /// </summary>
        /// <param name="items">List of items of the ContextPopup to add.</param>
        void AddItems(IEnumerable<ContextPopupItem> items);

        /// <summary>
        /// Removes items.
        /// </summary>
        /// <param name="items">List of items of the ContextPopup to remove.</param>
        void RemoveItems(IEnumerable<ContextPopupItem> items);

        /// <summary>
        /// Clears items.
        /// </summary>
        void ClearItems();

        /// <summary>
        /// Shows the ContextPopup. The ContextPopup is positioned at the horizontal and the vertical position
        /// of a specific anchor with offsets.
        /// </summary>
        /// <param name="anchor">The view to which the popup should be anchored.</param>
        /// <param name="xAnchorOffset">The horizontal offset from the anchor.</param>
        /// <param name="yAnchorOffset">The vertical offset from the anchor.</param>
        void Show(Xamarin.Forms.View anchor, int xAnchorOffset, int yAnchorOffset);

        /// <summary>
        /// Dismisses the ContextPopup.
        /// </summary>
        void Dismiss();

        /// <summary>
        /// Gets the direction of the ContextPopup if it is shown.
        /// This method returns false if it is not shown and the output argument is a default value.
        /// </summary>
        /// <param name="direction">The direction of the ContextPopup.</param>
        /// <returns>True if the ContextPopup is shown, false otherwise.</returns>
        bool TryGetContextPopupDirection(out ContextPopupDirection direction);

        #endregion
    }
}