/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
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

using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicPlayer.Control
{
    /// <summary>
    /// Vertically scrollable grid.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScrollableGrid : ContentView
    {
        #region properties

        /// <summary>
        /// Allows to set number of rows in list.
        /// </summary>
        public static readonly BindableProperty RowsProperty = BindableProperty.Create(
                                                nameof(Rows),
                                                typeof(int),
                                                typeof(ScrollableGrid),
                                                0,
                                                BindingMode.OneWay,
                                                propertyChanged: OnPropertyChanged);

        /// <summary>
        /// Gets or sets number of rows in list.
        /// </summary>
        public int Rows
        {
            get => (int)GetValue(RowsProperty);
            set => SetValue(RowsProperty, value);
        }

        /// <summary>
        /// Allows to set width of single item.
        /// </summary>
        public static readonly BindableProperty WidthOfItemProperty = BindableProperty.Create(
                                                nameof(WidthOfItem),
                                                typeof(int),
                                                typeof(ScrollableGrid),
                                                0,
                                                BindingMode.OneWay,
                                                propertyChanged: OnPropertyChanged);

        /// <summary>
        /// Gets or sets width of single item.
        /// </summary>
        public int WidthOfItem
        {
            get => (int)GetValue(WidthOfItemProperty);
            set => SetValue(WidthOfItemProperty, value);
        }

        /// <summary>
        /// Allows to set height of single item.
        /// </summary>
        public static readonly BindableProperty HeightOfItemProperty = BindableProperty.Create(
                                                nameof(HeightOfItem),
                                                typeof(int),
                                                typeof(ScrollableGrid),
                                                0,
                                                BindingMode.OneWay,
                                                propertyChanged: OnPropertyChanged);

        /// <summary>
        /// Gets or sets height of single item.
        /// </summary>
        public int HeightOfItem
        {
            get => (int)GetValue(HeightOfItemProperty);
            set => SetValue(HeightOfItemProperty, value);
        }

        /// <summary>
        /// Allows to set space between lists.
        /// </summary>
        public static readonly BindableProperty SpaceBetweenColumnsProperty = BindableProperty.Create(
                                                nameof(SpaceBetweenColumns),
                                                typeof(int),
                                                typeof(ScrollableGrid),
                                                0,
                                                BindingMode.OneWay,
                                                propertyChanged: OnPropertyChanged);

        /// <summary>
        /// Gets or sets space between lists.
        /// </summary>
        public int SpaceBetweenColumns
        {
            get => (int)GetValue(SpaceBetweenColumnsProperty);
            set => SetValue(SpaceBetweenColumnsProperty, value);
        }

        /// <summary>
        /// Allows to set the item source.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
                                                nameof(ItemsSource),
                                                typeof(IEnumerable),
                                                typeof(ScrollableGrid),
                                                default(IEnumerable),
                                                BindingMode.OneWay,
                                                propertyChanged: OnPropertyChanged);

        /// <summary>
        /// Gets or sets the item source.
        /// </summary>
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// Allows to set data template for item.
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
                                                nameof(ItemTemplate),
                                                typeof(DataTemplate),
                                                typeof(ScrollableGrid),
                                                null,
                                                propertyChanged: OnPropertyChanged);

        /// <summary>
        /// Gets or sets data template for item.
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        /// <summary>
        /// Allows to set command to invoke when the grid item is pressed.
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
                                                nameof(Command),
                                                typeof(ICommand),
                                                typeof(ScrollableGrid),
                                                null,
                                                propertyChanged: OnPropertyChanged);

        /// <summary>
        /// The command to invoke when grid item is pressed.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Gets the lists of items.
        /// </summary>
        public List<List<object>> DividedList { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ScrollableGrid()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the property changed event for properties.
        /// </summary>
        /// <param name="bindable">BindableObject which raised the event.</param>
        /// <param name="oldValue">Old value of property.</param>
        /// <param name="newValue">New value of property</param>
        private static void OnPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ScrollableGrid scrollableGrid = (ScrollableGrid)bindable;
            scrollableGrid.DivideItemsIntoColumns();
            if (scrollableGrid.ItemTemplate != null)
            {
                scrollableGrid.GenerateGrid();
            }
        }

        /// <summary>
        /// Divides the items source into lists of specified length.
        /// </summary>
        private void DivideItemsIntoColumns()
        {
            int i = 0;
            DividedList = new List<List<object>>();
            if (ItemsSource != null)
            {
                foreach (var t in ItemsSource)
                {
                    if (Rows != 0)
                    {
                        if (i % Rows == 0)
                        {
                            DividedList.Add(new List<object>());
                        }

                        DividedList[i / Rows].Add(t);
                    }

                    i++;
                }
            }
        }

        /// <summary>
        /// Displays the grid.
        /// </summary>
        private void GenerateGrid()
        {
            int x = 0;
            int index = 0;
            Grid grid = new Grid();
            for (int i = 0; i < Rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = HeightOfItem });
            }

            grid.RowSpacing = 2;
            scroll.Content = grid;
            foreach (var t in DividedList)
            {
                int y = 0;
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = WidthOfItem });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = SpaceBetweenColumns });
                foreach (var item in t)
                {
                    Button btn = new Button();
                    btn.HeightRequest = HeightOfItem;
                    btn.WidthRequest = WidthOfItem;
                    btn.Opacity = 0;
                    btn.Command = Command;
                    btn.CommandParameter = index;

                    View cell = ItemTemplate.CreateContent() as View;
                    cell.BindingContext = item;

                    grid.Children.Add(cell);
                    Grid.SetRow(cell, y);
                    Grid.SetColumn(cell, x);

                    grid.Children.Add(btn);
                    Grid.SetRow(btn, y);
                    Grid.SetColumn(btn, x);

                    btn.Focused += (s, e) =>
                    {
                        btn.Opacity = 0.1;
                    };

                    btn.Unfocused += (s, e) =>
                    {
                        btn.Opacity = 0;
                    };

                    y++;
                    index++;
                }

                x += 2;
            }
        }

        #endregion
    }
}