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
    /// The class of ScrollContainer.
    /// </summary>
    public class ScrollContainer : CustomView
    {
        // View Container will be the first item added to ScrollContainer
        // and parent to all the items added to the ScrollContainer.
        private View _container;
        // Size of the item / images added to the ScrollContainer.
        private Size2D _itemSize;
        // List collection of View items/images  added to the ScrollContainer.
        private List<View> _itemList;
        // Number of items / images  added to the ScrollContainer.
        private int _itemCount;
        // Index of currently focused View item / image  on the ScrollContainer.
        private int _focusedItem;
        // Used for horizontal scroll position.
        private float _currentScrollPosition;
        // Used for gap / padding between items / images on the ScrollContainer.
        private float _gap;
        // Width of the ScrollContainer.
        private float _width;
        // Height of the ScrollContainer.
        private float _height;
        // Flag to check if ScrollContainer is enabled or not.
        private bool _isFocused;
        // Extra horizontal margin is used to add an extra gap between
        // items / images after a focused and scaled item / image.
        private float _marginX;
        // Extra vertical margin (not used at the moment).
        private float _marginY;
        // Vertical Position offset Factor of item height.
        private float _offsetYFactor;
        // Horizontal Position offset of ScrollContainer.
        private float _offsetX;
        // Reference to Dali stage.
        private Window _stage;
        // Reference to Dali stage size.
        private Size2D _stageSize;
        // Reference to Shadow border ImageView applied to the focused item on ScrollContainer.
        private ImageView _shadowBorder;
        // Reference to SpotLight ImageView applied to the focused item on ScrollContainer.
        private ImageView _spotLight;
        // SpotLight Animation applied to the focused item on ScrollContainer.
        private Animation _spotLightAnimation;
        // Focused position animation on ScrollContainer.
        private Animation _focusAnimation;
        // Scroll animation on items of ScrollContainer.
        private Animation _scrollAnimation;
        // Focus Transition (scaling /unscaling) animation on items of ScrollContainer.
        private Animation _focusTransitionAnimation;
        // Circular path used for SpotLight Animation
        // applied to the focused item on ScrollContainer.
        private Path _circularPath;

        static CustomView CreateInstance()
        {
            return new ScrollContainer();
        }

        /// <summary>
        /// static constructor registers the control type (for user can add kinds of visuals to it)
        /// </summary>
        static ScrollContainer()
        {
            // ViewRegistry registers control type with DALi type registery
            // also uses introspection to find any properties that need to be registered with type registry
            //ViewRegistry.Instance.Register(CreateInstance, typeof(ScrollContainer));
            CustomViewRegistry.Instance.Register(CreateInstance, typeof(ScrollContainer));
        }

        /// <summary>
        /// The constructor of scrollContainer.
        /// </summary>
        public ScrollContainer() : base(typeof(ScrollContainer).FullName, CustomViewBehaviour.DisableStyleChangeSignals |
                                        CustomViewBehaviour.RequiresKeyboardNavigationSupport)
        {
        }

        /// <summary>
        /// Get _isFocused.
        /// </summary>
        public bool IsFocused
        {
            get
            {
                return _isFocused;
            }
        }

        /// <summary>
        /// Get _container.
        /// </summary>
        public Tizen.NUI.BaseComponents.View Container
        {
            get
            {
                return _container;
            }
        }

        /// <summary>
        /// Get _itemCount.
        /// </summary>
        public int ItemCount
        {
            get
            {
                return _itemCount;
            }
        }

        /// <summary>
        /// Get/Set _itemSize.
        /// </summary>
        public Size2D ItemSize
        {
            get
            {
                return _itemSize;
            }

            set
            {
                _itemSize = value;

                Position topLeft = new Position(-0.25f * _itemSize.Width, -0.25f * _itemSize.Height, 0.0f);
                Position topRight = new Position(0.25f * _itemSize.Width, -0.25f * _itemSize.Height, 0.0f);
                Position bottomRight = new Position(0.25f * _itemSize.Width, 0.25f * _itemSize.Height, 0.0f);
                Position bottomLeft = new Position(-0.25f * _itemSize.Width, 0.25f * _itemSize.Height, 0.0f);

                _circularPath = new Path();
                _circularPath.AddPoint(topLeft);
                _circularPath.AddPoint(topRight);
                _circularPath.AddPoint(bottomRight);
                _circularPath.AddPoint(bottomLeft);
                _circularPath.AddPoint(topLeft);
                _circularPath.GenerateControlPoints(0.5f);
            }
        }

        /// <summary>
        /// Get/Set _gap.
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
        /// Get/Set _marginX.
        /// </summary>
        public float MarginX
        {
            get
            {
                return _marginX;
            }

            set
            {
                _marginX = value;
            }
        }

        /// <summary>
        /// Get/Set _offsetYFactor.
        /// </summary>
        public float OffsetYFator
        {
            get
            {
                return _offsetYFactor;
            }

            set
            {
                _offsetYFactor = value;
            }
        }

        /// <summary>
        /// Get/Set _offsetX.
        /// </summary>
        public float OffsetX
        {
            get
            {
                return _offsetX;
            }

            set
            {
                _offsetX = value;
            }
        }

        /// <summary>
        /// Get/Set _marginY.
        /// </summary>
        public float MarginY
        {
            get
            {
                return _marginY;
            }

            set
            {
                _marginY = value;
            }
        }

        /// <summary>
        /// Get/Set _width.
        /// </summary>
        public float Width
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;
            }
        }

        /// <summary>
        /// Get/Set _height.
        /// </summary>
        public float Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
            }
        }

        /// <summary>
        /// Get/Set _shadowBorder.
        /// </summary>
        public ImageView ShadowBorder
        {
            get
            {
                return _shadowBorder;
            }

            set
            {
                _shadowBorder = value;
            }
        }

        /// <summary>
        /// Get/Set _spotLight.
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
        /// Get _focusedItem.
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
        /// This override method is called automatically after the Control has been initialized.
        /// Any second phase initialization is done here.
        /// </summary>
        public override void OnInitialize()
        {
            _itemSize = new Size2D(0, 0);
            _gap = 0.0f;
            _width = 0.0f;
            _height = 0.0f;
            _currentScrollPosition = 0.0f;
            _itemCount = 0;
            _focusedItem = -1;
            _isFocused = false;
            _marginX = 50.0f;
            _marginY = 0.0f;
            _offsetYFactor = 0.0f;
            _offsetX = 0.0f;

            _container = new View();
            this.Add(_container);

            _itemList = new List<View>();

            this.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            this.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            this.WidthResizePolicy = ResizePolicyType.FillToParent;
            this.HeightResizePolicy = ResizePolicyType.FillToParent;
            this.Focusable = (true);

            _container.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            _container.Position = new Position(0, 50, 0);
            _container.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            _container.WidthResizePolicy = ResizePolicyType.FillToParent;
            _container.HeightResizePolicy = ResizePolicyType.FillToParent;

            _stage = Window.Instance;
            _stageSize = _stage.Size;

            _spotLightAnimation = new Animation(Constants.SpotLightDuration);
            _focusTransitionAnimation = new Animation(Constants.FocusTransitionDuration);
            _focusAnimation = new Animation(Constants.FocusDuration);
            _focusAnimation.EndAction = Animation.EndActions.StopFinal;
            _scrollAnimation = new Animation(Constants.ScrollDuration);
            _scrollAnimation.EndAction = Animation.EndActions.StopFinal;

            //changed to internal
            //EnableGestureDetection(Gesture.Type.Pan);
        }

        /// <summary>
        /// This will be invoked automatically if an item/image is added to the ScrollContainer
        /// </summary>
        /// <param name="actor">the child view</param>
        public override void OnChildAdd(View actor)
        {
            View item = actor;
            //View item = actor as View;

            if (item is View && item != _container)
            {
                item.PivotPoint = Tizen.NUI.PivotPoint.BottomCenter;
                item.PositionUsesPivotPoint = true;
                item.ParentOrigin = Tizen.NUI.ParentOrigin.BottomCenter;

                item.Size2D = _itemSize;
                item.HeightResizePolicy = ResizePolicyType.Fixed;
                item.WidthResizePolicy = ResizePolicyType.Fixed;
                item.Focusable = (true);
                item.Position = GetItemPosition(_itemCount, _currentScrollPosition);
                item.Name = _itemCount.ToString();

                _container.Add(item);
                _itemList.Add(item);

                _itemCount++;
            }
        }

        /// <summary>
        /// This will be invoked automatically if an item/image
        /// is removed from the ScrollContainer
        /// </summary>
        /// <param name="actor">the child view will be removed</param>
        public override void OnChildRemove(View actor)
        {
            View item = actor;

            if (item is View && item != _container)
            {
                _container.Remove(item);

                _itemCount--;
                _itemList.Remove(item);
            }
        }

        /// <summary>
        /// This override function supports two dimensional keyboard navigation.
        /// This function returns the next keyboard focusable actor in ScrollContainer control towards the given direction.
        /// </summary>
        /// <param name="currentFocusedView">current Focused View</param>
        /// <param name="direction">direction</param>
        /// <param name="loopEnabled">loop Enabled</param>
        /// <returns>the next keyboard focusable actor in ScrollContainer control towards the given direction</returns>returns>
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

        public override void OnFocusChangeCommitted(View commitedFocusableView)
        {
            Focus(_focusedItem);
        }


        // This override function is invoked before chosen focusable actor will be focused.
        // This allows the application to preform any actions (i.e. Scroll and SpotLight animations) before the focus is actually moved to the chosen actor.

        // This override function is invoked whenever a pan gesture is detected on this control.
        // Perform Scroll Animation based upon pan gesture velocity / speed.
        /*public override void OnPan(PanGesture pan)
        {
          return;  //currently not used
        }*/

        /// <summary>
        /// This function returns current focused view
        /// </summary>
        /// <returns>
        /// current focused view.
        /// </returns>
        public View GetCurrentFocusedActor()
        {
            if (_focusedItem < 0)
            {
                _focusedItem = 0;
            }

            return _itemList[_focusedItem];
        }

        /// <summary>
        /// This function returns current focused view
        /// </summary>
        /// <param name="focused">focused?</param>
        public void SetFocused(bool focused)
        {
            _isFocused = focused;

            // Perform Focus animation if the ScrollContainer is not focused already
            if (!_isFocused)
            {
                _focusTransitionAnimation.Clear();

                for (int i = 0; i < _itemList.Count; ++i)
                {
                    Position targetPosition = GetItemPosition(i, _currentScrollPosition);

                    _focusTransitionAnimation.AnimateTo(_itemList[i], "Position", targetPosition, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));

                    _focusTransitionAnimation.AnimateTo(_itemList[i], "Scale", new Vector3(1.0f, 1.0f, 1.0f), new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));
                }

                _focusTransitionAnimation.Play();
            }
            else
            {
                Focus(_focusedItem);
            }
        }

        /// <summary>
        /// Obtain ID of first visible item/image on the screen of the ScrollContainer
        /// </summary>
        /// <returns>
        /// Obtain ID of first visible item/image on the screen of the ScrollContainer
        /// </returns>
        private int GetFirstVisibleItemId()
        {
            int firstItemId = -1;

            if (_isFocused)
            {
                firstItemId = (int)Math.Floor((-1.0 * _currentScrollPosition + _marginX * 2.0f) / (_itemSize.Width + _gap));
            }
            else
            {
                firstItemId = (int)Math.Floor(-1.0 * _currentScrollPosition / (_itemSize.Width + _gap));
            }

            if (firstItemId < 0)
            {
                firstItemId = 0;
            }

            return firstItemId;
        }

        /// <summary>
        /// Obtain ID of last visible item/image on the screen of the ScrollContainer
        /// </summary>
        /// <returns>
        /// Obtain ID of last visible item/image on the screen of the ScrollContainer
        /// </returns>
        private int GetLastVisibleItemId()
        {
            int lastItemId = -1;

            if (_isFocused)
            {
                lastItemId = (int)Math.Ceiling(((_width - _currentScrollPosition - _marginX * 2.0f) / _itemSize.Width) - 1);
            }
            else
            {
                lastItemId = (int)Math.Ceiling(((_width - _currentScrollPosition) / _itemSize.Width) - 1);
            }

            if (lastItemId >= _itemList.Count)
            {

                lastItemId = _itemList.Count - 1;
            }

            return lastItemId;
        }

        /// <summary>
        /// Obtain Next item/image (Right of the currently focused item) of the ScrollContainer
        /// </summary>
        /// <param name="loopEnabled">loop Enabled</param>
        /// <returns>
        /// Obtain Next item/image (Right of the currently focused item) of the ScrollContainer
        /// </returns>
        private View FocusNext(bool loopEnabled)
        {
            int nextItem = -1;

            if (_focusedItem < GetFirstVisibleItemId() || _focusedItem > GetLastVisibleItemId())
            {
                nextItem = GetFirstVisibleItemId();
            }
            else
            {
                nextItem = _focusedItem + 1;
            }

            if (nextItem >= _itemList.Count)
            {
                if (loopEnabled)
                {
                    nextItem = 0;
                }
                else
                {
                    nextItem = _itemList.Count - 1;
                }
            }

            _focusedItem = nextItem;
            return _itemList[_focusedItem];
        }

        /// <summary>
        /// Obtain Previous item/image (left of the currently focused item) of the ScrollContainer
        /// </summary>
        /// <param name="loopEnabled">loop Enabled</param>
        /// <returns>
        /// Obtain Previous item/image (left of the currently focused item) of the ScrollContainer
        /// </returns>
        private View FocusPrevious(bool loopEnabled)
        {
            int previousItem = -1;

            if (_focusedItem < GetFirstVisibleItemId() || _focusedItem > GetLastVisibleItemId())
            {
                previousItem = GetFirstVisibleItemId();
            }
            else
            {
                previousItem = _focusedItem - 1;
            }

            if (previousItem < 0)
            {
                if (loopEnabled)
                {
                    previousItem = _itemList.Count - 1;
                }
                else
                {
                    previousItem = 0;
                }
            }

            _focusedItem = previousItem;
            return _itemList[_focusedItem];
        }

        /// <summary>
        /// Perform ScrollAnimation on each item
        /// </summary>
        /// <param name="amount">amount</param>
        /// <param name="baseItem">base item</param>
        private void Scroll(float amount, int baseItem)
        {
            float tagetScrollPosition = _currentScrollPosition + amount;
            float totalItemSize = _itemList.Count * (_itemSize.Width + _gap) + _gap + (_marginX * 2.0f);

            float maxScrollPosition = _width - totalItemSize;

            if (tagetScrollPosition < maxScrollPosition)
            {
                tagetScrollPosition = maxScrollPosition;
            }

            if (tagetScrollPosition > 0.0f)
            {
                tagetScrollPosition = 0.0f;
            }

            _scrollAnimation.Clear();

            for (int i = 0; i < _itemList.Count; ++i)
            {
                Position targetPosition = GetItemPosition(i, tagetScrollPosition);

                _scrollAnimation.AnimateTo(_itemList[i], "Position", targetPosition, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));
            }

            _currentScrollPosition = tagetScrollPosition;
            _scrollAnimation.Play();
        }

        /// <summary>
        /// This function uses ItemId as next FocusedItem
        /// and preforms Scroll and SpotLight animations on that item.
        /// </summary>
        /// <param name="itemId">item id</param>
        private void Focus(int itemId)
        {

            if (itemId < 0)
            {
                itemId = 0;
            }
            else if (itemId >= _itemList.Count)
            {
                itemId = _itemList.Count - 1;
            }
            FocusManager.Instance.SetCurrentFocusView(_itemList[itemId]);
            // Perform Spot Light animation
            if (_focusedItem != itemId && _spotLight != null)
            {
                _spotLightAnimation.Clear();
                _spotLightAnimation.AnimatePath(_spotLight, _circularPath, new Vector3(0.0f, 0.0f, 0.0f));
                _spotLightAnimation.Looping = true;
                _spotLightAnimation.Play();
            }

            _focusedItem = itemId;

            Position itemPosition = GetItemPosition(_focusedItem, _currentScrollPosition);

            _focusAnimation.Clear();

            float relativeItemPositionX = itemPosition.X - _itemSize.Width * 0.5f + (_stageSize.Width * 0.5f) + _offsetX;
            if (relativeItemPositionX < _marginX + _offsetX + _gap)
            {
                float amount = _marginX + _offsetX + _gap - relativeItemPositionX;
                Scroll(amount, itemId + 1); // Perform Scroll animation
            }
            else if (relativeItemPositionX + _itemSize.Width + _gap + _marginX > _stageSize.Width)
            {
                float amount = relativeItemPositionX + _marginX + _gap + _itemSize.Width - _stageSize.Width;
                Scroll(-amount, itemId - 1); // Perform Scroll animation
            }
            else
            {
                // Perform animation when item is focused
                for (int i = 0; i < _itemList.Count; ++i)
                {
                    Position targetPosition = GetItemPosition(i, _currentScrollPosition);

                    _focusAnimation.AnimateTo(_itemList[i], "Position", targetPosition, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));
                }
            }

            for (int i = 0; i < _itemList.Count; ++i)
            {
                // Perform scale animation on Focused item
                if (i == _focusedItem)
                {
                    _focusAnimation.AnimateTo(_itemList[i], "Scale", new Vector3(1.2f, 1.2f, 1.2f), new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));
                }
                else
                {

                    _focusAnimation.AnimateTo(_itemList[i], "Scale", new Vector3(1.0f, 1.0f, 1.0f), new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSine));
                }
            }

            _focusAnimation.Play();

        }

        /// <summary>
        /// Calculate Position of any item/image of ScrollContainer
        /// </summary>
        /// <param name="itemId">item id</param>
        /// <param name="scrollPosition">scroll position</param>
        /// <returns>
        /// Position of any item/image of ScrollContainer
        /// </returns>
        private Position GetItemPosition(int itemId, float scrollPosition)
        {
            if (_isFocused)
            {
                // used (_itemSize.Width * 0.5f) because of AnchorPointCenter
                // used (_stageSize.Width * 0.5f) because of ParentOriginCenter
                if (_focusedItem > itemId)
                {
                    float positionX = (_itemSize.Width * itemId) + (_gap * (itemId + 1)) + scrollPosition + (_itemSize.Width * 0.5f) - (_stageSize.Width * 0.5f);
                    return new Position(positionX, -_itemSize.Height * _offsetYFactor, 0.0f);
                }
                else if (_focusedItem == itemId)
                {
                    float positionX = (_itemSize.Width * itemId) + (_gap * (itemId + 1)) + scrollPosition + _marginX + (_itemSize.Width * 0.5f) - (_stageSize.Width * 0.5f);
                    return new Position(positionX, -_itemSize.Height * _offsetYFactor, 0.0f);
                }
                else
                {
                    float positionX = (_itemSize.Width * itemId) + (_gap * (itemId + 1)) + scrollPosition + _marginX * 2.0f + (_itemSize.Width * 0.5f) - (_stageSize.Width * 0.5f);
                    return new Position(positionX, -_itemSize.Height * _offsetYFactor, 0.0f);
                }
            }
            else
            {
                float positionX = (_itemSize.Width * itemId) + (_gap * (itemId + 1)) + scrollPosition + (_itemSize.Width * 0.5f) - (_stageSize.Width * 0.5f);
                return new Position(positionX, -_itemSize.Height * _offsetYFactor, 0.0f);
            }
        }


    }

}

