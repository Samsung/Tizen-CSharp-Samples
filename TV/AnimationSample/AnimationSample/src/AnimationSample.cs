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
using Tizen;
using System.Runtime.InteropServices;
using Tizen.NUI;
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;

namespace AnimationSample
{
    /// <summary>
    /// This sample demonstrates how to use the Animation class to create and cancel animations.
    /// </summary>
    class AnimationSample : NUIApplication
    {
        private ImageView view;
        //This animation change the size of the view during the animation.
        private Animation sizeAnimation;
        //This animation change the position of the view during the animation.
        private Animation positionAnimation;
        //This animation change the scale of the view during the animation.
        private Animation scaleAnimation;
        //This animation change the opacity of the view during the animation.
        private Animation opacityAnimation;
        //This animation change the orientation of the view during the animation.
        private Animation orientationAnimation;
        //This animation change the pixelAreal of the imageView during the animation.
        private Animation pixelArealAnimation;
        private TextLabel guide;
        private PushButton _positionButton;
        private PushButton _sizeButton;
        private PushButton _scaleButton;
        private PushButton _orientationButton;
        private PushButton _opacityButton;
        private PushButton _pixelAreaButton;
        private PushButton _PositionSizeOpacity;

        private const string resources = "/home/owner/apps_rw/org.tizen.example.AnimationSample/res/images";
        private string normalImagePath = resources + "/Button/btn_bg_25_25_25_95.9.png";
        private string focusImagePath = resources + "/Button/btn_bg_255_255_255_200.9.png";
        private string pressImagePath = resources + "/Button/btn_bg_0_129_198_100.9.png";


        /// <summary>
        /// The constructor with null
        /// </summary>
        public AnimationSample() : base()
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
        /// Stop all animation.
        /// </summary>
        private void AllStop()
        {
            sizeAnimation.Stop();
            positionAnimation.Stop();
            scaleAnimation.Stop();
            opacityAnimation.Stop();
            orientationAnimation.Stop();
            pixelArealAnimation.Stop();
        }

