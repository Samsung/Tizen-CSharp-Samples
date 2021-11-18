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
using System.Collections;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace ImageGallery.Tizen.Mobile.Controls
{
    /// <summary>
    /// The control class which allows to show dynamic list using scrollable stack layout.
    /// </summary>
    public class StackListControl : ContentView
    {
        #region fields

        /// <summary>
        /// Internal stack layout used as a placeholder for control items.
        /// </summary>
        private readonly StackLayout _internalStack;

        /// <summary>
        /// Previous items size.
        /// </summary>
        private int _previousItemsSize;

        /// <summary>
        /// Current timeline size.
        /// </summary>
        private int _currentItemsSize;

        #endregion

        #region properties

        /// <summary>
        /// Items source bindable property definition.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                "ItemsSource",
                typeof(IList),
                typeof(StackListControl),
                default(IList));

        // <summary>
        /// Item template bindable property definition.
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(
                "ItemTemplate",
                typeof(DataTemplate),
                typeof(StackListControl),
                default(DataTemplate));

        /// <summary>
        /// Source of items to template and display.
        /// </summary>
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Data template to define the visual appearance of objects from items source.
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        #endregion

        /// <summary>
        /// The control constructor.
        /// </summary>
        public StackListControl()
        {
            _internalStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Content = new ScrollView
            {
                Content = _internalStack
            };
        }

        /// <summary>
        /// Handles change of control's properties.
        /// Updates list content in case of items source and item template change.
        /// </summary>
        /// <param name="propertyName">Name of property which changed.</param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ItemsSourceProperty.PropertyName && ItemsSource != null)
            {
                if (ItemsSource is INotifyCollectionChanged)
                {
                    ((INotifyCollectionChanged)ItemsSource).CollectionChanged += OnItemsSourceChanged;
                }

                _previousItemsSize = ItemsSource.Count;

                if (ItemTemplate != null)
                {
                    CreateContent(ItemsSource);
                }
            }

            if (propertyName == ItemTemplateProperty.PropertyName)
            {
                if (ItemsSource != null)
                {
                    CreateContent(ItemsSource);
                }
            }
        }

        /// <summary>
        /// Handles "CollectionChanged" event of the ItemsSource property.
        /// Updates the control's content by adding or removing proper items.
        /// </summary>
        /// <param name="sender">Object invoking the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnItemsSourceChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!(sender is IList))
            {
                return;
            }

            var itemsList = (IList)sender;

            _currentItemsSize = itemsList.Count;

            if (_previousItemsSize == _currentItemsSize)
            {
                return;
            }

            if (_previousItemsSize > _currentItemsSize)
            {
                RemoveFromList(ResolveRemovedItem(itemsList));
            }
            else
            {
                CreateContent(itemsList);
            }

            _previousItemsSize = _currentItemsSize;
        }

        /// <summary>
        /// Obtains and returns item (view instance) to be removed from the list.
        /// </summary>
        /// <param name="data">New items source data.</param>
        /// <returns>Item to be removed.</returns>
        private View ResolveRemovedItem(IList data)
        {
            foreach (View child in _internalStack.Children)
            {
                var image = child.BindingContext;

                if (!data.Contains(image))
                {
                    return child;
                }
            }

            return null;
        }

        /// <summary>
        /// Removes view item from the list.
        /// </summary>
        /// <param name="viewItem">View element to be removed.</param>
        private void RemoveFromList(View viewItem)
        {
            if (viewItem == null)
            {
                return;
            }

            _internalStack.Children.Remove(viewItem);
        }

        /// <summary>
        /// Crates content of the control using provided items source and item template.
        /// </summary>
        /// <param name="items">List items source.</param>
        private void CreateContent(IList items)
        {
            if (items == null || items.Count == 0)
            {
                return;
            }

            if (ItemsSource == null)
            {
                return;
            }

            _internalStack.Children.Clear();

            foreach (var data in items)
            {
                View view = (View)ItemTemplate.CreateContent();
                if (view == null)
                {
                    return;
                }

                view.BindingContext = data;
                _internalStack.Children.Add(view);
            }
        }
    }
}
