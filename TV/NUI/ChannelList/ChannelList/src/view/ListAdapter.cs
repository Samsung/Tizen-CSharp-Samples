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
using Tizen.NUI.BaseComponents;

/// <summary>
/// Namespace for Tizen.NUI package
/// </summary>
namespace ChannelList
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
    public abstract class ListAdapter
    {
        /// <summary>
        /// Construct ListAdapter with data.
        /// </summary>
        /// <param name="objects">data list</param>
        public ListAdapter(List<object> objects)
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
        public abstract float GetItemHeight(int index);

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
        /// <param name="viewType">Item type set by users.</param>
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
            Tizen.Log.Fatal("NUI.List", "Add item ...");
            listData.Add(data);

            // Send Add event.
            DataChangeEventArgs e = new DataChangeEventArgs();
            e.ChangeType = DataChangeEventType.Add;
            e.data = data;
            OnDataChangeEvent(this, e);
        }

        /// <summary>
        /// Adds the specified data at the end of the List.
        /// </summary>
        /// <param name="data">The dataList to add at the end of the List.</param>
        public void AddAll(List<object> data)
        {
            Tizen.Log.Fatal("NUI.List", "Add items ...");
            listData.AddRange(data);

            // Send AddAll event.
            DataChangeEventArgs e = new DataChangeEventArgs();
            e.ChangeType = DataChangeEventType.AddAll;
            e.data = data;
            OnDataChangeEvent(this, e);
        }

        /// <summary>
        /// Removes the specified data from the List.
        /// </summary>
        /// <param name="fromIndex">The from index of data to remove from the list.</param>
        /// <param name="removeNum">The remove number of data to remove from the list.</param>
        public void Remove(int fromIndex, int removeNum)
        {
            Tizen.Log.Fatal("NUI.List", "Remove item from " + fromIndex + ", number " + removeNum);
            if (fromIndex + removeNum > listData.Count)
            {
                return;
            }

            listData.RemoveRange(fromIndex, removeNum);

            // Send remove event.
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
            Tizen.Log.Fatal("NUI.List", "Insert item at " + index);
            listData.Insert(index, data);
            DataChangeEventArgs e = new DataChangeEventArgs();

            // Send insert event
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
            Tizen.Log.Fatal("NUI.List", "Clear ListView");
            DataChangeEventArgs e = new DataChangeEventArgs();
            // Clear event
            e.ChangeType = DataChangeEventType.Clear;
            OnDataChangeEvent(this, e);
            listData.Clear();

            // Load event
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
            /// <summary> Add data at one time. </summary>
            AddAll,
            /// <summary> Remove data. </summary>
            Remove,
            /// <summary> Insert data. </summary>
            Insert,
            /// <summary> Clear all data. </summary>
            Clear,
            /// <summary> Load data. </summary>
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

            private DataChangeEventType m_ChangeType; //change type
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

        private List<object> listData; //data list
        private EventHandler<DataChangeEventArgs> dataChangeEventHandlers; //date change event handler
    }
}
