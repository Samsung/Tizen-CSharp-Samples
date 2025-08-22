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
    /// Channel List sample.
    /// </summary>
    public class ChannelListSample : NUIApplication
    {
        private Size2D windowSize; //window size
        private static string resourcesChannel = "/home/owner/apps_rw/org.tizen.example.ChannelList/res/images/channelList/"; // channel resource path.
        private static string resourcesPrinciple = "/home/owner/apps_rw/org.tizen.example.ChannelList/res/images/principle/"; // principle resource path.
        private string titleBackGroundImage = resourcesChannel + "channel_list_bg_ch_title.png"; //title background image.
        private string titleShadowImage = resourcesChannel + "channel_list_title_shadow.png"; // title show image.
        private string titleIconAll = resourcesChannel + "channel_list_title_icon_all.png"; // title icon all image.
        private string titleIconFavorite = resourcesChannel + "channel_list_title_icon_favorite.png"; // title icon favorite image.
        private string titleIconGenre = resourcesChannel + "channel_list_title_icon_genre.png"; //title icon genre image.
        private string titleIconSort = resourcesChannel + "channel_list_title_icon_sort.png"; // title icon sort image.
        private string subListShadow = resourcesChannel + "channel_list_shadow.png"; // sublist shadow image.
        private string editImage = resourcesPrinciple + "ICON/ICON_BLACK/SMALL/i_edit.png"; //edit image.
        private TableView contentLayout, listContentLayout, selectContentLayout; // main layout, list area layout, 
        private List listView, subListView, selectListView, genreListView;
        private View editBackGround1;
        private ImageView titleIcon, selectTitleIcon;
        private TextLabel titleText, selectTitleText;
        private int playIndex = -1;
        private int subSelectedIndex = 0, preSubSelectIndex = 0, selectViewIndex = -1, genreSelectIndex;
        private string playProgramIndex = "";

        protected override void OnCreate()
        {
            base.OnCreate();
            Window.Instance.BackgroundColor = new Color(0.38f, 0.46f, 0.54f, 1.0f);
            windowSize = Window.Instance.Size;
            Initialize();
        }

        private void Initialize()
        {
            Tizen.Log.Fatal("NUI.ChannelList", "Initialize...");
            // Create the main layout to place title area and list area.
            // ================
            // |  Title area  |
            // ----------------
            // |  list area   |
            // ================
            contentLayout = new TableView(2, 1);
            contentLayout.WidthResizePolicy = ResizePolicyType.FitToChildren;
            contentLayout.HeightResizePolicy = ResizePolicyType.FitToChildren;
            contentLayout.PositionUsesPivotPoint = true;
            contentLayout.PivotPoint = PivotPoint.TopRight;
            contentLayout.ParentOrigin = ParentOrigin.TopRight;
            contentLayout.Position2D = new Position2D((int)(windowSize.Width * 0.145833f), 0);
            for (uint i = 0; i < 2; i++)
            {
                contentLayout.SetFitHeight(i);
            }

            contentLayout.SetFitWidth(0);
            Window.Instance.GetDefaultLayer().Add(contentLayout);

            CreateTitle();
            CreateContent();
            CreateSelectContent();
            FocusManager.Instance.SetCurrentFocusView(listView);
            // Don't use default focus indicator
            FocusManager.Instance.FocusIndicator = new View();
            Window.Instance.KeyEvent += AppBack;
        }

        private void AppBack(object source, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "XF86Back")
                {
                    Tizen.Log.Fatal("NUI.ChannelList", "XF86Back");
                    this.Exit();
                }
            }
        }

        private void CreateTitle()
        {
            Tizen.Log.Fatal("NUI.ChannelList", "CreateTitle...");
            //Title_area W:0.385937 x H:0.112037
            // Title area,title background image
            ImageView titleBackGround = new ImageView(titleBackGroundImage);
            titleBackGround.SizeWidth = windowSize.Width * (0.323437f + 0.207812f);//0.385937f
            titleBackGround.SizeHeight = windowSize.Height * 0.112037f;
            contentLayout.AddChild(titleBackGround, new TableView.CellPosition(0, 0));

            //Title area, edit icon background:  0.061458 x 0.108333
            View editBackGround = new View();
            editBackGround.BackgroundColor = new Vector4(1, 1, 1, 0.09f);
            editBackGround.SizeWidth = windowSize.Width * 0.061458f;
            editBackGround.SizeHeight = windowSize.Height * 0.108333f;
            editBackGround.PositionUsesPivotPoint = true;
            editBackGround.PivotPoint = PivotPoint.TopLeft;
            editBackGround.ParentOrigin = ParentOrigin.TopLeft;
            editBackGround.Position = new Position(windowSize.Width * (0.323437f + 0.001041f), windowSize.Height * 0.001851f, 0.0f);
            titleBackGround.Add(editBackGround);

            //Title area, edit background 
            editBackGround1 = new View();
            editBackGround1.BackgroundColor = new Vector4(1, 1, 1, 0.09f);
            editBackGround1.SizeWidth = windowSize.Width * (0.207812f - 0.061458f - 0.001041f * 2);
            editBackGround1.SizeHeight = windowSize.Height * 0.108333f;
            editBackGround1.PositionUsesPivotPoint = true;
            editBackGround1.PivotPoint = PivotPoint.TopRight;
            editBackGround1.ParentOrigin = ParentOrigin.TopRight;
            editBackGround1.Position = new Position(-windowSize.Width * 0.001041f, windowSize.Height * 0.001851f, 0.0f);
            titleBackGround.Add(editBackGround1);
            editBackGround1.Hide();

            //edit icon: 0.019791  x 0.035185
            //Title area, edit icon
            ImageView edit = new ImageView(editImage);
            edit.SizeWidth = windowSize.Width * 0.019791f;
            edit.SizeHeight = windowSize.Height * 0.035185f;
            edit.PositionUsesPivotPoint = true;
            edit.PivotPoint = PivotPoint.Center;
            edit.ParentOrigin = ParentOrigin.Center;
            edit.Hide();
            editBackGround.Add(edit);

            //edit text
            //Title area, edit text
            TextLabel editText = new TextLabel();
            editText.SizeWidth = windowSize.Width * (0.207812f - 0.061458f - 0.001041f * 2);
            editText.SizeHeight = windowSize.Height * 0.047037f;//0.037037f
            editText.PositionUsesPivotPoint = true;
            editText.PivotPoint = PivotPoint.CenterLeft;
            editText.ParentOrigin = ParentOrigin.CenterLeft;
            editText.Position = new Position(windowSize.Width * 0.001041f, windowSize.Height * 0.001851f, 0.0f);
            editText.HorizontalAlignment = HorizontalAlignment.Begin;
            editText.VerticalAlignment = VerticalAlignment.Center;
            editText.TextColor = new Color(1, 1, 1, 1);
            //editText.PointSize = 8.0f;
            editText.PointSize = DeviceCheck.PointSize8;
            editText.FontFamily = "SamsungOne 300";
            editText.Text = "Select";
            editText.Hide();
            editBackGround1.Add(editText);

            //Title_icon image size: 0.041666 x 0.074074
            //Title area, title icon
            titleIcon = new ImageView(titleIconAll);
            titleIcon.SizeWidth = windowSize.Width * 0.041666f;
            titleIcon.SizeHeight = windowSize.Height * 0.074074f;
            titleIcon.PositionUsesPivotPoint = true;
            titleIcon.PivotPoint = PivotPoint.CenterLeft;
            titleIcon.ParentOrigin = ParentOrigin.CenterLeft;
            titleIcon.Position = new Position(windowSize.Width * 0.011458f, 0, 0);
            titleBackGround.Add(titleIcon);

            //Title_text size width: 0.322395 - 0.011458f - 0.041666f - 0.010416f * 2
            //Title area, title text
            titleText = new TextLabel("All Channels");
            titleText.SizeWidth = windowSize.Width * (0.385937f - 0.011458f - 0.041666f - 0.010416f * 2 - 0.063541f);
            titleText.SizeHeight = windowSize.Height * 0.074074f;
            titleText.PositionUsesPivotPoint = true;
            titleText.PivotPoint = PivotPoint.CenterLeft;
            titleText.ParentOrigin = ParentOrigin.CenterLeft;
            titleText.Position = new Position(windowSize.Width * (0.011458f + 0.041666f + 0.010416f), 0, 0);
            titleText.HorizontalAlignment = HorizontalAlignment.Begin;
            titleText.VerticalAlignment = VerticalAlignment.Center;
            //titleText.PointSize = 48.0f;
            titleText.PointSize = DeviceCheck.PointSize10;
            titleText.TextColor = new Vector4(1, 1, 1, 1);
            titleText.FontFamily = "SamsungOne 300";
            titleBackGround.Add(titleText);
        }

        private void CreateContent()
        {
            Tizen.Log.Fatal("NUI.ChannelList", "CreateContent...");
            //Create list content layout to place main list area and sublib area.
            // =====================================
            // |  main list area  |   Sublist area |
            // =====================================
            listContentLayout = new TableView(1, 2);
            listContentLayout.WidthResizePolicy = ResizePolicyType.FitToChildren;
            listContentLayout.HeightResizePolicy = ResizePolicyType.FitToChildren;
            for (uint i = 0; i < 2; i++)
            {
                listContentLayout.SetFitWidth(i);
            }

            listContentLayout.SetFitHeight(0);
            contentLayout.AddChild(listContentLayout, new TableView.CellPosition(1, 0));

            CreatelistView();
            CreateSubList();
        }

        private void CreatelistView()
        {
            Tizen.Log.Fatal("NUI.ChannelList", "CreatelistView...");
            // Create main list.
            listView = new List();
            listView.BackgroundColor = new Vector4(0, 8.0f / 255.0f, 12.0f / 255.0f, 0.95f);
            listView.Name = "All_Channel";
            listView.SizeWidth = windowSize.Width * 0.323437f;
            listView.SizeHeight = windowSize.Height * 0.887962f;
            listView.PreloadFrontItemSize = 1;
            listView.PreloadBackItemSize = 1;

            // Initial main list data set.
            List<object> dataList = new List<object>();
            int num = (new ListItemData(listView.Name, 0)).Num;

            for (int i = 0; i < num; i++)
            {
                ListItemData data = new ListItemData(listView.Name, i);
                dataList.Add(data);
            }

            // Create main list adapter.
            SampleListAdapter mAdapter = new SampleListAdapter(dataList);
            listView.SetAdapter(mAdapter);
            listView.Focusable = true;
            listView.KeyEvent += OnKeyPressed; // Add key event handler.
            listContentLayout.AddChild(listView, new TableView.CellPosition(0, 0));
        }

        private void CreateSubList()
        {
            Tizen.Log.Fatal("NUI.ChannelList", "CreateSubList...");
            // Create sublist.
            subListView = new List();
            subListView.BackgroundColor = new Vector4(8.0f / 255.0f, 12.0f / 255.0f, 15.0f / 255.0f, 0.95f);
            subListView.Name = "Sub_List";
            subListView.SizeWidth = windowSize.Width * 0.207812f;
            subListView.SizeHeight = windowSize.Height * 0.887962f;
            subListView.PreloadFrontItemSize = 1;
            subListView.PreloadBackItemSize = 1;

            //Initial sublist data set.
            List<object> dataList = new List<object>();
            int num = (new SubListData(0)).Num;
            for (int i = 0; i < num; i++)
            {
                SubListData data = new SubListData(i);
                dataList.Add(data);
            }

            //Crate sublist adapter.
            SubListAdapter mAdapter = new SubListAdapter(dataList);
            mAdapter.SelectedIndex = 0;
            subListView.SetAdapter(mAdapter);
            subListView.Focusable = true;
            subListView.KeyEvent += OnKeyPressed; //Add key event handler.
            listContentLayout.AddChild(subListView, new TableView.CellPosition(0, 1));

            // Add shadow image for sublist area.
            ImageView shadow = new ImageView(subListShadow);
            shadow.SizeWidth = windowSize.Width * 0.021875f;
            shadow.SizeHeight = windowSize.Height * 0.887962f;
            subListView.Add(shadow);
        }

        private void CreateSelectContent()
        {
            Tizen.Log.Fatal("NUI.ChannelList", "CreateSelectContent...");
            // Create selected layout, to show favorite list or genre list
            // ===================
            // |  Title area     |
            // -------------------
            // |   list area     |
            // ===================
            selectContentLayout = new TableView(2, 1);
            selectContentLayout.WidthResizePolicy = ResizePolicyType.FitToChildren;
            selectContentLayout.HeightResizePolicy = ResizePolicyType.FitToChildren;
            selectContentLayout.PositionUsesPivotPoint = true;
            selectContentLayout.PivotPoint = PivotPoint.TopRight;
            selectContentLayout.ParentOrigin = ParentOrigin.TopRight;
            for (uint i = 0; i < 2; i++)
            {
                selectContentLayout.SetFitHeight(i);
            }

            selectContentLayout.SetFitWidth(0);
            selectContentLayout.Position = new Position(windowSize.Width * 0.344270f, 0, 0);
            Window.Instance.GetDefaultLayer().Add(selectContentLayout);

            CreateSelectTitle();
            CreateGerneListView();
            CreateSelectListView();
        }

        private void CreateSelectTitle()
        {
            Tizen.Log.Fatal("NUI.ChannelList", "CreateSelectTitle...");

            //Selected, Title area, title background
            ImageView titleBackGround = new ImageView(titleBackGroundImage);
            titleBackGround.SizeWidth = windowSize.Width * 0.344270f;//0.385937f
            titleBackGround.SizeHeight = windowSize.Height * 0.112037f;
            selectContentLayout.AddChild(titleBackGround, new TableView.CellPosition(0, 0));

            //Selected, Title area, title icon
            selectTitleIcon = new ImageView(titleIconFavorite);
            selectTitleIcon.SizeWidth = windowSize.Width * 0.041666f;
            selectTitleIcon.SizeHeight = windowSize.Height * 0.074074f;
            selectTitleIcon.PositionUsesPivotPoint = true;
            selectTitleIcon.PivotPoint = PivotPoint.CenterLeft;
            selectTitleIcon.ParentOrigin = ParentOrigin.CenterLeft;
            selectTitleIcon.Position = new Position(windowSize.Width * 0.011458f, 0, 0);
            titleBackGround.Add(selectTitleIcon);

            //Selected, Title area, title text
            selectTitleText = new TextLabel("Favorite");
            selectTitleText.SizeWidth = windowSize.Width * (0.385937f - 0.011458f - 0.041666f - 0.010416f * 2 - 0.063451f);
            selectTitleText.SizeHeight = windowSize.Height * 0.074074f;
            selectTitleText.PositionUsesPivotPoint = true;
            selectTitleText.PivotPoint = PivotPoint.CenterLeft;
            selectTitleText.ParentOrigin = ParentOrigin.CenterLeft;
            selectTitleText.Position = new Position(windowSize.Width * (0.011458f + 0.041666f + 0.010416f), 0, 0);
            selectTitleText.HorizontalAlignment = HorizontalAlignment.Begin;
            selectTitleText.VerticalAlignment = VerticalAlignment.Center;
            //selectTitleText.PointSize = 48.0f;
            selectTitleText.PointSize = DeviceCheck.PointSize10;
            selectTitleText.TextColor = new Vector4(1, 1, 1, 1);
            selectTitleText.FontFamily = "SamsungOne 300";
            titleBackGround.Add(selectTitleText);
        }

        private void CreateGerneListView()
        {
            Tizen.Log.Fatal("NUI.ChannelList", "CreateGerneListView...");
            //Create genre list.
            genreListView = new List();
            genreListView.BackgroundColor = new Vector4(8.0f / 255.0f, 12.0f / 255.0f, 15.0f / 255.0f, 0.95f);
            genreListView.Name = "Genre";
            genreListView.SizeWidth = windowSize.Width * 0.344270f;
            genreListView.SizeHeight = windowSize.Height * 0.887962f;
            genreListView.PreloadFrontItemSize = 1;
            genreListView.PreloadBackItemSize = 1;

            //Initial genre list data set.
            List<object> dataList = new List<object>();
            int num = (new GenreListData(0)).Num;
            for (int i = 0; i < num; i++)
            {
                GenreListData data = new GenreListData(i);
                dataList.Add(data);
            }

            //Create genre list adapter.
            GenreListAdapter mAdapter = new GenreListAdapter(dataList);
            genreListView.SetAdapter(mAdapter);
            genreListView.Focusable = true;
            genreListView.KeyEvent += OnKeyPressed; // Add key event handler
            selectContentLayout.AddChild(genreListView, new TableView.CellPosition(1, 0));
        }

        private void CreateSelectListView()
        {
            Tizen.Log.Fatal("NUI.ChannelList", "CreateSelectListView...");
            // Create favorite list.
            selectListView = new List();
            selectListView.BackgroundColor = new Vector4(8.0f / 255.0f, 12.0f / 255.0f, 15.0f / 255.0f, 0.95f);
            selectListView.Name = "Favorite";
            selectListView.SizeWidth = windowSize.Width * 0.344270f;
            selectListView.SizeHeight = windowSize.Height * 0.887962f;
            selectListView.PreloadFrontItemSize = 1;
            selectListView.PreloadBackItemSize = 1;

            // Initial favorite list date set.
            List<object> dataList = new List<object>();
            int num = (new FavoriteListData(0)).Num;
            for (int i = 0; i < num; i++)
            {
                FavoriteListData data = new FavoriteListData(i);
                dataList.Add(data);
            }

            // Create favorite list adapter.
            SelectListAdapter mAdapter = new SelectListAdapter(dataList);
            selectListView.SetAdapter(mAdapter);
            selectListView.Focusable = true;
            selectListView.KeyEvent += OnKeyPressed; //Add key event handler.
            selectContentLayout.AddChild(selectListView, new TableView.CellPosition(1, 0));
        }

        private bool OnKeyPressed(object source, View.KeyEventArgs e)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "OnKeyPressed...KeyPressedName: " + e.Key.KeyPressedName);
            List listview = source as List;
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "Up")
                { // Handle up key event.
                    listview.MoveFocus(MoveDirection.Up);
                }
                else if (e.Key.KeyPressedName == "Down")
                { // Handle up key event.
                    listview.MoveFocus(MoveDirection.Down);
                }
                else if (e.Key.KeyPressedName == "Right")
                { // Handle right key event.
                    Tizen.Log.Fatal("NUI.ChannelList", "RightKeyPressed");
                    if (listview == listView)
                    {
                        Tizen.Log.Fatal("NUI.ChannelList", "RightKeyPressed listview == listView");
                        FocusManager.Instance.SetCurrentFocusView(subListView);
                        editBackGround1.Show();
                        Animation animation = new Animation(500);
                        animation.AnimateTo(contentLayout, "Position", new Position(0, 0, 0));
                        animation.Play();
                        animation.Finished += (obj, ee) =>
                        {
                            FocusManager.Instance.SetCurrentFocusView(subListView);
                        };

                        SubListAdapter mAdapter = subListView.GetAdapter() as SubListAdapter;
                        mAdapter.Open = false;
                        for (int i = 0; i < mAdapter.GetCount(); i++)
                        {
                            if ((subListView.GetLoadedItemView(i) as SubListItem) != null)
                            {
                                (subListView.GetLoadedItemView(i) as SubListItem).IconOpacity(true);
                            }
                        }
                    }
                }
                else if (e.Key.KeyPressedName == "Left" || e.Key.KeyPressedName == "BackSpace")
                { // Handle left key or BackSpace key event.
                    if (listview == subListView)
                    { // if sublist focused.
                        FocusManager.Instance.SetCurrentFocusView(listView);
                        Animation animation = new Animation(500);
                        animation.AnimateTo(contentLayout, "Position", new Position(windowSize.Width * 0.145833f, 0, 0));
                        animation.Play();
                        animation.Finished += (obj, ee) =>
                        {
                            editBackGround1.Hide();
                            FocusManager.Instance.SetCurrentFocusView(listView);
                        };

                        SubListAdapter mAdapter = subListView.GetAdapter() as SubListAdapter;
                        mAdapter.Open = true;
                        for (int i = 0; i < mAdapter.GetCount(); i++)
                        {
                            if (i == subSelectedIndex)
                            {
                                if ((listview.GetLoadedItemView(i) as SubListItem) != null)
                                {
                                    (listview.GetLoadedItemView(i) as SubListItem).IconOpacity(true);
                                }
                            }
                            else
                            {
                                if ((listview.GetLoadedItemView(i) as SubListItem) != null)
                                {
                                    (listview.GetLoadedItemView(i) as SubListItem).IconOpacity(false);
                                }
                            }
                        }
                    }
                    else if (listview == selectListView || listview == genreListView)
                    { //if favorite list or genre list focused
                        if (subListView.GetLoadedItemView(subSelectedIndex) != null)
                        {
                            (subListView.GetLoadedItemView(subSelectedIndex) as SubListItem).SelectedText(false);
                            (subListView.GetLoadedItemView(subSelectedIndex) as SubListItem).IconUrl = ((subListView.GetAdapter().GetData(subSelectedIndex)) as SubListData).IconN;
                        }

                        subSelectedIndex = preSubSelectIndex;
                        (subListView.GetLoadedItemView(subSelectedIndex) as SubListItem).SelectedText(true);
                        (subListView.GetLoadedItemView(subSelectedIndex) as SubListItem).IconUrl = ((subListView.GetAdapter().GetData(subSelectedIndex)) as SubListData).IconS;
                        (subListView.GetAdapter() as SubListAdapter).SelectedIndex = subSelectedIndex;

                        Animation animation = new Animation();
                        animation.AnimateTo(contentLayout, "colorAlpha", 1.0f, 0, 334);
                        animation.AnimateTo(selectContentLayout, "PositionX", windowSize.Width * 0.344270f, 0, 500);
                        animation.Play();

                        animation.Finished += (obj, ee) =>
                        {
                            FocusManager.Instance.SetCurrentFocusView(listView);
                        };
                    }
                }
                else if (e.Key.KeyPressedName == "Return")
                { //Handle Return event.
                    if (listview == listView && playIndex != listview.FocusItemIndex)
                    {
                        Tizen.Log.Fatal("NUI.ChannelList", "listview selected...");
                        if (listview.GetLoadedItemView(playIndex) != null)
                        {
                            (listview.GetLoadedItemView(playIndex) as ListItem).Play(false);
                        }

                        playIndex = listview.FocusItemIndex;
                        playProgramIndex = (listview.GetLoadedItemView(listview.FocusItemIndex) as ListItem).ProgramIndex;
                        (listview.GetLoadedItemView(listview.FocusItemIndex) as ListItem).Play(true);
                        (listview.GetAdapter() as SampleListAdapter).PlayProgramIndex = playProgramIndex;
                    }
                    else if (listview == subListView)
                    {
                        Tizen.Log.Fatal("NUI.ChannelList", "sublistview selected...");
                        Tizen.Log.Fatal("NUI.ChannelList", "subSelectedIndex: " + subSelectedIndex + ", FocusItemIndex: " + listview.FocusItemIndex);
                        if (subSelectedIndex != listview.FocusItemIndex)
                        {
                            if (listview.GetLoadedItemView(subSelectedIndex) != null)
                            {
                                (listview.GetLoadedItemView(subSelectedIndex) as SubListItem).SelectedText(false);
                                (listview.GetLoadedItemView(subSelectedIndex) as SubListItem).IconUrl = ((listview.GetAdapter().GetData(subSelectedIndex)) as SubListData).IconN;
                            }

                            subSelectedIndex = listview.FocusItemIndex;
                            (listview.GetLoadedItemView(subSelectedIndex) as SubListItem).SelectedText(true);
                            (listview.GetLoadedItemView(subSelectedIndex) as SubListItem).IconUrl = ((listview.GetAdapter().GetData(subSelectedIndex)) as SubListData).IconS;
                            (listview.GetAdapter() as SubListAdapter).SelectedIndex = subSelectedIndex;
                        }

                        SubListSelecteChanged(subSelectedIndex);
                    }
                    else if (listview == selectListView)
                    {
                        Tizen.Log.Fatal("NUI.ChannelList", "favoratelistview selected...");
                        //FavoriteSelect();
                    }
                    else if (listview == genreListView)
                    {
                        Tizen.Log.Fatal("NUI.ChannelList", "genrelistview selected...");
                        //GenreSelect();

                    }

                }
                else if (e.Key.KeyPressedName == "XF86Back")
                {
                    return false;
                }

            }

            return true;
        }

        private void FavoriteSelect()
        {
            Tizen.Log.Fatal("NUI.ChannelList", "FavoriteSelect...");
            string selectFavorite = (selectListView.GetLoadedItemView(selectListView.FocusItemIndex) as SelectListItem).FavoriteText;
            Animation animation = new Animation();
            animation.AnimateTo(contentLayout, "colorAlpha", 1.0f, 0, 334);
            animation.AnimateTo(selectContentLayout, "PositionX", windowSize.Width * 0.344270f, 0, 500);

            animation.Finished += (obj, ee) =>
            {
                FocusManager.Instance.SetCurrentFocusView(listView);
            };

            if (titleText.Text == selectFavorite)
            {
                animation.Play();
                return;
            }

            if (selectListView.GetLoadedItemView(selectViewIndex) != null)
            {
                (selectListView.GetLoadedItemView(selectViewIndex) as SelectListItem).Select(false);
            }

            if (selectListView.GetLoadedItemView(selectListView.FocusItemIndex) != null)
            {
                (selectListView.GetLoadedItemView(selectListView.FocusItemIndex) as SelectListItem).Select(true);
            }

            if (genreListView.GetLoadedItemView(genreSelectIndex) != null)
            {
                (genreListView.GetLoadedItemView(genreSelectIndex) as GenreListItem).Select(false);
            }

            genreSelectIndex = -1;
            selectViewIndex = selectListView.FocusItemIndex;

            preSubSelectIndex = subSelectedIndex;
            playIndex = -1;
            listView.Name = selectFavorite;
            List<object> dataList = new List<object>();
            int num = (new ListItemData(listView.Name, 0)).Num;

            for (int i = 0; i < num; i++)
            {
                ListItemData data = new ListItemData(listView.Name, i);
                dataList.Add(data);
                if (data.ProgramIndex == playProgramIndex)
                {
                    playIndex = i;
                }

            }

            SampleListAdapter mAdapter = new SampleListAdapter(dataList);
            mAdapter.PlayProgramIndex = playProgramIndex;
            listView.SetAdapter(mAdapter);

            titleIcon.ResourceUrl = titleIconFavorite;
            titleText.Text = selectFavorite;
            animation.Play();
        }

        private void GenreSelect()
        {
            Tizen.Log.Fatal("NUI.ChannelList", "GenreSelect...");
            string selectGenre = (genreListView.GetLoadedItemView(genreListView.FocusItemIndex) as GenreListItem).Text;
            Animation animation = new Animation();
            animation.AnimateTo(contentLayout, "colorAlpha", 1.0f, 0, 334);
            animation.AnimateTo(selectContentLayout, "PositionX", windowSize.Width * 0.344270f, 0, 500);

            animation.Finished += (obj, ee) =>
            {
                FocusManager.Instance.SetCurrentFocusView(listView);
            };

            if (titleText.Text == selectGenre)
            {
                animation.Play();
                return;
            }

            if (genreListView.GetLoadedItemView(genreSelectIndex) != null)
            {
                (genreListView.GetLoadedItemView(genreSelectIndex) as GenreListItem).Select(false);
            }

            if (genreListView.GetLoadedItemView(genreListView.FocusItemIndex) != null)
            {
                (genreListView.GetLoadedItemView(genreListView.FocusItemIndex) as GenreListItem).Select(true);
            }

            if (selectListView.GetLoadedItemView(selectViewIndex) != null)
            {
                (selectListView.GetLoadedItemView(selectViewIndex) as SelectListItem).Select(false);
            }

            selectViewIndex = -1;
            genreSelectIndex = genreListView.FocusItemIndex;

            preSubSelectIndex = subSelectedIndex;
            playIndex = -1;
            listView.Name = selectGenre;
            List<object> dataList = new List<object>();
            int num = (new ListItemData(listView.Name, 0)).Num;

            for (int i = 0; i < num; i++)
            {
                ListItemData data = new ListItemData(listView.Name, i);
                dataList.Add(data);
                if (data.ProgramIndex == playProgramIndex)
                {
                    playIndex = i;
                }

            }

            SampleListAdapter mAdapter = new SampleListAdapter(dataList);
            mAdapter.PlayProgramIndex = playProgramIndex;
            listView.SetAdapter(mAdapter);

            titleIcon.ResourceUrl = titleIconGenre;
            titleText.Text = selectGenre;
            animation.Play();
        }

        private void SubListSelecteChanged(int index)
        {
            Tizen.Log.Fatal("NUI.ChannelList", "SubListSelectChanged...");
            Tizen.Log.Fatal("NUI.ChannelList", "index: " + index + ", titleText.Text: " + titleText.Text);
            //All Channels
            if (index == 0 && titleText.Text != "All_Channel")
            {
                /*playIndex = -1;
                listView.Name = "All_Channel";
                List<object> dataList = new List<object>();
                int num = (new ListItemData(listView.Name, 0)).Num;

                if (selectListView.GetLoadedItemView(selectViewIndex) != null)
                {
                    (selectListView.GetLoadedItemView(selectViewIndex) as SelectListItem).Select(false);
                }
                selectViewIndex = -1;
                if (genreListView.GetLoadedItemView(genreSelectIndex) != null)
                {
                    (genreListView.GetLoadedItemView(genreSelectIndex) as GenreListItem).Select(false);
                }
                genreSelectIndex = -1;

                for (int i = 0; i < num; i++)
                {
                    ListItemData data = new ListItemData(listView.Name, i);
                    dataList.Add(data);
                    if (data.ProgramIndex == playProgramIndex)
                    {
                        playIndex = i;
                    }
                }
                SampleListAdapter mAdapter = new SampleListAdapter(dataList);
                mAdapter.PlayProgramIndex = playProgramIndex;
                listView.SetAdapter(mAdapter);

                titleIcon.ResourceUrl = titleIconAll;
                titleText.Text = "All Channels";
                */
                Animation animation = new Animation(500);
                animation.AnimateTo(contentLayout, "Position", new Position(windowSize.Width * 0.145833f, 0, 0));
                animation.Play();
                animation.Finished += (obj, ee) =>
                {
                    editBackGround1.Hide();
                    FocusManager.Instance.SetCurrentFocusView(listView);
                };
                return;
            }

            if (index == 1)
            {
                //Show selected list, favorite list
                Animation animation = new Animation();
                selectContentLayout.RemoveChildAt(new TableView.CellPosition(1, 0));
                selectContentLayout.AddChild(selectListView, new TableView.CellPosition(1, 0));
                selectTitleIcon.ResourceUrl = titleIconFavorite;
                selectTitleText.Text = "Favorite";
                selectContentLayout.RaiseToTop();
                animation.AnimateTo(contentLayout, "colorAlpha", 0.4f, 0, 334);
                animation.AnimateTo(contentLayout, "PositionX", windowSize.Width * 0.145833f, 0, 500);
                animation.AnimateTo(selectContentLayout, "PositionX", 0, 0, 500);
                animation.Play();

                animation.Finished += (obj, ee) =>
                {
                    FocusManager.Instance.SetCurrentFocusView(selectListView);
                };

                return;
            }

            if (index == 2)
            {
                // show selected list, genre list
                Animation animation = new Animation();
                selectContentLayout.RemoveChildAt(new TableView.CellPosition(1, 0));
                selectContentLayout.AddChild(genreListView, new TableView.CellPosition(1, 0));
                selectTitleIcon.ResourceUrl = titleIconGenre;
                selectTitleText.Text = "Genre";
                selectContentLayout.RaiseToTop();
                animation.AnimateTo(contentLayout, "colorAlpha", 0.4f, 0, 334);
                animation.AnimateTo(contentLayout, "PositionX", windowSize.Width * 0.145833f, 0, 500);
                animation.AnimateTo(selectContentLayout, "PositionX", 0, 0, 500);
                animation.Play();

                animation.Finished += (obj, ee) =>
                {
                    FocusManager.Instance.SetCurrentFocusView(genreListView);
                };

                return;
            }
        }
    }
}