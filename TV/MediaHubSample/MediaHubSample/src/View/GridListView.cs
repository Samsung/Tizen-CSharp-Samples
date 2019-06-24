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
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using System;

namespace Tizen.NUI.MediaHub
{
    /// <summary>
    /// GridListView
    /// </summary>
    public class GridListView
    {
        private View parentView;
        private GridView gridList;
        private GridListDataBridge bridge;
        private int viewType;
        protected TextLabel textContent;
        private bool isInEdit;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parentView">The parent view of the grid</param>
        /// <param name="viewType">The type</param>
        /// <param name="isInEdit">The flag show whether the grid is in edit state or not</param>
        public GridListView(View parentView, int viewType, bool isInEdit = false)
        {
            Tizen.Log.Fatal("NUI", "GridListView viewType:" + viewType);
            this.parentView = parentView;
            this.parentView.Focusable = true;
            this.viewType = viewType;
            this.isInEdit = isInEdit;
        }

        /// <summary>
        /// Hide the grid.
        /// </summary>
        public void Hide()
        {
            gridList.Hide();
        }

        /// <summary>
        /// Show the grid.
        /// </summary>
        public void Show()
        {
            gridList.Show();
        }

        /// <summary>
        /// Destory the grid
        /// </summary>
        public void Destroy()
        {
            parentView.Remove(gridList);
            gridList.Dispose();
            gridList = null;
        }

        /// <summary>
        /// Get the grid
        /// </summary>
        /// <returns>return thr grid instance</returns>
        public GridView GetGridView()
        {
            return gridList;
        }

        /// <summary>
        /// Create the grid.
        /// </summary>
        /// <param name="dataList">The data source</param>
        /// <param name="groupList">The group</param>
        public void CreateList(List<object> dataList, List<object> groupList = null)
        {
            Tizen.Log.Fatal("NUI", "[ListView]createList ");

            gridList = new GridView();
            gridList.Name = "ContentGridList";
            gridList.PivotPoint = PivotPoint.TopLeft;
            gridList.ParentOrigin = ParentOrigin.TopLeft;
            gridList.Position = new Position(0, 0, 0);
            gridList.Focusable = true;
            gridList.RowSpace = 28;
            gridList.ColumnSpace = 20;
            gridList.ClippingMode = ClippingModeType.Disabled;
            gridList.Margin = new Vector4(81, 68, 81, 38);
            gridList.Size2D = new Size2D(1920, 730);
            gridList.FocusInScaleFactor = 1.1f;
            gridList.FocusInScaleAnimationAttrs = new AnimationAttributes();
            gridList.FocusInScaleAnimationAttrs.BezierPoint1 = new Vector2(0.21f, 2);
            gridList.FocusInScaleAnimationAttrs.BezierPoint2 = new Vector2(0.14f, 1);
            gridList.FocusInScaleAnimationAttrs.Duration = 600;
            gridList.FocusOutScaleAnimationAttrs = new AnimationAttributes();
            gridList.FocusOutScaleAnimationAttrs.BezierPoint1 = new Vector2(0.19f, 1);
            gridList.FocusOutScaleAnimationAttrs.BezierPoint2 = new Vector2(0.22f, 1);
            gridList.FocusOutScaleAnimationAttrs.Duration = 850;


            if (groupList == null || groupList.Count == 0)
            {
                int count = dataList.Count;
                int col = count / 2;
                if (count % 2 != 0)
                {
                    col += 1;
                }

                gridList.AddRegularGrid(col, 2, 218, 298,0);
                bridge = new GridListDataBridge(dataList, this.viewType, isInEdit);
            }

            parentView.Add(gridList);

            gridList.KeyEvent += OnGridListViewKeyEventHandler;

            Tizen.Log.Fatal("NUI", "getGroups SetBridge");

            gridList.SetBridge(bridge);
        }

        /// <summary>
        /// Set the edit status
        /// </summary>
        /// <param name="status">the status</param>
        public void SetEditStatus(bool status)
        {
            if (bridge != null)
            {
                bridge.EditMode = true;
            }
        }

        /// <summary>
        /// The callback of KeyEvent.
        /// </summary>
        /// <param name="source">The sender object</param>
        /// <param name="e">The keyEvent args</param>
        /// <returns>return whether the key has been customed or not</returns>
        private bool OnGridListViewKeyEventHandler(object source, View.KeyEventArgs e)
        {
            Tizen.Log.Fatal("NUI", "[GridListView.cs@OnLinerKeyEventHandler]e.Key.State : " + e.Key.State + ";  e.Key.KeyPressedName : " + e.Key.KeyPressedName);
            bool ret = true;
            if (e.Key.State == Key.StateType.Down)
            {
                GridView view = source as GridView;

                switch (e.Key.KeyPressedName)
                {
                    case "Right":
                        view.Move(e.Key.KeyPressedName);
                        break;
                    case "Left":
                        view.Move(e.Key.KeyPressedName);
                        break;
                    case "Down":
                        int groupIndex;
                        int itemIndex;
                        //int allcount =view.GetItemCount(0);
                        view.GetFocusItemIndex(out groupIndex, out itemIndex);
                        int allcount = view.GetItemCount(groupIndex);
                        if (itemIndex % 2 != 0 || allcount == 1)
                        {
                            ret = false;
                        }
                        else
                        {
                            view.Move(e.Key.KeyPressedName);
                        }

                        break;
                    case "Up":
                        view.GetFocusItemIndex(out groupIndex, out itemIndex);
                        if (itemIndex % 2 != 1)
                        {
                            ret = false;
                        }
                        else
                        {
                            view.Move(e.Key.KeyPressedName);
                        }

                        break;
                    case "Return":
                        // Ok key
                        EventCallBack callBack = new EventCallBack();
                        callBack.OnItemPress(view);
                        break;
                    case "XF86Back":
                        // return key
                        ret = false;
                        break;
                    default:
                        ret = false;
                        break;
                }
            }

            return ret;
        }

    }
}