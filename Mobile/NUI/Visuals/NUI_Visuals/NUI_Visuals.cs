/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;


namespace NUI_Visuals
{
    class Program : NUIApplication
    {
        /// <summary>
        /// visual view with buttons at the bottom of the window
        /// </summary>
        private VisualView ButtonVisualView;
        /// <summary>
        /// visual view being the main part of the window - contains all elements
        /// of the CurrentVisualView (width = 3 * WindowWidth)
        /// </summary>
        private VisualView MainVisualView;
        /// <summary>
        /// list with visual views - the main part of the window being displayed
        /// after clicking the corresponding button
        /// </summary>
        private List<VisualView> CurrentVisualView = new List<VisualView>();

        /// <summary>
        /// the width of the window = CurrentWidth + 2 * FrameSize
        /// </summary>
        private int WindowWidth;
        /// <summary>
        /// width of the frame around buttons and around the currently visible visual view
        /// </summary>
        private int FrameSize = 20;
        /// <summary>
        /// ButtonVisualView height to MainVisualView height ratio
        /// </summary>
        private float ButtonToMainRatio = 0.1f;
        /// <summary>
        /// the width of the visual views being elements of the CurrentVisualView
        /// </summary>
        private int CurrentWidth;
        /// <summary>
        /// the height of the visual views being elements of the CurrentVisualView
        /// </summary>
        private int CurrentHeight;

        /// <summary>
        /// array of image visuals displayed after clicking the corresponding button
        /// </summary>
        private string[] ImageType = {"Image", "Animated", "SVG", "Mesh", "NPatch"};
        /// <summary>
        /// number of image visuals
        /// </summary>
        private uint ImageCount = 5;
        /// <summary>
        /// path to the folder with images
        /// </summary>
        private static string ImageUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/";
        /// <summary>
        /// relative width of each image (percentage part of the width of CurrentVisualView[i])
        /// </summary>
        private float ImageRelativeWidth = 0.395f;
        /// <summary>
        /// relative height of each image (percentage part of the height of CurrentVisualView[i])
        /// </summary>
        private float ImageRelativeHeight = 0.2f;

        /// <summary>
        /// array of primitive visuals displayed after clicking the corresponding button
        /// </summary>
        private string[] PrimitiveName = {"Sphere", "ConicalFrustrum", "Cone", "Cylinder", "BevelledCube", "Octahedron", "Cube"};
        /// <summary>
        /// number of primitive visuals
        /// </summary>
        private uint PrimitiveCount = 7;
        /// <summary>
        /// relative width of each primitive (percentage part of the width
        /// of CurrentVisualView[i])
        /// </summary>
        private float PrimitiveRelativeWidth = 0.4f;
        /// <summary>
        /// relative height of each primitive (percentage part of the height
        /// of CurrentVisualView[i])
        /// </summary>
        private float PrimitiveRelativeHeight = 0.13f;

        /// <summary>
        /// buttons associated with the visuals shown
        /// </summary>
        private List<Button> Buttons = new List<Button>();
        /// <summary>
        /// color of the button, which is associated with currently visible visuals
        /// </summary>
        private Color ActiveColor = new Color(0.66f, 0.6f, 0.9f, 1);
        /// <summary>
        /// color of the rest of the buttons
        /// </summary>
        private Color GrayColor = new Color(0.78f, 0.78f, 0.78f, 1);

