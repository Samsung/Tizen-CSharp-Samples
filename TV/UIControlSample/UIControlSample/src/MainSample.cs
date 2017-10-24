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
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;
using Tizen;

namespace UIControlSample
{
    /// <summary>
    /// A sample of UIControls
    /// </summary>
    class MainSample : NUIApplication
    {
        /// <summary>
        /// The list of sample
        /// </summary>
        public readonly static string[] samples = new string[]
        {
            "CheckBoxSample",
            "RadioButtonSample",
            "InputFieldSample",
            "PopupSample",
            "SliderSample",
            "ProgressBarSample",
            "ToggleSample",
            "ActivityIndicatorSample",
        };

        private Vector3 TABLE_RELATIVE_SIZE = new Vector3(0.95f, 0.9f, 0.8f);
        const int BUTTON_PRESS_ANIMATION_TIME = 350;
        private int flagBack = 0;
        private IExample currentSample;
        private ScrollView scrollView;
        private View backGroundView, mPressedView, defaultFocusIndicatorView;
        private ImageView logo;
        private View[] tile;
        private View[] tileBackGround;
        private static string resource = "/home/owner/apps_rw/org.tizen.example.UIControlSample/res";
        private string logo_path = resource + "/images/Logo-for-demo.png";
        private string json_file = resource + "/style/demo-theme.json";

        private Animation mPressedAnimation;
        private Animation mRotateAnimation;
        struct FocusEffect
        {
            public ImageView view;
            public Animation animation;
        };
        private FocusEffect[] mFocusEffect;
        private string fragmentShader =
            "  varying mediump vec2  vTexCoord;" +
            "  uniform lowp    vec4  uColor;" +
            "  uniform sampler2D     sTexture;" +
            "  uniform mediump vec3  uCustomPosition;" +
            "" +
            "  void main()" +
            "  {" +
            "    if( texture2D( sTexture, vTexCoord ).a <= 0.0001 )" +
            "    {" +
            "      discard;" +
            "    }" +
            "    mediump vec2 wrapTexCoord = vec2( ( vTexCoord.x / 4.0 ) + ( uCustomPosition.x / 4.0 ) + ( uCustomPosition.z / 2.0 ), vTexCoord.y / 4.0 );" +
            "    mediump vec4 color = texture2D( sTexture, wrapTexCoord );" +
            "    mediump float positionWeight = ( uCustomPosition.y + 0.3 ) * color.r * 2.0;" +
            "" +
            "    gl_FragColor = vec4( positionWeight, positionWeight, positionWeight, 0.9 ) * uColor + vec4( uColor.xyz, 0.0 );" +
            "  }";
        private uint mTotalPages = 1;
        private uint numOfSamples = (uint)samples.Length;
        private uint currentRow = 0;
        private uint currentColumn = 0;
        private uint EXAMPLES_PER_ROW = 3;
        private uint ROWS_PER_PAGE = 2;
        private TableView[] mpages;

        /// <summary>
        /// The constructor with null
        /// </summary>
        public MainSample() : base()
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
        /// UIControls Sample Application initialisation.
        /// </summary>
        public void Initialize()
        {
            // Create Background view.
            backGroundView = CreateBackGroundView();
            // Add logo
            logo = new ImageView(logo_path);
            logo.Name = "LOGO_IMAGE";
            logo.PivotPoint = PivotPoint.TopCenter;
            logo.ParentOrigin = new Position(0.5f, 0.1f, 0.5f);
            logo.HeightResizePolicy = ResizePolicyType.UseNaturalSize;
            logo.WidthResizePolicy = ResizePolicyType.UseNaturalSize;
            // The logo should appear on top of everything.
            logo.DrawMode = DrawModeType.Overlay2D;
            backGroundView.Add(logo);

            // Scrollview occupying the majority of the screen
            scrollView = new ScrollView();
            scrollView.PositionUsesPivotPoint = true;
            scrollView.PivotPoint = PivotPoint.BottomCenter;
            scrollView.ParentOrigin = new Vector3(0.5f, 1.0f - 0.05f, 0.5f);
            scrollView.WidthResizePolicy = ResizePolicyType.FillToParent;
            scrollView.HeightResizePolicy = ResizePolicyType.SizeRelativeToParent;
            scrollView.SizeModeFactor = new Vector3(0.0f, 0.6f, 0.0f);
            float buttonsPageMargin = (1.0f - TABLE_RELATIVE_SIZE.X) * 0.5f * Window.Instance.Size.Width;
            scrollView.Padding = new Vector4(buttonsPageMargin, buttonsPageMargin, 0.0f, 0.0f);
            scrollView.AxisAutoLockEnabled = true;
            backGroundView.Add(scrollView);

            // Add scroll view effect and setup constraints on pages.
            ApplyScrollViewEffect();

            // Add pages and tiles.
            Populate();

            // Set initial orientation
            uint degree = 0;
            Rotate(degree);

            Window.Instance.KeyEvent += InstanceKey;
            Tizen.Log.Fatal("NUI", "MainSample");
            defaultFocusIndicatorView = new ImageView();
            defaultFocusIndicatorView.HeightResizePolicy = ResizePolicyType.FillToParent;
            defaultFocusIndicatorView.WidthResizePolicy = ResizePolicyType.FillToParent;
            defaultFocusIndicatorView.PositionUsesPivotPoint = true;
            defaultFocusIndicatorView.PivotPoint = PivotPoint.Center;
            defaultFocusIndicatorView.ParentOrigin = ParentOrigin.Center;
            Tizen.Log.Fatal("NUI", "MainSample");
            Activate();
        }

