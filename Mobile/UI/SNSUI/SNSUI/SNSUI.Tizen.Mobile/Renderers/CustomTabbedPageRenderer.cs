/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using ElmSharp;
using EToolbarItem = ElmSharp.ToolbarItem;
using EToolbarItemEventArgs = ElmSharp.ToolbarItemEventArgs;
using SNSUI.Extensions;
using SNSUI.Tizen.Renderers;
using System.Collections.Generic;
using System;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]

namespace SNSUI.Tizen.Renderers
{
    /// <summary>
    /// A class for a custom render to support additional functionality.
    /// </summary>
    public class CustomTabbedPageRenderer : VisualElementRenderer<CustomTabbedPage>, IVisualElementRenderer
    {
        /// <summary>
        /// A nested class that can store an index and a page pair.
        /// </summary>
        class PageInfo
        {
            public int Index;
            public Page Page;
        }

        Naviframe _navi;
        Box _box;
        Scroller _scroller;
        Toolbar _tpage;
        Dictionary<EToolbarItem, PageInfo> _pageInfo = new Dictionary<EToolbarItem, PageInfo>();

        public CustomTabbedPageRenderer()
        {
            RegisterPropertyHandler(TabbedPage.BarBackgroundColorProperty, UpdateBarBackgroundColor);
            RegisterPropertyHandler("CurrentPage", CurrentPageChanged);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CustomTabbedPage> e)
        {
            if (_tpage == null)
            {
                _navi = new Naviframe(Forms.Context.MainWindow);

                _tpage = new Toolbar(Forms.Context.MainWindow)
                {
                    Style = "tabbar",
                    Text = Element.Title,
                    ShrinkMode = ToolbarShrinkMode.Expand,
                    SelectionMode = ToolbarSelectionMode.Always,
                    TransverseExpansion = true,
                };
                _tpage.Show();
                _tpage.Selected += OnCurrentPageChanged;

                _box = new Box(Forms.Context.MainWindow)
                {
                    AlignmentX = -1,
                    AlignmentY = -1,
                    WeightX = 1,
                    WeightY = 1,
                };
                _box.IsHorizontal = true;
                _box.Show();

                _scroller = new Scroller(Forms.Context.MainWindow)
                {
                    AlignmentX = -1,
                    AlignmentY = -1,
                    WeightX = 1,
                    WeightY = 1,
                };
                _scroller.Style = "tabbar";
                _scroller.HorizontalScrollBarVisiblePolicy = ScrollBarVisiblePolicy.Visible;
                _scroller.SetPageSize(App.ScreenWidth, 0);
                _scroller.HorizontalPageScrollLimit = 1;
                _scroller.PageScrolled += OnPageScrolled;
                _scroller.SetContent(_box);
                _scroller.Show();

                var item = _navi.Push(_scroller);
                item.Style = "tabbar/notitle";
                item.SetPartContent("tabbar", _tpage);

                SetNativeControl(_navi);
            }

            base.OnElementChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (_navi != null)
            {
                _navi.Unrealize();
                _navi = null;
            }

            if (_box != null)
            {
                _box.Unrealize();
                _box = null;
            }

            if (_tpage != null)
            {
                _tpage.Selected -= OnCurrentPageChanged;

                _tpage.Unrealize();
                _tpage = null;
            }

            if (_scroller != null)
            {
                _scroller.Unrealize();
                _scroller.PageScrolled -= OnPageScrolled;
            }

            base.Dispose(disposing);
        }

        protected override void OnElementReady()
        {
            InitializePageInfo();
            base.OnElementReady();
        }

        /// <summary>
        /// To initialize page info.
        /// </summary>
        void InitializePageInfo()
        {
            var logicalChildren = (Element as IElementController).LogicalChildren;
            int pageIndex = 0;

            foreach (Page child in logicalChildren)
            {
                var toolbarItem = _tpage.Append(null, string.IsNullOrEmpty(child.Icon) ? null : ResourcePath.GetPath(child.Icon));

                /// To Set a backgroundcolor for every toolbar item
                toolbarItem.SetPartColor("bg", Element.BarBackgroundColor.ToNative());

                _pageInfo.Add(toolbarItem, new PageInfo { Index = pageIndex++, Page = child });

                var content = Platform.GetOrCreateRenderer(child).NativeView;
                content.SetAlignment(-1, -1);
                content.SetWeight(1, 1);
                content.MinimumWidth = App.ScreenWidth;
                content.Show();
                _box.PackEnd(content);

                if (Element.CurrentPage == child)
                {
                    toolbarItem.IsSelected = true;
                    ShowCurrentPage(_pageInfo[toolbarItem]);
                }
            }
        }

        /// <summary>
        /// A handler that be invoked when the current page is changed from tpage.
        /// </summary>
        /// <param name="sender">A sender</param>
        /// <param name="e">An event</param>
        void OnCurrentPageChanged(object sender, EToolbarItemEventArgs e)
        {
            if (_tpage.SelectedItem == null)
            {
                return;
            }

            /// To send a "SendDisappearing" event for the previous current page
            (Element.CurrentPage as IPageController)?.SendDisappearing();

            /// To set the new current page, show it and send a "SendAppearing" event for it
            Element.CurrentPage = _pageInfo[_tpage.SelectedItem].Page;
            ShowCurrentPage(_pageInfo[_tpage.SelectedItem]);
            (Element.CurrentPage as IPageController)?.SendAppearing();
        }

        /// <summary>
        /// A handler that be invoked when the CurrentPaget of the TabbedPage is changed.
        /// </summary>
        void CurrentPageChanged()
        {
            foreach (var pageInfo in _pageInfo)
            {
                if (pageInfo.Value.Page == Element.CurrentPage)
                {
                    pageInfo.Key.IsSelected = true;
                }
            }
        }

        /// <summary>
        /// To update the background color of A tabbar.
        /// </summary>
        void UpdateBarBackgroundColor()
        {
            foreach (var pageinfo in _pageInfo)
            {
                pageinfo.Key.SetPartColor("bg", Element.BarBackgroundColor.ToNative());
            }
        }

        /// <summary>
        /// To show up a current page changed by scrolling to it.
        /// </summary>
        /// <param name="info">A page info that has to display</param>
        void ShowCurrentPage(PageInfo info)
        {
            _scroller.ScrollTo(info.Index, 0, false);
        }

        /// <summary>
        /// A handler that be invoked when pages is scolled.
        /// </summary>
        /// <param name="s">a sender</param>
        /// <param name="e">an event</param>
        void OnPageScrolled(object s, EventArgs e)
        {
            foreach (var pageinfo in _pageInfo)
            {
                if (pageinfo.Value.Index == _scroller.HorizontalPageIndex)
                {
                    pageinfo.Key.IsSelected = true;
                }
            }
        }
    }
}