        /// <summary>
        /// overridden method called when the app is launched
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        /// <summary>
        /// The method to create the TextVisual
        /// </summary>
        /// <param name="label"> The label of the Visual centered horizontally and
        /// vertically aligned to the top within the Visual's area </param>
        /// <param name="size"> The label's font size in points </param>
        /// <param name="relativePosition"> The relative position of the Visual
        /// determined as the shift of the anchor point (the visual's top center)
        /// from the origin (the parent's top begin) </param>
        /// <returns> TextVisual with given properties </returns>
        private TextVisual CreateTextVisual(string label, float size, RelativeVector2 relativePosition)
        {
            TextVisual LabelText = new TextVisual();
            LabelText.Text = label;
            LabelText.RelativePosition = relativePosition;
            LabelText.PointSize = size;
            LabelText.MultiLine = true;
            LabelText.FontFamily = "Arial";
            LabelText.HorizontalAlignment = HorizontalAlignment.Center;
            LabelText.VerticalAlignment = VerticalAlignment.Top;
            LabelText.Origin = Visual.AlignType.TopBegin;
            LabelText.AnchorPoint = Visual.AlignType.TopCenter;

            PropertyMap TextFontStyle = new PropertyMap();
            TextFontStyle.Add("weight", new PropertyValue("bold"));
            TextFontStyle.Add("slant", new PropertyValue("italic"));
            LabelText.FontStyle = TextFontStyle;

            return LabelText;
        }

        /// <summary>
        /// The method to create and set common properties for buttons
        /// </summary>
        /// <param name="label"> The button's label </param>
        /// <param name="xPosition"> The horizontal position of the button
        /// within the control </param>
        /// <param name="buttonSize">  The button size </param>
        /// <returns> Button with given properties </returns>
        private Button SetButton(string label, int xPosition, Size2D buttonSize)
        {
            Button ThisButton = new Button();

            ThisButton.Size = buttonSize;
            ThisButton.TextLabel.MultiLine = true;
            ThisButton.Position2D = new Position2D(xPosition, 0);
            ThisButton.BackgroundColor = new Color(0.78f, 0.78f, 0.78f, 1);
            ThisButton.PointSizeSelector = new FloatSelector
            {
                Other = 8,
                Pressed = 12
            };
            ThisButton.TextColorSelector = new ColorSelector
            {
                Other = new Color(0, 0, 1, 1),
                Pressed = new Color(1, 0, 0, 1)
            };
            ThisButton.TextSelector = new StringSelector
            {
                Other = label,
                Pressed = "PRESSED"
            };

            return ThisButton;
        }

        /// <summary>
        /// The method to change the clicked button color to ActiveColor
        /// and the other buttons to gray
        /// </summary>
        /// <param name="choosenButton"> The index of the active button </param>
        private void SetButtonsColors(int choosenButton)
        {
            for (var i = 0; i < Buttons.Count; ++i)
            {
                Buttons[i].BackgroundColor = GrayColor;
            }
            Buttons[choosenButton].BackgroundColor = ActiveColor;
        }



        /// <summary>
        /// Callback method invoked on button1 click. The method set the visibility
        /// width of the MainVisualView from 0 to WindowWidth
        /// </summary>
        /// <param name="sender">Object that invoked event</param>
        /// <param name="e">Event arguments</param>
        private void OnClickedButton1(object sender, EventArgs e)
        {
            SetButtonsColors(0);
            MainVisualView.Position2D = new Position2D(0, FrameSize);
        }

        /// <summary>
        /// Callback method invoked on button2 click. The method set the visibility width
        /// of the MainVisualView from WindowWidth to 2*WindowWidth
        /// </summary>
        /// <param name="sender">Object that invoked event</param>
        /// <param name="e">Event arguments</param>
        private void OnClickedButton2(object sender, EventArgs e)
        {
            SetButtonsColors(1);
            MainVisualView.Position2D = new Position2D(-WindowWidth, FrameSize);
        }

        /// <summary>
        /// Callback method invoked on button3 click. The method set the visibility width
        /// of the MainVisualView from 2*WindowWidth to 3*WindowWidth
        /// </summary>
        /// <param name="sender">Object that invoked event</param>
        /// <param name="e">Event arguments</param>
        private void OnClickedButton3(object sender, EventArgs e)
        {
            SetButtonsColors(2);
            MainVisualView.Position2D = new Position2D(-2 * WindowWidth, FrameSize);
        }

