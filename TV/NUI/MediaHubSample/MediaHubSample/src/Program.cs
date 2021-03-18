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
    /// Program.
    /// </summary>
    public class Program : NUIApplication
    {
        private View bgView;
        private HeadView headView;
        private IExample currentSample;
        private bool isHomePageFlag = true;

        protected GridListView gridListView = null;

        private View contentBgView;
        private GridView gridView;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Program()
        {

        }

        /// <summary>
        /// Overrides the OnCreate function
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            OnInitialize();
        }

        /// <summary>
        /// Create the main page
        /// </summary>
        private void OnInitialize()
        {
            View focusIndicator = new View();
            focusIndicator.Opacity = 0.0f;
            FocusManager.Instance.FocusIndicator = focusIndicator;

            isHomePageFlag = true;
            CreateBGImage();
            CreateHeadView();
            CreateContentView();
            Window.Instance.KeyEvent += InstanceKey;
            Window.Instance.GetDefaultLayer().Add(bgView);
            Activate();

        }

        /// <summary>
        /// This Application will be exited when back key entered.
        /// </summary>
        /// <param name="source">Window.Instance</param>
        /// <param name="e">event</param>
        private void AppBack(object source, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "XF86Back")
                {
                    Tizen.Log.Fatal("MediaHub", "XF86Back");
                    this.Exit();
                }
            }
        }

        /// <summary>
        /// Create the background image.
        /// </summary>
        private void CreateBGImage()
        {
            bgView = new View();
            bgView.Size2D = new Size2D(1920, 1080);
            bgView.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            bgView.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            bgView.Position = new Position(0, 0, 0);

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
            contentBgView.Position = new Position(0, 150, 0);
            contentBgView.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            contentBgView.ParentOrigin = Tizen.NUI.PivotPoint.TopLeft;
            bgView.Add(contentBgView);
        }

        /// <summary>
        /// Create the head view.
        /// </summary>
        private void CreateHeadView()
        {
            headView = new HeadView(bgView);
            headView.RenderView();
            this.bgView.Add(headView);
        }

        /// <summary>
        /// Create the content.
        /// </summary>
        private void CreateContentView()
        {
            GridListView preGridList = gridListView;

            gridListView = new GridListView(contentBgView, ContentViewType.ALL, false);

            HomePageData data = new HomePageData();
            List<object> dataList = data.GetData();
            gridListView.CreateList(dataList);
            Tizen.Log.Fatal("NUI", "Create Grid for first Page!");
            gridView = gridListView.GetGridView();
            gridView.KeyEvent += DoTilePress;

            if (preGridList != null)
            {
                preGridList.Destroy();
            }
        }

        /// <summary>
        /// The call back for the KeyEvent of grid
        /// </summary>
        /// <param name="source">the object</param>
        /// <param name="e">the args of the event</param>
        /// <returns>return whether the key has been consumed</returns>
        private bool DoTilePress(object source, View.KeyEventArgs e)
        {
            GridView gridView = source as GridView;
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "Return")
                {
                    int focusedGroupIndex = 0;
                    int focusedItemIndex = 0;
                    gridView.GetFocusItemIndex(out focusedGroupIndex, out focusedItemIndex);
                    if (focusedItemIndex == 3)
                    {
                        //ShowMainPage
                        Tizen.Log.Fatal("NUI", " Item 3 Clicked!!!!!!!!!!!!!!");
                        Deactivate();
                        object item = Activator.CreateInstance(global::System.Type.GetType("Tizen.NUI.MediaHub." + "MainPage"));
                        if (item is IExample)
                        {
                            this.Deactivate();
                            global::System.GC.Collect();
                            global::System.GC.WaitForPendingFinalizers();

                            currentSample = item as IExample;
                            if (currentSample != null)
                            {
                                currentSample.Activate();
                            }
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Active
        /// </summary>
        public void Activate()
        {
            isHomePageFlag = true;
            Window.Instance.GetDefaultLayer().Add(bgView);
            FocusManager.Instance.SetCurrentFocusView(gridView);
            gridView.SetFocusItemIndex(0, 3);
        }

        /// <summary>
        /// Remove the image from Window.Instance.
        /// </summary>
        public void Deactivate()
        {
            isHomePageFlag = false;
            Window.Instance.GetDefaultLayer().Remove(bgView);
        }

        /// <summary>
        /// Callback when have key pressed.
        /// </summary>
        /// <param name = "sender" > sender.</ param >
        /// < param name="e">event</param>
        private void InstanceKey(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "XF86Back")
                {
                    Tizen.Log.Info("UISample", e.Key.KeyPressedName);
                    if (!isHomePageFlag)
                    {
                        this.Activate();
                    }
                    else
                    {
                        this.Exit();
                    }
                }
            }
        }
    }

    public class Application
    {
        [STAThread]
        static void Main(string[] args)
        {
            new Tizen.NUI.MediaHub.Program().Run(args);
        }
    }
}