        /// <summary>
        /// Animation Sample Application initialisation.
        /// </summary>
        private void Initialize()
        {
            Window.Instance.BackgroundColor = Color.Black;
            View focusIndicator = new View();
            FocusManager.Instance.FocusIndicator = focusIndicator;

            _positionButton = CreateButton("Position", "PositionAnimation");
            _sizeButton = CreateButton("Size", "SizeAnimation");
            _scaleButton = CreateButton("Scale", "ScaleAnimation");
            _orientationButton = CreateButton("Orientation", "OrientationAnimation");
            _opacityButton = CreateButton("Opacity", "OpacityAnimation");
            _pixelAreaButton = CreateButton("PixelArea", "PixelAreaAnimation");
            _PositionSizeOpacity = CreateButton("PositionSizeOpacity", "Position + Size + Opacity animation at same time!");
            _PositionSizeOpacity.Size2D = new Size2D(1400, 80);
            //Set the callback of button's clicked event
            _positionButton.Clicked += ButtonClick;
            _sizeButton.Clicked += ButtonClick;
            _scaleButton.Clicked += ButtonClick;
            _orientationButton.Clicked += ButtonClick;
            _opacityButton.Clicked += ButtonClick;
            _pixelAreaButton.Clicked += ButtonClick;
            _PositionSizeOpacity.Clicked += ButtonClick;

            //Create a tableView as the container of the pushButton.
            TableView tableView = new TableView(3, 3);
            tableView.Size2D = new Size2D(1500, 440);
            tableView.PivotPoint = PivotPoint.TopLeft;
            tableView.ParentOrigin = ParentOrigin.TopLeft;
            tableView.Position2D = new Position2D(300, 630);

            tableView.AddChild(_positionButton, new TableView.CellPosition(0, 0));
            tableView.AddChild(_sizeButton, new TableView.CellPosition(0, 1));
            tableView.AddChild(_scaleButton, new TableView.CellPosition(0, 2));
            tableView.AddChild(_orientationButton, new TableView.CellPosition(1, 0));
            tableView.AddChild(_opacityButton, new TableView.CellPosition(1, 1));
            tableView.AddChild(_pixelAreaButton, new TableView.CellPosition(1, 2));
            tableView.AddChild(_PositionSizeOpacity, new TableView.CellPosition(2, 0, 1, 3));

            Window.Instance.GetDefaultLayer().Add(tableView);
            FocusManager.Instance.SetCurrentFocusView(_positionButton);

            //This textLable is used to show the title.
            guide = new TextLabel();
            guide.HorizontalAlignment = HorizontalAlignment.Center;
            guide.VerticalAlignment = VerticalAlignment.Center;
            guide.BackgroundColor = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
            guide.TextColor = Color.White;
            guide.PositionUsesPivotPoint = true;
            guide.ParentOrigin = ParentOrigin.TopLeft;
            guide.PivotPoint = PivotPoint.TopLeft;
            guide.Size2D = new Size2D(1920, 100);
            guide.FontFamily = "Samsung One 600";
            guide.Position2D = new Position2D(0, 0);
            guide.MultiLine = false;
            guide.PointSize = 15.0f;
            guide.Text = "Animation Sample";
            Window.Instance.GetDefaultLayer().Add(guide);

            // Create the view to animate.
            view = new ImageView();
            view.Size2D = new Size2D(200, 200);
            view.Position = new Position(860, 250, 0);
            view.PositionUsesPivotPoint = true;
            view.PivotPoint = PivotPoint.TopLeft;
            view.ParentOrigin = ParentOrigin.TopLeft;
            view.ResourceUrl = resources + "/gallery-2.jpg";

            // Add view on Window.
            Window.Instance.GetDefaultLayer().Add(view);

            // Create the position animation.
            // The duration of the animation is 1.5s;
            positionAnimation = new Animation();
            positionAnimation.Duration = 1500;
            // Sets the default alpha function for the animation.
            positionAnimation.DefaultAlphaFunction = new AlphaFunction(new Vector2(0.3f, 0), new Vector2(0.15f, 1));
            // To reset the view's position.
            positionAnimation.AnimateTo(view, "Position", new Position(100, 150, 0), 0, 0);
            positionAnimation.AnimateTo(view, "Position", new Position(300, 200, 0), 0, 300);
            positionAnimation.AnimateTo(view, "Position", new Position(500, 200, 0), 0, 800);
            positionAnimation.AnimateTo(view, "Position", new Position(600, 250, 0), 0, 1000);
            positionAnimation.AnimateTo(view, "Position", new Position(860, 300, 0), 0, 1500);
            //StopFinal: If the animation is stopped, the animated property values are saved as if the animation had run to completion
            positionAnimation.EndAction = Animation.EndActions.StopFinal;

             // Create the size animation using AnimateTo.
             sizeAnimation = new Animation(1000);
            sizeAnimation.AnimateTo(view, "size", new Vector3(view.SizeWidth, view.SizeHeight, 0));
            sizeAnimation.AnimateTo(view, "sizeWidth", view.SizeWidth * 1.25f);
            sizeAnimation.AnimateTo(view, "sizeHeight", view.SizeHeight * 1.25f);
            sizeAnimation.AnimateTo(view, "sizeDepth", 0);
            //StopFinal: If the animation is stopped, the animated property values are saved as if the animation had run to completion
            sizeAnimation.EndAction = Animation.EndActions.StopFinal;

            // Create the scale animation using AnimateTo.
            scaleAnimation = new Animation(1500);
            scaleAnimation.AnimateTo(view, "scale", new Vector3(1.2f, 1.2f, 1.0f), 0, 200, new AlphaFunction(AlphaFunction.BuiltinFunctions.Sin));
            scaleAnimation.AnimateTo(view, "scaleX", 1.5f, 200, 500, new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear));
            scaleAnimation.AnimateTo(view, "ScaleY", 1.5f, 500, 800, new AlphaFunction(AlphaFunction.BuiltinFunctions.Bounce));
            scaleAnimation.AnimateTo(view, "scaleX", 1.0f, 800, 1200, new AlphaFunction(AlphaFunction.BuiltinFunctions.Bounce));
            scaleAnimation.AnimateTo(view, "scaleX", 1.0f, 1200, 1500, new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear));
            //StopFinal: If the animation is stopped, the animated property values are saved as if the animation had run to completion
            scaleAnimation.EndAction = Animation.EndActions.StopFinal;