        /// <summary>
        /// The method to fill the list of buttons, to add buttons to their
        /// view and to connect buttons' clicked event with the proper function call
        /// </summary>
        private void InitializeButtons()
        {
            int ButtonWidth = (int)((ButtonVisualView.Size2D[0] - 2 * FrameSize) / 3);
            int ButtonHeight = (int)ButtonVisualView.Size2D[1];
            Size2D ButtonSize = new Size2D(ButtonWidth, ButtonHeight);

            Buttons.Add(SetButton("Color Border Gradient", 0, ButtonSize));
            Buttons.Add(SetButton("Images", (ButtonWidth + FrameSize), ButtonSize));
            Buttons.Add(SetButton("Primitives", 2 * (ButtonWidth + FrameSize), ButtonSize));

            for (var i = 0; i < Buttons.Count; i++)
            {
                ButtonVisualView.Add(Buttons[i]);
            }

            Buttons[0].Clicked += OnClickedButton1;
            Buttons[1].Clicked += OnClickedButton2;
            Buttons[2].Clicked += OnClickedButton3;
        }

        /// <summary>
        /// The method to create the PrimitiveVisual - the properties for a given
        /// primitive are set in the switch-case block, properties common for
        /// all primitives are set outside the block (except MixColor property,
        /// which is set for some primitives individually)
        /// </summary>
        /// <param name="primitiveName"> Then name of the PrimitiveVisual </param>
        /// <returns> The created PrimitiveVisual </returns>
        private PrimitiveVisual CreatePrimitiveVisual(string primitiveName)
        {
            PrimitiveVisual ThisPrimitiveVisual = new PrimitiveVisual();

            ThisPrimitiveVisual.MixColor = new Color(0.6f, 0.4f, 1.0f, 1.0f);
            ThisPrimitiveVisual.LightPosition = new Vector3(0.0f,0.0f,300.0f);
            ThisPrimitiveVisual.Slices = 100;
            ThisPrimitiveVisual.Stacks = 100;

            switch (primitiveName)
            {
                case "Sphere":
                    ThisPrimitiveVisual.Shape = PrimitiveVisualShapeType.Sphere;
                    break;

                case "ConicalFrustrum":
                    ThisPrimitiveVisual.Shape = PrimitiveVisualShapeType.ConicalFrustrum;
                    ThisPrimitiveVisual.ScaleHeight = 2.0f;
                    ThisPrimitiveVisual.ScaleTopRadius = 0.3f;
                    ThisPrimitiveVisual.ScaleBottomRadius = 1.0f;
                    ThisPrimitiveVisual.MixColor = Color.Green;
                    break;

                case "Cone":
                    ThisPrimitiveVisual.Shape = PrimitiveVisualShapeType.Cone;
                    ThisPrimitiveVisual.ScaleHeight = 2.0f;
                    ThisPrimitiveVisual.ScaleBottomRadius = 1.0f;
                    ThisPrimitiveVisual.MixColor = new Color(0.4f, 0.4f, 1.0f, 1.0f);
                    break;

                case "Cylinder":
                    ThisPrimitiveVisual.Shape = PrimitiveVisualShapeType.Cylinder;
                    ThisPrimitiveVisual.ScaleHeight = 1.0f;
                    ThisPrimitiveVisual.ScaleRadius = 0.5f;
                    break;

                case "Cube":
                    ThisPrimitiveVisual.Shape = PrimitiveVisualShapeType.Cube;
                    ThisPrimitiveVisual.ScaleDimensions = new Vector3(1.0f, 0.4f, 0.8f);
                    ThisPrimitiveVisual.MixColor = new Color(0.4f, 0.4f, 1.0f, 1.0f);
                    break;

                case "Octahedron":
                    ThisPrimitiveVisual.Shape = PrimitiveVisualShapeType.Octahedron;
                    ThisPrimitiveVisual.ScaleDimensions = new Vector3(1.0f, 0.7f, 1.0f);
                    ThisPrimitiveVisual.MixColor = Color.Green;
                    break;

                case "BevelledCube":
                    ThisPrimitiveVisual.Shape = PrimitiveVisualShapeType.BevelledCube;
                    ThisPrimitiveVisual.ScaleDimensions = new Vector3(0.0f, 0.5f, 1.1f);
                    ThisPrimitiveVisual.BevelPercentage = 0.3f;
                    ThisPrimitiveVisual.BevelSmoothness = 0.0f;
                    break;
            }

            return ThisPrimitiveVisual;
        }

