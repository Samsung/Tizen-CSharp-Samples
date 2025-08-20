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
using System.Diagnostics;
using System.IO;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

/// <summary>
/// namespace for Tizen.NUI package
/// </summary>
namespace Tizen.NUI
{
    /// <summary>
    /// A Adapter object acts as a pipeline between an AdapterView and the
    /// underlying data for that view. The Adapter provides access to the data items.
    /// The Adapter is also responsible for making a View for each item in the data set.
    /// </summary>
    /// <code>
    /// public class ListItem1Data
    /// {
    ///     private string str;
    ///     public ListItem1Data(int i)
    ///     {
    ///         str = str + "-" + i.ToString();
    ///     }
    ///     public string TextString
    ///     {
    ///         get
    ///         {
    ///             return str;
    ///         }
    ///     }
    /// }
    /// public class TestListAdapter : ListAdapter
    /// {
    ///     public TestListAdapter(List<object> objects)
    ///        : base(objects)
    ///     {
    ///     }
    ///     public override View GetItemView(int index)
    ///     {
    ///         object data = GetData(index);
    ///         ListItem1Data itemData = data as ListItem1Data;
    /// 
    ///         TextItem obj = new TextItem("C_TextItem_WhiteSingleline01");
    ///         obj.Name = "ListItem" + index;
    ///         obj.StateListLineEnabled = true;
    ///         obj.MainText = itemData.TextString;
    ///     }
    ///     public override int GetItemHeight(int index)
    ///     {
    ///         return 80;
    ///     }
    ///     public override void UpdateItem(int index, View view)
    ///     {
    ///         object data = GetData(index);
    ///         ListItem1Data itemData = data as ListItem1Data;
    /// 
    ///         TextItem itemView = view as TextItem;
    ///         if (itemView != null)
    ///         {
    ///             itemView.MainText = itemData.TextString;
    ///         }
    ///     }
    ///     public override void FocusChange(int index, View view, bool FlagFocused)
    ///     {
    ///         TextItem itemView = view as TextItem;
    ///         itemView.StateFocused = flagFocused ;
    ///     }
    ///     public override void UnloadItem(int index, View view)
    ///     {
    ///         view.Dispose();
    ///     }
    ///     public override void UnloadItemByViewType(int viewType, View view)
    ///     {
    ///         view.Dispose();
    ///     }
    /// }
    /// </code>
    public abstract class ListBridge
    {
        /// <summary>
        /// Construct ListAdapter with data.
        /// </summary>
        /// <param name="objects">data list</param>
        public ListBridge(List<object> objects)
        {
            listData = objects;
        }

        /// <summary>
        /// Get a View that displays the data at the specified index in the data set.
        /// </summary>
        /// <param name="index">The index of the data within the Adapter's data set of the item whose view we want.</param>
        /// <returns>A View corresponding to the data at the specified index.</returns>
        public abstract View GetItemView(int index);

        /// <summary>
        /// Get view height associated with the specified index in the data set.
        /// </summary>
        /// <param name="index">The index of the data within the Adapter's data set.</param>
        /// <returns>Hight of View corresponding to the data at the specified index.</returns>
        public abstract int GetItemHeight(int index);

        /// <summary>
        /// Update View that displays the data at the specified index in the data set when data change.
        /// </summary>
        /// <param name="index">The index of the data within the Adapter's data set</param>
        /// <param name="view">A View that displays the data at the specified index in the data set.</param>
        public abstract void UpdateItem(int index, View view);

        /// <summary>
        /// Update View that displays the data at the specified index in the data set when focus change.
        /// </summary>
        /// <param name="index">The index of the data within the Adapter's data set</param>
        /// <param name="view">A View that displays the data at the specified index in the data set.</param>
        /// <param name="FlagFocused">True means the item state change to focus, false means the item state change to unfocus.</param>
        public abstract void FocusChange(int index, View view, bool FlagFocused);

        /// <summary>
        /// Unload view at the specified index when item scroll out or deleted.
        /// </summary>
        /// <param name="index">The index of the data within the Adapter's data set</param>
        /// <param name="view">A View that displays the data at the specified index in the data set.</param>
        public abstract void UnloadItem(int index, View view);

        /// <summary>
        /// Unload view according to the specified viewType.
        /// </summary>
        /// <param name="viewType"> Item type set by users.</param>
        /// <param name="view">A View that displays the data at the specified index in the data set.</param>
        public abstract void UnloadItemByViewType(int viewType, View view);

        /// <summary>
        /// Get the type of View that will be created by GetView for the specified item.
        /// </summary>
        /// <param name="index">The index of the data within the Adapter's data set of the item whose view we want.</param>
        /// <returns>An integer representing the type of View.
        /// <remarks>Integers must be in the range 0 to GetItemTypeCount() - 1.</remarks>
        /// </returns>
        public virtual int GetItemType(int index)
        {
            return 0;
        }

        /// <summary>
        /// Get the number of view types.
        /// </summary>
        /// <returns>Returns the number of types of Views that will be created by GetView().
        /// If the Adapter always returns the same type of View for all items, this method should return 1.</returns>
        public virtual int GetItemTypeCount()
        {
            return 1;
        }

        /// <summary>
        /// Check whether item is enable or not
        /// </summary>
        /// <param name="index">The index of the data within the Adapter's data set</param>
        /// <returns>return whether item is enable or not.</returns>
        public virtual bool IsItemEnabled(int index)
        {
            return true;
        }

        /// <summary>
        /// How many items are in the data set represented by this Adapter.
        /// </summary>
        /// <returns>Count of items</returns>
        public int GetCount()
        {
            return listData.Count;
        }

        /// <summary>
        /// Get the data item associated with the specified index in the data set.
        /// </summary>
        /// <param name="index">index of the data we want within the Adapter's data set.</param>
        /// <returns>The data at the specified index.</returns>
        public object GetData(int index)
        {
            return listData.ElementAt(index);
        }

        /// <summary>
        /// Adds the specified data at the end of the List.
        /// </summary>
        /// <param name="data">The data to add at the end of the List.</param>
        public void Add(object data)
        {
            Tizen.Log.Debug("NUI", "Add item ...");
            listData.Add(data);
            DataChangeEventArgs e = new DataChangeEventArgs();
            e.ChangeType = DataChangeEventType.Add;
            e.data = data;
            OnDataChangeEvent(this, e);
        }
        /// <summary>
        /// Adds the specified datas at the end of the List.
        /// </summary>
        /// <param name="datas">The dataList to add at the end of the List.</param>
        public void AddAll(List<object> datas)
        {
            Tizen.Log.Debug("NUI", "Add items ...");
            listData.AddRange(datas);
            DataChangeEventArgs e = new DataChangeEventArgs();
            e.ChangeType = DataChangeEventType.AddAll;
            e.data = datas;
            OnDataChangeEvent(this, e);
        }
        /// <summary>
        /// Removes the specified datas from the List.
        /// </summary>
        /// <param name="fromIndex">The from index of data to remove from the list.</param>
        /// <param name="removeNum">The remove number of data to remove from the list.</param>
        public void Remove(int fromIndex, int removeNum)
        {
            Tizen.Log.Debug("NUI", "Remove item from " + fromIndex + ", number " + removeNum);
            if (fromIndex + removeNum > listData.Count)
            {
                return;
            }

            listData.RemoveRange(fromIndex, removeNum);

            DataChangeEventArgs e = new DataChangeEventArgs();
            e.ChangeType = DataChangeEventType.Remove;
            e.param[0] = fromIndex;
            e.param[1] = removeNum;
            OnDataChangeEvent(this, e);
            //e.ChangeType = DataChangeEventType.Load;
            //OnDataChangeEvent(this, e);
        }
        /// <summary>
        /// Inserts the specified data at the specified index in the list.
        /// </summary>
        /// <param name="index">The index at which the data must be inserted.</param>
        /// <param name="data">The data to insert into the list.</param>
        public void Insert(int index, object data)
        {
            Tizen.Log.Debug("NUI", "Insert item at " + index);
            listData.Insert(index, data);
            DataChangeEventArgs e = new DataChangeEventArgs();
            e.ChangeType = DataChangeEventType.Insert;
            e.data = data;
            e.param[0] = index;

            OnDataChangeEvent(this, e);
        }

