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

namespace VisualSample
{
    /// <summary>
    /// This sample application demonstrates many kinds of visuals.
    /// </summary>
    class VisualSample : NUIApplication
    {
        // The text to mark visual.
        private TextLabel[] text;
        // A visual view be used to added some visuals on it.
        private VisualView _visualView;
        // A visual view be used to added svg visual on it.
        private VisualView _svgVisuallView;
        // The source of images.
        private const String resources = "/home/owner/apps_rw/org.tizen.example.VisualSample/res/images/";
        private String image_jpg = resources + "gallery-1.jpg";
        private String image_gif = resources + "dog-anim.gif";
        private String image_svg = resources + "Kid1.svg";
        private String image_9patch = resources + "focus_grid.9.png";

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
        /// Visual Sample Application initialisation.
        /// </summary>
        private void Initialize()
        {
            Window.Instance.BackgroundColor = Color.White;
            // Show Visuals Type.
            // There are 11 Visuals will be marked with text
            // Color visual will be used to _visualView's Background
            // TextVisual don't need use it.
            text = new TextLabel[11];

            // Create a visual view.
            // Make it fill to Window.
            // Visuals will put on it.
            _visualView = new VisualView();
            _visualView.PositionUsesPivotPoint = true;
            _visualView.ParentOrigin = ParentOrigin.TopLeft;
            _visualView.PivotPoint = PivotPoint.TopLeft;
            _visualView.Size2D = Window.Instance.Size;

            // color visual.
            // color.R/G/B/A range 0 - 1(contain 0 and 1)
            // Renders a color to the visual's quad geometry.
            ColorVisual colorVisual = new ColorVisual();
            colorVisual.Color = new Color(0.8f, 0.8f, 0.8f, 1.0f);
            _visualView.Background = colorVisual.OutputVisualMap;
            Window.Instance.GetDefaultLayer().Add(_visualView);

            // Create the text mark normal image visual.
            text[0] = CreateTextLabel("normal image visual", new Position2D(20, 20));
            Window.Instance.GetDefaultLayer().Add(text[0]);
            // normal image visual.
            // Renders a raster image ( jpg, png etc.) into the visual's quad geometry.
            ImageVisual imageVisual = new ImageVisual();
            imageVisual.URL = image_jpg;
            imageVisual.Size = new Size2D(200, 200);
            imageVisual.Position = new Vector2(20.0f, 120.0f);
            imageVisual.PositionPolicy = VisualTransformPolicyType.Absolute;
            imageVisual.SizePolicy = VisualTransformPolicyType.Absolute;
            imageVisual.Origin = Visual.AlignType.TopBegin;
            imageVisual.AnchorPoint = Visual.AlignType.TopBegin;
            _visualView.AddVisual("imageVisual", imageVisual);

            // Create the text mark normal image visual.
            text[1] = CreateTextLabel("svg image visual", new Position2D(340, 20));
            Window.Instance.GetDefaultLayer().Add(text[1]);
            // svg image visual.
            // Renders a svg image into the visual's quad geometry
            _svgVisuallView = new VisualView();
            _svgVisuallView.Size2D = new Vector2(200, 200);
            _svgVisuallView.PositionUsesPivotPoint = true;
            _svgVisuallView.ParentOrigin = ParentOrigin.TopLeft;
            _svgVisuallView.PivotPoint = PivotPoint.TopLeft;
            _svgVisuallView.Position = new Position(340, 120, 0);
            Window.Instance.GetDefaultLayer().Add(_svgVisuallView);
            SVGVisual svgVisual = new SVGVisual();
            svgVisual.URL = image_svg;
            svgVisual.Size = new Vector2(200, 200);
            svgVisual.PositionPolicy = VisualTransformPolicyType.Absolute;
            svgVisual.SizePolicy = VisualTransformPolicyType.Absolute;
            svgVisual.Origin = Visual.AlignType.Center;
            svgVisual.AnchorPoint = Visual.AlignType.TopBegin;
            _svgVisuallView.AddVisual("svgVisual", svgVisual);

            // Create the text mark npatch image visual.
            text[2] = CreateTextLabel("npatch image visual", new Position2D(680, 20));
            Window.Instance.GetDefaultLayer().Add(text[2]);
            // n patch image visual.
            // Renders an npatch or a 9patch image. Uses nonquad
            // geometry. Both geometry and texture are cached to
            // reduce memory consumption if the same npatch image
            // is used elsewhere.
            NPatchVisual nPatchVisual = new NPatchVisual();
            nPatchVisual.URL = image_9patch;
            nPatchVisual.Size = new Vector2(200, 200);
            nPatchVisual.Position = new Vector2(680, 120);
            nPatchVisual.PositionPolicy = VisualTransformPolicyType.Absolute;
            nPatchVisual.SizePolicy = VisualTransformPolicyType.Absolute;
            nPatchVisual.Origin = Visual.AlignType.TopBegin;
            nPatchVisual.AnchorPoint = Visual.AlignType.TopBegin;
            _visualView.AddVisual("nPatchVisual", nPatchVisual);

            // Create the text mark animated image visual.
            text[3] = CreateTextLabel("animated image visual", new Position2D(1000, 20));
            Window.Instance.GetDefaultLayer().Add(text[3]);
            // animated image visual.
            // Renders an animated image into the visual's quad geometry.
            // Currently, only the GIF format is supported.
            AnimatedImageVisual animatedImageVisual = new AnimatedImageVisual();
            animatedImageVisual.URL = image_gif;
            animatedImageVisual.Size = new Size2D(200, 200);
            animatedImageVisual.Position = new Vector2(1000.0f, 120.0f);
            animatedImageVisual.PositionPolicy = VisualTransformPolicyType.Absolute;
            animatedImageVisual.SizePolicy = VisualTransformPolicyType.Absolute;
            animatedImageVisual.Origin = Visual.AlignType.TopBegin;
            animatedImageVisual.AnchorPoint = Visual.AlignType.TopBegin;
            _visualView.AddVisual("animatedImageVisual", animatedImageVisual);

            // text visual.
            TextVisual textVisual = new TextVisual();
            textVisual.Text = "This is a TextVisual";
            textVisual.PointSize = 5.0f;
            textVisual.Size = new Vector2(400.0f, 100.0f);
            textVisual.Position = new Vector2(1150.0f, 60.0f);
            textVisual.PositionPolicy = VisualTransformPolicyType.Absolute;
            textVisual.SizePolicy = VisualTransformPolicyType.Absolute;
            textVisual.Origin = Visual.AlignType.TopBegin;
            textVisual.AnchorPoint = Visual.AlignType.TopBegin;
            textVisual.HorizontalAlignment = HorizontalAlignment.Center;
            _visualView.AddVisual("textVisual", textVisual);

            // Create the text mark border visual.
            text[4] = CreateTextLabel("border visual", new Position2D(20, 450));
            Window.Instance.GetDefaultLayer().Add(text[4]);
            // borderVisual
            // Renders a color as an internal border to the visual's geometry.
            BorderVisual borderVisual = new BorderVisual();
            borderVisual.Color = Color.Red;
            borderVisual.BorderSize = 5.0f;
            borderVisual.Size = new Vector2(200.0f, 200.0f);
            borderVisual.Position = new Vector2(20.0f, 550.0f);
            borderVisual.PositionPolicy = VisualTransformPolicyType.Absolute;
            borderVisual.SizePolicy = VisualTransformPolicyType.Absolute;
            borderVisual.Origin = Visual.AlignType.TopBegin;
            borderVisual.AnchorPoint = Visual.AlignType.TopBegin;
            _visualView.AddVisual("borderVisual", borderVisual);

            // Create the text mark gradient visual.
            text[5] = CreateTextLabel("gradient visual", new Position2D(240, 450));
            Window.Instance.GetDefaultLayer().Add(text[5]);
            // gradient visual
            // Renders a smooth transition of colors to the visual's quad geometry.
            // Both Linear and Radial gradients are supported.
            GradientVisual gradientVisual = new GradientVisual();
            // Create the PropertyArray of stopOffset.
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
            gradientVisual.StartPosition = new Vector2(0.5f,  0.5f);
            // Set the end position of a linear gradient.
            gradientVisual.EndPosition = new Vector2(-0.5f, -0.5f);
            // Set the center point of a radial gradient.
            gradientVisual.Center = new Vector2(0.5f, 0.5f);
            // Set the size of the radius of a radial gradient.
            gradientVisual.Radius = 1.414f;
            gradientVisual.Size = new Vector2(200.0f, 200.0f);
            gradientVisual.Position = new Vector2(240.0f, 550.0f);
            gradientVisual.PositionPolicy = VisualTransformPolicyType.Absolute;
            gradientVisual.SizePolicy = VisualTransformPolicyType.Absolute;
            gradientVisual.Origin = Visual.AlignType.TopBegin;
            gradientVisual.AnchorPoint = Visual.AlignType.TopBegin;
            _visualView.AddVisual("gradientVisual1", gradientVisual);

            // Create the text mark gradient visual.
            text[6] = CreateTextLabel("primitive visual: Cone", new Position2D(460, 450));
            Window.Instance.GetDefaultLayer().Add(text[6]);
            // primitive visual: Cone
            // Renders a simple 3D shape, such as a cube or sphere. Scaled to fit the control.
            // The shapes are generated with clockwise winding and backface culling on by default.
            // Cone : Equivalent to a conical frustrum with top radius of zero.
            PrimitiveVisual primitiveVisual1 = new PrimitiveVisual();
            primitiveVisual1.Shape = PrimitiveVisualShapeType.Cone;
            primitiveVisual1.BevelPercentage = 0.3f;
            primitiveVisual1.BevelSmoothness = 0.0f;
            primitiveVisual1.ScaleDimensions = new Vector3(1.0f,1.0f,0.3f);
            primitiveVisual1.MixColor = new Vector4((245.0f / 255.0f), (188.0f / 255.0f), (73.0f / 255.0f), 1.0f);
            primitiveVisual1.Size = new Vector2(200.0f, 200.0f);
            primitiveVisual1.Position = new Vector2(460.0f, 550.0f);
            primitiveVisual1.PositionPolicy = VisualTransformPolicyType.Absolute;
            primitiveVisual1.SizePolicy = VisualTransformPolicyType.Absolute;
            primitiveVisual1.Origin = Visual.AlignType.TopBegin;
            primitiveVisual1.AnchorPoint = Visual.AlignType.TopBegin;
            _visualView.AddVisual("primitiveVisual1", primitiveVisual1);

            // Create the text mark primitive visual: Sphere.
            text[7] = CreateTextLabel("primitive visual: Sphere", new Position2D(680, 450));
            Window.Instance.GetDefaultLayer().Add(text[7]);
            // primitive visual: Sphere
            // Sphere : Default.
            PrimitiveVisual primitiveVisual2 = new PrimitiveVisual();
            primitiveVisual2.Shape = PrimitiveVisualShapeType.Sphere;
            primitiveVisual2.BevelPercentage = 0.3f;
            primitiveVisual2.BevelSmoothness = 0.0f;
            primitiveVisual2.ScaleDimensions = new Vector3(1.0f,1.0f,0.3f);
            primitiveVisual2.MixColor = new Vector4((245.0f / 255.0f), (188.0f / 255.0f), (73.0f / 255.0f), 1.0f);
            primitiveVisual2.Size = new Vector2(200.0f, 200.0f);
            primitiveVisual2.Position = new Vector2(680.0f, 550.0f);
            primitiveVisual2.PositionPolicy = VisualTransformPolicyType.Absolute;
            primitiveVisual2.SizePolicy = VisualTransformPolicyType.Absolute;
            primitiveVisual2.Origin = Visual.AlignType.TopBegin;
            primitiveVisual2.AnchorPoint = Visual.AlignType.TopBegin;
            _visualView.AddVisual("primitiveVisual2", primitiveVisual2);

            // Create the text mark primitive visual: Cylinder.
            text[8] = CreateTextLabel("primitive visual: Cylinder", new Position2D(1000, 450));
            Window.Instance.GetDefaultLayer().Add(text[8]);
            // primitive visual: Cylinder
            // Cylinder : Equivalent to a conical frustrum with
            // equal radii for the top and bottom circles.
            PrimitiveVisual primitiveVisual3 = new PrimitiveVisual();
            primitiveVisual3.Shape = PrimitiveVisualShapeType.Cylinder;
            primitiveVisual3.BevelPercentage = 0.3f;
            primitiveVisual3.BevelSmoothness = 0.0f;
            primitiveVisual3.ScaleDimensions = new Vector3(1.0f,1.0f,0.3f);
            primitiveVisual3.MixColor = new Vector4((245.0f / 255.0f), (188.0f / 255.0f), (73.0f / 255.0f), 1.0f);
            primitiveVisual3.Size = new Vector2(200.0f, 200.0f);
            primitiveVisual3.Position = new Vector2(1000.0f, 550.0f);
            primitiveVisual3.PositionPolicy = VisualTransformPolicyType.Absolute;
            primitiveVisual3.SizePolicy = VisualTransformPolicyType.Absolute;
            primitiveVisual3.Origin = Visual.AlignType.TopBegin;
            primitiveVisual3.AnchorPoint = Visual.AlignType.TopBegin;
            _visualView.AddVisual("primitiveVisual3", primitiveVisual3);

            // Create the text mark primitive visual: ConicalFrustrum.
            text[9] = CreateTextLabel("primitive visual: ConicalFrustrum", new Position2D(1220, 450));
            Window.Instance.GetDefaultLayer().Add(text[9]);
            // primitive visual: ConicalFrustrum
            // ConicalFrustrum : The area bound between two circles,
            // i.e. a cone with the tip removed.
            PrimitiveVisual primitiveVisual4 = new PrimitiveVisual();
            primitiveVisual4.Shape = PrimitiveVisualShapeType.ConicalFrustrum;
            primitiveVisual4.BevelPercentage = 0.3f;
            primitiveVisual4.BevelSmoothness = 0.0f;
            primitiveVisual4.ScaleDimensions = new Vector3(1.0f,1.0f,0.3f);
            primitiveVisual4.MixColor = new Vector4((245.0f / 255.0f), (188.0f / 255.0f), (73.0f / 255.0f), 1.0f);
            primitiveVisual4.Size = new Vector2(200.0f, 200.0f);
            primitiveVisual4.Position = new Vector2(1220.0f, 550.0f);
            primitiveVisual4.PositionPolicy = VisualTransformPolicyType.Absolute;
            primitiveVisual4.SizePolicy = VisualTransformPolicyType.Absolute;
            primitiveVisual4.Origin = Visual.AlignType.TopBegin;
            primitiveVisual4.AnchorPoint = Visual.AlignType.TopBegin;
            _visualView.AddVisual("primitiveVisual4", primitiveVisual4);

            // Create the text mark primitive visual: ConicalFrustrum.
            text[10] = CreateTextLabel("primitive visual: Cube", new Position2D(1460, 450));
            Window.Instance.GetDefaultLayer().Add(text[10]);
            // primitive visual: Cube
            // Cube : Equivalent to a bevelled cube with a
            // bevel percentage of zero.
            PrimitiveVisual primitiveVisual5 = new PrimitiveVisual();
            primitiveVisual5.Shape = PrimitiveVisualShapeType.Cube;
            primitiveVisual5.BevelPercentage = 0.3f;
            primitiveVisual5.BevelSmoothness = 0.0f;
            primitiveVisual5.ScaleDimensions = new Vector3(1.0f,1.0f,0.3f);
            primitiveVisual5.MixColor = new Vector4((245.0f / 255.0f), (188.0f / 255.0f), (73.0f / 255.0f), 1.0f);
            primitiveVisual5.Size = new Vector2(200.0f, 200.0f);
            primitiveVisual5.Position = new Vector2(1460.0f, 550.0f);
            primitiveVisual5.PositionPolicy = VisualTransformPolicyType.Absolute;
            primitiveVisual5.SizePolicy = VisualTransformPolicyType.Absolute;
            primitiveVisual5.Origin = Visual.AlignType.TopBegin;
            primitiveVisual5.AnchorPoint = Visual.AlignType.TopBegin;
            _visualView.AddVisual("primitiveVisual5", primitiveVisual5);

            Window.Instance.KeyEvent += AppBack;
        }

        /// <summary>
        /// To create a TextLabel which indicate Visuals'type
        /// </summary>
        /// <param name="text">text</param>
        /// <param name="position2D">position</param>
        /// <returns>
        /// the created textLabel
        /// </returns>
        private TextLabel CreateTextLabel(string text, Position2D position2D)
        {
            TextLabel textLabel = new TextLabel();
            textLabel.Text = text;
            textLabel.PointSize = 6.0f;
            textLabel.Size2D = new Size2D(200, 100);
            textLabel.MultiLine = true;
            textLabel.Position2D = position2D;
            textLabel.HorizontalAlignment = HorizontalAlignment.Center;
            textLabel.VerticalAlignment = VerticalAlignment.Center;
            return textLabel;
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