        /// <summary>
        /// The method to create visual map for different kinds of primitives
        /// </summary>
        /// <param name="visualType"> The Visual type </param>
        /// <returns> The VisualMap to the Visual of the given type </returns>
        private VisualMap CreateVisualMap(string visualType)
        {
            VisualMap ThisVisualMap = null;

            switch (visualType)
            {
                case "Border":
                    BorderVisual ThisBorderVisual = new BorderVisual();
                    /// obligatory properties
                    ThisBorderVisual.Color = Color.Blue;
                    ThisBorderVisual.BorderSize = 15.0f;
                    ThisVisualMap = ThisBorderVisual;
                    break;

                case "Color":
                    ColorVisual ThisColorVisual = new ColorVisual();
                    /// obligatory properties
                    ThisColorVisual.MixColor = new Color(0.2f, 0.0f, 1.0f, 0.7f);
                    /// optional properties
                    ThisColorVisual.CornerRadius = 35.0f;
                    ThisVisualMap = ThisColorVisual;
                    break;

                case "RadialGradient":
                    GradientVisual ThisRadialGradientVisual = new GradientVisual();
                    /// obligatory properties
                    /// coordinate system: top-left - (-0.5,-0.5); bottom-right - (0.5,0.5)
                    ThisRadialGradientVisual.Center = new Vector2(0.0f, 0.0f);
                    ThisRadialGradientVisual.Radius = 0.9f;
                    /// optional properties
                    PropertyArray ThisStopColor = new PropertyArray();
                    ThisStopColor.Add(new PropertyValue(Color.Yellow));
                    ThisStopColor.Add(new PropertyValue(Color.Blue));
                    ThisStopColor.Add(new PropertyValue(new Color(0.0f, 1.0f, 0.0f, 1.0f)));
                    ThisStopColor.Add(new PropertyValue(new Vector4(120.0f, 0.0f, 255.0f, 255.0f) / 255.0f));
                    ThisRadialGradientVisual.StopColor = ThisStopColor;
                    PropertyArray ThisStopOffset = new PropertyArray();
                    ThisStopOffset.Add(new PropertyValue(0.0f));
                    ThisStopOffset.Add(new PropertyValue(0.2f));
                    ThisStopOffset.Add(new PropertyValue(0.4f));
                    ThisStopOffset.Add(new PropertyValue(0.6f));
                    ThisRadialGradientVisual.StopOffset = ThisStopOffset;
                    ThisVisualMap = ThisRadialGradientVisual;
                    break;

                case "LinearGradient":
                    GradientVisual ThisLinearGradientVisual = new GradientVisual();
                    /// obligatory properties
                    /// coordinate system: top-left - (-0.5,-0.5); bottom-right - (0.5,0.5)
                    ThisLinearGradientVisual.StartPosition = new Vector2(-0.5f, 0.5f);
                    ThisLinearGradientVisual.EndPosition = new Vector2(0.5f, -0.5f);
                    /// optional properties
                    ThisLinearGradientVisual.StopColor = new PropertyArray();
                    ThisLinearGradientVisual.StopColor.Add(new PropertyValue(Color.Green));
                    ThisLinearGradientVisual.StopColor.Add(new PropertyValue(Color.Blue));
                    ThisVisualMap = ThisLinearGradientVisual;
                    break;

                case "Image":
                    ImageVisual ThisImageVisual = new ImageVisual();
                    /// obligatory properties
                    ThisImageVisual.URL = ImageUrl + "belt.jpg";
                    /// optional properties
                    ThisImageVisual.Origin = Visual.AlignType.TopBegin;
                    ThisImageVisual.AnchorPoint = Visual.AlignType.TopBegin;
                    ThisImageVisual.RelativePosition = new RelativeVector2(0.1f, 0.1f);
                    ThisVisualMap = ThisImageVisual;
                    break;

                case "NPatch":
                    NPatchVisual ThisNPatchVisual = new NPatchVisual();
                    /// obligatory properties
                    ThisNPatchVisual.URL = ImageUrl + "heartsframe.png";
                    /// optional properties (for all visual types)
                    ThisNPatchVisual.Origin = Visual.AlignType.Center;
                    ThisNPatchVisual.AnchorPoint = Visual.AlignType.Center;
                    ThisNPatchVisual.RelativePosition = new RelativeVector2(0.0f, 0.0f);
                    ThisVisualMap = ThisNPatchVisual;
                    break;

                case "SVG":
                    SVGVisual ThisSvgVisual = new SVGVisual();
                    /// obligatory properties
                    ThisSvgVisual.URL = ImageUrl + "tiger.svg";
                    /// optional properties (for all visual types)
                    ThisSvgVisual.Origin = Visual.AlignType.BottomBegin;
                    ThisSvgVisual.AnchorPoint = Visual.AlignType.BottomBegin;
                    ThisSvgVisual.RelativePosition = new RelativeVector2(0.1f, -0.1f);
                    ThisVisualMap = ThisSvgVisual;
                    break;

                case "Animated":
                    AnimatedImageVisual ThisAnimatedVisual = new AnimatedImageVisual();
                    /// obligatory properties
                    ThisAnimatedVisual.URL = ImageUrl + "buble.gif";
                    /// optional properties (for all visual types)
                    ThisAnimatedVisual.Origin = Visual.AlignType.TopEnd;
                    ThisAnimatedVisual.AnchorPoint = Visual.AlignType.TopEnd;
                    ThisAnimatedVisual.RelativePosition = new RelativeVector2(-0.1f, 0.1f);
                    ThisVisualMap = ThisAnimatedVisual;
                    break;

                case "Mesh":
                    MeshVisual ThisMeshVisual = new MeshVisual();
                    /// obligatory properties
                    ThisMeshVisual.ObjectURL = ImageUrl + "Dino.obj";
                    ThisMeshVisual.MaterialtURL = ImageUrl + "Dino.mtl";
                    ThisMeshVisual.TexturesPath = ImageUrl + "textures/";
                    /// optional properties (for all visual map types)
                    ThisMeshVisual.Origin = Visual.AlignType.BottomEnd;
                    ThisMeshVisual.AnchorPoint = Visual.AlignType.BottomEnd;
                    ThisMeshVisual.RelativePosition = new RelativeVector2(0, -0.03f);
                    ThisVisualMap = ThisMeshVisual;
                    break;
            }

            /// properties common for visuals groups
            switch (visualType)
            {
                case "Border":
                case "Color":
                case "RadialGradient":
                case "LinearGradient":
                    ThisVisualMap.RelativeSize = new RelativeVector2(0.3f, 0.2f);
                    ThisVisualMap.Origin = Visual.AlignType.TopBegin;
                    ThisVisualMap.AnchorPoint = Visual.AlignType.TopBegin;
                    break;

                case "Image":
                case "NPatch":
                case "SVG":
                case "Animated":
                    ThisVisualMap.RelativeSize = new RelativeVector2(ImageRelativeWidth, ImageRelativeHeight);
                    break;

                case "Mesh":
                    ThisVisualMap.RelativeSize = new RelativeVector2(2 * ImageRelativeWidth, 2 * ImageRelativeHeight);
                    break;

                case "Sphere":
                case "ConicalFrustrum":
                case "Cone":
                case "Cylinder":
                case "BevelledCube":
                case "Octahedron":
                case "Cube":
                    ThisVisualMap = CreatePrimitiveVisual(visualType);
                    ThisVisualMap.RelativeSize = new RelativeVector2(PrimitiveRelativeWidth, PrimitiveRelativeHeight);
                    ThisVisualMap.Origin = Visual.AlignType.TopCenter;
                    ThisVisualMap.AnchorPoint = Visual.AlignType.Center;
                    break;
            }

            return ThisVisualMap;
        }

