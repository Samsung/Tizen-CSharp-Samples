/** Copyright (c) 2017 Samsung Electronics Co., Ltd.
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
using System.Text;
using System.Diagnostics;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI
{
    /// <summary>
    /// A view that shows items in two-dimension grid, which can scroll horizontally or vertically.
    /// The items in the grid come from the GirdBridge associated with this view.
    /// </summary>
    public class GridView : View
    {
        /// <summary>
        /// GridView event types
        /// </summary>
        public enum GridEventType
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
        }

        /// <summary>
        /// GridView orientation types
        /// </summary>
        public enum GridType
        {
            /// <summary>
            /// Items laid horizontal
            /// </summary>
            Horizontal = 0,
            /// <summary>
            /// Items laid vertical
            /// </summary>
            Vertical
        }

        /// <summary>
        /// GridView event args at event call back, user can get information
        /// </summary>
        public class GridEventArgs : EventArgs
        {
            /// <summary>
            /// GridView event type
            /// </summary>
            public GridEventType EventType
            {
                get;
                set;
            }

            /// <summary>
            /// GridView event data
            /// </summary>
            public int[] param = new int[4];
        }

        /// <summary>
        /// Construct GridView.
        /// </summary>
        public GridView()
        {
            Initialize();

            // register event handler for relayout
            Relayout += OnRelayoutUpdate;

            FocusGained += ControlFocusGained;
            FocusLost += ControlFocusLost;
        }

        /// <summary>
        /// Get/Set list type.
        /// </summary>
        public GridType Type
        {
            get { return gridType; }
            set { gridType = value; }
        }

        /// <summary>
        /// Get/Set list spaces around items.
        /// </summary>
        public new Vector4 Margin
        {
            get { return margin; }
            set { margin = value; }
        }

        /// <summary>
        /// Get/Set space between columns
        /// </summary>
        public int ColumnSpace
        {
            get { return columnSpace; }
            set { columnSpace = value; }
        }

        /// <summary>
        /// Get/Set space between rows
        /// </summary>
        public int RowSpace
        {
            get { return rowSpace; }
            set { rowSpace = value; }
        }

        /// <summary>
        /// Get/Set space between groups
        /// </summary>
        public int GroupSpace
        {
            get { return groupSpace; }
            set { groupSpace = value; }
        }

        /// <summary>
        /// Get/Set space for title
        /// </summary>
        public int TitleSpace
        {
            get { return titleSpace; }
            set { titleSpace = value; }
        }

        /// <summary>
        /// Get/Set enable flag of whether support straight path focus move.
        /// </summary>
        public bool StateStraightPathEnable
        {
            get;
            set;
        }
        /// <summary>
        /// Get/Set enable flag of right circulation of GridView.
        /// </summary>
        public bool StateRightCircularEnable
        {
            get;
            set;
        }

        /// <summary>
        /// Get/Set enable flag of left circulation of GridView.
        /// </summary>
        public bool StateLeftCircularEnable
        {
            get;
            set;
        }

        /// <summary>
        /// Get/Set focus move animation duration of GridView.
        /// </summary>
        public int FocusMoveDuration
        {
            get
            {
                return focusMoveAniDuration;
            }

            set
            {
                focusMoveAniDuration = value;
                //focusMoveAni.Duration = value;
                //scaleAni.Duration = value;
            }
        }

        /// <summary>
        /// Get/Set scroll bar fade out time
        /// </summary>
        public int ScrollBarFadeOutDuration
        {
            get;
            set;
        }


        /// <summary>
        /// Preload item buffer size at front
        /// </summary>
        public int PreloadFrontColumnSize
        {
            get { return preloadFrontColumnSize; }
            set { preloadFrontColumnSize = value ; }
        }

        /// <summary>
        /// Preload item buffer size at back
        /// </summary>
        public int PreloadBackColumnSize
        {
            get { return preloadBackColumnSize; }
            set { preloadBackColumnSize = value; }
        }

        /// <summary>
        /// Focus in scale factor
        /// </summary>
        public float FocusInScaleFactor
        {
            get { return focusInScaleFactor; }
            set { focusInScaleFactor = value; }
        }

        /// <summary>
        /// Focus in scale animation duration
        /// </summary>
        public AnimationAttributes FocusInScaleAnimationAttrs
        {
            get { return focusInScaleAnimationAttrs; }
            set { focusInScaleAnimationAttrs = value; }
        }

        /// <summary>
        /// Focus out scale animation duration
        /// </summary>
        public AnimationAttributes FocusOutScaleAnimationAttrs
        {
            get { return focusOutScaleAnimationAttrs; }
            set { focusOutScaleAnimationAttrs = value; }
        }
        /// <summary>
        /// Sets the data behind this GridView.
        /// </summary>
        /// <param name="bridge">The GridBridge which is responsible for maintaining the data
        /// backing this grid and for producing a view to represent an item in the data set.</param>
        public void SetBridge(GridBridge bridge)
        {
            Tizen.Log.Fatal("NUI", string.Format("[GridView]Set bridge for GridView"));
            Debug.Assert(bridge != null);

            if (gridBridge != null)
            {
                return;
            }

            gridBridge = bridge;

            gridBridge.DataChangeEvent += OnDataChange;

            for (int i = 0; i < groupList.Count; i++)
            {
                View groupTitle = gridBridge.GetGroupTitleView(i);
                if (groupTitle != null)
                {
                    groupList[i].title = groupTitle;

                    if (GridType.Horizontal == gridType)
                    {
                        groupTitle.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
                        groupTitle.PivotPoint = Tizen.NUI.PivotPoint.BottomLeft;
                        groupTitle.PositionUsesPivotPoint = true;
                        groupTitle.PositionX = groupList[i].startPos;
                        groupTitle.PositionY = -titleSpace;
                        Tizen.Log.Fatal("NUI", "titleView.PositionX: " + groupTitle.PositionX + ", titleView.PositionY: " + groupTitle.PositionY);
                    }
                    else
                    {
                        groupTitle.PositionX = 0;
                        groupTitle.PositionY = groupList[i].startPos - titleSpace;
                    }

                    itemGroup.Add(groupTitle);
                }
            }

            SetViewTypeCount(gridBridge.GetItemTypeCount());


            if (groupList.Count > 0)
            {
                Load();
                if (HasFocus() == true && curInMemoryItemSet.Count != 0)
                {
                    SetFocus(focusGroupIndex, focusItemIndex);
                }
            }
        }
        /// <summary>
        /// Add groups to GridView.
        /// </summary>
        /// <param name="groupCount">The count of groups to add.</param>
        public void AddGroup(int groupCount)
        {
            Tizen.Log.Fatal("NUI", string.Format("[GridView]Add Group Number{0}", groupCount));
            int prevCount = groupList.Count;
            for (int i = prevCount; i < groupCount + prevCount; i++)
            {
                Group newGroup = new Group();
                newGroup.index = i;
                newGroup.columnList = new List<Column>();
                newGroup.itemList = new List<GridItem>();
                newGroup.itemIndexToCell = new Dictionary<int,Cell>();

                if (GridType.Horizontal == gridType)
                {
                    newGroup.startPos = newGroup.endPos = (i == 0 ? 0 : groupSpace + groupList.ElementAt(i - 1).endPos);
                }
                else
                {
                    newGroup.startPos = newGroup.endPos = (i == 0 ? 0 : groupSpace + groupList.ElementAt(i - 1).endPos + titleSpace);
                }

                if (gridBridge != null)
                {
                    View groupTitle = gridBridge.GetGroupTitleView(i);
                    if (groupTitle != null)
                    {
                        newGroup.title = groupTitle;
                        if (GridType.Horizontal == gridType)
                        {
                            groupTitle.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
                            groupTitle.PivotPoint = Tizen.NUI.PivotPoint.BottomLeft;
                            groupTitle.PositionUsesPivotPoint = true;
                            groupTitle.PositionX = newGroup.startPos;
                            groupTitle.PositionY = -titleSpace;
                            Tizen.Log.Fatal("NUI", "titleView.PositionX: " + groupTitle.PositionX + ", titleView.PositionY: " + groupTitle.PositionY);
                        }
                        else
                        {
                            groupTitle.PositionX = 0;
                            groupTitle.PositionY = newGroup.startPos - titleSpace;
                        }

                        itemGroup.Add(groupTitle);
                    }
                }

                groupList.Add(newGroup);
           }
        }

        internal void ResetGroup(int groupCount)
        {
            Tizen.Log.Fatal("NUI", "...." + groupCount);
            if (groupList.Count < groupCount)
            {
                AddGroup(groupCount - groupList.Count);
            }
            else if (groupList.Count > groupCount)
            {
                int removeCount = groupList.Count - groupCount;
                for (int i = groupCount; i < groupList.Count; i++)
                {
                    if (groupList.ElementAt(i).title != null)
                    {
                        itemGroup.Remove(groupList.ElementAt(i).title);
                        gridBridge.RemoveGroupTitleView(i, groupList.ElementAt(i).title);
                    }

                    Clear(i);
                }

                groupList.RemoveRange(groupCount, removeCount);
            }
        }

        // Add column to GridView.
        internal void AddColumn(int columnWidth, int groupIndex = 0)
        {
            if (groupList.Count == 0)
            {
                Group newGroup = new Group();
                newGroup.index = 0;
                newGroup.columnList = new List<Column>();
                newGroup.itemList = new List<GridItem>();
                newGroup.itemIndexToCell = new Dictionary<int, Cell>();
                newGroup.startPos = newGroup.endPos =  0;
                groupList.Add(newGroup);
            }

            Tizen.Log.Fatal("NUI", string.Format("[GridView]Add column columnWidth {0} to group {1}",columnWidth, groupIndex));

            if (!IsGroupIndexValid(groupIndex))
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]Invalid group index"));
                return;
            }

            Group curGroup = groupList.ElementAt(groupIndex);

            Column newColumn = new Column();
            newColumn.groupIndex = groupIndex;
            newColumn.index = curGroup.columnList.Count;
            newColumn.space = columnWidth;

            if (newColumn.index == 0)
            {
                newColumn.startPos = curGroup.startPos;
            }
            else
            {
                newColumn.startPos = curGroup.columnList.ElementAt(newColumn.index - 1).endPos + columnSpace;
            }

            newColumn.endPos = newColumn.startPos + columnWidth;

            if (newColumn.cellList == null)
            {
                newColumn.cellList = new List<Cell>();
            }

            curGroup.columnList.Add(newColumn);

            float posOffset = (0 == newColumn.index) ? columnWidth : columnWidth + columnSpace;
            curGroup.endPos += posOffset;

            for (int i = groupIndex + 1; i < (int)groupList.Count; i++)
            {
                if (GridType.Horizontal == gridType && groupList.ElementAt(i).title != null)
                {
                    groupList.ElementAt(i).title.PositionX += posOffset;
                }

                groupList.ElementAt(i).ResetPosition(posOffset);
            }

            Tizen.Log.Fatal("NUI", "...");
            return;
        }
        /// <summary>
        /// Add row to the column in GridView.
        /// </summary>
        /// <param name="columnIndex">The column index;</param>
        /// <param name="rowHeight">The row space.</param>
        /// <param name="groupIndex">The group index.</param>
        public void AddRowToColumn(int columnIndex, int rowHeight, int groupIndex = 0)
        {
            Tizen.Log.Fatal("NUI", string.Format("[GridView]Add row rowHeight {0} to column {1} of group {2}", rowHeight, columnIndex, groupIndex));
            if (!IsColumnIndexValid(groupIndex, columnIndex))
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]Invalid column index"));
                return;
            }

            Group curGroup = groupList.ElementAt(groupIndex);
            Column curColumn = curGroup.columnList.ElementAt(columnIndex);

            int curRowNumInCol = curColumn.cellList.Count;

            Cell newCell = new Cell();

            Cell lastCell = (curRowNumInCol == 0 ? null : curColumn.cellList.ElementAt(curRowNumInCol - 1));

            if (GridType.Horizontal == gridType)
            {
                newCell.rect.X = curColumn.startPos;
                newCell.rect.Y = (curRowNumInCol == 0 ? 0 : lastCell.rect.Bottom() + rowSpace);
                newCell.rect.Height = rowHeight;
                newCell.rect.Width = curColumn.space;
            }
            else
            {
                newCell.rect.X = (curRowNumInCol == 0) ? 0 : lastCell.rect.Right() + columnSpace;
                newCell.rect.Y = curColumn.startPos;
                newCell.rect.Height = rowHeight;
                newCell.rect.Width = curColumn.space;
            }

            newCell.columnIndex = columnIndex;
            newCell.rowIndex = curRowNumInCol;
            newCell.index = curRowNumInCol;
            if (curRowNumInCol == 0)
            {
                if (columnIndex == 0)
                {
                    newCell.itemIndex = 0;
                }
                else
                {
                    newCell.itemIndex = curGroup.columnList.ElementAt(columnIndex - 1).endItemIndex + 1;
                }
            }
            else
            {
                newCell.itemIndex = lastCell.itemIndex + 1;
            }

            Tizen.Log.Fatal("NUI", " >>>>>>> newCell.itemIndex: " + newCell.itemIndex + " newCell.rect[" + newCell.rect.X + ", " + newCell.rect.Y + ", " + newCell.rect.Width + ", " + newCell.rect.Height + "]");

            curColumn.cellList.Add(newCell);
            Tizen.Log.Fatal("NUI", "...");

            if (curRowNumInCol == 0)
            {
                curColumn.startItemIndex = newCell.itemIndex;
            }

            curColumn.endItemIndex = newCell.itemIndex;
            Tizen.Log.Fatal("NUI", " >>>>>>> curColumn.startItemIndex: " + curColumn.startItemIndex + " curColumn.endItemIndex: " + curColumn.endItemIndex);

            curGroup.itemIndexToCell.Add(newCell.itemIndex, newCell);
            curGroup.realCellCount++;

            Tizen.Log.Fatal("NUI", "Group: " + groupIndex + ", Column: " + columnIndex + " Cell.rowIndex: " + newCell.rowIndex + " Cell.columnIndex: " + newCell.columnIndex + " Cell.itemIndex: " + newCell.itemIndex + " curGroup.realCellCount:" + curGroup.realCellCount);
        }

        // Add row to the all column in same group of GridView.
        internal void AddRowToAllColumn(int rowHeight, int groupIndex)
        {
            Tizen.Log.Fatal("NUI", string.Format("[GridView]Add row rowHeight {0} to to all column of group {1}", rowHeight, groupIndex));
            if (!IsGroupIndexValid(groupIndex))
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]Invalid group index"));
                return;
            }

            int columnCount = groupList.ElementAt(groupIndex).columnList.Count;
            for (int i = 0; i < columnCount; i++)
            {
                AddRowToColumn(i, rowHeight, groupIndex);
            }
        }
        /// <summary>
        /// Quickly add an regular grid by pass column/row number and cell size.
        /// </summary>
        /// <param name="columnCount">The columns count.</param>
        /// <param name="rowCount">The rows count.</param>
        /// <param name="columnWidth">The column width.</param>
        /// <param name="rowHeight">The row height.</param>
        /// <param name="groupIndex">The groupIndex, default value is -1, means all the groups will use the same regular grid.</param>
        public void AddRegularGrid(int columnCount, int rowCount, int columnWidth, int rowHeight, int groupIndex = -1)
        {
            Tizen.Log.Fatal("NUI", string.Format("[GridView]Add row x column : ({0} x {1}) rowHeight: {2} , columnWidth: {3} to group {4}", rowCount, columnCount, rowHeight, columnWidth, groupIndex));

            if (groupIndex != -1)
            {
                if (groupList.Count == 0)
                {
                    Group newGroup = new Group();
                    newGroup.index = 0;
                    newGroup.columnList = new List<Column>();
                    newGroup.itemList = new List<GridItem>();
                    newGroup.itemIndexToCell = new Dictionary<int, Cell>();
                    newGroup.startPos = newGroup.endPos = 0;
                    groupList.Add(newGroup);
                }
                else if (!IsGroupIndexValid(groupIndex))
                {
                    Tizen.Log.Fatal("NUI", string.Format("[GridView]Invalid group index"));
                    return;
                }
            }

            if (gridType == GridType.Vertical)
            {
                int temp = rowCount;
                rowCount = columnCount;
                columnCount = temp;
            }

            this.rowCnt = rowCount;
            this.colCnt = columnCount;
            this.rowH = rowHeight;
            this.colW = columnWidth;

            if (columnCount == -1)
            {
                return;
            }

            if (groupIndex == -1)
            {
                for (int k = 0; k < groupList.Count; k++)
                {
                    for (int i = 0; i < columnCount; i++)
                    {
                        AddColumn(columnWidth, groupIndex);

                        for (int j = 0; j < rowCount; j++)
                        {
                            AddRowToColumn(i, rowHeight, groupIndex);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < columnCount; i++)
                {
                    AddColumn(columnWidth, groupIndex);

                    for (int j = 0; j < rowCount; j++)
                    {
                        AddRowToColumn(i, rowHeight, groupIndex);
                    }
                }
            }
        }

        /// <summary>
        /// Attach title to the group.
        /// </summary>
        /// <param name="titleView">The title view.</param>
        /// <param name="groupIndex">The group index.</param>
        public void AttachGroupTitle(View titleView, int groupIndex = 0)
        {
            Tizen.Log.Fatal("NUI", string.Format("[GridView]Attach group title to {0}", groupIndex));
            if (!IsGroupIndexValid(groupIndex))
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]Invalid group index"));
                return;
            }

            Group curGroup = groupList.ElementAt(groupIndex);
            if (GridType.Horizontal == gridType)
            {
                titleView.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
                titleView.PivotPoint = Tizen.NUI.PivotPoint.BottomLeft;
                titleView.PositionUsesPivotPoint = true;
                titleView.PositionX = curGroup.startPos;
                titleView.PositionY = -titleSpace;
            }
            else
            {
                titleView.PositionX = 0;
                titleView.PositionY = curGroup.startPos - titleSpace;
            }

            curGroup.title = titleView;
            itemGroup.Add(titleView);
        }

        /// <summary>
        /// Get group title of specified group index.
        /// </summary>
        /// <param name="groupIndex">Group index of GridIndex view.</param>
        /// <returns>Title view of specified group index.</returns>
        public View GetGroupTitle(int groupIndex)
        {
            if (!IsGroupIndexValid(groupIndex))
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]Invalid group index"));
                return null;
            }

            return groupList[groupIndex].title;
        }

        /// <summary>
        /// Set focus group index and item index of GridView.
        /// </summary>
        /// <param name="itemIndex">focus item index</param>
        /// <param name="groupIndex">focus group index</param>
        public void SetFoucsItemIndex(int itemIndex, int groupIndex = 0)
        {
            Tizen.Log.Fatal("NUI", string.Format("[GridView]Set Focus to item {0} of group {1}", itemIndex, groupIndex));
            if (!IsItemIndexValid(groupIndex, itemIndex))
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]Invalid item index"));
                return;
            }

            if (groupIndex == focusGroupIndex && itemIndex == focusItemIndex)
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]Already focused"));
                return;
            }
            //if (IsFocused == true)
            //{
            //    SetFocus(groupIndex, itemIndex);
            //}
            if (HasFocus() == true)
            {
                SetFocus(groupIndex, itemIndex);
            }
            else
            {
                focusGroupIndex = groupIndex;
                focusItemIndex = itemIndex;
                focusTagGroupIndex = focusGroupIndex;
                focusTagItemIndex = focusItemIndex;
            }
        }

        /// <summary>
        /// Get focus group index and item index of GridView.
        /// </summary>
        /// <param name="groupIndex">focus group index</param>
        /// <param name="itemIndex">focus item index</param>
        public void GetFocusItemIndex(out int groupIndex, out int itemIndex)
        {
            groupIndex = focusGroupIndex;
            itemIndex = focusItemIndex;
        }

        /// <summary>
        /// Get loaded item view in GridView.
        /// </summary>
        /// <param name="groupIndex">Specified group index</param>
        /// <param name="itemIndex">Specified item index</param>
        /// <returns> Return the ItemView, if the item is unloaded, returns null.</returns>
        public object GetLoadedItemView(int groupIndex, int itemIndex)
        {
            GridItem item = null;
            if (IsGroupIndexValid(groupIndex) == true && IsItemIndexValid(groupIndex, itemIndex))
            {
                item = groupList.ElementAt(groupIndex).itemList.ElementAt(itemIndex);
            }

            return item?.itemView;
        }

        /// <summary>
        /// User to do the page move.
        /// </summary>
        /// <param name="bAni">Used to control whether support animation.</param>
        public void MoveNextPage(bool bAni = true)
        {
            Tizen.Log.Fatal("NUI", string.Format("[GridView]Move to next page"));
            PageMove(false, bAni);
        }

        /// <summary>
        /// User to do the page move.
        /// </summary>
        /// <param name="bAni">Used to control whether support animation.</param>
        public void MovePrevPage(bool bAni = true)
        {
            Tizen.Log.Fatal("NUI", string.Format("[GridView]Move to previous page"));
            PageMove(true, bAni);
        }

        /// <summary>
        /// Update an item.
        /// </summary>
        /// <param name="itemIndex">The itemIndex.</param>
        /// <param name="groupIndex">The groupIndex.</param>
        public void UpdateItem(int itemIndex, int groupIndex = 0)
        {
            Tizen.Log.Fatal("NUI", string.Format("[GridView]Update item {0} of group {1}", itemIndex, groupIndex));
            if (!IsItemIndexValid(groupIndex, itemIndex))
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]Invalid group index"));
                return;
            }

            Cell curCell = GetCellByItemIndex(groupIndex, itemIndex);
            int columnIndex = curCell.columnIndex;

            if ((groupIndex > onScreenRange.startGroupIndex && groupIndex < onScreenRange.endGroupIndex)
                || (groupIndex == onScreenRange.startGroupIndex && columnIndex >= onScreenRange.startColumnIndex)
                || (groupIndex == onScreenRange.endGroupIndex && columnIndex <= onScreenRange.endColumnIndex))
            {
                gridBridge.UpdateItem(groupIndex, itemIndex, groupList.ElementAt(groupIndex).itemList.ElementAt(itemIndex).itemView);
            }
        }
        /// <summary>
        /// Update all items on screen.
        /// </summary>
        public void UpdateAllItems()
        {
            foreach (GridItem item in curInMemoryItemSet)
            {
                gridBridge.UpdateItem(item.groupIndex, item.index, item.itemView);
            }
        }
        /// <summary>
        /// Get group number of GridView.
        /// </summary>
        /// <returns>Groups count</returns>
        public int GetGroupCount()
        {
            return groupList.Count;
        }
        /// <summary>
        /// Get column count of specified group.
        /// </summary>
        /// <param name="groupIndex">The group index.</param>
        /// <returns>Columns count</returns>
        public int GetColumnCount(int groupIndex)
        {
            Tizen.Log.Fatal("NUI", "... groupIndex = " + groupIndex);
            if (!IsGroupIndexValid(groupIndex))
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]Invalid group index"));
                return INVALID_COUNT;
            }

            Group curGroup = groupList.ElementAt(groupIndex);

            if (curGroup.columnList == null)
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]Invalid column list"));
                return INVALID_COUNT;
            }

            Tizen.Log.Fatal("NUI", "... curGroup.columnList.Count = " + curGroup.columnList.Count);
            return curGroup.columnList.Count;
        }
        /// <summary>
        /// Get cell count of specified column of specified group.
        /// </summary>
        /// <param name="groupIndex">The group index.</param>
        /// <param name="columnIndex">The column index.</param>
        /// <returns>Cells count</returns>
        public int GetCellCount(int groupIndex, int columnIndex)
        {
            if (!IsColumnIndexValid(groupIndex, columnIndex))
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]Invalid group index"));
                return INVALID_COUNT;
            }

            Column curColumn = groupList.ElementAt(groupIndex).columnList.ElementAt(columnIndex);

            if (curColumn.cellList == null)
            {
                return INVALID_COUNT;
            }

            return curColumn.cellList.Count;
        }


        /// <summary>
        /// Get item count of a group.
        /// </summary>
        /// <param name="groupIndex">The group index.</param>
        /// <returns>Items count</returns>
        public int GetItemCount(int groupIndex)
        {
            if (!IsGroupIndexValid(groupIndex))
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]Invalid group index"));
                return INVALID_COUNT;
            }

            Group curGroup = groupList.ElementAt(groupIndex);
            return curGroup.itemList.Count;
        }
        /// <summary>
        /// Get the GridBridge of GridView.
        /// </summary>
        /// <returns>The GridBridge of GridView.</returns>
        public GridBridge GetBridge()
        {
            return gridBridge;
        }
        /// <summary>
        /// Get on screen item range.
        /// </summary>
        /// <param name="startGroupIndex">The group index of start range.</param>
        /// <param name="startItemIndex">The item index of start range.</param>
        /// <param name="endGroupIndex">The group index of end range.</param>
        /// <param name="endItemIndex">The item index of end range.</param>
        public void GetOnscreenRange(out int startGroupIndex, out int startItemIndex, out int endGroupIndex, out int endItemIndex)
        {
            startGroupIndex = onScreenRange.startGroupInScrn;
            startItemIndex = onScreenRange.startItemIndexInScrn;
            endGroupIndex = onScreenRange.endGroupInScrn;
            endItemIndex = onScreenRange.endItemIndexInScrn;
        }

        /// <summary>
        /// Move focus according to parameter direction
        /// </summary>
        /// <param name="direction">Focus move direction, can be "Left", "Right",  "Up" or "Down"</param>
        public void Move(string direction)
        {
            MoveFocus(direction);
        }

        /// <summary>
        /// Get screen position of specified item.
        /// </summary>
        /// <param name="groupIndex">Group index of linear view.</param>
        /// <param name="itemIndex">Item index of group.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when groupIndex or item index is invalid.
        /// </exception>
        /// <returns>Screen position of specified item.</returns>
        public Position2D GetItemScreenPosition(int groupIndex, int itemIndex)
        {
            if (!IsItemIndexValid(groupIndex, itemIndex))
            {
                throw new ArgumentException("Group index or item index is invalid");
            }

            GridItem curItem = groupList.ElementAt(groupIndex).itemList.ElementAt(itemIndex);
            return curItem?.itemView?.ScreenPosition;
        }

        /// <summary>
        /// Dispose of GridView.
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

                Tizen.Log.Fatal("NUI", string.Format("[GridView]Dispose"));
                focusMoveAni?.Stop();
                focusMoveAni?.Dispose();
                focusMoveAni = null;

                ClearRecycleBin();
                ResetGroup(0);
            }

            //Release your own unmanaged resources here.
            //You should not access any managed member here except static instance.
            //because the execution order of Finalizes is non-deterministic.
            //Unreference this from if a static instance refer to this.

            //You must call base.Dispose(type) just before exit.
            base.Dispose(type);
        }

        /// <summary>
        /// GridView event handler
        /// </summary>
        public event EventHandler<GridEventArgs> GridEvent
        {
            add
            {
                gridEventHandlers += value;
            }

            remove
            {
                gridEventHandlers -= value;
            }
        }
        /// <summary>
        /// Update when GridView attributes changed.
        /// </summary>
        /// <param name="sender">the object</param>
        /// <param name="e">the args of the event</param>
        protected void OnRelayoutUpdate(object sender, EventArgs e)
        {
            gridWidth = this.SizeWidth;
            gridHeight = this.SizeHeight;
            Tizen.Log.Fatal("NUI", "...OnRelayoutUpdateOnRelayoutUpdateOnRelayoutUpdateOnRelayoutUpdate...");
            if (gridBridge != null)
            {
                Load();

                if (HasFocus() == true && curInMemoryItemSet.Count != 0)
                {
                    SetFocus(focusGroupIndex, focusItemIndex);
                }
            }
        }

        private void OnListEvent(GridEventArgs e)
        {
            if (gridEventHandlers != null)
            {
                gridEventHandlers(this, e);
            }
        }

        private void Initialize()
        {
            ClippingMode = ClippingModeType.ClipChildren;


            groupList = new List<Group>();
            itemGroup = new View();
            itemGroup.Name = "ItemGroup";
            itemGroup.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            itemGroup.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            this.Add(itemGroup);

            itemGroupRect = new Rect(0, 0, 0, 0);

            curInMemoryItemSet = new HashSet<GridItem>();
            nextInMemoryItemSet = new HashSet<GridItem>();

            lastVisibleArea = new Rect(0, 0, 0, 0);
            onScreenRange = new Range(-1, -1, -1, -1);

            refXForFocusMove = -1;
            refYForFocusMove = -1;

            focusGroupIndex = 0;
            focusItemIndex = 0;

            focusTagGroupIndex = 0;
            focusTagItemIndex = 0;

            StateRightCircularEnable = false;
            StateLeftCircularEnable = false;


            focusMoveAni = new LinkerAnimation(itemGroup, 100, 100, 0);
            focusMoveAni.ViaEvent += OnFocusMoveViaEvent;

            ScrollBarFadeOutDuration = 1000;
        }

        private void Load()
        {
            Tizen.Log.Fatal("NUI", "...lOAD...");
            int groupCount = groupList.Count;
            if (groupCount == 0)
            {
                return;
            }

            for (int i = 0; i < groupCount; i++)
            {
                Group curGroup = groupList.ElementAt(i);
                int dataCount = gridBridge.GetItemCount(i);

                int finalCount = curGroup.realCellCount < dataCount ? curGroup.realCellCount : dataCount;

                int bindedItemCount = curGroup.itemList.Count;

                for (int j = 0; j < finalCount; j++)
                {
                    GridItem newItem;
                    if (j < bindedItemCount)
                    {
                        newItem = curGroup.itemList.ElementAt(j);
                    }
                    else
                    {
                        Cell curCell = curGroup.itemIndexToCell[j];
                        newItem = new GridItem();
                        newItem.groupIndex = i;
                        newItem.index = j;
                        newItem.rect = curCell.rect;
                        curGroup.itemList.Add(newItem);
                    }
                }

                //add for right move between group when not all columns filled
                if (finalCount >= 1)
                {
                    Cell lastItemCell = GetCellByItemIndex(i, finalCount - 1);
                    curGroup.lastFilledColumnIndex = lastItemCell.columnIndex;
                }
            }

            UpdateItemGroupSize();

            InitItemGroupPosition();
            UpdateInMemoryItems();
            Tizen.Log.Fatal("NUI", ">>>>>>>>> itemGroupRect: " + itemGroupRect.X + ", " + itemGroupRect.Y + ", " + itemGroupRect.Width + ", " + itemGroupRect.Height);
            Tizen.Log.Fatal("NUI", ">>>>>>>>> itemGroup: " + itemGroup.PositionX + ", " + itemGroup.PositionY + ", " + itemGroup.SizeWidth + ", " + itemGroup.SizeHeight);
        }

        private void UpdateInMemoryItems()
        {
            Rect windowRect = new Rect(-itemGroupRect.X, -itemGroupRect.Y, gridWidth, gridHeight);

            List<GridItem> loadItemList = new List<GridItem>();
            List<GridItem> unloadItemList = new List<GridItem>();

            GetChangedItems(lastVisibleArea, windowRect, ref loadItemList, ref unloadItemList);
            Tizen.Log.Fatal("NUI", ">>>>>>>>>  loadItemList.Count: " + loadItemList.Count + " unloadItemList.Count: " + unloadItemList.Count);
            lastVisibleArea.X = windowRect.X;
            lastVisibleArea.Y = windowRect.Y;
            lastVisibleArea.Width = windowRect.Width;
            lastVisibleArea.Height = windowRect.Height;

            foreach (GridItem item in unloadItemList)
            {
                UnloadItem(item);
            }

            foreach (GridItem item in loadItemList)
            {
                LoadItem(item);
            }

        }

        private void UpdateInMemoryItems(Position itemGroupPos)
        {
            Rect windowRect = new Rect(-itemGroupPos.X, -itemGroupPos.Y, gridWidth, gridHeight);

            List<GridItem> loadItemList = new List<GridItem>();
            List<GridItem> unloadItemList = new List<GridItem>();

            GetChangedItems(lastVisibleArea, windowRect, ref loadItemList, ref unloadItemList);
            lastVisibleArea.X = windowRect.X;
            lastVisibleArea.Y = windowRect.Y;
            lastVisibleArea.Width = windowRect.Width;
            lastVisibleArea.Height = windowRect.Height;

            foreach (GridItem item in unloadItemList)
            {
                UnloadItem(item);
            }

            foreach (GridItem item in loadItemList)
            {
                LoadItem(item);
            }

        }

        private void UnloadItem(GridItem item, bool bDirectly = false)
        {
            Tizen.Log.Fatal("NUI", "item: " + item + " bDirectly: " + bDirectly);
            itemGroup.Remove(item.itemView);

            if (bDirectly)
            {
                gridBridge.UnloadItem(item.groupIndex, item.index, item.itemView);
            }
            else
            {
                AddRecycleView(item.groupIndex, item.index, item.itemView);
            }

            item.itemView = null;

            curInMemoryItemSet.Remove(item);
        }

        private void LoadItem(GridItem item)
        {
            curInMemoryItemSet.Add(item);

            if (item.itemView != null)
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]LoadItem item view is null"));
                return;
            }

            View temp = GetRecycleView(item.groupIndex,item.index);
            Tizen.Log.Fatal("NUI", " temp: " + temp);
            if (temp != null)
            {
                item.itemView = temp;
                gridBridge.UpdateItem(item.groupIndex, item.index, item.itemView);
            }
            else
            {
                item.itemView = gridBridge.GetItemView(item.groupIndex, item.index);

                item.itemView.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
                item.itemView.PivotPoint = Tizen.NUI.PivotPoint.Center;
                item.itemView.PositionUsesPivotPoint = false;
            }

            Tizen.Log.Fatal("NUI", "item.index: " + item.index + " item.rect.X: " + item.rect.X + " item.rect.Y: " + item.rect.Y);
            item.itemView.Position = new Position(item.rect.X, item.rect.Y, 0.0f);
            item.itemView.Size2D = new Size2D((int)item.rect.Width, (int)item.rect.Height);

            itemGroup.Add(item.itemView);
        }

        private void SetViewTypeCount(int typeCount)
        {
            if (typeCount < 1)
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]SetViewTypeCount type count less than 1"));
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

        private void AddRecycleView(int groupIndex, int itemIndex, View view)
        {
            int viewType = gridBridge.GetItemType(groupIndex, itemIndex);

            if (viewType >= viewTypeCount)
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]AddRecycleView viewType larger than viewTypeCount"));
                return;
            }

            if (view != null)
            {
                view.Hide();
                view.Scale = new Vector3(1, 1, 1);
            }

            recycledViews[viewType].Add(view);
        }

        private View GetRecycleView(int groupIndex, int itemIndex)
        {
            int viewType = gridBridge.GetItemType(groupIndex, itemIndex);
            Tizen.Log.Fatal("NUI", " viewType:" + viewType + " viewTypeCount:" + viewTypeCount);

            if (viewType >= viewTypeCount)
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]AddRecycleView viewType larger than viewTypeCount"));
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
            itemGroup.Remove(view);
            gridBridge.UnloadItemByViewType(viewType, view);
        }

        private void GetChangedItems(Rect aLastVisibleArea, Rect curVisibleArea, ref List<GridItem> loadItem, ref List<GridItem> unloadItem)
        {
            Tizen.Log.Fatal("NUI", ">>>>>>>>>  aLastVisibleArea.X: " + aLastVisibleArea.X + " aLastVisibleArea.Y: " + aLastVisibleArea.Y + " aLastVisibleArea.Width: " + aLastVisibleArea.Width + " aLastVisibleArea.Height: " + aLastVisibleArea.Height);
            Tizen.Log.Fatal("NUI", ">>>>>>>>>  curVisibleArea.X: " + curVisibleArea.X + " curVisibleArea.Y: " + curVisibleArea.Y + " curVisibleArea.Width: " + curVisibleArea.Width + " curVisibleArea.Height: " + curVisibleArea.Height);
            GetOnscreenRange(curVisibleArea, ref onScreenRange);

            //Adjust the new start column: if the start column has items which are merge_hidden,
            //find the item's smalllest start column as the new start column.
            Column startColumn = groupList.ElementAt(onScreenRange.startGroupIndex).columnList.ElementAt(onScreenRange.startColumnIndex);
            int cellCount = startColumn.cellList.Count;

            //save new range items to list
            nextInMemoryItemSet.Clear();
            GetItemsInRange(onScreenRange, ref nextInMemoryItemSet);

            loadItem = nextInMemoryItemSet.Except(curInMemoryItemSet).ToList();
            unloadItem = curInMemoryItemSet.Except(nextInMemoryItemSet).ToList();
        }

        private void GetOnscreenRange(Rect visibleArea, ref Range range, bool bAllInArea = false)
        {
            Cell focusedCell = GetCellByItemIndex(focusGroupIndex, focusItemIndex);

            int focusColumnIndex = focusedCell.columnIndex;

            float areaStartPos, areaEndPos, focusedCellStart, focusedCellEnd;
            if (GridType.Horizontal == gridType)
            {
                areaStartPos = visibleArea.X;
                areaEndPos = visibleArea.X + visibleArea.Width;
                focusedCellStart = focusedCell.rect.X;
                focusedCellEnd = focusedCell.rect.X + focusedCell.rect.Width;
            }
            else
            {
                areaStartPos = visibleArea.Y;
                areaEndPos = visibleArea.Y + visibleArea.Height;
                focusedCellStart = focusedCell.rect.Y;
                focusedCellEnd = focusedCell.rect.Y + focusedCell.rect.Height;
            }

            bool bStartFound = false, bEndFound = false;

            int groupCount = groupList.Count;
            //find start range
            if ((bAllInArea == true && focusedCellStart < areaStartPos)
                || (bAllInArea == false && focusedCellEnd < areaStartPos))
            {
               for (int i = focusGroupIndex; i < groupCount && bStartFound == false; i++)
               {
                   Group curGroup = groupList.ElementAt(i);
                   int startColumnIndex = (i == focusItemIndex) ? focusColumnIndex : 0;
                   int columnCount = curGroup.columnList.Count;
                   for (int j = startColumnIndex; j < columnCount; j++)
                   {
                       Column curColumn = curGroup.columnList.ElementAt(j);
                       if (true == bAllInArea)
                       {
                           if (curColumn.startPos >= areaStartPos)
                           {
                               range.startGroupIndex = i;
                               range.startColumnIndex = j;
                               bStartFound = true;
                               break;
                           }
                       }
                       else
                       {
                           if (curColumn.endPos > areaStartPos)
                           {
                               range.startGroupIndex = i;
                               range.startColumnIndex = j;
                               bStartFound = true;
                               break;
                           }
                       }
                   }
               }
            }
            else
            {
                for (int i = focusGroupIndex; i >= 0 && bStartFound == false; i--)
                {
                    Group curGroup = groupList.ElementAt(i);
                    int columnCount = curGroup.columnList.Count;
                    int startColumnIndex = (i == focusItemIndex) ? focusColumnIndex : columnCount - 1;
                    for (int j = startColumnIndex; j >= 0; j--)
                    {
                        Column curColumn = curGroup.columnList.ElementAt(j);
                        if (curColumn.startPos <= areaStartPos)
                        {
                            if (bAllInArea == true)
                            {
                                if (j == columnCount - 1)
                                {
                                    range.startGroupIndex = i + 1;
                                    range.startColumnIndex = 0;
                                }
                                else
                                {
                                    range.startGroupIndex = i;
                                    range.startColumnIndex = j + 1;
                                }

                                bStartFound = true;
                                break;
                            }
                            else
                            {
                                if (curColumn.endPos > areaStartPos)
                                {
                                    range.startGroupIndex = i;
                                    range.startColumnIndex = j;
                                }
                                else
                                {
                                    if (j == columnCount - 1)
                                    {
                                        range.startGroupIndex = i + 1;
                                        range.startColumnIndex = 0;
                                    }
                                    else
                                    {
                                        range.startGroupIndex = i;
                                        range.startColumnIndex = j + 1;
                                    }
                                }

                                bStartFound = true;
                                break;
                            }
                        }
                    }
                }
            }

            if (bStartFound == false)
            {
                range.startGroupIndex = 0;
                range.startColumnIndex = 0;
            }

            //find end range
            if ((bAllInArea == true && focusedCellEnd >= areaEndPos)
                || (bAllInArea == false && focusedCellStart >= areaEndPos))
            {
                for (int i = focusGroupIndex; i >= 0 && bEndFound == false; i--)
                {
                    Group curGroup = groupList.ElementAt(i);
                    int columnCount = curGroup.columnList.Count;
                    int startColumnIndex = (i == focusItemIndex) ? focusColumnIndex : columnCount - 1;
                    for (int j = startColumnIndex; j >= 0; j--)
                    {
                        Column curColumn = curGroup.columnList.ElementAt(j);
                        if (bAllInArea == true)
                        {
                            if (curColumn.endPos <= areaEndPos)
                            {
                                range.endGroupIndex = i;
                                range.endColumnIndex = j;
                                bEndFound = true;
                                break;
                            }
                        }
                        else
                        {
                            if (curColumn.startPos <= areaEndPos)
                            {
                                range.endGroupIndex = i;
                                range.endColumnIndex = j;
                                bEndFound = true;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = focusGroupIndex; i < groupCount && bEndFound == false; i++)
                {
                    Group curGroup = groupList.ElementAt(i);
                    int startColumnIndex = (i == focusItemIndex) ? focusColumnIndex : 0;
                    int columnCount = curGroup.columnList.Count;
                    for (int j = startColumnIndex; j < columnCount; j++)
                    {
                        Column curColumn = curGroup.columnList.ElementAt(j);
                        if (curColumn.endPos >= areaEndPos)
                        {
                            if (curColumn.startPos < areaEndPos)
                            {
                                range.endGroupIndex = i;
                                range.endColumnIndex = j;
                            }
                            else
                            {
                                if (j == 0)
                                {
                                    range.endGroupIndex = i - 1;
                                    range.endColumnIndex = groupList.ElementAt(i - 1).columnList.Count - 1;
                                }
                                else
                                {
                                    range.endGroupIndex = i;
                                    range.endColumnIndex = j - 1;
                                }
                            }

                            bEndFound = true;
                            break;
                        }
                    }
                }
            }

            if (bEndFound == false)
            {
                range.endGroupIndex = groupCount - 1;
                range.endColumnIndex = groupList.ElementAt(range.endGroupIndex).columnList.Count - 1;
            }

            range.startGroupInScrn = range.startGroupIndex;
            range.startColumnInScrn = range.startColumnIndex;
            range.endGroupInScrn = range.endGroupIndex;
            range.endColumnInScrn = range.endColumnIndex;

            //get on screen start and end item index
            Group startGroup = groupList.ElementAt(range.startGroupInScrn);
            Column startColumn = startGroup.columnList.ElementAt(range.startColumnInScrn);
            Cell startCell = startColumn.cellList.ElementAt(0);
            range.startItemIndexInScrn = startCell.itemIndex;
            if (range.startItemIndexInScrn >= startGroup.itemList.Count)
            {
                range.startItemIndexInScrn = startGroup.itemList.Count - 1;
            }

            Group endGroup = groupList.ElementAt(range.endGroupInScrn);
            Column endColumn = endGroup.columnList.ElementAt(range.endColumnInScrn);
            Cell endCell = endColumn.cellList.ElementAt(endColumn.cellList.Count - 1);
            range.endItemIndexInScrn = endCell.itemIndex;
            if (range.endItemIndexInScrn >= endGroup.itemList.Count)
            {
                range.endItemIndexInScrn = endGroup.itemList.Count - 1;
            }

            //add buffer columns
            int frontBufferCount = preloadFrontColumnSize;
            while (frontBufferCount-- > 0)
            {
                if (range.startColumnIndex > 0)
                {
                    range.startColumnIndex--;
                }
                else
                {
                    if (range.startGroupIndex > 0)
                    {
                        range.startGroupIndex--;
                        range.startColumnIndex = groupList.ElementAt(range.startGroupIndex).columnList.Count - 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            int backBufferCount = preloadBackColumnSize;
            while (backBufferCount-- > 0)
            {
                if (range.endColumnIndex < groupList.ElementAt(range.endGroupIndex).columnList.Count - 1)
                {
                    range.endColumnIndex++;
                }
                else
                {
                    if (range.endGroupIndex < groupList.Count - 1)
                    {
                        range.endGroupIndex++;
                        range.endColumnIndex = 0;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void GetItemsInRange(Range range, ref HashSet<GridItem> itemSet)
        {
            for (int i = range.startGroupIndex; i <= range.endGroupIndex; i++)
            {
                Group curGroup = groupList.ElementAt(i);
                int startColumnIndex = (i == range.startGroupIndex) ? range.startColumnIndex : 0;
                int endColumnIndex = (i == range.endGroupIndex) ? range.endColumnIndex : curGroup.columnList.Count - 1;

                for (int j = startColumnIndex; j <= endColumnIndex; j++)
                {
                    Column curColumn = curGroup.columnList.ElementAt(j);

                    for (int k = 0; k < curColumn.cellList.Count; k++)
                    {
                        Cell curCell = curColumn.cellList.ElementAt(k);
                        if (curCell.itemIndex < curGroup.itemList.Count)
                        {
                            itemSet.Add(curGroup.itemList.ElementAt(curCell.itemIndex));
                        }
                    }
                }
            }
        }

        private void MoveFocus(string direction)
        {
            string logicalDirection = GetLogicalDirection(direction);
            MoveFocus(logicalDirection, focusTagGroupIndex, focusTagItemIndex);
        }

        // Get the specific groupIndex,itemIndex neighbor item according to direction
        private GridItem GetItemByDirection(string direction, int fromGroupIndex, int fromItemIndex)
        {
            Tizen.Log.Fatal("NUI", ">>>>>>>>> direction: " + direction + " fromGroupIndex: " + fromGroupIndex + " fromItemIndex: " + fromItemIndex);
            Cell currentCell = GetCellByItemIndex(fromGroupIndex, fromItemIndex);

            //adjust focused column index based on reference line
            Group currentGroup = groupList.ElementAt(fromGroupIndex);
            int currentColumnIndex = currentCell.columnIndex;
            Column currentColumn = currentGroup.columnList.ElementAt(currentColumnIndex);

            int destFocusGroupIndex = -2, destFocusItemIndex = -2;
            int destFocusColumnIndex = -1;
            switch (direction)
            {
                case "Up":
                    destFocusGroupIndex = fromGroupIndex;
                    if (currentCell.rowIndex == 0)
                    {
                        destFocusItemIndex = -1;
                    }
                    else
                    {
                        Cell nextCell = currentColumn.cellList.ElementAt(currentCell.rowIndex - 1);
                        destFocusItemIndex = nextCell.itemIndex;
                    }

                    break;
                case "Down":
                    destFocusGroupIndex = fromGroupIndex;
                    if (currentCell.rowIndex >= currentColumn.cellList.Count - 1)
                    {
                        destFocusItemIndex = -1;
                    }
                    else if (fromItemIndex == currentGroup.itemList.Count - 1)
                    {
                        destFocusColumnIndex = currentColumnIndex - 1;
                        Column nextColumn = currentGroup.columnList.ElementAt(destFocusColumnIndex);
                        Cell nextCell = nextColumn.cellList.ElementAt(currentCell.rowIndex + 1);
                        destFocusItemIndex = nextCell.itemIndex;
                    }
                    else
                    {
                        Cell nextCell = currentColumn.cellList.ElementAt(currentCell.rowIndex + 1);
                        destFocusItemIndex = nextCell.itemIndex;
                    }

                    break;
                case "Left":
                    if (currentCell.columnIndex == 0)
                    {
                        if (fromGroupIndex == 0)
                        {
                            destFocusItemIndex = -1;
                        }
                        else
                        {
                            destFocusGroupIndex = fromGroupIndex - 1;
                            Group nextGroup = groupList.ElementAt(destFocusGroupIndex);
                            destFocusColumnIndex = nextGroup.columnList.Count - 1;

                            Column nextColumn = nextGroup.columnList.ElementAt(destFocusColumnIndex);
                            if (currentCell.rowIndex >= nextColumn.cellList.Count)
                            {
                                Cell nextCell = nextColumn.cellList.ElementAt(nextColumn.cellList.Count - 1);
                                destFocusItemIndex = nextCell.itemIndex;
                            }
                            else
                            {
                                Cell nextCell = nextColumn.cellList.ElementAt(currentCell.rowIndex);
                                destFocusItemIndex = nextCell.itemIndex;
                            }
                        }
                    }
                    else
                    {
                        destFocusGroupIndex = fromGroupIndex;
                        destFocusColumnIndex = currentCell.columnIndex - 1;
                        Tizen.Log.Fatal("NUI", ">>>>>>>>> destFocusGroupIndex: " + destFocusGroupIndex + " destFocusColumnIndex:" + destFocusColumnIndex + " currentCell.rowIndex: " + currentCell.rowIndex);
                        Column nextColumn = currentGroup.columnList.ElementAt(destFocusColumnIndex);
                        Cell nextCell = nextColumn.cellList.ElementAt(currentCell.rowIndex);
                        destFocusItemIndex = nextCell.itemIndex;
                        Tizen.Log.Fatal("NUI", ">>>>>>>>> destFocusItemIndex: " + destFocusItemIndex);
                    }

                    break;
                case "Right":
                    if (currentColumnIndex == currentGroup.columnList.Count - 1)
                    {
                        if (fromGroupIndex == groupList.Count - 1)
                        {
                            destFocusItemIndex = -1;
                        }
                        else
                        {
                            destFocusGroupIndex = fromGroupIndex + 1;
                            destFocusColumnIndex = 0;
                            Group nextGroup = groupList.ElementAt(destFocusGroupIndex);
                            Column nextColumn = nextGroup.columnList.ElementAt(destFocusColumnIndex);
                            Cell nextCell = nextColumn.cellList.ElementAt(currentCell.rowIndex);
                            destFocusItemIndex = nextCell.itemIndex;
                        }
                    }
                    else
                    {
                        destFocusGroupIndex = fromGroupIndex;
                        destFocusColumnIndex = currentColumnIndex + 1;

                        Tizen.Log.Fatal("NUI", ">>>>>>>>> destFocusGroupIndex: " + destFocusGroupIndex + " destFocusColumnIndex:" + destFocusColumnIndex + " currentCell.rowIndex: " + currentCell.rowIndex);
                        Column nextColumn = currentGroup.columnList.ElementAt(destFocusColumnIndex);
                        if (currentCell.rowIndex >= nextColumn.cellList.Count)
                        {

                            Cell nextCell = nextColumn.cellList.ElementAt(nextColumn.cellList.Count - 1);
                            destFocusItemIndex = nextCell.itemIndex;
                            Tizen.Log.Fatal("NUI", ">>>>>>>>> destFocusItemIndex: " + destFocusItemIndex + " currentCell.rowIndex: " + currentCell.rowIndex + " nextColumn.cellList.Count: " + nextColumn.cellList.Count);
                        }
                        else
                        {
                            Cell nextCell = nextColumn.cellList.ElementAt(currentCell.rowIndex);
                            destFocusItemIndex = nextCell.itemIndex;
                            Tizen.Log.Fatal("NUI", ">>>>>>>>> destFocusItemIndex: " + destFocusItemIndex + " currentCell.rowIndex: " + currentCell.rowIndex + " nextColumn.cellList.Count: " + nextColumn.cellList.Count);
                        }
                    }

                    break;
                default:
                    break;
            }


            Tizen.Log.Fatal("NUI", ">>>>>>>>> destFocusGroupIndex: " + destFocusGroupIndex + " destFocusColumnIndex:" + destFocusColumnIndex + " destFocusItemIndex: " + destFocusItemIndex);
            GridItem item = null;
            if (IsGroupIndexValid(destFocusGroupIndex) == true && IsItemIndexValid(destFocusGroupIndex, destFocusItemIndex))
            {
                item = groupList.ElementAt(destFocusGroupIndex).itemList.ElementAt(destFocusItemIndex);
            }

            return item;
        }

        private void MoveFocus(string direction, int fromGroupIndex, int fromItemIndex)
        {
            Tizen.Log.Fatal("NUI", ">>>>>>>>> direction: " + direction + " fromGroupIndex: " + fromGroupIndex + " fromItemIndex: " + fromItemIndex);
            Cell curFocusedCell = GetCellByItemIndex(fromGroupIndex, fromItemIndex);
            //change reference line of focus move
            if (refXForFocusMove == -1)
            {
                refXForFocusMove = curFocusedCell.rect.X;
            }

            if (refYForFocusMove == -1)
            {
                refYForFocusMove = curFocusedCell.rect.Y;
            }

            //adjust focused column index based on reference line
            Group focusedGroup = groupList.ElementAt(fromGroupIndex);
            int focusedColumnIndex = curFocusedCell.columnIndex;

            Column focusedColumn = focusedGroup.columnList.ElementAt(focusedColumnIndex);

            int destFocusGroupIndex = -2, destFocusItemIndex = -2;
            int destFocusColumnIndex = -1;
            switch (direction)
            {
                case "Up":
                    destFocusGroupIndex = fromGroupIndex;
                    if (curFocusedCell.index == 0)
                    {
                        destFocusItemIndex = -1;
                    }
                    else
                    {
                        Cell nextCell = focusedColumn.cellList.ElementAt(curFocusedCell.index - 1);
                        {
                            destFocusItemIndex = nextCell.itemIndex;
                            if (destFocusItemIndex >= 0 && destFocusItemIndex < groupList.ElementAt(destFocusGroupIndex).itemList.Count)
                            {
                                if (GridType.Horizontal == gridType)
                                {
                                    refYForFocusMove = nextCell.rect.Y;
                                }
                                else
                                {
                                    refXForFocusMove = nextCell.rect.X;
                                }
                            }
                        }
                    }

                    break;
                case "Down":
                    destFocusGroupIndex = fromGroupIndex;
                    int destCellIndex = curFocusedCell.index + 1;
                    if (destCellIndex >= focusedColumn.cellList.Count || fromItemIndex == focusedGroup.itemList.Count - 1)
                    {
                        destFocusItemIndex = -1;
                        if (fromItemIndex == focusedGroup.itemList.Count - 1)
                        {
                            destFocusColumnIndex = focusedColumnIndex - 1;
                            Cell cellBelow = FindCellBelow(curFocusedCell, destFocusGroupIndex, destFocusColumnIndex);
                            if (cellBelow != null)
                            {
                                destFocusItemIndex = cellBelow.itemIndex;
                                refXForFocusMove = cellBelow.rect.X;
                                refYForFocusMove = cellBelow.rect.Y;
                            }
                        }
                    }
                    else
                    {
                        Cell nextCell = null;
                        while(destCellIndex < focusedColumn.cellList.Count)
                        {
                            nextCell = focusedColumn.cellList.ElementAt(destCellIndex);
                            {
                                break;
                            }
                        }

                        if (destCellIndex >= focusedColumn.cellList.Count)
                        {
                            destFocusItemIndex = -1;
                        }
                        else
                        {
                            destFocusItemIndex = nextCell.itemIndex;
                            if (destFocusItemIndex >= 0 && destFocusItemIndex < groupList.ElementAt(destFocusGroupIndex).itemList.Count)
                            {
                                if (GridType.Horizontal == gridType)
                                {
                                    refYForFocusMove = nextCell.rect.Y;
                                }
                                else
                                {
                                    refXForFocusMove = nextCell.rect.X;
                                }
                            }
                        }
                    }

                    break;
                case "Left":
                    if (curFocusedCell.columnIndex == 0)
                    {
                        if (fromGroupIndex == 0)
                        {
                            if (StateLeftCircularEnable && true/*check whether  allow to loop*/)
                            {
                                destFocusGroupIndex = groupList.Count - 1;
                                destFocusColumnIndex = groupList.ElementAt(destFocusGroupIndex).columnList.Count - 1;

                                GetNearestItem(curFocusedCell, destFocusGroupIndex, destFocusColumnIndex, ref destFocusItemIndex, true);
                                int itemCount = groupList.ElementAt(destFocusGroupIndex).itemList.Count;
                                while (destFocusItemIndex >= itemCount)
                                {
                                    destFocusColumnIndex--;
                                    GetNearestItem(curFocusedCell, destFocusGroupIndex, destFocusColumnIndex, ref destFocusItemIndex, true);
                                }

                                bool bEnabled = gridBridge.IsItemEnabled(destFocusGroupIndex, destFocusItemIndex);
                                while (bEnabled == false)
                                {
                                    destFocusColumnIndex--;
                                    GetNearestItem(curFocusedCell, destFocusGroupIndex, destFocusColumnIndex, ref destFocusItemIndex, true);
                                    bEnabled = gridBridge.IsItemEnabled(destFocusGroupIndex, destFocusItemIndex);
                                }

                                /*ToDo Left Circular*/
                                SetFocus(destFocusGroupIndex, destFocusItemIndex);
                                return;
                            }
                            else
                            {
                                destFocusItemIndex = -1;
                            }

                        }
                        else
                        {
                            destFocusGroupIndex = fromGroupIndex - 1;
                            destFocusColumnIndex = groupList.ElementAt(destFocusGroupIndex).columnList.Count - 1;

                            GetNearestItem(curFocusedCell, destFocusGroupIndex, destFocusColumnIndex, ref destFocusItemIndex, true);
                            int itemCount = groupList.ElementAt(destFocusGroupIndex).itemList.Count;
                            while (destFocusItemIndex >= itemCount)
                            {
                                destFocusColumnIndex--;
                                GetNearestItem(curFocusedCell, destFocusGroupIndex, destFocusColumnIndex, ref destFocusItemIndex, true);
                            }
                        }
                    }
                    else
                    {
                        destFocusGroupIndex = fromGroupIndex;
                        destFocusColumnIndex = curFocusedCell.columnIndex - 1;

                        GetNearestItem(curFocusedCell, destFocusGroupIndex, destFocusColumnIndex, ref destFocusItemIndex, true);
                    }

                    break;
                case "Right":
                    {
                        focusedColumnIndex = curFocusedCell.columnIndex;
                    }

                    Tizen.Log.Fatal("NUI", ">>>>>>>>> focusedColumnIndex: " + focusedColumnIndex);
                    if (focusedColumnIndex == focusedGroup.columnList.Count - 1 || focusedColumnIndex == focusedGroup.lastFilledColumnIndex)
                    {
                        if (fromGroupIndex == groupList.Count - 1)
                        {
                            if (StateRightCircularEnable && true/*check whether allow right circular*/)
                            {
                                destFocusGroupIndex = 0;
                                destFocusColumnIndex = 0;

                                GetNearestItem(curFocusedCell, destFocusGroupIndex, destFocusColumnIndex, ref destFocusItemIndex, true);

                                bool bEnabled = gridBridge.IsItemEnabled(destFocusGroupIndex, destFocusItemIndex);
                                while (bEnabled == false && destFocusColumnIndex < focusedGroup.columnList.Count - 1)
                                {
                                    destFocusColumnIndex++;
                                    GetNearestItem(curFocusedCell, destFocusGroupIndex, destFocusColumnIndex, ref destFocusItemIndex, true);
                                    bEnabled = gridBridge.IsItemEnabled(destFocusGroupIndex, destFocusItemIndex);

                                    refXForFocusMove = refYForFocusMove = -1;
                                }

                                /*ToDo Left Circular*/
                                SetFocus(destFocusGroupIndex, destFocusItemIndex);
                                return;
                            }
                            else
                            {
                                destFocusItemIndex = -1;
                            }
                        }
                        else
                        {
                            destFocusGroupIndex = fromGroupIndex + 1;
                            destFocusColumnIndex = 0;

                            GetNearestItem(curFocusedCell, destFocusGroupIndex, destFocusColumnIndex, ref destFocusItemIndex, true);
                        }
                    }
                    else
                    {
                        destFocusGroupIndex = fromGroupIndex;
                        destFocusColumnIndex = focusedColumnIndex + 1;

                        Tizen.Log.Fatal("NUI", ">>>>>>>>> destFocusGroupIndex: " + destFocusGroupIndex + " destFocusColumnIndex:" + destFocusColumnIndex);
                        GetNearestItem(curFocusedCell, destFocusGroupIndex, destFocusColumnIndex, ref destFocusItemIndex, true);
                        Tizen.Log.Fatal("NUI", ">>>>>>>>> destFocusItemIndex: " + destFocusItemIndex);

                        if (StateRightCircularEnable)
                        {
                            if (destFocusItemIndex >= groupList.ElementAt(destFocusGroupIndex).itemList.Count)
                            {
                                MoveFocus(direction, destFocusGroupIndex, destFocusItemIndex);
                                return;
                            }
                        }
                    }

                    break;
                default:
                    break;
            }


            Tizen.Log.Fatal("NUI", ">>>>>>>>> destFocusGroupIndex: " + destFocusGroupIndex + " destFocusColumnIndex:" + destFocusColumnIndex);
            if (destFocusGroupIndex >= 0 && destFocusItemIndex >= groupList.ElementAt(destFocusGroupIndex).itemList.Count)
            {
                destFocusItemIndex = -1;
            }

            Tizen.Log.Fatal("NUI", ">>>>>>>>> destFocusItemIndex: " + destFocusItemIndex);
            if (destFocusItemIndex == -1)
            {
                //notify event focus move out
                return;
            }

            if (gridBridge.IsItemEnabled(destFocusGroupIndex, destFocusItemIndex) == false)
            {
                MoveFocus(direction, destFocusGroupIndex, destFocusItemIndex);
            }
            else
            {
                PushFocus(destFocusGroupIndex, destFocusItemIndex);
            }

            Tizen.Log.Fatal("NUI", ">>>>>>>>> ");
        }


        private void GetNearestItem(Cell focusedCell, int destGroupIndex, int destColumnIndex, ref int destItemIndex, bool bDirectMove = false)
        {
            Group destGroup = groupList.ElementAt(destGroupIndex);
            Column destColumn = destGroup.columnList.ElementAt(destColumnIndex);

            Cell nextCell = destColumn.cellList.ElementAt(0);
            int cellCount = destColumn.cellList.Count;

            bool bNotAllItemDisabled = false;
            for (int i = 0; i < cellCount; i++)
            {
                Cell curCell = destColumn.cellList.ElementAt(i);
                if (curCell.itemIndex >= 0 && curCell.itemIndex < destGroup.itemList.Count)
                {
                    if (gridBridge.IsItemEnabled(destGroupIndex, curCell.itemIndex))
                    {
                        bNotAllItemDisabled = true;
                        break;
                    }
                }
            }

            float yDis = gridHeight + 1;
            bool bFound = false;
            for (int i = 0; i < cellCount; i++)
            {
                Cell curCell = destColumn.cellList.ElementAt(i);
                if (curCell.itemIndex >= destGroup.itemList.Count)
                {
                    break;
                }

                if (curCell.itemIndex >= 0 && bNotAllItemDisabled)
                {
                    if (gridBridge.IsItemEnabled(destGroupIndex, curCell.itemIndex) == false)
                    {
                        continue;
                    }
                }

                if (curCell.itemIndex < 0)
                {
                    Column realColumn = destGroup.columnList.ElementAt(curCell.columnIndex);
                    Cell realCell = realColumn.cellList.ElementAt(curCell.rowIndex);
                    if (realCell.itemIndex >= destGroup.itemList.Count || gridBridge.IsItemEnabled(destGroupIndex, realCell.itemIndex) == false)
                    {
                        continue;
                    }
                }

                if (GridType.Vertical == gridType)
                {
                    float tempXDis;
                    if (StateStraightPathEnable)
                    {
                        tempXDis = Math.Abs(curCell.rect.X - refXForFocusMove);
                    }
                    else
                    {
                        tempXDis = Math.Abs(curCell.rect.X - focusedCell.rect.X);
                    }

                    if (tempXDis < yDis)
                    {
                        bFound = true;
                        yDis = tempXDis;
                        nextCell = curCell;
                        if (yDis == 0)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    float tempYDis;
                    if (StateStraightPathEnable)
                    {
                        tempYDis = Math.Abs(curCell.rect.Y - refYForFocusMove);
                    }
                    else
                    {
                        tempYDis = Math.Abs(curCell.rect.Y - focusedCell.rect.Y);
                    }

                    if (tempYDis < yDis)
                    {
                        bFound = true;
                        yDis = tempYDis;
                        nextCell = curCell;
                        if (yDis == 0)
                        {
                            break;
                        }
                    }
                }

            }

            {
                destItemIndex = nextCell.itemIndex;
                if (bFound)
                {
                    refXForFocusMove = nextCell.rect.X;
                    if (bDirectMove == false)
                    {
                        refYForFocusMove = nextCell.rect.Y;
                    }
                }
            }
        }

        private string GetLogicalDirection(string direction)
        {
            string logicalDirection = direction;

            if (GridType.Vertical == gridType)
            {
                if ("Left" == direction)
                {
                    logicalDirection = "Up";
                }
                else if ("Right" == direction)
                {
                    logicalDirection = "Down";
                }
                else if ("Down" == direction)
                {
                    logicalDirection = "Right";
                }
                else if ("Up" == direction)
                {
                    logicalDirection = "Left";
                }
            }

            return logicalDirection;
        }

        private Cell FindCellBelow(Cell fromCell, int destGroupIndex, int destColumnIndex)
        {
            Group destGroup = groupList.ElementAt(destGroupIndex);

            if (destColumnIndex < 0 || destColumnIndex >= destGroup.columnList.Count)
            {
                return null;
            }

            Column destColumn = destGroup.columnList.ElementAt(destColumnIndex);

            for (int i = 0; i < destColumn.cellList.Count; i++)
            {
                Cell destCell = destColumn.cellList.ElementAt(i);

                if (destCell.rect.Y > fromCell.rect.Y && destCell.rect.Y + destCell.rect.Height > fromCell.rect.Y + fromCell.rect.Height)
                {
                    {
                        return destCell;
                    }
                }
            }

            return null;
        }

        private void SetFocus(int groupIndex, int itemIndex, bool bAnimation = false, Position newItemGroupPos = null)
        {
            Tizen.Log.Fatal("NUI", ">>>>>>>>> groupIndex: " + groupIndex + " itemIndex: " + itemIndex);
            if (!IsItemIndexValid(groupIndex, itemIndex))
            {
                return;
            }

            bool bNeedScroll = false;

            if (newItemGroupPos == null)
            {
                Position itemGroupPos = new Position(0, 0, 0);
                bNeedScroll = GetItemGroupPosByFocusItem(groupIndex, itemIndex, itemGroupPos);

                itemGroupRect.X = itemGroupPos.X;
                itemGroupRect.Y = itemGroupPos.Y;
            }
            else
            {
                if (GridType.Horizontal == gridType && itemGroupRect.X != newItemGroupPos.X
                   || GridType.Vertical == gridType && itemGroupRect.Y != newItemGroupPos.Y)
                {
                    bNeedScroll = true;
                }

                itemGroupRect.X = newItemGroupPos.X;
                itemGroupRect.Y = newItemGroupPos.Y;
            }

            Tizen.Log.Fatal("NUI", ">>>>>>>>> bNeedScroll: " + bNeedScroll + " itemGroupRect.X: " + itemGroupRect.X + " itemGroupRect.Y: " + itemGroupRect.Y);
            ResetItemGroupRectPos();

            Tizen.Log.Fatal("NUI", ">>>>>>>>> bNeedScroll: " + bNeedScroll + " itemGroupRect.X: " + itemGroupRect.X + " itemGroupRect.Y: " + itemGroupRect.Y);
            if (bNeedScroll)
            {
                Tizen.Log.Fatal("NUI", ">>>>>>>>> bAnimation: " + bAnimation);
                if (bAnimation == true)
                {
                    FocusMoveViaObject obj = new FocusMoveViaObject();
                    obj.toGroupIndex = groupIndex;
                    obj.toItemIndex = itemIndex;
                    focusMoveViaQ.Enqueue(obj);

                    if (gridType == GridType.Horizontal)
                    {
                        focusMoveAni.SetDestination("PositionX", itemGroupRect.X);
                    }
                    else
                    {
                        focusMoveAni.SetDestination("PositionY", itemGroupRect.Y);
                    }

                    focusMoveAni.Play();
                }
                else
                {
                    UpdateInMemoryItems();
                    itemGroup.PositionX = itemGroupRect.X;
                    itemGroup.PositionY = itemGroupRect.Y;
                    NotifyFocusChange(focusGroupIndex, focusItemIndex, groupIndex, itemIndex);
                }
            }
            else
            {
                NotifyFocusChange(focusGroupIndex, focusItemIndex, groupIndex, itemIndex);
            }

            focusTagGroupIndex = groupIndex;
            focusTagItemIndex = itemIndex;

            Tizen.Log.Fatal("NUI", ">>>>>>>>> itemGroupRect: " + itemGroupRect.X + ", " + itemGroupRect.Y + ", " + itemGroupRect.Width + ", " + itemGroupRect.Height);
            Tizen.Log.Fatal("NUI", ">>>>>>>>> itemGroup: " + itemGroup.PositionX + ", " + itemGroup.PositionY + ", " + itemGroup.SizeWidth + ", " + itemGroup.SizeHeight);
        }


        private void PushFocus(int groupIndex, int itemIndex)
        {
            Tizen.Log.Fatal("NUI", ">>>>>>>>> groupIndex: " + groupIndex + " itemIndex:" + itemIndex);
            if (!IsItemIndexValid(groupIndex, itemIndex))
            {
                return;
            }

            Position itemGroupPos = new Position(0, 0, 0);
            bool bNeedScroll = GetItemGroupPosByFocusItem(groupIndex, itemIndex, itemGroupPos);

            itemGroupRect.X = itemGroupPos.X;
            itemGroupRect.Y = itemGroupPos.Y;
            Tizen.Log.Fatal("NUI", ">>>>>>>>> itemGroupRect.X: " + itemGroupRect.X + " itemGroupRect.Y:" + itemGroupRect.Y);

            ResetItemGroupRectPos();

            //focusTag keep move, but real focus will set when focusMoveVia trigger,
            //so remember context of focus move, include from and to, and itemGroupPos information
            focusTagGroupIndex = groupIndex;
            focusTagItemIndex = itemIndex;

            Tizen.Log.Fatal("NUI", ">>>>>>>>> bNeedScroll: " + bNeedScroll + " itemGroupRect.X:" + itemGroupRect.X + " itemGroupRect.Y:" + itemGroupRect.Y);
            if (bNeedScroll)
            {
                if (gridType == GridType.Horizontal)
                {
                    focusMoveAni.SetDestination("PositionX", itemGroupRect.X);
                }
                else
                {
                    focusMoveAni.SetDestination("PositionY", itemGroupRect.Y);
                }

                FocusMoveViaObject obj = new FocusMoveViaObject();
                Tizen.Log.Fatal("NUI", ">>>>>>>>> focusMoveViaQ.Count: " + focusMoveViaQ.Count);
                Tizen.Log.Fatal("NUI", "::::: " + focusGroupIndex + ", " + focusItemIndex);

                obj.toGroupIndex = groupIndex;
                obj.toItemIndex = itemIndex;
                focusMoveViaQ.Enqueue(obj);
                focusMoveAni.Play();
            }
            else
            {
                if (focusMoveAni.IsPlaying() == false)
                {
                    Tizen.Log.Fatal("NUI", "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ " + groupIndex + " " + itemIndex + " ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    NotifyFocusChange(focusGroupIndex, focusItemIndex, groupIndex, itemIndex);
                }
            }
            //To keep focusBar on the top of

            Tizen.Log.Fatal("NUI", ">>>>>>>>> ");
        }

        private bool GetItemGroupPosByFocusItem(int aFocusGroupIndex, int aFocusItemIndex, Position itemGroupPos)
        {
            bool bNeedScroll = false;
            if (!IsItemIndexValid(aFocusGroupIndex, aFocusItemIndex))
            {
                return false;
            }

            Group focusedGroup = groupList.ElementAt(aFocusGroupIndex);
            GridItem focusedItem = focusedGroup.itemList.ElementAt(aFocusItemIndex);

            Tizen.Log.Fatal("NUI", ">>>>>>>>> itemGroupRect: " + itemGroupRect.X + ", " + itemGroupRect.Y + ", " + itemGroupRect.Width + ", " + itemGroupRect.Height);
            Tizen.Log.Fatal("NUI", ">>>>>>>>> itemGroup: " + itemGroup.PositionX + ", " + itemGroup.PositionY + ", " + itemGroup.SizeWidth + ", " + itemGroup.SizeHeight);
            Tizen.Log.Fatal("NUI", ">>>>>>>>> focusedItem.rect: " + focusedItem.rect.X + ", " + focusedItem.rect.Y + ", " + focusedItem.rect.Width + ", " + focusedItem.rect.Height);

            Rect focusBarRectBaseOnList = new Rect(focusedItem.rect.X, focusedItem.rect.Y, focusedItem.rect.Width, focusedItem.rect.Height);
            focusBarRectBaseOnList.X += itemGroupRect.X;
            focusBarRectBaseOnList.Y += itemGroupRect.Y;

            itemGroupPos.X = itemGroupRect.X;
            itemGroupPos.Y = itemGroupRect.Y;

            float startFocusRange;
            float endFocusRange;

            if (GridType.Horizontal == gridType)
            {
                if (0 == focusedItem.rect.X)
                {
                    startFocusRange = margin[0];
                }
                else
                {
                    Tizen.Log.Fatal("NUI", "Left  aFocusItemIndex:: " + aFocusGroupIndex + ", aFocusItemIndex:: " + aFocusItemIndex);
                    GridItem prevItem = GetItemByDirection("Left", aFocusGroupIndex, aFocusItemIndex);
                    //Tizen.Log.Fatal("NUI", "startFocusRange :: " + margin[0]);
                    startFocusRange = prevItem != null ? prevItem.rect.Width / 2 : margin[0];
                }

                if (itemGroupRect.Width == focusedItem.rect.Right())
                {
                    endFocusRange = gridWidth - margin[2];
                }
                else
                {
                    GridItem nextItem = GetItemByDirection("Right", aFocusGroupIndex, aFocusItemIndex);
                    //Tizen.Log.Fatal("NUI", "gridWidth::" + gridWidth + "  gridWidth - margin[2] :: " + (gridWidth - margin[2]));
                    //Tizen.Log.Fatal("NUI", "gridWidth - nextItem.rect.Width / 2 :: " + (gridWidth - nextItem.rect.Width / 2));
                    endFocusRange = nextItem != null ? gridWidth - nextItem.rect.Width / 2 : gridWidth - margin[2];
                    Tizen.Log.Fatal("NUI", "endFocusRange :: " + endFocusRange);
                }

                if (focusBarRectBaseOnList.X < startFocusRange)
                {
                    Tizen.Log.Fatal("NUI", "focusBarRectBaseOnList.X :: " + focusBarRectBaseOnList.X + "  startFocusRange :: " + startFocusRange);
                    itemGroupPos.X += startFocusRange - focusBarRectBaseOnList.X;
                    bNeedScroll = true;
                }
                else if (focusBarRectBaseOnList.Right() > endFocusRange)
                {
                    Tizen.Log.Fatal("NUI", "focusBarRectBaseOnList.Right() :: " + focusBarRectBaseOnList.Right() + "  endFocusRange :: " + endFocusRange);
                    itemGroupPos.X += endFocusRange - focusBarRectBaseOnList.Right();
                    bNeedScroll = true;
                }
            }
            else
            {
                if (0 == focusedItem.rect.Y)
                {
                    startFocusRange = margin[1];
                }
                else
                {
                    GridItem prevItem = GetItemByDirection("Left", aFocusGroupIndex, aFocusItemIndex);
                    startFocusRange = prevItem != null ? prevItem.rect.Height / 2 : margin[1];
                }

                if (itemGroupRect.Height == focusedItem.rect.Bottom())
                {
                    endFocusRange = gridHeight - margin[3];
                }
                else
                {
                    GridItem nextItem = GetItemByDirection("Right", aFocusGroupIndex, aFocusItemIndex);
                    endFocusRange = nextItem != null ? gridHeight - nextItem.rect.Height / 2 : gridHeight - margin[3];
                }

                if (focusBarRectBaseOnList.Y < startFocusRange)
                {
                    itemGroupPos.Y += startFocusRange - focusBarRectBaseOnList.Y;
                    bNeedScroll = true;
                }
                else if (focusBarRectBaseOnList.Bottom() > endFocusRange)
                {
                    itemGroupPos.Y += endFocusRange - focusBarRectBaseOnList.Bottom();
                    bNeedScroll = true;
                }
            }

            return bNeedScroll;
        }

        private void NotifyFocusChange(int fromGroupIndex,int fromItemIndex,int toGroupIndex,int toItemIndex)
        {
            Tizen.Log.Fatal("NUI", ">>>>>>>>> fromGroupIndex: " + fromGroupIndex + " fromItemIndex: " + fromItemIndex + " toGroupIndex: " + toGroupIndex + " toItemIndex: " + toItemIndex);

            if (IsItemIndexValid(fromGroupIndex, fromItemIndex) && (fromGroupIndex != toGroupIndex || fromItemIndex != toItemIndex))
            {
                Group preGroup = groupList.ElementAt(fromGroupIndex);
                GridItem preItem = preGroup.itemList.ElementAt(fromItemIndex);

                ScaleDownItem(preItem);
                if (preItem.itemView != null)
                {
                    gridBridge.FocusChange(fromGroupIndex, fromItemIndex, preItem.itemView, false);
                }
            }

            if (IsItemIndexValid(toGroupIndex, toItemIndex))
            {
                Group curGroup = groupList.ElementAt(toGroupIndex);
                GridItem item = curGroup.itemList.ElementAt(toItemIndex);

                ScaleUpItem(item);
                if (item.itemView != null)
                {
                    Tizen.Log.Fatal("NUI", "Call GridBridge.FocusChange to set focus");
                    gridBridge.FocusChange(toGroupIndex, toItemIndex, item.itemView, true);
                }
            }

            //notify user focus change
            GridEventArgs evtArgs = new GridEventArgs();
            evtArgs.EventType = GridEventType.FocusChange;
            evtArgs.param[0] = fromGroupIndex;
            evtArgs.param[1] = fromItemIndex;
            evtArgs.param[2] = toGroupIndex;
            evtArgs.param[3] = toItemIndex;
            OnListEvent(evtArgs);

            if (toGroupIndex != -1 && toItemIndex != -1)
            {
                focusGroupIndex = toGroupIndex;
                focusItemIndex = toItemIndex;
            }

            Tizen.Log.Fatal("NUI", ">>>>>>>>> focusGroupIndex: " + focusGroupIndex + " focusItemIndex: " + focusItemIndex);
        }

        private void ScaleUpItem(GridItem item)
        {
            if (item.itemView == null)
            {
                return;
            }

            item.itemView.RaiseToTop();
            item.ScaleItem(new Vector3(focusInScaleFactor, focusInScaleFactor, 1.0f), focusInScaleAnimationAttrs);
        }

        private void ScaleDownItem(GridItem item)
        {
            if (item.itemView == null)
            {
                return;
            }

            item.ScaleItem(new Vector3(1.0f, 1.0f, 1.0f), focusOutScaleAnimationAttrs);
        }

        private void UpdateItemGroupSize()
        {
            if (groupList.Count == 0)
            {
                return;
            }

            Group lastGroup = groupList.ElementAt(groupList.Count - 1);

            int lastItemIndex = lastGroup.itemList.Count - 1;
            if (lastItemIndex < 0)
            {
                return;
            }

            Cell lastFilledCell = GetCellByItemIndex(groupList.Count - 1 ,lastItemIndex);
            int lastFilledColumnIndex = lastFilledCell.columnIndex;
            Column lastFilledColumn = lastGroup.columnList.ElementAt(lastFilledColumnIndex);

            int cellCount = lastFilledColumn.cellList.Count;
            for (int i = 0; i < cellCount;  i++)
            {
                Cell curCell = lastFilledColumn.cellList.ElementAt(i);
                if (curCell.columnIndex > lastFilledColumnIndex)
                {
                    lastFilledColumnIndex = lastFilledColumn.cellList.ElementAt(i).columnIndex;
                }
            }

            lastFilledColumn = lastGroup.columnList.ElementAt(lastFilledColumnIndex);

            float width, height;

            if (GridType.Horizontal == gridType)
            {
                width = lastFilledColumn.endPos;
                height = gridHeight - (margin[1] + margin[3]);
            }
            else
            {
                width = gridWidth - (margin[0] + margin[2]);
                height = lastFilledColumn.endPos;
            }

            itemGroupRect.Width = width;
            itemGroupRect.Height = height;

            Tizen.Log.Fatal("NUI", ">>>>>>>>> itemGroupRect: " + itemGroupRect.X + ", " + itemGroupRect.Y + ", " + itemGroupRect.Width + ", " + itemGroupRect.Height);
            Tizen.Log.Fatal("NUI", ">>>>>>>>> itemGroup: " + itemGroup.PositionX + ", " + itemGroup.PositionY + ", " + itemGroup.SizeWidth + ", " + itemGroup.SizeHeight);
            itemGroup.SizeWidth = itemGroupRect.Width;
            itemGroup.SizeHeight = itemGroupRect.Height;
        }

        private void InitItemGroupPosition()
        {
            if (GridType.Horizontal == gridType)
            {
                if (itemGroupRect.X > margin[0])
                {
                    itemGroupRect.X = margin[0];
                }
                else if (itemGroupRect.Right() < gridWidth)
                {
                    if (itemGroupRect.X >= gridWidth)
                    {
                        itemGroupRect.X = gridWidth - itemGroupRect.Width;
                    }
                    else
                    {
                        itemGroupRect.X = 0;
                    }
                }

                itemGroupRect.Y = margin[1];
            }
            else
            {
                itemGroupRect.X = margin[0];
                if (itemGroupRect.Y > margin[1])
                {
                    itemGroupRect.Y = margin[1];
                }
                else if (itemGroupRect.Y + itemGroupRect.Height < gridHeight)
                {
                    if (itemGroupRect.Y >= gridHeight)
                    {
                        itemGroupRect.Y = gridHeight - itemGroupRect.Height;
                    }
                    else
                    {
                        itemGroupRect.Y = 0;
                    }
                }
            }

            Tizen.Log.Fatal("NUI", ">>>>>>>>> itemGroupRect: " + itemGroupRect.X + ", " + itemGroupRect.Y + ", " + itemGroupRect.Width + ", " + itemGroupRect.Height);
            ResetItemGroupRectPos();

            itemGroup.PositionX = itemGroupRect.X;
            itemGroup.PositionY = itemGroupRect.Y;
        }

        private void ResetItemGroupRectPos()
        {
            //adjust item Group pos, to add margin when item group scroll to Head or Tail
            if (GridType.Horizontal == gridType)
            {
                if (itemGroupRect.X == 0)
                {
                    itemGroupRect.X += margin[0];
                }
                else if (itemGroupRect.Right() == gridWidth)
                {
                    itemGroupRect.X -= margin[2];
                }
            }
            else
            {
                if (itemGroupRect.Y == 0)
                {
                    itemGroupRect.Y += margin[1];
                }
                else if (itemGroupRect.Bottom() == gridHeight)
                {
                    itemGroupRect.Y -= margin[3];
                }
            }
        }

        private Cell GetCellByItemIndex(int groupIndex, int itemIndex)
        {
            Cell curCell = null;
            if (!IsGroupIndexValid(groupIndex))
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]Invalid group index"));
                return null;
            }

            Group curGroup = groupList.ElementAt(groupIndex);
            if (curGroup.itemIndexToCell.ContainsKey(itemIndex))
            {
                curCell = curGroup.itemIndexToCell[itemIndex];
            }

            return curCell;
        }

        private bool IsGroupIndexValid(int groupIndex)
        {
            return (groupIndex >= 0 && groupList != null && groupIndex < groupList.Count);
        }

        private bool IsItemIndexValid(int groupIndex, int itemIndex)
        {
            if (!IsGroupIndexValid(groupIndex))
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]Invalid group index"));
                return false;
            }

            Group curGroup = groupList.ElementAt(groupIndex);

            return (itemIndex >= 0 && curGroup.itemList != null && itemIndex < curGroup.itemList.Count);
        }

        private bool IsColumnIndexValid(int groupIndex, int columnIndex)
        {
            if (!IsGroupIndexValid(groupIndex))
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]Invalid group index"));
                return false;
            }

            Group curGroup = groupList.ElementAt(groupIndex);

            return (columnIndex >= 0 && curGroup.columnList != null && columnIndex < curGroup.columnList.Count);
        }

        private bool IsCellIndexValid(int groupIndex, int columnIndex, int cellIndex)
        {
            if (!IsColumnIndexValid(groupIndex, columnIndex))
            {
                Tizen.Log.Fatal("NUI", string.Format("[GridView]Invalid column index"));
                return false;
            }

            Column curColumn = groupList.ElementAt(groupIndex).columnList.ElementAt(columnIndex);

            return (cellIndex >= 0 && curColumn.cellList != null && cellIndex < curColumn.cellList.Count);
        }

        /// <summary>
        /// Called when the control gain key input focus.
        /// </summary>
        /// <param name="sender">the object</param>
        /// <param name="e">the args of the event</param>
        public void ControlFocusGained(object sender, EventArgs e)
        {
            OnFocusGained();
        }

        public void OnFocusGained()
        {
            Tizen.Log.Fatal("NUI", ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> ");
            SetFocus(focusGroupIndex, focusItemIndex);
        }

        /// <summary>
        /// Called when the control loses key input focus.
        /// </summary>
        /// <param name="sender">the object</param>
        /// <param name="e">the args of the event</param>
        public void ControlFocusLost(object sender, EventArgs e)
        {
            OnFocusLost();
        }

        public void OnFocusLost()
        {
            Tizen.Log.Fatal("NUI", "<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            NotifyFocusChange(focusGroupIndex, focusItemIndex, -1, -1);
        }

        private void OnFocusMoveViaEvent(object o, EventArgs e)
        {
            Tizen.Log.Fatal("NUI", "--------------------- " + focusMoveViaQ.Count + " -------------------------------");
            if (focusMoveViaQ.Count > 0)
            {
                {
                    FocusMoveViaObject obj = focusMoveViaQ.Dequeue();
                    {
                        UpdateInMemoryItems(itemGroup.Position);
                    }

                    {
                        //common focus notify focus change at destination of every via
                        NotifyFocusChange(focusGroupIndex, focusItemIndex, obj.toGroupIndex, obj.toItemIndex);
                    }
                }

                //viaMap1.Remove(args.ViaNode);
            }
        }

        private void OnDataChange(object o, GridBridge.DataChangeEventArgs e)
        {
            switch (e.ChangeType)
            {
                case GridBridge.DataChangeEventType.Add:
                    AddItem(e.param[0]);
                    break;
                case GridBridge.DataChangeEventType.Remove:
                    DeleteItem(e.param[0], e.param[1], e.param[2]);
                    break;
                case GridBridge.DataChangeEventType.Insert:
                    InsertItem(e.param[0], e.param[1]);
                    break;
                case GridBridge.DataChangeEventType.Clear:
                    Clear();
                    break;
                case GridBridge.DataChangeEventType.Reset:
                    ResetByData();
                    break;
                case GridBridge.DataChangeEventType.Load:
                    Load();
                    Group curGroup = groupList.ElementAt(e.param[0]);
                    for (int i = e.param[1]; i < gridBridge.GetItemCount(e.param[0]); i++)
                    {
                        if (i < curGroup.itemList.Count)
                        {
                            gridBridge.UpdateItem(e.param[0], i, curGroup.itemList.ElementAt(i).itemView);
                        }
                    }

                    if (focusItemIndex >= e.param[1] && focusItemIndex <= e.param[1] + e.param[2] - 1)
                    {
                        if (e.param[1] > 0)
                        {
                            SetFoucsItemIndex(e.param[1] - 1, focusGroupIndex);
                        }
                        else if (e.param[1] + e.param[2] < curGroup.itemList.Count)
                        {
                            SetFoucsItemIndex(e.param[1] + e.param[2], focusGroupIndex);
                        }
                    }
                    else if (focusItemIndex >= e.param[1] + e.param[2])
                    {
                        SetFoucsItemIndex(focusItemIndex - e.param[2], focusGroupIndex);
                    }

                    break;
                default:

                    break;
            }

            refXForFocusMove = refYForFocusMove = -1;
        }

        private void AddItem(int groupIndex)
        {
            int dataCount = gridBridge.GetItemCount(groupIndex);
            if (dataCount > groupList.ElementAt(groupIndex).realCellCount)
            {
                AddColumn(colW, groupIndex);
                int columnIndex = groupList.ElementAt(groupIndex).columnList.Count - 1;
                for (int i = 0; i < rowCnt; i++)
                {
                    AddRowToColumn(columnIndex, rowH, groupIndex);
                }
            }

            Load();
        }

        private void AddAll(int groupIndex, int count)
        {
            Load();
        }


        private void DeleteItem(int groupIndex, int fromItemIndex, int deleteItemNum)
        {
            Group curGroup = groupList.ElementAt(groupIndex);

            GridItem item = null;

            int toItemIndex = fromItemIndex + deleteItemNum - 1;

            int count = 0;

            for (int i = fromItemIndex; i < curGroup.itemList.Count; i++)
            {
                item = curGroup.itemList.ElementAt(i);
                if (curInMemoryItemSet.Contains(item))
                {
                    UnloadItem(item, true);
                    count++;
                }
            }

            for (int i = 0; i < count; i++)
            {
                curGroup.itemList.Remove(curGroup.itemList.ElementAt(curGroup.itemList.Count - 1));
            }
        }

        private void InsertItem(int groupIndex, int itemIndex)
        {
            Load();

            Group curGroup = groupList.ElementAt(groupIndex);
            for (int i = itemIndex; i < gridBridge.GetItemCount(groupIndex); i++)
            {
                if (i < curGroup.itemList.Count)
                {
                    gridBridge.UpdateItem(groupIndex, i, curGroup.itemList.ElementAt(i).itemView);
                }
            }
        }

        private void Clear()
        {
            for (int i = 0; i < groupList.Count; i++)
            {
                Clear(i);
            }

            ClearRecycleBin();
        }

        private void Clear(int groupIndex)
        {
            Tizen.Log.Fatal("NUI", "--------------------- " + groupIndex + " -------------------------------");
            Group curGroup = groupList.ElementAt(groupIndex);
            for (int i = 0; i < curGroup.itemList.Count; i++)
            {
                GridItem item = curGroup.itemList.ElementAt(i);
                if (curInMemoryItemSet.Contains(item))
                {
                    UnloadItem(item, false);
                }
            }

            curGroup.itemList.Clear();
        }

        private void ResetByData()
        {
            Tizen.Log.Fatal("NUI", "....");
            int groupCount = gridBridge.GetGroupCount();
            ResetGroup(groupCount);
            for (int i = 0; i < groupCount; i++)
            {
                Tizen.Log.Fatal("NUI", ".... group: " + i);
                int dataCount = gridBridge.GetItemCount(i);
                Tizen.Log.Fatal("NUI", ".... dataCount: " + dataCount + " rowCnt: " + rowCnt);
                int columnCount = (dataCount + rowCnt - 1) / rowCnt;
                Group group = groupList.ElementAt(i);
                Tizen.Log.Fatal("NUI", ".... columnCount: " + columnCount + " group.columnList.Count: " + group.columnList.Count + " group.itemIndexToCell.Count: " + group.itemIndexToCell.Count);
                if (columnCount > group.columnList.Count)
                {
                    for (int c = group.columnList.Count; c < columnCount; c++)
                    {
                        AddColumn(colW, i);
                        for (int r = 0; r < rowCnt; r++)
                        {
                            AddRowToColumn(c, rowH, i);
                        }
                    }
                }
                else if (columnCount < group.columnList.Count)
                {
                    float posOffset = -(group.columnList.Count - columnCount) * (colW + columnSpace) + ColumnSpace;

                    for (int c = columnCount; c < group.columnList.Count; c++)
                    {
                        for (int r = 0; r < rowCnt; r++)
                        {
                            int itemIndex = c * rowCnt + r;
                            group.itemIndexToCell.Remove(itemIndex);
                            group.realCellCount--;
                            Tizen.Log.Fatal("NUI", ".... itemIndexToCell.Remove: " + itemIndex);
                        }
                    }

                    group.columnList.RemoveRange(columnCount, group.columnList.Count - columnCount);
                    group.endPos = group.columnList.ElementAt(columnCount - 1).endPos;
                    for (int j = i + 1; j < groupList.Count; j++)
                    {
                        if (GridType.Horizontal == gridType && groupList.ElementAt(j).title != null)
                        {
                            groupList.ElementAt(j).title.PositionX += posOffset;
                        }

                        groupList.ElementAt(j).ResetPosition(posOffset);
                    }

                }

                Tizen.Log.Fatal("NUI", ".... group.columnList.Count: " + group.columnList.Count);
            }

            focusGroupIndex = 0;
            focusItemIndex = 0;
            Tizen.Log.Fatal("NUI", "....");
            Load();
            if (HasFocus() == true && curInMemoryItemSet.Count != 0)
            {
                SetFocus(0, 0);
            }
        }

        /*page length set to grid length*/
        private void PageMove(bool bPrev, bool bAni)
        {
            Rect visibleAreaOfItemGroup = new Rect(-itemGroupRect.X, -itemGroupRect.Y, gridWidth, gridHeight);

            if (GridType.Horizontal == gridType)
            {
                if (bPrev)
                {
                    visibleAreaOfItemGroup.X -= gridWidth;
                    if (0 > visibleAreaOfItemGroup.X)
                    {
                        visibleAreaOfItemGroup.X = 0;
                    }
                }
                else
                {
                    visibleAreaOfItemGroup.X += gridWidth;
                    if (-visibleAreaOfItemGroup.X + itemGroupRect.Width < gridWidth)
                    {
                        visibleAreaOfItemGroup.X = itemGroupRect.Width - gridWidth;
                    }
                }

                if (-itemGroupRect.X == visibleAreaOfItemGroup.X)
                {
                    return;
                }
            }
            else
            {
                if (bPrev)
                {
                    visibleAreaOfItemGroup.Y -= gridHeight;
                    if (0 > visibleAreaOfItemGroup.Y)
                    {
                        visibleAreaOfItemGroup.Y = 0;
                    }
                }
                else
                {
                    visibleAreaOfItemGroup.Y += gridHeight;
                    if (-visibleAreaOfItemGroup.Y + itemGroupRect.Height < gridHeight)
                    {
                        visibleAreaOfItemGroup.Y = itemGroupRect.Height - gridHeight;
                    }
                }

                if (-itemGroupRect.Y == visibleAreaOfItemGroup.Y)
                {
                    return;
                }
            }

            Range newRange = new Range(0, 0, 0, 0);
            GetOnscreenRange(visibleAreaOfItemGroup, ref newRange, true);

            Group curFocusedGroup = groupList.ElementAt(focusGroupIndex);
            Cell curFocusedCell = GetCellByItemIndex(focusGroupIndex, focusItemIndex);
            Column curFocusedColumn = curFocusedGroup.columnList.ElementAt(curFocusedCell.columnIndex);

            float focusStartLine = curFocusedColumn.startPos;
            float focusEndLine = curFocusedColumn.endPos;
            float focusMiddleLine = 0;

            if (newRange.startGroupInScrn >= 0 && newRange.endGroupInScrn >= 0)
            {
                if (GridType.Horizontal == gridType)
                {
                    focusStartLine = focusStartLine + itemGroupRect.X + visibleAreaOfItemGroup.X;
                    focusEndLine = focusEndLine + itemGroupRect.X + visibleAreaOfItemGroup.X;
                }
                else
                {
                    focusStartLine = focusStartLine + itemGroupRect.Y + visibleAreaOfItemGroup.Y;
                    focusEndLine = focusEndLine + itemGroupRect.Y + visibleAreaOfItemGroup.Y;
                }

                focusMiddleLine = focusStartLine + (focusEndLine - focusStartLine) / 2;

                int nextFocusGroup = -1; int nextFocusColumn = -1;
                bool bFound = false;
                for (int i = newRange.startGroupInScrn; i <= newRange.endGroupInScrn && bFound == false; i++)
                {
                    Group curGroup = groupList.ElementAt(i);

                    int startColumnIndex = (i == newRange.startGroupInScrn) ? newRange.startColumnInScrn : 0;
                    int endColumnIndex = (i == newRange.endGroupInScrn) ? newRange.endColumnInScrn : curGroup.columnList.Count - 1;

                    for (int j = startColumnIndex; j <= endColumnIndex; j++)
                    {
                        Column curColumn = curGroup.columnList.ElementAt(j);
                        if (curColumn.startPos <= focusMiddleLine && curColumn.endPos >= focusMiddleLine)
                        {
                            nextFocusGroup = i;
                            nextFocusColumn = j;
                            bFound = true;
                            break;
                        }
                    }

                    if (bFound == false && i == newRange.startGroupInScrn && focusMiddleLine < curGroup.columnList.ElementAt(startColumnIndex).startPos)
                    {
                        nextFocusGroup = i;
                        nextFocusColumn = startColumnIndex;
                        break;
                    }

                    else if (bFound == false && i == newRange.endGroupInScrn && focusMiddleLine > curGroup.columnList.ElementAt(endColumnIndex).endPos)
                    {
                        nextFocusGroup = i;
                        nextFocusColumn = endColumnIndex;
                        break;
                    }
                }

                if (nextFocusGroup >= 0 && nextFocusColumn >= 0)
                {
                    int nextFocusItem = -1;
                    GetNearestItem(curFocusedCell, nextFocusGroup, nextFocusColumn, ref nextFocusItem);

                    if (nextFocusItem >= 0)
                    {
                        while (gridBridge.IsItemEnabled(nextFocusGroup, nextFocusItem) == false)
                        {
                            nextFocusItem = GetNextColumnIndex(bPrev, nextFocusColumn, ref nextFocusGroup);
                        }

                        if (nextFocusItem >= 0)
                        {
                            Position newItemGroupPos = new Position(-visibleAreaOfItemGroup.X, -visibleAreaOfItemGroup.Y, 0);
                            SetFocus(nextFocusGroup, nextFocusItem, bAni, newItemGroupPos);
                        }
                    }
                }
            }
        }

        private int GetNextColumnIndex(bool bPrev, int curColumnIndex, ref int curGroupIndex)
        {
            if (bPrev == true)
            {
                if (curColumnIndex == 0)
                {
                    if (curGroupIndex == 0)
                    {
                        return -1;
                    }
                    else
                    {
                        curGroupIndex--;
                        return groupList.ElementAt(curGroupIndex).columnList.Count - 1;
                    }
                }
                else
                {
                    return curColumnIndex - 1;
                }
            }
            else
            {
                Group curGroup = groupList.ElementAt(curGroupIndex);
                if (curGroup.columnList.Count - 1 == curColumnIndex)
                {
                    if (groupList.Count - 1 == curGroupIndex)
                    {
                        return -1;
                    }
                    else
                    {
                        curGroupIndex++;
                        return 0;
                    }
                }
                else
                {
                    return curColumnIndex + 1;
                }
            }
        }

        private class GridItem
        {
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

            public int groupIndex;
            public int index;

            public View itemView;
            public Rect rect;

            private Animation scaleAni = null;
        }

        private class Cell
        {
            public int columnIndex;
            public int index;
            public int itemIndex;
            public int rowIndex;

            public Rect rect;
            public Cell()
            {
                rect = new Rect(0, 0, 0, 0);
            }
        }

        private class Column
        {
            public int groupIndex;
            public int index;

            public float space;
            public float startPos;
            public float endPos;

            public int startItemIndex;
            public int endItemIndex;

            public List<Cell> cellList;
        }

        private class Group
        {
            public int index;

            public float startPos;
            public float endPos;

            public int realCellCount;
            public int lastFilledColumnIndex;

            public List<Column> columnList;
            public List<GridItem> itemList;
            public Dictionary<int, Cell> itemIndexToCell;

            public void ResetPosition(float posOffset)
            {
                startPos += posOffset;
                endPos += posOffset;

                int count = columnList.Count;
                for (int i = 0; i < count; i++)
                {
                    Column column = columnList.ElementAt(i);
                    column.startPos += posOffset;
                    column.endPos += posOffset;

                    int cellCount = column.cellList.Count;
                    for (int j = 0; j < cellCount; j++)
                    {
                        column.cellList.ElementAt(j).rect.X += posOffset;
                    }
                }
            }

            public View title;
        }

        private class Range
        {
            public int startGroupIndex;
            public int startColumnIndex;

            public int endGroupIndex;
            public int endColumnIndex;

            public int startGroupInScrn;
            public int startColumnInScrn;

            public int endGroupInScrn;
            public int endColumnInScrn;

            public int startItemIndexInScrn;
            public int endItemIndexInScrn;

            public Range()
            {
                startGroupIndex = 0;
                startColumnIndex = 0;
                endGroupIndex = 0;
                endColumnIndex = 0;
            }

            public Range(int a, int b, int c, int d)
            {
                startGroupIndex = a;
                startColumnIndex = b;
                endGroupIndex = c;
                endColumnIndex = d;
            }
        }

        private class FocusMoveViaObject
        {
            public int toGroupIndex;
            public int toItemIndex;
        }

        const int INVALID_COUNT = -1;

        private List<Group> groupList;
        private GridBridge gridBridge;

        private View itemGroup;
        private Rect itemGroupRect;

        private Vector4 margin = Vector4.Zero;

        private float gridWidth;
        private float gridHeight;

        private int preloadFrontColumnSize = 1;
        private int preloadBackColumnSize = 1;

        //layout attributes
        private GridType gridType = GridType.Horizontal;
        private int groupSpace = 0;
        private int columnSpace = 0;
        private int rowSpace = 0;
        private int titleSpace = 0;

        private int rowCnt = 0;
        private int colCnt = 0;
        private int colW = 0;
        private int rowH = 0;

        //focus
        private int focusGroupIndex;
        private int focusItemIndex;

        private float refXForFocusMove;
        private float refYForFocusMove;

        private int focusTagGroupIndex;
        private int focusTagItemIndex;
        //animation
        LinkerAnimation focusMoveAni;

        private float focusInScaleFactor;

        private AnimationAttributes focusInScaleAnimationAttrs;

        private AnimationAttributes focusOutScaleAnimationAttrs;

        private int focusMoveAniDuration;

        private Queue<FocusMoveViaObject> focusMoveViaQ = new Queue<FocusMoveViaObject>();

        //for load
        private HashSet<GridItem> curInMemoryItemSet;
        private HashSet<GridItem> nextInMemoryItemSet;

        private Rect lastVisibleArea;

        private Range onScreenRange;

        //reuse
        private List<View>[] recycledViews;
        private int viewTypeCount;

        private EventHandler<GridEventArgs> gridEventHandlers;
    }

    /// <summary>
    /// The attributes of Animation.
    /// </summary>
    /// <code>
    /// AnimationAttributes attrs = new AnimationAttributes();
    /// attrs.BezierPoint1 = new  Vector2(0.21f, 2);
    /// attrs.BezierPoint2 = new Vector2(0.14f, 1);
    /// attrs.Duration = 1000;
    /// </code>
    public class AnimationAttributes
    {
        /// <summary>V1 on bezier curve</summary>
        public Vector2 BezierPoint1;
        /// <summary>V2 on bezier curve</summary>
        public Vector2 BezierPoint2;
        /// <summary>Animation duration</summary>
        public int Duration;

        /// <summary>
        /// Constructs a AnimationAttributes..
        /// </summary>
        public AnimationAttributes()
        {
        }

        /// <summary>
        /// Constructs a AnimationAttributes with curve style..
        /// </summary>
        /// <param name="curve">curve</param>
        public AnimationAttributes(string curve)
        {
        }

        /// <summary>
        /// Constructs a AnimationAttributes that is a copy of attrs..
        /// </summary>
        /// <param name="attrs">Construct Attributes</param>
        public AnimationAttributes(AnimationAttributes attrs = null)
        {
            if (attrs == null)
            {
                return;
            }

            if (attrs.BezierPoint1 != null)
            {
                BezierPoint1 = new Vector2(attrs.BezierPoint1[0], attrs.BezierPoint1[1]);
            }

            if (attrs.BezierPoint2 != null)
            {
                BezierPoint2 = new Vector2(attrs.BezierPoint2[0], attrs.BezierPoint2[1]);
            }

            Duration = attrs.Duration;
        }

        /// <summary> Clone a AnimationAttributes </summary>
        /// <returns> The AnimationAttributes copy</returns>
        public AnimationAttributes Clone()
        {
            return new AnimationAttributes(this);
        }
    }

    /// <summary>
    /// A GridBridge object acts as a pipeline between an BridgeView and the
    /// underlying data for that view. The Bridge provides access to the data items.
    /// The Bridge is also responsible for making a View for each item in the data set.
    /// </summary>
    public abstract class GridBridge
    {
        /// <summary>
        /// Event type of data change.
        /// </summary>
        public enum DataChangeEventType
        {
            Add,
            Remove,
            Insert,
            Clear,
            Load,
            Reset
        }

        /// <summary>
        /// Data change event args at event call back,
        /// user can get information about data change.
        /// </summary>
        /// <code>
        ///private void OnDataChange(object o, GridBridge.DataChangeEventArgs e)
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
        /// Event handler of data change event.
        /// </summary>
        /// <param name="sender">The object who sends the event.</param>
        /// <param name="e">The event arguments.</param>
        public delegate void EventHandler<DataChangeEventArgs>(object sender, DataChangeEventArgs e);

        /// <summary>
        /// Data change event handler, user can add/remove.
        /// </summary>
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

        /// <summary>
        /// Constructor to create a grid view bridge with multi-groups.
        /// </summary>
        /// <param name="objects">Data list of differet groups.</param>
        public GridBridge(List<List<object>> objects)
        {
            gridData = objects;
        }

        /// <summary>
        /// Simple constructor to create a grid view bridge with one group.
        /// </summary>
        /// <param name="objects">Data list of grid view.</param>
        public GridBridge(List<object> objects)
        {
            gridData = new List<List<object>>();
            gridData.Add(objects);
        }

        /// <summary>
        /// Create item View.
        /// </summary>
        /// <param name="groupIndex">Group index of item in grid view.</param>
        /// <param name="itemIndex">Item index of specified group of grid view.</param>
        /// <returns>View of specified item.</returns>
        public abstract View GetItemView(int groupIndex, int itemIndex);

        /// <summary>
        /// Create group title of specified group, need to be overrided.
        /// </summary>
        /// <param name="groupIndex">Group index of grid view.</param>
        /// <returns>Group title view.</returns>
        public virtual View GetGroupTitleView(int groupIndex)
        {
            return null;
        }

        /// <summary>
        /// Remove group title of specified group, need to be overrided.
        /// </summary>
        /// <param name="groupIndex">Group index of grid view.</param>
        /// <param name="groupTitle">Group title view.</param>
        public virtual void RemoveGroupTitleView(int groupIndex, View groupTitle)
        {

        }

        /// <summary>
        /// Update View that displays the data at the specified index in the data set when data change.
        /// </summary>
        /// <param name="groupIndex">The group index.</param>
        /// <param name="itemIndex">The item index.</param>
        /// <param name="view">A View that displays the data at the specified index in the data set.</param>
        public abstract void UpdateItem(int groupIndex, int itemIndex, View view);

        /// <summary>
        /// Update View that displays the data at the specified index in the data set when focus change.
        /// </summary>
        /// <param name="groupIndex">The group index.</param>
        /// <param name="itemIndex">The item index.</param>
        /// <param name="view">Item view of specified item.</param>
        /// <param name="FlagFocused">Focus in or focus out</param>
        /// <param name="bSelected">LinearView item selected or not.</param>
        public abstract void FocusChange(int groupIndex, int itemIndex, View view, bool FlagFocused, bool bSelected = false);

        /// <summary>
        /// Unload view at the specified index when item scroll out or deleted.
        /// </summary>
        /// <param name="groupIndex">The group index.</param>
        /// <param name="itemIndex">The item index.</param>
        /// <param name="view">Item view of specified item.</param>
        public abstract void UnloadItem(int groupIndex, int itemIndex, View view);

        /// <summary>
        /// Unload view according to the specified viewType.
        /// </summary>
        /// <param name="viewType"> Item type set by users.</param>
        /// <param name="view">A View that displays the data at the specified index in the data set.</param>
        public abstract void UnloadItemByViewType(int viewType, View view);

        /// <summary>
        /// Get the type of View that will be created by GetView for the specified item.
        /// </summary>
        /// <param name="groupIndex">The group index.</param>
        /// <param name="itemIndex">The item index.</param>
        /// <remarks>Integers must be in the range 0 to GetItemTypeCount() - 1.</remarks>
        /// <returns>Item type.</returns>
        public virtual int GetItemType(int groupIndex, int itemIndex)
        {
            return 0;
        }

        /// <summary>
        /// Get the number of view types.
        /// </summary>
        /// <returns>The count of view type.</returns>
        public virtual int GetItemTypeCount()
        {
            return 1;
        }

        /// <summary>
        /// Check whether item is enable or not.
        /// </summary>
        /// <param name="groupIndex">The group index.</param>
        /// <param name="itemIndex">The item index.</param>
        /// <returns>Item is enabled or disabled.</returns>
        public virtual bool IsItemEnabled(int groupIndex, int itemIndex)
        {
            return true;
        }

        /// <summary>
        /// Get the data at the specified index.
        /// </summary>
        /// <param name="itemIndex">The item index.</param>
        /// <param name="groupIndex">The group index.</param>
        /// <returns>The data at the specified index.</returns>
        public object GetData(int itemIndex, int groupIndex = 0)
        {
            object data = null;
            if (gridData != null && gridData.ElementAt(groupIndex) != null && itemIndex >= 0 && itemIndex < gridData.ElementAt(groupIndex).Count)
            {
                data = gridData.ElementAt(groupIndex).ElementAt(itemIndex);
            }

            return data;
        }

        /// <summary>
        /// Get data group count of the bridge.
        /// </summary>
        /// <returns>Group count of the data set.</returns>
        public int GetGroupCount()
        {
            int count = 0;
            if (gridData != null)
            {
                count = gridData.Count;
            }

            return count;
        }

        /// <summary>
        /// How many items are in the data set of specified group.
        /// </summary>
        /// <param name="groupIndex">The data group index.</param>
        /// <returns>Count of items of specified group.</returns>
        public int GetItemCount(int groupIndex = 0)
        {
            int count = 0;
            if (gridData != null && gridData.ElementAtOrDefault(groupIndex) != null)
            {
                count = gridData.ElementAt(groupIndex).Count;
            }

            return count;
        }

        /// <summary>
        /// How many items are in the data set of whole data list.
        /// </summary>
        /// <returns>Count of total items</returns>
        public int GetTotalCount()
        {
            int count = 0;
            if (gridData != null)
            {
                foreach (List<object> group in gridData)
                {
                    count += group.Count;
                }
            }

            return count;
        }

        /// <summary>
        /// Adds the specified data at the end of the List of specified group.
        /// </summary>
        /// <param name="data">The data to add at the end of the List.</param>
        /// <param name="groupIndex">The group index which need to add data.</param>
        /// <param name="sendEvent">Notify list to remove item or not.</param>
        public void Add(object data, int groupIndex = 0, bool sendEvent = true)
        {
            Debug.Assert(groupIndex < gridData.Count);

            gridData[groupIndex].Add(data);
            DataChangeEventArgs e = new DataChangeEventArgs();
            e.ChangeType = DataChangeEventType.Add;
            e.param[0] = groupIndex;
            e.data = data;
            OnDataChangeEvent(this, e, sendEvent);
        }

        /// <summary>
        /// Removes the specified data from the List of specified group.
        /// </summary>
        /// <param name="fromIndex">The from index of data to remove from the list.</param>
        /// <param name="removeNum">The remove number of data to remove from the list.</param>
        /// <param name="groupIndex">The group index which need to add data.</param>
        /// <param name="withAni">Remove with animation or not.</param>
        /// <param name="sendEvent">Notify list to remove item or not.</param>

        public void RemoveItems(int fromIndex, int removeNum, int groupIndex = 0, bool withAni = false, bool sendEvent = true)
        {
            Debug.Assert(groupIndex < gridData.Count);

            if (fromIndex + removeNum > gridData[groupIndex].Count)
            {
                return;
            }

            DataChangeEventArgs e = new DataChangeEventArgs();
            e.ChangeType = DataChangeEventType.Remove;
            e.param[0] = groupIndex;
            e.param[1] = fromIndex;
            e.param[2] = removeNum;
            e.param[3] = withAni ? 1 : 0;
            OnDataChangeEvent(this, e, sendEvent);
            gridData[groupIndex].RemoveRange(fromIndex, removeNum);
            e.ChangeType = DataChangeEventType.Load;
            OnDataChangeEvent(this, e, sendEvent);
        }

        /// <summary>
        /// Removes the specified data from the List of specified group.
        /// </summary>
        /// <param name="itemIndex">The from index of data to remove from the list.</param>
        /// <param name="groupIndex">The group index which need to add data.</param>
        /// <param name="withAni">Remove with animation or not.</param>
        /// <param name="sendEvent">Notify list to remove item or not.</param>
        public void Remove(int itemIndex, int groupIndex = 0, bool withAni = false, bool sendEvent = true)
        {
            if (itemIndex < 0 || itemIndex >= GetItemCount(groupIndex))
            {
                Tizen.Log.Fatal("NUI", "Error!! item index is invalid");
                return;
            }

            RemoveItems(itemIndex, 1, groupIndex, withAni, sendEvent);
        }

        /// <summary>
        /// Inserts the specified data at the specified index in the List of specified group.
        /// </summary>
        /// <param name="index">The index at which the data must be inserted.</param>
        /// <param name="data">The data to insert into the list.</param>
        /// <param name="groupIndex">The group index which need to insert data.</param>
        /// <param name="sendEvent">Notify list to remove item or not.</param>
        public void Insert(int index, object data, int groupIndex = 0, bool sendEvent = true)
        {
            Debug.Assert(groupIndex < gridData.Count);

            gridData[groupIndex].Insert(index, data);
            DataChangeEventArgs e = new DataChangeEventArgs();
            e.ChangeType = DataChangeEventType.Insert;
            e.data = data;
            e.param[0] = groupIndex;
            e.param[1] = index;
            OnDataChangeEvent(this, e, sendEvent);
        }

        /// <summary>
        /// Remove all elements from the list.
        /// </summary>
        /// <param name="sendEvent">Notify list to remove item or not.</param>
        public void Clear(bool sendEvent = true)
        {
            if (gridData != null)
            {
                DataChangeEventArgs e = new DataChangeEventArgs();
                e.ChangeType = DataChangeEventType.Clear;
                OnDataChangeEvent(this, e, sendEvent);
                gridData.Clear();
            }
        }

        /// <summary>
        /// Reset bridge, and Update LinearView with new bridge data.
        /// </summary>
        /// <param name="objects">Data list of differet groups.</param>
        /// <param name="sendEvent">Notify list to remove item or not.</param>
        public void Reset(List<List<object>> objects, bool sendEvent = true)
        {
            Clear();
            gridData = objects;
            DataChangeEventArgs e = new DataChangeEventArgs();
            e.ChangeType = DataChangeEventType.Reset;
            OnDataChangeEvent(this, e, sendEvent);
        }

        /// <summary>
        /// Reset bridge, and Update LinearView with new bridge data.
        /// </summary>
        /// <param name="objects">Data list of grid view.</param>
        /// <param name="sendEvent">Notify list to remove item or not.</param>
        public void Reset(List<object> objects, bool sendEvent = true)
        {
            Clear();
            gridData = new List<List<object>>();
            gridData.Add(objects);
            DataChangeEventArgs e = new DataChangeEventArgs();
            e.ChangeType = DataChangeEventType.Reset;
            OnDataChangeEvent(this, e, sendEvent);
        }

        /// <summary>
        /// Swap data of specified group.
        /// </summary>
        /// <param name="groupIndex">The group index which need to insert data.</param>
        /// <param name="from">First item index of the group.</param>
        /// <param name="to">Second item index of the group.</param>
        public void SwapGroupData(int groupIndex, int from, int to)
        {
            List<object> groupDataList = gridData.ElementAt(groupIndex);
            object fromData = groupDataList.ElementAt(from);
            groupDataList[from] = groupDataList[to];
            groupDataList[to] = fromData;
        }

        private void OnDataChangeEvent(object sender, DataChangeEventArgs e, bool sendEvent = true)
        {
            if (!sendEvent)
            {
                return;
            }

            if (dataChangeEventHandlers != null)
            {
                dataChangeEventHandlers(sender, e);
            }
        }

        private List<List<object>> gridData;
        private EventHandler<DataChangeEventArgs> dataChangeEventHandlers;
    }
}
