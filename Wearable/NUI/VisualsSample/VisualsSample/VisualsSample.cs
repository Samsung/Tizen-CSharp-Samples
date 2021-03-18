/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd.
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

namespace VisualsSample
{
    /// <summary>
    /// A sample of ImageView
    /// </summary>
    class VisualsSample : NUIApplication
    {
        /// <summary>
        /// A list of image URL
        /// </summary>
        private static string mResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/";
        // JPG Image Url
        private static string mImageJpgUrl = mResourceUrl + "gallery-1.jpg";
        // GIF Image Url
        private static string mImageGifUrl = mResourceUrl + "dog-anim.gif";
        // SVG Image Url
        private static string mImageSvgUrl = mResourceUrl + "Kid1.svg";
        // Nine patch Image Url
        private static string mImageNpatchUrl = mResourceUrl + "heartsframe.9.png";

        // A string list of Visual type
        private string[] mVisualType =
        {
            "Image",
            "Animated",
            "Color",
            "NPatch",
            "Border",
            "Gradient",
            "SVG",
            "Cone",
            "Sphere",
            "Cylinder",
            "ConicalFrustrum",
            "Cube",
            "Octahedron",
            "BevelledCube"
        };
        // A number of sample cases
        private uint mVisualCount = 14;
        private uint mCurruntButtonIndex = 0;

        private VisualView mVisualView;
        private Animation[] mVisualViewAnimation;

        // UI properties
        private Position mTableViewStartPosition = new Position(0, 0, 0);
        private Size2D mVisualSize = new Size2D(150, 150);

        private Vector2 mTouchedPosition;
        private bool mTouched = false;

        // Window Size
        private Size2D mWindowSize;
        // Text point sizes
        private float mLargePointSize = 10.0f;
        private float mMiddlePointSize = 5.0f;
        private float mSmallPointSize = 3.0f;

        /// <summary>
        /// The constructor with null
        /// </summary>
        public VisualsSample() : base()
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
        /// ImageView Sample Application initialization.
        /// </summary>
        public void Initialize()
        {
            // Set the background Color of Window.
            Window.Instance.BackgroundColor = Color.Black;
            mWindowSize = Window.Instance.Size;

            // Create Title TextLabel
            TextLabel Title = new TextLabel("Visuals");
            Title.HorizontalAlignment = HorizontalAlignment.Center;
            Title.VerticalAlignment = VerticalAlignment.Center;
            // Set Text color White
            Title.TextColor = Color.White;
            Title.PositionUsesPivotPoint = true;
            Title.ParentOrigin = ParentOrigin.TopCenter;
            Title.PivotPoint = PivotPoint.TopCenter;
            Title.Position2D = new Position2D(0, mWindowSize.Height / 10);
            // Use Samsung One 600 font
            Title.FontFamily = "Samsung One 600";
            // Set MultiLine to false
            Title.MultiLine = false;
            Title.PointSize = mLargePointSize;
            Window.Instance.GetDefaultLayer().Add(Title);

            // Create Visuals.
            CreateVisuals();

            // Create subTitle TextLabel
            TextLabel subTitle = new TextLabel("Swipe for other visuals");
            subTitle.HorizontalAlignment = HorizontalAlignment.Center;
            subTitle.VerticalAlignment = VerticalAlignment.Center;
            // Set Text color White
            subTitle.TextColor = Color.White;
            subTitle.PositionUsesPivotPoint = true;
            subTitle.ParentOrigin = ParentOrigin.BottomCenter;
            subTitle.PivotPoint = PivotPoint.BottomCenter;
            subTitle.Position2D = new Position2D(0, -50);
            // Use Samsung One 600 font
            subTitle.FontFamily = "Samsung One 600";
            // Set MultiLine to false
            subTitle.MultiLine = false;
            subTitle.PointSize = mSmallPointSize;
            Window.Instance.GetDefaultLayer().Add(subTitle);

            // Animation setting for the sample animation
            mVisualViewAnimation = new Animation[2];
            mVisualViewAnimation[0] = new Animation();
            mVisualViewAnimation[0].Duration = 100;
            mVisualViewAnimation[0].AnimateBy(mVisualView, "Position", new Vector3(-360, 0, 0));
            mVisualViewAnimation[1] = new Animation();
            mVisualViewAnimation[1].Duration = 100;
            mVisualViewAnimation[1].AnimateBy(mVisualView, "Position", new Vector3(360, 0, 0));

            // Add Signal Callback functions
            Window.Instance.TouchEvent += OnWindowTouched;
            Window.Instance.KeyEvent += OnKey;
        }