        /// <summary>
        /// Remove all elements from the list.
        /// </summary>
        public void Clear()
        {
            Tizen.Log.Debug("NUI", "Clear ListView");
            DataChangeEventArgs e = new DataChangeEventArgs();
            e.ChangeType = DataChangeEventType.Clear;
            OnDataChangeEvent(this, e);
            listData.Clear();
            e.ChangeType = DataChangeEventType.Load;
            OnDataChangeEvent(this, e);
        }

        /// <summary>
        /// Data changed event type.
        /// </summary>
        public enum DataChangeEventType
        {
            /// <summary> Add a data. </summary>
            Add,
            /// <summary> Add datas at one time. </summary>
            AddAll,
            /// <summary> Remove datas. </summary>
            Remove,
            /// <summary> Insert datas. </summary>
            Insert,
            /// <summary> Clear all datas. </summary>
            Clear,
            /// <summary> Load datas. </summary>
            Load
        }

        /// <summary>
        /// Data changed event arguments.
        /// </summary>
        /// <code>
        ///private void OnDataChange(object o, GridAdapter.DataChangeEventArgs e)
        ///{
        ///}
        /// </code>
        public class DataChangeEventArgs : EventArgs
        {
            /// <summary>
            /// Data changed event type.
            /// </summary>
            public DataChangeEventType ChangeType
            {
                get { return m_ChangeType; }
                set { m_ChangeType = value; }
            }

            /// <summary>
            /// Changed data.
            /// </summary>
            public object data;

            /// <summary>
            /// Data change event parameters.
            /// </summary>
            public int[] param = new int[4];

            private DataChangeEventType m_ChangeType;
        }

        /// <summary>
        /// Data changed event function delegate.
        /// </summary>
        /// <param name="sender"> the object who send this event </param>
        /// <param name="e"> event arguments </param>
        public delegate void EventHandler<DataChangeEventArgs>(object sender, DataChangeEventArgs e);
        /// <summary>
        /// Data changed event.
        /// </summary>
        /// <param name="DataChangeEventArgs"> Data change event arguments </param>
        public event EventHandler<DataChangeEventArgs> DataChangeEvent
        {
            add
            {
                dataChangeEventHandlers += value;
            }

            remove
            {
                dataChangeEventHandlers -= value;
            }
        }

        private void OnDataChangeEvent(object sender, DataChangeEventArgs e)
        {
            if (dataChangeEventHandlers != null)
            {
                dataChangeEventHandlers(sender, e);
            }
        }

        private List<object> listData;
        private EventHandler<DataChangeEventArgs> dataChangeEventHandlers;
    }

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
    /// List v2 = new List("C_ListBasic_White");
    /// v2.Name = "List";
    /// v2.BackgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    /// v2.Size = new Size(622, 800, 0);
    /// v2.PivotPoint = PivotPoint.TopLeft;
    /// v2.ParentOrigin = ParentOrigin.TopLeft;
    /// v2.Position = new Position(100, 100, 0);
    /// Window.Instance.GetDefaultLayer().Add(v2);
    /// List<object> dataList = new List<object>();
    /// for (int i = 0; i< 100; ++i)
    /// {
    ///     dataList.Add(new ListItem1Data(i));
    /// }
    /// ListAdapter adapter = new TestListAdapter(dataList);
    /// v2.SetAdapter(adapter);
    /// </code>
    public class ListView : View
    {
        /// <summary>
        /// List event types
        /// </summary>
        public enum ListEventType
        {
            /// <summary> Event triggered when focus change </summary>
            FocusChange,
            /// <summary> Event triggered when item scroll in </summary>
            ItemScrolledIn,
            /// <summary> Event triggered when item scroll out </summary>
            ItemScrolledOut,
            /// <summary> Event triggered when focus move out of list </summary>
            FocusMoveOut
        }
        /// <summary>
        /// List attributes need to set when create
        /// </summary>
        /// <code>
        /// List.Attributes listAttrs = new List.Attributes();
        /// listAttrs.Margin = new Vector4(36, 0, 36, 0);
        /// listAttrs.PreloadFrontItemSize = 1;
        /// listAttrs.PreloadBackItemSize = 1;
        /// listAttrs.ScrollAnimationAttrs = new AnimationAttributes();
        /// listAttrs.ScrollAnimationAttrs.BezierPoint1 = new Vector2(0.30f, 0);
        /// listAttrs.ScrollAnimationAttrs.BezierPoint2 = new Vector2(0.15f, 1);
        /// listAttrs.ScrollAnimationAttrs.Duration = 100;
        /// listAttrs.FocusInScaleFactor = 1.08f;
        /// listAttrs.FocusInScaleAnimationAttrs = new AnimationAttributes();
        /// listAttrs.FocusInScaleAnimationAttrs.BezierPoint1 = new Vector2(0.21f, 2);
        /// listAttrs.FocusInScaleAnimationAttrs.BezierPoint2 = new Vector2(0.14f, 1);
        /// listAttrs.FocusInScaleAnimationAttrs.Duration = 600;
        /// listAttrs.FocusOutScaleAnimationAttrs = new AnimationAttributes();
        /// listAttrs.FocusOutScaleAnimationAttrs.BezierPoint1 = new Vector2(0.19f, 1);
        /// listAttrs.FocusOutScaleAnimationAttrs.BezierPoint2 = new Vector2(0.22f, 1);
        /// listAttrs.FocusOutScaleAnimationAttrs.Duration = 850;
        /// </code>

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
        /// For register type to View Registry
        /// </summary>
        public ListView()
        {
            Initialize();
        }

         /// <summary>
        /// Relayout Callback.
        /// </summary>
        /// <param name="sender">The object of relayout.</param>
        /// <param name="e">The event args.</param>
        private void OnRelayout(object sender, EventArgs e)
        {
            Tizen.Log.Debug("NUI", " OnRelayout..." + (sender as View)?.Name);

            OnUpdate();
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
                flagEnableRecycle = false;
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
                Tizen.Log.Debug("NUI", "=========get FocusItemIndex : " + curFocusItemIndex);
                return curFocusItemIndex; 
            }

            set
            {
                Tizen.Log.Debug("NUI", "=========FocusItemIndex set to: " + value);
                if (!IsValidItemIndex(value))
                {
                    return;
                }

                if (HasFocus() == true)
                {
                    SetFocus(value);
                }
                else
                {
                    curFocusItemIndex = value;
                }

                Tizen.Log.Debug("NUI", "==========FocusItemIndex set to: " + curFocusItemIndex);
            }
        }

        /// <summary>
        /// Get/Set Parenet List.
        /// </summary>
        public ListView ParentList
        {
        	get
            {
        		return parentList; 
        	}

        	set
        	{
        		parentList = value;
        	}
        }
        /// <summary>
        /// Get/Set Child list.
        /// </summary>
        public ListView ChildList
        {
        	get
            {
        		return childList; 
        	}

        	set
        	{
        		childList = value;
        	}
        }



        /// <summary>
        /// Get/Set Preload front item size.
        /// </summary>
        public int PreloadFrontItemSize
        {
        	get
            {
        		return preloadFrontItemSize; 
        	}

        	set
        	{
        		preloadFrontItemSize = value;
        	}
        }

        /// <summary>
        /// Get/Set Preload front item size.
        /// </summary>
        public int PreloadBackItemSize
        {
        	get
            {
        		return preloadBackItemSize; 
        	}

        	set
        	{
        		preloadBackItemSize = value;
        	}
        }


        /// <summary>
        /// Get/Set Scroll animation attributre.
        /// </summary>
        public AnimationAttributes ScrollAnimationAttrs
        {
            get
            {
                return scrollAnimationAttrs; 
            }

            set
            {
                scrollAnimationAttrs = value;
            }
        }

