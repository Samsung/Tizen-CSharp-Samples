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
    /// <summary>
    /// System menu include 3 icons
    /// </summary>
    public class SystemMenu
    {
        private Animation _systemMenuAnimation;
        private Animation _systemSelectorAnimation;
        private bool _active;
        private float _systemIconGap;
        private float _systemIconSeparation;
        private Color[] _systemSelectorColors;
        private VisualView _systemSelector;
        //todor del?
        private View _systemSelectorClippingBox;
        private ColorVisual _systemSelectorColor;
        private View _menu;
        // Reference to the ImageView used for launcher separation (Launcher consists of three image icons on left of Menu ScrollContainer).
        private ImageView _menuSeparator;
        /// <summary>
        /// ImageViews used for system Icons.
        /// </summary>
        public ImageView[] _systemIcons;
        private ImageView[] _systemStencilIcons;
        private int _focusedIcon;
        private int _maxIconIndex;
        /// <summary>
        /// Set or get menu icon whether active
        /// </summary>
        public bool IsActive
        {
            get
            {
                return _active;
            }

            set
            {
                _active = value;
            }
        }

        // Note: Changing the FocusedIcon does not animate to the new icon. It is used to initialise or modify where the focus will appear when the menu is displayed.
        /// <summary>
        /// Set or get focused icon
        /// if changed,move selector
        /// </summary>
        public int FocusedIcon
        {
            get
            {
                return _focusedIcon;
            }

            set
            {
                // Setting this property causes the selector to animate to the new position.
                if (_focusedIcon != value)
                {
                    _focusedIcon = value;
                    MoveSelector(true);
                }
            }
        }

        /// <summary>
        /// The class's constructor
        /// </summary>
        /// <param name="menuSize">menu size</param>
        /// <param name="stencilContainerPosition">stencil container position</param>
        public SystemMenu(Size2D menuSize, Position2D stencilContainerPosition)
        {
            _systemMenuAnimation = new Animation(Constants.SystemMenuAnimationDuration);
            _systemMenuAnimation.EndAction = Animation.EndActions.Cancel;
            _systemSelectorAnimation = new Animation(Constants.SystemMenuSelectorAnimationDuration);
            _systemSelectorAnimation.EndAction = Animation.EndActions.Cancel;

            _menu = new View();
            _menu.Name = "systemMenu";
            _menu.WidthResizePolicy = ResizePolicyType.Fixed;
            _menu.HeightResizePolicy = ResizePolicyType.FillToParent;
            _menu.ParentOrigin = ParentOrigin.CenterLeft;
            _menu.PivotPoint = PivotPoint.CenterLeft;
            _menu.PositionUsesPivotPoint = true;
            _menu.Size2D = new Size2D(menuSize.Width, 0);

            // Add a shadow seperator image between last system icon and Menu ScrollContainer.
            _menuSeparator = new ImageView(Constants.ResourcePath + "/images/Effect/focus_launcher_shadow_n_enhanced.png");
            _menuSeparator.Name = "menuSeparator";
            _menuSeparator.WidthResizePolicy = ResizePolicyType.UseNaturalSize;
            _menuSeparator.HeightResizePolicy = ResizePolicyType.FillToParent;
            _menuSeparator.ParentOrigin = ParentOrigin.CenterRight;
            _menuSeparator.PivotPoint = PivotPoint.CenterLeft;
            _menuSeparator.PositionUsesPivotPoint = true;
            _menuSeparator.Position = new Position(-1.0f, 0.0f, 0.0f);
            _menu.Add(_menuSeparator);

            // Calculate layout ratios for icons (so they have correct positions regardless of menu scale).
            float count = (float)Constants.SystemItemsCount;
            float menuWidth = Constants.SystemMenuWidth;
            _systemIconGap = (menuWidth - (count * Constants.SystemIconWidth)) / (count + 1);
            _systemIconSeparation = ((menuWidth - _systemIconGap - (count * _systemIconGap)) / count) + _systemIconGap;

            // Add Launcher items to Bottom Container. Launcher is used to display three images on left of Menu ScrollContainer
            _systemIcons = new ImageView[Convert.ToInt32(Constants.SystemItemsCount)];
            for (int icon = 0; icon < Constants.SystemItemsCount; ++icon)
            {
                _systemIcons[icon] = new ImageView(Constants.ResourcePath + "/images/SystemMenu/icon-" + (icon + 1) + ".png");
                // We use the name to keep track of which icon we have selected.
                _systemIcons[icon].Name = icon.ToString();
                _systemIcons[icon].WidthResizePolicy = ResizePolicyType.UseNaturalSize;
                _systemIcons[icon].HeightResizePolicy = ResizePolicyType.UseNaturalSize;
                _systemIcons[icon].PivotPoint = PivotPoint.Center;
                _systemIcons[icon].ParentOrigin = ParentOrigin.Center;
                _systemIcons[icon].Position = GetSystemIconPosition(icon, false);
                _systemIcons[icon].PositionUsesPivotPoint = true;
                _systemIcons[icon].Focusable = true;
                _systemIcons[icon].InheritScale = false;
                _systemIcons[icon].Scale = new Vector3(0.5f, 0.5f, 0.5f);
                _menu.Add(_systemIcons[icon]);
            }

            // Create the system menu selector highlight.
            _systemSelector = new VisualView();
            _systemSelector.WidthResizePolicy = ResizePolicyType.Fixed;
            _systemSelector.HeightResizePolicy = ResizePolicyType.FillToParent;
            _systemSelector.Size2D = new Size2D((int)(Constants.SystemMenuWidth / 3.0f) - 1, 0);
            _systemSelector.PositionUsesPivotPoint = true;
            _systemSelector.Focusable = true;
            _systemSelector.ParentOrigin = ParentOrigin.Center;
            _systemSelector.PivotPoint = PivotPoint.Center;

            // Create a color visual that we can animate the color of, allowing us to also use the selector for clipping.
            _systemSelectorColor = new ColorVisual();
            _systemSelectorColor.PositionPolicy = VisualTransformPolicyType.Relative;
            _systemSelectorColor.SizePolicy = VisualTransformPolicyType.Relative;
            _systemSelectorColor.Origin = Visual.AlignType.Center;
            _systemSelectorColor.AnchorPoint = Visual.AlignType.Center;
            _systemSelectorColor.Color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
            _systemSelector.AddVisual("ColorVisual1", _systemSelectorColor);

            // Create a View to represent the size of the clipping area, so we can use the fast bounding-box clipping mode.
            _systemSelectorClippingBox = new View();
            _systemSelectorClippingBox.WidthResizePolicy = ResizePolicyType.FillToParent;
            _systemSelectorClippingBox.HeightResizePolicy = ResizePolicyType.FillToParent;
            _systemSelectorClippingBox.PositionUsesPivotPoint = true;
            _systemSelectorClippingBox.ParentOrigin = ParentOrigin.Center;
            _systemSelectorClippingBox.PivotPoint = PivotPoint.Center;

            // The system selector will clip its children as well as rendering its visual to the color buffer.
            _systemSelectorClippingBox.ClippingMode = ClippingModeType.ClipToBoundingBox;

            _systemSelector.Add(_systemSelectorClippingBox);
            _menu.Add(_systemSelector);

            _systemSelectorColors = new Color[_systemIcons.Length];
            _systemSelectorColors[0] = new Color(1.0f, 0.4f, 0.3f, 0.5f);
            _systemSelectorColors[1] = new Color(0.2f, 0.7f, 0.2f, 0.5f);
            _systemSelectorColors[2] = new Color(0.2f, 0.7f, 0.9f, 0.5f);

            View stencilIconContainer = new View();
            stencilIconContainer.Name = "stencilIconContainer";
            stencilIconContainer.WidthResizePolicy = ResizePolicyType.Fixed;
            stencilIconContainer.HeightResizePolicy = ResizePolicyType.Fixed;
            stencilIconContainer.PivotPoint = PivotPoint.BottomLeft;
            stencilIconContainer.PositionUsesPivotPoint = true;

            // todor
            stencilIconContainer.InheritPosition = false;

            //TODO: DALi: Because we cannot use parent origin with "Inherit Position = false", this menu now needs to know the window/stage size - which it should not have to care about.
            stencilIconContainer.Position2D = stencilContainerPosition;
            stencilIconContainer.Size2D = menuSize;
            _systemSelectorClippingBox.Add(stencilIconContainer);

            _systemStencilIcons = new ImageView[Convert.ToInt32(Constants.SystemItemsCount)];
            for (int icon = 0; icon < Constants.SystemItemsCount; ++icon)
            {
                _systemStencilIcons[icon] = new ImageView(Constants.ResourcePath + "/images/SystemMenu/icon-" + (icon + 1) + "-active.png");
                // We use the name to keep track of which icon we have selected.
                _systemStencilIcons[icon].Name = icon.ToString();
                _systemStencilIcons[icon].WidthResizePolicy = ResizePolicyType.UseNaturalSize;
                _systemStencilIcons[icon].HeightResizePolicy = ResizePolicyType.UseNaturalSize;
                _systemStencilIcons[icon].PivotPoint = PivotPoint.Center;
                _systemStencilIcons[icon].ParentOrigin = ParentOrigin.Center;
                _systemStencilIcons[icon].Position = GetSystemIconPosition(icon, false);
                _systemStencilIcons[icon].PositionUsesPivotPoint = true;
                _systemStencilIcons[icon].InheritScale = false;
                _systemStencilIcons[icon].Scale = new Vector3(0.5f, 0.5f, 0.5f);
                stencilIconContainer.Add(_systemStencilIcons[icon]);
            }

            _maxIconIndex = _systemIcons.Length - 1;

            // Set up state and position of the selector.
            _focusedIcon = -1;
            MoveSelector(true, true);
        }

        /// <summary>
        /// Get menu view
        /// </summary>
        /// <returns>The menu view</returns>
        public View GetView()
        {
            return _menu;
        }

        /// <summary>
        /// Show a icon or a new view
        /// </summary>
        /// <param name="show">Is show or not</param>
        /// <param name="direction">direction</param>
        /// <returns>
        /// If showing, return the icon that will be shown for KeyboardFocusManager.
        /// else return a new view.
        /// </returns>
        public View Show(bool show, View.FocusDirection direction)
        {
            _active = show;

            _systemMenuAnimation.Clear();

            float moveDelta = Constants.SystemMenuWidth;
            float scaleDelta = 1.0f + (moveDelta / Constants.SystemMenuWidth);
            AlphaFunction alphaFunction;
            if (show)
            {
                alphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseInOutSine);
            }
            else
            {
                alphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine);
            }

            _systemMenuAnimation.AnimateTo(_menu, "ScaleX", show ? scaleDelta : 1.0f, alphaFunction);

            for (int i = 0; i < _systemIcons.Length; ++i)
            {
                Position targetPosition = GetSystemIconPosition(i, show);
                _systemMenuAnimation.AnimateTo(_systemIcons[i], "Scale", show ? new Vector3(1.0f, 1.0f, 1.0f) : new Vector3(0.5f, 0.5f, 0.5f), alphaFunction);
                _systemMenuAnimation.AnimateTo(_systemIcons[i], "Position", targetPosition, alphaFunction);

                if (show)
                {
                    //todor comment
                    targetPosition.X += ((((Constants.SystemMenuWidth * scaleDelta) / 2.0f) - (Constants.SystemMenuWidth / 2.0f)) / scaleDelta);
                }

                _systemMenuAnimation.AnimateTo(_systemStencilIcons[i], "Scale", show ? new Vector3(1.0f, 1.0f, 1.0f) : new Vector3(0.5f, 0.5f, 0.5f), alphaFunction);
                _systemMenuAnimation.AnimateTo(_systemStencilIcons[i], "Position", targetPosition, alphaFunction);
            }

            _systemMenuAnimation.Play();

            MoveSelector(false, false, direction);

            if (show)
            {
                // If showing, return the icon that will be shown for KeyboardFocusManager.
                return _systemIcons[_focusedIcon];
            }

            return new View();
        }

        /// <summary>
        /// Move selector to certain direction
        /// </summary>
        /// <param name="moveOnly">whether only move</param>
        /// <param name="instant">Whether instant,default is false</param>
        /// <param name="direction">move direction</param>
        private void MoveSelector(bool moveOnly, bool instant = false, View.FocusDirection direction = View.FocusDirection.Right)
        {
            int targetIconPosition = _focusedIcon;
            int viewIcon = targetIconPosition;

            int showSelectorStartTime = 0;
            bool delaySelectorPositionChange = false;

            if (!moveOnly)
            {
                switch (direction)
                {
                    case View.FocusDirection.Left:
                    {
                        if (_active)
                        {
                            _focusedIcon = _maxIconIndex;
                            targetIconPosition = _focusedIcon;
                        }

                        break;
                    }

                    case View.FocusDirection.Right:
                    {
                        if (!_active)
                        {
                            targetIconPosition = _maxIconIndex + 1;
                            _focusedIcon = -1;
                        }

                        break;
                    }

                    case View.FocusDirection.Up:
                    {
                        if (!_active)
                        {
                            instant = true;
                            targetIconPosition = _maxIconIndex + 1;
                        }

                        break;
                    }

                    case View.FocusDirection.Down:
                    {
                        if (_active)
                        {
                            if (_focusedIcon == -1)
                            {
                                _focusedIcon = _maxIconIndex;
                                targetIconPosition = _focusedIcon;
                            }

                            showSelectorStartTime = Constants.SystemMenuAnimationDuration;
                            delaySelectorPositionChange = true;
                        }

                        break;
                    }
                }

                viewIcon = _focusedIcon;
                if (viewIcon > _maxIconIndex)
                {
                    viewIcon = _maxIconIndex;
                }

                if (viewIcon == - 1)
                {
                    viewIcon = 0;
                }

            }

            float selectorWidth = (Constants.SystemMenuWidth / (float)_systemIcons.Length);
            float iconX = _active ? (Constants.SystemMenuWidth / (float)_systemIcons.Length) * (float)(targetIconPosition - (_systemIcons.Length / 2)) : (Constants.SystemMenuWidth - selectorWidth);
            int startTime = 0;

            _systemSelectorAnimation.Clear();

            // Handle the selector color & transparency.
            if (instant)
            {
                // We are hiding instantly, set the color to transparent and set the position directly (without animating).
                _systemSelectorColor.Color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                _systemSelector.PositionX = iconX;
            }
            else
            {
                // We animate the fade if we are showing, or if we are hiding *gradually* - IE. not moving the position instantly.
                Color targetColor = _active ? _systemSelectorColors[viewIcon] : new Color(1.0f, 1.0f, 0.0f, 0.0f);

                // We set the endtime to starttime + duration so, if necessary, the selector animation can start *after* the menu animation ends
                _systemSelectorAnimation = _systemSelector.AnimateVisual(_systemSelectorColor, "mixColor",
                    targetColor, showSelectorStartTime, showSelectorStartTime + Constants.SystemMenuAnimationDuration, AlphaFunction.BuiltinFunctions.EaseOutSine);

                if (delaySelectorPositionChange)
                {
                    startTime = Constants.SystemMenuSelectorAnimationDuration;
                }

                // We allow the start time to be modified but fix the end time.
                // This allows us to delay a property set if required (by starting and finishing the animation after the menu animation ends).
                _systemSelectorAnimation.AnimateTo(_systemSelector, "PositionX", iconX, startTime, Constants.SystemMenuSelectorAnimationDuration, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));

                _systemSelectorAnimation.Play();
            }
        }
     
        private Position GetSystemIconPosition(int icon, bool active)
        {
            float x = (_systemIconSeparation - (active ? 0.0f : (_systemIconGap * 1.5f))) * (float)(icon - (_systemIcons.Length / 2));
            float y = active ? 0.0f : (icon % 2) == 0 ? 20.0f : -20.0f;
            return new Position(x, y, 0.0f);
        }

    }
}