        /// <summary>
        /// Creates and sets up the custom effect used for the keyboard (and mouse) focus.
        /// </summary>
        private void CreateFocusEffect()
        {
            mFocusEffect = new FocusEffect[2];
            // Loop to create both actors for the focus highlight effect.
            for (uint i = 0; i < 2; i++)
            {
                mFocusEffect[i].view = new ImageView("/home/owner/apps_rw/org.tizen.example.UIControlSample/res/images/tile-focus.9.png");
                mFocusEffect[i].view.PositionUsesPivotPoint = true;
                mFocusEffect[i].view.PivotPoint = PivotPoint.Center;
                mFocusEffect[i].view.ParentOrigin = ParentOrigin.Center;
                mFocusEffect[i].view.HeightResizePolicy = ResizePolicyType.FillToParent;
                mFocusEffect[i].view.WidthResizePolicy = ResizePolicyType.FillToParent;
                mFocusEffect[i].view.InheritScale = false;

                // Setup initial values pre-animation.
                mFocusEffect[i].view.Scale = new Vector3(1.0f, 1.0f, 1.0f);
                mFocusEffect[i].view.Opacity = 0.0f;

                // Create and setup the animation to do the following:
                //   1) Initial fade in over short period of time
                //   2) Zoom in (larger) and fade out simultaneously over longer period of time.
                mFocusEffect[i].animation = new Animation(1000);
                mFocusEffect[i].animation.AnimateTo(mFocusEffect[i].view, "colorAlpha", 0.7f, 0, 160, new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear));
                mFocusEffect[i].animation.AnimateTo(mFocusEffect[i].view, "scale", new Vector3(1.05f, 1.05f, 1.05f), 160, 840, new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear));
                mFocusEffect[i].animation.AnimateTo(mFocusEffect[i].view, "colorAlpha", 0.0f, 160, 840, new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear));
                mFocusEffect[i].animation.Looping = true;
            }

            // Parent the secondary effect from the primary.
            mFocusEffect[0].view.Add(mFocusEffect[1].view);

            // Play the animation on the 1st glow object.
            mFocusEffect[0].animation.Play();

            // Stagger the animation on the 2st glow object half way through.
            mFocusEffect[1].animation.PlayFrom(500);
            FocusManager.Instance.FocusIndicator = mFocusEffect[0].view;
        }

        /// <summary>
        /// Rotates RootActor orientation to that specified.
        /// </summary>
        /// <param name="degrees">The requested angle.</param>
        private void Rotate(uint degrees)
        {
            // Resize the root actor.
            Vector2 stageSize = Window.Instance.Size;
            Vector3 targetSize = new Vector3(stageSize.X, stageSize.Y, 1.0f);
            if (degrees == 90 || degrees == 270)
            {
                targetSize = new Vector3(stageSize.Y, stageSize.X, 1.0f);
            }

            if (mRotateAnimation)
            {
                mRotateAnimation.Stop();
                mRotateAnimation.Clear();
            }

            mRotateAnimation = new Animation(500);
            mRotateAnimation.AnimateTo(backGroundView, "Orientation", new Rotation(new Radian(new Degree(360 - degrees)), PositionAxis.Z), new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOut));
            mRotateAnimation.AnimateTo(backGroundView, "size", targetSize, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOut));
            mRotateAnimation.Play();
        }

        /// <summary>
        /// Callback when the keyboard focus is going to be changed.
        /// </summary>
        /// <param name="resource">FocusManager.Instance</param>
        /// <param name="e">event</param>
        /// <returns> The view to move the keyboard focus to.</returns>
        private View OnKeyboardPreFocusChange(object resource, FocusManager.PreFocusChangeEventArgs e)
        {
            View CurrentView = e.CurrentView;
            View nextView = e.ProposedView;

            if (!CurrentView && !nextView)
            {
                // Set the initial focus to the first tile in the current page should be focused.
                nextView = mpages[scrollView.GetCurrentPage()].GetChildAt(new TableView.CellPosition(0, 0));
            }
            else if (!nextView)
            {
                // ScrollView is being focused but nothing in the current page can be focused further
                // in the given direction. We should work out which page to scroll to next.
                uint currentPage = scrollView.GetCurrentPage();
                uint newPage = currentPage;
                if (e.Direction == View.FocusDirection.Left)
                {
                    newPage--;
                }
                else if (e.Direction == View.FocusDirection.Right)
                {
                    newPage++;
                }

                newPage = global::System.Math.Max(0, global::System.Math.Min(mTotalPages - 1, newPage));
                if (newPage == currentPage)
                {
                    if (e.Direction == View.FocusDirection.Left)
                    {
                        newPage = mTotalPages - 1;
                    }
                    else if (e.Direction == View.FocusDirection.Right)
                    {
                        newPage = 0;
                    }
                }

                // Scroll to the page in the given direction
                scrollView.ScrollTo(newPage);

                if (e.Direction == View.FocusDirection.Left)
                {
                    // Work out the cell position for the last tile
                    uint remainingExamples = numOfSamples - newPage * EXAMPLES_PER_ROW * ROWS_PER_PAGE;
                    currentRow = (remainingExamples >= EXAMPLES_PER_ROW * ROWS_PER_PAGE) ? ROWS_PER_PAGE - 1 : ((remainingExamples % (EXAMPLES_PER_ROW * ROWS_PER_PAGE) + EXAMPLES_PER_ROW) / EXAMPLES_PER_ROW - 1);
                    currentColumn = remainingExamples >= EXAMPLES_PER_ROW * ROWS_PER_PAGE ? EXAMPLES_PER_ROW - 1 : (remainingExamples % (EXAMPLES_PER_ROW * ROWS_PER_PAGE) - currentRow * EXAMPLES_PER_ROW - 1);

                    // Move the focus to the last tile in the new page.
                    nextView = mpages[newPage].GetChildAt(new TableView.CellPosition(currentRow, currentColumn));
                }
                else
                {
                    // Move the focus to the first tile in the new page.
                    nextView = mpages[newPage].GetChildAt(new TableView.CellPosition(0, 0));
                }
            }

            return nextView;
        }

        /// <summary>
        /// Populates the contents (ScrollView) with all the
        /// Examples that have been Added using the AddExample(...)
        /// call
        /// </summary>
        private void Populate()
        {
            Vector2 stagesize = Window.Instance.Size;
            mTotalPages = (numOfSamples + EXAMPLES_PER_ROW * ROWS_PER_PAGE - 1) / (EXAMPLES_PER_ROW * ROWS_PER_PAGE);
            mpages = new TableView[mTotalPages];
            tile = new View[numOfSamples];
            tileBackGround = new View[numOfSamples];

            int iter = 0;
            // Calculate the number of images going across (columns) within a page,
            // according to the screen resolution and dpi.
            float margin = 2.0f;
            float tileParentMultiplier = 1.0f / EXAMPLES_PER_ROW;
            for (int t = 0; t < mTotalPages; t++)
            {
                // Create Table
                TableView page = new TableView(ROWS_PER_PAGE, EXAMPLES_PER_ROW);
                page.PositionUsesPivotPoint = true;
                page.PivotPoint = PivotPoint.Center;
                page.ParentOrigin = ParentOrigin.Center;
                page.HeightResizePolicy = ResizePolicyType.FillToParent;
                page.WidthResizePolicy = ResizePolicyType.FillToParent;
                page.Position2D = new Position2D((int)stagesize.Width * t, 0);
                scrollView.Add(page);

                for (uint row = 0; row < ROWS_PER_PAGE; row++)
                {
                    for (uint column = 0; column < EXAMPLES_PER_ROW; column++)
                    {
                        // Calculate the tiles relative pmScrollingosition
                        // on the page (between 0 - 1 in each dimension).
                        Vector2 position = new Vector2(column / (EXAMPLES_PER_ROW - 1.0f), row / (EXAMPLES_PER_ROW - 1.0f));
                        tile[iter] = CreateTile(samples[iter], samples[iter], new Vector3(tileParentMultiplier, tileParentMultiplier, 1.0f), position);
                        tile[iter].Padding = new Vector4(margin, margin, margin, margin);
                        tileBackGround[iter] = tile[iter].GetChildAt(0);
                        page.AddChild(tile[iter], new TableView.CellPosition(row, column));
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

                mpages[t] = page;
                if (iter == numOfSamples)
                {
                    break;
                }
            }

            // Update Ruler info.
            PropertyMap rulerMap = new PropertyMap();
            rulerMap.Add((int)ScrollModeType.XAxisScrollEnabled, new PropertyValue(true));
            rulerMap.Add((int)ScrollModeType.XAxisSnapToInterval, new PropertyValue(Window.Instance.Size.Width));
            rulerMap.Add((int)ScrollModeType.XAxisScrollBoundary, new PropertyValue(Window.Instance.Size.Width * mTotalPages));
            rulerMap.Add((int)ScrollModeType.YAxisScrollEnabled, new PropertyValue(false));
            scrollView.ScrollMode = rulerMap;
        }

        /// <summary>
        /// Creates a tile for the main menu.
        /// </summary>
        /// <param name="name">The unique name for this Tile</param>
        /// <param name="title"> The text caption that appears on the Tile</param>
        /// <param name="sizeMultiplier">ile's parent size.</param>
        /// <param name="position">The tiles relative position within a page</param>
        /// <returns>
        /// The view for the created tile.
        /// </returns>
        View CreateTile(string name, string title, Vector3 sizeMultiplier, Vector2 position)
        {
            View focusableTile = new View();
            focusableTile.StyleName = "DemoTile";
            focusableTile.HeightResizePolicy = ResizePolicyType.SizeRelativeToParent;
            focusableTile.WidthResizePolicy = ResizePolicyType.SizeRelativeToParent;
            focusableTile.SizeModeFactor = sizeMultiplier;
            focusableTile.Name = name;

            // Set the tile to be keyboard focusable
            focusableTile.Focusable = true;

            PropertyMap normalMap = new PropertyMap();
            normalMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            normalMap.Add(ImageVisualProperty.URL, new PropertyValue(resource + "/images/demo-tile-texture.9.png"));
            normalMap.Add(Visual.Property.Shader, new PropertyValue(Visual.ShaderProperty.FragmentShader));
            normalMap.Add(Visual.Property.MixColor, new PropertyValue(new Vector4(0.4f, 0.6f, 0.9f, 0.6f)));
            PropertyMap fragmentShaderMap = new PropertyMap();
            fragmentShaderMap.Add(Visual.ShaderProperty.FragmentShader, new PropertyValue(fragmentShader));
            normalMap.Add(Visual.Property.Shader, new PropertyValue(fragmentShaderMap));
            focusableTile.Background = normalMap;

            // Create an ImageView for the 9-patch border around the tile.
            ImageView borderImage = new ImageView();
            borderImage.StyleName = "DemoTileBorder";
            borderImage.PositionUsesPivotPoint = true;
            borderImage.PivotPoint = PivotPoint.Center;
            borderImage.ParentOrigin = ParentOrigin.Center;
            borderImage.HeightResizePolicy = ResizePolicyType.FillToParent;
            borderImage.WidthResizePolicy = ResizePolicyType.FillToParent;
            borderImage.Opacity = 0.8f;
            focusableTile.Add(borderImage);

            TextLabel label = new TextLabel();
            label.PositionUsesPivotPoint = true;
            label.PivotPoint = PivotPoint.Center;
            label.ParentOrigin = ParentOrigin.Center;
            label.StyleName = "LauncherLabel";
            label.MultiLine = true;
            label.Text = title;
            label.PointSize = 5.0f;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.HeightResizePolicy = ResizePolicyType.FillToParent;
            label.WidthResizePolicy = ResizePolicyType.FillToParent;
            // Pad around the label as its size is the same as the 9-patch border.
            // It will overlap it without padding.
            label.Padding = new Vector4(8.0f, 8.0f, 8.0f, 8.0f);
            focusableTile.Add(label);

            focusableTile.KeyEvent += DoTilePress;

            return focusableTile;
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
                currentColumn = (i % (ROWS_PER_PAGE * EXAMPLES_PER_ROW)) % ROWS_PER_PAGE;
                currentRow = (i % (ROWS_PER_PAGE * EXAMPLES_PER_ROW)) / ROWS_PER_PAGE;
                mPressedAnimation = new Animation(BUTTON_PRESS_ANIMATION_TIME);
                mPressedAnimation.EndAction = Animation.EndActions.Discard;

                // scale the content actor within the Tile,
                // as to not affect the placement within the Table.
                View content = tileBackGround[i];
                mPressedAnimation.AnimateTo(content, "scale", new Vector3(0.7f, 0.7f, 1.0f), 0, BUTTON_PRESS_ANIMATION_TIME / 2, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseInOut));
                mPressedAnimation.AnimateTo(content, "scale", Vector3.One, BUTTON_PRESS_ANIMATION_TIME / 2, BUTTON_PRESS_ANIMATION_TIME, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseInOut));

                // Rotate button on the Y axis when GetChildAtpressed.
                mPressedAnimation.AnimateTo(content, "Orientation", new Rotation(new Radian(new Degree(0.0f)), PositionAxis.Y));
                mPressedAnimation.AnimateTo(content, "Orientation", new Rotation(new Radian(new Degree(180.0f)), PositionAxis.Y), 0, BUTTON_PRESS_ANIMATION_TIME / 2);
                mPressedAnimation.AnimateTo(content, "Orientation", new Rotation(new Radian(new Degree(0.0f)), PositionAxis.Y), BUTTON_PRESS_ANIMATION_TIME / 2, BUTTON_PRESS_ANIMATION_TIME);

                mPressedAnimation.Play();
                mPressedAnimation.Finished += OnPressedAnimationFinished;
            }

            return false;
        }

        /// <summary>
        /// Setup the effect on the scroll view
        /// </summary>
        void ApplyScrollViewEffect()
        {
            scrollView.SetScrollSnapDuration(0.66f);
            scrollView.SetScrollFlickDuration(0.5f);
            scrollView.SetScrollSnapAlphaFunction(new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOut));
            scrollView.SetScrollFlickAlphaFunction(new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOut));
            scrollView.SetWrapMode(true);
        }

        /// <summary>
        /// Create the background image
        /// </summary>
        /// <returns>background view</returns>
        private View CreateBackGroundView()
        {
            View view = new View();
            view.Name = "background";
            view.StyleName = "LauncherBackground";
            view.PositionUsesPivotPoint = true;
            view.PivotPoint = PivotPoint.Center;
            view.ParentOrigin = ParentOrigin.Center;
            view.HeightResizePolicy = ResizePolicyType.FillToParent;
            view.WidthResizePolicy = ResizePolicyType.FillToParent;
            return view;
        }

        /// <summary>
        /// Signal emitted when the pressed animation has completed.
        /// </summary>The animation source.
        /// <param name="source">The animation source.</param>
        /// <param name="e">event</param>
        private void OnPressedAnimationFinished(object source, EventArgs e)
        {
            mPressedAnimation = null;
            if (mPressedView != null)
            {
                string name = mPressedView.Name;

                this.Deactivate();
                Tizen.Log.Fatal("NUI", "OnPressedAnimationFinished");
                object item = Activator.CreateInstance(global::System.Type.GetType("UIControlSample." + name));
                if (item is IExample)
                {
                    Tizen.Log.Fatal("NUI", "OnPressedAnimationFinished");
                    this.Deactivate();
                    global::System.GC.Collect();
                    global::System.GC.WaitForPendingFinalizers();

                    currentSample = item as IExample;
                    Tizen.Log.Fatal("NUI", "OnPressedAnimationFinished");
                    currentSample.Activate();
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
            Tizen.Log.Fatal("NUI", "MainSample");
            Window.Instance.GetDefaultLayer().Add(backGroundView);
            FocusManager.Instance.PreFocusChange += OnKeyboardPreFocusChange;
            StyleManager.Get().ApplyTheme(json_file);
            CreateFocusEffect();
            Tizen.Log.Fatal("NUI", "MainSample");
            FocusManager.Instance.SetCurrentFocusView(mpages[scrollView.GetCurrentPage()].GetChildAt(new TableView.CellPosition(currentRow, currentColumn)));
        }

        /// <summary>
        /// Remove the image from Window.Instance.
        /// </summary>
        public void Deactivate()
        {
            FocusManager.Instance.FocusIndicator = defaultFocusIndicatorView;
            FocusManager.Instance.PreFocusChange -= OnKeyboardPreFocusChange;
            Window.Instance.GetDefaultLayer().Remove(backGroundView);
        }

        /// <summary>
        /// The enter point of the application
        /// </summary>
        /// <param name="args">args</param>
        [STAThread]
        static void Main(string[] args)
        {
            Tizen.Log.Fatal("NUI", "MainSample");
            MainSample mainSample = new MainSample();
            mainSample.Run(args);
        }
    }
}