        /// <summary>
        /// The method to set the visual view that is visible after clicking
        /// the button1 - contains visuals text, border, color, gradient
        /// </summary>
        /// <returns> The created VisualView </returns>
        private VisualView CreateVisualView1()
        {
            VisualView CurrentVisualView = new VisualView();
            CurrentVisualView.Size2D = new Size2D(CurrentWidth, CurrentHeight);
            CurrentVisualView.ParentOrigin = ParentOrigin.TopLeft;;
            CurrentVisualView.PositionUsesPivotPoint = true;
            CurrentVisualView.PivotPoint = PivotPoint.TopLeft;
            CurrentVisualView.Position2D = new Position2D(FrameSize, 0);
            CurrentVisualView.BackgroundColor = Color.White;

            VisualMap ThisVisualMap = null;
            string VisualType;

            /// the main title
            ThisVisualMap = CreateTextVisual("VISUALS", 20.0f, new RelativeVector2(0.5f, 0.0f));
            CurrentVisualView.AddVisual("TextVisuals", ThisVisualMap);

            /// border visual and its title
            VisualType = "Border";
            ThisVisualMap = CreateVisualMap(VisualType);
            ThisVisualMap.RelativePosition = new RelativeVector2(0.025f, 0.2f);
            CurrentVisualView.AddVisual(VisualType, ThisVisualMap);

            ThisVisualMap = CreateTextVisual(VisualType, 13.0f, new RelativeVector2(0.175f, 0.4f));
            CurrentVisualView.AddVisual("Text" + VisualType, ThisVisualMap);

            /// border visual - underneath the previous one
            BorderVisual ThisBorderVisual = (BorderVisual)CreateVisualMap(VisualType);
            ThisBorderVisual.Color = Color.Green;
            ThisBorderVisual.RelativePosition = new RelativeVector2(0.045f, 0.18f);
            ThisBorderVisual.DepthIndex = ThisVisualMap.DepthIndex - 1;
            CurrentVisualView.AddVisual(VisualType + "2", ThisBorderVisual);

            /// color visual and its title
            VisualType = "Color";
            ThisVisualMap = CreateVisualMap(VisualType);
            ThisVisualMap.RelativePosition = new RelativeVector2(0.675f, 0.2f);
            CurrentVisualView.AddVisual(VisualType, ThisVisualMap);

            ThisVisualMap = CreateTextVisual(VisualType, 13.0f, new RelativeVector2(0.825f, 0.4f));
            CurrentVisualView.AddVisual("Text" + VisualType, ThisVisualMap);

            /// radial gradient 1
            VisualType = "Gradient";
            ThisVisualMap = CreateVisualMap("Radial" + VisualType);
            ThisVisualMap.RelativePosition = new RelativeVector2(0.025f, 0.6f);
            CurrentVisualView.AddVisual("Radial" + VisualType + "1", ThisVisualMap);

            /// linear gradient and the title
            ThisVisualMap = CreateVisualMap("Linear" + VisualType);
            ThisVisualMap.RelativePosition = new RelativeVector2(0.350f, 0.6f);
            CurrentVisualView.AddVisual("Linear" + VisualType, ThisVisualMap);

            ThisVisualMap = CreateTextVisual(VisualType, 13.0f, new RelativeVector2(0.5f, 0.8f));
            CurrentVisualView.AddVisual("Text" + VisualType, ThisVisualMap);

            /// radial gradient 2
            GradientVisual ThisGradientVisual = (GradientVisual)CreateVisualMap("Radial" + VisualType);
            ThisGradientVisual.Center = new Vector2(0.2f, 0.4f);
            ThisGradientVisual.Radius = 0.2f;
            ThisGradientVisual.StopColor = new PropertyArray();
            ThisGradientVisual.StopColor.Add(new PropertyValue(Color.Blue));
            ThisGradientVisual.StopColor.Add(new PropertyValue(Color.Green));
            ThisGradientVisual.SpreadMethod = GradientVisualSpreadMethodType.Repeat;
            ThisGradientVisual.RelativePosition = new RelativeVector2(0.675f, 0.6f);
            CurrentVisualView.AddVisual("Radial" + VisualType + "2", ThisGradientVisual);

            /// CurrentVisualView added to the MainVisualView
            MainVisualView.Add(CurrentVisualView);
            return CurrentVisualView;
        }

