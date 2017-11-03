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


namespace ImageSample
{
    /// <summary>
    /// This sample application demonstrates how to use image view and shows some properties of image view. 
    /// </summary>
    class ImageSample : NUIApplication
    {
        private const string resources = "/home/owner/apps_rw/org.tizen.example.ImageSample/res/images/";
        private String image_jpg = resources + "gallery-1.jpg";
        private String image_png = resources + "dali-logo.png";
        private String image_gif = resources + "dog-anim.gif";
        private String image_svg = resources + "Kid1.svg";
        private TableView tableView;
        private ImageView[] imageView;
        private PushButton pushButton, animatedButton, onOffButton, zoomInButton, zoomOutButton, resetButton, fittingModeButton, samplingModeButton;
        private ImageView animatedImage, svgImage, image;
        private PropertyMap pngImageMap;
        private bool onoff = true;
        private float svgScale = 1;
        private int fittingMode, samplingMode;

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
        /// Flex Container Sample Application initialization.
        /// </summary>
        public void Initialize()
        {
            Window.Instance.BackgroundColor = new Color(0.8f, 0.8f, 0.8f, 1.0f);

            // Create a 3 * 3 tableView
            // The tableView will be put nine same images.
            // These images will do PixelArea animation to mosaic a bigger image
            tableView = new TableView(3, 3);
            tableView.Size2D = new Size2D(600, 600);
            tableView.PositionUsesPivotPoint = true;
            tableView.PivotPoint = PivotPoint.TopLeft;
            tableView.ParentOrigin = ParentOrigin.TopLeft;
            tableView.Position2D = new Position2D(100, 100);
            Window.Instance.GetDefaultLayer().Add(tableView);

            // Create nine image Views whose imageurl are same.
            imageView = new ImageView[9];
            for (uint i = 0; i < 3; i++)
            {
                for (uint j = 0; j < 3; j++)
                {
                    imageView[i * 3 + j] = new ImageView(image_jpg);
                    imageView[i * 3 + j].HeightResizePolicy = ResizePolicyType.FillToParent;
                    imageView[i * 3 + j].WidthResizePolicy = ResizePolicyType.FillToParent;
                    // Put these imageView at different position.
                    tableView.AddChild(imageView[i * 3 + j], new TableView.CellPosition(i, j));
                }
            }

            // Create a imageView who will show jpg image
            // The ImageView will do the animation that image repeat 3 on  u coordinate
            animatedImage = new ImageView();
            animatedImage.SizeWidth = 200;
            animatedImage.SizeHeight = 200;
            animatedImage.PositionUsesPivotPoint = true;
            animatedImage.PivotPoint = PivotPoint.Center;
            animatedImage.ParentOrigin = ParentOrigin.TopLeft;
            animatedImage.Position2D = tableView.Position2D + new Position2D((int)(tableView.SizeWidth + animatedImage.SizeWidth * 1.5 + 100), (int)animatedImage.SizeHeight / 2);
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.AnimatedImage))
               .Add(ImageVisualProperty.URL, new PropertyValue(image_gif))
               .Add(ImageVisualProperty.WrapModeU, new PropertyValue((int)WrapModeType.Repeat))
               .Add(ImageVisualProperty.WrapModeV, new PropertyValue((int)WrapModeType.Default));
            animatedImage.ImageMap = map;
            Window.Instance.GetDefaultLayer().Add(animatedImage);

            // Create a imageView who will show svg image
            // The SVG <image> element allows for raster images
            // to be rendered within an SVG object.
            svgImage = new ImageView(image_svg);
            svgImage.SizeWidth = 200;
            svgImage.SizeHeight = 200;
            svgImage.PositionUsesPivotPoint = true;
            svgImage.PivotPoint = PivotPoint.Center;
            svgImage.ParentOrigin = ParentOrigin.TopLeft;
            svgImage.Position2D = animatedImage.Position2D + new Position2D(0, (int)animatedImage.SizeHeight + 200);
            Window.Instance.GetDefaultLayer().Add(svgImage);

