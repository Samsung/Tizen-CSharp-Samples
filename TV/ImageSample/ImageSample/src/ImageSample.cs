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
using System.Runtime.InteropServices;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;
using Tizen;

namespace ImageSample
{
    /// <summary>
    /// A sample of ImageView
    /// </summary>
    class ImageSample : NUIApplication
    {
        /// <summary>
        /// The list of sample
        /// </summary>
        public readonly static string[] samples = new string[]
        {
            "PixelAreaSample",
            "SvgSample",
            "GifSample",
            "NinePatchSample",
            "MaskSample",
            "FittingModeSample",
        };

        private Button[] buttons;
        private TableView tableView;
        private Vector3 TABLE_RELATIVE_SIZE = new Vector3(0.95f, 0.9f, 0.8f);
        const int BUTTON_PRESS_ANIMATION_TIME = 350;
        private IExample currentSample;
        private View backGroundView, mPressedView;

        private static string resource = "/home/owner/apps_rw/org.tizen.example.ImageSample/res";
        private string logo_path = resource + "/images/Logo-for-demo.png";
        private string json_file = resource + "/style/demo-theme.json";
        private string normalImagePath = resource + "/images/Button/btn_bg_25_25_25_95.9.png";
        private string focusImagePath = resource + "/images/Button/btn_bg_255_255_255_200.9.png";
        private string pressImagePath = resource + "/images/Button/btn_bg_0_129_198_100.9.png";

        private TextLabel guide;
        private uint numOfSamples = (uint)samples.Length;
        private uint currentRow = 0;
        private uint currentColumn = 0;
        private uint EXAMPLES_PER_ROW = 3;
        private uint ROWS_PER_PAGE = 2;
        //private TableView[] mpages;

        /// <summary>
        /// The constructor with null
        /// </summary>
        public ImageSample() : base()
        {
        }

        /// <summary>
        /// Overrides this method if want to handle behaviour.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        /// <summary>
        /// ImageView Sample Application initialisation.
        /// </summary>
        public void Initialize()
        {
            // Create Background view.
            backGroundView = CreateBackGroundView();
            View focusIndicator = new View();
            FocusManager.Instance.FocusIndicator = focusIndicator;
            guide = new TextLabel();
            guide.HorizontalAlignment = HorizontalAlignment.Center;
            guide.VerticalAlignment = VerticalAlignment.Center;
            guide.PositionUsesPivotPoint = true;
            guide.ParentOrigin = ParentOrigin.TopLeft;
            guide.PivotPoint = PivotPoint.TopLeft;
            guide.Size2D = new Size2D(1920, 96);
            guide.FontFamily = "Samsung One 600";
            guide.Position2D = new Position2D(0, 94);
            guide.MultiLine = false;
            //guide.PointSize = 15.0f;
            guide.PointSize = DeviceCheck.PointSize15;
            guide.Text = "Image Sample \n";
            guide.TextColor = Color.White;
            //guide.BackgroundColor = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);

            backGroundView.Add(guide);
            Populate();

            Window.Instance.KeyEvent += InstanceKey;
            Activate();
        }

        /// <summary>
        /// Create TableView, Create pushButtons and add them to tableView.
        /// </summary>
        private void Populate()
        {
            Vector2 stagesize = Window.Instance.Size;
            //mTotalPages = (numOfSamples + EXAMPLES_PER_ROW * ROWS_PER_PAGE - 1) / (EXAMPLES_PER_ROW * ROWS_PER_PAGE);
            tableView = new TableView(ROWS_PER_PAGE, EXAMPLES_PER_ROW);
            buttons = new Button[numOfSamples];

            tableView.PositionUsesPivotPoint = true;
            //tableView.BackgroundColor = Color.White;
            tableView.Size2D = new Size2D(1500, 400);
            tableView.PivotPoint = PivotPoint.TopLeft;
            tableView.ParentOrigin = ParentOrigin.TopLeft;
            tableView.Position2D = new Position2D(210, 390);

            backGroundView.Add(tableView);
            int iter = 0;
            ushort margin = 20;
            ushort topmargin = 30;
            float tileParentMultiplier = 1.0f / EXAMPLES_PER_ROW;
            for (uint row = 0; row < ROWS_PER_PAGE; row++)
            {
                for (uint column = 0; column < EXAMPLES_PER_ROW; column++)
                {
                    // Calculate the position of each button
                    Vector2 position = new Vector2(column / (EXAMPLES_PER_ROW - 1.0f), row / (EXAMPLES_PER_ROW - 1.0f));
                    buttons[iter] = CreateTile(samples[iter], samples[iter], new Vector3(tileParentMultiplier, tileParentMultiplier, 1.0f), position);
                    buttons[iter].Padding = new Extents(margin, margin, topmargin, topmargin);
                    tableView.AddChild(buttons[iter], new TableView.CellPosition(row, column));
                    iter++;
                    if (iter == numOfSamples)
                    {
                        break;
                    }
                }

                if (iter == numOfSamples)
                {
                    break;
                }
            }
        }

        Button CreateTile(string name, string title, Vector3 sizeMultiplier, Vector2 position)
        {
            Button button = CreateButton(name, title);
            button.HeightResizePolicy = ResizePolicyType.SizeRelativeToParent;
            button.WidthResizePolicy = ResizePolicyType.SizeRelativeToParent;
            button.SizeModeFactor = sizeMultiplier;
            button.Focusable = true;
            button.KeyEvent += DoTilePress;
            return button;
        }

