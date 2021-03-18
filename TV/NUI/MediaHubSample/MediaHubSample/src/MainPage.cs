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
using Tizen.NUI.MediaHub;

namespace Tizen.NUI.MediaHub
{
    /// <summary>
    /// The main UI of the Media hub
    /// </summary>
    public class MainPage : IExample
    {
        private View bgView;
        private HeadView headView;
        private FootView footView;

        protected GridListView gridListView = null;

        private View contentBgView;
        private GridView gridView;

        //The photo image resource file path
        private string photoPath = CommonResource.GetLocalReosurceURL() + "mediaHub/Photos/";
        //The movie image resource file path
        private string MoviePath = CommonResource.GetLocalReosurceURL() + "mediaHub/Movies/";
        //The music image resource path
        private string MusicPath = CommonResource.GetLocalReosurceURL() + "mediaHub/Musics/";

        /// <summary>
        /// Get the footView
        /// </summary>
        /// <returns>return thr footView</returns>
        public FootView GetFootView()
        {
            return this.footView;
        }

        /// <summary>
        /// Get the bg view of the content.
        /// </summary>
        /// <returns>return the bg</returns>
        public View GetContentBgView()
        {
            return contentBgView;
        }

        /// <summary>
        /// Get the GridListView
        /// </summary>
        /// <returns>return the GridListView instance on the main page</returns>
        public GridListView GetGridListView()
        {
            return gridListView;
        }

        /// <summary>
        /// Reset the GridView on the mainPage
        /// </summary>
        /// <param name="view">the GridListView instance</param>
        public void SetGridListView(GridListView view)
        {
            gridListView = view;
        }

        /// <summary>
        /// Get the GridView
        /// </summary>
        /// <returns>return the GridView instance on the main page</returns>
        public GridView GetGridView()
        {
            return gridView;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainPage()
        {
            OnInitialize();
            CommonResource.MainViewInstance = this;
            Tizen.Log.Fatal("NUI", "Create MainPage");
        }

        /// <summary>
        /// OnInitialize.
        /// </summary>
        private void OnInitialize()
        {
            CreateBGImage();
            CreateHeadView();

            CreateFootView();
            CreateContentView();
            SetFocusRule();
            Activate();
            Tizen.Log.Fatal("NUI", "OnInitialize!!!!!!!!!!!!!!");
        }

        // <summary>
        /// Set focus rule for grid list items and footview buttons 
        /// </summary>
        private void SetFocusRule()
        {
            Tizen.Log.Fatal("NUI", "setFocusRule for GridView and FootView");
            if (gridView == null)
            {
                Tizen.Log.Fatal("NUI", "setFocusRule  gridList == null  just return");
                return;
            }

            gridView.DownFocusableView = footView.GetFilterButton();
            footView.GetFilterButton().DropDownButton.UpFocusableView = gridView;
            footView.GetSortButton().DropDownButton.UpFocusableView = gridView;
        }

        /// <summary>
        /// Create Background.
        /// </summary>
        private void CreateBGImage()
        {
            bgView = new View();
            bgView.Size2D = new Size2D(1920, 1080);
            //this.bgView.BackgroundColor = Color.Yellow;
            //bgView.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            //bgView.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            //bgView.Position = new Position(0, 0, 0);

            //CommonResource.BGView = bgView;
            //string mcBgPath = CommonResource.GetLocalReosurceURL() + "bg/mc_main_bg.png";
            //ImageView bgImage = new ImageView(mcBgPath);
            //bgImage.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            //bgImage.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            //bgImage.WidthResizePolicy = ResizePolicyType.FillToParent;
            //bgImage.HeightResizePolicy = ResizePolicyType.FillToParent;
            //bgView.Add(bgImage);
            bgView.BackgroundColor = Color.Black;
            contentBgView = new View();
            contentBgView.Size2D = new Size2D(1920, 710);
            contentBgView.Position = new Position(0, 200, 0);//150
            contentBgView.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            contentBgView.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            bgView.Add(contentBgView);

            Window.Instance.GetDefaultLayer().Add(bgView);
            CommonResource.BGView = bgView;
        }

        /// <summary>
        /// Create HeadView.
        /// </summary>
        private void CreateHeadView()
        {
            headView = new HeadView(bgView);
            headView.RenderView();
            this.bgView.Add(headView);
        }

        /// <summary>
        /// Create FootView.
        /// </summary>
        private void CreateFootView()
        {
            footView = new FootView();
            footView.RenderView();
            this.bgView.Add(footView);
            CommonResource.FootView = footView;
        }

        /// <summary>
        /// Create Content View.
        /// </summary>
        private void CreateContentView()
        {
            GridListView preGridList = gridListView;

            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);

            ResourceData data = new ResourceData();
            List<object> dataList = data.GetData();
            gridListView.CreateList(dataList);
            gridView = gridListView.GetGridView();
            FocusManager.Instance.SetCurrentFocusView(gridView);           

            if (preGridList != null)
            {
                preGridList.Destroy();
            }
        }

        public void Activate()
        {
            Window.Instance.GetDefaultLayer().Add(bgView);
            FocusManager.Instance.SetCurrentFocusView(gridView);
        }

        /// <summary>
        /// Remove the image from Window.Instance.
        /// </summary>
        public void Deactivate()
        {
            Window.Instance.GetDefaultLayer().Remove(bgView);
        }
    }

}