        /// <summary>
        /// The method to set visual view visible after clicking the button2
        /// - contains visuals images
        /// </summary>
        /// <returns> The created VisualView </returns>
        private VisualView CreateVisualView2()
        {
            VisualView CurrentVisualView = new VisualView();
            CurrentVisualView.Size2D = new Size2D(CurrentWidth, CurrentHeight);
            CurrentVisualView.ParentOrigin = ParentOrigin.TopLeft;;
            CurrentVisualView.PositionUsesPivotPoint = true;
            CurrentVisualView.PivotPoint = PivotPoint.TopLeft;
            CurrentVisualView.Position2D = new Position2D(WindowWidth + FrameSize, 0);
            CurrentVisualView.BackgroundColor = Color.White;

            VisualMap ThisVisualMap = null;

            /// the main title
            ThisVisualMap = CreateTextVisual("VISUALS", 20.0f, new RelativeVector2(0.5f, 0.0f));
            CurrentVisualView.AddVisual("TextVisuals", ThisVisualMap);

            RelativeVector2[] ImageTextRelativePosition = {new RelativeVector2(0.3f,0.3f),
                                                           new RelativeVector2(0.7f,0.3f),
                                                           new RelativeVector2(0.3f,0.9f),
                                                           new RelativeVector2(0.7f,0.9f),
                                                           new RelativeVector2(0.5f,0.6f)};

            for (uint i = 0; i < ImageCount; ++i)
            {
                ThisVisualMap = CreateVisualMap(ImageType[i]);
                /// to show how nice NPatch can be stretched
                if (ImageType[i] == "NPatch")
                {
                    ThisVisualMap.RelativeSize = new RelativeVector2(2 * ImageRelativeWidth, ImageRelativeHeight);
                }
                CurrentVisualView.AddVisual(ImageType[i], ThisVisualMap);
                CurrentVisualView.AddVisual("Text" + ImageType[i], CreateTextVisual(ImageType[i], 9.0f, ImageTextRelativePosition[i]));
            }

            MainVisualView.Add(CurrentVisualView);
            return CurrentVisualView;
        }