        /// <summary>
        /// Touch event handling of Window
        /// </summary>
        /// <param name="sender">Window</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private void OnWindowTouched(object sender, Window.TouchEventArgs e)
        {
            if (e.Touch.GetPointCount() < 1)
            {
                return;
            }

            switch (e.Touch.GetState(0))
            {
                // If State is Down (Touched at the outside of Button)
                // - Store touched position.
                // - Set the mTouched to true
                case PointStateType.Down:
                {
                    mTouchedPosition = e.Touch.GetScreenPosition(0);
                    mTouched = true;
                    break;
                }
                // If State is Motion
                // - Check the touched position is in the touchable position.
                // - Check the Motion is about Horizontal movement.
                // - If the amount of movement is larger than threshold, run the swipe animation(left or right).
                case PointStateType.Motion:
                {
                    if (!mTouched)
                    {
                        break;
                    }

                    // If the vertical movement is large, the gesture is ignored.
                    Vector2 displacement = e.Touch.GetScreenPosition(0) - mTouchedPosition;
                    if (Math.Abs(displacement.Y) > 20)
                    {
                        mTouched = false;
                        break;
                    }

                    // If displacement is larger than threshold
                    // Play Negative directional animation.
                    if (displacement.X > 30)
                    {
                        AnimateAStepNegative();
                        mTouched = false;
                    }
                    // If displacement is smaller than threshold
                    // Play Positive directional animation.
                    if (displacement.X < -30)
                    {
                        AnimateAStepPositive();
                        mTouched = false;
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// Animate the tableView to the Negative direction
        /// </summary>
        private void AnimateAStepNegative()
        {
            // If the state is not the first one, move ImageViews and PushButton a step.
            if (mCurruntButtonIndex > 0)
            {
                mCurruntButtonIndex--;

                mVisualViewAnimation[1].Play();
            }
        }

        /// <summary>
        /// Animate the tableView to the Positive direction
        /// </summary>
        private void AnimateAStepPositive()
        {
            // If the state is not the last one, move ImageViews and PushButton a step.
            if (mCurruntButtonIndex < mVisualCount - 1)
            {
                mCurruntButtonIndex++;

                mVisualViewAnimation[0].Play();
            }
        }

        /// <summary>
        /// Create TableView, Create PushButtons and add them to tableView.
        /// </summary>
        private void CreateVisuals()
        {
            // Visual View to contain each visual
            mVisualView = new VisualView();
            mVisualView.PositionUsesPivotPoint = true;
            mVisualView.ParentOrigin = ParentOrigin.CenterLeft;
            mVisualView.PivotPoint = PivotPoint.CenterLeft;
            // Set the VisualView size for the 14 kind of visuals
            mVisualView.Size2D = new Size2D(14 * 360, mVisualSize.Height);

            Window.Instance.GetDefaultLayer().Add(mVisualView);

            // Default Visual position
            Vector2 visualPosition = new Vector2(180, 0);
            uint visualIndex = 0;
            for (; visualIndex < mVisualCount; ++visualIndex)
            {
                // Create each Visual Map
                mVisualView.Add(CreateVisualMap(mVisualType[visualIndex], visualPosition));
                // Update Visual Position for the next visual
                visualPosition += new Vector2(360, 0);
            }
        }

        /// <summary>
        /// Create VisualMap
        /// </summary>
        /// <param name="type">A string that denote visual type</param>
        /// <param name="position">A position of the created visual</param>
        /// <returns>VisualView</returns>
        private VisualView CreateVisualMap(string type, Vector2 position)
        {
            // Visual Map declaration
            VisualMap visualMap = null;
            // TextVisual declaration
            TextVisual textVisual = null;
            switch (type)
            {
                // Image Visual
                case "Image":
                    ImageVisual imageVisual = new ImageVisual();
                    // Set Visual URL
                    imageVisual.URL = mImageJpgUrl;
                    visualMap = imageVisual;
                    // Create TextVisual
                    textVisual = CreateTextVisual(type + " Visual");
                    break;
                case "Color":
                    ColorVisual colorVisual = new ColorVisual();
                    // Set Visual Color
                    colorVisual.Color = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
                    visualMap = colorVisual;
                    // Create TextVisual
                    textVisual = CreateTextVisual(type + " Visual");
                    break;
                case "NPatch":
                    NPatchVisual nPatchVisual = new NPatchVisual();
                    // Set Visual URL
                    nPatchVisual.URL = mImageNpatchUrl;
                    visualMap = nPatchVisual;
                    // Create TextVisual
                    textVisual = CreateTextVisual(type + " Visual");
                    break;
                case "SVG":
                    SVGVisual svgVisual = new SVGVisual();
                    // Set Visual URL
                    svgVisual.URL = mImageSvgUrl;
                    visualMap = svgVisual;
                    // Create TextVisual
                    textVisual = CreateTextVisual(type + " Visual");
                    break;
                case "Animated":
                    AnimatedImageVisual animatedImageVisual = new AnimatedImageVisual();
                    // Set Visual URL
                    animatedImageVisual.URL = mImageGifUrl;
                    visualMap = animatedImageVisual;
                    // Create TextVisual
                    textVisual = CreateTextVisual(type + " Visual");
                    break;
                case "Border":
                    BorderVisual borderVisual = new BorderVisual();
                    // Set Visual Color
                    borderVisual.Color = Color.White;
                    // Set Visual Size
                    borderVisual.BorderSize = 5.0f;
                    visualMap = borderVisual;
                    // Create TextVisual
                    textVisual = CreateTextVisual(type + " Visual");
                    break;
                case "Gradient":
                    // Set GradientVisual properties
                    GradientVisual gradientVisual = new GradientVisual();
                    PropertyArray stopOffset = new PropertyArray();
                    stopOffset.Add(new PropertyValue(0.0f));
                    stopOffset.Add(new PropertyValue(0.3f));
                    stopOffset.Add(new PropertyValue(0.6f));
                    stopOffset.Add(new PropertyValue(0.8f));
                    stopOffset.Add(new PropertyValue(1.0f));
                    gradientVisual.StopOffset = stopOffset;
                    // Create the PropertyArray of stopColor.
                    PropertyArray stopColor = new PropertyArray();
                    stopColor.Add(new PropertyValue(new Vector4(129.0f, 198.0f, 193.0f, 255.0f) / 255.0f));
                    stopColor.Add(new PropertyValue(new Vector4(196.0f, 198.0f, 71.0f, 122.0f) / 255.0f));
                    stopColor.Add(new PropertyValue(new Vector4(214.0f, 37.0f, 139.0f, 191.0f) / 255.0f));
                    stopColor.Add(new PropertyValue(new Vector4(129.0f, 198.0f, 193.0f, 150.0f) / 255.0f));
                    stopColor.Add(new PropertyValue(Color.Yellow));
                    // Set the color at the stop offsets.
                    // At least 2 values required to show a gradient.
                    gradientVisual.StopColor = stopColor;
                    // Set the start position of a linear gradient.
                    gradientVisual.StartPosition = new Vector2(0.5f, 0.5f);
                    // Set the end position of a linear gradient.
                    gradientVisual.EndPosition = new Vector2(-0.5f, -0.5f);
                    // Set the center point of a radial gradient.
                    gradientVisual.Center = new Vector2(0.5f, 0.5f);
                    // Set the size of the radius of a radial gradient.
                    gradientVisual.Radius = 1.414f;
                    visualMap = gradientVisual;
                    // Create TextVisual
                    textVisual = CreateTextVisual(type + " Visual");
                    break;
                case "Cone":
                    visualMap = CreatePrimitiveVisual(PrimitiveVisualShapeType.Cone);
                    // Create TextVisual
                    textVisual = CreateTextVisual("PrimitiveVisual(" + type + ")");
                    break;
                case "Sphere":
                    visualMap = CreatePrimitiveVisual(PrimitiveVisualShapeType.Sphere);
                    // Create TextVisual
                    textVisual = CreateTextVisual("PrimitiveVisual(" + type + ")");
                    break;
                case "Cylinder":
                    visualMap = CreatePrimitiveVisual(PrimitiveVisualShapeType.Cylinder);
                    // Create TextVisual
                    textVisual = CreateTextVisual("PrimitiveVisual(" + type + ")");
                    break;
                case "ConicalFrustrum":
                    visualMap = CreatePrimitiveVisual(PrimitiveVisualShapeType.ConicalFrustrum);
                    // Create TextVisual
                    textVisual = CreateTextVisual("PrimitiveVisual(" + type + ")");
                    break;
                case "Cube":
                    visualMap = CreatePrimitiveVisual(PrimitiveVisualShapeType.Cube);
                    // Create TextVisual
                    textVisual = CreateTextVisual("PrimitiveVisual(" + type + ")");
                    break;
                case "Octahedron":
                    visualMap = CreatePrimitiveVisual(PrimitiveVisualShapeType.Octahedron);
                    // Create TextVisual
                    textVisual = CreateTextVisual("PrimitiveVisual(" + type + ")");
                    break;
                case "BevelledCube":
                    visualMap = CreatePrimitiveVisual(PrimitiveVisualShapeType.BevelledCube);
                    // Create TextVisual
                    textVisual = CreateTextVisual("PrimitiveVisual(" + type + ")");
                    break;
                default:
                    break;
            }
            if (visualMap != null) {
                // Set the common properties
                visualMap.Size = mVisualSize;
                visualMap.Position = new Vector2(0.0f, 0.0f);
                visualMap.PositionPolicy = VisualTransformPolicyType.Absolute;
                visualMap.SizePolicy = VisualTransformPolicyType.Absolute;
                visualMap.Origin = Visual.AlignType.Center;
                visualMap.AnchorPoint = Visual.AlignType.Center;
            }
            if (textVisual != null)
                textVisual.Position = new Vector2(0.0f, mVisualSize.Height / 2 + 25);

            VisualView subVisualView = new VisualView();

            subVisualView.PositionUsesPivotPoint = true;
            subVisualView.ParentOrigin = ParentOrigin.CenterLeft;
            subVisualView.PivotPoint = PivotPoint.Center;
            subVisualView.Position2D = position;
            subVisualView.Size2D = mVisualSize;

            subVisualView.AddVisual(type + "Visual", visualMap);
            subVisualView.AddVisual(type + "TextVisual", textVisual);

            return subVisualView;
        }

        /// <summary>
        /// Create a TextVisual.
        /// </summary>
        /// <param name="text">the text of the TextVisual</param>
        /// <returns>return a text visual</returns>
        private TextVisual CreateTextVisual(string text)
        {
            Tizen.NUI.TextVisual textVisual = new Tizen.NUI.TextVisual();
            textVisual.Text = text;
            textVisual.PointSize = mMiddlePointSize;
            textVisual.FontFamily = "Samsung One 400";
            textVisual.Size = new Vector2(300.0f, 30.0f);
            textVisual.TextColor = Color.White;
            textVisual.PositionPolicy = VisualTransformPolicyType.Absolute;
            textVisual.SizePolicy = VisualTransformPolicyType.Absolute;
            textVisual.Origin = Visual.AlignType.Center;
            textVisual.AnchorPoint = Visual.AlignType.Center;
            textVisual.HorizontalAlignment = HorizontalAlignment.Center;
            textVisual.VerticalAlignment = VerticalAlignment.Center;
            return textVisual;
        }

        /// <summary>
        /// Create a PrimitiveVisual.
        /// </summary>
        /// <param name="type">the type of the shape</param>
        /// <returns>return a primitiveVisual</returns>
        private Tizen.NUI.PrimitiveVisual CreatePrimitiveVisual(PrimitiveVisualShapeType type)
        {
            Tizen.NUI.PrimitiveVisual primitiveVisual = new Tizen.NUI.PrimitiveVisual();
            primitiveVisual.Shape = type;
            primitiveVisual.BevelPercentage = 0.3f;
            primitiveVisual.BevelSmoothness = 0.0f;
            primitiveVisual.ScaleDimensions = new Vector3(1.0f, 1.0f, 0.3f);
            primitiveVisual.MixColor = new Vector4((245.0f / 255.0f), (188.0f / 255.0f), (73.0f / 255.0f), 1.0f);
            return primitiveVisual;
        }

        /// <summary>
        /// This Application will be exited when back key entered.
        /// </summary>
        /// <param name="sender">Window.Instance</param>
        /// <param name="e">event</param>
        private void OnKey(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "XF86Back")
                {
                    this.Exit();
                }
            }
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
            // Add each property of TextVisual
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            map.Add(TextVisualProperty.Text, new PropertyValue(text));
            map.Add(TextVisualProperty.TextColor, new PropertyValue(color));
            //map.Add(TextVisualProperty.PointSize, new PropertyValue(8.0f));
            map.Add(TextVisualProperty.PointSize, new PropertyValue(mMiddlePointSize));
            map.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
            map.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));
            map.Add(TextVisualProperty.FontFamily, new PropertyValue("Samsung One 400"));
            return map;
        }

        /// <summary>
        /// The enter point of VisualsSample.
        /// </summary>
        /// <param name="args">args</param>
        static void Main(string[] args)
        {
            Log.Info("Tag", "========== Hello, VisualsSample ==========");
            new VisualsSample().Run(args);
        }
    }
}
