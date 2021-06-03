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
using Tizen.NUI.UIComponents;
using System;
using System.Collections.Generic;

namespace Tizen.NUI.MediaHub
{
    /// <summary>
    /// FootView
    /// </summary>
    public class FootView : View
    {

        private View parentView;
        private View buttonView;
        private View inforAreaView;
        private Dropdown filterBtn;
        private Dropdown sortBtn;
        private TextLabel titleText;

        private TextLabel[] informationTextWdg;
        private TextLabel[] informationLine;
        private List<object> Itemlist = new List<object>();
        private List<object> Itemlistmusic = new List<object>();

        public int FilterByLastIndex = 0;
        public int SortByLastIndex = 0;
        public string UpdateText = " ";

        private View mainView;

        /// <summary>
        /// Construct FootView
        /// </summary>
        public FootView()
        {   
            mainView = CommonResource.BGView;
            if (mainView != null)
            {
                parentView = mainView;
            }
        }

        /// <summary>
        /// Callback function on triggering event "CLEAR_INFOR", for cleaning foot view information
        /// </summary>
        /// <param name="o">object</param>
        private void OnClearInformation(object o)
        {
            informationTextWdg[0].Text = " ";
            informationTextWdg[1].Text = " ";
            informationTextWdg[2].Text = " ";
            informationLine[0].Text = " ";
            informationLine[1].Text = " ";
            titleText.Text = " ";
        }

