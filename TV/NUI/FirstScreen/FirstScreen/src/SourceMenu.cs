/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
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
 *
 */

using Tizen.NUI;
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Tizen.NUI.Constants;

namespace FirstScreen
{

    public class SourceMenu
    {
        private ScrollMenu _menu;            // Menu ScrollContainer used on the Bottom Container in this demo application.
        private Animation _showMenuAnimation;
        private Size2D _menuSize;
        private Size2D _itemSize;

        private Effect[] _showHideMenuEffect;

        /// <summary>
        /// Set or get show menu effect
        /// </summary>
        public Effect ShowMenuEffect
        {
            get
            {
                return _showHideMenuEffect[0];
            }

            set
            {
                _showHideMenuEffect[0] = value;
            }
        }

        /// <summary>
        /// Set or get hide menu effect
        /// </summary>
        public Effect HideMenuEffect
        {
            get
            {
                return _showHideMenuEffect[1];
            }

            set
            {
                _showHideMenuEffect[1] = value;
            }
        }

        /// <summary>
        /// Get item count in menu
        /// </summary>
        public int Count
        {
            get
            {
                return _menu.ItemCount;
            }
        }

        /// <summary>
        /// Creates and initializes a new instance of the SourceMenu class.
        /// Create Items for Menu ScrollContainer
        /// </summary>
        /// <param name="menuSize">menu size</param>
        /// <param name="itemSize">item size</param>
        public SourceMenu(Size2D menuSize, Size2D itemSize)
        {
            _menuSize = menuSize;
            _itemSize = itemSize;
            _showHideMenuEffect = new Effect[2];

            _showMenuAnimation = new Animation(Constants.ShowMenuDuration);
            _showMenuAnimation.EndAction = Animation.EndActions.Cancel;

            _menu = new ScrollMenu();
            _menu.Name = "sourceMenu";
            _menu.WidthResizePolicy = ResizePolicyType.Fixed;
            _menu.HeightResizePolicy = ResizePolicyType.Fixed;
            _menu.Size2D = _menuSize;
            _menu.PositionUsesPivotPoint = true;
            _menu.ParentOrigin = ParentOrigin.BottomRight;
            _menu.PivotPoint = PivotPoint.BottomRight;
            _menu.FocusCenter = false;
            _menu.HoldFocusWhileInactive = true;
            _menu.StartActive = true;
            _menu.ItemDimensions = _itemSize;
            _menu.Gap = Constants.SourceMenuPadding;
            _menu.Margin = Constants.SourceMenuMargin;
            _menu.FocusScale = Constants.SourceFocusScale;
            _menu.ScrollEndMargin = Constants.SourceMenuScrollEndMargin;

            _menu.ClippingMode = ClippingModeType.ClipToBoundingBox;

            for (int i = 0; i < Constants.MenuItemsCount; ++i)
            {
                ScrollMenu.ScrollItem scrollItem = new ScrollMenu.ScrollItem();
                scrollItem.item = new ImageView(Constants.ResourcePath + "/images/SourceMenu/mi" + i + ".png");
                scrollItem.item.Name = ("menu-item_" + _menu.ItemCount);
                _menu.AddItem(scrollItem);
            }
        }

        /// <summary>
        /// Get menu scroll
        /// </summary>
        /// <returns>menu scroll</returns>
        public ScrollMenu ScrollMenu()
        {
            return _menu;
        }

        /// <summary>
        /// Get current focused item 
        /// If not focused,get a new view
        /// </summary>
        /// <param name="focus">whether focused</param>
        /// <param name="animate">whether animate</param>
        /// <returns>current focused item or new view</returns>
        public View Focus(bool focus, bool animate)
        {
            _menu.SetActive(focus);
            if (animate)
            {
                Show(focus);
            }

            if (focus)
            {
                return _menu.GetCurrentFocusedItem();
            }
            else
            {
                return new View();
            }
        }

        /// <summary>
        /// Show or hide the menu.
        /// </summary>
        /// <param name="show">Is show or hide</param>
        public void Show(bool show)
        {
            _showHideMenuEffect[show ? 0 : 1].Play();
        }

    }
}
