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
    /// This sample application demonstrates the first screen on TV.
    /// </summary>
    public class FirstScreenApp : NUIApplication
    {
        // Reference to Dali stage.
        private Window _stage;
        // Reference to Dali stage size.
        private Size2D _stageSize;
        // Top Container added to the Stage will contain Poster ScrollContainers.
        private View _topContainer;
        // Bottom Container added to the Stage will contain Menu ScrollContainer.
        private View _bottomContainer;
        // Current Poster Container ID visible on the Top Container / Stage.
        private int _currentPostersContainerID;
        // Number of Poster ScrollContainers to be added on Top Container.
        private int _totalPostersContainers;
        // List collection of Poster ScrollContainers used on the Top Container
        // in this demo application.
        private List<ScrollContainer> _postersContainer;
        // Menu ScrollContainer used on the Bottom Container in this demo application.
        private ScrollContainer _menuContainer;
        // Clip layer (Dali Clip Layer instance) used for Bottom Container.
        private View _bottomClipLayer;
        // Clip layer (Dali Clip Layer instance) used for Top Container.
        private View _topClipLayer;
        // FocusEffect is used to apply Focus animation effect on any supplied item/image.
        private FocusEffect _focusEffect;
        // Contains the physical location of all images used in the demo application.
        private string _imagePath;
        // Reference to the ImageView (Keyboard Focus indicator)
        // applied to the focused item on ScrollContainer.
        private ImageView _keyboardFocusIndicator;
        // Reference to the ImageView used for launcher separation
        // (Launcher consists of three image icons on left of Menu ScrollContainer).
        private ImageView _launcherSeparator;
        // ImageViews used for launcher Icons.
        private ImageView[] launcherIcon;
        // Animation used to show/unhide Bottom Container
        // (Menu ScrollContainer) when it is focused.
        private Animation _showBottomContainerAnimation;
        // Animation used to hide Bottom Container
        // (Menu ScrollContainer) when it is focused.
        private Animation _hideBottomContainerAnimation;
        // Animation used to move Poster scrollContainer
        // from bottom to top and make it non-transparent.
        private Animation _showAnimation;
        // Animation used to make the unused specified Poster scrollContainer transparent.
        private Animation _hideAnimation;
        // The unused Poster scrollContainer which needs to be transparent.
        private ScrollContainer _hideScrollContainer;
        // Reference to Dali KeyboardFocusManager.
        FocusManager _keyboardFocusManager;

        /// <summary>
        /// Overrides this method if want to handle behaviour.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            OnInitialize();
        }

        /// <summary>
        /// Create Items for Poster ScrollContainer
        /// </summary>
        private void CreatePosters()
        {
            for (int j = 0; j < _totalPostersContainers; j++)
            {
                View posterContainer = _postersContainer[j].Container;
                for (int i = 0; i < Constants.PostersItemsCount; i++)
                {
                    if (j % _totalPostersContainers == 0)
                    {
                        View item = new ImageView(_imagePath + "/poster" + j + "/" + (i % 14) + ".jpg");
                        item.Name = ("poster-item-" + _postersContainer[j].ItemCount);
                        _postersContainer[j].Add(item);
                    }
                    else
                    {
                        View item = new ImageView(_imagePath + "/poster" + j + "/" + (i % 6) + ".jpg");
                        item.Name = ("poster-item-" + _postersContainer[j].ItemCount);
                        _postersContainer[j].Add(item);
                    }
                }

                if (j == 0)
                {
                    Show(_postersContainer[j]);
                }
                else
                {
                    Hide(_postersContainer[j]);
                }

                _postersContainer[j].SetFocused(false);
            }

            _currentPostersContainerID = 0;
        }

        /// <summary>
        /// Create Items for Menu ScrollContainer
        /// <summary>
        private void CreateMenu()
        {
            View menuContainer = _menuContainer.Container;
            menuContainer.Position = new Position(150, 0.0f, 0.0f);

            for (int i = 0; i < Constants.MenuItemsCount; i++)
            {
                ImageView menuItem = new ImageView(_imagePath + "/menu/" + i % 5 + ".png");
                menuItem.Name = ("menu-item-" + _menuContainer.ItemCount);
                _menuContainer.Add(menuItem);
            }
        }

        /// <summary>
        /// Callback when the keyboard focus is going to be changed.
        /// </summary>
        /// <param name="source">FocusManager.Instance</param>
        /// <param name="e">event</param>
        /// <returns> The view to move the keyboard focus to.</returns>
        private View OnKeyboardPreFocusChangeSignal(object source, FocusManager.PreFocusChangeEventArgs e)
        {
            if (!e.CurrentView && !e.ProposedView)
            {
                return _menuContainer;
            }

            View actor = _menuContainer.Container;

            if (e.Direction == View.FocusDirection.Up)
            {
                // Move the Focus to Poster ScrollContainer and hide Bottom Container (Menu ScrollContainer)
                if (_menuContainer.IsFocused)
                {
                    actor = _postersContainer[_currentPostersContainerID].GetCurrentFocusedActor();
                    _menuContainer.SetFocused(false);
                    _postersContainer[_currentPostersContainerID].SetFocused(true);
                    HideBottomContainer();
                }
            }
            else if (e.Direction == View.FocusDirection.Down)
            {
                // Show Bottom Container (Menu ScrollContainer) and move the Focus to it
                if (!_menuContainer.IsFocused)
                {
                    ShowBottomContainer();
                    actor = _menuContainer.GetCurrentFocusedActor();
                    _postersContainer[_currentPostersContainerID].SetFocused(false);
                    _menuContainer.SetFocused(true);
                }
            }
            else
            {
                // Set the next focus view.
                actor = e.ProposedView;
            }

            if (e.Direction == View.FocusDirection.Left)
            {
                if (_menuContainer.IsFocused)
                {
                    int id = _menuContainer.FocusedItemID % _totalPostersContainers;
                    if (id != _currentPostersContainerID)
                    {
                        Hide(_postersContainer[_currentPostersContainerID]);
                        _currentPostersContainerID = id;

                        Show(_postersContainer[_currentPostersContainerID]);
                    }
                }
            }
            else if (e.Direction == View.FocusDirection.Right)
            {
                if (_menuContainer.IsFocused)
                {
                    int id = _menuContainer.FocusedItemID % _totalPostersContainers;
                    if (id != _currentPostersContainerID)
                    {
                        Hide(_postersContainer[_currentPostersContainerID]);
                        _currentPostersContainerID = id;
                        Show(_postersContainer[_currentPostersContainerID]);
                    }
                }
            }

            return (View)actor;
        }

        /// <summary>
        /// Perform Focus animation Effect on the current Focused Item on ScrollContainer.
        /// </summary>
        /// <param name="scrollContainer">Scroll Container</param>
        /// <param name="direction">Direction</param>
        private void FocusAnimation(ScrollContainer scrollContainer, FocusEffectDirection direction)
        {
            _focusEffect.FocusAnimation(scrollContainer.GetCurrentFocusedActor(), scrollContainer.ItemSize, 1000, direction);
        }

        /// <summary>
        /// Perform Show animation on ScrollContainer (used only for Poster Container)
        /// </summary>
        /// <param name="scrollContainer">scrollContainer</param>
        private void Show(ScrollContainer scrollContainer)
        {
            scrollContainer.Add(scrollContainer.Container);

            _hideScrollContainer = null;

            // This animation will move Poster scrollContainer
            // from bottom to top and make it non-transparent.
            _showAnimation = new Animation(350);
            _showAnimation.AnimateTo(scrollContainer.Container, "ColorAlpha", 1.0f);

            scrollContainer.Container.PositionY = scrollContainer.Container.Position.Y + 200.0f;
            float targetPositionY = scrollContainer.Container.Position.Y - 200.0f;

            _showAnimation.AnimateTo(scrollContainer.Container, "PositionY", targetPositionY, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));

            _showAnimation.Play();
        }

        /// <summary>
        /// Perform Hide animation on ScrollContainer (used only for Poster Container)
        /// </summary>
        /// <param name="scrollContainer">scrollContainer</param>
        private void Hide(ScrollContainer scrollContainer)
        {
            if (_hideAnimation)
            {
                _hideAnimation.Clear();
                _hideAnimation.Reset();
            }

            int duration = 350;
            _hideAnimation = new Animation(duration);
            _hideAnimation.Duration = duration;
            _hideAnimation.AnimateTo(scrollContainer.Container, "ColorAlpha", 0.0f, 0, (int)((float)duration * 0.75f), new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));
            _hideAnimation.Finished += OnHideAnimationFinished;
            _hideScrollContainer = scrollContainer;
            _hideAnimation.Play();
        }

        /// <summary>
        /// This removes all the items from the specified unused
        /// Poster ScrollContainer (hence Stage) to improve performance.
        /// <summary>
        private void OnHideAnimationFinished(object source, EventArgs e)
        {
            if (_hideScrollContainer)
            {
                _hideScrollContainer.Remove(_hideScrollContainer.Container);
            }
        }

        /// <summary>
        /// Hide Bottom Container (Menu ScrollContainer) when it is not focused
        /// </summary>
        private void HideBottomContainer()
        {
            _hideBottomContainerAnimation.AnimateTo(_bottomContainer, "Position", new Position(0.0f, _stageSize.Height * Constants.BottomContainerHidePositionFactor, 0.0f),
                    new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));

            _hideBottomContainerAnimation.Play();
        }

        /// <summary>
        /// Show (unhide) Bottom Container (Menu ScrollContainer) when it is focused
        /// </summary>
        private void ShowBottomContainer()
        {
            _showBottomContainerAnimation.AnimateTo(_bottomContainer, "Position", new Position(0.0f, _stageSize.Height * Constants.BottomContainerShowPositionFactor, 0.0f),
                            new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));
            _showBottomContainerAnimation.Play();
        }

        /// <summary>
        /// First screen demo Application initialisation
        /// </summary>
        private void OnInitialize()
        {
            Tizen.Log.Debug("NUI", "OnInitialize() is called!");
            _hideScrollContainer = null;
            _stage = Window.Instance;
            _stageSize = _stage.Size;
            _totalPostersContainers = Constants.TotalPostersContainers;
            _imagePath = Constants.ImageResourcePath;

            _postersContainer = new List<ScrollContainer>();
            _menuContainer = new ScrollContainer();

            _hideBottomContainerAnimation = new Animation(250);
            _showBottomContainerAnimation = new Animation(250);

            // Create a Top Container for poster items
            _topContainer = new View();
            _topContainer.Size2D = new Vector2(_stageSize.Width, _stageSize.Height * Constants.TopContainerHeightFactor);
            _topContainer.Position = new Position(0.0f, _stageSize.Height * Constants.TopContainerPositionFactor - 80, 0.0f);
            _topContainer.ParentOrigin = ParentOrigin.TopLeft;
            _topContainer.PivotPoint = PivotPoint.TopLeft;
            _topContainer.PositionUsesPivotPoint = true;

            // Add a background to Top container
            PropertyMap visual = new PropertyMap();
            visual.Insert(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            visual.Insert(ImageVisualProperty.URL, new PropertyValue(_imagePath + "/focuseffect/background.png"));
            _topContainer.Background = visual;
            _topContainer.Name = "TopControl";

            // Create a Bottom Container
            _bottomContainer = new View();
            _bottomContainer.Size2D = new Vector2(_stageSize.Width, _stageSize.Height * Constants.BottomContainerHeightFactor);
            _bottomContainer.Position = new Position(0.0f, _stageSize.Height * Constants.BottomContainerHidePositionFactor, 0.0f);
            _bottomContainer.ParentOrigin = ParentOrigin.TopLeft;
            _bottomContainer.PivotPoint = PivotPoint.TopLeft;
            _bottomContainer.PositionUsesPivotPoint = true;

            // Add a background to Bottom Container
            visual = new PropertyMap();
            visual.Insert(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            visual.Insert(ImageVisualProperty.URL, new PropertyValue(_imagePath + "/focuseffect/background.png"));
            _bottomContainer.Background = visual;
            _bottomContainer.Name = "BottomControl";

            // Add both Top and Bottom Containers to Stage
            _stage.GetDefaultLayer().Add(_topContainer);
            _stage.GetDefaultLayer().Add(_bottomContainer);

            // Add a clip layer to Top Container
            _topClipLayer = new View();
            _topClipLayer.ParentOrigin = ParentOrigin.BottomCenter;
            _topClipLayer.PivotPoint = PivotPoint.BottomCenter;
            _topClipLayer.PositionUsesPivotPoint = true;

            _topContainer.Add(_topClipLayer);

            // Create a SpotLight for items / images of both
            // Poster and Menu ScrollContainers
            ImageView spotLight = new ImageView(_imagePath + "/focuseffect/highlight_spot.png");
            spotLight.WidthResizePolicy = ResizePolicyType.UseNaturalSize;
            spotLight.HeightResizePolicy = ResizePolicyType.UseNaturalSize;
            spotLight.ParentOrigin = ParentOrigin.Center;
            spotLight.PivotPoint = PivotPoint.Center;
            spotLight.PositionUsesPivotPoint = true;
            spotLight.Name = "spotLight";

            // Create a shadowBorder for items / images of Poster ScrollContainers
            ImageView shadowBorder = new ImageView(_imagePath + "/focuseffect/thumbnail_shadow.9.png");
            shadowBorder.ParentOrigin = ParentOrigin.Center;
            shadowBorder.PivotPoint = PivotPoint.Center;
            shadowBorder.PositionUsesPivotPoint = true;
            shadowBorder.WidthResizePolicy = ResizePolicyType.SizeFixedOffsetFromParent;
            shadowBorder.HeightResizePolicy = ResizePolicyType.SizeFixedOffsetFromParent;
            shadowBorder.SizeModeFactor = (new Vector3(32.0f, 41.0f, 0.0f));
            shadowBorder.Name = "poster shadowBorder";

            // Create Poster Containers and add them to Top Clip layer
            for (int i = 0; i < _totalPostersContainers; i++)
            {
                _postersContainer.Add(new ScrollContainer());
                _postersContainer[i].Container.Name = "poster" + i;
                if (i == 0)
                {
                    _postersContainer[i].ItemSize = new Vector2((_stageSize.Width * Constants.Poster0ItemWidthFactor) - Constants.PostersContainerPadding,
                                                                _stageSize.Height * Constants.PostersItemHeightFactor);
                }
                else
                {
                    _postersContainer[i].ItemSize = new Vector2((_stageSize.Width * Constants.Poster1ItemWidthFactor) - Constants.PostersContainerPadding,
                                                                _stageSize.Height * Constants.PostersItemHeightFactor);
                }

                _postersContainer[i].Gap = Constants.PostersContainerPadding;
                _postersContainer[i].MarginX = Constants.PostersContainerMargin;
                _postersContainer[i].OffsetYFator = Constants.PostersContainerOffsetYFactor;
                _postersContainer[i].Width = _stageSize.Width;
                _postersContainer[i].Height = _stageSize.Height * Constants.PostersContainerHeightFactor;
                _postersContainer[i].ShadowBorder = shadowBorder;
                _postersContainer[i].ShadowBorder.Position = new Position(0.0f, 4.0f, 0.0f);
                _topClipLayer.Add(_postersContainer[i]);
            }

            // Add a clip layer to Bottom Container
            _bottomClipLayer = new View();
            _bottomClipLayer.PivotPoint = PivotPoint.BottomLeft;
            _bottomClipLayer.PositionUsesPivotPoint = true;
            _bottomClipLayer.ParentOrigin = ParentOrigin.BottomLeft;
            _bottomClipLayer.Position = new Position(Constants.LauncherWidth, 35, 0);
            _bottomClipLayer.SizeWidth = _stageSize.Width - Constants.LauncherWidth;
            _bottomClipLayer.SizeHeight = _stageSize.Height * Constants.MenuContainerHeightFactor;
            _bottomContainer.ClippingMode = ClippingModeType.ClipChildren;
            _bottomContainer.Add(_bottomClipLayer);

            // Add Launcher items to Bottom Container. Launcher is used
            // to display three images on left of Menu ScrollContainer
            launcherIcon = new ImageView[Convert.ToInt32(Constants.LauncherItemsCount)];
            for (int launcherIndex = 0; launcherIndex < Constants.LauncherItemsCount; launcherIndex++)
            {
                launcherIcon[launcherIndex] = new ImageView(_imagePath + "/focuseffect/" + launcherIndex + "-normal.png");
                launcherIcon[launcherIndex].Name = "launcherIcon" + launcherIndex;
                launcherIcon[launcherIndex].WidthResizePolicy = ResizePolicyType.UseNaturalSize;
                launcherIcon[launcherIndex].HeightResizePolicy = ResizePolicyType.UseNaturalSize;
                launcherIcon[launcherIndex].ParentOrigin = ParentOrigin.CenterLeft;
                launcherIcon[launcherIndex].PivotPoint = PivotPoint.CenterLeft;
                launcherIcon[launcherIndex].PositionUsesPivotPoint = true;
                launcherIcon[launcherIndex].Position = new Position(Constants.LauncherIconWidth * launcherIndex + Constants.LauncherLeftMargin, 0.0f, 0.0f);
                _bottomContainer.Add(launcherIcon[launcherIndex]);
            }

            // Add a shadow seperator image between last Launcher
            // icon and Menu ScrollContainer
            _launcherSeparator = new ImageView(_imagePath + "/focuseffect/focus_launcher_shadow_n.png");
            _launcherSeparator.Name = "launcherSeparator";
            _launcherSeparator.WidthResizePolicy = ResizePolicyType.UseNaturalSize;
            _launcherSeparator.HeightResizePolicy = ResizePolicyType.FillToParent;
            _launcherSeparator.ParentOrigin = ParentOrigin.CenterLeft;
            _launcherSeparator.PivotPoint = PivotPoint.CenterLeft;
            _launcherSeparator.PositionUsesPivotPoint = true;
            _launcherSeparator.Position = new Position(Constants.LauncherIconWidth * Constants.LauncherItemsCount + Constants.LauncherLeftMargin, 0.0f, 0.0f);
            _bottomContainer.Add(_launcherSeparator);

            // Create Menu Container and add it to Bottom Clip Layer
            Vector2 menuItemSize = new Vector2((_stageSize.Width * Constants.MenuItemWidthFactor) - Constants.MenuContainerPadding,
                                        _stageSize.Height * Constants.MenuItemHeightFactor);
            _menuContainer.Container.Name = "menu";
            _menuContainer.ItemSize = new Vector2(110, 110);
            Tizen.Log.Fatal("NUI", "ItemSize.Width: " + _menuContainer.ItemSize.Width + "  ItemSize.Height: " + _menuContainer.ItemSize.Height);
            _menuContainer.Gap = Constants.MenuContainerPadding;
            _menuContainer.MarginX = Constants.MenuContainerMargin;
            _menuContainer.OffsetYFator = Constants.MenuContainerOffsetYFactor;
            _menuContainer.OffsetX = Constants.LauncherWidth;
            _menuContainer.Width = _stageSize.Width - Constants.LauncherWidth;
            _menuContainer.Height = _stageSize.Height * Constants.MenuContainerHeightFactor;
            _menuContainer.ShadowBorder = new ImageView(_imagePath + "/focuseffect/focus_launcher_shadow.9.png");
            _menuContainer.ShadowBorder.Name = "_menuContainer.ShadowBorder";
            _menuContainer.ShadowBorder.Size2D = new Vector2(_menuContainer.ItemSize.Width + 40.0f, _menuContainer.ItemSize.Height + 50.0f);
            _menuContainer.ShadowBorder.Position = new Position(0.0f, 0.0f, 0.0f);
            _menuContainer.ShadowBorder.ParentOrigin = ParentOrigin.Center;
            _menuContainer.ShadowBorder.PivotPoint = PivotPoint.Center;
            _menuContainer.ShadowBorder.PositionUsesPivotPoint = true;
            _menuContainer.ClippingMode = ClippingModeType.ClipChildren;
            _bottomClipLayer.Add(_menuContainer);

            // Create Items for Poster ScrollContainer
            CreatePosters();
            // Create Items for Menu ScrollContainer
            CreateMenu();

            // Initialize PreFocusChange event of KeyboardFocusManager
            _keyboardFocusManager = FocusManager.Instance;
            _keyboardFocusManager.PreFocusChange += OnKeyboardPreFocusChangeSignal;

            _keyboardFocusIndicator = new ImageView(_imagePath + "/focuseffect/highlight_stroke.9.png");
            _keyboardFocusIndicator.ParentOrigin = ParentOrigin.Center;
            _keyboardFocusIndicator.PivotPoint = PivotPoint.Center;
            _keyboardFocusIndicator.PositionUsesPivotPoint = true;
            _keyboardFocusIndicator.WidthResizePolicy = ResizePolicyType.FillToParent;
            _keyboardFocusIndicator.HeightResizePolicy = ResizePolicyType.FillToParent;

            _keyboardFocusManager.FocusIndicator = (_keyboardFocusIndicator);

            _keyboardFocusManager.SetAsFocusGroup(_menuContainer, true);
            _keyboardFocusManager.SetAsFocusGroup(_postersContainer[0], true);
            _keyboardFocusManager.SetAsFocusGroup(_postersContainer[1], true);
            _keyboardFocusManager.FocusGroupLoop = (true);

            _focusEffect = new FocusEffect();

            // Move Focus to Bottom Container (Menu ScrollContainer)
            ShowBottomContainer();
            _menuContainer.Focusable = true;
            _menuContainer.SetFocused(true);

#if true
            //test.
            TextLabel _dateOfTest = new TextLabel();
            _dateOfTest.WidthResizePolicy = ResizePolicyType.FillToParent;
            _dateOfTest.HeightResizePolicy = ResizePolicyType.SizeRelativeToParent;
            _dateOfTest.PivotPoint = PivotPoint.TopCenter;
            _dateOfTest.PositionUsesPivotPoint = true;
            _dateOfTest.ParentOrigin = ParentOrigin.TopCenter;
            _dateOfTest.SizeModeFactor = new Vector3(0.0f, 0.1f, 0.0f);
            _dateOfTest.BackgroundColor = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
            _dateOfTest.TextColor = Color.White;
            _dateOfTest.Text = " FirstScreen ";
            _dateOfTest.HorizontalAlignment = HorizontalAlignment.Center;
            _dateOfTest.VerticalAlignment = VerticalAlignment.Center;
            _dateOfTest.PointSize = 12.0f;
            _dateOfTest.UnderlineEnabled = true;
            _stage.GetDefaultLayer().Add(_dateOfTest);
#endif
            Window.Instance.KeyEvent += AppBack;
        }

        /// <summary>
        /// This Application will be exited when back key entered.
        /// </summary>
        /// <param name="source">Window.Instance</param>
        /// <param name="e">event</param>
        private void AppBack(object source, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "XF86Back")
                {
                    this.Exit();
                }
            }
        }
    }
}
