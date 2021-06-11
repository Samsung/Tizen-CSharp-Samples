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

namespace VisualSample
{
    /// <summary>
    /// A sample of ImageView
    /// </summary>
    class VisualSample : NUIApplication
    {

        private static string resource = "/home/owner/apps_rw/org.tizen.example.VisualSample/res/images/";
        private string normalImagePath = resource + "/images/Button/btn_bg_25_25_25_95.9.png";
        private string focusImagePath = resource + "/images/Button/btn_bg_255_255_255_200.9.png";
        private string pressImagePath = resource + "/images/Button/btn_bg_0_129_198_100.9.png";

        private String image_jpg = resource + "gallery-1.jpg";
        private String image_gif = resource + "dog-anim.gif";
        private String image_svg = resource + "Kid1.svg";
        private String image_9patch = resource + "heartsframe.9.png";

        private TextLabel guide;

        private VisualView _visualView;
        //private TableView[] mpages;

        /// <summary>
        /// The constructor with null
        /// </summary>
        public VisualSample() : base()
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
            View focusIndicator = new View();
            FocusManager.Instance.FocusIndicator = focusIndicator;
            Window.Instance.BackgroundColor = Color.Black;

            //Create the text of guide.
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
            guide.PointSize = PointSize15;
            guide.Text = "Visual Sample \n";
            guide.TextColor = Color.White;
            Window.Instance.GetDefaultLayer().Add(guide);
            
            //Create the visual view for example.
            _visualView = new VisualView();
            _visualView.PositionUsesPivotPoint = true;
            _visualView.ParentOrigin = ParentOrigin.TopLeft;
            _visualView.PivotPoint = PivotPoint.TopLeft;
            _visualView.Position2D = new Position2D(0, 160);
            _visualView.Size2D = new Size2D(1920, 800);
            Window.Instance.GetDefaultLayer().Add(_visualView);