        /// <summary>
        /// The method to set the visual view visible after clicking the button3
        /// - contains visuals primitives
        /// </summary>
        /// <returns> The created VisualView </returns>
        private VisualView CreateVisualView3()
        {
            VisualView CurrentVisualView = new VisualView();
            CurrentVisualView.Size2D = new Size2D(CurrentWidth, CurrentHeight);
            CurrentVisualView.ParentOrigin = ParentOrigin.TopLeft;;
            CurrentVisualView.PositionUsesPivotPoint = true;
            CurrentVisualView.PivotPoint = PivotPoint.TopLeft;
            CurrentVisualView.Position2D = new Position2D(2 * WindowWidth + FrameSize, 0);
            CurrentVisualView.BackgroundColor = Color.White;

            VisualMap ThisVisualMap = null;

            /// the main title
            ThisVisualMap = CreateTextVisual("VISUALS", 20.0f, new RelativeVector2(0.5f, 0.0f));
            CurrentVisualView.AddVisual("TextVisuals", ThisVisualMap);

            float DeltaWidth = (1.0f + PrimitiveRelativeWidth) / 6.0f;
            float DeltaHeight = (float)((0.9f - Math.Ceiling(PrimitiveCount / 2.0f) * PrimitiveRelativeHeight) / (1.0f + Math.Ceiling(PrimitiveCount / 2.0f)));
            RelativeVector2 VisualRelativePosition = new RelativeVector2(-DeltaWidth, 0.1f + DeltaHeight);

            for (uint i = 0; i < PrimitiveCount; ++i)
            {
                ThisVisualMap = CreateVisualMap(PrimitiveName[i]);
                ThisVisualMap.RelativePosition = VisualRelativePosition + new RelativeVector2(0, 0.06f);
                CurrentVisualView.AddVisual(PrimitiveName[i], ThisVisualMap);
                CurrentVisualView.AddVisual("Text" + PrimitiveName[i], CreateTextVisual(PrimitiveName[i], 9.0f, VisualRelativePosition + new RelativeVector2(0.5f, PrimitiveRelativeHeight)));
                if (i % 2 == 0)
                {
                    VisualRelativePosition += new RelativeVector2(2.0f * DeltaWidth, (DeltaHeight + PrimitiveRelativeHeight) / 2.0f);
                }
                else
                {
                    VisualRelativePosition += new RelativeVector2(-2.0f * DeltaWidth, (DeltaHeight + PrimitiveRelativeHeight) / 2.0f);
                }
            }

            MainVisualView.Add(CurrentVisualView);
            return CurrentVisualView;
        }