            // Create the orientation animation using AnimateTo.
            orientationAnimation = new Animation();
            orientationAnimation.AnimateTo(view, "Orientation", new Rotation(new Radian(new Degree(60.0f)), PositionAxis.X), 0, 400);
            orientationAnimation.AnimateTo(view, "Orientation", new Rotation(new Radian(new Degree(60.0f)), PositionAxis.Y), 400, 800);
            orientationAnimation.AnimateTo(view, "Orientation", new Rotation(new Radian(new Degree(60.0f)), PositionAxis.Z), 800, 1000);
            orientationAnimation.AnimateTo(view, "Orientation", new Rotation(new Radian(0.0f), PositionAxis.X), 1000, 1400);
            orientationAnimation.AnimateTo(view, "Orientation", new Rotation(new Radian(0.0f), PositionAxis.Y), 1400, 1800);
            orientationAnimation.AnimateTo(view, "Orientation", new Rotation(new Radian(0.0f), PositionAxis.Z), 1800, 2200);
            //StopFinal: If the animation is stopped, the animated property values are saved as if the animation had run to completion
            orientationAnimation.EndAction = Animation.EndActions.StopFinal;

            // Create the Opacity animation using AnimateTo.
            opacityAnimation = new Animation(1500);
            opacityAnimation.AnimateTo(view, "Opacity", 0.5f, 0, 400);
            opacityAnimation.AnimateTo(view, "Opacity", 0.0f, 400, 800);
            opacityAnimation.AnimateTo(view, "Opacity", 0.5f, 800, 1250);
            opacityAnimation.AnimateTo(view, "Opacity", 1.0f, 1250, 1500);
            //StopFinal: If the animation is stopped, the animated property values are saved as if the animation had run to completion
            opacityAnimation.EndAction = Animation.EndActions.StopFinal;

            // Create the pixelArea animation using AnimateTo.
            pixelArealAnimation = new Animation(2000);
            RelativeVector4 vec1 = new RelativeVector4(0.0f, 0.0f, 0.5f, 0.5f);
            RelativeVector4 vec2 = new RelativeVector4(0.0f, 0.0f, 0.0f, 0.0f);
            RelativeVector4 vec3 = new RelativeVector4(0.0f, 0.0f, 1.0f, 1.0f);
            pixelArealAnimation.AnimateTo(view, "pixelArea", vec1, 0, 500);
            pixelArealAnimation.AnimateTo(view, "pixelArea", vec2, 500, 1000);
            pixelArealAnimation.AnimateTo(view, "pixelArea", vec1, 1000, 1500);
            pixelArealAnimation.AnimateTo(view, "pixelArea", vec3, 1500, 2000);
            //StopFinal: If the animation is stopped, the animated property values are saved as if the animation had run to completion
            pixelArealAnimation.EndAction = Animation.EndActions.StopFinal;

