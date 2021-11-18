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
using System.Collections;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImageGallery.Tizen.Mobile.Controls
{
    /// <summary>
    /// The control class which displays grid using items source and specified number of columns.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataGridControl : ContentView
    {
        #region fields

        /// <summary>
        /// Grid spacing value.
        /// </summary>
        private const int GRID_SPACING = 2;

        /// <summary>
        /// Internal instance of the Xamarin grid.
        /// </summary>
        private Grid _grid;

        /// <summary>
        /// Previous grid size.
        /// </summary>
        private int _previousGridSize;

        /// <summary>
        /// Current grid size.
        /// </summary>
        private int _currentGridSize;

        #endregion

        #region properties

        /// <summary>
        /// Number of grid columns.
        /// </summary>
        public int Cols { set; get; }

        /// <summary>
        /// Item template bindable property definition.
        /// </summary>
        public static BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate",
            typeof(DataTemplate), typeof(DataGridControl), default(DataTemplate));

        /// <summary>
        /// Data template to define the visual appearance of objects from items source.
        /// </summary>
        public DataTemplate ItemTemplate
        {
            set => SetValue(ItemTemplateProperty, value);
            get => (DataTemplate)GetValue(ItemTemplateProperty);
        }

        /// <summary>
        /// Items source bindable property definition.
        /// </summary>
        public static BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource",
            typeof(IList), typeof(DataGridControl), default(IList));

        /// <summary>
        /// Source of items to template and display.
        /// </summary>
        public IList ItemsSource
        {
            set => SetValue(ItemsSourceProperty, value);
            get => (IList)GetValue(ItemsSourceProperty);
        }

        #endregion

        #region methods

        /// <summary>
        /// The control constructor.
        /// </summary>
        public DataGridControl()
        {
            InitializeComponent();
        }

        /// Handles change of control's properties.
        /// Updates grid content in case of items source and item template change.
        /// <param name="propertyName">Name of changed property.</param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName != ItemsSourceProperty.PropertyName && propertyName != ItemTemplateProperty.PropertyName)
            {
                return;
            }

            if (ItemsSource == null || ItemTemplate == null)
            {
                return;
            }

            CreateGrid(ItemsSource);

            if (ItemsSource is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)ItemsSource).CollectionChanged += OnItemsSourceChanged;
            }

            _previousGridSize = ItemsSource.Count;
        }

        /// <summary>
        /// Handles "CollectionChanged" event of the items source.
        /// Updates control's content by removing items or recreating internal grid.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnItemsSourceChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _currentGridSize = ItemsSource.Count;

            if (_previousGridSize == _currentGridSize)
            {
                return;
            }

            if (_previousGridSize > _currentGridSize)
            {
                RemoveFromGrid(ResolveRemovedItem(ItemsSource));
            }
            else
            {
                CreateGrid(ItemsSource);
            }

            _previousGridSize = _currentGridSize;
        }

        /// <summary>
        /// Obtains and returns item to be removed from the grid.
        /// </summary>
        /// <param name="gridData">Grid items source.</param>
        /// <returns>Returns grid's child to be removed.</returns>
        private View ResolveRemovedItem(IList gridData)
        {
            foreach (View child in _grid.Children)
            {
                var item = child.BindingContext;

                if (!gridData.Contains(item))
                {
                    return child;
                }
            }

            return null;
        }

        /// <summary>
        /// Removes view item from the grid.
        /// </summary>
        /// <param name="child">Grid's child to be removed.</param>
        private void RemoveFromGrid(View child)
        {
            if (child == null)
            {
                return;
            }

            var index = _grid.Children.IndexOf(child);

            _grid.Children.Remove(child);

            var count = _grid.Children.Count;

            for (int i = index; i < count; i++)
            {
                _grid.Children.Add(_grid.Children[i], i % Cols, i / Cols);
            }

            int rowsNeeded = (count % Cols == 0) ? count / Cols : count / Cols + 1;
            int rowsExisting = _grid.RowDefinitions.Count;

            if (rowsExisting > rowsNeeded)
            {
                _grid.RowDefinitions.RemoveAt(rowsExisting - 1);
            }
        }

        /// <summary>
        /// Creates grid using items source and item template.
        /// </summary>
        /// <param name="gridData">Grid items source.</param>
        private void CreateGrid(IList gridData)
        {
            if (gridData == null || gridData.Count == 0)
            {
                return;
            }

            int itemsNumber = gridData.Count;
            int rowHeight = ((720 - ((Cols - 1) * GRID_SPACING)) / Cols);
            int rowNumber = CountRowNumber(itemsNumber);
            int col = 0;
            int row = 0;

            _grid = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowSpacing = GRID_SPACING,
                ColumnSpacing = GRID_SPACING
            };
            _grid.BindingContext = gridData;

            for (int i = 0; i < Cols; i += 1)
            {
                _grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < rowNumber; i += 1)
            {
                _grid.RowDefinitions.Add(new RowDefinition
                {
                    Height = rowHeight
                });
            }

            for (int i = 0; i < itemsNumber; i += 1)
            {
                col = i % Cols;
                row = i / Cols;

                View view = (View)ItemTemplate.CreateContent();
                view.BindingContext = gridData[i];

                _grid.Children.Add(view, col, row);
            }

            this.Content = _grid;
        }

        /// <summary>
        /// Calculates and returns number of the grid rows
        /// based on the items number and number of the grid cols.
        /// </summary>
        /// <param name="itemsNumber">Number of grid items.</param>
        /// <returns>Number of the grid rows.</returns>
        private int CountRowNumber(int itemsNumber)
        {
            int rows;

            try
            {
                rows = (int)Math.Ceiling((double)itemsNumber / Cols);
            }
            catch (DivideByZeroException)
            {
                rows = 0;
            }

            return rows;
        }

        #endregion
    }
}