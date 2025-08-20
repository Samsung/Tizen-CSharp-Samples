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
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.MediaHub
{
    /// <summary>
    /// The bridge for the Grid
    /// </summary>
    class GridListDataBridge : GridBridge
    {
        private int viewType;
        private bool editMode = false;

        /// <summary>
        /// Get/Set the editMode
        /// </summary>
        public bool EditMode
        {
            get
            {
                return editMode;
            }

            set
            {
                editMode = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="objects">the data object group</param>
        /// <param name="viewType">the type of the item</param>
        /// <param name="isInEdit">whether the item is editable</param>
        public GridListDataBridge(List<List<object>> objects, int viewType, bool isInEdit) : base(objects)
        {
            this.viewType = viewType;
            this.editMode = isInEdit;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="objects">the data object</param>
        /// <param name="viewType">the type of the item</param>
        /// <param name="isInEdit">whether the item is editable</param>
        public GridListDataBridge(List<object> objects, int viewType, bool isInEdit) : base(objects)
        {
            this.viewType = viewType;
            this.editMode = isInEdit;
        }

        /// <summary>
        /// Called when the focus changed in grid
        /// </summary>
        /// <param name="groupIndex">The index of the group</param>
        /// <param name="itemIndex">The index of the item</param>
        /// <param name="view">the item</param>
        /// <param name="FlagFocused">The flag to tell user it is focusGained or focusLost</param>
        /// <param name="bSelected">It is selected or not</param>
        public override void FocusChange(int groupIndex, int itemIndex, View view, bool FlagFocused, bool bSelected = false)
        {   
            ThumbnailView itemView = view as ThumbnailView;
            itemView.StateFocused = FlagFocused;
            Tizen.Log.Fatal("NUI", "FocusChange in GridView!");
            FootView footView = CommonResource.FootView;

            if (FlagFocused == true)
            {
                Tizen.Log.Fatal("NUI", " Gridlist view onFocus change index:" + itemIndex);
                ContentModel item = (ContentModel)GetData(itemIndex, groupIndex);
                FootModel footModel = new FootModel();
                footModel.ItemType = item.MediaItemType;
                footModel.Title = item.DisplayName;
                footModel.Size = item.Size;
                footModel.Format = item.Format;
                footModel.Data = item.Data;
                if (footView)
                {
                    footView.Update(footModel);
                }

                Tizen.Log.Fatal("NUI", " Gridlist view onFocus change footModel.ItemType:" + footModel.ItemType);
            }

        }

        /// <summary>
        /// Get the item according to the group index and item index.
        /// </summary>
        /// <param name="groupIndex">the group index</param>
        /// <param name="itemIndex">the item index</param>
        /// <returns>the specific item</returns>
        public override View GetItemView(int groupIndex, int itemIndex)
        {
           // int itemType = GetItemType(groupIndex, itemIndex);
            object data = GetData(itemIndex, groupIndex);
            View obj = null;
            switch (this.viewType)
            {
                case ContentViewType.ALL:

                    Tizen.Log.Fatal("NUI" ,"[ListView] will create ALL itemView");
                    obj = new AllContentThumbnailView(data, editMode);
                    break;
            }

            return obj;
        }

        /// <summary>
        /// Unload the item according to the group index and item index.
        /// </summary>
        /// <param name="groupIndex">the group index</param>
        /// <param name="itemIndex">the item index</param>
        /// <param name="view">the specific item</param>
        public override void UnloadItem(int groupIndex, int itemIndex, View view)
        {
            ThumbnailView itemView = view as ThumbnailView;
            if (itemView != null)
            {
                itemView.UnloadItem();
                itemView.Dispose();
                itemView = null;
            }
        }
        
        /// <summary>
        /// Unload item according to the type
        /// </summary>
        /// <param name="viewType">the view type</param>
        /// <param name="view">the view</param>
        public override void UnloadItemByViewType(int viewType, View view)
        {
        }

        /// <summary>
        /// update the item according to the group index and item index.
        /// </summary>
        /// <param name="groupIndex">the group index</param>
        /// <param name="itemIndex">the item index</param>
        /// <param name="view">the item</param>
        public override void UpdateItem(int groupIndex, int itemIndex, View view)
        {
            Tizen.Log.Fatal("NUI", "Gridlistview UpdateItem  groupIndex:" + groupIndex + " itemIndex:" + itemIndex);
            if (view == null)
            {
                Tizen.Log.Fatal("NUI", "Gridlistview UpdateItem  view == null");
                return;
            }

            ThumbnailView itemView = view as ThumbnailView;
            itemView.UpdateItem(this, groupIndex, itemIndex, EditMode);
        }
    }    
}