            view.Focusable = true;
            Window.Instance.KeyEvent += AppBack;
        }

        /// <summary>
        /// Called by buttons
        /// </summary>
        /// <param name="source">The clicked button</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool ButtonClick(object source, EventArgs e)
        {
            PushButton button = source as PushButton;
            if (button.Name == "Position")
            {
                //Stop all the animation and Play position Animation.
                AllStop();
                positionAnimation.Play();
            }
            else if(button.Name == "Size")
            {
                //Stop all the animation and Play position Animation.
                AllStop();
                sizeAnimation.Play();
            }
            else if (button.Name == "Scale")
            {
                //Stop all the animation and Play position Animation.
                AllStop();
                scaleAnimation.Play();
            }
            else if (button.Name == "Orientation")
            {
                //Stop all the animation and Play position Animation.
                AllStop();
                orientationAnimation.Play();
            }
            else if (button.Name == "Opacity")
            {
                //Stop all the animation and Play position Animation.
                AllStop();
                opacityAnimation.Play();
            }
            else if (button.Name == "PixelArea")
            {
                //Stop all the animation and Play position Animation.
                AllStop();
                pixelArealAnimation.Play();
            }
            else if(button.Name == "PositionSizeOpacity")
            {
                //Stop all the animation and Play position, scale opacitity Animation at the same time.
                AllStop();
                positionAnimation.Play();
                scaleAnimation.Play();
                opacityAnimation.Play();

            }
            return false;
        }
        /// <summary>
        /// Create an Text visual.
        /// </summary>
        /// <param name="text">The text of the Text visual</param>
        /// <param name="color">The color of the text</param>
        private PropertyMap CreateTextVisual(string text, Color color)
        {
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            map.Add(TextVisualProperty.Text, new PropertyValue(text));
            map.Add(TextVisualProperty.TextColor, new PropertyValue(color));
            map.Add(TextVisualProperty.PointSize, new PropertyValue(8.0f));
            map.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
            map.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));
            map.Add(TextVisualProperty.FontFamily, new PropertyValue("Samsung One 400"));
            return map;
        }

        /// <summary>
        /// Create an Image visual.
        /// </summary>
        /// <param name="imagePath">The url of the image</param>
        private PropertyMap CreateImageVisual(string imagePath)
        {
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            map.Add(ImageVisualProperty.URL, new PropertyValue(imagePath));
            return map;
        }

        private PushButton CreateButton(string name, string text)
        {
            PushButton button = new PushButton();
            button.Focusable = true;
            button.Size2D = new Size2D(400, 80);
            button.Focusable = true;
            button.Name = name;
            // Create the label which will show when _pushbutton focused.
            PropertyMap _focusText = CreateTextVisual(text, Color.Black);

            // Create the label which will show when _pushbutton unfocused.
            PropertyMap _unfocusText = CreateTextVisual(text, Color.White);
            button.Label = _unfocusText;

            // Create normal background visual.
            PropertyMap normalMap = CreateImageVisual(normalImagePath);

            // Create focused background visual.
            PropertyMap focusMap = CreateImageVisual(focusImagePath);

            // Create pressed background visual.
            PropertyMap pressMap = CreateImageVisual(pressImagePath);
            button.SelectedVisual = pressMap;
            button.UnselectedBackgroundVisual = normalMap;

            // Chang background Visual and Label when focus gained.
            button.FocusGained += (obj, e) =>
            {
                button.UnselectedBackgroundVisual = focusMap;
                button.Label = _focusText;
            };

            // Chang background Visual and Label when focus lost.
            button.FocusLost += (obj, e) =>
            {
                button.UnselectedBackgroundVisual = normalMap;
                button.Label = _unfocusText;
            };

            // Chang background Visual when pressed.
            button.KeyEvent += (obj, ee) =>
            {
                if ("Return" == ee.Key.KeyPressedName)
                {
                    if (Key.StateType.Down == ee.Key.State)
                    {
                        button.UnselectedBackgroundVisual = pressMap;
                        Tizen.Log.Fatal("NUI", "Press in pushButton sample!!!!!!!!!!!!!!!!");
                    }
                    else if (Key.StateType.Up == ee.Key.State)
                    {
                        button.UnselectedBackgroundVisual = focusMap;
                        Tizen.Log.Fatal("NUI", "Release in pushButton sample!!!!!!!!!!!!!!!!");
                    }
                }
                return false;
            };
            return button;
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
                Tizen.Log.Info("Key", e.Key.KeyPressedName);
                if (e.Key.KeyPressedName == "XF86Back")
                {
                    this.Exit();
                }
            }
        }
    }
}