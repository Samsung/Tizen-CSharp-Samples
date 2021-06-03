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
using System;
using System.Collections.Generic;
using System.Linq;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

/// <summary>
/// Namespace for Tizen.NUI package
/// </summary>
namespace ChannelList
{
    /// <summary>
    /// Enumeration for the focus move direction.
    /// </summary>
    public enum MoveDirection
    {
        /// <summary>
        /// Move focus to up.
        /// </summary>
        Up,
        /// <summary>
        /// Move focus to down.
        /// </summary>
        Down,
        /// <summary>
        /// Move focus to left.
        /// </summary>
        Left,
        /// <summary>
        /// Move focus to right.
        /// </summary>
        Right
    }

    /// <summary>
    /// A view that shows items in a vertically scrolling list. The items 
    /// come from the ListAdapter associated with this view.
    /// </summary>
    /// <code>
    /// List v2 = new List();
    /// v2.Name = "List";
    /// v2.BackgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    /// v2.Size = new Size(622, 800, 0);
    /// v2.AnchorPoint = AnchorPoint.TopLeft;
    /// v2.ParentOrigin = ParentOrigin.TopLeft;
    /// v2.Position = new Position(100, 100, 0);
    /// Window.Instance.GetDefaultLayer().Add(v2);
    /// List<object> dataList = new List<object>();
    /// for (int i = 0; i< 100; ++i)
    /// {
    ///     dataList.Add(new ListItem1Data(i));
    /// }
    /// ListAdapter mAdapter = new TestListAdapter(dataList);
    /// v2.SetAdapter(mAdapter);
    /// </code>
    public class List : View
    {
        /// <summary>
        /// List event types
        /// </summary>
        public enum ListEventType
        {
            /// <summary> 
            /// Event triggered when focus change
            /// </summary>
            FocusChange,
            /// <summary>
            /// Event triggered when item scroll in
            /// </summary>
            ItemScrolledIn,
            /// <summary>
            /// Event triggered when item scroll out
            /// </summary>
            ItemScrolledOut,
            /// <summary>
            /// Event triggered when focus move out of list
            /// </summary>
            FocusMoveOut
        }

        /// <summary>
        /// List event args at event call back, user can get information
        /// </summary>
        public class ListEventArgs : EventArgs
        {
            /// <summary>
            /// List event type
            /// </summary>
            public ListEventType EventType
            {
                get;
                set;
            }

            /// <summary>
            /// List event data
            /// </summary>
            public int[] param = new int[4];
        }

        /// <summary>
        /// The constructor
        /// </summary>
        public List()
        {
            //initial the list
            Initialize();
        }

        /// <summary>
        /// Dispose of List.
        /// </summary>
        /// <param name="type">Dispose caused type</param>
        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                //Called by User
                //Release your own managed resources here.
                //You should release all of your own disposable objects here.
                mFlagEnableRecycle = false;
                Clear();
            }