        /// <summary>
        /// Update footview display information
        /// </summary>
        /// <param name="footModel">FootModel class, include the foot view  display info</param>
        public void Update(FootModel footModel)
        {
            informationTextWdg[0].Text = " ";
            informationTextWdg[1].Text = " ";
            informationTextWdg[2].Text = " ";
            informationLine[0].Text = " ";
            informationLine[1].Text = " ";
            titleText.Text = " ";

            int itemType = footModel.ItemType;
            titleText.Text = footModel.Title;

            string strSize, strFormat, strData;
            
            strSize = "Size: " + footModel.Size;
            strFormat = "Format: " + footModel.Format;

            strData = footModel.Data;

            Itemlistmusic.Clear();
            Itemlist.Clear();

            //There is no information for the upFolder
            if (itemType == ContentItemType.eItemUpFolder)
            {
                informationTextWdg[0].Text = " ";
                informationTextWdg[1].Text = " ";
                informationTextWdg[2].Text = " ";
                informationLine[0].Text = " ";
                informationLine[1].Text = " ";
                titleText.Text = " ";
            }

            if (itemType == ContentItemType.eItemFolder)
            {
                informationTextWdg[0].Text = strData;
                informationTextWdg[1].Text = " ";
                informationTextWdg[2].Text = " ";
                informationLine[0].Text = " ";
                informationLine[1].Text = " ";
            }

            if (itemType == ContentItemType.eItemPhoto || itemType == ContentItemType.eItemVideo)
            {
                if (strSize != null && strSize != "")
                {
                    Itemlist.Add(strSize);
                }

                if (strFormat != null && strFormat != "")
                {
                    Itemlist.Add(strFormat);
                }

                if (strData != null && strData != "")
                {
                    Itemlist.Add(strData);
                }

                for (int i = 0; i < Itemlist.Count; i++)
                {
                    informationTextWdg[i].Text = (string)Itemlist[i];

                    float width = informationTextWdg[0].NaturalSize2D.Width;

                    if (width > 230)
                    {
                        width = 220;
                    }

                    informationTextWdg[0].NaturalSize2D.Width = (int)width;
                    Tizen.Log.Fatal("NUI", " FootView Update informationTextWdg[0].GetNaturalSize().Width = " + 0 + " " + informationTextWdg[0].NaturalSize2D.Width);

                    if (i > 0)
                    {
                        if (informationTextWdg[i - 1].NaturalSize2D.Width > 230)
                        {
                            width = 220;
                        }
                        else
                        {
                            width = informationTextWdg[i - 1].NaturalSize2D.Width;
                        }

                        informationLine[i - 1].PositionX = width + informationTextWdg[i - 1].PositionX + 12;
                        informationLine[i - 1].PositionY = 80;
                        informationLine[i - 1].Text = "|";
                        informationTextWdg[i].PositionX = informationLine[i - 1].PositionX + 12;
                        informationTextWdg[i].PositionY = 80;
                        if (width > 230)
                        {
                            width = 230;
                        }
                    }
                }

            }

            if (itemType == ContentItemType.eItemMusic)
            {

                if (strSize != null && strSize != "")
                {
                    Itemlistmusic.Add(strSize);
                }

                if (strFormat != null && strFormat != "")
                {
                    Itemlistmusic.Add(strFormat);
                }

                if (strData != null && strData != "")
                {
                    Itemlistmusic.Add(strData);
                }

                for (int i = 0; i < Itemlistmusic.Count; i++)
                {
                    informationTextWdg[i].Text = (string)Itemlistmusic[i];

                    float width = informationTextWdg[i].NaturalSize2D.Width;

                    if (width > 230)
                    {
                        width = 220;
                    }

                    informationTextWdg[0].NaturalSize2D.Width = (int)width;
                    Tizen.Log.Fatal("NUI", " FootView Update  informationTextWdg[0].GetNaturalSize().Width = " + 0 + " " + informationTextWdg[0].NaturalSize2D.Width);
                    if (i > 0)
                    {
                        if (informationTextWdg[i - 1].NaturalSize2D.Width > 230)
                        {
                            width = 220;
                        }
                        else
                        {
                            width = informationTextWdg[i - 1].NaturalSize2D.Width;
                        }

                        informationLine[i - 1].PositionX = width + informationTextWdg[i - 1].PositionX + 12;
                        informationLine[i - 1].PositionY = 80;
                        informationLine[i - 1].Text = "|";
                        informationTextWdg[i].PositionX = informationLine[i - 1].PositionX + 12;
                        informationTextWdg[i].PositionY = 80;
                        if (width > 230)
                        {
                            width = 230;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get filter button object for other view
        /// </summary>
        /// <returns>button object</returns>
        public Dropdown GetFilterButton()
        {
            return this.filterBtn;
        }

        /// <summary>
        /// Get sort button for other view
        /// </summary>
        /// <returns>button object</returns>
        public Dropdown GetSortButton()
        {
            return this.sortBtn;
        }

        /// <summary>
        /// Render foot view
        /// </summary>
        public void RenderView()
        {
            CreateInformation();
            this.CreateBt();
        }

        /// <summary>
        /// Show foot view
        /// </summary>
        public void ShowView()
        {
            this.buttonView.Show();
            inforAreaView.Show();
            this.Show();
        }

        /// <summary>
        /// Hide foot view
        /// </summary>
        public void HideView()
        {
            this.buttonView.Hide();
            inforAreaView.Hide();
        }

        /// <summary>
        /// Create foot view information in inforAreaView, it includes item tile text,date, size, and info line 
        /// </summary>
        private void CreateInformation()
        {
            Tizen.Log.Fatal("NUI","[FootView]createInformation!");
            this.inforAreaView = new View();
            this.inforAreaView.Size2D = new Size2D(600, 170);
            this.inforAreaView.Position = new Position(0, 900, 0);
            this.inforAreaView.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            this.inforAreaView.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            this.parentView.Add(inforAreaView);

            titleText = new TextLabel();
            titleText.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            titleText.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            titleText.Position = new Position(80, 40, 0);
            titleText.Size2D = new Size2D(650, 40);
            //titleText.PointSize = 6;
            titleText.PointSize = DeviceCheck.PointSize6;
            titleText.TextColor = Color.White;
            titleText.FontFamily = "SamsungOneUI_400";
            titleText.VerticalAlignment = Tizen.NUI.VerticalAlignment.Center;
            this.inforAreaView.Add(titleText);

            informationTextWdg = new TextLabel[3];

            for (int i = 0; i < 3; i++)
            {
                informationTextWdg[i] = new TextLabel();

                informationTextWdg[i].ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
                informationTextWdg[i].PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
                if (i == 0)
                { 
                    informationTextWdg[i].Position = new Position(80, 80, 0);
                }

                informationTextWdg[i].Size2D = new Size2D(200, 34);
                informationTextWdg[i].TextColor = Color.White;
                informationTextWdg[i].VerticalAlignment = Tizen.NUI.VerticalAlignment.Center;
                informationTextWdg[i].FontFamily = "SamsungOneUI_400";
                //informationTextWdg[i].PointSize = 6.0f;
                informationTextWdg[i].PointSize = DeviceCheck.PointSize6;
                informationTextWdg[i].Text = " ";
                this.inforAreaView.Add(informationTextWdg[i]);
            }

            informationLine = new TextLabel[2];
            for (int i = 0; i < 2; i++)
            {
                informationLine[i] = new TextLabel();
                informationLine[i].ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
                informationLine[i].PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
                informationLine[i].TextColor = Color.White;
                informationLine[i].VerticalAlignment = Tizen.NUI.VerticalAlignment.Center;
                informationLine[i].FontFamily = "SamsungOneUI_400";
                //informationLine[i].PointSize = 6.0f;
                informationLine[i].PointSize = DeviceCheck.PointSize6;
                informationLine[i].Text = " ";
                this.inforAreaView.Add(informationLine[i]);
            }
        }

        /// <summary>
        /// Create buttons for USB device type
        /// </summary>
        private void CreateUSBButton()
        {
            //To Create a bg view
            Tizen.Log.Fatal("NUI", "[FootView CreateUSBButton!]");

            this.buttonView = new View();
            this.buttonView.Size2D = new Size2D(750, 170);
            this.buttonView.Position = new Position(1170, 940, 0);
            this.buttonView.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            this.buttonView.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            //buttonView.KeyEvent += onKeyEvent;

            this.parentView.Add(buttonView);

            #region Filter by button

            //add filter button
            this.filterBtn = new Dropdown();
            this.filterBtn.ButtonText = "FilterBy : ALL";
            this.filterBtn.Position = new Position(0, 20, 0);
            this.filterBtn.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            this.filterBtn.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            this.filterBtn.Size2D = new Size2D(300, 80);
            this.filterBtn.BackgroundColor = new Color(0.0f, 0.0f, 0.0f, 0.1f);
            this.filterBtn.Name = "FilterBy";
            this.filterBtn.Focusable = true;
            this.filterBtn.SetListSize = new Size2D(300, 350);
            this.filterBtn.ListItemHeight = 70;
            this.filterBtn.AddListData("ALL");
            this.filterBtn.AddListData("Photo");
            this.filterBtn.AddListData("Video");
            this.filterBtn.AddListData("Music");
            this.filterBtn.ItemSelectedEvent += OnButtonClicked;

            this.buttonView.Add(filterBtn);
            #endregion

            #region Sort by button

            //add sort button
            this.sortBtn = new Dropdown();
            this.sortBtn.ButtonText = "SortBy : Title";
            this.sortBtn.Position = new Position(350, 20, 0);
            this.sortBtn.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            this.sortBtn.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            this.sortBtn.Size2D = new Size2D(300, 80);
            this.sortBtn.BackgroundColor = new Color(0.0f, 0.0f, 0.0f, 0.1f);
            this.sortBtn.Name = "SortBy";
            this.sortBtn.Focusable = true;
            this.sortBtn.SetListSize = new Size2D(300, 190);
            this.sortBtn.ListItemHeight = 70;
            this.sortBtn.AddListData("Title");
            this.sortBtn.AddListData("Size");
            this.sortBtn.ItemSelectedEvent += OnButtonClicked;
            this.buttonView.Add(sortBtn);
            #endregion
        }

        /// <summary>
        /// Set focus rule for buttons in up/down/left/right event
        /// </summary>
        private void SetFocusRule()
        {
            this.filterBtn.DropDownButton.RightFocusableView = this.sortBtn.DropDownButton;
            this.sortBtn.DropDownButton.LeftFocusableView = this.filterBtn.DropDownButton;
        }

        /// <summary>
        /// Create buttons
        /// </summary>
        private void CreateBt()
        {
            CreateUSBButton();
            SetFocusRule();
        }


        /// <summary>
        /// Callback function on button clicked event
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">command event args</param>
        private void OnButtonClicked(object sender, Dropdown.ItemSelectedEventArgs e)
        {
            Dropdown button = sender as Dropdown;
            GridListView gridListView = CommonResource.MainViewInstance.GetGridListView();

            View contentBgView = CommonResource.MainViewInstance.GetContentBgView();
            if (button.Name == "FilterBy")
            {
                GridListView preGridList = gridListView;
                int index = e.SelectedItemIndex;
                switch (index)
                {
                    case 0:
                        if (this.sortBtn.ButtonText == "SortBy : Title")
                        {
                            //Create item for all resource and sort the content by title
                            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);
                            gridListView.CreateList(CommonResource.DataList);
                        }
                        else if (this.sortBtn.ButtonText == "SortBy : Size")
                        {
                            //Create item for all resource and sort the content by size
                            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);
                            gridListView.CreateList(CommonResource.SortedList);
                        }
                        
                        break;
                    case 1:
                        if (this.sortBtn.ButtonText == "SortBy : Title")
                        {
                            //Create item for photos and sort the content by title
                            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);
                            gridListView.CreateList(CommonResource.PhotoDataList);
                        }
                        else if (this.sortBtn.ButtonText == "SortBy : Size")
                        {
                            //Create item for photos and sort the content by size
                            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);
                            gridListView.CreateList(CommonResource.SortedPhotoList);
                        }

                        break;
                    case 2:
                        if (this.sortBtn.ButtonText == "SortBy : Title")
                        {
                            //Create item for videos and sort the content by title
                            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);
                            gridListView.CreateList(CommonResource.VideoDataList);

                        }
                        else if (this.sortBtn.ButtonText == "SortBy : Size")
                        {
                            //Create item for videos and sort the content by size
                            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);
                            gridListView.CreateList(CommonResource.SortedVideoList);

                        }

                        break;
                    case 3:
                        if (this.sortBtn.ButtonText == "SortBy : Title")
                        {
                            //Create item for musics and sort the content by title
                            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);
                            gridListView.CreateList(CommonResource.MusicDataList);

                        }
                        else if (this.sortBtn.ButtonText == "SortBy : Size")
                        {
                            //Create item for musics and sort the content by size
                            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);
                            gridListView.CreateList(CommonResource.SortedMusicList);
                        }