        /// <summary>
        /// Get/Set Focus in scale factor.
        /// </summary>
        public float FocusInScaleFactor
        {
        	get
            {
        		return focusInScaleFactor; 
        	}

        	set
        	{
        		focusInScaleFactor = value;
        	}
        }
        /// <summary>
        /// Get/Set List top padding.
        /// </summary>
        public int ListTopPadding
        {
        	get
            {
        		return listTopPadding; 
        	}

        	set
        	{
        		listTopPadding = value;
        	}
        }
        /// <summary>
        /// Get/Set List bottom padding.
        /// </summary>
        public int ListBottomPadding
        {
        	get
            {
        		return listBottomPadding; 
        	}

        	set
        	{
        		listBottomPadding = value;
        	}
        }
        /// <summary>
        /// Get/Set List level.
        /// </summary>
        public int Level
        {
        	get
            {
        		return level; 
        	}

        	set
        	{
        		level = value;
        	}
        }

        /// <summary>
        /// Get/Set Focus in scale animation attribute.
        /// </summary>
        public AnimationAttributes FocusInScaleAnimationAttrs
        {
        	get
            {
        		return focusInScaleAnimationAttrs; 
        	}

        	set
        	{
        		focusInScaleAnimationAttrs = value;
        	}
        }

        /// <summary>
        /// Get/Set Focus out scale animation attribute.
        /// </summary>
        public AnimationAttributes FocusOutScaleAnimationAttrs
        {
        	get
            {
        		return focusOutScaleAnimationAttrs; 
        	}

        	set
        	{
        		focusOutScaleAnimationAttrs = value;
        	}
        }


/*        
                
        /// <summary> Focus in scale animation attributes </summary>
        private AnimationAttributes focusInScaleAnimationAttrs;
        
        /// <summary> Focus out scale animation attributes </summary>
        private  focusOutScaleAnimationAttrs;
                
        private int ListTopPadding;
        private int ListBottomPadding;
		
        private int level;
*/
        /// <summary> 
        /// Get/Set list spaces around items. 
        /// </summary>
        public new Vector4 Margin
        {
            get { return margin; }
            set { margin = value; }
        }

        /// <summary> 
        /// Get/Set space between items 
        /// </summary>
        public int ItemSpace
        {
            get { return itemSpace; }
            set { itemSpace = value; }
        }

        /// <summary>
        /// Get/Set enable flag of forward circulation of List.
        /// </summary>
        public bool ForwardCirculation
        {
            get { return flagUpLoop; }
            set { flagUpLoop = value; }
        }

        /// <summary>
        /// Get/Set enable flag of backward circulation of List.
        /// </summary>
        public bool BackwardCirculation
        {
            get { return flagDownLoop; }
            set { flagDownLoop = value; }
        }

        /// <summary>
        /// Get/Set enable flag whether remember last focus item index when List focus out.
        /// </summary>
        public bool AutoLastFocus
        {
            get { return flagRememberLastFocusIndex; }
            set { flagRememberLastFocusIndex = value; }
        }

        /// <summary>
        /// Get/Set enable flag whether enable recycle bin, when unload item.
        /// <remarks>default value is true, don't change except special requirement.</remarks> 
        /// </summary>
        public bool StateRecycleEnable
        {
            get { return flagEnableRecycle; }
            set { flagEnableRecycle = value; }
        }

        /// <summary>
        /// Get/Set focus and scroll animation duration of List.
        /// </summary>
        public int ScrollAniDuration
        {
            get { return srcollAniDuration; }
            set
            {
                srcollAniDuration = value;
                //scrollWayPointAni.Duration = srcollAniDuration;
            }
        }

        /// <summary>
        /// Get number of items of List.
        /// </summary>
        public int NumOfItem
        {
            get { return itemList.Count; }
        }

        /// <summary>
        /// Update View specified by the itemIndex.
        /// </summary>
        /// <param name="itemIndex">The index of item of List.</param>
        public void UpdateItem(int itemIndex)
        {
            Tizen.Log.Debug("NUI", "Update item  " + itemIndex);
            if (IsValidItemIndex(itemIndex) == false)
            {
                Tizen.Log.Debug("NUI", "Index out of range");
                return;
            }

            if (itemIndex < headItemIndex || itemIndex > tailItemIndex)
            {
                Tizen.Log.Debug("NUI", "Index out of visible area");
                return;
            }

            ListItem item = itemList[itemIndex];

            if (item.itemView != null)
            {
                adapter.UpdateItem(itemIndex, item.itemView);
            }
        }

        /// <summary>
        /// Sets the data behind this List.
        /// </summary>
        /// <param name="adapter">The ListAdapter which is responsible for maintaining the data
        /// backing this list and for producing a view to represent an item in the data set.</param>
        public void SetBridge(ListBridge adapter)
        {
            Tizen.Log.Debug("NUI", "..SetAdapter in.");
            Debug.Assert(adapter != null);

            if (this.adapter != null)
            {
                return;
            }

            ResetList();

            this.adapter = adapter;

            //request layout
            listItemCount = adapter.GetCount();

            ListItem item = null;

            Tizen.Log.Debug("NUI", "..SetAdapter...listItemCount:" + listItemCount);
            for (int i = 0; i < listItemCount; ++i)
            {
                item = new ListItem();
                itemList.Add(item);
                item.index = i;
                
                if (attrsApplied == true)
                {
                    item.rect.X = 0;
                    item.rect.Y = (i == 0) ? 0 : itemList[i - 1].rect.Bottom() + itemSpace;
                    item.rect.Width = this.SizeWidth - (margin[0] + margin[2]);
                    item.rect.Height = adapter.GetItemHeight(i); ;
                }

                Tizen.Log.Debug("NUI", "...item.rect.Height :" + item.rect.Height + "attrsApplied : " + attrsApplied);
            }

            Tizen.Log.Debug("NUI", ".adapter.GetItemTypeCount" + adapter.GetItemTypeCount());
            adapter.DataChangeEvent += OnDataChange;
            SetViewTypeCount(adapter.GetItemTypeCount());

            Tizen.Log.Debug("NUI", ".SetAdapter.attrsApplied:.." + attrsApplied);
            Load();
        }

        /// <summary>
        /// Get the ListAdapter of List.
        /// </summary>
        /// <returns>The ListAdapter of List.</returns>
        public ListBridge GetAdapter()
        {
            return adapter;
        }

        /// <summary>
        /// Page up this List and Change focus according to the page size.
        /// </summary>
        public void PageUp()
        {
            Tizen.Log.Debug("NUI", "...");
            if (attrsApplied == false)
            {
                return ;
            }

            int itemCount = PageItemNum();

            if (itemCount >= curFocusItemIndex)
            {
                SetFocus(0);
            }
            else
            {
                int focusIndex = curFocusItemIndex - itemCount;

                //int moveDis = itemList[curFocusItemIndex].rect.Y - itemList[focusIndex].rect.Y;
                itemGroupRect.Y = (itemGroupRect.Y + this.SizeHeight) > 0 ? 0 : (itemGroupRect.Y + itemList[curFocusItemIndex].rect.Y - itemList[focusIndex].rect.Y);

                UpdateInMemoryItems(itemGroupRect);
                itemGroup.PositionY = itemGroupRect.Y;
                SetFocus(focusIndex);

            }
        }

        /// <summary>
        /// Page down this List and Change focus according to the page size.
        /// </summary>
        public void PageDown()
        {
            Tizen.Log.Debug("NUI", "...");
            if (attrsApplied == false)
            {
                return;
            }

            int itemCount = PageItemNum();

            if (itemList.Count - itemCount - 1 <= curFocusItemIndex)
            {
                SetFocus(itemList.Count - 1);
            }
            else
            {
                int focusIndex = curFocusItemIndex + itemCount;
                float moveDis = itemList[focusIndex].rect.Y - itemList[curFocusItemIndex].rect.Y;
                itemGroupRect.Y = (itemGroupRect.Y + itemGroupRect.Height - this.SizeHeight) < moveDis ? this.SizeHeight - itemGroupRect.Height : (itemGroupRect.Y - moveDis);

                UpdateInMemoryItems(itemGroupRect);
                itemGroup.PositionY = itemGroupRect.Y;

                SetFocus(focusIndex);
            }
        }

