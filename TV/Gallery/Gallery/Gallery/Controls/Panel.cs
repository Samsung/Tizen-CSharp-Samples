using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Gallery.Controls
{
    public class Panel : ContentView
    {
        private ICommand _innerSelectedCommand;
        private readonly ScrollView _scrollView;
        private readonly Grid _itemsRootLayout;

        public event EventHandler SelectedItemChanged;

        /// <summary>
        /// Gets or sets the scrolling direction of the Panel.
        /// </summary>
        /// <remarks>
        /// If ScrollOrientation is Both, the number of columns in the Panel is fixed to the total area width / ItemsWidth.
        /// For height, it is expanded by the required size.
        /// </remarks>
        public ScrollOrientation ListOrientation { get; set; } = ScrollOrientation.Both;

        public int Spacing { get; set; } = 5;

        public bool IsAnimation { get; set; } = true;

        public bool AllowedFocused { get; set; } = true;

        public int ItemsWidth { get; set; } = 150;

        public int ItemsHeight { get; set; } = 150;

        public double ScrollX => _scrollView.ScrollX;

        public double ScrollY => _scrollView.ScrollY;

        public static readonly BindableProperty TappedCommandProperty =
            BindableProperty.Create("TappedCommand", typeof(ICommand), typeof(Panel), null);

        public static readonly BindableProperty SelectedCommandProperty =
            BindableProperty.Create("SelectedCommand", typeof(ICommand), typeof(Panel), null);

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(Panel), default(IEnumerable<object>), BindingMode.TwoWay, propertyChanged: ItemsSourceChanged);

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create("SelectedItem", typeof(object), typeof(Panel), null, BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(Panel), default(DataTemplate));

        public ICommand TappedCommand
        {
            get { return (ICommand)GetValue(TappedCommandProperty); }
            set { SetValue(TappedCommandProperty, value); }
        }

        public ICommand SelectedCommand
        {
            get { return (ICommand)GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        private static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (Panel)bindable;
            itemsLayout.SetItems();
        }

        public Panel()
        {
            _scrollView = new ScrollView();
            _scrollView.Orientation = ListOrientation;
            _itemsRootLayout = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowSpacing = Spacing,
                ColumnSpacing = Spacing,
            };
            _scrollView.Content = _itemsRootLayout;
            Content = _scrollView;
        }

        protected virtual void SetItems()
        {
            _itemsRootLayout.Children.Clear();
            _itemsRootLayout.RowDefinitions.Clear();
            _itemsRootLayout.ColumnDefinitions.Clear();

            _itemsRootLayout.RowSpacing = Spacing;
            _itemsRootLayout.ColumnSpacing = Spacing;

            _innerSelectedCommand = new Command<View>(async view =>
            {
                if (IsAnimation)
                {
                    double targetScale = view.Scale;
                    await view.ScaleTo(targetScale * 0.9, 100);
                    await view.ScaleTo(1, 100);
                }

                if (TappedCommand?.CanExecute(view.BindingContext) ?? false)
                {
                    TappedCommand?.Execute(view.BindingContext);
                }

                SelectedItem = view.BindingContext;
            });

            if (ItemsSource == null)
            {
                return;
            }

            Device.StartTimer(TimeSpan.Zero, () =>
            {
                int rowCount = Height == -1 ? 1 : (int)Height / (ItemsHeight + Spacing);
                int colCount = Width == -1 ? 1 : (int)Width / (ItemsWidth + Spacing);

                if (ListOrientation == ScrollOrientation.Both)
                {
                    for (int i = 0; i < rowCount; i++)
                    {
                        _itemsRootLayout.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
                    }

                    for (int i = 0; i < colCount; i++)
                    {
                        _itemsRootLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                    }
                }

                int startIndex = 0;

                foreach (var item in ItemsSource)
                {
                    if (ListOrientation == ScrollOrientation.Horizontal)
                    {
                        _itemsRootLayout.Children.Add(GetItemView(item), startIndex, 0);
                    }
                    else if (ListOrientation == ScrollOrientation.Vertical)
                    {
                        _itemsRootLayout.Children.Add(GetItemView(item), 0, startIndex);
                    }
                    else
                    {
                        _itemsRootLayout.Children.Add(GetItemView(item), startIndex % colCount, startIndex == 0 ? 0 : startIndex / colCount);
                    }
                    startIndex++;
                }

                SelectedItem = null;

                return false;
            });
        }

        protected virtual View GetItemView(object item)
        {
            var content = ItemTemplate.CreateContent();
            var view = content as View;

            if (view == null)
            {
                return null;
            }

            if (Device.Idiom == TargetIdiom.TV && AllowedFocused)
            {
                Grid tvGrid = new Grid();
                tvGrid.HeightRequest = ItemsHeight;
                tvGrid.WidthRequest = ItemsWidth;
                tvGrid.BindingContext = item;

                tvGrid.Children.Add(view);

                var focusBtn = new Button
                {
                    BackgroundColor = Color.Transparent,
                    Opacity = 0.3,
                    Command = _innerSelectedCommand,
                    CommandParameter = view,
                };
                focusBtn.Focused += (s, e) => tvGrid.Scale = 0.95;
                focusBtn.Unfocused += (s, e) => tvGrid.Scale = 1;

                tvGrid.Children.Add(focusBtn);

                return tvGrid;
            }
            else
            {
                view.HeightRequest = ItemsHeight;
                view.WidthRequest = ItemsWidth;
                view.BindingContext = item;
                var gesture = new TapGestureRecognizer
                {
                    Command = _innerSelectedCommand,
                    CommandParameter = view
                };

                AddGesture(view, gesture);
            }

            return view;
        }

        private void AddGesture(View view, TapGestureRecognizer gesture)
        {
            view.GestureRecognizers.Add(gesture);

            var layout = view as Layout<View>;

            if (layout == null)
            {
                return;
            }

            foreach (var child in layout.Children)
            {
                AddGesture(child, gesture);
            }
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsView = (Panel)bindable;
            if (newValue == oldValue && newValue != null)
            {
                return;
            }

            itemsView.SelectedItemChanged?.Invoke(itemsView, EventArgs.Empty);

            if (itemsView.SelectedCommand?.CanExecute(newValue) ?? false)
            {
                itemsView.SelectedCommand?.Execute(newValue);
            }
        }

        public void ScrollTo(object data, ScrollToPosition position, bool animation)
        {
            var view = _itemsRootLayout.Children.FirstOrDefault(v => v.BindingContext == data);

            if (view != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await _scrollView.ScrollToAsync(view, position, animation);
                });
            }
        }

        public void ScrollTo(double scrollX, double scrollY, bool animation)
        {
            if (ScrollX < 0 || ScrollY < 0)
                return;

            Device.BeginInvokeOnMainThread(async () =>
            {
                await _scrollView.ScrollToAsync(scrollX, scrollY, animation);
            });
        }
    }
}