        /// <summary>
        /// Create an Text visual.
        /// </summary>
        /// <param name="text">The text of the Text visual</param>
        /// <param name="color">The color of the text</param>
        /// <returns>return a map which contain the properties of the text visual</returns>
        private PropertyMap CreateTextVisual(string text, Color color)
        {
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            map.Add(TextVisualProperty.Text, new PropertyValue(text));
            map.Add(TextVisualProperty.TextColor, new PropertyValue(color));
            //map.Add(TextVisualProperty.PointSize, new PropertyValue(8.0f));
            map.Add(TextVisualProperty.PointSize, new PropertyValue(DeviceCheck.PointSize8));
            map.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
            map.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));
            map.Add(TextVisualProperty.FontFamily, new PropertyValue("Samsung One 400"));
            return map;
        }

        /// <summary>
        /// Create an Image visual.
        /// </summary>
        /// <param name="imagePath">The url of the image</param>
        /// <returns>return a map which contain the properties of the image visual</returns>
        private PropertyMap CreateImageVisual(string imagePath)
        {
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            map.Add(ImageVisualProperty.URL, new PropertyValue(imagePath));
            return map;
        }

        private Button CreateButton(string name, string text)
        {
            Button button = new Button();
            button.Focusable = true;
            button.Size2D = new Size2D(400, 80);
            button.Focusable = true;
            button.Name = name;
            button.BackgroundImage = normalImagePath;
            button.TextColor = Color.White;

            // Chang background Visual and Label when focus gained.
            button.FocusGained += (obj, e) =>
            {
                button.BackgroundImage = focusImagePath;
                button.TextColor = Color.Black;
            };

            // Chang background Visual and Label when focus lost.
            button.FocusLost += (obj, e) =>
            {
                button.BackgroundImage = normalImagePath;
                button.TextColor = Color.White;
            };

            // Chang background Visual when pressed.
            button.KeyEvent += (obj, ee) =>
            {
                if ("Return" == ee.Key.KeyPressedName)
                {
                    if (Key.StateType.Down == ee.Key.State)
                    {
                        button.BackgroundImage = pressImagePath;
                        Tizen.Log.Fatal("NUI", "Press in pushButton sample!!!!!!!!!!!!!!!!");
                    }
                    else if (Key.StateType.Up == ee.Key.State)
                    {
                        button.BackgroundImage = focusImagePath;
                        Tizen.Log.Fatal("NUI", "Release in pushButton sample!!!!!!!!!!!!!!!!");
                    }
                }

                return false;
            };
            return button;
        }

        /// <summary>
        /// Called by OnTilePressed and Accessibility to do the appropriate action.
        /// </summary>
        /// <param name="source">The view representing this tile.</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool DoTilePress(object source, View.KeyEventArgs e)
        {
            bool consumed = false;
            uint i = 0;
            View view = source as View;
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "Return")
                {
                    string name = view.Name;
                    for (i = 0; i < numOfSamples; i++)
                    {
                        if (samples[i] == name)
                        {
                            // Get the current selected view.
                            mPressedView = view;
                            consumed = true;
                            break;
                        }
                    }
                }
            }

            if (consumed)
            {
                Tizen.Log.Fatal("UISample", "MainSample index: " + i);
                currentColumn = (i % (ROWS_PER_PAGE * EXAMPLES_PER_ROW)) % EXAMPLES_PER_ROW;
                currentRow = (i % (ROWS_PER_PAGE * EXAMPLES_PER_ROW)) / EXAMPLES_PER_ROW;
                OnPressedAnimationFinished();
            }

            return false;
        }

        /// <summary>
        /// Create the background image
        /// </summary>
        /// <returns>background view</returns>
        private View CreateBackGroundView()
        {
            View view = new View();
            view.Name = "background";
            view.PositionUsesPivotPoint = true;
            view.PivotPoint = PivotPoint.TopLeft;
            view.ParentOrigin = ParentOrigin.TopLeft;
            view.HeightResizePolicy = ResizePolicyType.FillToParent;
            view.WidthResizePolicy = ResizePolicyType.FillToParent;
            return view;
        }

        private void OnPressedAnimationFinished()
        {
            //mPressedAnimation = null;
            if (mPressedView != null)
            {
                string name = mPressedView.Name;

                this.Deactivate();
                object item = Activator.CreateInstance(global::System.Type.GetType("ImageSample." + name));
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
                else
                {
                    Log.Error("Example", "FAILED : " + name);
                }

                mPressedView = null;
            }
        }

        /// <summary>
        /// Callback when have key pressed.
        /// </summary>
        /// <param name="sender">sender.</param>
        /// <param name="e">event</param>
        private void InstanceKey(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "XF86Back")
                {
                    Tizen.Log.Info("UISample", e.Key.KeyPressedName);
                    if (currentSample != null)
                    {
                        currentSample.Deactivate();
                        currentSample = null;
                        this.Activate();
                    }
                    else
                    {
                        this.Exit();
                    }
                }
            }
        }

        /// <summary>
        /// Show the image of this class.
        /// </summary>
        public void Activate()
        {
            Window.Instance.GetDefaultLayer().Add(backGroundView);
            FocusManager.Instance.SetCurrentFocusView(tableView.GetChildAt(new TableView.CellPosition(currentRow, currentColumn)));
        }

        /// <summary>
        /// Remove the image from Window.Instance.
        /// </summary>
        public void Deactivate()
        {
            Window.Instance.GetDefaultLayer().Remove(backGroundView);
        }
    }
}
