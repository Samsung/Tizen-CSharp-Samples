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
using Tizen.NUI.Constants;
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace FirstScreen
{
    /// <summary>
    /// ScrollMenu, a View which contains a list of item.
    /// </summary>
    public class ScrollMenu : CustomView
    {
        /// <summary>
        /// ScrollItem.
        /// </summary>
        public class ScrollItem
        {
            public View item;
            public View title;
            public View shadow;
            public View reflection;
        }
        // The list of items (and their components) for this ScrollMenu
        private List<ScrollItem> _items;
        // Size of the item / images added to the ScrollMenu.
        private Size2D _itemDimensions;
        // Index of currently focused View item / image  on the ScrollMenu.
        private int _focusedItem;
        private int _itemToFocus;
        // Used for horizontal scroll position.
        private float _currentScrollPosition;
        // Used for gap / padding between items / images on the ScrollMenu.
        private float _gap;
        private bool _focusCenter;
        // Flag to check if ScrollMenu is enabled or not.
        private bool _isActive;
        // Extra horizontal margin is used to add an extra gap between items / images after a focused and scaled item / image.
        private float _margin;
        private float _scrollEndMargin;
        // Reference to Dali stage size.
        private float _width;
        private ImageView _spotlightClip;
        // Reference to SpotLight ImageView applied to the focused item on ScrollMenu.
        private ImageView _spotLight;
        private ImageView _shine;
        private float _focusScale;
        private int _scrollEndAnimationDuration;
        private long _lastScrollEndAnimationTime;
        private bool _firstActive;
        private bool _holdFocusWhileInactive;
        private bool _startActive;
        // Focused position animation on ScrollMenu.
        private Animation _focusAnimation;
        // Scroll animation on items of ScrollMenu.
        private Animation _scrollAnimation;
        // Focus Transition (scaling /unscaling) animation on items of ScrollMenu.
        private Animation _scrollEndAnimation;
        private Effect _selectionEffect;
        private Effect _pulseEffect;

        /// <summary>
        /// Called by DALi Builder if it finds a ScrollMenu control in a JSON file
        /// </summary>
        /// <returns>return a ScrollMenu instance</returns>
        static CustomView CreateInstance()
        {
            return new ScrollMenu();
        }

        /// <summary>
        /// Static constructor registers the control type (for user can add kinds of visuals to it)
        /// </summary>
        static ScrollMenu()
        {
            // ViewRegistry registers control type with DALi type register
            // also uses introspection to find any properties that need to be registered with type registry
            CustomViewRegistry.Instance.Register(CreateInstance, typeof(ScrollMenu));
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ScrollMenu() : base(typeof(ScrollMenu).Name, CustomViewBehaviour.DisableStyleChangeSignals | CustomViewBehaviour.RequiresKeyboardNavigationSupport)
        {
        }

        /// <summary>
        /// Flag to check if ScrollMenu is enabled or not.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
        }

        /// <summary>
        /// The count of the item in this scrollMenu.
        /// </summary>
        public int ItemCount
        {
            get
            {
                return _items.Count;
            }
        }

        /// <summary>
        /// Size of the item / images added to the ScrollMenu
        /// </summary>
        public Size2D ItemDimensions
        {
            get
            {
                return _itemDimensions;
            }

            set
            {
                _itemDimensions = value;
            }
        }

        /// <summary>
        /// Used for gap / padding between items / images on the ScrollMenu.
        /// </summary>
        public float Gap
        {
            get
            {
                return _gap;
            }

            set
            {
                _gap = value;
            }
        }

        /// <summary>
        /// Extra horizontal margin is used to add an extra gap between items / images after a focused and scaled item / image.
        /// </summary>
        new public float Margin
        {
            get
            {
                return _margin;
            }

            set
            {
                _margin = value;
            }
        }

        /// <summary>
        /// Get/Set the value of ScrollEndMargin
        /// </summary>
        public float ScrollEndMargin
        {
            get
            {
                return _scrollEndMargin;
            }

            set
            {
                _scrollEndMargin = value;
            }
        }

        /// <summary>
        /// Get/Set the value of the FocusScale.
        /// It used for the scale animation when the item get focus.
        /// </summary>
        public float FocusScale
        {
            get
            {
                return _focusScale;
            }

            set
            {
                _focusScale = value;
            }
        }

        /// <summary>
        /// Reference to SpotLightClip ImageView applied to the focused item on ScrollMenu.
        /// </summary>
        public ImageView SpotLightClip
        {
            get
            {
                return _spotlightClip;
            }

            set
            {
                _spotlightClip = value;
            }
        }

        /// <summary>
        /// Reference to SpotLight ImageView applied to the focused item on ScrollMenu.
        /// </summary>
        public ImageView SpotLight
        {
            get
            {
                return _spotLight;
            }

            set
            {
                _spotLight = value;
            }
        }

        /// <summary>
        /// Reference to Shin ImageView applied to the focused item on ScrollMenu.
        /// </summary>
        public ImageView Shine
        {
            get
            {
                return _shine;
            }

            set
            {
                _shine = value;
            }
        }

        /// <summary>
        /// Get/Set the value of the FocusCenter.
        /// If it is true, we will keep the selected item in the center.
        /// If it is false, we allow the selection to move without scrolling if possible.
        /// </summary>
        public bool FocusCenter
        {
            get
            {
                return _focusCenter;
            }

            set
            {
                _focusCenter = value;
            }
        }

        /// <summary>
        /// Get/Set the value of HoldFocusWhileInactive
        /// If it is true, we are holding focus while becoming inactive.
        /// If it is false, we will set no focus item.
        /// </summary>
        public bool HoldFocusWhileInactive
        {
            get
            {
                return _holdFocusWhileInactive;
            }

            set
            {
                _holdFocusWhileInactive = value;
            }
        }

        /// <summary>
        /// Get/Set the value of StartActive.
        /// If it is false, we will set the scrollMenu to inactive.
        /// </summary>
        public bool StartActive
        {
            get
            {
                return _startActive;
            }

            set
            {
                _startActive = value;
            }
        }

        /// <summary>
        /// Index of currently focused View item / image  on the ScrollMenu.
        /// </summary>
        public int FocusedItemID
        {
            get
            {
                if (_focusedItem < 0)
                {
                    _focusedItem = 0;
                }

                return _focusedItem;
            }
        }

        /// <summary>
        /// Get/Set the Value of SelectionEffect.
        /// </summary>
        public Effect SelectionEffect
        {
            get
            {
                return _selectionEffect;
            }

            set
            {
                _selectionEffect = value;
            }
        }

        /// <summary>
        /// Get/Set the Value of PulseEffect.
        /// </summary>
        public Effect PulseEffect
        {
            get
            {
                return _pulseEffect;
            }

            set
            {
                _pulseEffect = value;
            }
        }

        /// <summary>
        /// This override method is called automatically after the Control has been initialized.
        /// Any second phase initialization is done here.
        /// </summary>
        public override void OnInitialize()
        {
            _itemDimensions = new Size2D(0, 0);
            _gap = 0.0f;
            _currentScrollPosition = 0.0f;
            _focusedItem = 0;
            _itemToFocus = 0;
            _isActive = false;
            _margin = 50.0f;
            _scrollEndMargin = 0.0f;
            _lastScrollEndAnimationTime = 0;
            _focusCenter = false;
            _holdFocusWhileInactive = false;
            _startActive = true;
            _firstActive = true;
            _focusScale = 1.2f;
            _scrollEndAnimationDuration = Constants.ScrollEndAnimationDuration;
            _items = new List<ScrollItem>();
            _focusAnimation = new Animation(Constants.FocusDuration);
            _focusAnimation.EndAction = Animation.EndActions.Cancel;
            _scrollAnimation = new Animation(Constants.ScrollDuration);
            _scrollAnimation.EndAction = Animation.EndActions.Cancel;

            this.Focusable = true;
        }

        /// <summary>
        /// Add a new it to scrollMenu
        /// </summary>
        /// <param name="scrollItem">the new item</param>
        public void AddItem(ScrollItem scrollItem)
        {
            if (scrollItem.item)
            {
                View item = scrollItem.item;
                item.PositionUsesPivotPoint = true;
                item.ParentOrigin = Tizen.NUI.ParentOrigin.Center;
                item.Size2D = _itemDimensions;

                if (scrollItem.title)
                {
                    item.Add(scrollItem.title);
                }

                // If we have a shadow or reflection, we need an extra container to provide the correct draw order.
                if (scrollItem.shadow || scrollItem.reflection)
                {
                    View itemContainer = new View();
                    itemContainer.PositionUsesPivotPoint = true;
                    itemContainer.PivotPoint = Tizen.NUI.PivotPoint.Center;
                    itemContainer.ParentOrigin = Tizen.NUI.ParentOrigin.Center;
                    itemContainer.Size2D = _itemDimensions;
                    itemContainer.Focusable = true;
                    itemContainer.Position = GetItemPosition(_items.Count, _currentScrollPosition, false);
                    
                    if (scrollItem.reflection)
                    {
                        scrollItem.reflection.PositionUsesPivotPoint = true;
                        scrollItem.reflection.Size2D = _itemDimensions;
                        scrollItem.reflection.PivotPoint = Tizen.NUI.PivotPoint.BottomCenter;
                        scrollItem.reflection.ParentOrigin = Tizen.NUI.ParentOrigin.BottomCenter;
                        scrollItem.reflection.Position = new Position(0.0f, 40.0f, 0.0f);
                        scrollItem.reflection.Scale = new Vector3(1.0f, -1.0f, 1.0f);
                        scrollItem.reflection.SetProperty(scrollItem.reflection.GetPropertyIndex("ColorAlpha"), new PropertyValue(0.3f));
                        itemContainer.Add(scrollItem.reflection);
                    }

                    if (scrollItem.shadow)
                    {
                        itemContainer.Add(scrollItem.shadow);
                    }

                    itemContainer.Add(item);
                    Add(itemContainer);
                    // Replace the root item with the new container.
                    scrollItem.item = itemContainer;
                }
                else
                {
                    // Item only, set it as focusable and position it directly.
                    item.Focusable = true;
                    item.Position = GetItemPosition(_items.Count, _currentScrollPosition, false);
                    Add(item);
                }

                _items.Add(scrollItem);
            }
        }

        /// <summary>
        /// This override function supports two dimensional keyboard navigation.
        /// This function returns the next keyboard focusable actor in ScrollMenu control towards the given direction.
        /// </summary>
        /// <param name="currentFocusedView"> the current focused view</param>
        /// <param name="direction">the direction of the focus move</param>
        /// <param name="loopEnabled">the focus in scrollmenu is loop enabled or not</param>
        /// <returns>next focusable view or current focused view</returns>
        public override View GetNextFocusableView(View currentFocusedView, View.FocusDirection direction, bool loopEnabled)
        {
            if (direction == View.FocusDirection.Left)
            {
                return FocusPrevious(loopEnabled);
            }
            else if (direction == View.FocusDirection.Right)
            {
                return FocusNext(loopEnabled);
            }
            else
            {
                return currentFocusedView;
            }
        }

        /// <summary>
        /// Informs this control that its chosen focusable view will be focused.
        /// This allows the application to preform any actions it wishes before the focus is actually moved to the chosen view.
        /// </summary>
        /// <param name="committedFocusableView">The committed focused view</param>
        public override void OnFocusChangeCommitted(View committedFocusableView)
        {
            FocusItem(_itemToFocus);
            ScrollTo(_itemToFocus);
        }

        /// <summary>
        /// This function returns current focused view
        /// </summary>
        /// <returns>return the current focused view</returns>
        public View GetCurrentFocusedItem()
        {
            if (_itemToFocus < 0)
            {
                _itemToFocus = 0;
            }

            return _items[_itemToFocus].item;
        }

        /// <summary>
        /// This function used to set the active or inactive of the scrollMenu
        /// </summary>
        /// <param name="active">the flag of active</param>
        public void SetActive(bool active)
        {
            _isActive = active;
            int focusItem = _itemToFocus;

            // Perform Focus animation if the ScrollMenu is not focused already
            if (!_isActive)
            {
                _selectionEffect.UnStage();

                // If we are holding focus while becoming inactive (EG. A tab keeping its context) then we do no further animation.
                if (_holdFocusWhileInactive)
                {
                    return;
                }

                // Set no focus item, as ee are becoming inactive.
                focusItem = -1;
            }

            // We force the focus as the item may be the same as the previous focus call.
            FocusItem(focusItem, true);
        }

        /// <summary>
        /// Obtain Next item/image (Right of the currently focused item) of the ScrollMenu
        /// </summary>
        /// <param name="loopEnabled">the flag of loop enable</param>
        /// <returns>return the next item of the scrollMenu</returns>
        private View FocusNext(bool loopEnabled)
        {
            _itemToFocus = _focusedItem + 1;
            if (_itemToFocus >= _items.Count)
            {
                if (loopEnabled)
                {
                    _itemToFocus = 0;
                }
                else
                {
                    _itemToFocus = _items.Count - 1;
                    ScrollEndAnimation(false);
                }
            }

            return _items[_itemToFocus].item;
        }

        /// <summary>
        /// Obtain Previous item/image (left of the currently focused item) of the ScrollMenu
        /// </summary>
        /// <param name="loopEnabled">the flag of loop enable</param>
        /// <returns>return the previous item of the scrollMenu</returns>
        private View FocusPrevious(bool loopEnabled)
        {
            _itemToFocus = _focusedItem - 1;
            if (_itemToFocus < 0)
            {
                if (loopEnabled)
                {
                    _itemToFocus = _items.Count - 1;
                }
                else
                {
                    _itemToFocus = 0;
                    ScrollEndAnimation(true);
                }
            }

            return _items[_itemToFocus].item;
        }

        private void ScrollEndAnimation(bool right)
        {
            // Block the end animation from being run if it was run recently.
            long timeNow = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            if (timeNow < (_lastScrollEndAnimationTime + (long)_scrollEndAnimationDuration))
            {
                return;
            }

            _lastScrollEndAnimationTime = timeNow;

            if (!_scrollEndAnimation)
            {
                _scrollEndAnimation = new Animation(_scrollEndAnimationDuration);
                _scrollEndAnimation.EndAction = Animation.EndActions.Discard;
            }
            else
            {
                _scrollEndAnimation.Clear();
            }

            // Animate all items in the direction.
            float direction = right ? 1.0f : -1.0f;
            for (int i = 0; i < _items.Count; ++i)
            {
                float endDelta = (right ? i : (_items.Count - 1) - i);
                float distance = 40.0f - (endDelta * 7.0f);
                if (distance > 0.0f)
                {
                    _scrollEndAnimation.AnimateBy(_items[i].item, "Position", new Vector3(distance * direction, 0.0f, 0.0f), new AlphaFunction(AlphaFunction.BuiltinFunctions.Bounce));
                }
            }

            // Rotate only the end item.
            int endItem = right ? 0 : _items.Count - 1;
            _scrollEndAnimation.AnimateBy(_items[endItem].item, "Orientation", new Rotation(new Radian(new Degree(30.0f * direction)), new Vector3(0.0f, 1.0f, 0.0f)), new AlphaFunction(AlphaFunction.BuiltinFunctions.Sin));
            _scrollEndAnimation.Play();
        }

        public void Pulse()
        {
            // Play the pulse effect on the currently selected item.
            _pulseEffect.Play(_items[_focusedItem].item);

            // Re-play the selection effect.
            _selectionEffect.Play();
        }

        /// <summary>
        /// This function uses ItemId as next FocusedItem and preforms Scroll and SpotLight animations on that item.
        /// </summary>
        /// <param name="itemId">the id of the item</param>
        /// <param name="force">force, default value is false</param>
        private void FocusItem(int itemId, bool force = false)
        {
            float focusedMargin = _margin;
            bool noFocus = itemId < 0;
            if (noFocus)
            {
                // If we want to render as if we have no focus, remove the focus margin.
                itemId = _focusedItem;
                focusedMargin = 0.0f;
            }

            // Perform Spot Light animation
            if (force || noFocus || (_focusedItem != itemId))
            {
                // Note: The focus shine and the bounce animation use the same animation. We use different end times here so the user can specify different durations for each.
                if (!noFocus && _spotLight != null && _spotlightClip != null)
                {
                    _spotlightClip.Add(_spotLight);

                    // Stage and Play the selection effect.
                    _selectionEffect.Show(_items[itemId].item);
                }

                // Iterate through all items animating to their new target scales.
                _focusAnimation.Clear();
                AlphaFunction focusedAlphaFunction = new AlphaFunction(new Vector2(0.32f, 0.08f), new Vector2(0.38f, 1.72f));
                AlphaFunction normalAlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine);
                Vector3 focusedScale = new Vector3(_focusScale, _focusScale, _focusScale);
                Vector3 normalScale = new Vector3(1.0f, 1.0f, 1.0f);
                for (int i = 0; i < _items.Count; ++i)
                {
                    bool focused = !noFocus && (i == itemId);
                    _focusAnimation.AnimateTo(_items[i].item, "Scale", focused ? focusedScale : normalScale, 0, Constants.FocusDuration, focused ? focusedAlphaFunction : normalAlphaFunction);
                }

                _focusAnimation.Play();
            }

            // The focused item should appear above all other items.
            _items[itemId].item.RaiseToTop();
        }

        /// <summary>
        /// Scroll to the position of the focused item.
        /// If the instant is "true", it will force this to happen instantly so there is no visible animation initially.
        /// </summary>
        /// <param name="itemId">The ID of the focused item</param>
        /// <param name="force">The flag of force</param>
        /// <param name="instant">The flag of instant</param>
        private void ScrollTo(int itemId, bool force = false, bool instant = false)
        {
            float focusedMargin = _margin;
            if (!_isActive || itemId < -1)
            {
                focusedMargin = 0.0f;
            }

            if (itemId < -1)
            {
                itemId = _focusedItem;
            }

            if (itemId != _focusedItem || force)
            {
                float amount = 0.0f;

                // Check the selection mode.
                if (_focusCenter)
                {
                    // FocusCenter keeps the selected item in the center.
                    // Therefore this will always scroll if the selection has changed. Calculate the delta.
                    amount = (_focusedItem - itemId) * (_itemDimensions.Width + _gap);
                    _focusedItem = itemId;
                }
                else
                {
                    // We allow the selection to move without scrolling if possible.
                    // Calculate if we need to scroll (if we have reached the end of the row). Then scroll if necessary.
                    _focusedItem = itemId;
                    Position itemPosition = GetItemPosition(itemId, _currentScrollPosition, !_isActive);
                    float relativeItemPositionX = itemPosition.X - _itemDimensions.Width * 0.5f + (_width * 0.5f);
                    float scrollEndTotal = _scrollEndMargin + _margin;

                    if (relativeItemPositionX < scrollEndTotal + _gap)
                    {
                        amount = scrollEndTotal + _gap - relativeItemPositionX;
                    }
                    else if (relativeItemPositionX + ItemDimensions.Width + _gap + scrollEndTotal > _width)
                    {
                        amount = -(scrollEndTotal + _gap + relativeItemPositionX + _itemDimensions.Width - _width);
                    }
                }

                _currentScrollPosition += amount;
                float totalItemSize = _items.Count * (_itemDimensions.Width + _gap) + _gap + (focusedMargin * 2.0f);
                float maxScrollPosition = _width - totalItemSize;
                _scrollAnimation.Clear();
                _scrollAnimation.Duration = instant ? 0 : Constants.ScrollDuration;

                for (int i = 0; i < _items.Count; ++i)
                {
                    Position targetPosition = GetItemPosition(i, _currentScrollPosition, !_isActive);
                    Rotation targetRotation = GetItemOrientation(targetPosition.X);

                    if (_items[i].title != null)
                    {
                        // Create a subtle parallax effect for the title text by exaggerating the position past the center of the menu.
                        float targetTitlePosition = (targetPosition.X / _width) * 10.0f;
                        _scrollAnimation.AnimateTo(_items[i].title, "PositionX", targetTitlePosition, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));
                    }

                    if (_items[i].shadow != null)
                    {
                        float relativeOffset = Math.Abs(targetPosition.X) / (_width * 2.0f);
                        float targetOpacity = 1.0f - (relativeOffset * 2.0f);
                        float scale = 1.0f + (relativeOffset * 1.6f);
                        Vector3 targetScale = new Vector3(scale, scale, scale);
                        float targetShadowPosition = (relativeOffset * 22.0f) - 45.0f;

                        _scrollAnimation.AnimateTo(_items[i].shadow, "Opacity", targetOpacity, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));
                        _scrollAnimation.AnimateTo(_items[i].shadow, "PositionY", targetShadowPosition, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));
                        _scrollAnimation.AnimateTo(_items[i].shadow, "Scale", targetScale, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));

                        if (_items[i].reflection != null)
                        {
                            float targetReflectionPosition = (relativeOffset * 220.0f) + 75.0f;
                            float targetReflectionOpacity = 0.5f - (relativeOffset * 3.0f);
                            _scrollAnimation.AnimateTo(_items[i].reflection, "PositionY", targetReflectionPosition, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));
                            _scrollAnimation.AnimateTo(_items[i].reflection, "Opacity", targetReflectionOpacity, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));
                        }
                    }

                    _scrollAnimation.AnimateTo(_items[i].item, "Position", targetPosition, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));
                    _scrollAnimation.AnimateTo(_items[i].item, "Orientation", targetRotation, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));
                }

                _scrollAnimation.Play();
            }
        }

        /// <summary>
        /// Calculate Position of any item/image of ScrollContainer.
        /// </summary>
        /// <param name="itemId">The ID of the item</param>
        /// <param name="scrollPosition">The scrollPosition of the item</param>
        /// <param name="noFocus">The flag of the focus, if the Item has focus, it will be true</param>
        /// <returns>item position</returns>
        private Position GetItemPosition(int itemId, float scrollPosition, bool noFocus)
        {
            float focusedMargin = _margin;
            if (noFocus)
            {
                focusedMargin = 0.0f;
            }

/*          
            TODO: 3D positioning effect
            float positionX = (_itemSize.Width + _gap) * (itemId) + scrollPosition;
            float angleNormalized = 0.75f + (positionX / (_width * 2.0f));
            float positionZ = 750.0f * (float)Math.Sin(angleNormalized * 360.0f * (Math.PI / 180.0f));
            float xDamping = Math.Min(positionX * positionX * 0.00022f, _itemSize.Width);
            positionX += (positionX < 0.0f) ? xDamping : -xDamping;
            return new Position(positionX, 0.0f, positionZ);
*/
            float positionX = ((_itemDimensions.Width + _gap) * itemId) + scrollPosition;
            if (_isActive)
            {
                if (_focusedItem < itemId)
                {
                    positionX += focusedMargin * 2.0f;
                }
                else if (_focusedItem == itemId)
                {
                    positionX += focusedMargin;
                }
            }

            return new Position(positionX, 0.0f, 0.0f);
        }

        private Rotation GetItemOrientation(float positionX)
        {
/*
            TODO: 3D rotation effect
            float angleNormalized = -(positionX / (_stageSize * 2.0f)) * 4.5f;
            return new Rotation(new Radian(angleNormalized), new Vector3(0.0f, 1.0f, 0.0f));
*/
            return new Rotation();
        }

        /// <summary>
        /// Called after the size negotiation has been finished for this ScrollMenu.
        /// The control is expected to assign this given size to itself or its children.
        /// </summary>
        /// <param name="size">The allocated size.</param>
        /// <param name="container">The control should add views to this container that it is not able to allocate a size for.</param>
        public override void OnRelayout(Vector2 size, RelayoutContainer container)
        {
            _width = size.X;

            // Only setup initial scroll position on first layout.
            if (_firstActive)
            {
                _firstActive = false;
                if (_focusCenter)
                {
                    // We chose a sensible first item to focus.
                    // To do this we work out how many items would be visible so we can position the 1st one on the left, and focus the item in the middle of the screen.
                    // w = item-width  |  c = container-width  |  g = gap between items  |  x = number of items to the left of the center selected one.
                    // So: w/2 + x(w + g) + g = c/2
                    // Solving for x:  x = (c/2 - g - w/2) / (w + g)
                    // We omit the extra gap on the left as it is better to show an item if possible.
                    _itemToFocus = (int)Math.Floor((double)(((_width / 2.0f) - (ItemDimensions.Width / 2.0f)) / (ItemDimensions.Width + _gap)));
                    
                    // We align the items to the left to fit as many on screen as possible, but if there are less items than will fit on the screen,
                    // we shouldn't move them past left of centered. So focus the center item (of all items) in this case.
                    if (_itemToFocus > (_items.Count / 2))
                    {
                        _itemToFocus = _items.Count / 2;
                    }
                }
                else
                {
                    // We ensure the 1st item is always on the far left of the container.
                    _currentScrollPosition += ((_itemDimensions.Width - _width) * 0.5f) - GetItemPosition(0, _currentScrollPosition, false).X;

                    // Check if the number of items we have is less than can fit in the container width.
                    // If so, offset the items so they appear centered rather than far left aligned.
                    float itemsLength = ((float)_items.Count * (ItemDimensions.Width + _gap)) + _gap;
                    float itemsTotalLength = itemsLength + (_scrollEndMargin * 2.0f);

                    if (itemsTotalLength < _width)
                    {
                        // Add the offset.
                        _currentScrollPosition += ((_width - itemsLength) / 2.0f) + _scrollEndMargin;
                    }
                }

                // Scroll to the position of the focused item. The last "true" forces this to happen instantly so there is no visible animation initially.
                ScrollTo(_itemToFocus, true, true);

                if (!_startActive)
                {
                    SetActive(false);
                }
            }
        }

    }
}
