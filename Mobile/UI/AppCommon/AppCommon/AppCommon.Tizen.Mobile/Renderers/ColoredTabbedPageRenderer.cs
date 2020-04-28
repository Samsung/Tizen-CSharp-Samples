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
using AppCommon.Extensions;
using AppCommon.Tizen.Mobile.Renderers;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;

[assembly: ExportRenderer(typeof(ColoredTabbedPage), typeof(ColoredTabbedPageRenderer))]

namespace AppCommon.Tizen.Mobile.Renderers
{
    /// <summary>
    /// A class for a custom render to support additional functionality
    /// </summary>
    public class ColoredTabbedPageRenderer : VisualElementRenderer<ColoredTabbedPage>, IVisualElementRenderer
    {
        Naviframe _navi;
        Box _box;
        Toolbar _tpage;
        EvasObject _tcontent;
        Dictionary<EToolbarItem, Page> _itemToItemPage = new Dictionary<EToolbarItem, Page>();

        public ColoredTabbedPageRenderer()
        {
            RegisterPropertyHandler(TabbedPage.BarBackgroundColorProperty, UpdateBarBackgroundColor);
            RegisterPropertyHandler("CurrentPage", CurrentPageChanged);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ColoredTabbedPage> e)
        {
            if (_tpage == null)
            {
                _navi = new Naviframe(Forms.NativeParent);

                _tpage = new Toolbar(Forms.NativeParent)
                {
                    Style = "tabbar",
                    Text = Element.Title,
                    ShrinkMode = ToolbarShrinkMode.Expand,
                    SelectionMode = ToolbarSelectionMode.Always,
                    TransverseExpansion = true,
                };
                _tpage.Show();
                _tpage.SelectionMode = ToolbarSelectionMode.Default;
                _tpage.Selected += OnCurrentPageChanged;

                _box = new Box(Forms.NativeParent)
                {
                    AlignmentX = -1,
                    AlignmentY = -1,
                    WeightX = 1,
                    WeightY = 1,
                };
                _box.Show();

                var item = _navi.Push(_box);
                item.Style = "tabbar/notitle";
                item.SetPartContent("tabbar", _tpage);

                SetNativeView(_navi);
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

            base.Dispose(disposing);
        }

        protected override void OnElementReady()
        {
            FillToolbarItems();
            base.OnElementReady();
        }

        void UpdateTitle(Page page)
        {
            if (_itemToItemPage.ContainsValue(page))
            {
                var pair = _itemToItemPage.FirstOrDefault(x => x.Value == page);
                pair.Key.Text = pair.Value.Title;
            }
        }

        void OnPageTitleChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Page.TitleProperty.PropertyName)
            {
                UpdateTitle(sender as Page);
            }
        }

        void FillToolbarItems()
        {
            var logicalChildren = (Element as IElementController).LogicalChildren;

            foreach (Page child in logicalChildren)
            {
                var childRenderer = Platform.GetRenderer(child);
                if (childRenderer != null)
                {
                    childRenderer.NativeView.Hide();
                }

                EToolbarItem toolbarItem = _tpage.Append(child.Title, null);
                toolbarItem.SetPartColor("bg", Element.BarBackgroundColor.ToNative());

                _itemToItemPage.Add(toolbarItem, child);
                if (Element.CurrentPage == child)
                {
                    toolbarItem.IsSelected = true;
                    OnCurrentPageChanged(null, null);
                }

                child.PropertyChanged += OnPageTitleChanged;
            }
        }

        void OnCurrentPageChanged(object sender, EToolbarItemEventArgs e)
        {
            if (_tpage.SelectedItem == null)
            {
                return;
            }

            if (_tcontent != null)
            {
                _tcontent.Hide();
                _box.UnPack(_tcontent);
                (Element.CurrentPage as IPageController)?.SendDisappearing();
            }

            Element.CurrentPage = _itemToItemPage[_tpage.SelectedItem];
            _tcontent = Platform.GetOrCreateRenderer(Element.CurrentPage).NativeView;
            _tcontent.SetAlignment(-1, -1);
            _tcontent.SetWeight(1, 1);
            _tcontent.Show();
            _box.PackEnd(_tcontent);
            (Element.CurrentPage as IPageController)?.SendAppearing();
        }

        void CurrentPageChanged()
        {
            foreach (KeyValuePair<EToolbarItem, Page> pair in _itemToItemPage)
            {
                if (pair.Value == Element.CurrentPage)
                {
                    pair.Key.IsSelected = true;
                    return;
                }
            }
        }

        /// <summary>
        /// To update the background color of A tabbar
        /// </summary>
        void UpdateBarBackgroundColor()
        {
            var logicalChildren = (Element as IElementController).LogicalChildren;
            foreach (KeyValuePair<EToolbarItem, Page> pair in _itemToItemPage)
            {
                EToolbarItem toolbarItem = pair.Key;
                toolbarItem.SetPartColor("bg", Element.BarBackgroundColor.ToNative());
            }
        }
    }
}