            //Release your own unmanaged resources here.
            //You should not access any managed member here except static instance.
            //because the execution order of Finalizes is non-deterministic.
            //Unreference this from if a static instance refer to this. 
            //You must call base.Dispose(type) just before exit.
            base.Dispose(type);
        }

        /// <summary>
        /// Get/Set focus item index of List.
        /// </summary>
        public int FocusItemIndex
        {
            get
            {
                Tizen.Log.Fatal("NUI.List", "get FocusItemIndex : " + mCurFocusItemIndex);
                return mCurFocusItemIndex; 
            }

            set
            {
                Tizen.Log.Fatal("NUI.List", "FocusItemIndex set to: " + value);
                //check index value is valid
                if (!IsValidItemIndex(value))
                {
                    return;
                }
                // check focusable
                if (Focusable == true)
                {
                    SetFocus(value);
                }
                else
                {
                    mCurFocusItemIndex = value;
                }

                Tizen.Log.Fatal("NUI.List", "FocusItemIndex set to: " + mCurFocusItemIndex);
            }
        }

        /// <summary>
        /// Get/Set Preload front item size.
        /// </summary>
        public int PreloadFrontItemSize
        {
        	get
            {
        		return mPreloadFrontItemSize; 
        	}

        	set
        	{
        		mPreloadFrontItemSize = value;
        	}
        }

        /// <summary>
        /// Get/Set Preload front item size.
        /// </summary>
        public int PreloadBackItemSize
        {
        	get
            {
        		return mPreloadBackItemSize; 
        	}

        	set
        	{
        		mPreloadBackItemSize = value;
        	}
        }

        /// <summary> 
        /// Get/Set space between items 
        /// </summary>
        public int ItemSpace
        {
            get { return mItemSpace; }
            set { mItemSpace = value; }
        }

        /// <summary>
        /// Get/Set enable flag whether enable recycle bin, when unload item.
        /// <remarks>default value is true, don't change except special requirement.</remarks> 
        /// </summary>
        public bool StateRecycleEnable
        {
            get { return mFlagEnableRecycle; }
            set { mFlagEnableRecycle = value; }
        }

        /// <summary>
        /// Get number of items of List.
        /// </summary>
        public int NumOfItem
        {
            get { return mItemList.Count; }
        }

        /// <summary>
        /// Update View specified by the itemIndex.
        /// </summary>
        /// <param name="itemIndex">The index of item of List.</param>
        public void UpdateItem(int itemIndex)
        {
            Tizen.Log.Fatal("NUI.List", "Update item  " + itemIndex);
            if (IsValidItemIndex(itemIndex) == false)
            {
                Tizen.Log.Fatal("NUI.List", "Index out of range");
                return;
            }

            if (itemIndex < mHeadItemIndex || itemIndex > mTailItemIndex)
            {
                Tizen.Log.Fatal("NUI.List", "Index out of visible area");
                return;
            }

            ListItem item = mItemList[itemIndex];

            if (item.itemView != null)
            {
                mAdapter.UpdateItem(itemIndex, item.itemView);//???
            }
        }

        /// <summary>
        /// Sets the data behind this List.
        /// </summary>
        /// <param name="adapter">The ListAdapter which is responsible for maintaining the data
        /// backing this list and for producing a view to represent an item in the data set.</param>
        public void SetAdapter(ListAdapter adapter)
        {
            Tizen.Log.Fatal("NUI.List", "SetAdapter...");
            Tizen.Log.Fatal("NUI.List", "mListItemCount: " + mListItemCount);
            ResetList();

            this.mAdapter = adapter;

            //request layout
            mListItemCount = adapter.GetCount();

            ListItem item = null;

            Tizen.Log.Fatal("NUI.List", "SetAdapter...mListItemCount:" + mListItemCount);
            for (int i = 0; i < mListItemCount; ++i)
            {
                item = new ListItem();
                mItemList.Add(item);
                item.index = i;
                
                if (mAttrsApplied == true)
                {
                    item.rect.X = 0;
                    item.rect.Y = (i == 0) ? 0 : mItemList[i - 1].rect.Bottom() + mItemSpace;
                    item.rect.Width = this.SizeWidth - (mMargin[0] + mMargin[2]);
                    item.rect.Height = adapter.GetItemHeight(i); ;
                }

            }

            adapter.DataChangeEvent += OnDataChange;
            SetViewTypeCount(mAdapter.GetItemTypeCount());
            Load();
        }

        /// <summary>
        /// Get the ListAdapter of List.
        /// </summary>
        /// <returns>The ListAdapter of List.</returns>
        public ListAdapter GetAdapter()
        {
            return mAdapter;
        }

        /// <summary>
        /// Page up this List and Change focus according to the page size.
        /// </summary>
        public void PageUp()
        {
            Tizen.Log.Fatal("NUI.List", "page up...");
            if (mAttrsApplied == false)
            {
                return;
            }

            int itemCount = PageItemNum();

            if (itemCount >= mCurFocusItemIndex)
            {
                SetFocus(0);
            }
            else
            {
                int focusIndex = mCurFocusItemIndex - itemCount;

                mItemGroupRect.Y = (mItemGroupRect.Y + this.SizeHeight) > 0 ? 0 : (mItemGroupRect.Y + mItemList[mCurFocusItemIndex].rect.Y - mItemList[focusIndex].rect.Y);

                UpdateInMemoryItems(mItemGroupRect);
                mItemGroup.PositionY = mItemGroupRect.Y;
                SetFocus(focusIndex);
            }
        }

        /// <summary>
        /// Page down this List and Change focus according to the page size.
        /// </summary>
        public void PageDown()
        {
            Tizen.Log.Fatal("NUI.List", "page down...");
            if (mAttrsApplied == false)
            {
                return;
            }

            int itemCount = PageItemNum();

            if (mItemList.Count - itemCount - 1 <= mCurFocusItemIndex)
            {
                SetFocus(mItemList.Count - 1);
            }
            else
            {
                int focusIndex = mCurFocusItemIndex + itemCount;
                float moveDis = mItemList[focusIndex].rect.Y - mItemList[mCurFocusItemIndex].rect.Y;
                mItemGroupRect.Y = (mItemGroupRect.Y + mItemGroupRect.Height - this.SizeHeight) < moveDis ? this.SizeHeight - mItemGroupRect.Height : (mItemGroupRect.Y - moveDis);

                UpdateInMemoryItems(mItemGroupRect);
                mItemGroup.PositionY = mItemGroupRect.Y;

                SetFocus(focusIndex);
            }
        }

        /// <summary>
        /// Scroll item whoes index is firstShowItemIndex to the top of list view.
        /// </summary>
        /// <param name="firstShowItemIndex"> The first item index on dest page </param>
        public void ScrollList(int firstShowItemIndex)
        {
            if (IsValidItemIndex(firstShowItemIndex) == false || mItemGroupRect.Height <= SizeHeight)
            {
                return;
            }

            ListItem item = mItemList.ElementAt(firstShowItemIndex);

            float posYOnList = item.rect.Y + mItemGroupRect.Y;
            if (mItemGroupRect.Y - posYOnList <= SizeHeight - mItemGroupRect.Height)
            {
                mItemGroupRect.Y = SizeHeight - mItemGroupRect.Height;
            }
            else
            {
                mItemGroupRect.Y = mItemGroupRect.Y - posYOnList;
            }

            UpdateInMemoryItems(mItemGroupRect);
            mItemGroup.PositionY = mItemGroupRect.Y;
            ChangeFocus(firstShowItemIndex);
            mScrollIndex = mCurFocusItemIndex;
        }

        /// <summary>
        /// Move focus according to parameter direction
        /// </summary>
        /// <param name="direction">Focus move direction</param>
        public void MoveFocus(MoveDirection direction)
        {
            Tizen.Log.Fatal("NUI.List", "Move Focus...Direction: " + direction);
            if (MoveDirection.Up == direction)
            {
                FocusItemIndex--;
            }
            else if (MoveDirection.Down == direction)
            {
                FocusItemIndex++;
            }
        }

        /// <summary>
        /// Get loaded item view in List.
        /// </summary>
        /// <param name="itemIndex">Specified item index</param>
        /// <returns> Return the ItemView, if the item is unloaded, returns null.</returns>
        public object GetLoadedItemView(int itemIndex)
        {
            ListItem item = null;
            //get the valid loaded item view
            if (IsValidItemIndex(itemIndex))
            {
                item = mItemList[itemIndex];
            }

            return item?.itemView;
        }

        /// <summary>
        /// List event handler
        /// </summary>
        public event EventHandler<ListEventArgs> ListEvent
        {
            add
            {
                mListEventHandlers += value;
            }

            remove
            {
                mListEventHandlers -= value;
            }
        }

        /// <summary>
        /// Update when List attributes changed.
        /// </summary>
        protected void OnUpdate()
        {
            Tizen.Log.Fatal("NUI.List", "OnUpdate...");
            //set top shadow view
            if (mTopShadowView == null)
            {
                mTopShadowView = new ImageView();
                mTopShadowView.Name = "TopShadowView";
                this.Add(mTopShadowView);
            }

            //set bottom shadow view
            if (mBottomShadowView == null)
            {
                mBottomShadowView = new ImageView();
                mBottomShadowView.Name = "BottomShadowView";
                this.Add(mBottomShadowView);
            }

            //set attribute applied flag
            mAttrsApplied = true;

            //update item rect and load list
            if (mAdapter != null)
            {
                // Update Items rect
                for (int i = 0; i < mItemList.Count(); i++)
                {
                    ListItem item = mItemList[i];
                    item.rect.X = 0;
                    item.rect.Y = (i == 0) ? 0 : mItemList[i - 1].rect.Bottom() + mItemSpace;
                    item.rect.Width = this.SizeWidth - (mMargin[0] + mMargin[2]);
                    item.rect.Height = mAdapter.GetItemHeight(item.index);
                }

                Load();
            }
        }

        private void OnListEvent(ListEventArgs e)
        {
            Tizen.Log.Fatal("NUI.List", "OnListEvent... ");
            if (mListEventHandlers != null)
            {
                mListEventHandlers(this, e);
            }
        }

        /// <summary>
        /// Load Item in the List.
        /// </summary>
        private void Load()
        {
            Tizen.Log.Fatal("NUI.List", "Load...");

            Tizen.Log.Fatal("NUI.List", "mAttrsApplied: " + mAttrsApplied);
            mFlagFirstIn = true;
            mListItemCount = mAdapter.GetCount();
            if (mListItemCount == 0)
            {
                return;
            }

            Tizen.Log.Fatal("NUI.List", "mListItemCount: " + mListItemCount);

            //update item group size
            UpdateItemGroupSize();

            //initial item group position
            InitItemGroupPos();

            //update loaded item
            UpdateInMemoryItems(mItemGroupRect);

            //update focus
            Tizen.Log.Fatal("NUI.List", "mCurFocusItemIndex: " + mCurFocusItemIndex);
            if (Focusable == true)
            {
                SetFocus(mCurFocusItemIndex);
            }

            Tizen.Log.Fatal("NUI.List", "Load out.");
        }

        /// <summary>
        /// Add a new Item to the list.
        /// </summary>
        private void AddItem()
        {
            Tizen.Log.Fatal("NUI.List", "AddItem...mItemList.Count:" + mItemList.Count + " mAttrsApplied:" + mAttrsApplied);
            ListItem item = new ListItem();
            item.index = mItemList.Count;
            mItemList.Add(item);

            //set new item rect
            item.rect.X = 0;
            item.rect.Y = (item.index == 0) ? 0 : mItemList[item.index - 1].rect.Bottom() + mItemSpace;
            item.rect.Width = this.SizeWidth - (mMargin[0] + mMargin[2]);
            item.rect.Height = mAdapter.GetItemHeight(item.index);

            //update item group size
            UpdateItemGroupSize();

            //load the new item
            if (item.index <= mTailItemIndex + PreloadBackItemSize)
            {
                LoadItem(item);
            }

            //update head item index and tail item index
            mHeadItemIndex = GetItemIndexByPosY(-mItemGroupRect.Y, true);
            mTailItemIndex = GetItemIndexByPosY(-mItemGroupRect.Y + this.SizeHeight, false);
            Tizen.Log.Fatal("NUI.List", "mTailItemIndex: " + mTailItemIndex);

            //update focus
            if (mCurFocusItemIndex == -1 && Focusable == true)
            {
                SetFocus(0);
            }
        }

        /// <summary>
        /// Add all items according to list count
        /// </summary>
        /// <param name="count">the count of the item</param>
        private void AddAll(int count)
        {
            Tizen.Log.Fatal("NUI.List", "AddAll...count: " + mTailItemIndex);
            //add each item
            for (int i = 0; i < count; i++)
            {
                AddItem();
            }

            //update focus
            if (Focusable == true)
            {
                SetFocus(mCurFocusItemIndex < 0 ? 0 : mCurFocusItemIndex);
            }
        }

        /// <summary>
        /// Delete items at specified index and number.
        /// </summary>
        /// <param name="fromItemIndex">the index of the first item</param>
        /// <param name="deleteItemNum">the number of the item</param>
        private void DeleteItem(int fromItemIndex, int deleteItemNum)
        {
            Tizen.Log.Fatal("NUI.List", "DeleteItem...fromItemIndex: " + fromItemIndex + ", deleteItemNum: " + deleteItemNum);
            if (fromItemIndex < 0 || fromItemIndex + deleteItemNum > mItemList.Count)
            {
                //item index out of range
                return;
            }

            int toItemIndex = fromItemIndex + deleteItemNum - 1;

            //calculate start pos for item which behind deleted items
            float startPosOfItemGroup = 0;
            if (0 == fromItemIndex)
            {
                startPosOfItemGroup = 0;
            }
            else
            {
                startPosOfItemGroup = mItemList[fromItemIndex - 1].rect.Bottom();
            }

            //update pos of items behind deleted items
            for (int i = toItemIndex + 1; i <= mItemList.Count - 1; ++i)
            {
                mItemList[i].rect.Y = startPosOfItemGroup + mItemSpace;
                startPosOfItemGroup += mItemList[i].rect.Height;
            }

            for (int i = fromItemIndex; i <= toItemIndex; i++)
            {
                ListItem item = mItemList[i];
                if (null != mCurInMemoryItemList.Find((o) => { return o == item; }))
                {
                    if (i == mCurFocusItemIndex)
                    {
                        item.itemView.Scale = new Vector3(1.0f, 1.0f, 1.0f);
                    }

                    UnloadItem(item);

                    mCurInMemoryItemList.Remove(item);
                }
            }

            mItemList.RemoveRange(fromItemIndex, deleteItemNum);

            if (mItemList.Count == 0)
            {
                mItemGroupRect.Height = 0;
                mItemGroup.PositionY = mItemGroupRect.Y;
                mItemGroup.SizeHeight = mItemGroupRect.Height;

                mHeadItemIndex = -1;
                mTailItemIndex = -1;
                return;
            }

            //refresh item pos
            for (int i = fromItemIndex; i <= mItemList.Count - 1; ++i)
            {
                ListItem item = mItemList[i];
                item.index = i;

                RefreshItemPosition(item);
            }

            mItemGroupRect.Height = mItemList[mItemList.Count - 1].rect.Bottom();


            //update focus item index
            int tempFocusIndex = mCurFocusItemIndex;
            if (mCurFocusItemIndex > toItemIndex)
            {
                mCurFocusItemIndex -= deleteItemNum;
            }
            else if (mCurFocusItemIndex >= mItemList.Count)
            {
                mCurFocusItemIndex = mItemList.Count - 1;
            }

            if (mCurFocusItemIndex != tempFocusIndex)
            {
                while (!IsValidItemIndex(mCurFocusItemIndex))
                {
                    mCurFocusItemIndex--;
                    if (mCurFocusItemIndex == -1)
                    {
                        break;
                    }
                }
            }

            if (mHeadItemIndex <= toItemIndex && mTailItemIndex >= fromItemIndex)
            {
                if (mTailItemIndex >= mItemList.Count)
                {
                    mTailItemIndex = mItemList.Count - 1;
                }

                Position itemGroupPos = new Position(0, 0, 0);
                bool flagScrollItem = CalItemGroupPosByFocusItemIndex(mCurFocusItemIndex, itemGroupPos);
                mItemGroupRect.Y = itemGroupPos.Y;

                mHeadItemIndex = -1;
                mTailItemIndex = -1;
                UpdateInMemoryItems(mItemGroupRect);

                mFlagFirstIn = true;
                ChangeFocus(mCurFocusItemIndex);
            }
            else if (mHeadItemIndex > toItemIndex)
            {
                for (int i = fromItemIndex; i <= toItemIndex; i++)
                {
                    ListItem item = mItemList[i];
                    mItemGroupRect.Y += item.rect.Height + mItemSpace;
                }
            }

            mItemGroup.PositionY = mItemGroupRect.Y;
            mItemGroup.SizeHeight = mItemGroupRect.Height;
        }

        /// <summary>
        /// Insert an item at specified index.
        /// </summary>
        /// <param name="index">the index of the item</param>
        private void InsertItem(int index)
        {
            Tizen.Log.Fatal("NUI.List", "index:" + index);
            ListItem item = null;
            item = new ListItem();
            item.index = index;
            mItemList.Insert(index, item);

            item.rect.X = 0;
            item.rect.Y = (index == 0) ? 0 : mItemList[index - 1].rect.Bottom() + mItemSpace;
            item.rect.Width = this.SizeWidth - (mMargin[0] + mMargin[2]);
            item.rect.Height = mAdapter.GetItemHeight(index);

            ListItem itemTemp = null;
            //update item index position
            for (int i = index + 1; i < mListItemCount; i++)
            {
                itemTemp = mItemList[i];
                itemTemp.index = i;
                itemTemp.rect.Y = mItemList[i - 1].rect.Bottom() + mItemSpace;
                if (itemTemp.itemView != null)
                {
                    itemTemp.itemView.PositionY = itemTemp.rect.Y;
                }
            }

            //update item group position
            if (mCurFocusItemIndex >= index)
            {
                mItemGroupRect.Y -= item.rect.Height;
                mItemGroup.PositionY = mItemGroupRect.Y;
                mCurFocusItemIndex++;
                mScrollIndex = mCurFocusItemIndex;
            }

            //update item group size
            UpdateItemGroupSize();

            //load the item
            //if (index >= (mHeadItemIndex <= currentAttrs.PreloadFrontItemSize ? 0 : mHeadItemIndex - currentAttrs.PreloadFrontItemSize) && index <= mTailItemIndex + currentAttrs.PreloadBackItemSize)
            if (index >= (mHeadItemIndex <= PreloadFrontItemSize ? 0 : mHeadItemIndex - PreloadFrontItemSize) && index <= mTailItemIndex + PreloadBackItemSize)
            {
                LoadItem(item);
            }

            //update tail item index
            mTailItemIndex = GetItemIndexByPosY(-mItemGroupRect.Y + this.SizeHeight, false);
        }

        /// <summary>
        /// Clear the list date set.
        /// </summary>
        private void Clear()
        {
            Tizen.Log.Fatal("NUI.List", "Clear...");
            foreach (ListItem item in mItemList)
            {
                item.Dispose();
            }

            Tizen.Log.Fatal("NUI.List", "mCurInMemoryItemList.Count: " + mCurInMemoryItemList.Count());
            foreach (ListItem item in mCurInMemoryItemList)
            {
                UnloadItem(item);
            }

            Tizen.Log.Fatal("NUI.List", "mUnloadItemList.Count: " + mUnloadItemList.Count());
            foreach (ListItem item in mUnloadItemList)
            {
                UnloadItem(item);
            }

            mUnloadItemList.Clear();
            mCurInMemoryItemList.Clear();

            mItemList.Clear();
            mCurFocusItemIndex = -1;
            mScrollIndex = 0;
            mHeadItemIndex = -1;
            mTailItemIndex = -1;

            Tizen.Log.Fatal("NUI.List", "...");
            InitItemGroupPos();
            UpdateItemGroupSize();

            ClearRecycleBin();
        }

        /// <summary>
        /// Called when the control gain key input focus.
        /// </summary>
        /// <param name="sender">the object</param>
        /// <param name="e">the args of the event</param>
        private void OnFocusGained(object sender, EventArgs e)
        {
            Tizen.Log.Fatal("NUI.List", "OnFocusGained.");

            int focusItemIndex = mCurFocusItemIndex < 0 ? 0 : mCurFocusItemIndex;

            SetFocus(focusItemIndex);
        }

        /// <summary>
        /// Called when the control loses key input focus.
        /// </summary>
        /// <param name="sender">the object</param>
        /// <param name="e">the args of the event</param>
        private void OnFocusLost(object sender, EventArgs e)
        {
            Tizen.Log.Fatal("NUI.List", "OnFocusLost.");
            //check current focus item index
            if (IsValidItemIndex(mCurFocusItemIndex))
            {
                ListItem focusOutItem = mItemList[mCurFocusItemIndex];
                mAdapter.FocusChange(mCurFocusItemIndex, focusOutItem.itemView, false);
            }

            mFlagFirstIn = true;
        }

        /// <summary>
        /// Called when the list data changed.
        /// </summary>
        /// <param name="o">the object</param>
        /// <param name="e">the args of the event</param>
        private void OnDataChange(object o, ListAdapter.DataChangeEventArgs e)
        {
            Tizen.Log.Fatal("NUI.List", "OnDataChange.");
            Tizen.Log.Fatal("NUI.List", "DataChangeType:" + e.ChangeType);

            //int oldItemCount = mListItemCount;
            mListItemCount = mAdapter.GetCount();

            switch (e.ChangeType)
            {
                //add an item
                case ListAdapter.DataChangeEventType.Add:
                    AddItem();
                    break;
                //add all items
                case ListAdapter.DataChangeEventType.AddAll:
                    AddAll((e.data as List<object>).Count);
                    break;
                //remove an item
                case ListAdapter.DataChangeEventType.Remove:
                    DeleteItem(e.param[0], e.param[1]);
                    break;
                //insert an item
                case ListAdapter.DataChangeEventType.Insert:
                    InsertItem(e.param[0]);
                    break;
                //clear items
                case ListAdapter.DataChangeEventType.Clear:
                    Clear();
                    break;
                //load an item
                case ListAdapter.DataChangeEventType.Load:
                    Load();
                    break;
                default:
                    break;
            }

            Tizen.Log.Fatal("NUI.List", "OnDataChange End");
        }

        /// <summary>
        /// Called when an new list constructed.
        /// </summary>
        private void Initialize()
        {
            Tizen.Log.Fatal("NUI.List", "Initialize...");
            ClippingMode = ClippingModeType.ClipChildren;

            // Create and Initialize item group 
            mItemGroup = new View();
            mItemGroup.Name = "ItemGroup";
            mItemGroup.PositionUsesPivotPoint = true;
            mItemGroup.ParentOrigin = Tizen.NUI.ParentOrigin.TopCenter;
            mItemGroup.PivotPoint = Tizen.NUI.PivotPoint.TopCenter;
            mItemGroup.WidthResizePolicy = ResizePolicyType.FillToParent;
            mItemGroup.HeightResizePolicy = ResizePolicyType.FitToChildren;
            this.Add(mItemGroup);

            //OnRelayoutEvent += OnRelayout;
            OnUpdate();

            //add focus gain and lost event
            FocusGained += OnFocusGained;
            FocusLost += OnFocusLost;

            Create();
        }

        /// <summary>
        /// Called when an new list was initialized.
        /// </summary>
        private void Create()
        {
            Tizen.Log.Fatal("NUI.List", "Create...");
            mScrollWayPointAni = new LinkerAnimation(mItemGroup, 100, 100, 0);

            /*unload front item when via event happen*/
            mScrollWayPointAni.ViaEvent += delegate(object o, EventArgs e)
            {
                Tizen.Log.Fatal("NUI.List", "mItemGroupRect: " + mItemGroupRect.X + ", " + mItemGroupRect.Y + ", " + mItemGroupRect.Width + ", " + mItemGroupRect.Height);
                Rect itemGroupVisualRect = new Rect(mItemGroup.PositionX, mItemGroup.PositionY, mItemGroup.SizeWidth, mItemGroup.SizeHeight);
                UpdateInMemoryItems(itemGroupVisualRect);

                int itemIndex = mFocusMoveQ.Dequeue();
                ChangeFocus(itemIndex);
                
                //if (mHeadItemIndex < (this.attrs as Attributes).PreloadFrontItemSize)
                if (mHeadItemIndex < PreloadFrontItemSize)
                {
                    return;
                }

                //check upload list and unload item
                if (mUnloadItemList.Count > 0)
                {
                    ListItem item = mUnloadItemList[0];
                    UnloadItem(item);
                    mUnloadItemList.Remove(item);
                }

                //if (itemIndex == 0)
                //{
                //    PerformCircular(true);
                //}
                //else if (itemIndex == mListItemCount - 1)
                //{
                //    PerformCircular(false);
                //}
            };
        }

        /// <summary>
        /// Initial list item group position.
        /// </summary>
        private void InitItemGroupPos()
        {
            Tizen.Log.Fatal("NUI.List", "InitItemGroupPos...");
            Tizen.Log.Fatal("NUI.List", "mAttrsApplied: " + mAttrsApplied + ", mCurFocusItemIndex: " + mCurFocusItemIndex);
            mItemGroupRect.X = 0; // Laid top-center on List
            mItemGroupRect.Y = mMargin[1];

            if (IsValidItemIndex(mCurFocusItemIndex))
            {
                Rect focusItemRect = mItemList[mCurFocusItemIndex].rect;
                if (mItemGroupRect.Y + focusItemRect.Bottom() > SizeHeight)
                {
                    if (IsValidItemIndex(mCurFocusItemIndex - 1))
                    {
                        mItemGroupRect.Y = mItemList[mCurFocusItemIndex - 1].rect.Height / 2 - mItemList[mCurFocusItemIndex - 1].rect.Bottom();
                    }
                }
            }

            //Initial item group position
            mItemGroup.PositionX = mItemGroupRect.X;
            mItemGroup.PositionY = mItemGroupRect.Y;
            Tizen.Log.Fatal("NUI.List", "mItemGroupRect: X:" + mItemGroupRect.X + ", Y: " + mItemGroupRect.Y + ", W: " + mItemGroupRect.Width + ", H: " + mItemGroupRect.Height);
        }

        /// <summary>
        /// update item group size.
        /// </summary>
        private void UpdateItemGroupSize()
        {
            Tizen.Log.Fatal("NUI.List", "UpdateItemGroupSize... ");
            Tizen.Log.Fatal("NUI.List", "mItemList.Count: " + mItemList.Count);
            //update item group height
            if (mItemList.Count == 0)
            {
                mItemGroupRect.Height = 0;
            }
            else
            {
                mItemGroupRect.Height = mItemList[mItemList.Count - 1].rect.Bottom();
            }
            //update item group width
            mItemGroupRect.Width = this.SizeWidth - (mMargin[0] + mMargin[2]);
            Tizen.Log.Fatal("NUI.List", "mItemGroupRect: X: " + mItemGroupRect.X + ", Y: " + mItemGroupRect.Y + ", W: " + mItemGroupRect.Width + ", H: " + mItemGroupRect.Height);

            //update item group size
            mItemGroup.SizeWidth = mItemGroupRect.Width;
            mItemGroup.SizeHeight = mItemGroupRect.Height;
        }

        /// <summary>
        /// Refresh the specified item position.
        /// </summary>
        /// <param name="item">the item</param>
        private void RefreshItemPosition(ListItem item)
        {
            //check item is valid
            if (null == item)
            {
                return;
            }

            //update item view position
            if (null != item.itemView)
            {
                item.itemView.PositionX = item.rect.X;
                item.itemView.PositionY = item.rect.Y;
            }
        }

        /// <summary>
        /// update list in memory.
        /// </summary>
        /// <param name="groupRect">the group rect</param>
        /// <param name="flagFocusAni">the flag of focus animation</param>
        private void UpdateInMemoryItems(Rect groupRect, bool flagFocusAni = false)
        {
            Tizen.Log.Fatal("NUI.List", "UpdateInMemoryItems...flagFocusAni: " + flagFocusAni);
            Rect visibleAreaOfItemGroup = new Rect(-groupRect.X, -groupRect.Y, this.SizeWidth, this.SizeHeight);

            int newHeadItemIndex = -1;
            int newTailItemIndex = -1;

            //get new head item index and new tail item index
            newHeadItemIndex = GetItemIndexByPosY(visibleAreaOfItemGroup.Y, true);
            newTailItemIndex = GetItemIndexByPosY(visibleAreaOfItemGroup.Bottom(), false);
            Tizen.Log.Fatal("NUI.List", "newHeadItemIndex: " + newHeadItemIndex);
            Tizen.Log.Fatal("NUI.List", "newTailItemIndex: " + newTailItemIndex);

            List<ListItem> loadList = new List<ListItem>();
            List<ListItem> unloadList = new List<ListItem>();
            List<ListItem> scrollList = new List<ListItem>();

            GetItemChangeInfo(mHeadItemIndex, mTailItemIndex, newHeadItemIndex, newTailItemIndex, loadList, unloadList, scrollList);
 
            mHeadItemIndex = newHeadItemIndex;
            mTailItemIndex = newTailItemIndex;

            Tizen.Log.Fatal("NUI.List", "newHeadItemIndex: " + newHeadItemIndex);
            Tizen.Log.Fatal("NUI.List", "newTailItemIndex: " + newTailItemIndex);
            Tizen.Log.Fatal("NUI.List", "StateRecycleEnable: " + StateRecycleEnable + ", flagFocusAni: " + flagFocusAni);

            //need to conside scroll, don't load and unload right now
            foreach (ListItem item in unloadList)
            {
                if (StateRecycleEnable == false || flagFocusAni == false)
                {
                    UnloadItem(item);                       
                    mCurInMemoryItemList.Remove(item);
                }
                else
                {
                    if (null == mUnloadItemList.Find((o) => { return o == item; }))
                    {
                        mUnloadItemList.Add(item);
                    }
                }
            }

            //load item from load list
            foreach (ListItem item in loadList)
            {
                LoadItem(item);
            }
        }

        /// <summary>
        /// Load an specified item.
        /// </summary>
        /// <param name="item">the item which should be loaded</param>
        private void LoadItem(ListItem item)
        {
            Tizen.Log.Fatal("NUI.List", "LoadItem...");
            Tizen.Log.Fatal("NUI.List", "item.index:" + item.index);
            //check whether the item has been loaded 
            if (null != mCurInMemoryItemList.Find((o) => { return o == item; }))
            {
                return;
            }

            //load the item
            mCurInMemoryItemList.Add(item);
            Tizen.Log.Fatal("NUI.List", "mCurInMemoryItemList add an item...");

            //remove from unload item list
            if (null != mUnloadItemList.Find((o) => { return o == item; }))
            {
                mUnloadItemList.Remove(item);
            }

            Tizen.Log.Fatal("NUI.List", "item.index:" + item.index);
            View temp = GetRecycleView(item.index);

            Tizen.Log.Fatal("NUI.List", "StateRecycleEnable:" + StateRecycleEnable + " temp:" + temp);
            if (StateRecycleEnable == true && temp != null)
            {
                Tizen.Log.Fatal("NUI.List", "LoadItem...StateRecycleEnable: true");
                item.itemView = temp;
                mAdapter.UpdateItem(item.index, item.itemView);
            }
            else
            {
                item.itemView = mAdapter.GetItemView(item.index);
                Tizen.Log.Fatal("NUI.List", "item.itemView :" + item.itemView);
                item.itemView.ParentOrigin = Tizen.NUI.ParentOrigin.TopCenter;
                item.itemView.PivotPoint = Tizen.NUI.PivotPoint.TopCenter;
                item.itemView.PositionUsesPivotPoint = true;
            }

            Tizen.Log.Fatal("NUI.List", "LoadItem...update item position and size.");
            
            //set item view position and size
            item.itemView.PositionX = item.rect.X;
            item.itemView.PositionY = item.rect.Y;
            item.itemView.SizeWidth = item.rect.Width;
            item.itemView.SizeHeight = item.rect.Height;

            //add the item to iten group
            mItemGroup.Add(item.itemView);
            Tizen.Log.Fatal("NUI.List", "mItemGroup.Add...X: " + item.itemView.PositionX + ", Y: " + item.itemView.PositionY);
            Tizen.Log.Fatal("NUI.List", "mItemGroup.Add...Width: " + item.itemView.SizeWidth + ", Height: " + item.itemView.SizeHeight);

            //send item scroll in event.
            ListEventArgs evtArgs = new ListEventArgs();
            evtArgs.EventType = ListEventType.ItemScrolledIn;
            evtArgs.param[0] = item.index;

            OnListEvent(evtArgs);
            Tizen.Log.Fatal("NUI.List", "LoadItem out...");
        }

        /// <summary>
        /// unload an specified item
        /// </summary>
        /// <param name="item">the item which should be unload</param>
        private void UnloadItem(ListItem item)
        {
            Tizen.Log.Fatal("NUI.List", "UploadItem...");
            Tizen.Log.Fatal("NUI.List", "item.index: " + item.index);
            Tizen.Log.Fatal("NUI.List", "mItemGroup.ChildCount: " + mItemGroup.ChildCount);
            // remove item from item group
            if (item.itemView != null)
            {
                mItemGroup.Remove(item.itemView);
            }

            //check whether need to recycle item
            if (mFlagEnableRecycle == false)
            {
                mAdapter.UnloadItem(item.index, item.itemView);
            }
            else
            {
                AddRecycleView(item.index, item.itemView);
            }

            //send item scroll out event.
            ListEventArgs evtArgs = new ListEventArgs();
            evtArgs.EventType = ListEventType.ItemScrolledOut;
            evtArgs.param[0] = item.index;

            OnListEvent(evtArgs);

            //item.itemView = null;
            Tizen.Log.Fatal("NUI.List", "UploadItem out...");
        }

        /// <summary>
        /// get item change information
        /// </summary>
        /// <param name="curHeadItemIndex">the current head index</param>
        /// <param name="curTailItemIndex">the current tail index</param>
        /// <param name="newHeadItemIndex">the new head index</param>
        /// <param name="newTailItemIndex">the new tail index</param>
        /// <param name="loadItem">the list of the load item</param>
        /// <param name="unloadItem">the list of the unload item</param>
        /// <param name="nochangedItem">the list of the no change item</param>
        private void GetItemChangeInfo(int curHeadItemIndex, int curTailItemIndex, int newHeadItemIndex, int newTailItemIndex, List<ListItem> loadItem, List<ListItem> unloadItem, List<ListItem> nochangedItem)
        {
            Tizen.Log.Fatal("NUI.List", "GetItemChangeInfo...");
            Tizen.Log.Fatal("NUI.List", "curHeadItemIndex: " + curHeadItemIndex);
            Tizen.Log.Fatal("NUI.List", "curTailItemIndex: " + curTailItemIndex);
            Tizen.Log.Fatal("NUI.List", "newHeadItemIndex: " + newHeadItemIndex);
            Tizen.Log.Fatal("NUI.List", "newTailItemIndex: " + newTailItemIndex);

            List<int> loadIndexList = new List<int>();
            List<int> unloadIndexList = new List<int>();
            List<int> nochangeIndexList = new List<int>();

            CompareIndexRange(curHeadItemIndex, curTailItemIndex, newHeadItemIndex, newTailItemIndex, mItemList.Count, loadIndexList, unloadIndexList, nochangeIndexList);

            foreach (int i in loadIndexList)
            {
                loadItem.Add(mItemList[i]);
            }

            foreach (int i in unloadIndexList)
            {
                unloadItem.Add(mItemList[i]);
            }

            foreach (int i in nochangeIndexList)
            {
                nochangedItem.Add(mItemList[i]);
            }
        }

        /// <summary>
        /// Get item index according Y position
        /// </summary>
        /// <param name="posY">the position of Y</param>
        /// <param name="isHead">is Head or not</param>
        /// <returns>return the item index</returns>
        private int GetItemIndexByPosY(float posY, bool isHead)
        {
            int itemIndex = -1;

            int itemCount = mItemList.Count;
            //check itemcout
            if (itemCount == 0)
            {
                return -1;
            }

            float firstItemStartPos = mItemList[0].rect.Y;
            float lastItemEndPos = mItemList[itemCount - 1].rect.Bottom();

            int begin = 0, end = itemCount - 1;

            if (true == isHead)
            {
                if (lastItemEndPos <= posY)
                {
                    //last item is upper than visible area top position, so no item at visible area
                    itemIndex = -1;
                }
                else
                {
                    if (firstItemStartPos >= posY)
                    {
                        itemIndex = 0;
                    }
                    else
                    {
                        do
                        {
                            if (begin + 1 == end && (begin + end) / 2 == itemIndex)
                            {
                                itemIndex = (begin + end) / 2 + 1;
                            }
                            else
                            {
                                itemIndex = (begin + end) / 2;
                            }

                            if (end - begin == 1)
                            {
                                if (mItemList[begin].rect.Bottom() <= posY && mItemList[end].rect.Y >= posY)
                                {
                                    itemIndex = end;
                                    break;
                                }
                            }

                            ListItem item = mItemList[itemIndex];

                            if (item.rect.Y <= posY && item.rect.Bottom() > posY)
                            {
                                break;
                            }
                            else if (item.rect.Bottom() <= posY)
                            {
                                begin = itemIndex;
                            }
                            else
                            {
                                end = itemIndex;
                            }

                        }
                        while (begin != end);

                    }
                }
            }
            else
            {
                if (firstItemStartPos >= posY)
                {
                    itemIndex = -1;
                }
                else
                {
                    do
                    {
                        if (begin + 1 == end && (begin + end) / 2 == itemIndex)
                        {
                            itemIndex = (begin + end) / 2 + 1;
                        }
                        else
                        {
                            itemIndex = (begin + end) / 2;
                        }

                        if (end - begin == 1)
                        {
                            if (mItemList[begin].rect.Bottom() <= posY && mItemList[end].rect.Y >= posY)
                            {
                                itemIndex = begin;
                                break;
                            }
                        }

                        ListItem item = mItemList[itemIndex];

                        if (item.rect.Y < posY && item.rect.Bottom() >= posY)
                        {
                            break;
                        }
                        else if (item.rect.Bottom() < posY)
                        {
                            begin = itemIndex;
                        }
                        else
                        {
                            end = itemIndex;
                        }

                    }
                    while (begin != end);

                }
            }

            return itemIndex;
        }

        /// <summary>
        /// Compare index range
        /// </summary>
        /// <param name="oldStart">the old start index</param>
        /// <param name="oldEnd">the old end index</param>
        /// <param name="newStart">the new start index</param>
        /// <param name="newEnd">the new end index</param>
        /// <param name="maxIndex">the max index</param>
        /// <param name="loadItem">the list of the load item</param>
        /// <param name="unloadItem">the list of the unload item</param>
        /// <param name="noChangeItem">the list of the no change item</param>
        private void CompareIndexRange(int oldStart, int oldEnd, int newStart, int newEnd, int maxIndex, List<int> loadItem, List<int> unloadItem, List<int> noChangeItem)
        {
            Tizen.Log.Fatal("NUI.List", "CompareIndexRange...");
            int headBufferNum = PreloadFrontItemSize;
            int tailBufferNum = PreloadBackItemSize;
            int realStart = -1;
            int realEnd = -1;

            //check old start
            if (-1 == oldStart)
            {
                if (0 <= newStart && 0 <= newEnd)
                {
                    realStart = newStart - headBufferNum;
                    realEnd = newEnd + tailBufferNum;
                    for (int i = realStart; i <= realEnd; i++)
                    {
                        if (IsValidItemIndex(i) == true)
                        {
                            loadItem.Add(i);
                        }
                    }
                }
            }
            else
            {
                if (0 > newStart || 0 > newEnd)
                {
                    realStart = oldStart - headBufferNum;
                    realEnd = oldEnd + tailBufferNum;
                    for (int i = realStart; i <= realEnd; i++)
                    {
                        if (IsValidItemIndex(i) == true)
                        {
                            unloadItem.Add(i);
                        }
                    }
                }
                else if (oldStart <= newStart)
                {
                    //OldStart......NewStart
                    if (newStart <= oldEnd)
                    {
                        //OldStart......NewStart......OldEnd
                        if (oldEnd <= newEnd)
                        {
                            realStart = oldStart - headBufferNum;
                            realEnd = newStart - headBufferNum;
                            //OldStart......NewStart......OldEnd......NewEnd
                            for (int i = realStart; i < realEnd; i++)
                            {
                                if (IsValidItemIndex(i) == true)
                                {
                                    unloadItem.Add(i);
                                }
                            }

                            realStart = newStart - headBufferNum;
                            realEnd = oldEnd + tailBufferNum;
                            for (int i = realStart; i <= realEnd; i++)
                            {
                                if (IsValidItemIndex(i) == true)
                                {
                                    noChangeItem.Add(i);
                                }     
                            }

                            realStart = oldEnd + tailBufferNum + 1;
                            realEnd = newEnd + tailBufferNum;
                            for (int i = realStart; i <= realEnd; i++)
                            {
                                if (IsValidItemIndex(i) == true)
                                {
                                    loadItem.Add(i);
                                }
                            }
                        }
                        else
                        {
                            realStart = oldStart - headBufferNum;
                            realEnd = newStart - headBufferNum;
                            //OldStart......NewStart......NewEnd......OldEnd
                            for (int i = realStart; i < realEnd; i++)
                            {
                                if (IsValidItemIndex(i) == true)
                                {
                                    unloadItem.Add(i);
                                }
                            }

                            realStart = newStart - headBufferNum;
                            realEnd = newEnd + tailBufferNum;
                            for (int i = realStart; i <= realEnd; i++)
                            {
                                if (IsValidItemIndex(i) == true)
                                {
                                    noChangeItem.Add(i);
                                }    
                            }

                            realStart = newEnd + tailBufferNum + 1;
                            realEnd = oldEnd + tailBufferNum;
                            for (int i = realStart; i <= realEnd; i++)
                            {
                                if (IsValidItemIndex(i) == true)
                                {
                                    unloadItem.Add(i);
                                }
                            }
                        }
                    }
                    else
                    {
                        realStart = oldStart - headBufferNum;
                        realEnd = oldEnd + tailBufferNum;
                        //OldStart......OldEnd......NewStart
                        for (int i = realStart; i <= realEnd; i++)
                        {
                            if (IsValidItemIndex(i) == true)
                            {
                                unloadItem.Add(i);
                            }
                        }

                        realStart = newStart - headBufferNum;
                        realEnd = newEnd + tailBufferNum;
                        for (int i = realStart; i <= realEnd; i++)
                        {
                            if (IsValidItemIndex(i) == true)
                            {
                                loadItem.Add(i);
                            }
                        }
                    }
                }
                else
                {
                    //NewStart......OldStart
                    if (oldStart <= newEnd)
                    {
                        //NewStart......OldStart......NewEnd
                        if (newEnd <= oldEnd)
                        {
                            realStart = newStart - headBufferNum;
                            realEnd = oldStart - headBufferNum;
                            //NewStart......OldStart......NewEnd......OldEnd
                            for (int i = realStart; i < realEnd; i++)
                            {
                                if (IsValidItemIndex(i) == true)
                                {
                                    loadItem.Add(i);
                                }
                            }

                            realStart = oldStart - headBufferNum;
                            realEnd = newEnd + tailBufferNum;
                            for (int i = realStart; i <= realEnd; i++)
                            {
                                if (IsValidItemIndex(i) == true)
                                {
                                    noChangeItem.Add(i);
                                }    
                            }

                            realStart = newEnd + tailBufferNum + 1;
                            realEnd = oldEnd + tailBufferNum;
                            for (int i = realStart; i <= realEnd; i++)
                            {
                                if (IsValidItemIndex(i) == true)
                                {
                                    unloadItem.Add(i);
                                }
                            }
                        }
                        else
                        {
                            realStart = newStart - headBufferNum;
                            realEnd = oldStart - headBufferNum;
                            //NewStart......OldStart......OldEnd......NewEnd
                            for (int i = realStart; i < realEnd; i++)
                            {
                                if (IsValidItemIndex(i) == true)
                                {
                                    loadItem.Add(i);
                                }
                            }

                            realStart = oldEnd + tailBufferNum;
                            realEnd = newEnd + tailBufferNum;
                            for (int i = realStart + 1; i <= realEnd; i++)
                            {
                                if (IsValidItemIndex(i) == true)
                                {
                                    loadItem.Add(i);
                                }
                            }
                        }
                    }
                    else
                    {
                        realStart = oldStart - headBufferNum;
                        realEnd = oldEnd + tailBufferNum;
                        //NewStart......NewEnd......OldStart
                        for (int i = realStart; i <= realEnd; i++)
                        {
                            if (IsValidItemIndex(i) == true)
                            {
                                unloadItem.Add(i);
                            }
                        }

                        realStart = newStart - headBufferNum;
                        realEnd = newEnd + tailBufferNum;
                        for (int i = realStart; i <= realEnd; i++)
                        {
                            if (IsValidItemIndex(i) == true)
                            {
                                loadItem.Add(i);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Notify focus change region
        /// </summary>
        /// <param name="from">the first item index</param>
        /// <param name="to">the last item index</param>
        private void NotifyFocusChange(int from, int to)
        {
            Tizen.Log.Fatal("NUI.List", "=========NotifyFocusChange: from: " + from + " to " + to);
            //check whether to change focus
            if (mFlagFirstIn == false && from == to) 
            {
                return;
            }

            mFlagFirstIn = false;
            // check from is valid
            if (IsValidItemIndex(from) == true && mItemList[from].itemView != null && from != to)
            {
                ListItem preFocusItem = mItemList[from];
                mAdapter.FocusChange(from, preFocusItem.itemView, false);
            }

            //check to is valid
            if (IsValidItemIndex(to) == true && mItemList[to].itemView != null)
            {
                ListItem curFocusItem = mItemList[to];
                mAdapter.FocusChange(to, curFocusItem.itemView, true);
            }

            //send focus change event.
            ListEventArgs evtArgs = new ListEventArgs();
            evtArgs.EventType = ListEventType.FocusChange;
            evtArgs.param[0] = from;
            evtArgs.param[1] = to;

            OnListEvent(evtArgs);
        }

        /// <summary>
        /// Set focus item index.
        /// </summary>
        /// <param name="focusItemIndex">the index of the focused item</param>
        private void SetFocus(int focusItemIndex)
        {
            Tizen.Log.Fatal("NUI.List", "=========SetFocus index: " + focusItemIndex);
            if (mAttrsApplied == false)
            {
                Tizen.Log.Fatal("NUI.List", "=========SetFocus mAttrsApplied: " + mAttrsApplied);
                mCurFocusItemIndex = focusItemIndex;
                return;
            }

            //check item is enable.
            if (!IsValidItemIndex(focusItemIndex) || !mAdapter.IsItemEnabled(focusItemIndex))
            {
                Tizen.Log.Fatal("NUI.List", "=========SetFocus index invalid or unenabled");
                return;
            }

            ListItem item = mItemList[focusItemIndex];

            //calculate dest pos of item group by dest item rect
            Position itemGroupPos = new Position(0, 0, 0);
            bool flagScrollItem = CalItemGroupPosByFocusItemIndex(focusItemIndex, itemGroupPos);
            mItemGroupRect.Y = itemGroupPos.Y;
            Tizen.Log.Fatal("NUI.List", "=========SetFocus flagScrollItem: " + flagScrollItem);

            //check whether need to scroll
            if (true == flagScrollItem)
            {
                UpdateInMemoryItems(mItemGroupRect);
                mItemGroup.PositionY = mItemGroupRect.Y;
            }

            ChangeFocus(focusItemIndex);
            mScrollIndex = mCurFocusItemIndex;
            // UpdateScroll(focusItemIndex);
        }

        /// <summary>
        /// Change focus index to specfied index.
        /// </summary>
        /// <param name="focusItemIndex">the index of the focused item</param>
        private void ChangeFocus(int focusItemIndex)
        {
            Tizen.Log.Fatal("NUI.List", "=========ChangeFocus focusItemIndex: " + focusItemIndex);
            // check current focus index is valid and it doesn't equal with current focus item.
            if (IsValidItemIndex(mCurFocusItemIndex) && mCurFocusItemIndex != focusItemIndex)
            {
                ListItem focusOutItem = mItemList[mCurFocusItemIndex];
            }
            //check focus item index is valid.
            if (IsValidItemIndex(focusItemIndex))
            {
                ListItem focusInItem = mItemList[focusItemIndex];
            }

            //Check whether need to show top shadow view.
            if (mTopShadowView != null)
            {
                if (mHeadItemIndex == 0 && mItemGroup.PositionY + mItemList[mHeadItemIndex].rect.Top() >= 0)
                {
                    mTopShadowView.Hide();
                }
                else
                {
                    mTopShadowView.RaiseToTop();
                    mTopShadowView.Show();
                }
            }

            //check whether need to show bottom shadow view.
            if (mBottomShadowView != null)
            {
                if (mTailItemIndex == NumOfItem - 1 && mItemGroup.PositionY + mItemList[mTailItemIndex].rect.Bottom() <= this.SizeHeight)
                {
                    mBottomShadowView.Hide();
                }
                else
                {
                    mBottomShadowView.RaiseToTop();
                    mBottomShadowView.Show();
                }
            }

            //send focus change event.
            NotifyFocusChange(mCurFocusItemIndex, focusItemIndex);

            //preFocusItemIndex = mCurFocusItemIndex;

            //update current focus item index.
            mCurFocusItemIndex = focusItemIndex;
        }

        /// <summary>
        /// Check index is valid in list
        /// </summary>
        /// <param name="index">the index of the item</param>
        /// <returns>whether the item with the specific index is valid or not</returns>
        private bool IsValidItemIndex(int index)
        {
            //check index is valid
            return (index >= 0 && index < mItemList.Count);
        }

        /// <summary>
        /// Calculate item group position according to focus item index.
        /// </summary>
        /// <param name="focusIndex">the index of the focused item</param>
        /// <param name="itemGroupPos">the item group position</param>
        /// <returns>return whether get the position successfully or not</returns>
        private bool CalItemGroupPosByFocusItemIndex(int focusIndex, Position itemGroupPos)
        {
            Tizen.Log.Fatal("NUI.List", "CalItemGroupPosByFocusItemIndex... focusIndex: " + focusIndex);
            bool needScroll = false;
            Rect destItemRectOnList = new Rect(mItemList[focusIndex].rect);
            destItemRectOnList.Y += mItemGroupRect.Y;

            float startFocusRange = mMargin[1];
            float endFocusRange = this.SizeHeight - mMargin[3];

            itemGroupPos.Y = mItemGroupRect.Y;
            Tizen.Log.Fatal("NUI.List", "destItemRectOnList.Y: " + destItemRectOnList.Y);
            Tizen.Log.Fatal("NUI.List", "destItemRectOnList.Bottom: " + destItemRectOnList.Bottom());

            //check whether need to scroll and calculate item group Y position.
            if (destItemRectOnList.Y < startFocusRange)
            {
                needScroll = true;
                itemGroupPos.Y += startFocusRange - destItemRectOnList.Y;
            }
            else if (destItemRectOnList.Bottom() > endFocusRange)
            {
                needScroll = true;
                itemGroupPos.Y += endFocusRange - destItemRectOnList.Bottom();
            }

            return needScroll;
        }

        /// <summary>
        /// Get page number.
        /// </summary>
        /// <returns>return the number of the page item</returns>
        private int PageItemNum()
        {
            //check current focus item index is valid
            if (IsValidItemIndex(mCurFocusItemIndex) == false)
            {
                return 0;
            }

            return (int)(this.SizeHeight / mItemList[mCurFocusItemIndex].rect.Height);
        }

        /// <summary>
        /// Reset list.
        /// </summary>
        private void ResetList()
        {
            //remove all views from the View, Clear everything out.
            Clear();
        }

        /// <summary>
        /// Set view type count.
        /// </summary>
        /// <param name="typeCount">the count of view type</param>
        private void SetViewTypeCount(int typeCount)
        {
            Tizen.Log.Fatal("NUI.List", "SetViewTypeCount...");
            //check type count is valid
            if (typeCount < 1)
            {
                return;
            }

            Tizen.Log.Fatal("NUI.List", "typeCount: " + typeCount);

            mViewTypeCount = typeCount;
            List<View>[] viewListArray = new List<View>[mViewTypeCount];

            for (int i = 0; i < mViewTypeCount; i++)
            {
                viewListArray[i] = new List<View>();
            }

            // update recycled view list
            mRecycledViews = viewListArray;
        }

        /// <summary>
        /// Add an view at specified index into recycle list.
        /// </summary>
        /// <param name="index">the index</param>
        /// <param name="view">the view</param>
        private void AddRecycleView(int index, View view)
        {
            Tizen.Log.Fatal("NUI.List", "AddRecycleView...");
            int viewType = mAdapter.GetItemType(index);

            Tizen.Log.Fatal("NUI.List", "viewType: " + viewType);
            Tizen.Log.Fatal("NUI.List", "mViewTypeCount: " + mViewTypeCount);

            //check viewtype is valid
            if (viewType >= mViewTypeCount)
            {
                return;
            }

            //hide the item view
            if (view != null)
            {
                view.Hide();
            }

            //add it into recycled view list.
            mRecycledViews[viewType].Add(view);
        }

        /// <summary>
        /// Get recycle view at specified index.
        /// </summary>
        /// <param name="index">the index</param>
        /// <returns>return the view with the specific index</returns>
        private View GetRecycleView(int index)
        {
            int viewType = mAdapter.GetItemType(index);

            //check viewtype is valid
            if (viewType >= mViewTypeCount)
            {
                return null;
            }

            View obj = null;
            int viewCount = mRecycledViews[viewType].Count;
            if (viewCount != 0)
            {
                obj = mRecycledViews[viewType][viewCount - 1];
                mRecycledViews[viewType].Remove(obj);
            }

            //show the item view
            if (obj != null)
            {
                obj.Show();
            }

            return obj;
        }

        /// <summary>
        /// Clear recycle view list.
        /// </summary>
        private void ClearRecycleBin()
        {
            Tizen.Log.Fatal("NUI.List", "ClearRecycleBin...");
            for (int i = 0; i < mViewTypeCount; i++)
            {
                for (int j = 0; j < mRecycledViews[i].Count; j++)
                {
                    UnloadRecycledView(i, mRecycledViews[i][j]);
                }

                mRecycledViews[i].Clear();
            }

        }

        /// <summary>
        /// Unload view at specified view type.
        /// </summary>
        /// <param name="viewType">view type</param>
        /// <param name="view">the view should be unload</param>
        private void UnloadRecycledView(int viewType, View view)
        {
            Tizen.Log.Fatal("NUI.List", "unloadRecycledVie...");
            mAdapter.UnloadItemByViewType(viewType, view);
        }

        /// <summary>
        /// Internal list item class.
        /// </summary>
        private class ListItem
        {
            // Dispose methoud
            public void Dispose()
            {
                if (itemView == null)
                {
                    return;
                }

                itemView.Dispose();
                itemView = null;
            }

            public int index = -1; // item index
            public View itemView = null; //item view
            public Rect rect = new Rect(0, 0, 0, 0); //item rect
        }

        private bool mAttrsApplied = true; //attribute apply flag

        private ListAdapter mAdapter = null; // adapter
        private int mListItemCount; // listitem count

        //private Rectangle listViewRect;
        private List<ListItem> mItemList = new List<ListItem>(); //total item list
        private List<ListItem> mCurInMemoryItemList = new List<ListItem>(); //current in memory item list
        private List<ListItem> mUnloadItemList = new List<ListItem>(); //unloaded item list

        private View mItemGroup; //item group view
        private Rect mItemGroupRect = new Rect(0, 0, 0, 0); // item group rect

        private int mHeadItemIndex = -1; //head item index
        private int mTailItemIndex = -1; //tail item index

        //for focus move
        private int mCurFocusItemIndex = -1; // current focus item index

        private Queue<int> mFocusMoveQ = new Queue<int>(); // focus move queue

        private Queue<ImageView> mFocusBarQ = new Queue<ImageView>(); // focus bar queue

        private LinkerAnimation mScrollWayPointAni; //scroll animation

        private int mItemSpace = 0; //item space
        private Vector4 mMargin = Vector4.Zero; //margin

        private bool mFlagEnableRecycle = true; // recycle flag
        private bool mFlagFirstIn = true; // first in flag

        private ImageView mTopShadowView = null; // top shadow image view
        private ImageView mBottomShadowView = null; //bottom shadow image view

        private List<View>[] mRecycledViews; // recycled views list
        private int mViewTypeCount; //view type count

        private int mScrollIndex; //scroll index

        private EventHandler<ListEventArgs> mListEventHandlers; //list event handler

        /// <summary> Preload item buffer size at front </summary>
        private int mPreloadFrontItemSize; //proload front item size
        
        /// <summary> Preload item buffer size at back </summary>
        private int mPreloadBackItemSize; //proload back item size
    }
}
