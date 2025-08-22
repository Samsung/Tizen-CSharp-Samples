/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Forms;

namespace UIComponents.Extensions
{
    /// <summary>
    /// Thumbnail index for view 
    /// </summary>
    public class ThumbnailIndex : View
    {
        ObservableCollection<ThumbnailItem> _items;

        public ThumbnailIndex()
        {
            _items = new ObservableCollection<ThumbnailItem>();
            _items.CollectionChanged += ItemsCollectionChanged;
        }

        /// <summary>
        /// List for Thumbnail items
        /// </summary>
        public IList<ThumbnailItem> ThumbnailItems
        {
            get
            {
                return _items;
            }
        }

        /// <summary>
        /// Called when items collection is changed.
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Argument of NotifyCollectionChangedEventArgs</param>
        void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    //
                    break;
                case NotifyCollectionChangedAction.Remove:
                    //
                    break;
                default: 
                    //
                    break;
            }
        }
    }
}
