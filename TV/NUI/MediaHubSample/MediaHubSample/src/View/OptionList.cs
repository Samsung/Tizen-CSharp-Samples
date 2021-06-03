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
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.MediaHub
{
    /// <summary>
    /// OptionList is the Class that realized simple menu.    
    /// </summary>
    /// <example>
    /// <code>
    /// optList = new OptionList();
    /// optList.Size = new Size(650, 450, 0);
    /// optList.ItemHeight = 80;
    /// Window.Instance.GetDefaultLayer().Add(optList);
    /// for (int i = 0; i < 5; i++)
    /// {
    ///    optList.AddData("item0" + i);
    /// }
    /// optList.OptionListEvent += OnOptionListEvent;
    /// optList.FocusItemIndex = 2;
    /// 
    /// public void OnOptionListEvent(object o, ListView.ListEventArgs e)
    /// {
    ///     switch (e.EventType)
    ///     {
    ///         case ListView.ListEventType.FocusChange:
    ///             break;
    ///        ...
    ///     }
    /// }
    /// </code>
    /// </example>
    public class OptionList : View
    {
        /// <summary>
        /// OptionList item states
        /// </summary>
        public enum ItemStates
        {
            /// <summary>
            /// State normal
            /// </summary>
            Normal = 0,           
            /// <summary>
            /// State focused
            /// </summary>
            Focused,                 
            /// <summary>
            /// State selected
            /// </summary>
            Selected,               
            /// <summary>
            /// State disabled
            /// </summary>
            Disabled,                 
            /// <summary>
            /// State disabled and focused
            /// </summary>
            DisableFocused,
            /// <summary>
            /// State for setting all state at once
            /// </summary>
            StateAll            
        }

        /// <summary>
        /// Construct OptionList
        /// </summary>
        public OptionList()
        {
            Initialize();
        }

        /// <summary>
        /// Set Item height, must be set before AddData
        /// </summary>
        public int ItemHeight
        {
            get;
            set;
        }


        /// <summary>
        /// Dispose of OptionList.
        /// </summary>
        /// <param name="type">the dispose type</param>
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
                this.Remove(list);
                list.Dispose();
                list = null;

                if (bgImage != null)
                {
                    this.Remove(bgImage);
                    bgImage.Dispose();
                    bgImage = null;
                }

                if (coverImage != null)
                {
                    this.Remove(coverImage);
                    coverImage.Dispose();
                    coverImage = null;
                }
            }

            //Release your own unmanaged resources here.
            //You should not access any managed member here except static instance.
            //because the execution order of Finalizes is non-deterministic.
            //Unreference this from if a static instance refer to this. 

            //You must call base.Dispose(type) just before exit.
            base.Dispose(type);
        }

        /// <summary>
        /// Selected index.  
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException"> Thrown when SelectItemIndex value out of range[0, NumOfItem-1] </exception>
        /// <example>
        /// <code>
        /// try
        /// {
        ///     optionList.SelectItemIndex = 6;
        /// }
        /// catch(ArgumentException)
        /// {
        ///     Log.Error(LogTag, "SelectItemIndex value is out of range. " +e.Message);
        /// }
        /// </code>
        /// </example>
        public int SelectItemIndex
        {
            get
            {
                return selectedIndex;
            }

            set
            {
                if (selectedIndex == value)
                {
                    return;
                }

                if (list != null)
                {
                    if (value < 0 || value >= list.NumOfItem)
                    {
                        Tizen.Log.Fatal("NUI", "Select item index is out of range! Items count = " + list.NumOfItem + ", index = " + value);
                        throw new IndexOutOfRangeException("Select item index is out of range! Items count = " + list.NumOfItem + ", index = " + value);
                    }

                    OptionListItemData itemData = (bridge as OptionListBridge).GetData(value) as OptionListItemData;
                    if (itemData.IsDisabled == true)
                    {
                        return;
                    }

                    if (selectedIndex >= 0)
                    {
                        OptionListItemData data = null;
                        data = (bridge as OptionListBridge).GetData(selectedIndex) as OptionListItemData;
                        data.IsSelected = false;
                        list.UpdateItem(selectedIndex);
                    }

                    itemData = (bridge as OptionListBridge).GetData(value) as OptionListItemData;
                    itemData.IsSelected = true;
                    list.UpdateItem(value);

                    selectedIndex = value;
                }
            }
        }

        /// <summary>
        /// Get item number.  
        /// </summary>
        public int NumOfItem
        {
            get
            {
                return list.NumOfItem;
            }
        }

        /// <summary>
        /// Get/Set focus item index of OptionList.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException"> Thrown when FocusItemIndex value out of range[0, NumOfItem-1] </exception>
        /// <example>
        /// <code>
        /// try
        /// {
        ///     optionList.FocusItemIndex = 6;
        /// }
        /// catch(ArgumentException)
        /// {
        ///     Log.Error(LogTag, "FocusItemIndex value is out of range. " +e.Message);
        /// }
        /// </code>
        /// </example>
        public int FocusItemIndex
        {
            get
            {
                return list.FocusItemIndex;
            }

            set
            {
                if (value < 0 || value >= list.NumOfItem)
                {
                    Tizen.Log.Fatal("NUI", "Focus item index is out of range! Items count = " + list.NumOfItem + ", index = " + value);
                    throw new IndexOutOfRangeException("Focus item index is out of range! Items count = " + list.NumOfItem + ", index = " + value);
                }

                list.FocusItemIndex = value;
            }
        }

        /// <summary>
        /// Add/Remove listener to/from OptionList.
        /// </summary>
        public event EventHandler<ListView.ListEventArgs> OptionListEvent
        {
            add
            {
                list.ListEvent += value;
            }

            remove
            {
                list.ListEvent -= value;
            }
        }

        /// <summary>
        /// Move focus according to parameter direction
        /// </summary>
        /// <param name="direction">Focus move direction</param>
        public void Move(MoveDirection direction)
        {
            list.MoveFocus(direction);
        }

        /// <summary>
        /// Add OptionList item text string, only effect for UI style.
        /// </summary>
        /// <param name="textString">OptionList item text string</param>
        public void AddData(string textString)
        {
            OptionListItemData itemData = new OptionListItemData();
            itemData.ItemHeight = ItemHeight;
            itemData.TextString = textString;
            (bridge as OptionListBridge).Add(itemData);
        }

        /// <summary>
        /// Removes the specified data from the List.
        /// </summary>
        /// <param name="fromIndex">The from index of data to remove from the list.</param>
        /// <param name="removeNum">The remove number of data to remove from the list.</param>
        /// <exception cref="System.IndexOutOfRangeException"> Thrown when RemoveData index out of range[0, NumOfItem-1] </exception>
        /// <example>
        /// <code>
        /// try
        /// {
        ///     optionList.RemoveData(5, 3);
        /// }
        /// catch(ArgumentException)
        /// {
        ///     Log.Error(LogTag, "RemoveData index out of range. " +e.Message);
        /// }
        /// </code>
        /// </example>
        public void RemoveData(int fromIndex, int removeNum = 1)
        {
            if (fromIndex < 0 || fromIndex + removeNum > list.NumOfItem)
            {
                Tizen.Log.Fatal("NUI", "Remove data index is out of range! Valid range is 0 to " + (list.NumOfItem - 1) + ", Remove range is " + fromIndex + " to " + (fromIndex + removeNum - 1));
                throw new IndexOutOfRangeException("Remove data index is out of range! Valid range is 0 to " + (list.NumOfItem - 1) + ", Remove range is " + fromIndex + " to " + (fromIndex + removeNum - 1));
            }

            (bridge as OptionListBridge).Remove(fromIndex, removeNum);
        }

        /// <summary>
        /// Enable or disable OptionList item.
        /// </summary>
        /// <param name="index">OptionList item index</param>
        /// <param name="disable">true is disable the item, false is enable the item</param>
        /// <exception cref="System.IndexOutOfRangeException"> Thrown when dim item index out of range[0, NumOfItem-1] </exception>
        /// <example>
        /// <code>
        /// try
        /// {
        ///     optionList.RemoveData(5, 3);
        /// }
        /// catch(ArgumentException)
        /// {
        ///     Log.Error(LogTag, "Disable item index out of range. " +e.Message);
        /// }
        /// </code>
        /// </example>
        public void DisableItem(int index, bool disable = true)
        {
            if (index < 0 || index >= list.NumOfItem)
            {
                Tizen.Log.Fatal("NUI", "Disable item index is out of range! Items count = " + list.NumOfItem + ", index = " + index);
                throw new IndexOutOfRangeException("Disable item index is out of range! Items count = " + list.NumOfItem + ", index = " + index);
            }

            OptionListItemData data = (bridge as OptionListBridge).GetData(index) as OptionListItemData;
            data.IsDisabled = disable;
            list.UpdateItem(index);
        }

        /// <summary>
        /// Get text string.
        /// </summary>
        /// <param name="index">OptionList item index</param>
        /// <exception cref="System.IndexOutOfRangeException"> Thrown when item index out of range[0, NumOfItem-1] </exception>
        /// <example>
        /// <code>
        /// try
        /// {
        ///     optionList.TextString(5);
        /// }
        /// catch(ArgumentException)
        /// {
        ///     Log.Error(LogTag, "Item index out of range. " +e.Message);
        /// }
        /// </code>
        /// </example>
        /// <returns>the text string of the specific index</returns>
        public string TextString(int index)
        {
            if (index < 0 || index >= list.NumOfItem)
            {
                Tizen.Log.Fatal("NUI", "Item index is out of range! Items count = " + list.NumOfItem + ", index = " + index);
                throw new IndexOutOfRangeException("Item index is out of range! Items count = " + list.NumOfItem + ", index = " + index);
            }

            OptionListItemData data = (bridge as OptionListBridge).GetData(index) as OptionListItemData;
            return data.TextString;
        }

        /// <summary>
        /// Update.
        /// </summary>
        private void OnUpdate()
        {
            bgImage = new ImageView();
            bgImage.Name = "BackgroundImage";
            bgImage.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            bgImage.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            bgImage.WidthResizePolicy = ResizePolicyType.SizeFixedOffsetFromParent;
            bgImage.HeightResizePolicy = ResizePolicyType.SizeFixedOffsetFromParent;
            bgImage.SizeModeFactor = new Vector3(8, 12, 0);
            bgImage.Position = new Position(-4, -6, 0);
            bgImage.ResourceUrl = CommonResource.GetLocalReosurceURL() + "component/c_listdropdown/c_list_dropdown_bg.9.png";
            this.Add(bgImage);

            coverImage = new ImageView();
            coverImage.Name = "CoverImage";
            coverImage.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            coverImage.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            coverImage.WidthResizePolicy = ResizePolicyType.FillToParent;
            coverImage.HeightResizePolicy = ResizePolicyType.Fixed;
            coverImage.Size2D = new Size2D(0, 173);
            coverImage.ResourceUrl = CommonResource.GetLocalReosurceURL() + "component/c_listdropdown/c_list_dropdown_bg_cover.9.png";
            this.Add(coverImage);

            Vector4 listMargin = new Vector4(11, 4, 11, 4);
            // Tizen.Log.Fatal("NUI", " ...listMargin:" + listMargin[0] + ", " + listMargin[1] + ", " + listMargin[2] + ", " + listMargin[3]);
            list.Position = new Position(listMargin[0], listMargin[1], 0);
            list.Size2D = new Size2D((int)(this.SizeWidth - (listMargin[0] + listMargin[2])), (int)(this.SizeHeight - (listMargin[1] + listMargin[3])));
            list.Margin = new Vector4(list.SizeWidth * 0.04f + 7, 80 * 0.04f + 12, list.SizeWidth * 0.04f + 7, 80 * 0.04f + 12);
            
            list.Update();

            if (coverImage != null)
            {
                coverImage.LowerToBottom();
            }

            if (bgImage != null)
            {
                bgImage.LowerToBottom();
            }
        }

        /// <summary>
        /// Called when the control gain key input focus.
        /// </summary>
        /// <param name="sender">the object</param>
        /// <param name="e">the args of the event</param>
        public void OnFocusGained(object sender, EventArgs e)
        {
            focusedView = list;
        }

        /// <summary>
        /// Called when the control loses key input focus.
        /// </summary>
        /// <param name="sender">the object</param>
        /// <param name="e">the args of the event</param>
        public void OnFocusLost(object sender, EventArgs e)
        {
            focusedView = null;
        }

        /// <summary>
        /// Initialize
        /// </summary>
        private void Initialize()
        {
            Relayout += OnRelayout;
            KeyEvent += ControlKeyEvent;
            FocusGained += OnFocusGained;
            FocusLost += OnFocusLost;

            //Initialize ListView
            list = new ListView();
            list.Name = "ListView";
            list.PreloadFrontItemSize = 0;
            list.PreloadBackItemSize = 0;
            list.FocusInScaleFactor = 1.08f;
            AnimationAttributes scrollAnimationAttrs = new AnimationAttributes();
            scrollAnimationAttrs.Duration = 100;
            scrollAnimationAttrs.BezierPoint1 = new Vector2(0.3f, 0);
            scrollAnimationAttrs.BezierPoint2 = new Vector2(0.15f, 1);
            list.ScrollAnimationAttrs = scrollAnimationAttrs;
            AnimationAttributes focusInScaleAnimationAttrs = new AnimationAttributes();
            focusInScaleAnimationAttrs.Duration = 600;
            focusInScaleAnimationAttrs.BezierPoint1 = new Vector2(0.21f, 2);
            focusInScaleAnimationAttrs.BezierPoint2 = new Vector2(0.14f, 1);
            list.FocusInScaleAnimationAttrs = focusInScaleAnimationAttrs;
            AnimationAttributes focusOutScaleAnimationAttrs = new AnimationAttributes();
            focusOutScaleAnimationAttrs.Duration = 850;
            focusOutScaleAnimationAttrs.BezierPoint1 = new Vector2(0.19f, 1);
            focusOutScaleAnimationAttrs.BezierPoint2 = new Vector2(0.22f, 1);
            list.FocusOutScaleAnimationAttrs = focusOutScaleAnimationAttrs;

            list.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            list.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            Add(list);

            list.Focusable = true;

            bridge = new OptionListBridge(dataList);
            list.SetBridge(bridge);
            
        }

        /// <summary>
        /// Update.
        /// </summary>
        public void Update()
        {
            OnUpdate();
        }

        /// <summary>
        /// The callback of OnRelayoutEvent event.
        /// </summary>
        /// <param name="source">the object</param>
        /// <param name="e">the args of the event</param>
        private void OnRelayout(object source, EventArgs e)
        {
            OnUpdate();
        }

        /// <summary>
        /// The callback of KeyEvent.
        /// </summary>
        /// <param name="source">the object</param>
        /// <param name="e">the args of the event</param>
        /// <returns>If the key is consumed return true, else return false</returns>
        private bool ControlKeyEvent(object source, KeyEventArgs e)
        {
            return OnKey(e.Key);
        }

        /// <summary>
        /// Onkey.
        /// </summary>
        /// <param name="key">the key value</param>
        /// <returns>return whether the the key has been consumed</returns>
        public bool OnKey(Key key)
        {
            return false;
        }


        //members
        private ImageView bgImage = null;
        private ImageView coverImage = null;
        private ListView list = null;
        private List<object> dataList = new List<object>();
        private ListBridge bridge = null;
        private int selectedIndex = -1;

        private View focusedView = null;
    }

    /// <summary>
    /// option list data
    /// </summary>
    internal class OptionListItemData
    {
        /// <summary>
        /// Get/Set IsSelected property.
        /// </summary>
        public bool IsSelected
        {
            get;
            set;
        }

        /// <summary>
        /// Get/Set IsFocused property.
        /// </summary>
        public bool IsFocused
        {
            get;
            set;
        }

        /// <summary>
        /// Get/Set IsDisabled property.
        /// </summary>
        public bool IsDisabled
        {
            get;
            set;
        }

        /// <summary>
        /// Get/Set ItemHeight property.
        /// </summary>
        public int ItemHeight
        {
            get;
            set;
        }

        /// <summary>
        /// Get/Set TextString property.
        /// </summary>
        public string TextString
        {
            get;
            set;
        }
    }


    /// <summary>
    /// OptionListAdapter is the Class that adapt optionlist item.    
    /// </summary>
    internal class OptionListBridge : ListBridge
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="objects">The data resource</param>
        public OptionListBridge(List<object> objects)
            : base(objects)
        {
        }

        /// <summary>
        /// Get the item according to the index.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        /// <returns>return the item</returns>
        public override View GetItemView(int index)
        {
            object data = GetData(index);
            OptionListItemData itemData = data as OptionListItemData;

            TextItem textItem = new TextItem();
            textItem.Name = "OptionListItem";
            textItem.MainText = itemData.TextString;
            textItem.StateEnabled = !itemData.IsDisabled;
            textItem.StateSelected = itemData.IsSelected;
            return textItem;
        }

        /// <summary>
        /// Get the item's height
        /// </summary>
        /// <param name="index">The index of the item</param>
        /// <returns>The height of the item</returns>
        public override int GetItemHeight(int index)
        {
            object data = GetData(index);
            OptionListItemData itemData = data as OptionListItemData;
            return itemData.ItemHeight;
        }

        /// <summary>
        /// Update the item
        /// </summary>
        /// <param name="index">The index of the item</param>
        /// <param name="view">the view</param>
        public override void UpdateItem(int index, View view)
        {
             TextItem textItem = view as TextItem;
            OptionListItemData itemData = GetData(index) as OptionListItemData;
            if (textItem != null)
            {
                if (textItem.StateEnabled != !itemData.IsDisabled)
                {
                    textItem.StateEnabled = !itemData.IsDisabled;
                }

                if (textItem.StateFocused != itemData.IsFocused)
                {
                    textItem.StateFocused = itemData.IsFocused;
                }

                if (textItem.StateSelected != itemData.IsSelected)
                {
                    textItem.StateSelected = itemData.IsSelected;
                }

                textItem.MainText = itemData.TextString;
            }
        }

        /// <summary>
        /// The method will be called when the focus changed in the list
        /// </summary>
        /// <param name="index">The index of the item</param>
        /// <param name="view">The item view</param>
        /// <param name="flagFocused">The flag show the item is getting focus or losing focus</param>
        public override void FocusChange(int index, View view, bool flagFocused)
        {
            OptionListItemData itemData = GetData(index) as OptionListItemData;
            itemData.IsFocused = flagFocused;

            TextItem textItem = view as TextItem;
            textItem.StateFocused = flagFocused;
        }

        /// <summary>
        /// Unload item in the list
        /// </summary>
        /// <param name="index">The index of the item</param>
        /// <param name="view">The item view</param>
        public override void UnloadItem(int index, View view)
        {
            if (view != null)
            {
                view.Dispose();
            }

            return;
        }

        /// <summary>
        /// Unload the item according to the type.
        /// </summary>
        /// <param name="viewType">The type </param>
        /// <param name="view">the view</param>
        public override void UnloadItemByViewType(int viewType, View view)
        {
            if (view != null)
            {
                view.Dispose();
            }

            return;
        }
    }
}