            // Create a image which be used to show the property of FittingMode and SamplingMode
            // Set FittingModeType is ShrinkToFit.
            // Set SamplingModeType is Box.
            image = new ImageView();
            image.Size2D = new Size2D(200, 200);
            pngImageMap = new PropertyMap();
            pngImageMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image))
                       .Add(ImageVisualProperty.URL, new PropertyValue(image_png))
                       .Add(ImageVisualProperty.DesiredWidth, new PropertyValue(200))
                       .Add(ImageVisualProperty.DesiredHeight, new PropertyValue(200))
                       .Add(ImageVisualProperty.FittingMode, new PropertyValue((int)FittingModeType.ShrinkToFit))
                       .Add(ImageVisualProperty.SamplingMode, new PropertyValue((int)SamplingModeType.Box));
            fittingMode = (int)FittingModeType.ShrinkToFit;
            samplingMode = (int)SamplingModeType.Box;
            image.ImageMap = pngImageMap;
            image.PositionUsesPivotPoint = true;
            image.PivotPoint = PivotPoint.Center;
            image.ParentOrigin = ParentOrigin.TopLeft;
            image.Position2D = animatedImage.Position2D + new Position2D((int)(animatedImage.SizeWidth * 1.5 + image.SizeWidth / 2 + 100), 0);
            Window.Instance.GetDefaultLayer().Add(image);

            // Create a PushButton which control imageView[] do the pixelArea animation
            pushButton = new PushButton();
            pushButton.Label = CreateText("PixelArea");
            pushButton.PositionUsesPivotPoint = true;
            pushButton.PivotPoint = PivotPoint.Center;
            pushButton.ParentOrigin = ParentOrigin.TopLeft;
            pushButton.Position2D = tableView.Position2D + new Position2D((int)tableView.SizeWidth / 2 - 100, (int)tableView.SizeHeight + 50);
            pushButton.Clicked += PixelAreaButtonClick;
            pushButton.Focusable = true;
            Window.Instance.GetDefaultLayer().Add(pushButton);

            // Create onOffButton which can make imageview[0] show or hide.
            onOffButton = new PushButton();
            onOffButton.Label = CreateText("OnOff");
            onOffButton.PositionUsesPivotPoint = true;
            onOffButton.PivotPoint = PivotPoint.Center;
            onOffButton.ParentOrigin = ParentOrigin.TopLeft;
            onOffButton.Position2D = tableView.Position2D + new Position2D((int)tableView.SizeWidth / 2 + 100, (int)tableView.SizeHeight + 50);
            onOffButton.Clicked += OnOffButtonClick;
            onOffButton.Focusable = true;
            Window.Instance.GetDefaultLayer().Add(onOffButton);

            // Create animatedButton which can make animatedImage do
            // the animation of PixelArea and Scale
            animatedButton = new PushButton();
            animatedButton.Label = CreateText("Animate PixelArea & Scale");
            animatedButton.PositionUsesPivotPoint = true;
            animatedButton.PivotPoint = PivotPoint.Center;
            animatedButton.ParentOrigin = ParentOrigin.TopLeft;
            animatedButton.Position2D = animatedImage.Position2D + new Position2D(0, (int)animatedImage.SizeHeight / 2 + 50);
            animatedButton.Clicked += AnimatedButtonClick;
            animatedButton.Focusable = true;
            Window.Instance.GetDefaultLayer().Add(animatedButton);

            // Create zoomInButton which can make svgImage bigger
            zoomInButton = new PushButton();
            zoomInButton.Label = CreateText("Zoom In");
            zoomInButton.PositionUsesPivotPoint = true;
            zoomInButton.PivotPoint = PivotPoint.Center;
            zoomInButton.ParentOrigin = ParentOrigin.TopLeft;
            zoomInButton.Position2D = svgImage.Position2D + new Position2D(-200, (int)svgImage.SizeHeight / 2 + 50);
            zoomInButton.Clicked += ZoomInButtonClick;
            zoomInButton.Focusable = true;
            Window.Instance.GetDefaultLayer().Add(zoomInButton);

            // Create zoomOutButton which can make svgImage smaller
            zoomOutButton = new PushButton();
            zoomOutButton.Label = CreateText("Zoom Out");
            zoomOutButton.PositionUsesPivotPoint = true;
            zoomOutButton.PivotPoint = PivotPoint.Center;
            zoomOutButton.ParentOrigin = ParentOrigin.TopLeft;
            zoomOutButton.Position2D = svgImage.Position2D + new Position2D(0, (int)svgImage.SizeHeight  / 2 + 50);
            zoomOutButton.Clicked += ZoomOutButtonClick;
            zoomOutButton.Focusable = true;
            Window.Instance.GetDefaultLayer().Add(zoomOutButton);

            // Create resetButton which can make svgImage's size to the original size
            resetButton = new PushButton();
            resetButton.Label = CreateText("Reset");
            resetButton.PositionUsesPivotPoint = true;
            resetButton.PivotPoint = PivotPoint.Center;
            resetButton.ParentOrigin = ParentOrigin.TopLeft;
            resetButton.Position2D = svgImage.Position2D + new Position2D(200, (int)svgImage.SizeHeight / 2 + 50);
            resetButton.Focusable = true;
            resetButton.Clicked += (obj, e) =>
            {
                svgScale = 1;
                svgImage.Size2D = new Vector2(200, 200);
                return true;
            };
            Window.Instance.GetDefaultLayer().Add(resetButton);

            // Create fittingModeButton which can change image's FittingMode
            fittingModeButton = new PushButton();
            fittingModeButton.SizeWidth = 400;
            fittingModeButton.Label = CreateText("FittingMode : ShrinkToFit");
            fittingModeButton.PositionUsesPivotPoint = true;
            fittingModeButton.PivotPoint = PivotPoint.Center;
            fittingModeButton.ParentOrigin = ParentOrigin.TopLeft;
            fittingModeButton.Position2D = image.Position2D + new Position2D(0, (int)image.SizeHeight / 2 + 50);
            fittingModeButton.Clicked += FittingModeButtonClick;
            fittingModeButton.Focusable = true;
            Window.Instance.GetDefaultLayer().Add(fittingModeButton);

            // Create samplingModeButton which can change image's SamplingMode
            samplingModeButton = new PushButton();
            samplingModeButton.SizeWidth = 400;
            samplingModeButton.Label = CreateText("SamplingMode : Box");
            samplingModeButton.PositionUsesPivotPoint = true;
            samplingModeButton.PivotPoint = PivotPoint.Center;
            samplingModeButton.ParentOrigin = ParentOrigin.TopLeft;
            samplingModeButton.Position2D = image.Position2D + new Position2D(0, (int)image.SizeHeight / 2 + 120);
            samplingModeButton.Clicked += SamplingModeButtonClick;
            samplingModeButton.Focusable = true;
            Window.Instance.GetDefaultLayer().Add(samplingModeButton);

            // Set pushButton as the current focus view
            FocusManager.Instance.SetCurrentFocusView(pushButton);
            FocusManager.Instance.PreFocusChange += PreFocusChange;

            // Control the rule of moving focus view when the direction key entered
            pushButton.RightFocusableView = onOffButton;
            onOffButton.RightFocusableView = zoomInButton;
            onOffButton.LeftFocusableView = pushButton;
            zoomInButton.LeftFocusableView = onOffButton;
            zoomInButton.RightFocusableView = zoomOutButton;
            zoomInButton.UpFocusableView = animatedButton;
            zoomOutButton.LeftFocusableView = zoomInButton;
            zoomOutButton.RightFocusableView = resetButton;
            zoomOutButton.UpFocusableView = animatedButton;
            resetButton.LeftFocusableView = zoomOutButton;
            resetButton.UpFocusableView = animatedButton;
            animatedButton.RightFocusableView = fittingModeButton;
            animatedButton.DownFocusableView = zoomInButton;
            fittingModeButton.LeftFocusableView = animatedButton;
            fittingModeButton.DownFocusableView = samplingModeButton;
            samplingModeButton.UpFocusableView = fittingModeButton;

            Window.Instance.KeyEvent += AppBack;
        }
        /// <summary>
        /// Callback when the keyboard focus is going to be changed.
        /// </summary>
        /// <param name="resource">FocusManager.Instance</param>
        /// <param name="e">event</param>
        /// <returns> The view to move the keyboard focus to.</returns>
        private View PreFocusChange(object resource, FocusManager.PreFocusChangeEventArgs e)
        {
            View currentView = e.CurrentView;
            View nextView = e.ProposedView;
            if (currentView == null && nextView == null)
            {
                nextView = pushButton;
            }

            return nextView;
        }

        /// <summary>
        /// The event will be triggered when pushButton clicked.
        /// Play the pixelArea animation of imageView.
        /// </summary>
        /// <param name="source">pushButton.</param>
        /// <param name="e">event</param>
        /// <returns>the consume flag</returns>
        private bool PixelAreaButtonClick(object source, EventArgs e)
        {
            Animation animation = new Animation(10000);
            for (uint i = 0; i < 3; i++)
            {
                for (uint j = 0; j < 3; j++)
                {
                    // animate the pixel area property on image view,
                    // the animatable pixel area property is registered on the actor,
                    // which overwrites the property on the renderer
                    animation.AnimateTo(imageView[j * 3 + i], "pixelArea", new Vector4(0.33f * i, 0.33f * j, 0.33f, 0.33f), new AlphaFunction(AlphaFunction.BuiltinFunctions.Bounce));
                }
            }

            animation.Play();
            return true;
        }

        /// <summary>
        /// The event will be triggered when animatedButton clicked.
        /// Create the pixelArea and scale animation of animatedImage.
        /// </summary>
        /// <param name="source">animatedButton.</param>
        /// <param name="e">event</param>
        /// <returns>the consume flag</returns>
        private bool AnimatedButtonClick(object source, EventArgs e)
        {
            Animation animation = new Animation();
            animation.Duration = 3000;
            animation.AnimateTo(animatedImage, "pixelArea", new Vector4(-1.0f, 0.0f, 3.0f, 1.0f), new AlphaFunction(AlphaFunction.BuiltinFunctions.Sin));
            animation.AnimateTo(animatedImage, "scaleX", 3.0f, new AlphaFunction(AlphaFunction.BuiltinFunctions.Sin));
            animation.Play();
            return true;
        }

        /// <summary>
        /// The event will be triggered when onOffButton clicked
        /// </summary>
        /// <param name="source">onOffButton.</param>
        /// <param name="e">event</param>
        /// <returns>the consume flag</returns>
        private bool OnOffButtonClick(object source, EventArgs e)
        {
            if (onoff)
            {
                imageView[0].Unparent();
                onoff = false;
            }
            else
            {
                tableView.AddChild(imageView[0], new TableView.CellPosition(0, 0));
                onoff = true;
            }

            return true;
        }

        /// <summary>
        /// The event will be triggered when zoomInButton clicked
        /// </summary>
        /// <param name="source">zoomInButton.</param>
        /// <param name="e">event</param>
        /// <returns>the consume flag</returns>
        private bool ZoomInButtonClick(object source, EventArgs e)
        {
            svgScale /= 1.1f;
            svgImage.Size2D = (new Vector2(200, 200)) * svgScale;
            return true;
        }

        /// <summary>
        /// The event will be triggered when zoomOutButton clicked
        /// </summary>
        /// <param name="source">zoomOutButton.</param>
        /// <param name="e">event</param>
        /// <returns>the consume flag</returns>
        private bool ZoomOutButtonClick(object source, EventArgs e)
        {
            svgScale *= 1.1f;
            svgImage.Size2D = (new Vector2(200, 200)) * svgScale;
            return true;
        }

        /// <summary>
        /// The event will be triggered when fittingModeButton clicked
        /// FittingMode converts a scaling mode to the definition of which dimensions matter
        /// when box filtering as a part of that mode.
        //
        /// Shrink to fit attempts to make one or zero dimensions smaller than the
        /// desired dimensions and one or two dimensions exactly the same as the desired
        /// ones, so as long as one dimension is larger than the desired size, box
        /// filtering can continue even if the second dimension is smaller than the
        /// desired dimensions.!
        ///
        /// X Dimension is ignored by definition in FIT_HEIGHT mode
        ///
        /// Y dimension is irrelevant when downscaling in FIT_WIDTH mode
        ///
        /// Scale to fill mode keeps both dimensions at least as large as desired
        /// </summary>
        /// <param name="source">fittingModeButton.</param>
        /// <param name="e">event</param>
        /// <returns>the consume flag</returns>
        private bool FittingModeButtonClick(object source, EventArgs e)
        {
            switch (fittingMode)
            {
                case (int)FittingModeType.FitHeight:
                    fittingMode = (int)FittingModeType.FitWidth;
                    pngImageMap.Add(ImageVisualProperty.FittingMode, new PropertyValue((int)FittingModeType.FitWidth));
                    fittingModeButton.Label = CreateText("FittingMode : FitWidth");
                    break;
                case (int)FittingModeType.FitWidth:
                    fittingMode = (int)FittingModeType.ScaleToFill;
                    pngImageMap.Add(ImageVisualProperty.FittingMode, new PropertyValue((int)FittingModeType.ScaleToFill));
                    fittingModeButton.Label = CreateText("FittingMode : ScaleToFill");
                    break;
                case (int)FittingModeType.ScaleToFill:
                    fittingMode = (int)FittingModeType.ShrinkToFit;
                    pngImageMap.Add(ImageVisualProperty.FittingMode, new PropertyValue((int)FittingModeType.ShrinkToFit));
                    fittingModeButton.Label = CreateText("FittingMode : ShrinkToFit");
                    break;
                case (int)FittingModeType.ShrinkToFit:
                    fittingMode = (int)FittingModeType.FitHeight;
                    pngImageMap.Add(ImageVisualProperty.FittingMode, new PropertyValue((int)FittingModeType.FitHeight));
                    fittingModeButton.Label = CreateText("FittingMode : FitHeight");
                    break;
            }

            image.ImageMap = pngImageMap;
            return true;
        }

        /// <summary>
        /// The event will be triggered when samplingModeButton clicked
        /// SamplingMode generate the rectangle to use as the target of a pixel sampling pass
        ///
        /// Box: Iteratively box filter to generate an image of 1/2, 1/4, 1/8,
        /// etc width and height and approximately the desired size.
        ///
        /// BoxThenLinear : Iteratively box filter to almost the right size,
        /// then for each output pixel, read four pixels from the last level
        /// of box filtering and write their weighted average.
        ///
        /// BoxThenNeares : Iteratively box filter to generate an image of 1/2,
        /// 1/4, 1/8 etc width and height and approximately the desired size,
        /// then for each output pixel, read one pixel from the last level of box filtering.
        ///
        /// DontCare : For caching algorithms where a client strongly prefers a cache-hit to reuse a cached image.
        ///
        /// Linear : For each output pixel, read a quad of four input pixels and write a weighted average of them.
        ///
        /// Nearest : For each output pixel, read one input pixel.
        ///
        /// NoFilter : No filtering is performed. If the SCALE_TO_FILL scaling mode is enabled, the borders of
        /// the image may be trimmed to match the aspect ratio of the desired dimensions.
        /// </summary>
        /// <param name="source">samplingModeButton.</param>
        /// <param name="e">event</param>
        /// <returns>the consume flag</returns>
        private bool SamplingModeButtonClick(object source, EventArgs e)
        {
            switch (samplingMode)
            {
                case (int)SamplingModeType.Box:
                    samplingMode = (int)SamplingModeType.BoxThenLinear;
                    pngImageMap.Add(ImageVisualProperty.SamplingMode, new PropertyValue((int)SamplingModeType.BoxThenLinear));
                    samplingModeButton.Label = CreateText("SamplingMode : BoxThenLinear");
                    break;
                case (int)SamplingModeType.BoxThenLinear:
                    samplingMode = (int)SamplingModeType.BoxThenNearest;
                    pngImageMap.Add(ImageVisualProperty.SamplingMode, new PropertyValue((int)SamplingModeType.BoxThenNearest));
                    samplingModeButton.Label = CreateText("SamplingMode : BoxThenNearest");
                    break;
                case (int)SamplingModeType.BoxThenNearest:
                    samplingMode = (int)SamplingModeType.DontCare;
                    pngImageMap.Add(ImageVisualProperty.SamplingMode, new PropertyValue((int)SamplingModeType.DontCare));
                    samplingModeButton.Label = CreateText("SamplingMode : DontCare");
                    break;
                case (int)SamplingModeType.DontCare:
                    samplingMode = (int)SamplingModeType.Linear;
                    pngImageMap.Add(ImageVisualProperty.SamplingMode, new PropertyValue((int)SamplingModeType.Linear));
                    samplingModeButton.Label = CreateText("SamplingMode : Linear");
                    break;
                case (int)SamplingModeType.Linear:
                    samplingMode = (int)SamplingModeType.Nearest;
                    pngImageMap.Add(ImageVisualProperty.SamplingMode, new PropertyValue((int)SamplingModeType.Nearest));
                    samplingModeButton.Label = CreateText("SamplingMode : Nearest");
                    break;
                case (int)SamplingModeType.Nearest:
                    samplingMode = (int)SamplingModeType.NoFilter;
                    pngImageMap.Add(ImageVisualProperty.SamplingMode, new PropertyValue((int)SamplingModeType.NoFilter));
                    samplingModeButton.Label = CreateText("SamplingMode : NoFilter");
                    break;
                case (int)SamplingModeType.NoFilter:
                    samplingMode = (int)SamplingModeType.Box;
                    pngImageMap.Add(ImageVisualProperty.SamplingMode, new PropertyValue((int)SamplingModeType.Box));
                    samplingModeButton.Label = CreateText("SamplingMode : Box");
                    break;
            }

            image.ImageMap = pngImageMap;
            return true;
        }

        /// <summary>
        /// Create text propertyMap used to set Button.Label
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>The created propertyMap</returns>
        private PropertyMap CreateText(string text)
        {
            PropertyMap textVisual = new PropertyMap();
            textVisual.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            textVisual.Add(TextVisualProperty.Text, new PropertyValue(text));
            textVisual.Add(TextVisualProperty.TextColor, new PropertyValue(Color.Black));
            textVisual.Add(TextVisualProperty.PointSize, new PropertyValue(7));
            textVisual.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
            textVisual.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));
            return textVisual;
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
                    this.Exit();
                }
            }
        }
    }
}