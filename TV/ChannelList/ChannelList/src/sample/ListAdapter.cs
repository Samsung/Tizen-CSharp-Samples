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
using System.Collections.Generic;
using ChannelList;

/// <summary>
/// namespace for channel list sample
/// </summary>
namespace ListSample
{
    /// <summary>
    /// Sample list Adaptor.
    /// A Adapter object acts as a pipeline between an AdapterView and the
    /// underlying data for that view. The Adapter provides access to the data items.
    /// The Adapter is also responsible for making a View for each item in the data set.
    /// This is used for all channel list.
    /// </summary>
    public class SampleListAdapter : ListAdapter
    {
        private Size2D windowSize = Window.Instance.Size; //window size
        private string playProgramIndex; //play program index

        /// <summary>
        /// The constructor with date sets.
        /// </summary>
        /// <param name="objects">the objects</param>
        public SampleListAdapter(List<object> objects)
            : base(objects)
        {
        }

        /// <summary>
        /// Get/Set the playing program index.
        /// </summary>
        public string PlayProgramIndex
        {
            get
            {
                return playProgramIndex;
            }

            set
            {
                playProgramIndex = value;
            }
        }

        /// <summary>
        /// Get a View that displays the data at the specified index in the data set.
        /// </summary>
        /// <param name="index">The index of the all channel list.</param>
        /// <returns>A View corresponding to the all channel list at the specified index.</returns>
        public override View GetItemView(int index)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "GetItemView ... " + index);
            object data = GetData(index);
            ListItemData itemData = data as ListItemData;

            ListItem itemView = new ListItem();
            itemView.ProgramIndex = itemData.ProgramIndex;
            itemView.ProgramText = itemData.Program;
            itemView.ChannelText = itemData.Channel;
            if (itemData.ProgramIndex == playProgramIndex)
            {
                itemView.Play(true);
            }
            else
            {
                itemView.Play(false);
            }

            if (itemData.Favorite == true)
            {
                itemView.ShowIcon(3);
            }