            Populate();
            Window.Instance.KeyEvent += AppBack;
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
                    //When receive back key, exit the app.
                    this.Exit();
                }
            }
        }

        /// <summary>
        /// Create TableView, Create pushButtons and add them to tableView.
        /// </summary>
        private void Populate()
        {
            Vector2 stagesize = Window.Instance.Size;
            
            //Create the image visual.
            Tizen.NUI.VisualMap imageVisual = CreateVisualMap("Image", new Vector2(60, 100.0f), image_jpg);
            Tizen.NUI.TextVisual imageText = CreateTextVisual("ImageVisual", new Vector2(60, 0.0f));
            _visualView.AddVisual("imageVisual", imageVisual);
            _visualView.AddVisual("imageText", imageText);

            //Create the gif visual.
            Tizen.NUI.VisualMap gifVisual = CreateVisualMap("Animated", new Vector2(460, 100.0f), image_gif);
            Tizen.NUI.TextVisual gifText = CreateTextVisual("AnimatedImageVisual", new Vector2(460, 0.0f));
            _visualView.AddVisual("gifVisual", gifVisual);
            _visualView.AddVisual("gifText", gifText);

            //Create the color visual.
            Tizen.NUI.VisualMap colorVisual = CreateVisualMap("Color", new Vector2(860, 100.0f), image_gif);
            Tizen.NUI.TextVisual colorText = CreateTextVisual("ColorVisual", new Vector2(860, 0.0f));
            _visualView.AddVisual("colorVisual", colorVisual);
            _visualView.AddVisual("colorText", colorText);

            //Create the svg visual view.
            VisualView svgView = new VisualView();
            svgView.PositionUsesPivotPoint = true;
            svgView.ParentOrigin = ParentOrigin.TopLeft;
            svgView.PivotPoint = PivotPoint.TopLeft;
            svgView.Position2D = new Position2D(1280, 100);
            svgView.Size2D = new Size2D(200, 200);
            _visualView.Add(svgView);

            //Add svg visual map and svg text visual to svg visual view.
            Tizen.NUI.VisualMap svgVisual = CreateVisualMap("SVG", new Vector2(0, 0.0f), image_svg);
            Tizen.NUI.TextVisual svgText = CreateTextVisual("SVGVisual", new Vector2(1260, 0.0f));
            svgView.AddVisual("svgVisual", svgVisual);
            _visualView.AddVisual("svgText", svgText);

            //Add nPatch visual-map and nPatch visual-text to visual view.
            Tizen.NUI.VisualMap nPatchVisual = CreateVisualMap("NPatch", new Vector2(1660, 100.0f), image_9patch);
            Tizen.NUI.TextVisual nPatchText = CreateTextVisual("NPatchVisual", new Vector2(1660, 0.0f));
            _visualView.AddVisual("nPatchVisual", nPatchVisual);
            _visualView.AddVisual("nPatchText", nPatchText);

            //Create border visual view and add it to root.
            VisualView borderView = new VisualView();
            borderView.BackgroundColor = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
            borderView.PositionUsesPivotPoint = true;
            borderView.ParentOrigin = ParentOrigin.TopLeft;
            borderView.PivotPoint = PivotPoint.TopLeft;
            borderView.Position2D = new Position2D(80, 400);
            borderView.Size2D = new Size2D(200, 200);
            _visualView.Add(borderView);

            //Create and add the visual of border to border view.
            Tizen.NUI.VisualMap borderVisual = CreateVisualMap("Border", new Vector2(0, 0.0f), image_9patch);
            Tizen.NUI.TextVisual borderText = CreateTextVisual("BorderVisual", new Vector2(60, 300.0f));
            borderView.AddVisual("borderVisual", borderVisual);
            _visualView.AddVisual("borderText", borderText);

            //Create and add the visual of gradient to root visual view.
            Tizen.NUI.VisualMap gradientVisual = CreateVisualMap("Gradient", new Vector2(460, 400.0f), image_9patch);
            Tizen.NUI.TextVisual gradientText = CreateTextVisual("GradientVisual", new Vector2(460, 300.0f));
            _visualView.AddVisual("gradientVisual", gradientVisual);
            _visualView.AddVisual("gradientText", gradientText);

            // primitive visual: Cone
            // Renders a simple 3D shape, such as a cube or sphere. Scaled to fit the control.
            // The shapes are generated with clockwise winding and backface culling on by default.
            // Cone : Equivalent to a conical frustum with the top radius of zero
            Tizen.NUI.PrimitiveVisual cone = CreatePrimitiveVisual(PrimitiveVisualShapeType.Cone, new Vector2(860, 400));
            Tizen.NUI.TextVisual coneText = CreateTextVisual("Cone", new Vector2(860, 300.0f));
            _visualView.AddVisual("coneText", coneText);
            _visualView.AddVisual("cone", cone);

            // primitive visual: Sphere
            // Sphere : A perfectly round geometrical object in the three-dimensional space
            Tizen.NUI.PrimitiveVisual sphere = CreatePrimitiveVisual(PrimitiveVisualShapeType.Sphere, new Vector2(1260, 400.0f));
            Tizen.NUI.TextVisual sphereText = CreateTextVisual("Sphere", new Vector2(1260, 300.0f));
            _visualView.AddVisual("sphereText", sphereText);
            _visualView.AddVisual("sphere", sphere);

            // primitive visual: Cylinder
            // Cylinder : Equivalent to a conical frustum with the top radius of zero
            Tizen.NUI.PrimitiveVisual cylinder = CreatePrimitiveVisual(PrimitiveVisualShapeType.Cylinder, new Vector2(1660, 400.0f));
            Tizen.NUI.TextVisual cylinderText = CreateTextVisual("Cylinder", new Vector2(1660, 300.0f));
            _visualView.AddVisual("cylinderText", cylinderText);
            _visualView.AddVisual("cylinder", cylinder);

            // primitive visual: ConicalFrustum
            // ConicalFrustum : Equivalent to a conical frustum with
            // equal radii for the top and bottom circles.
            Tizen.NUI.PrimitiveVisual conicalFrustum = CreatePrimitiveVisual(PrimitiveVisualShapeType.ConicalFrustrum, new Vector2(60.0f, 700));
            Tizen.NUI.TextVisual conicalFrustumText = CreateTextVisual("ConicalFrustum", new Vector2(60.0f, 600.0f));
            _visualView.AddVisual("conicalFrustumText", conicalFrustumText);
            _visualView.AddVisual("conicalFrustum", conicalFrustum);

            // primitive visual: Cube
            // Cube : Equivalent to a conical frustum with equal radii for the top and bottom
            //     circles.
            Tizen.NUI.PrimitiveVisual cube = CreatePrimitiveVisual(PrimitiveVisualShapeType.Cube, new Vector2(460.0f, 700.0f));
            Tizen.NUI.TextVisual cubeText = CreateTextVisual("Cube", new Vector2(460.0f, 600.0f));
            _visualView.AddVisual("cubeText", cubeText);
            _visualView.AddVisual("cube", cube);

            // primitive visual: Octahedron
            // Equivalent to a bevelled cube with a bevel percentage of zero
            Tizen.NUI.PrimitiveVisual octahedron = CreatePrimitiveVisual(PrimitiveVisualShapeType.Octahedron, new Vector2(860.0f, 700.0f));
            Tizen.NUI.TextVisual octaheText = CreateTextVisual("Octahedron", new Vector2(860.0f, 600.0f));
            _visualView.AddVisual("octaheText", octaheText);
            _visualView.AddVisual("octahedron", octahedron);

            // primitive visual: BevelledCube
            // BevelledCube : Equivalent to a bevelled cube with a bevel percentage of one
            Tizen.NUI.PrimitiveVisual bevelledCube = CreatePrimitiveVisual(PrimitiveVisualShapeType.BevelledCube, new Vector2(1260, 700.0f));
            Tizen.NUI.TextVisual bevelledCubeText = CreateTextVisual("BevelledCube", new Vector2(1260, 600.0f));
            _visualView.AddVisual("bevelledCubeText", bevelledCubeText);
            _visualView.AddVisual("bevelledCube", bevelledCube);


        }

        /// <summary>
        /// Create a PrimitiveVisual.
        /// </summary>
        /// <param name="type">the type of the shape</param>
        /// <param name="position">the position of the primitiveVisual</param>
        /// <returns>return a primitiveVisual</returns>
        private Tizen.NUI.PrimitiveVisual CreatePrimitiveVisual(PrimitiveVisualShapeType type, Vector2 position)
        {
            //Create the primitive visual.
            Tizen.NUI.PrimitiveVisual primitiveVisual = new Tizen.NUI.PrimitiveVisual();
            primitiveVisual.Shape = type;
            primitiveVisual.BevelPercentage = 0.3f;
            primitiveVisual.BevelSmoothness = 0.0f;
            primitiveVisual.ScaleDimensions = new Vector3(1.0f, 1.0f, 0.3f);
            primitiveVisual.MixColor = new Vector4((245.0f / 255.0f), (188.0f / 255.0f), (73.0f / 255.0f), 1.0f);
            primitiveVisual.Size = new Vector2(200.0f, 200.0f);
            primitiveVisual.Position = position;
            primitiveVisual.PositionPolicy = VisualTransformPolicyType.Absolute;
            primitiveVisual.SizePolicy = VisualTransformPolicyType.Absolute;
            primitiveVisual.Origin = Visual.AlignType.TopBegin;
            primitiveVisual.AnchorPoint = Visual.AlignType.TopBegin;
            return primitiveVisual;
        }


        private Tizen.NUI.VisualMap CreateVisualMap(string type, Vector2 position, string url)
        {
            //Create the visual map with position and url.
            VisualMap visualMap = null;
            Tizen.NUI.ImageVisual imageVisual = null;
            Tizen.NUI.ColorVisual colorVisual = null;
            Tizen.NUI.NPatchVisual nPatchVisual = null;
            Tizen.NUI.SVGVisual svgVisual = null;
            Tizen.NUI.AnimatedImageVisual animatedImageVisual = null;
            Tizen.NUI.BorderVisual borderVisual = null;
            Tizen.NUI.GradientVisual gradientVisual = null;
            switch (type)
            {
                case "Image":
                    imageVisual = new Tizen.NUI.ImageVisual();
                    imageVisual.URL = url;
                    visualMap = imageVisual;
                    break;
                case "Color":
                    colorVisual = new Tizen.NUI.ColorVisual();
                    colorVisual.Color = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
                    visualMap = colorVisual;
                    break;
                case "NPatch":
                    nPatchVisual = new Tizen.NUI.NPatchVisual();
                    nPatchVisual.URL = url;
                    visualMap = nPatchVisual;
                    break;
                case "SVG":
                    svgVisual = new Tizen.NUI.SVGVisual();
                    svgVisual.URL = url;
                    visualMap = svgVisual;
                    break;
                case "Animated":
                    animatedImageVisual = new Tizen.NUI.AnimatedImageVisual();
                    animatedImageVisual.URL = url;
                    visualMap = animatedImageVisual;
                    break;
                case "Border":
                    borderVisual = new Tizen.NUI.BorderVisual();
                    borderVisual.Color = Color.Black;
                    borderVisual.BorderSize = 5.0f;
                    visualMap = borderVisual;
                    break;
                case "Gradient":
                    gradientVisual = new Tizen.NUI.GradientVisual();
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
                    break;
                default:
                    imageVisual = new Tizen.NUI.ImageVisual();
                    imageVisual.URL = url;
                    visualMap = imageVisual;
                    break;
            }

            visualMap.Size = new Size2D(200, 200);
            visualMap.Position = position;
            visualMap.PositionPolicy = VisualTransformPolicyType.Absolute;
            visualMap.SizePolicy = VisualTransformPolicyType.Absolute;
            visualMap.Origin = Visual.AlignType.TopBegin;
            visualMap.AnchorPoint = Visual.AlignType.TopBegin;
            return visualMap;
        }

        /// <summary>
        /// Create a TextVisual.
        /// </summary>
        /// <param name="text">the text of the TextVisual</param>
        /// <param name="position">the position of the textVisual</param>
        /// <returns>return a text visual</returns>
        private Tizen.NUI.TextVisual CreateTextVisual(string text, Vector2 position)
        {
            //Create the text visual with position.
            Tizen.NUI.TextVisual textVisual = new Tizen.NUI.TextVisual();
            textVisual.Text = text;
            textVisual.PointSize = PointSize8;
            textVisual.FontFamily = "Samsung One 400";
            textVisual.Size = new Vector2(400.0f, 100.0f);
            textVisual.TextColor = Color.White;
            textVisual.Position = position;
            textVisual.PositionPolicy = VisualTransformPolicyType.Absolute;
            textVisual.SizePolicy = VisualTransformPolicyType.Absolute;
            textVisual.Origin = Visual.AlignType.TopBegin;
            textVisual.AnchorPoint = Visual.AlignType.TopBegin;
            textVisual.HorizontalAlignment = HorizontalAlignment.Begin;
            textVisual.VerticalAlignment = VerticalAlignment.Center;
            return textVisual;
        }

        /// <summary>
        /// Create an Text visual.
        /// </summary>
        /// <param name="text">The text of the Text visual</param>
        /// <param name="color">The color of the text</param>
        /// <returns>return a map which contain the properties of the text visual</returns>
        private PropertyMap CreateTextVisual(string text, Color color)
        {
            //Create the text visual with color.
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            map.Add(TextVisualProperty.Text, new PropertyValue(text));
            map.Add(TextVisualProperty.TextColor, new PropertyValue(color));
            map.Add(TextVisualProperty.PointSize, new PropertyValue(PointSize8));
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
            //Create the image visual with url.
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            map.Add(ImageVisualProperty.URL, new PropertyValue(imagePath));
            return map;
        }

        private Button CreateButton(string name, string text)
        {
            //Create the Button with name and text.
            Button button = new Button();
            button.Focusable = true;
            button.Size2D = new Size2D(440, 80);
            button.Focusable = true;
            button.Name = name;
            button.Text = text;
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
        /// Create the background image
        /// </summary>
        /// <returns>background view</returns>
        private View CreateBackGroundView()
        {
            //Create the back ground view.
            View view = new View();
            view.Name = "background";
            view.PositionUsesPivotPoint = true;
            view.PivotPoint = PivotPoint.TopLeft;
            view.ParentOrigin = ParentOrigin.TopLeft;
            view.HeightResizePolicy = ResizePolicyType.FillToParent;
            view.WidthResizePolicy = ResizePolicyType.FillToParent;
            return view;
        }

        /// <summary>
        /// Whether it is a SR emul
        /// </summary>
        /// <returns>If it is a SR emul, then return true</returns>
        public bool IsSREmul()
        {
            //Judge weather is SRE mul.
            int widthDpi = (int)Window.Instance.Dpi.Width;
            int heightDpi = (int)Window.Instance.Dpi.Height;
            if (widthDpi == 314 && heightDpi == 314)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Return the point size of 8
        /// </summary>
        public float PointSize8
        {
            get
            {
                if (IsSREmul())
                {
                    return 8.0f;
                }
                else
                {
                    return 34.0f;
                }
            }
        }

        /// <summary>
        /// Return the point size of 15
        /// </summary>
        public float PointSize15
        {
            get
            {
                if (IsSREmul())
                {
                    return 15.0f;
                }
                else
                {
                    return 65.0f;
                }
            }
        }
    }
}