                        break;
                }

                CommonResource.MainViewInstance.SetGridListView(gridListView);
                if (preGridList != null)
                {
                    preGridList.Destroy();
                }
            }
            else if (button.Name == "SortBy")
            {
                int index = e.SelectedItemIndex;
                GridListView preGridList = gridListView;
                Tizen.Log.Fatal("NUI", "DropDown.ItemSelectedEvent, index = " + index);
                switch (index)
                {
                    case 0:
                        if (this.filterBtn.ButtonText == "FilterBy : ALL")
                        {
                            //Create item for all resource and sort the content by title
                            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);
                            gridListView.CreateList(CommonResource.DataList);
                        }
                        else if (this.filterBtn.ButtonText == "FilterBy : Photo")
                        {
                            //Create item for photos and sort the content by title
                            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);
                            gridListView.CreateList(CommonResource.PhotoDataList);
                        }
                        else if (this.filterBtn.ButtonText == "FilterBy : Video")
                        {
                            //Create item for videos and sort the content by title
                            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);
                            gridListView.CreateList(CommonResource.VideoDataList);
                        }
                        else if (this.filterBtn.ButtonText == "FilterBy : Music")
                        {
                            //Create item for musics and sort the content by title
                            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);
                            gridListView.CreateList(CommonResource.MusicDataList);
                        }

                        break;
                    case 1:
                        if (this.filterBtn.ButtonText == "FilterBy : ALL")
                        {
                            //Create item for all resources and sort the content by size
                            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);
                            gridListView.CreateList(CommonResource.SortedList);
                        }
                        else if (this.filterBtn.ButtonText == "FilterBy : Photo")
                        {
                            //Create item for photos and sort the content by size
                            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);
                            gridListView.CreateList(CommonResource.SortedPhotoList);
                        }
                        else if (this.filterBtn.ButtonText == "FilterBy : Video")
                        {
                            //Create item for videos and sort the content by size
                            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);
                            gridListView.CreateList(CommonResource.SortedVideoList);
                        }
                        else if (this.filterBtn.ButtonText == "FilterBy : Music")
                        {
                            //Create item for musics and sort the content by size
                            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);
                            gridListView.CreateList(CommonResource.SortedMusicList);
                        }

                        break;
                }

                CommonResource.MainViewInstance.SetGridListView(gridListView);
                if (preGridList != null)
                {
                    preGridList.Destroy();
                }
            }

            GridView gridView = gridListView.GetGridView();
            gridView.DownFocusableView = filterBtn;
            filterBtn.DropDownButton.UpFocusableView = gridView;
            sortBtn.DropDownButton.UpFocusableView = gridView;

        }

    }
}
