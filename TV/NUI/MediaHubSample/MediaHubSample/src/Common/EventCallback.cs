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
using Tizen.NUI.BaseComponents;
using System;
using System.Collections.Generic;

namespace Tizen.NUI.MediaHub
{
    /// <summary>
    /// Call back for GridView Item press, Item Selected.
    /// </summary>
    public class EventCallBack
    {
        /// <summary>
        /// Call back method for GridView Item press.
        /// </summary>
        /// <param name="view">The grid instance</param>
        public void OnItemPress(GridView view)
        {
            int groupIndex = 0;
            int itemIndex = 0;
            ContentModel item = null;
            if (view != null)
            {
                try
                {
                    view.GetFocusItemIndex(out groupIndex, out itemIndex);
                    item = (ContentModel)view.GetBridge().GetData(itemIndex, groupIndex);
                }
                catch (Exception e)
                {
                    Tizen.Log.Fatal("NUI", "onItemPress error: " + e.Message);
                }

                try
                {
                    if (item == null)
                    {
                        return;
                    }

                    if (item.MediaItemType == ContentItemType.eItemFolder)
                    {

                    }
                    else if (item.MediaItemType == ContentItemType.eItemUpFolder)
                    {
                    }
                    else
                    {
                        SelectItems(item, itemIndex, groupIndex, view);
                    }
                }
                catch (Exception e2)
                {
                    Tizen.Log.Fatal("NUI", "onItemPress exception error: " + e2.Message);
                }

            }

        }
        /// <summary>
        /// Call back method for GridView Item selected
        /// </summary>
        /// <param name="item">The data on the item</param>
        /// <param name="itemIndex">The index of the item</param>
        /// <param name="groupIndex">The index of th grid</param>
        /// <param name="gridView">The grid instance</param>
        public void SelectItems(ContentModel item, int itemIndex, int groupIndex, GridView gridView)
        {
            Tizen.Log.Fatal("NUI", "editModeItemPress itemIndex:" + itemIndex + ", groupIndex:" + groupIndex);
            ImageItem imageItem = (ImageItem)gridView.GetLoadedItemView(groupIndex, itemIndex);
            if (imageItem.StateSelected)
            {
                imageItem.StateSelected = false;
            }
            else
            {
                imageItem.StateSelected = true;
            }
        }


    }


}
