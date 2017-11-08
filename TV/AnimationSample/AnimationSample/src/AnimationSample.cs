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
        private View view;
        private Animation sizeAnimation;
        private Animation positionAnimation;
        private Animation scaleAnimation;
        private Animation visibleAnimation;
        private Animation orientationAnimation;
        private Animation colorAnimation;
        private TextLabel guide;

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
            visibleAnimation.Stop();
            orientationAnimation.Stop();
            colorAnimation.Stop();
        }

        /// <summary>
        /// The event will be triggered when have key clicked
        /// </summary>
        /// <param name="source">view</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool OnKeyPressed(object source, View.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                // Only do the position animation.
                if (e.Key.KeyPressedName == "1")
                {
                    AllStop();
                    positionAnimation.Play();
                }
                // Only do the size animation.
                else if (e.Key.KeyPressedName == "2")
                {
                    AllStop();
                    sizeAnimation.Play();
                }
                // Only do the scale animation.
                else if (e.Key.KeyPressedName == "3")
                {
                    AllStop();
                    scaleAnimation.Play();
                }
                // Only do the orientation animation.
                else if (e.Key.KeyPressedName == "4")
                {
                    AllStop();
                    orientationAnimation.Play();
                }
                // Only do the visible animation.
                else if (e.Key.KeyPressedName == "5")
                {
                    AllStop();
                    visibleAnimation.Play();
                }
                // Only do the color animation.
                else if (e.Key.KeyPressedName == "6")
                {
                    AllStop();
                    colorAnimation.Play();
                }
            }

            return false;
        }

        /// <summary>
        /// Animation Sample Application initialisation.
        /// </summary>
        private void Initialize()
        {
            Window.Instance.BackgroundColor = new Color(1.0f, 0.92f, 0.80f, 1.0f);
            View focusIndicator = new View();
            FocusManager.Instance.FocusIndicator = focusIndicator;

            // Create the guide describes how to use this sample application.
            // KEY 1: Play Position animation.
            // KEY 2: Play Size animation.
            // KEY 3: Play Scale animation.
            // KEY 4: Play Orientation animation.
            // KEY 5: Play Visible animation.
            // KEY 6: Play Color animation.
            guide = new TextLabel();
            guide.PositionUsesPivotPoint = true;
            guide.ParentOrigin = ParentOrigin.BottomRight;
            guide.PivotPoint = PivotPoint.BottomRight;
            guide.MultiLine = true;
            guide.PointSize = 9.0f;
            guide.Text = "Animation Sample Guide\n" +
                            "KEY 1: Play Position animation.\n" +
                            "KEY 2: Play Size animation.\n" +
                            "KEY 3: Play Scale animation.\n" +
                            "KEY 4: Play Orientation animation.\n" +
                            "KEY 5: Play Visible animation.\n" +
                            "KEY 6: Play Color animation.";
            Window.Instance.GetDefaultLayer().Add(guide);

            // Create the view to animate.
            view = new View();
            view.Size2D = new Size2D(200, 200);
            view.Position = new Position(0, 0, 0);
            view.PositionUsesPivotPoint = true;
            view.PivotPoint = PivotPoint.Center;
            view.ParentOrigin = ParentOrigin.Center;
            view.BackgroundColor = Color.Red;

            // Add view on Window.
            Window.Instance.GetDefaultLayer().Add(view);

            // Create the position animation.
            // The duration of the animation is 1.5s;
            positionAnimation = new Animation();
            positionAnimation.Duration = 1500;
            // Sets the default alpha function for the animation.
            positionAnimation.DefaultAlphaFunction = new AlphaFunction(new Vector2(0.3f, 0), new Vector2(0.15f, 1));
            // To reset the view's position.
            positionAnimation.AnimateTo(view, "Position", new Position(0, 0, 0), 0, 0);

            // Create the position animation using AnimateBy.
            // Animates a property value by relative amount.
            positionAnimation.AnimateBy(view, "Position", new Position(200, 300, 0), 0, 125);
            positionAnimation.AnimateBy(view, "PositionX", -200, 125, 250);
            positionAnimation.AnimateBy(view, "PositionY", -300, 250, 375);
            positionAnimation.AnimateBy(view, "PositionZ", 0, 375, 500);

            // Create the position animation using AnimatePath.
            // Animates an view's position through a predefined path.
            Path path = new Path();
            path.AddPoint(new Position(300, 0, 0));
            path.AddPoint(new Position(300, 400, 0));
            path.AddPoint(new Position(0, 0, 0));
            path.GenerateControlPoints(0.5f);
            positionAnimation.AnimatePath(view, path, new Vector3(0, 0, 0), 500, 1000);

            // Create the position animation using AnimateBetween.
            // Animates position between keyframes.
            KeyFrames keyFrames = new KeyFrames();
            keyFrames.Add(0.0f, new Vector3(0, 0, 0));
            keyFrames.Add(0.3f, new Vector3(100, 200, 0));
            keyFrames.Add(0.6f, new Vector3(100, 0, 0));
            keyFrames.Add(1.0f, new Vector3(0, 0, 0));
            positionAnimation.AnimateBetween(view, "Position", keyFrames, 1000, 1500);

            // Create the size animation using AnimateTo.
            sizeAnimation = new Animation(1000);
            sizeAnimation.AnimateTo(view, "size", new Vector3(view.SizeWidth, view.SizeHeight, 0));
            sizeAnimation.AnimateTo(view, "sizeWidth", view.SizeWidth * 1.25f);
            sizeAnimation.AnimateTo(view, "sizeHeight", view.SizeHeight * 1.25f);
            sizeAnimation.AnimateTo(view, "sizeDepth", 0);

            // Create the scale animation using AnimateTo.
            scaleAnimation = new Animation(1000);
            scaleAnimation.AnimateTo(view, "scale", new Vector3(1.2f, 1.2f, 1.0f), 0, 200, new AlphaFunction(AlphaFunction.BuiltinFunctions.Sin));
            scaleAnimation.AnimateTo(view, "scaleX", 1.5f, 200, 400, new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear));
            scaleAnimation.AnimateTo(view, "ScaleY", 1.5f, 400, 700, new AlphaFunction(AlphaFunction.BuiltinFunctions.Bounce));
            scaleAnimation.AnimateTo(view, "ScaleZ", 1.0f, 700, 1000, new AlphaFunction(AlphaFunction.BuiltinFunctions.Count));

            // Create the orientation animation using AnimateTo.
            orientationAnimation = new Animation();
            orientationAnimation.AnimateTo(view, "Orientation", new Rotation(new Radian(new Degree(60.0f)), PositionAxis.X), 0, 400);
            orientationAnimation.AnimateTo(view, "Orientation", new Rotation(new Radian(new Degree(60.0f)), PositionAxis.Y), 400, 800);
            orientationAnimation.AnimateTo(view, "Orientation", new Rotation(new Radian(new Degree(60.0f)), PositionAxis.Z), 800, 1000);
            orientationAnimation.AnimateTo(view, "Orientation", new Rotation(new Radian(0.0f), PositionAxis.X), 1000, 1400);
            orientationAnimation.AnimateTo(view, "Orientation", new Rotation(new Radian(0.0f), PositionAxis.Y), 1400, 1800);
            orientationAnimation.AnimateTo(view, "Orientation", new Rotation(new Radian(0.0f), PositionAxis.Z), 1800, 2200);

            // Create the visible animation using AnimateTo.
            visibleAnimation = new Animation();
            visibleAnimation.AnimateTo(view, "Visible", false, 0, 500);
            visibleAnimation.AnimateTo(view, "Visible", true, 500, 1000);

            // Create the color animation using AnimateTo.
            colorAnimation = new Animation();
            colorAnimation.AnimateTo(view, "color", Color.Blue, 0, 200);
            colorAnimation.AnimateTo(view, "colorRed", Color.Red.R, 200, 400);
            colorAnimation.AnimateTo(view, "colorGreen", Color.Red.G, 400, 600);
            colorAnimation.AnimateTo(view, "colorBlue", Color.Red.B, 600, 800);
            colorAnimation.AnimateTo(view, "colorAlpha", Color.Red.A, 800, 100000);

            view.KeyEvent += OnKeyPressed;
            view.Focusable = true;
            FocusManager.Instance.SetCurrentFocusView(view);
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
                Tizen.Log.Info("Key", e.Key.KeyPressedName);
                if (e.Key.KeyPressedName == "XF86Back")
                {
                    this.Exit();
                }
            }
        }
    }
}