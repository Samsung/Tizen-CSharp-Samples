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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms;

namespace VoiceRecorder.Tizen.Mobile.Control
{
    /// <summary>
    /// The ContextPopup class allows a contextual popup to be anchored at a view.
    /// </summary>
    /// <example>
    /// <code>
    /// ContextPopup popup = new ContextPopup
    /// {
    ///     DirectionPriorities = new ContextPopupDirectionPriorities(ContextPopupDirection.Down, ContextPopupDirection.Right, ContextPopupDirection.Left, ContextPopupDirection.Up),
    /// };
    /// popup.Items.Add(new ContextPopupItem("Text only item"));
    /// popup.Items.Add(new ContextPopupItem("Home icon", "home"));
    /// popup.Items.Add(new ContextPopupItem("Chat", StandardIconResource.MenuChat.Name));
    /// popup.SelectedIndexChanged += (s, e) =>
    /// {
    ///     var ctxPopup = s as ContextPopup;
    ///     Debug.WriteLine("Item with index {0} selected", ctxPopup.SelectedIndex);
    ///     Debug.WriteLine("It has label: " + (ctxPopup.SelectedItem as ContextPopupItem).Label);
    /// };
    ///
    /// Button btn = new Button
    /// {
    ///     Text = "Toggle popup"
    /// };
    /// btn.Clicked += (s, e) =>
    /// {
    ///     popup.Show(s as Button);
    /// };
    /// </code>
    /// </example>
    public class ContextPopup : BindableObject
    {
        #region fields

        /// <summary>
        /// Instance of IContextPopup.
        /// </summary>
        IContextPopup _contextPopup;

        /// <summary>
        /// Backing field of Items property.
        /// </summary>
        ObservableCollection<ContextPopupItem> _items;

        /// <summary>
        /// Backing field of DirectionPriorities property.
        /// </summary>
        static ContextPopupDirectionPriorities _priorities =
            new ContextPopupDirectionPriorities(ContextPopupDirection.Up, ContextPopupDirection.Left,
                ContextPopupDirection.Right, ContextPopupDirection.Down);

        #endregion

        #region properties

        /// <summary>
        /// BindableProperty. Identifies the Orientation bindable property.
        /// </summary>
        [Obsolete("OrientationProperty is obsolete as of version 2.3.5-r256-001. The orientation is always vertical.")]
        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create(nameof(Orientation), typeof(ContextPopupOrientation), typeof(ContextPopup),
                defaultValue: ContextPopupOrientation.Vertical);

        /// <summary>
        /// BindableProperty. Identifies the IsAutoHidingEnabled bindable property.
        /// </summary>
        public static readonly BindableProperty IsAutoHidingEnabledProperty =
            BindableProperty.Create(nameof(IsAutoHidingEnabled), typeof(bool), typeof(ContextPopup),
                defaultValue: true);

        /// <summary>
        /// BindableProperty. Identifies the DirectionPriorities bindable property.
        /// </summary>
        public static readonly BindableProperty DirectionPrioritiesProperty =
            BindableProperty.Create(nameof(DirectionPriorities), typeof(ContextPopupDirectionPriorities),
                typeof(ContextPopup), defaultValue: _priorities);

        /// <summary>
        /// BindableProperty. Identifies the SelectedIndex bindable property.
        /// </summary>
        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(ContextPopup), defaultValue: -1,
                propertyChanged: OnSelectedIndexChanged, coerceValue: CoerceSelectedIndex);

