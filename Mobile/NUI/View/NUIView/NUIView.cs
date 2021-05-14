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

using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace NUIView
{
    class Program : NUIApplication
    {
        /// <summary>
        /// The root view of the application
        /// </summary>
        private static View RootView;
        /// <summary>
        /// The main view of the application
        /// </summary>
        private static View MainView;
        /// <summary>
        /// View representing parent
        /// </summary>
        private static View ParentView;
        /// <summary>
        /// View representing child
        /// </summary>
        private static View ChildView;
        private static Animation Animation;
        /// <summary>
        /// Button visible at the bottom of the screen
        /// </summary>
        private Button Button;
        /// <summary>
        /// Boolean to hold information if a view was touched
        /// </summary>
        private bool ViewTouched = true;
        /// <summary>
        /// Text with information about currently displayed views
        /// </summary>
        private static TextLabel TextLabel;
        /// <summary>
        /// Index of the current view (starting at 1)
        /// </summary>
        private int CurrentView = 1;
        /// <summary>
        /// Delegate holding the next view function
        /// </summmary>
        public delegate void ViewFunc();

        /// <summary>
        /// Overridden method called when the app is being launched
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        /// <summary>
        /// Method that initializes views
        /// </summary>
        void Initialize()
        {
            // store window instance and size in local variables
            Window Window = Window.Instance;
            Size2D ScreenSize = Window.WindowSize;

            // create root view that will contain all other views
            RootView = new View();
            RootView.PositionUsesPivotPoint = true;
            RootView.PivotPoint = PivotPoint.Center;
            RootView.ParentOrigin = ParentOrigin.Center;
            RootView.WidthResizePolicy = ResizePolicyType.FillToParent;
            RootView.HeightResizePolicy = ResizePolicyType.FillToParent;
            RootView.BackgroundColor = Color.Black;

            // create button and place it at the bottom of the screen
            Button = new Button();
            Button.Text = "Next";
            Button.PositionUsesPivotPoint = true;
            Button.PivotPoint = PivotPoint.TopCenter;
            Button.ParentOrigin = new Vector3(0.5f, 0.9f, 0.0f);
            Button.WidthResizePolicy = ResizePolicyType.FillToParent;
            Button.HeightResizePolicy = ResizePolicyType.SizeRelativeToParent;
            Button.SetSizeModeFactor(new Vector3(0.0f, 0.1f, 0.0f));

            // create main view and place it over the button
            MainView = new View();
            MainView.PositionUsesPivotPoint = true;
            MainView.PivotPoint = PivotPoint.TopCenter;
            MainView.ParentOrigin = new Vector3(0.5f, 0.2f, 0.0f);
            MainView.WidthResizePolicy = ResizePolicyType.FillToParent;
            MainView.HeightResizePolicy = ResizePolicyType.SizeRelativeToParent;
            MainView.SetSizeModeFactor(new Vector3(0.0f, 0.7f, 0.0f));

            // create text label that will hold text information
            TextLabel = new TextLabel();
            TextLabel.PositionUsesPivotPoint = true;
            TextLabel.PivotPoint = PivotPoint.TopCenter;
            TextLabel.ParentOrigin = new Vector3(0.5f, 0.1f, 0.0f);
            TextLabel.MultiLine = true;
            TextLabel.TextColor = Color.White;

            // add view to their parents
            RootView.Add(TextLabel);
            RootView.Add(MainView);
            RootView.Add(Button);
            Window.Add(RootView);

            // create first view
            View1();
            CurrentView = 1;

            // add events to button on click and key event to window
            Button.ClickEvent += ButtonClicked;
            Window.KeyEvent += OnKeyEvent;
        }

        /// <summary>
        /// The method called when the button is clicked
        /// </summary>
        /// <param name="sender">Button instance</param>
        /// <param name="e">Event arguments</param>
        private void ButtonClicked(object sender, Button.ClickEventArgs e)
        {
            // clear the previous view, unparent and set to null to dispose of
            ChildView.Unparent();
            ParentView.Unparent();
            ChildView.Dispose();
            ParentView.Dispose();
            ChildView = null;
            ParentView = null;

            // update current view to next view
            if (CurrentView == 1) {
                View2();
                CurrentView = 2;
            }
            else if(CurrentView == 2) {
                View3();
                CurrentView = 3;
            }
            else if(CurrentView == 3) {
                View4();
                CurrentView = 4;
            }
            else if(CurrentView == 4) {
                View5();
                CurrentView = 5;
            }
            else if(CurrentView == 5) {
                View6();
                CurrentView = 6;
            }
            else if(CurrentView == 6) {
                View1();
                CurrentView = 1;
            }

        }

        /// <summary>
        /// View showing simple view positioning
        /// </summary>
        private void View1() {
            // Prepare the ParentView
            ParentView = new View();
            ParentView.Size2D = new Size2D(600, 300);
            ParentView.PositionUsesPivotPoint = true;
            ParentView.PivotPoint = PivotPoint.Center;
            ParentView.ParentOrigin = ParentOrigin.Center;
            ParentView.BackgroundColor = new Color(0.26f, 0.85f, 0.95f, 1.0f);

            // Prepare the ChildView
            ChildView = new View();
            ChildView.Size2D = new Size2D(100, 100);
            ChildView.PositionUsesPivotPoint = true;
            ChildView.PivotPoint = PivotPoint.TopLeft;
            ChildView.ParentOrigin = ParentOrigin.TopLeft;
            ChildView.Position = new Position(60, 10, 0);
            ChildView.BackgroundColor = new Color(1.0f, 0.41f, 0.47f, 1.0f);

            TextLabel.Text = "PivotPoint: TopLeft\nParentOrigin: TopLeft\nPosition: (60, 10)";

            // Add children views
            ParentView.Add(ChildView);
            MainView.Add(ParentView);
        }

        /// <summary>
        /// View showing simple view positioning with negative position values
        /// </summary>
        private void View2() {
            // Prepare the ParentView
            ParentView = new View();
            ParentView.Size2D = new Size2D(600, 300);
            ParentView.PositionUsesPivotPoint = true;
            ParentView.PivotPoint = PivotPoint.Center;
            ParentView.ParentOrigin = ParentOrigin.Center;
            ParentView.BackgroundColor = new Color(0.26f, 0.85f, 0.95f, 1.0f);

            // Prepare the ChildView
            ChildView = new View();
            ChildView.Size2D = new Size2D(100, 100);
            ChildView.PositionUsesPivotPoint = true;
            ChildView.PivotPoint = PivotPoint.BottomRight;
            ChildView.ParentOrigin = ParentOrigin.BottomRight;
            ChildView.Position = new Position(-100, -50);
            ChildView.BackgroundColor = new Color(1.0f, 0.41f, 0.47f, 1.0f);

            TextLabel.Text = "PivotPoint: BottomRight\nParentOrigin: BottomRight\nPosition: (-100, -50)";

            // Add children views
            ParentView.Add(ChildView);
            MainView.Add(ParentView);
        }

        /// <summary>
        /// View showing child scaling
        /// Animation is added to show scaling behaviour
        /// </summary>
        private void View3() {
            // Prepare the ParentView
            ParentView = new View();
            ParentView.Size2D = new Size2D(600, 300);
            ParentView.PositionUsesPivotPoint = true;
            ParentView.PivotPoint = PivotPoint.Center;
            ParentView.ParentOrigin = ParentOrigin.Center;
            ParentView.BackgroundColor = new Color(0.26f, 0.85f, 0.95f, 1.0f);

            // Prepare the ChildView
            ChildView = new View();
            ChildView.Size2D = new Size2D(250, 200);
            ChildView.PositionUsesPivotPoint = true;
            ChildView.PivotPoint = PivotPoint.CenterRight;
            ChildView.ParentOrigin = ParentOrigin.CenterRight;
            ChildView.Position = new Position(-50, 0);
            ChildView.BackgroundColor = new Color(1.0f, 0.41f, 0.47f, 1.0f);
            ChildView.Scale = new Vector3(1.0f, 1.0f, 1.0f);

            TextLabel.Text = "PivotPoint: CenterRight\nParentOrigin: CenterRight\nPosition: (-50, 0)\nChiled View Scale: 1.0 to 2.0";

            Animation = new Animation(2000);
            Animation.AnimateTo(ChildView, "Scale", new Vector3(2.0f, 1.0f, 1.0f), 0, 1000);
            Animation.AnimateTo(ChildView, "Scale", new Vector3(1.0f, 1.0f, 1.0f), 1000, 2000);
            Animation.Looping = true;
            Animation.Play();

            // Add children views
            ParentView.Add(ChildView);
            MainView.Add(ParentView);
        }

        /// <summary>
        /// View showing parent scaling
        /// Animation is added to show scaling behaviour
        /// </summary>
        private void View4() {
            // Prepare the ParentView
            ParentView = new View();
            ParentView.Size2D = new Size2D(300, 300);
            ParentView.PositionUsesPivotPoint = true;
            ParentView.PivotPoint = PivotPoint.Center;
            ParentView.ParentOrigin = ParentOrigin.Center;
            ParentView.BackgroundColor = new Color(0.26f, 0.85f, 0.95f, 1.0f);
            ParentView.Scale = new Vector3(1.0f, 1.0f, 1.0f);

            // Prepare the ChildView
            ChildView = new View();
            ChildView.Size2D = new Size2D(250, 200);
            ChildView.PositionUsesPivotPoint = true;
            ChildView.PivotPoint = PivotPoint.CenterLeft;
            ChildView.ParentOrigin = ParentOrigin.CenterLeft;
            ChildView.Position = new Position(25, 0);
            ChildView.BackgroundColor = new Color(1.0f, 0.41f, 0.47f, 1.0f);

            TextLabel.Text = "PivotPoint: Center\nParentOrigin: Center\nPosition: (25, 0)\nParent View Scale: 1.0 to 2.0";

            // Prepare the Animation
            Animation = new Animation(2000);
            Animation.AnimateTo(ParentView, "Scale", new Vector3(2.0f, 1.0f, 1.0f), 0, 1000);
            Animation.AnimateTo(ParentView, "Scale", new Vector3(1.0f, 1.0f, 1.0f), 1000, 2000);
            Animation.Looping = true;
            Animation.Play();

            // Add children views
            ParentView.Add(ChildView);
            MainView.Add(ParentView);
        }

        /// <summary>
        /// View showing changing orientation
        /// Animation is added to show changing orientation
        /// </summary>
        private void View5() {
            // Prepare the ParentView
            ParentView = new View();
            ParentView.Size2D = new Size2D(300, 300);
            ParentView.PositionUsesPivotPoint = true;
            ParentView.PivotPoint = PivotPoint.Center;
            ParentView.ParentOrigin = ParentOrigin.Center;
            ParentView.BackgroundColor = new Color(0.26f, 0.85f, 0.95f, 1.0f);
            ParentView.Orientation = new Rotation(new Radian(new Degree(45.0f)), PositionAxis.Z);

            // Prepare the ChildView
            ChildView = new View();
            ChildView.Size2D = new Size2D(200, 200);
            ChildView.PositionUsesPivotPoint = true;
            ChildView.PivotPoint = PivotPoint.Center;
            ChildView.ParentOrigin = ParentOrigin.Center;
            ChildView.BackgroundColor = new Color(1.0f, 0.41f, 0.47f, 1.0f);

            TextLabel.Text = "PivotPoint: Center\nParentOrigin: Center\nParent View Orientation: 0 to 45";

            Animation = new Animation(2000);
            Animation.AnimateTo(ParentView, "Orientation", new Rotation(new Radian(new Degree(0.0f)), PositionAxis.Z), 0, 1000);
            Animation.AnimateTo(ParentView, "Orientation", new Rotation(new Radian(new Degree(45.0f)), PositionAxis.Z), 1000, 2000);
            Animation.Looping = true;
            Animation.Play();

            // Add children views
            ParentView.Add(ChildView);
            MainView.Add(ParentView);
        }

        /// <summary>
        /// View with touch event
        /// Changes colors on click
        /// </summary>
        private void View6() {
            // Prepare the ParentView
            ParentView = new View();
            ParentView.Size2D = new Size2D(600, 300);
            ParentView.PositionUsesPivotPoint = true;
            ParentView.PivotPoint = PivotPoint.Center;
            ParentView.ParentOrigin = ParentOrigin.Center;
            ParentView.BackgroundColor = new Color(0.26f, 0.85f, 0.95f, 1.0f);

            // Prepare the ChildView
            ChildView = new View();
            ChildView.Size2D = new Size2D(200, 200);
            ChildView.PositionUsesPivotPoint = true;
            ChildView.PivotPoint = PivotPoint.Center;
            ChildView.ParentOrigin = ParentOrigin.Center;
            ChildView.BackgroundColor = new Color(1.0f, 0.41f, 0.47f, 1.0f);

            TextLabel.Text = "Touch event example";
            
            // Register event
            ParentView.TouchEvent += OnViewTouch;

            // Add children views
            ParentView.Add(ChildView);
            MainView.Add(ParentView);
        }

        /// <summary>
        /// Method which is called when the view is touched
        /// </summary>
        /// <param name="sender">View instance</param>
        /// <param name="e">Event arguments</param>
        private bool OnViewTouch(object sender, View.TouchEventArgs e)
        {
            View TouchedView = sender as View;
            if (e.Touch.GetState(0) == PointStateType.Down)
            {
                if (ViewTouched)
                {
                    // If the view is in ViewTouched state, change the BackgroundColor
                    TouchedView.BackgroundColor = new Color(0.8f, 0.2f, 0.1f, 1.0f);
                    ViewTouched = false;
                }
                else {
                    // Otherwise, change back the BackgroundColor
                    TouchedView.BackgroundColor = new Color(0.26f, 0.85f, 0.95f, 1.0f);
                    ViewTouched = true;
                }
            }
            return true;
        }

        /// <summary>
        /// Method which is called when key event happens
        /// </summary>
        /// <param name="sender">Window instance</param>
        /// <param name="e">Event arguments</param>
        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                // Exit application on back or escape
                Exit();
            }
        }

        static void Main(string[] args)
        {
            var App = new Program();
            // Run app
            App.Run(args);
            App.Dispose();
        }
    }
}