        void Initialize()
        {
            Window.Instance.KeyEvent += OnKeyEvent;
            Window.Instance.BackgroundColor = Color.Blue;
            Size2D WindowinSize = Window.Instance.WindowSize;
            WindowWidth = (int)WindowinSize[0];
            int WindowHeight = (int)WindowinSize[1];


            /// the buttons visual view
            ButtonVisualView = new VisualView();
            ButtonVisualView.Size2D = new Size2D((int)(WindowWidth - 2 * FrameSize), (int)((WindowHeight - 3 * FrameSize) * ButtonToMainRatio));
            ButtonVisualView.ParentOrigin = ParentOrigin.BottomLeft;
            ButtonVisualView.PositionUsesPivotPoint = true;
            ButtonVisualView.PivotPoint = PivotPoint.BottomLeft;
            ButtonVisualView.Position2D = new Position2D(FrameSize, -FrameSize);
            InitializeButtons();
            Buttons[0].BackgroundColor = new Color(0.66f, 0.6f, 0.9f, 1);
            Window.Instance.Add(ButtonVisualView);

            /// the main visual view
            MainVisualView = new VisualView();
            CurrentWidth = WindowWidth - 2 * FrameSize;
            CurrentHeight = (int)((WindowHeight - 3 * FrameSize) * (1 - ButtonToMainRatio));
            MainVisualView.Size2D = new Size2D(3 * WindowWidth, CurrentHeight);
            MainVisualView.ParentOrigin = ParentOrigin.TopLeft;;
            MainVisualView.PositionUsesPivotPoint = true;
            MainVisualView.PivotPoint = PivotPoint.TopLeft;
            MainVisualView.Position2D = new Position2D(0, FrameSize);
            MainVisualView.BackgroundColor = Color.Blue;
            Window.Instance.Add(MainVisualView);

            CurrentVisualView.Add(CreateVisualView1());
            CurrentVisualView.Add(CreateVisualView2());
            CurrentVisualView.Add(CreateVisualView3());

        }

        /// <summary>
        /// The method called when key pressed down
        /// </summary>
        /// <param name="sender">Key instance</param>
        /// <param name="e">Key's args</param>
        private void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "Escape" ||
                e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "BackSpace"))
            {
                Exit();
            }
        }

        static void Main(string[] args)
        {
            var App = new Program();
            App.Run(args);
            App.Dispose();
        }
    }
}