        /// <summary>
        /// Scroll item whoes index is firstShowItemIndex to the top of list view.
        /// </summary>
        /// <param name="firstShowItemIndex"> The first item index on dest page </param>
        public void ScrollList(int firstShowItemIndex)
        {
            Tizen.Log.Debug("NUI", "... " + firstShowItemIndex);
            if (IsValidItemIndex(firstShowItemIndex) == false || itemGroupRect.Height <= SizeHeight)
            {
                return;
            }

            ListItem item = itemList.ElementAt(firstShowItemIndex);

            float posYOnList = item.rect.Y + itemGroupRect.Y;
            if (itemGroupRect.Y - posYOnList <= SizeHeight - itemGroupRect.Height)
            {
                itemGroupRect.Y = SizeHeight - itemGroupRect.Height;
            }
            else
            {
                itemGroupRect.Y = itemGroupRect.Y - posYOnList;
            }

            UpdateInMemoryItems(itemGroupRect);
            itemGroup.PositionY = itemGroupRect.Y;
            ChangeFocus(firstShowItemIndex);
            scrollIndex = curFocusItemIndex;
        }

        /// <summary>
        /// Move focus according to parameter direction
        /// </summary>
        /// <param name="direction">Focus move direction</param>
        public void MoveFocus(MoveDirection direction)
        {
            if (MoveDirection.Up == direction)
            {
                MovePre();
            }
            else if (MoveDirection.Down == direction)
            {
                MoveNext();
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
            if (IsValidItemIndex(itemIndex))
            {
                item = itemList[itemIndex];
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
                listEventHandlers += value;
            }

            remove
            {
                listEventHandlers -= value;
            }
        }

        public void Update()
        {
            OnUpdate();
        }

        /// <summary>
        /// Update when List attributes changed.
        /// </summary>
        protected void OnUpdate()
        {
            if (topShadowView == null)
            {
                topShadowView = new ImageView();
                topShadowView.Name = "TopShadowView";
                this.Add(topShadowView);
            }

            if (bottomShadowView == null)
            {
                bottomShadowView = new ImageView();
                bottomShadowView.Name = "BottomShadowView";
                this.Add(bottomShadowView);
            }

            attrsApplied = true;
            if (adapter != null)
            {
                // Update Items rect
                for (int i = 0; i < itemList.Count(); i++)
                {
                    ListItem item = itemList[i];
                    item.rect.X = 0;
                    item.rect.Y = (i == 0) ? 0 : itemList[i - 1].rect.Bottom() + itemSpace;
                    item.rect.Width = this.SizeWidth - (margin[0] + margin[2]);
                    item.rect.Height = adapter.GetItemHeight(item.index);
                }

                Load();
            }
        }

        /// <summary>
        /// Called when the control gain key input focus.
        /// </summary>
        public void OnFocusGained()
        {
            Tizen.Log.Debug("NUI", " OnFocusGained >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

            int focusItemIndex = curFocusItemIndex < 0 ? 0 : curFocusItemIndex;

            SetFocus(focusItemIndex);
        }

        /// <summary>
        /// Called when the control loses key input focus.
        /// </summary>
        public void OnFocusLost()
        {
            Tizen.Log.Debug("NUI", "<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");

            if (IsValidItemIndex(curFocusItemIndex))
            {
                ListItem focusOutItem = itemList[curFocusItemIndex];
                focusOutItem.ScaleItem(new Vector3(1.0f, 1.0f, 1.0f), FocusOutScaleAnimationAttrs);

                adapter.FocusChange(curFocusItemIndex, focusOutItem.itemView, false);
            }

            flagFirstIn = true;
        }

        private void OnListEvent(ListEventArgs e)
        {
            if (listEventHandlers != null)
            {
                listEventHandlers(this, e);
            }
        }

        /// <summary>
        /// Load Item in the List.
        /// </summary>
        private void Load()
        {
            Tizen.Log.Debug("NUI", "..Load..in .");

            Tizen.Log.Debug("NUI", "Load.attrsApplied." + attrsApplied);
            flagFirstIn = true;
            listItemCount = adapter.GetCount();
            if (listItemCount == 0)
            {
                return;
            }

            Tizen.Log.Debug("NUI", "..Load.to UpdateItemGroupSize..");

            UpdateItemGroupSize();
            InitItemGroupPos();
            UpdateInMemoryItems(itemGroupRect);

            Tizen.Log.Debug("NUI", "Load.curFocusItemIndex.." + curFocusItemIndex);
            if (HasFocus() == true)
            {
                SetFocus(curFocusItemIndex);
            }

            Tizen.Log.Debug("NUI", "..Load out.");
        }

        /// <summary>
        /// Add a new Item to the list.
        /// </summary>
        private void AddItem()
        {
            Tizen.Log.Debug("NUI", "...itemList.Count:" + itemList.Count + " attrsApplied:" + attrsApplied);
            ListItem item = new ListItem();
            item.index = itemList.Count;
            itemList.Add(item);

            item.rect.X = 0;
            item.rect.Y = (item.index == 0) ? 0 : itemList[item.index - 1].rect.Bottom() + itemSpace;
            item.rect.Width = this.SizeWidth - (margin[0] + margin[2]);
            item.rect.Height = adapter.GetItemHeight(item.index);

            UpdateItemGroupSize();

            if (item.index <= tailItemIndex + PreloadBackItemSize)
            {
                LoadItem(item);
            }

            headItemIndex = GetItemIndexByPosY(-itemGroupRect.Y, true);
            tailItemIndex = GetItemIndexByPosY(-itemGroupRect.Y + this.SizeHeight, false);
            Tizen.Log.Debug("NUI", "tailItemIndex: " + tailItemIndex);

            if (curFocusItemIndex == -1 && HasFocus() == true)
            {
                SetFocus(0);
            }
        }

        private void AddAll(int count)
        {
            for (int i = 0; i < count; i++)
            {
                AddItem();
            }

            if (HasFocus() == true)
            {
                SetFocus(curFocusItemIndex < 0 ? 0 : curFocusItemIndex);
            }
        }

        private void DeleteItem(int fromItemIndex, int deleteItemNum)
        {
            if (fromItemIndex < 0 || fromItemIndex + deleteItemNum > itemList.Count)
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
                startPosOfItemGroup = itemList[fromItemIndex - 1].rect.Bottom();
            }

            //update pos of items behind deleted items
            for (int i = toItemIndex + 1; i <= itemList.Count - 1; ++i)
            {
                itemList[i].rect.Y = startPosOfItemGroup + itemSpace;
                startPosOfItemGroup += itemList[i].rect.Height;
            }

            for (int i = fromItemIndex; i <= toItemIndex; i++)
            {
                ListItem item = itemList[i];
                if (null != curInMemoryItemList.Find((o) => { return o == item; }))
                {
                    if (i == curFocusItemIndex)
                    {
                        item.itemView.Scale = new Vector3(1.0f, 1.0f, 1.0f);
                    }

                    UnloadItem(item);

                    curInMemoryItemList.Remove(item);
                }
            }

            itemList.RemoveRange(fromItemIndex, deleteItemNum);

            if (itemList.Count == 0)
            {
                itemGroupRect.Height = 0;
                itemGroup.PositionY = itemGroupRect.Y;
                itemGroup.SizeHeight = itemGroupRect.Height;

                headItemIndex = -1;
                tailItemIndex = -1;
                return;
            }

            //refresh item pos
            for (int i = fromItemIndex; i <= itemList.Count - 1; ++i)
            {
                ListItem item = itemList[i];
                item.index = i;

                RefreshItemPosition(item);
            }

            itemGroupRect.Height = itemList[itemList.Count - 1].rect.Bottom();


            //update focus item index
            int tempFocusIndex = curFocusItemIndex;
            if (curFocusItemIndex > toItemIndex)
            {
                curFocusItemIndex -= deleteItemNum;
            }
            else if (curFocusItemIndex >= itemList.Count)
            {
                curFocusItemIndex = itemList.Count - 1;
            }

            if (curFocusItemIndex != tempFocusIndex)
            {
                while (!IsValidItemIndex(curFocusItemIndex))
                {
                    curFocusItemIndex--;
                    if (curFocusItemIndex == -1)
                    {
                        break;
                    }
                }
            }

            if (headItemIndex <= toItemIndex && tailItemIndex >= fromItemIndex)
            {
                if (tailItemIndex >= itemList.Count)
                {
                    tailItemIndex = itemList.Count - 1;
                }

                Position itemGroupPos = new Position(0, 0, 0);
                bool flagScrollItem = CalItemGroupPosByFocusItemIndex(curFocusItemIndex, itemGroupPos);
                itemGroupRect.Y = itemGroupPos.Y;

                headItemIndex = -1;
                tailItemIndex = -1;
                UpdateInMemoryItems(itemGroupRect);

                flagFirstIn = true;
                ChangeFocus(curFocusItemIndex);
            }
            else if (headItemIndex > toItemIndex)
            {
                for (int i = fromItemIndex; i <= toItemIndex; i++)
                {
                    ListItem item = itemList[i];
                    itemGroupRect.Y += item.rect.Height + itemSpace;
                }
            }

            itemGroup.PositionY = itemGroupRect.Y;
            itemGroup.SizeHeight = itemGroupRect.Height;
        }

        private void InsertItem(int index)
        {
            Tizen.Log.Debug("NUI", "index:" + index);
            ListItem item = null;
            item = new ListItem();
            item.index = index;
            itemList.Insert(index, item);

            item.rect.X = 0;
            item.rect.Y = (index == 0) ? 0 : itemList[index - 1].rect.Bottom() + itemSpace;
            item.rect.Width = this.SizeWidth - (margin[0] + margin[2]);
            item.rect.Height = adapter.GetItemHeight(index);


            ListItem itemTemp = null;
            for (int i = index + 1; i < listItemCount; i++)
            {
                itemTemp = itemList[i];
                itemTemp.index = i;
                itemTemp.rect.Y = itemList[i - 1].rect.Bottom() + itemSpace;
                if (itemTemp.itemView != null)
                {
                    itemTemp.itemView.PositionY = itemTemp.rect.Y;
                }
            }

            if (curFocusItemIndex >= index)
            {
                itemGroupRect.Y -= item.rect.Height;
                itemGroup.PositionY = itemGroupRect.Y;
                curFocusItemIndex++;
                scrollIndex = curFocusItemIndex;
            }

            UpdateItemGroupSize();

            //if (index >= (headItemIndex <= currentAttrs.PreloadFrontItemSize ? 0 : headItemIndex - currentAttrs.PreloadFrontItemSize) && index <= tailItemIndex + currentAttrs.PreloadBackItemSize)
            if (index >= (headItemIndex <= PreloadFrontItemSize ? 0 : headItemIndex - PreloadFrontItemSize) && index <= tailItemIndex + PreloadBackItemSize)
            {
                LoadItem(item);
            }

            tailItemIndex = GetItemIndexByPosY(-itemGroupRect.Y + this.SizeHeight, false);
        }

        private void Clear()
        {
            foreach (ListItem item in itemList)
            {
                item.Dispose();
            }

            Tizen.Log.Debug("NUI", "" + curInMemoryItemList);
            Tizen.Log.Debug("NUI", "" + curInMemoryItemList.Count());
            foreach (ListItem item in curInMemoryItemList)
            {
                UnloadItem(item);
            }

            Tizen.Log.Debug("NUI", "" + unloadItemList);
            Tizen.Log.Debug("NUI", "" + unloadItemList.Count());
            foreach (ListItem item in unloadItemList)
            {
                UnloadItem(item);
            }

            unloadItemList.Clear();
            curInMemoryItemList.Clear();

            itemList.Clear();
            curFocusItemIndex = -1;
            scrollIndex = 0;
            headItemIndex = -1;
            tailItemIndex = -1;

            Tizen.Log.Debug("NUI", "...");
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
            Tizen.Log.Debug("NUI", ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

            int focusItemIndex = curFocusItemIndex < 0 ? 0 : curFocusItemIndex;

            SetFocus(focusItemIndex);
        }

        /// <summary>
        /// Called when the control loses key input focus.
        /// </summary>
        /// <param name="sender">the object</param>
        /// <param name="e">the args of the event</param>
        private void OnFocusLost(object sender, EventArgs e)
        {
            Tizen.Log.Debug("NUI", "<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");

            if (IsValidItemIndex(curFocusItemIndex))
            {
                ListItem focusOutItem = itemList[curFocusItemIndex];
                focusOutItem.ScaleItem(new Vector3(1.0f, 1.0f, 1.0f), FocusOutScaleAnimationAttrs);

                adapter.FocusChange(curFocusItemIndex, focusOutItem.itemView, false);
            }

            flagFirstIn = true;
        }

        private void OnDataChange(object o, ListBridge.DataChangeEventArgs e)
        {
            Tizen.Log.Debug("NUI", "DataChangeType:" + e.ChangeType);
            StopAllAni();

            //int oldItemCount = listItemCount;
            listItemCount = adapter.GetCount();

            switch (e.ChangeType)
            {
                case ListBridge.DataChangeEventType.Add:
                    AddItem();
                    break;
                case ListBridge.DataChangeEventType.AddAll:
                    AddAll((e.data as List<object>).Count);
                    break;
                case ListBridge.DataChangeEventType.Remove:
                    DeleteItem(e.param[0], e.param[1]);
                    break;
                case ListBridge.DataChangeEventType.Insert:
                    InsertItem(e.param[0]);
                    break;
                case ListBridge.DataChangeEventType.Clear:
                    Clear();
                    break;
                case ListBridge.DataChangeEventType.Load:
                    Load();
                    break;
                default:

                    break;
            }

            Tizen.Log.Debug("NUI", "....End");
        }

        private void Initialize()
        {
            ClippingMode = ClippingModeType.ClipChildren;

            // Create and Initialize item group 
            itemGroup = new View();
            itemGroup.Name = "ItemGroup";
            itemGroup.PositionUsesPivotPoint = true;
            itemGroup.ParentOrigin = Tizen.NUI.ParentOrigin.TopCenter;
            itemGroup.PivotPoint = Tizen.NUI.PivotPoint.TopCenter;
            //itemGroup.WidthResizePolicy = ResizePolicyType.FillToParent;
            //itemGroup.HeightResizePolicy = ResizePolicyType.FitToChildren;
            this.Add(itemGroup);

            Relayout += OnRelayout;
            FocusGained += OnFocusGained;
            FocusLost += OnFocusLost;

            Create();
        }

        private void Create()
        {

            scrollWayPointAni = new LinkerAnimation(itemGroup, 100, 100, 0);
            /*unload front item when via event happen*/

            scrollWayPointAni.ViaEvent += delegate(object o, EventArgs e)
            {
                Tizen.Log.Debug("NUI", " scrollWayPointAni itemGroup: " + itemGroup.PositionX + ", " + itemGroup.PositionY + ", " + itemGroup.SizeWidth + ", " + itemGroup.SizeHeight);
                Tizen.Log.Debug("NUI", "itemGroupRect: " + itemGroupRect.X + ", " + itemGroupRect.Y + ", " + itemGroupRect.Width + ", " + itemGroupRect.Height);
                Rect itemGroupVisualRect = new Rect(itemGroup.PositionX, itemGroup.PositionY, itemGroup.SizeWidth, itemGroup.SizeHeight);
                UpdateInMemoryItems(itemGroupVisualRect);

                int itemIndex = focusMoveQ.Dequeue();
                ChangeFocus(itemIndex);


                //if (headItemIndex < (this.attrs as Attributes).PreloadFrontItemSize)
                if (headItemIndex < PreloadFrontItemSize)
                {
                    return;
                }

                if (unloadItemList.Count > 0)
                {
                    ListItem item = unloadItemList[0];
                    UnloadItem(item);
                    unloadItemList.Remove(item);
                }

                if (itemIndex == 0)
                {
                    PerformCircular(true);
                }
                else if (itemIndex == listItemCount - 1)
                {
                    PerformCircular(false);
                }
            };
        }

        private void InitItemGroupPos()
        {
            Tizen.Log.Debug("NUI", " InitItemGroupPos ~~~ attrsApplied: " + attrsApplied + " curFocusItemIndex: " + curFocusItemIndex);
            itemGroupRect.X = 0; // Laid top-center on List
            itemGroupRect.Y = margin[1];

            if (IsValidItemIndex(curFocusItemIndex))
            {
                Rect focusItemRect = itemList[curFocusItemIndex].rect;
                if (itemGroupRect.Y + focusItemRect.Bottom() > SizeHeight)
                {
                    if (IsValidItemIndex(curFocusItemIndex - 1))
                    {
                        itemGroupRect.Y = itemList[curFocusItemIndex - 1].rect.Height / 2 - itemList[curFocusItemIndex - 1].rect.Bottom();
                    }
                }
            }

            itemGroup.PositionX = itemGroupRect.X;
            itemGroup.PositionY = itemGroupRect.Y;
            Tizen.Log.Debug("NUI", "itemGroupRect: " + itemGroupRect.X + ", " + itemGroupRect.Y + ", " + itemGroupRect.Width + ", " + itemGroupRect.Height);
            Tizen.Log.Debug("NUI", "itemGroup: " + itemGroup.PositionX + ", " + itemGroup.PositionY + ", " + itemGroup.SizeWidth + ", " + itemGroup.SizeHeight);
        }

        private void UpdateItemGroupSize()
        {
            if (itemList.Count == 0)
            {
                itemGroupRect.Height = 0;
            }
            else
            {
                itemGroupRect.Height = itemList[itemList.Count - 1].rect.Bottom();
            }

            itemGroupRect.Width = (int)(this.SizeWidth - (margin[0] + margin[2]));
            Tizen.Log.Debug("NUI", "UpdateItemGroupSize itemGroupRect: " + itemGroupRect.X + ", " + itemGroupRect.Y + ", " + itemGroupRect.Width + ", " + itemGroupRect.Height);

            itemGroup.SizeWidth = itemGroupRect.Width;
            itemGroup.SizeHeight = itemGroupRect.Height;
       }

        private void RefreshItemPosition(ListItem item)
        {
            if (null == item)
            {
                return;
            }

            if (null != item.itemView)
            {
                item.itemView.PositionX = item.rect.X;
                item.itemView.PositionY = item.rect.Y;
            }
        }

        private void UpdateInMemoryItems(Rect groupRect, bool flagFocusAni = false)
        {
            Rect visibleAreaOfItemGroup = new Rect(-groupRect.X, -groupRect.Y, this.SizeWidth, this.SizeHeight);

            int newHeadItemIndex = -1;
            int newTailItemIndex = -1;
            Tizen.Log.Debug("NUI", " UpdateInMemoryItems groupRect.X : " + groupRect.X + ",groupRect.Y : " + groupRect.Y);
            Tizen.Log.Debug("NUI", " UpdateInMemoryItems this.SizeWidth : " + this.SizeWidth + ",this.SizeHeight : " + this.SizeHeight);
            Tizen.Log.Debug("NUI", " UpdateInMemoryItems visibleAreaOfItemGroup.Y : " + visibleAreaOfItemGroup.Y + ",Bottom : " + visibleAreaOfItemGroup.Bottom());

            newHeadItemIndex = GetItemIndexByPosY(visibleAreaOfItemGroup.Y, true);
            newTailItemIndex = GetItemIndexByPosY(visibleAreaOfItemGroup.Bottom(), false);
            Tizen.Log.Debug("NUI", ".UpdateInMemoryItems : headItemIndex, tailItemIndex, newHeadItemIndex, newTailItemIndex." + headItemIndex + tailItemIndex.ToString() + newHeadItemIndex + newTailItemIndex);

            List<ListItem> loadList = new List<ListItem>();
            List<ListItem> unloadList = new List<ListItem>();
            List<ListItem> scrollList = new List<ListItem>();

            Tizen.Log.Debug("NUI", ".UpdateInMemoryItems : headItemIndex, tailItemIndex, newHeadItemIndex, newTailItemIndex." + headItemIndex + tailItemIndex + newHeadItemIndex + newTailItemIndex);
            GetItemChangeInfo(headItemIndex, tailItemIndex, newHeadItemIndex, newTailItemIndex, loadList, unloadList, scrollList);
            Tizen.Log.Debug("NUI", "...loadList:" + loadList.Count + "...unloadList:" + unloadList.Count + "...scrollList:" + scrollList.Count);
 
            headItemIndex = newHeadItemIndex;
            tailItemIndex = newTailItemIndex;
            Tizen.Log.Debug("NUI", "...headItemIndex:" + headItemIndex);
            Tizen.Log.Debug("NUI", "...tailItemIndex:" + tailItemIndex);

            Tizen.Log.Debug("NUI", "... StateRecycleEnable:" + StateRecycleEnable + " flagFocusAni:" + flagFocusAni);

            //need to conside scroll, don't load and unload right now
            foreach (ListItem item in unloadList)
            {
                if (StateRecycleEnable == false || flagFocusAni == false)
                {
                        UnloadItem(item);                       
                        curInMemoryItemList.Remove(item);
                }
                else
                {
                    if (null == unloadItemList.Find((o) => { return o == item; }))
                    {
                        unloadItemList.Add(item);
                    }
                }
            }

            Tizen.Log.Debug("NUI", " UpdateInMemoryItems Start LoadItem. ");
            foreach (ListItem item in loadList)
            {
                LoadItem(item);
            }

            Tizen.Log.Debug("NUI", "...");

        }

        private void LoadItem(ListItem item)
        {
            Tizen.Log.Debug("NUI", ".LoadItem...item.index:" + item.index);
            if (null != curInMemoryItemList.Find((o) => { return o == item; }))
            {
                return;
            }

            curInMemoryItemList.Add(item);
            Tizen.Log.Debug("NUI", "....");

            if (null != unloadItemList.Find((o) => { return o == item; }))
            {
                unloadItemList.Remove(item);
            }

            Tizen.Log.Debug("NUI", ".LoadItem...item.index:" + item.index);
            View temp = GetRecycleView(item.index);
            Tizen.Log.Debug("NUI", "....StateRecycleEnable:" + StateRecycleEnable + " temp:" + temp);
            if (StateRecycleEnable == true && temp != null)
            {
                Tizen.Log.Debug("NUI", ".LoadItem...-----------");
                item.itemView = temp;
                adapter.UpdateItem(item.index, item.itemView);
            }
            else
            {
                item.itemView = adapter.GetItemView(item.index);
                Tizen.Log.Debug("NUI", ".LoadItem item.itemView :" + item.itemView);
                item.itemView.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
                item.itemView.PivotPoint = Tizen.NUI.PivotPoint.Center;
                item.itemView.PositionUsesPivotPoint = false;
            }

            Tizen.Log.Debug("NUI", "....");
            
            item.itemView.PositionX = item.rect.X;
            item.itemView.PositionY = item.rect.Y;
            item.itemView.Size2D = new Size2D((int)item.rect.Width, (int)item.rect.Height);

            itemGroup.Add(item.itemView);
            Tizen.Log.Debug("NUI", "itemGroup.Add...X:" + item.itemView.PositionX + "y : " + item.itemView.PositionY + " Height : " + item.itemView.SizeHeight);

            ListEventArgs evtArgs = new ListEventArgs();
            evtArgs.EventType = ListEventType.ItemScrolledIn;
            evtArgs.param[0] = item.index;

            OnListEvent(evtArgs);

        }

        private void UnloadItem(ListItem item)
        {
            Tizen.Log.Debug("NUI", "....item.index:" + item.index);
            itemGroup.Remove(item.itemView);

            if (flagEnableRecycle == false)
            {
                adapter.UnloadItem(item.index, item.itemView);
            }
            else
            {
                AddRecycleView(item.index,item.itemView);
            }

            ListEventArgs evtArgs = new ListEventArgs();
            evtArgs.EventType = ListEventType.ItemScrolledOut;
            evtArgs.param[0] = item.index;

            OnListEvent(evtArgs);

            item.itemView = null;
        }

        private void GetItemChangeInfo(int curHeadItemIndex, int curTailItemIndex, int newHeadItemIndex, int newTailItemIndex, List<ListItem> loadItem, List<ListItem> unloadItem, List<ListItem> nochangedItem)
        {
            List<int> loadIndexList = new List<int>();
            List<int> unloadIndexList = new List<int>();
            List<int> nochangeIndexList = new List<int>();

            CompareIndexRange(curHeadItemIndex, curTailItemIndex, newHeadItemIndex, newTailItemIndex, itemList.Count, loadIndexList, unloadIndexList, nochangeIndexList);

            foreach (int i in loadIndexList)
            {
                loadItem.Add(itemList[i]);
            }

            foreach (int i in unloadIndexList)
            {
                unloadItem.Add(itemList[i]);
            }

            foreach (int i in nochangeIndexList)
            {
                nochangedItem.Add(itemList[i]);
            }
        }

        private int GetItemIndexByPosY(float posY, bool isHead)
        {
            Tizen.Log.Debug("NUI", "GetItemIndexByPosY posY: " + posY + " isHead: " + isHead);
            int itemIndex = -1;

            int itemCount = itemList.Count;

            if (itemCount == 0)
            {
                return -1;
            }

            float firstItemStartPos = itemList[0].rect.Y;
            float lastItemEndPos = itemList[itemCount - 1].rect.Bottom();
            Tizen.Log.Debug("NUI", " firstItemStartPos: " + firstItemStartPos + " lastItemEndPos: " + lastItemEndPos);

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
                                if (itemList[begin].rect.Bottom() <= posY && itemList[end].rect.Y >= posY)
                                {
                                    itemIndex = end;
                                    break;
                                }
                            }

                            ListItem item = itemList[itemIndex];

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
                            if (itemList[begin].rect.Bottom() <= posY && itemList[end].rect.Y >= posY)
                            {
                                itemIndex = begin;
                                break;
                            }
                        }

                        ListItem item = itemList[itemIndex];

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

        private void CompareIndexRange(int oldStart, int oldEnd, int newStart, int newEnd, int maxIndex, List<int> loadItem, List<int> unloadItem, List<int> noChangeItem)
        {
            int headBufferNum = PreloadFrontItemSize;
            int tailBufferNum = PreloadBackItemSize;
            int realStart = -1;
            int realEnd = -1;
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

        private void MovePre()
        {
            bool bMoved = false;
            while (scrollIndex > 0)
            {
                if (adapter.IsItemEnabled(--scrollIndex) == true)
                {
                    MoveFocus(scrollIndex);
                    bMoved = true;
                    break;
                }
            }    

            if (bMoved == false && 0 == scrollIndex)
            {
                circular = true;
                PerformCircular(true);
            }
        }

        private void MoveNext()
        {
            bool bMoved = false;
            Tizen.Log.Debug("NUI", " -1-----MoveNext scrollIndex : " + scrollIndex + ", listItemCount : " +  listItemCount);

            while (scrollIndex < listItemCount - 1)
            {
                if (adapter.IsItemEnabled(++scrollIndex) == true)
                {
                    MoveFocus(scrollIndex);
                    bMoved = true;
                    break;
                }
            }

            if (bMoved == false && listItemCount - 1 == scrollIndex)
            {
                PerformCircular(false);
            }
        }

        private void MoveFocus(int focusItemIndex)
        {
            if (!IsValidItemIndex(focusItemIndex))
            {
                return;
            }

            Tizen.Log.Debug("NUI", "X1 MoveFocus at FocusIndex : " + focusItemIndex);

            ListItem item = itemList[focusItemIndex];

            //calculate dest pos of item group by dest item rect
            Position itemGroupPos = new Position(0, 0, 0);
            bool flagScrollItem = CalItemGroupPosByFocusItemIndex(focusItemIndex, itemGroupPos);

            itemGroupRect.X = (int)itemGroupPos.X;
            itemGroupRect.Y = (int)itemGroupPos.Y;
            Tizen.Log.Debug("NUI", "X1 MoveFocus at FocusIndex itemGroupRect X,Y : " + itemGroupRect.X + itemGroupRect.Y);

            if (true == flagScrollItem)
            {
                Tizen.Log.Debug("NUI", " start to scroll.");
                focusMoveQ.Enqueue(focusItemIndex);
                scrollWayPointAni.SetDestination("PositionY", itemGroupRect.Y);
                scrollWayPointAni.Play();
            }
            else
            {
                Tizen.Log.Debug("NUI", " no scroll.");
                if (scrollWayPointAni.IsPlaying() == false)
                {
                    ChangeFocus(focusItemIndex);
                }
            }
        }

        private void NotifyFocusChange(int from, int to)
        {
            if (flagFirstIn == false && from == to)
            {
                return;
            }

            flagFirstIn = false;
            if (IsValidItemIndex(from) == true && itemList[from].itemView != null && from != to)
            {
                ListItem preFocusItem = itemList[from];
                adapter.FocusChange(from, preFocusItem.itemView, false);
            }

            if (IsValidItemIndex(to) == true && itemList[to].itemView != null)
            {
                ListItem curFocusItem = itemList[to];
                adapter.FocusChange(to, curFocusItem.itemView, true);
            }

            ListEventArgs evtArgs = new ListEventArgs();
            evtArgs.EventType = ListEventType.FocusChange;
            evtArgs.param[0] = from;
            evtArgs.param[1] = to;

            OnListEvent(evtArgs);
        }

        private void SetFocus(int focusItemIndex)
        {
            if (attrsApplied == false)
            {
                curFocusItemIndex = focusItemIndex;
                return;
            }

            if (!IsValidItemIndex(focusItemIndex) || !adapter.IsItemEnabled(focusItemIndex))
            {
                return;
            }

            ListItem item = itemList[focusItemIndex];

            //calculate dest pos of item group by dest item rect
            Position itemGroupPos = new Position(0, 0, 0);
            bool flagScrollItem = CalItemGroupPosByFocusItemIndex(focusItemIndex, itemGroupPos);
            Tizen.Log.Debug("NUI", "itemGroup: " + itemGroup.PositionX + ", " + itemGroup.PositionY + ", " + itemGroup.SizeWidth + ", " + itemGroup.SizeHeight);

            Tizen.Log.Debug("NUI", "flagScrollItem: " + flagScrollItem);
            itemGroupRect.Y = (int)itemGroupPos.Y;

            if (true == flagScrollItem)
            {
                UpdateInMemoryItems(itemGroupRect);
                itemGroup.PositionY = itemGroupRect.Y;
            }

            Tizen.Log.Debug("NUI", "itemGroupRect: " + itemGroupRect.X + ", " + itemGroupRect.Y + ", " + itemGroupRect.Width + ", " + itemGroupRect.Height);

            ChangeFocus(focusItemIndex);
            scrollIndex = curFocusItemIndex;
            // UpdateScroll(focusItemIndex);
        }

        private void ChangeFocus(int focusItemIndex)
        {
            if (IsValidItemIndex(curFocusItemIndex) && curFocusItemIndex != focusItemIndex)
            {
                ListItem focusOutItem = itemList[curFocusItemIndex];
                focusOutItem.ScaleItem(new Vector3(1.0f, 1.0f, 1.0f), FocusOutScaleAnimationAttrs);
            }

            if (IsValidItemIndex(focusItemIndex))
            {
                ListItem focusInItem = itemList[focusItemIndex];
                focusInItem.ScaleItem(new Vector3(FocusInScaleFactor, FocusInScaleFactor, 1.0f), FocusInScaleAnimationAttrs);
            }

            if (topShadowView != null)
            {
                if (headItemIndex == 0 && itemGroup.PositionY + itemList[headItemIndex].rect.Top() >= 0)
                {
                    topShadowView.Hide();
                }
                else
                {
                    topShadowView.RaiseToTop();
                    topShadowView.Show();
                }
            }

            if (bottomShadowView != null)
            {
                if (tailItemIndex == NumOfItem - 1 && itemGroup.PositionY + itemList[tailItemIndex].rect.Bottom() <= this.SizeHeight)
                {
                    bottomShadowView.Hide();
                }
                else
                {
                    bottomShadowView.RaiseToTop();
                    bottomShadowView.Show();
                }
            }

            NotifyFocusChange(curFocusItemIndex, focusItemIndex);

            //preFocusItemIndex = curFocusItemIndex;
            curFocusItemIndex = focusItemIndex;
            Tizen.Log.Debug("NUI", " ############################--ChangeFocus curFocusItemIndex / focusItemIndex : ", curFocusItemIndex + "/" + focusItemIndex);
        }

        private bool IsValidItemIndex(int index)
        {
            return (index >= 0 && index < itemList.Count);
        }

        private bool CalItemGroupPosByFocusItemIndex(int focusIndex, Position itemGroupPos)
        {
            bool needScroll = false;
            Rect destItemRectOnList = new Rect(itemList[focusIndex].rect);
            destItemRectOnList.Y += itemGroupRect.Y;
            Tizen.Log.Debug("NUI", "X1itemGroupRect.Y: " + itemGroupRect.Y + " destItemRectOnList: " + destItemRectOnList.X + ", " + destItemRectOnList.Y + ", " + destItemRectOnList.Width + ", " + destItemRectOnList.Height);

            float startFocusRange = (focusIndex == 0) ? margin[1] : itemList[focusIndex - 1].rect.Height / 2;
            float endFocusRange = (focusIndex == itemList.Count - 1) ? this.SizeHeight - margin[3] : this.SizeHeight - itemList[focusIndex + 1].rect.Height / 2;
            Tizen.Log.Debug("NUI", "startFocusRange: " + startFocusRange + ", endFocusRange:" + endFocusRange);
            itemGroupPos.Y = itemGroupRect.Y;
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

            if (tailItemIndex == itemList.Count - 1)
            {
                if (headItemIndex == 0)
                {
                    itemGroupPos.Y = margin[1];
                }
                else
                {
                    itemGroupPos.Y = this.SizeHeight - margin[3] - itemGroupRect.Height;
                }
            }

            Tizen.Log.Debug("NUI", "itemGroupPosX,Y : " + itemGroupPos.X + ", " + itemGroupPos.Y + "margin[1] : " + margin[1]);
            return needScroll;
        }

        private int PageItemNum()
        {
            if (IsValidItemIndex(curFocusItemIndex) == false)
            {
                return 0;
            }

            return (int)(this.SizeHeight / itemList[curFocusItemIndex].rect.Height);
        }

        private void ResetList()
        {
            //remove all views from the View, Clear everything out.
            Clear();
        }

        private void SetViewTypeCount(int typeCount)
        {
            if (typeCount < 1)
            {

                return;
            }

            viewTypeCount = typeCount;
            List<View>[] viewListArray = new List<View>[viewTypeCount];

            for (int i = 0; i < viewTypeCount; i++)
            {
                viewListArray[i] = new List<View>();
            }

            recycledViews = viewListArray;
        }

        private void AddRecycleView(int index, View view)
        {
            int viewType = adapter.GetItemType(index);

            if (viewType >= viewTypeCount)
            {
                return;
            }

            if (view != null)
            {
                view.Hide();
            }

            recycledViews[viewType].Add(view);
        }

        private View GetRecycleView(int index)
        {
            int viewType = adapter.GetItemType(index);

            if (viewType >= viewTypeCount)
            {
                return null;
            }

            View obj = null;
            int viewCount = recycledViews[viewType].Count;
            if (viewCount != 0)
            {
                obj = recycledViews[viewType][viewCount - 1];
                recycledViews[viewType].Remove(obj);
            }

            if (obj != null)
            {
                obj.Show();
            }

            return obj;
        }

        private void ClearRecycleBin()
        {
            for (int i = 0; i < viewTypeCount; i++)
            {
                for (int j = 0; j < recycledViews[i].Count; j++)
                {
                    UnloadRecycledView(i, recycledViews[i][j]);
                }

                recycledViews[i].Clear();
            }
        }

        private void UnloadRecycledView(int viewType, View view)
        {
            adapter.UnloadItemByViewType(viewType, view);
        }

        private void PerformCircular(bool forward)
        {
            if (circular == false
                || listItemCount <= 1 
                || (forward && !ForwardCirculation) 
                || (!forward && !BackwardCirculation))
            {
                circular = false;

                ListEventArgs evtArgs = new ListEventArgs();
                evtArgs.EventType = ListEventType.FocusMoveOut;
                evtArgs.param[0] = forward == true ? (int)MoveDirection.Up : (int)MoveDirection.Down;

                OnListEvent(evtArgs);
                return;
            }

            if (IsAniPlaying() == true)
            {
                return;
            }
           
            int focusItemIndex = 0;
            if (forward == true)
            {
                focusItemIndex = listItemCount - 1;
            }

            SetFocus(focusItemIndex);

            circular = false;
        }

        private bool IsAniPlaying()
        {
            bool isPlaying = false;
            
            return isPlaying;
        }

        private void StopAllAni()
        {
        }

        private class ListItem
        {
            public void Dispose()
            {
                scaleAni?.Stop();
                scaleAni?.Dispose();
                scaleAni = null;
            }

            public void ScaleItem(Vector3 scaleFactor, AnimationAttributes aniAttrs)
            {
                if (itemView == null || scaleFactor == null || aniAttrs == null)
                {
                    return;
                }

                if (scaleAni == null)
                {
                    scaleAni = new Animation();
                }

                if (scaleAni.State == Animation.States.Playing)
                {
                    scaleAni.Stop(Animation.EndActions.StopFinal);
                }

                scaleAni.Clear();
                scaleAni.AnimateTo(itemView, "Scale", scaleFactor, 0, aniAttrs.Duration, new AlphaFunction(aniAttrs.BezierPoint1, aniAttrs.BezierPoint2));
                scaleAni.Play();
            }

            public int index = -1;
            public View itemView = null;
            public Rect rect = new Rect(0, 0, 0, 0);

            private Animation scaleAni = null;
        }


        private bool attrsApplied = true;

        private ListBridge adapter = null;
        private int listItemCount;

        //private Rectangle listViewRect;
        private List<ListItem> itemList = new List<ListItem>();
        private List<ListItem> curInMemoryItemList = new List<ListItem>();
        private List<ListItem> unloadItemList = new List<ListItem>();

        private View itemGroup;
        private Rect itemGroupRect = new Rect(0, 0, 0, 0);

        private int headItemIndex = -1;
        private int tailItemIndex = -1;

        //for focus move
        private int curFocusItemIndex = -1;

        private Queue<int> focusMoveQ = new Queue<int>();

        private Queue<ImageView> focusBarQ = new Queue<ImageView>();

        //animation
        private int srcollAniDuration;
        private LinkerAnimation scrollWayPointAni;

        private int itemSpace = 0;
        private Vector4 margin = Vector4.Zero;

        private bool flagUpLoop = false;
        private bool flagDownLoop = false;

        private bool flagRememberLastFocusIndex = true;
        private bool flagEnableRecycle = true;
        
        private bool flagFirstIn = true;

        private ImageView topShadowView = null;
        private ImageView bottomShadowView = null;

        private List<View>[] recycledViews;
        private int viewTypeCount;

        private int scrollIndex;
        private bool circular = true;

        private EventHandler<ListEventArgs> listEventHandlers;

        /// <summary> Preload item buffer size at front </summary>
        private int preloadFrontItemSize;
        
        /// <summary> Preload item buffer size at back </summary>
        private int preloadBackItemSize;
        
        /// <summary> List scroll animation attributes </summary>
        private AnimationAttributes scrollAnimationAttrs;
        
        /// <summary> Focus in scale factor </summary>
        private float focusInScaleFactor;
        
        /// <summary> Focus in scale animation attributes </summary>
        private AnimationAttributes focusInScaleAnimationAttrs;
        
        /// <summary> Focus out scale animation attributes </summary>
        private AnimationAttributes focusOutScaleAnimationAttrs;
                
        private int listTopPadding;
        private int listBottomPadding;
		
        private int level;
        private ListView parentList;
        private ListView childList;
		
    }


}
