/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using System.Collections.Specialized;

namespace Clock.Data
{
    /// <summary>
    /// Location data collection class
    /// </summary>
    public class LocationObservableCollection : List<Location>, INotifyCollectionChanged
    {
        /// <summary>
        /// The event that notifies dynamic changes, such as when items get added or removed
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        /// Adds Location object to a list
        /// </summary>
        /// <param name="item">Location object to add</param>
        /// <seealso cref="Location">
        public new void Add(Location item)
        {
            base.Add(item);
            CollectionChanged?.Invoke(this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        /// <summary>
        /// Inserts a collection of Locations in the list at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the new elements should be inserted.</param>
        /// <param name="collection">The collection of alarm records whose elements should be inserted into the LocationObservableCollection</param>
        public new void InsertRange(int index, IEnumerable<Location> collection)
        {
            base.InsertRange(index, collection);
            CollectionChanged?.Invoke(this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new List<Location>(collection), index));
        }

        /// <summary>
        /// Removes the specified Location object from the list.
        /// </summary>
        /// <param name="item">Location object to remove from the LocationObservableCollection</param>
        /// <seealso cref="Location">
        public new void Remove(Location item)
        {
            base.Remove(item);
            CollectionChanged?.Invoke(this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        }

        /// <summary>
        /// Removes all elements from the list.
        /// </summary>
        public new void Clear()
        {
            base.Clear();
            CollectionChanged?.Invoke(this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