        /// <summary>
        /// BindableProperty. Identifies the SelectedItem bindable property.
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem),typeof(object), typeof(ContextPopup), null,
                propertyChanged: OnSelectedItemChanged);

        /// <summary>
        /// BindableProperty. Identifies the ItemsSource bindable property.
        /// </summary>
        [Obsolete("ItemsSourceProperty is obsolete as of version 2.3.5-r256-001. Please use Items instead.")]
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(ContextPopup), default(IList));

        /// <summary>
        /// Occurs when the ContextPopup is dismissed.
        /// </summary>
        public event EventHandler Dismissed;

        /// <summary>
        /// Occurs when the index of the selected ContextPopupItem changes.
        /// </summary>
        public event EventHandler SelectedIndexChanged;

        /// <summary>
        /// Occurs when a ContextPopupItem is selected.
        /// </summary>
        public event EventHandler ItemSelected;

        /// <summary>
        /// Gets or sets the orientation of the ContextPopup.
        /// </summary>
        [Obsolete("Orientation is obsolete as of version 2.3.5-r256-001. The orientation is always vertical.")]
        public ContextPopupOrientation Orientation
        {
            get { return (ContextPopupOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// Gets or sets whether ContextPopup should be hidden automatically
        /// or not when parent of ContextPopup is resized.
        /// </summary>
        /// <remarks>
        /// Setting IsAutoHidingEnabled to false will not be dismissed automatically wheneversuch as mouse
        /// clicked its background area, language is changed, and its parent geometry is updated(changed).
        /// </remarks>
        public bool IsAutoHidingEnabled
        {
            get { return (bool)GetValue(IsAutoHidingEnabledProperty); }
            set { SetValue(IsAutoHidingEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets the direction priorities for the ContextPopup.
        /// The position of the ContextPopup depends on the available space.
        /// Therefore, DirectionPriorities are used to specify desired first, second, third, and fourth
        /// priorities for positioning the ContextPopup.
        /// </summary>
        public ContextPopupDirectionPriorities DirectionPriorities
        {
            get { return (ContextPopupDirectionPriorities)GetValue(DirectionPrioritiesProperty); }
            set { SetValue(DirectionPrioritiesProperty, value); }
        }

        /// <summary>
        /// Gets or sets the index of the selected item of the ContextPopup.
        /// It is -1 when no item is selected.
        /// </summary>
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected item of the ContextPopup.
        /// </summary>
        public ContextPopupItem SelectedItem
        {
            get { return (ContextPopupItem)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets the list of items in the ContextPopup.
        /// </summary>
        [Obsolete("ItemsSource is obsolete as of version 2.3.5-r256-001. Please use Items instead.")]
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets the list of items in the ContextPopup.
        /// </summary>
        public IList<ContextPopupItem> Items
        {
            get { return _items; }
        }

        #endregion

        #region methods

        /// <summary>
        /// The constructor, which creates a new ContextPopup instance.
        /// </summary>
        public ContextPopup()
        {
            _contextPopup = DependencyService.Get<IContextPopup>(DependencyFetchTarget.NewInstance);

            _contextPopup.Dismissed += (s, e) => Dismissed?.Invoke(this, EventArgs.Empty);
            _contextPopup.ItemSelected += (s, e) => ItemSelected?.Invoke(this, EventArgs.Empty);

            _items = new ObservableCollection<ContextPopupItem>();
            _items.CollectionChanged += ItemsCollectionChanged;

            SetBinding(IsAutoHidingEnabledProperty, new Binding(nameof(IsAutoHidingEnabled), mode: BindingMode.TwoWay, source: _contextPopup));
            SetBinding(DirectionPrioritiesProperty, new Binding(nameof(DirectionPriorities), mode: BindingMode.TwoWay, source: _contextPopup));
            SetBinding(SelectedItemProperty, new Binding(nameof(SelectedItem), mode: BindingMode.TwoWay, source: _contextPopup));
        }

        /// <summary>
        /// Shows the ContextPopup. The ContextPopup is positioned at the horizontal and the vertical position of a specific anchor.
        /// </summary>
        /// <param name="anchor">The view to which the popup should be anchored.</param>
        public void Show(Xamarin.Forms.View anchor)
        {
            Show(anchor, 0, 0);
        }

        /// <summary>
        /// Shows the ContextPopup. The ContextPopup is positioned at the horizontal and the vertical position of a specific anchor with offsets.
        /// </summary>
        /// <param name="anchor">The view to which the popup should be anchored.</param>
        /// <param name="xOffset">The horizontal offset from the anchor.</param>
        /// <param name="yOffset">The vertical offset from the anchor.</param>
        public void Show(Xamarin.Forms.View anchor, int xOffset, int yOffset)
        {
            _contextPopup.Show(anchor, xOffset, yOffset);
        }

        /// <summary>
        /// Shows the ContextPopup. The ContextPopup is positioned at the horizontal and the vertical position
        /// of a specific anchor with offsets.
        /// </summary>
        /// <param name="anchor">The view to which the popup should be anchored.</param>
        /// <param name="xOffset">The horizontal offset from the anchor.</param>
        /// <param name="yOffset">The vertical offset from the anchor.</param>
        public void Show(Xamarin.Forms.View anchor, double xOffset, double yOffset)
        {
            Show(anchor, (int)xOffset, (int)yOffset);
        }

        /// <summary>
        /// Calls model to dismiss the ContextPopup.
        /// </summary>
        public void Dismiss()
        {
            _contextPopup.Dismiss();
        }

        /// <summary>
        /// Gets the direction of the ContextPopup if it is shown.
        /// This method returns false if it is not shown and the output argument is a default value.
        /// </summary>
        /// <param name="direction">The direction of the ContextPopup.</param>
        /// <returns>True if the ContextPopup is shown, false otherwise.</returns>
        public bool TryGetContextPopupDirection(out ContextPopupDirection direction)
        {
            direction = default(ContextPopupDirection);
            return _contextPopup.TryGetContextPopupDirection(out direction);
        }

        /// <summary>
        /// Calls method to add, remove or reset items in items collection
        /// depending on the type of event data.
        /// </summary>
        /// <param name="sender">Instance of the object which invokes event.</param>
        /// <param name="e">Contains event data.</param>
        void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddItems(e);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    RemoveItems(e);
                    break;

                default: // Move, replace, reset
                    ResetItems();
                    break;
            }

            SelectedIndex = SelectedIndex.Clamp(-1, Items.Count - 1);
            UpdateSelectedItem();
        }

        /// <summary>
        ///  Calls model to clear items.
        /// </summary>
        void ResetItems()
        {
            _contextPopup.ClearItems();
        }

        /// <summary>
        ///  Calls model to remove items.
        /// </summary>
        /// <param name="e">Contains event data.</param>
        void RemoveItems(NotifyCollectionChangedEventArgs e)
        {
            _contextPopup.RemoveItems(e.OldItems.OfType<ContextPopupItem>());
        }

        /// <summary>
        ///  Calls model to add items.
        /// </summary>
        /// <param name="e">Contains event data.</param>
        void AddItems(NotifyCollectionChangedEventArgs e)
        {
            _contextPopup.AddItems(e.NewItems.OfType<ContextPopupItem>());
        }

        /// <summary>
        /// Is called when the index of the selected ContextPopupItem changes.
        /// Handles "RecordingQualityUpdated" event of the VoiceRecorderModel class.
        /// Updates value of CurrentRecordingQuality property.
        /// </summary>
        /// <param name="bindable">Instance of ContextPopup.</param>
        /// <param name="oldValue">Old value of an item.</param>
        /// <param name="newValue">New value of an item.</param>
        static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var contextPopup = (ContextPopup)bindable;
            contextPopup.UpdateSelectedItem();
            contextPopup.SelectedIndexChanged?.Invoke(contextPopup, EventArgs.Empty);
        }

        /// <summary>
        /// Returns -1 if the list of items of ContextPopup is null.
        /// Otherwise, limits value of given index from -1 to number of elements in the list of items.
        /// </summary>
        /// <param name="bindable">Instance of ContextPopup.</param>
        /// <param name="value">Index of a selected item.</param>
        /// <returns>Returns -1 if the list of items of ContextPopup is null
        /// or cropped value of selected index.</returns>
        static object CoerceSelectedIndex(BindableObject bindable, object value)
        {
            var contextPopup = (ContextPopup)bindable;
            return contextPopup.Items == null ? -1 : ((int)value).Clamp(-1, contextPopup.Items.Count - 1);
        }

        /// <summary>
        /// Updates SelectedItem property with newly selected item
        /// or sets SelectedItem property to null if there is no selected item.
        /// </summary>
        void UpdateSelectedItem()
        {
            if (SelectedIndex == -1)
            {
                SelectedItem = null;
                return;
            }

            SelectedItem = Items[SelectedIndex];
        }

        /// <summary>
        /// Calls model to update SelectedIndex property with new value.
        /// </summary>
        /// <param name="bindable">Instance of ContextPopup.</param>
        /// <param name="oldValue">Previous value of SelectedIndex property.</param>
        /// <param name="newValue">New value of SelectedIndex property.</param>
        static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var contextPopup = (ContextPopup)bindable;
            contextPopup.UpdateSelectedIndex(newValue);
        }


        /// <summary>
        /// Updates SelectedIndex property with index of selected item.
        /// </summary>
        /// <param name="selectedItem">Selected item of a ContextPopup.</param>
        void UpdateSelectedIndex(object selectedItem)
        {
            SelectedIndex = Items.IndexOf((ContextPopupItem)selectedItem);
        }
    }

    #endregion

    /// <summary>
    /// The NumericExtensions class provides methods to operate on numbers.
    /// </summary>
    internal static class NumericExtensions
    {
        #region methods

        /// <summary>
        /// If the given value exceeds the range, it is cropped to the limited value.
        /// The range is specified by the min and max values.
        /// </summary>
        /// <param name="self">The value to crop.</param>
        /// <param name="min">The smallest possible value in the range.</param>
        /// <param name="max">The biggest possible value in the range.</param>
        /// <returns>Returns original value if it is inside the range, cropped value otherwise.</returns>
        public static int Clamp(this int self, int min, int max)
        {
            return Math.Min(max, Math.Max(self, min));
        }

        #endregion
    }
}