            return itemView;
        }

        /// <summary>
        /// Get view height associated with the specified index in the all channel list.
        /// </summary>
        /// <param name="index">The index of the all channel list.</param>
        /// <returns>Hight of View corresponding to the all channel list at the specified index.</returns>
        public override float GetItemHeight(int index)
        {
            float itemHight = windowSize.Height * 0.117952f;
            Tizen.Log.Fatal("NUI.ChannelList", "GetItemHeight : " + itemHight);

            return itemHight;
        }

        /// <summary>
        /// Update View that displays the data at the specified index in the all channel list when data change.
        /// </summary>
        /// <param name="index">The index of the all channel list.</param>
        /// <param name="view">A View that displays the data at the specified index in the all channel list.</param>
        public override void UpdateItem(int index, View view)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "UpdateItem... ");
            object data = GetData(index);
            ListItemData itemData = data as ListItemData;

            ListItem itemView = view as ListItem;
            if (itemView != null)
            {
                itemView.ProgramIndex = itemData.ProgramIndex;
                itemView.ProgramText = itemData.Program;
                itemView.ChannelText = itemData.Channel;

                if (itemData.ProgramIndex == playProgramIndex)
                {
                    itemView.Play(true);
                }
                else
                {
                    itemView.Play(false);
                }

                if (itemData.Favorite == true)
                {
                    itemView.ShowIcon(3);
                }
            }
        }

        /// <summary>
        /// Update View that displays the data at the specified index in the all channel list when focus change.
        /// </summary>
        /// <param name="index">The index of the all channel list.</param>
        /// <param name="view">A View that displays the data at the specified index in the all channel list.</param>
        /// <param name="flagFocused">True means the item state change to focus, false means the item state change to unfocus.</param>
        public override void FocusChange(int index, View view, bool flagFocused)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "FocusChange. index:" + index);

            object data = GetData(index);
            ListItemData itemData = data as ListItemData;

            ListItem itemView = view as ListItem;
            if (itemView != null)
            {
                itemView.Focus(flagFocused);
            }
        }

        /// <summary>
        /// Unload view at the specified index when item scroll out or deleted.
        /// </summary>
        /// <param name="index">The index of the all channel list.</param>
        /// <param name="view">A View that displays the data at the specified index in the all channel list.</param>
        public override void UnloadItem(int index, View view)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "UnloadItem... ");
            object data = GetData(index);
            ListItemData itemData = data as ListItemData;

            ListItem itemView = view as ListItem;
            if (itemView != null)
            {
                itemView.ProgramIndex = itemData.ProgramIndex;
                itemView.ProgramText = itemData.Program;
                itemView.ChannelText = itemData.Channel;
            }

            view?.Dispose();
        }

        /// <summary>
        /// Unload view according to the specified viewType.
        /// </summary>
        /// <param name="viewType">Item type set by users.</param>
        /// <param name="view">A View that displays the data at the specified type in the all channel list.</param>
        public override void UnloadItemByViewType(int viewType, View view)
        {
            view?.Dispose(); ;
        }
    }

    /// <summary>
    /// Sublist adapter.
    /// A Adapter object acts as a pipeline between an AdapterView and the
    /// underlying data for that view. The Adapter provides access to the data items.
    /// The Adapter is also responsible for making a View for each item in the data set.
    /// This is used for sub-list.
    /// </summary>
    public class SubListAdapter : ListAdapter
    {
        private Size2D windowSize = Window.Instance.Size; //window size
        private int selectedIndex = -1; //selected index
        private bool open = false; //open flag

        /// <summary>
        /// The constructor with data set.
        /// </summary>
        /// <param name="objects">the objects</param>
        public SubListAdapter(List<object> objects)
            : base(objects)
        {
        }

        /// <summary>
        /// Get/Set the selected index.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }

            set
            {
                selectedIndex = value;
            }
        }

        /// <summary>
        /// Get/Set whether the item is opened.
        /// </summary>
        public bool Open
        {
            get
            {
                return open;
            }

            set
            {
                open = value;
            }
        }

        /// <summary>
        /// Get a View that displays the data at the specified index in the sub list.
        /// </summary>
        /// <param name="index">The index of the sub list.</param>
        /// <returns>A View corresponding to the sub list at the specified index.</returns>
        public override View GetItemView(int index)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "GetItemView ... " + index);
            object data = GetData(index);
            SubListData itemData = data as SubListData;

            SubListItem itemView = new SubListItem();
            itemView.Text = itemData.Text;
            if (index == selectedIndex)
            {
                itemView.IconUrl = itemData.IconS;
                itemView.SelectedText(true);
                itemView.IconOpacity(true);
            }
            else
            {
                itemView.IconUrl = itemData.IconN;
                itemView.SelectedText(false);
                if (Open == false)
                {
                    itemView.IconOpacity(false);
                }

            }

            return itemView;
        }

        /// <summary>
        /// Get view height associated with the specified index in the sub list.
        /// </summary>
        /// <param name="index">The index of the sub list.</param>
        /// <returns>Hight of View corresponding to the sub list at the specified index.</returns>
        public override float GetItemHeight(int index)
        {
            float itemHight = windowSize.Height * 0.126851f;
            Tizen.Log.Fatal("NUI.ChannelList", "GetItemHeight : " + itemHight + ", index: " + index);

            return itemHight;
        }

        /// <summary>
        /// Update View that displays the data at the specified index in the sub list when data change.
        /// </summary>
        /// <param name="index">The index of the sub list.</param>
        /// <param name="view">A View that displays the data at the specified index in the sub list.</param>
        public override void UpdateItem(int index, View view)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "UpdateItem..., index: " + index);
            object data = GetData(index);
            SubListData itemData = data as SubListData;

            SubListItem itemView = view as SubListItem;
            if (itemView != null)
            {
                itemView.Text = itemData.Text;
                if (index == selectedIndex)
                {
                    itemView.IconUrl = itemData.IconS;
                    itemView.SelectedText(true);
                    itemView.IconOpacity(true);
                }
                else
                {
                    itemView.IconUrl = itemData.IconN;
                    itemView.SelectedText(false);
                    if (Open == false)
                    {
                        itemView.IconOpacity(false);
                    }
                }
            }
        }

        /// <summary>
        /// Update View that displays the data at the specified index in the sub list when focus change.
        /// </summary>
        /// <param name="index">The index of the sub list.</param>
        /// <param name="view">A View that displays the data at the specified index in the sub list.</param>
        /// <param name="flagFocused">True means the item state change to focus, false means the item state change to unfocus.</param>
        public override void FocusChange(int index, View view, bool flagFocused)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "FocusChange. index:" + index);

            object data = GetData(index);
            SubListItem itemData = data as SubListItem;

            SubListItem itemView = view as SubListItem;
            if (itemView != null)
            {
                itemView.Focus(flagFocused);
            }
        }

        /// <summary>
        /// Unload view at the specified index when item scroll out or deleted.
        /// </summary>
        /// <param name="index">The index of the sub list.</param>
        /// <param name="view">A View that displays the data at the specified index in the sub list.</param>
        public override void UnloadItem(int index, View view)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "UnloadItem... ");
            object data = GetData(index);
            SubListData itemData = data as SubListData;

            SubListItem itemView = view as SubListItem;
            if (itemView != null)
            {
                itemView.Text = itemData.Text;
                itemView.IconUrl = itemData.IconN;
            }

            view?.Dispose();
        }

        /// <summary>
        /// Unload view according to the specified viewType.
        /// </summary>
        /// <param name="viewType">Item type set by users.</param>
        /// <param name="view">A View that displays the data at the specified type in the sub list.</param>
        public override void UnloadItemByViewType(int viewType, View view)
        {
            view?.Dispose(); ;
        }
    }

    /// <summary>
    /// Favorite list adapter.
    /// A Adapter object acts as a pipeline between an AdapterView and the
    /// underlying data for that view. The Adapter provides access to the data items.
    /// The Adapter is also responsible for making a View for each item in the data set.
    /// This is used for favorite list.
    /// </summary>
    public class SelectListAdapter : ListAdapter
    {
        private Size2D windowSize = Window.Instance.Size; //window size

        /// <summary>
        /// The constructor with parameter.
        /// </summary>
        /// <param name="objects">the objects</param>
        public SelectListAdapter(List<object> objects)
            : base(objects)
        {
        }

        /// <summary>
        /// Get a View that displays the data at the specified index in the favorite list.
        /// </summary>
        /// <param name="index">The index of the favorite list.</param>
        /// <returns>A View corresponding to the favorite list at the specified index.</returns>
        public override View GetItemView(int index)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "GetItemView ... " + index);
            object data = GetData(index);
            FavoriteListData itemData = data as FavoriteListData;

            SelectListItem itemView = new SelectListItem();
            itemView.FavoriteText = itemData.Name;
            itemView.FavoriteNum = itemData.FavoriteNum;

            return itemView;
        }

        /// <summary>
        /// Get view height associated with the specified index in the favorite list.
        /// </summary>
        /// <param name="index">The index of the favorite list.</param>
        /// <returns>Hight of View corresponding to the favorite list at the specified index.</returns>
        public override float GetItemHeight(int index)
        {
            float itemHight = windowSize.Height * 0.129629f;
            Tizen.Log.Fatal("NUI.ChannelList", "GetItemHeight : " + itemHight);

            return itemHight;
        }

        /// <summary>
        /// Update View that displays the data at the specified index in the favorite list when data change.
        /// </summary>
        /// <param name="index">The index of the favorite list.</param>
        /// <param name="view">A View that displays the data at the specified index in the favorite list.</param>
        public override void UpdateItem(int index, View view)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "UpdateItem... ");
            object data = GetData(index);
            FavoriteListData itemData = data as FavoriteListData;

            SelectListItem itemView = view as SelectListItem;
            if (itemView != null)
            {
                itemView.FavoriteText = itemData.Name;
                itemView.FavoriteNum = itemData.FavoriteNum;
            }
        }

        /// <summary>
        /// Update View that displays the data at the specified index in the favorite list when focus change.
        /// </summary>
        /// <param name="index">The index of the favorite list.</param>
        /// <param name="view">A View that displays the data at the specified index in the favorite list.</param>
        /// <param name="flagFocused">True means the item state change to focus, false means the item state change to unfocus.</param>
        public override void FocusChange(int index, View view, bool flagFocused)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "FocusChange. index:" + index);

            object data = GetData(index);
            FavoriteListData itemData = data as FavoriteListData;

            SelectListItem itemView = view as SelectListItem;
            if (itemView != null)
            {
                itemView.Focus(flagFocused);
            }
        }

        /// <summary>
        /// Unload view at the specified index when item scroll out or deleted.
        /// </summary>
        /// <param name="index">The index of the favorite list.</param>
        /// <param name="view">A View that displays the data at the specified index in the favorite list.</param>
        public override void UnloadItem(int index, View view)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "UnloadItem... ");
            object data = GetData(index);
            FavoriteListData itemData = data as FavoriteListData;

            SelectListItem itemView = view as SelectListItem;
            if (itemView != null)
            {
                itemView.FavoriteText = itemData.Name;
                itemView.FavoriteNum = itemData.FavoriteNum;
            }

            view?.Dispose();
        }

        /// <summary>
        /// Unload view according to the specified viewType.
        /// </summary>
        /// <param name="viewType">Item type set by users.</param>
        /// <param name="view">A View that displays the data at the specified type in the favorite list.</param>
        public override void UnloadItemByViewType(int viewType, View view)
        {
            view?.Dispose(); ;
        }
    }

    /// <summary>
    /// Genre list adapter.
    /// A Adapter object acts as a pipeline between an AdapterView and the
    /// underlying data for that view. The Adapter provides access to the data items.
    /// The Adapter is also responsible for making a View for each item in the data set.
    /// This is used for genre list.
    /// </summary>
    public class GenreListAdapter : ListAdapter
    {
        private Size2D windowSize = Window.Instance.Size; //window size

        /// <summary>
        /// The constructor with data sets.
        /// </summary>
        /// <param name="objects">the objects</param>
        public GenreListAdapter(List<object> objects)
            : base(objects)
        {
        }

        /// <summary>
        /// Get a View that displays the data at the specified index in the genre list.
        /// </summary>
        /// <param name="index">The index of the genre list.</param>
        /// <returns>A View corresponding to the genre list at the specified index.</returns>
        public override View GetItemView(int index)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "GetItemView ... " + index);
            object data = GetData(index);
            GenreListData itemData = data as GenreListData;

            GenreListItem itemView = new GenreListItem();
            itemView.Text = itemData.Name;

            return itemView;
        }

        /// <summary>
        /// Get view height associated with the specified index in the genre list.
        /// </summary>
        /// <param name="index">The index of the genre list.</param>
        /// <returns>Hight of View corresponding to the genre list at the specified index.</returns>
        public override float GetItemHeight(int index)
        {
            float itemHight = windowSize.Height * 0.092592f;
            Tizen.Log.Fatal("NUI.ChannelList", "GetItemHeight : " + itemHight);

            return itemHight;
        }

        /// <summary>
        /// Update View that displays the data at the specified index in the genre list when data change.
        /// </summary>
        /// <param name="index">The index of the genre list.</param>
        /// <param name="view">A View that displays the data at the specified index in the genre list.</param>
        public override void UpdateItem(int index, View view)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "UpdateItem...index: " + index);
            object data = GetData(index);
            GenreListData itemData = data as GenreListData;

            GenreListItem itemView = view as GenreListItem;
            if (itemView != null)
            {
                itemView.Text = itemData.Name;
            }
        }

        /// <summary>
        /// Update View that displays the data at the specified index in the genre list when focus change.
        /// </summary>
        /// <param name="index">The index of the genre list.</param>
        /// <param name="view">A View that displays the data at the specified index in the genre list.</param>
        /// <param name="flagFocused">True means the item state change to focus, false means the item state change to unfocus.</param>
        public override void FocusChange(int index, View view, bool flagFocused)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "FocusChange... index:" + index + ", flagFocused: " + flagFocused);

            object data = GetData(index);
            GenreListData itemData = data as GenreListData;

            GenreListItem itemView = view as GenreListItem;
            if (itemView != null)
            {
                itemView.Focus(flagFocused);
            }
        }

        /// <summary>
        /// Unload view at the specified index when item scroll out or deleted.
        /// </summary>
        /// <param name="index">The index of the genre list.</param>
        /// <param name="view">A View that displays the data at the specified index in the genre list.</param>
        public override void UnloadItem(int index, View view)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "UnloadItem...index: " + index);
            object data = GetData(index);
            GenreListData itemData = data as GenreListData;

            GenreListItem itemView = view as GenreListItem;
            if (itemView != null)
            {
                itemView.Text = itemData.Name;
            }

            view?.Dispose();
        }

        /// <summary>
        /// Unload view according to the specified viewType.
        /// </summary>
        /// <param name="viewType">Item type set by users.</param>
        /// <param name="view">A View that displays the data at the specified type in the genre list.</param>
        public override void UnloadItemByViewType(int viewType, View view)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "UnloadItemByViewType...viewType: " + viewType);
            view?.Dispose();
        }
    }
}