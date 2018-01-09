/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
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
using System.Collections.Generic;
using System.ComponentModel;
using Weather.Tizen.Mobile.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(CarouselView), typeof(CarouselViewRenderer))]

namespace Weather.Tizen.Mobile.Renderers
{
    public class CarouselViewRenderer : ViewRenderer<CarouselView, ElmSharp.Scroller>
    {
        #region fields

        /// <summary>
        /// The children views.
        /// </summary>
        protected List<View> Children = new List<View>();

        /// <summary>
        /// The container for children native views.
        /// </summary>
        private ElmSharp.Box _box;

        /// <summary>
        /// The width of the single view.
        /// </summary>
        private int _pageWidth;

        /// <summary>
        /// The height of the single view.
        /// </summary>
        private int _pageHeight;

        /// <summary>
        /// The index of the currently displayed view.
        /// </summary>
        private int _pageIndex;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets item view controller.
        /// </summary>
        private IItemViewController ItemsController => Element;

        /// <summary>
        /// Gets or sets carousel view controller.
        /// </summary>
        private ICarouselViewController Controller => Element;

        #endregion

        #region methods

        /// <summary>
        /// Invoked on element changes.
        /// </summary>
        /// <param name="e">Event parameters.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<CarouselView> e)
        {
            if (Control == null)
            {
                _box = new ElmSharp.Box(Forms.Context.MainWindow) {IsHorizontal = true};

                var scroller = new ElmSharp.Scroller(Forms.Context.MainWindow)
                {
                    HorizontalScrollBarVisiblePolicy = ElmSharp.ScrollBarVisiblePolicy.Invisible,
                    VerticalScrollBarVisiblePolicy = ElmSharp.ScrollBarVisiblePolicy.Invisible,
                    HorizontalPageScrollLimit = 1,
                };
                scroller.SetContent(_box);
                scroller.PageScrolled += OnScrolled;
                scroller.Resized += OnResized;
                SetNativeControl(scroller);
            }

            if (e.NewElement != null)
            {
                UpdateContent();
                Controller.CollectionChanged += OnCollectionChanged;
            }

            base.OnElementChanged(e);
        }

        /// <summary>
        /// Disposes resources used by this class.
        /// </summary>
        /// <param name="disposing">True if the memory release was requested on demand.</param>
        protected override void Dispose(bool disposing)
        {
            Controller.CollectionChanged -= OnCollectionChanged;
            foreach (var child in Children)
            {
                Platform.GetRenderer(child)?.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Invoked on collection changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCollectionChanged(object sender,
            System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateContent();
        }

        /// <summary>
        /// Handles the size changes.
        /// </summary>
        void OnResized(object sender, EventArgs e)
        {
            var g = NativeView.Geometry;
            _pageWidth = g.Width;
            _pageHeight = g.Height;
            UpdateChildrenSize();
        }

        /// <summary>
        /// Handles the PageScrolled event of the scroller.
        /// </summary>
        void OnScrolled(object sender, EventArgs e2)
        {
            var previousIndex = _pageIndex;
            _pageIndex = Control.HorizontalPageIndex;
            if (_pageIndex == previousIndex)
            {
                return;
            }
            Controller.SendSelectedPositionChanged(_pageIndex);
            Controller.SendSelectedItemChanged(Controller.GetItem(_pageIndex));
        }

        /// <summary>
        /// Updates the size of all the children.
        /// </summary>
        private void UpdateChildrenSize()
        {
            Control.SetPageSize(_pageWidth, _pageHeight);
            var bounds = new Rectangle(0, 0, _pageWidth, _pageHeight);
            foreach (var child in Children)
            {
                var renderer = Platform.GetRenderer(child);
                renderer.Element.Layout(bounds);
                var nativeChild = renderer.NativeView;
                if (nativeChild != null)
                {
                    nativeChild.MinimumWidth = _pageWidth;
                    nativeChild.MinimumHeight = _pageHeight;
                }
            }
            Control.ScrollTo(new ElmSharp.Rect(_pageIndex * _pageWidth, 0, _pageWidth, _pageHeight), false);
        }

        /// <summary>
        /// Create renderers of the children views and arrange them.
        /// </summary>
        private void UpdateContent()
        {
            foreach (var child in Children)
                Platform.GetRenderer(child)?.Dispose();

            _box.UnPackAll();
            Children.Clear();
            if (Element.ItemsSource != null)
            {
                foreach (var item in Element.ItemsSource)
                {
                    var view = CreateItemView(item);
                    Children.Add(view);
                    var renderer = Platform.GetOrCreateRenderer(view);
                    _box.PackEnd(renderer?.NativeView);
                }
            }
        }

        /// <summary>
        /// Invoked on element properties changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CarouselView.PositionProperty.PropertyName &&
                !Controller.IgnorePositionUpdates)
            {
                if (_pageIndex != Element.Position)
                {
                    _pageIndex = Element.Position;
                    Control.ScrollTo(new ElmSharp.Rect(_pageIndex * _pageWidth, 0, _pageWidth, _pageHeight), true);
                    (Element as IElementController).SetValueFromRenderer(CarouselView.ItemProperty,
                        ItemsController.GetItem(_pageIndex));
                }
            }
            else if (e.PropertyName == CarouselView.ItemProperty.PropertyName)
            {
                var previousIndex = _pageIndex;
                _pageIndex = 0;
                var selectedItem = Element.Item;
                if (selectedItem == null && ItemsController.GetItem(Element.Position) == null)
                {
                    _pageIndex = Element.Position;
                }
                else
                {
                    var index = 0;

                    foreach (var item in Element.ItemsSource)
                    {
                        if (selectedItem != null && selectedItem.Equals(item))
                        {
                            _pageIndex = index;
                            break;
                        }
                        ++index;
                    }
                }

                (Element as IElementController).SetValueFromRenderer(CarouselView.PositionProperty, _pageIndex);

                if (_pageIndex != previousIndex)
                {
                    Control.ScrollTo(new ElmSharp.Rect(_pageIndex * _pageWidth, 0, _pageWidth, _pageHeight), true);
                }
            }
            else if (e.PropertyName == ItemsView.ItemsSourceProperty.PropertyName)
            {
                UpdateContent();
                if (_pageHeight > 0 && _pageWidth > 0)
                {
                    UpdateChildrenSize();
                }
            }
        }

        /// <summary>
        /// Creates the item's view.
        /// </summary>
        /// <returns>The View bound to the given item.</returns>
        /// <param name="item">Item of the ItemsSource.</param>
        private View CreateItemView(object item)
        {
            var view = ItemsController.CreateView(ItemsController.GetItemType(item));
            view.Parent = Element;
            ItemsController.BindView(view, item);
            view.Layout(new Rectangle(0, 0, Element.Width, Element.Height));
            return view;
        }

        #endregion
    }
}