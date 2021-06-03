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
    /// Menu application
    /// Load in Tizen project
    /// </summary>
    public class MenuScreen : NUIApplication
    {
        private bool _benchmarkMode;
        // Reference to Dali window.
        private Window _window;
        //Reference to Dali window size.
        private Size2D _windowSize;
        //Current Poster Container ID visible on the Top Container / Stage.
        private int _currentMenuIndex;
        //List collection of Poster ScrollContainers used on the Top Container in this demo application.
        private List<PosterMenu> _posterMenus;
        //Menu ScrollContainer used on the Bottom Container in this demo application.
        private SourceMenu _menu;
        private SystemMenu _systemMenu;
        private View _posterMenuContainer;
        // Contains the physical location of all resources used in the demo application.
        private string _resourcePath;
        private ImageView _menuContainer;
        // TODO: We do not need to create an animation for showing and hiding once the Effect class has an OnAnimationFinished signal.
        List<int> _posterMenuHideStack;
        private Animation _moveSourceMenuAnimation;
        // Reference to Dali KeyboardFocusManager.
        FocusManager _keyboardFocusManager;

        // Pre-calculations for re-use:
        private float _posterMenuHeight;
        private float _posterMenuY;

        // Action Reminder related variables:
        private ActionReminder _actionReminder;
        private Timer _actionReminderTimer;
        private uint _actionReminderTimePeriod;
        private int _actionReminderTickCounter;
        private int _actionReminderRemoteShowTicks;

        // Automation related variables:
        private Automation _automation;

        // Configuration:
        private TVConfiguration.Configuration _configuration;

        // Properties:
        /// <summary>
        /// Get or set benchmark mode
        /// </summary>
        public bool BenchmarkMode
        {
            get
            {
                return _benchmarkMode;
            }

            set
            {
                _benchmarkMode = value;
            }
        }

        /// <summary>
        /// Handle when app creates
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            OnInitialize();
        }

        /// <summary>
        /// Creates and initializes a new instance of MenuScreen class.
        /// </summary>
        public MenuScreen()
        : base("", Constants.TransparentWindow ? WindowMode.Transparent : WindowMode.Opaque)
        {
        }

        private void CreateMenuHighlightEffects(ScrollMenu menu, String resourceSubDirectory, ImageView spotLight)
        {
            View highlightContainer = new View();
            highlightContainer.PositionUsesPivotPoint = true;
            highlightContainer.PivotPoint = Tizen.NUI.PivotPoint.Center;
            highlightContainer.ParentOrigin = Tizen.NUI.ParentOrigin.Center;
            highlightContainer.WidthResizePolicy = ResizePolicyType.FillToParent;
            highlightContainer.HeightResizePolicy = ResizePolicyType.FillToParent;
            ImageView spotlightClip = ClippingImage.Create(_resourcePath + "/images/" + resourceSubDirectory + "/rounded-mask.png");
            spotlightClip.WidthResizePolicy = ResizePolicyType.FillToParent;
            spotlightClip.HeightResizePolicy = ResizePolicyType.FillToParent;
            ImageView shineClip = ClippingImage.Create(_resourcePath + "/images/" + resourceSubDirectory + "/rounded-border-mask.png");
            shineClip.WidthResizePolicy = ResizePolicyType.FillToParent;
            shineClip.HeightResizePolicy = ResizePolicyType.FillToParent;
            // We use a member variable for the shine as it gets reparented it can get garbage collected before it is accessed.
            ImageView shine = new ImageView(_resourcePath + "/images/HighlightEffect/source-shine.png");
            shine.Name = "shine";
            shine.WidthResizePolicy = ResizePolicyType.FillToParent;
            shine.HeightResizePolicy = ResizePolicyType.FillToParent;
            shineClip.Add(shine);
            highlightContainer.Add(spotlightClip);
            highlightContainer.Add(shineClip);
            // TODO: We are only adding these to the menu as they cannot be encapsulated in the Effect class yet.
            menu.SpotLightClip = spotlightClip;
            menu.SpotLight = spotLight;
            menu.Shine = shine;

            // Create the selection effect, which includes a moving shine and a spotlight rotating in a circle.
            Effect selectionEffect = new Effect(highlightContainer);
            selectionEffect.Name = "selectionEffect";
            Size2D itemDimensions = menu.ItemDimensions;
            selectionEffect.AddAction(new Effect.AnimateBetween("shine", "PositionX", -itemDimensions.Width, itemDimensions.Width, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseInOutSine), 0, Constants.ShineDuration));
            Position topLeft =     new Position(-0.25f * itemDimensions.Width, -0.25f * itemDimensions.Height, 0.0f);
            Position topRight =    new Position(0.25f * itemDimensions.Width, -0.25f * itemDimensions.Height, 0.0f);
            Position bottomRight = new Position(0.25f * itemDimensions.Width,  0.25f * itemDimensions.Height, 0.0f);
            Position bottomLeft =  new Position(-0.25f * itemDimensions.Width,  0.25f * itemDimensions.Height, 0.0f);
            Path circularPath = new Path();
            circularPath.AddPoint(topLeft);
            circularPath.AddPoint(topRight);
            circularPath.AddPoint(bottomRight);
            circularPath.AddPoint(bottomLeft);
            circularPath.AddPoint(topLeft);
            circularPath.GenerateControlPoints(0.5f);
            Effect.AnimatePath spotlightAction = new Effect.AnimatePath("spotLight", circularPath, new Vector3(0.0f, 0.0f, 0.0f), new AlphaFunction(AlphaFunction.BuiltinFunctions.Default), 0, Constants.SpotLightDuration);
            spotlightAction.Looping = true;
            selectionEffect.AddAction(spotlightAction);
            menu.SelectionEffect = selectionEffect;

            // Create the pulse effect, which is a non-symetric X/Y scale "bounce" effect to the animation.
            Effect pulseEffect = new Effect(highlightContainer);
            pulseEffect.Name = "selectionEffect";
            // Note: The menu will automatically set the target of the pulse effect to the correct menu item itself.
            pulseEffect.AddAction(new Effect.AnimateTo(null, "Scale", new Vector3(1.3f, 1.0f, 1.0f), new AlphaFunction(AlphaFunction.BuiltinFunctions.Bounce)));
            menu.PulseEffect = pulseEffect;
        }

        private View OnKeyboardPreFocusChangeSignal(object source, FocusManager.PreFocusChangeEventArgs e)
        {
            // Reset the action reminder timer, as the user has provided input.
            _actionReminderTimer.Stop();
            _actionReminderTickCounter = 0;
            _actionReminderTimer.Start();

            View view = null;

            if (_menu.ScrollMenu().IsActive)
            {
                int id = _menu.ScrollMenu().FocusedItemID;
                if (e.Direction == View.FocusDirection.Up)
                {
                    _menu.Focus(false, true);
                    view = _posterMenus[_currentMenuIndex].Focus(true);
                }
                else if (e.Direction == View.FocusDirection.Left)
                {
                    --id;
                    if (id < 0)
                    {
                        _menu.Focus(false, false);
                        view = _systemMenu.Show(true, View.FocusDirection.Left);
                        MoveSourceMenuAnimation(true);
                    }
                }
                else if (e.Direction == View.FocusDirection.Right)
                {
                    ++id;
                    if (id >= _menu.Count)
                    {
                        id = _menu.Count - 1;
                    }
                }

                if (view == null)
                {
                    id = id % _menu.Count;
                    if (id != _currentMenuIndex)
                    {
                        UpdateActivePosterMenu(id);
                    }

                    view = e.ProposedView;
                }
            }
            else if (_posterMenus[_currentMenuIndex].ScrollMenu().IsActive)
            {
                if (e.Direction == View.FocusDirection.Down)
                {
                    _posterMenus[_currentMenuIndex].Focus(false);
                    if (_systemMenu.FocusedIcon >= 0)
                    {
                        _menu.Show(true);
                        view = _systemMenu.Show(true, View.FocusDirection.Down);
                        MoveSourceMenuAnimation(true);
                    }
                    else
                    {
                        view = _menu.Focus(true, true);
                    }
                }
                else
                {
                    view = e.ProposedView;
                }
            }
            else if (_systemMenu.IsActive)
            {
                int icon = _systemMenu.FocusedIcon;
                if (e.Direction == View.FocusDirection.Left)
                {
                    --icon;
                    if (icon < 0)
                    {
                        icon = 0;
                    }
                }
                else if (e.Direction == View.FocusDirection.Right)
                {
                    ++icon;
                    if (icon > 2)
                    {
                        icon = -1;
                        _systemMenu.Show(false, View.FocusDirection.Right);
                        MoveSourceMenuAnimation(false);
                        view = _menu.Focus(true, false);
                    }
                }
                else if (e.Direction == View.FocusDirection.Up)
                {
                    _systemMenu.Show(false, View.FocusDirection.Up);
                    MoveSourceMenuAnimation(false);
                    _menu.Focus(false, true);
                    view = _posterMenus[_currentMenuIndex].Focus(true);
                }

                if (view == null)
                {
                    _systemMenu.FocusedIcon = icon;
                    view = _systemMenu._systemIcons[icon];
                }
            }

            return view;
        }

        private void MoveSourceMenuAnimation(bool show)
        {
            _moveSourceMenuAnimation.Clear();
            _moveSourceMenuAnimation.AnimateTo(_menu.ScrollMenu(), "PositionX", show ? Constants.SystemMenuWidth : 0.0f,
                                                new AlphaFunction(show ? AlphaFunction.BuiltinFunctions.EaseInOutSine : AlphaFunction.BuiltinFunctions.EaseOutSine));
            _moveSourceMenuAnimation.Play();
        }

        // Show the newly active poster-menu, and hide the old, inactive one (if there is one).
        private void UpdateActivePosterMenu(int posterMenuId)
        {
            // If we were in the process of hiding this menu, we aren't anymore!
            _posterMenuHideStack.Remove(posterMenuId);

            // Check if there is a hide animation in progress.
            while (_posterMenuHideStack.Count > 0)
            {
                // Currently hiding. Force it to end
                // We need to force the hide instantly, but not the show. The showing menu now becomes the hiding one,
                // and we want that to transition smoothly, so we don't suddenly finish the show, we just start hiding it from where it is.
                UnparentPosterMenu(_posterMenuHideStack[0]);
            }

            // Hide the previously selected poster-menu.
            // We use the Animation.Finished signal to unparent the menu when done.
            _posterMenuHideStack.Add(_currentMenuIndex);
            _posterMenus[_currentMenuIndex].Show(false);

            // Add the new poster-menu to the window (it is kept off-window for performance), and show it.
            _currentMenuIndex = posterMenuId;
            _posterMenuContainer.Add(_posterMenus[_currentMenuIndex].ScrollMenu());
            _posterMenus[_currentMenuIndex].Show(true);
        }

        // This removes a PosterMenu the items from the specified unused Poster ScrollMenu (and Stage) to improve performance.
        private void UnparentPosterMenu(int posterMenuIndex)
        {
            if (posterMenuIndex != -1 && posterMenuIndex != _currentMenuIndex)
            {
                // Finished hiding (via animation end or force), remove from the clean-up list.
                _posterMenuHideStack.Remove(posterMenuIndex);
                ScrollMenu hiddenPosterMenu = _posterMenus[posterMenuIndex].ScrollMenu();

                // Unparent the poster-menu.
                if (hiddenPosterMenu != null && hiddenPosterMenu.GetParent() != null)
                {
                    hiddenPosterMenu.GetParent().Remove(hiddenPosterMenu);
                }
            }
        }

        // Called when the hide poster menu effect has finished.
        private bool OnHidePosterEffectFinished(object source, Effect.EffectEventArgs e)
        {
            UnparentPosterMenu(e.tag);
            return true;
        }

        private void CreatePosterMenus(ImageView spotLight)
        {
            _posterMenus = new List<PosterMenu>();
            int posterMenuHeight = Convert.ToInt32(_windowSize.Height * Constants.TopContainerHeightFactor);

            // Create Poster Containers and add them.
            for (int menuNumber = 0; menuNumber < _configuration.menuDefinitions.Count; ++menuNumber)
            {
                // Create Items for the Poster ScrollContainer.
                String imagePath = _resourcePath + "/images/PosterMenu/" + menuNumber;
                PosterMenu posterMenu = new PosterMenu(posterMenuHeight, imagePath, _configuration.menuDefinitions[menuNumber]);

                TextLabel title = new TextLabel(_configuration.menuDefinitions[menuNumber].description);
                title.Name = "menu-title_" + (menuNumber + 1);
                title.WidthResizePolicy = ResizePolicyType.UseNaturalSize;
                title.PositionUsesPivotPoint = true;
                title.ParentOrigin = new Position(0.05f, 0.04f, 0.5f);
                title.PivotPoint = PivotPoint.TopLeft;
                //title.PointSize = Constants.TVBuild ? 29.0f : 23.0f; //TODO: Set with style sheet
                //title.PointSize = Constants.TVBuild ? 8.0f : 23.0f;
                title.PointSize = DeviceCheck.PointSize8;

                ScrollMenu posterMenuView = posterMenu.ScrollMenu();
                String posterMenuName = "posterMenu-" + menuNumber;
                posterMenuView.Name = posterMenuName;
                posterMenuView.Add(title);
                CreateMenuHighlightEffects(posterMenuView, ("PosterMenu/" + menuNumber), spotLight);
                posterMenuView.PositionY = 200.0f;

                // Create show & hide effects
                Effect showEffect = new Effect(posterMenuView, Constants.ShowPosterDuration);
                showEffect.EndAction = Animation.EndActions.Cancel;
                showEffect.AddAction(new Effect.AnimateTo(posterMenuName, "ColorAlpha", 1.0f, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine)));
                showEffect.AddAction(new Effect.AnimateTo(posterMenuName, "PositionY", 0.0f, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine)));
                posterMenu.ShowMenuEffect = showEffect;
                Effect hideEffect = new Effect(posterMenuView, Constants.ShowPosterDuration);
                hideEffect.EndAction = Animation.EndActions.StopFinal;
                hideEffect.Finished += OnHidePosterEffectFinished;
                hideEffect.Tag = menuNumber;
                hideEffect.AddAction(new Effect.AnimateTo(posterMenuName, "ColorAlpha", 0.0f, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine)));
                hideEffect.AddAction(new Effect.AnimateTo(posterMenuName, "PositionY", 200.0f, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine)));
                posterMenu.HideMenuEffect = hideEffect;

                _posterMenus.Add(posterMenu);
            }
        }

        private bool AutomationHandler(object sender, Automation.AutomationEventBase automationEvent)
        {
            AutomationConfiguration.TVAutomationEvent tvEvent = automationEvent as AutomationConfiguration.TVAutomationEvent;
            // Move the keyboard focus automatically, and update the delay until the next automation event.
            _keyboardFocusManager.MoveFocus(tvEvent.direction);
            return true;
        }

        // Initialisation
        private void OnInitialize()
        {
            // Initialize menus.
            TVConfiguration tvConfiguration = new TVConfiguration();
            _configuration = tvConfiguration.GetConfiguration();

            _window = Window.Instance;
            _window.SetOpaqueState(false);
            _windowSize = _window.Size;
            _resourcePath = Constants.ResourcePath;
            _posterMenuHideStack = new List<int>();

            // Precalculations for re-use:
            // The poster-menu height is based on factors for scalability.
            // The height of the poster-menu is increased to take into account the amount revealed by the lower-menu animation.
            _posterMenuHeight = _windowSize.Height * (Constants.TopContainerHeightFactor + (Constants.MenuContainerHeightFactor * Constants.MenuContainerHideFactor));
            // Y position is the % of the screen occupied by the menu, minus the amount of the screen that the menu hides by.
            // This is so when the menu is partially hidden, all of the posters can be seen exactly.
            // We increase the Hide factor to give more overlap as the hide animation overshoots when it bounces.
            _posterMenuY = _windowSize.Height * (Constants.MenuContainerHeightFactor - (Constants.MenuContainerHeightFactor * Constants.MenuContainerHideFactor * 1.5f));

            if (!Constants.TransparentWindow)
            {
                ImageView backgroundImage = new ImageView(_resourcePath + "/images/Backgrounds/source_bg.png");
                backgroundImage.Name = "backgroundImage";
                backgroundImage.WidthResizePolicy = ResizePolicyType.FillToParent;
                backgroundImage.HeightResizePolicy = ResizePolicyType.Fixed;
                backgroundImage.Size2D = new Size2D(0, _windowSize.Height - (int)(_posterMenuHeight - _posterMenuY));
                backgroundImage.ParentOrigin = ParentOrigin.TopCenter;
                backgroundImage.PivotPoint = PivotPoint.TopCenter;
                backgroundImage.PositionUsesPivotPoint = true;
                _window.Add(backgroundImage);
            }

            // Create a SpotLight for items / images of both Poster and Menu ScrollContainers
            ImageView spotLight = new ImageView(_resourcePath + "/images/HighlightEffect/highlight-spotlight.png");
            spotLight.Name = "spotLight";
            spotLight.WidthResizePolicy = ResizePolicyType.UseNaturalSize;
            spotLight.HeightResizePolicy = ResizePolicyType.UseNaturalSize;
            spotLight.ParentOrigin = ParentOrigin.Center;
            spotLight.PivotPoint = PivotPoint.Center;
            spotLight.PositionUsesPivotPoint = true;

            // Create the poster menu in the middle of the screen
            _posterMenuContainer = new ImageView(_resourcePath + "/images/Backgrounds/background.png");
            _posterMenuContainer.WidthResizePolicy = ResizePolicyType.FillToParent;
            _posterMenuContainer.HeightResizePolicy = ResizePolicyType.Fixed;
            _posterMenuContainer.Size2D = new Size2D(0, (int)_posterMenuHeight);
            _posterMenuContainer.PositionUsesPivotPoint = true;
            _posterMenuContainer.ParentOrigin = ParentOrigin.BottomCenter;
            _posterMenuContainer.PivotPoint = PivotPoint.BottomCenter;
            _posterMenuContainer.Position = new Position(0.0f, -_posterMenuY, 0.0f);
            _window.Add(_posterMenuContainer);
            CreatePosterMenus(spotLight);
            _posterMenuContainer.Add(_posterMenus[0].ScrollMenu());
            _currentMenuIndex = 1;
            UpdateActivePosterMenu(0);

            _menuContainer = new ImageView(_resourcePath + "/images/Backgrounds/background.png");
            _menuContainer.Name = "menuContainer";
            _menuContainer.WidthResizePolicy = ResizePolicyType.FillToParent;
            _menuContainer.HeightResizePolicy = ResizePolicyType.Fixed;
            _menuContainer.Size2D = new Size2D(0, (int)(_windowSize.Height * Constants.MenuContainerHeightFactor));
            _menuContainer.PositionUsesPivotPoint = true;
            _menuContainer.ParentOrigin = ParentOrigin.BottomRight;
            _menuContainer.PivotPoint = PivotPoint.BottomRight;
            _window.Add(_menuContainer);

            // Create the lower menu and items.
            Size2D sourceMenuSize = new Size2D(Convert.ToInt32(_windowSize.Width - Constants.SystemMenuWidth), Convert.ToInt32(_windowSize.Height * Constants.MenuContainerHeightFactor));
            Size2D sourceMenuItemSize = new Size2D(Convert.ToInt32((_windowSize.Width * Constants.MenuItemWidthFactor) - Constants.SourceMenuPadding), Convert.ToInt32(_windowSize.Height * Constants.MenuItemHeightFactor));
            _menu = new SourceMenu(sourceMenuSize, sourceMenuItemSize);

            // Create the showing and hiding menu effects.
            _menu.ShowMenuEffect = new Effect(_menuContainer, Constants.ShowMenuDuration);
            _menu.ShowMenuEffect.EndAction = Animation.EndActions.Cancel;
            _menu.ShowMenuEffect.AddAction(new Effect.AnimateTo("menuContainer", "Position", new Position(0.0f, 0.0f, 0.0f), new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine)));

            // Hide animation bounces away.
            _menu.HideMenuEffect = new Effect(_menuContainer, Constants.ShowMenuDuration);
            _menu.HideMenuEffect.EndAction = Animation.EndActions.Cancel;
            float yPosition = _windowSize.Height * Constants.MenuContainerHeightFactor * Constants.MenuContainerHideFactor;
            AlphaFunction hideAlphaFunction = new AlphaFunction(new Vector2(0.32f, 0.08f), new Vector2(0.38f, 1.72f));
            _menu.HideMenuEffect.AddAction(new Effect.AnimateTo("menuContainer", "Position", new Position(0.0f, yPosition, 0.0f), hideAlphaFunction));

            CreateMenuHighlightEffects(_menu.ScrollMenu(), "SourceMenu", spotLight);
            _menuContainer.Add(_menu.ScrollMenu());
            _moveSourceMenuAnimation = new Animation(Constants.ShowMenuDuration);
            _moveSourceMenuAnimation.EndAction = Animation.EndActions.Cancel;

            // Create the system menu on the bottom left of the screen.
            _systemMenu = new SystemMenu(new Size2D(Convert.ToInt32(Constants.SystemMenuWidth), Convert.ToInt32(_windowSize.Height * Constants.MenuContainerHeightFactor)),
                                          new Position2D(-_windowSize.Width / 2, _windowSize.Height / 2));
            _menuContainer.Add(_systemMenu.GetView());

            // Initialize PreFocusChange event of KeyboardFocusManager
            _keyboardFocusManager = FocusManager.Instance;
            _keyboardFocusManager.PreFocusChange += OnKeyboardPreFocusChangeSignal;
            _keyboardFocusManager.FocusIndicator = new View(); //todo
            _keyboardFocusManager.SetAsFocusGroup(_systemMenu.GetView(), true);
            _keyboardFocusManager.SetAsFocusGroup(_menu.ScrollMenu(), true);
            for (int i = 0; i < _posterMenus.Count; ++i)
            {
                _keyboardFocusManager.SetAsFocusGroup(_posterMenus[i].ScrollMenu(), true);
            }

            _keyboardFocusManager.FocusGroupLoop = false;

            // Set up automation if in benchmarking mode.
            if (_benchmarkMode)
            {
                _automation = new Automation();
                _automation.EventSignal += AutomationHandler;
                AutomationConfiguration automationConfiguration = new AutomationConfiguration();
                automationConfiguration.Configure(_automation, _menu.Count);
                _automation.Start();
            }

            _actionReminder = new ActionReminder(_posterMenuContainer);
            _actionReminder.GetView().ParentOrigin = new Position(0.07f, -0.25f, 0.5f);
            _actionReminderTickCounter = 0;
            _actionReminderTimePeriod = Constants.ActionReminderTimePeriod;
            _actionReminderRemoteShowTicks = Constants.ActionReminderRemoteShowTicks;
            _actionReminderTimer = new Timer(_actionReminderTimePeriod);
            _actionReminderTimer.Tick += ActionReminderTimerHandler;

            // Start the timer that will fire the animation sequence when triggered.
            _actionReminderTimer.Start();

            // Move Focus to Bottom Container (Menu ScrollContainer)
            _menu.ScrollMenu().SetActive(true);
            _keyboardFocusManager.SetCurrentFocusView(_menu.ScrollMenu().GetCurrentFocusedItem());

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
                Tizen.Log.Info("Key", e.Key.KeyPressedName);
                if (e.Key.KeyPressedName == "XF86Back")
                {
                    this.Exit();
                }
            }
        }

        private bool ActionReminderTimerHandler(object o, EventArgs e)
        {
            if (++_actionReminderTickCounter >= _actionReminderRemoteShowTicks)
            {
                // Play the Action-Reminder remote control animation.
                _actionReminder.Show();
                _actionReminderTickCounter = 0;
            }

            // Pulse the currently selected menu item.
            if (_menu.ScrollMenu().IsActive)
            {
                _menu.ScrollMenu().Pulse();
            }
            else if (_posterMenus[_currentMenuIndex].ScrollMenu().IsActive)
            {
                _posterMenus[_currentMenuIndex].ScrollMenu().Pulse();
            }

            return true;
        }

    }
}
