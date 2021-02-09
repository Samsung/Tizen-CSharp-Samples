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
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace NUIView
{
    class Program : NUIApplication
    {
        private static View _rootView, _mainView, _parentView, _childView;
        private static Animation animation;
        private Button _button;
        private bool _viewTouched = true;
        private static TextLabel _textLabel;
        private int _currentView = 1;
        public delegate void viewFunc();

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window window = Window.Instance;
            Size2D screenSize = window.WindowSize;

            _rootView = new View();
            _rootView.PositionUsesPivotPoint = true;
            _rootView.PivotPoint = PivotPoint.Center;
            _rootView.ParentOrigin = ParentOrigin.Center;
            _rootView.WidthResizePolicy = ResizePolicyType.FillToParent;
            _rootView.HeightResizePolicy = ResizePolicyType.FillToParent;
            _rootView.BackgroundColor = Color.Black;

            _button = new Button();
            _button.Text = "Next";
            _button.PositionUsesPivotPoint = true;
            _button.PivotPoint = PivotPoint.TopCenter;
            _button.ParentOrigin = new Vector3(0.5f, 0.9f, 0.0f);
            _button.WidthResizePolicy = ResizePolicyType.FillToParent;
            _button.HeightResizePolicy = ResizePolicyType.SizeRelativeToParent;
            _button.SetSizeModeFactor(new Vector3(0.0f, 0.1f, 0.0f));

            _mainView = new View();
            _mainView.PositionUsesPivotPoint = true;
            _mainView.PivotPoint = PivotPoint.TopCenter;
            _mainView.ParentOrigin = new Vector3(0.5f, 0.2f, 0.0f);
            _mainView.WidthResizePolicy = ResizePolicyType.FillToParent;
            _mainView.HeightResizePolicy = ResizePolicyType.SizeRelativeToParent;
            _mainView.SetSizeModeFactor(new Vector3(0.0f, 0.7f, 0.0f));

            _textLabel = new TextLabel();
            _textLabel.PositionUsesPivotPoint = true;
            _textLabel.PivotPoint = PivotPoint.TopCenter; 
            _textLabel.ParentOrigin = new Vector3(0.5f, 0.1f, 0.0f);
            _textLabel.MultiLine = true;
            _textLabel.TextColor = Color.White;


            _rootView.Add(_textLabel);
            _rootView.Add(_mainView);
            _rootView.Add(_button);
            window.Add(_rootView);

            view1();
            _currentView = 1;

            _button.ClickEvent += ButtonClicked;
            window.KeyEvent += OnKeyEvent;
        }


        private void ButtonClicked(object sender, Button.ClickEventArgs e)
        {
            _childView.Unparent();
            _parentView.Unparent();
            _childView.Dispose();
            _parentView.Dispose();
            _childView = null;
            _parentView = null;

            if (_currentView == 1){
                view2();
                _currentView = 2;
            }
            else if(_currentView == 2){
                view3();
                _currentView = 3;
            }
            else if(_currentView == 3){
                view4();
                _currentView = 4;
            }
            else if(_currentView == 4){
                view5();
                _currentView = 5;
            }
            else if(_currentView == 5){
                view6();
                _currentView = 6;
            }
            else if(_currentView == 6){
                view1();
                _currentView = 1;
            }
        
        }

        private void view1(){
            _parentView = new View();
            _parentView.Size2D = new Size2D(600, 300);
            _parentView.PositionUsesPivotPoint = true;
            _parentView.PivotPoint = PivotPoint.Center;
            _parentView.ParentOrigin = ParentOrigin.Center;
            _parentView.BackgroundColor = new Color(0.26f, 0.85f, 0.95f, 1.0f);

            _childView = new View();
            _childView.Size2D = new Size2D(100, 100);
            _childView.PositionUsesPivotPoint = true;
            _childView.PivotPoint = PivotPoint.TopLeft;
            _childView.ParentOrigin = ParentOrigin.TopLeft;
            _childView.Position = new Position(60, 10, 0);
            _childView.BackgroundColor = new Color(1.0f, 0.41f, 0.47f, 1.0f);

            _textLabel.Text = "PivotPoint: TopLeft\nParentOrigin: TopLeft\nPosition: (60, 10)";

            _parentView.Add(_childView);
            _mainView.Add(_parentView);
        }

        private void view2(){
            _parentView = new View();
            _parentView.Size2D = new Size2D(600, 300);
            _parentView.PositionUsesPivotPoint = true;
            _parentView.PivotPoint = PivotPoint.Center;
            _parentView.ParentOrigin = ParentOrigin.Center;
            _parentView.BackgroundColor = new Color(0.26f, 0.85f, 0.95f, 1.0f);
            
            _childView = new View();
            _childView.Size2D = new Size2D(100, 100);
            _childView.PositionUsesPivotPoint = true;
            _childView.PivotPoint = PivotPoint.BottomRight;
            _childView.ParentOrigin = ParentOrigin.BottomRight;
            _childView.Position = new Position(-100, -50);
            _childView.BackgroundColor = new Color(1.0f, 0.41f, 0.47f, 1.0f);

            _textLabel.Text = "PivotPoint: BottomRight\nParentOrigin: BottomRight\nPosition: (-100, -50)";

            _parentView.Add(_childView);
            _mainView.Add(_parentView);
        }

        private void view3(){
            _parentView = new View();
            _parentView.Size2D = new Size2D(600, 300);
            _parentView.PositionUsesPivotPoint = true;
            _parentView.PivotPoint = PivotPoint.Center;
            _parentView.ParentOrigin = ParentOrigin.Center;
            _parentView.BackgroundColor = new Color(0.26f, 0.85f, 0.95f, 1.0f);

            _childView = new View();
            _childView.Size2D = new Size2D(250, 200);
            _childView.PositionUsesPivotPoint = true;
            _childView.PivotPoint = PivotPoint.CenterRight;
            _childView.ParentOrigin = ParentOrigin.CenterRight;
            _childView.Position = new Position(-50, 0);
            _childView.BackgroundColor = new Color(1.0f, 0.41f, 0.47f, 1.0f);
            _childView.Scale = new Vector3(1.0f, 1.0f, 1.0f);

            _textLabel.Text = "PivotPoint: CenterRight\nParentOrigin: CenterRight\nPosition: (-50, 0)\nChiled View Scale: 1.0 to 2.0";

            animation = new Animation(2000);
            animation.AnimateTo(_childView, "Scale", new Vector3(2.0f, 1.0f, 1.0f), 0, 1000);
            animation.AnimateTo(_childView, "Scale", new Vector3(1.0f, 1.0f, 1.0f), 1000, 2000);
            animation.Looping = true;
            animation.Play();

            _parentView.Add(_childView);
            _mainView.Add(_parentView);
        }

        private void view4(){
            _parentView = new View();
            _parentView.Size2D = new Size2D(300, 300);
            _parentView.PositionUsesPivotPoint = true;
            _parentView.PivotPoint = PivotPoint.Center;
            _parentView.ParentOrigin = ParentOrigin.Center;
            _parentView.BackgroundColor = new Color(0.26f, 0.85f, 0.95f, 1.0f);
            _parentView.Scale = new Vector3(1.0f, 1.0f, 1.0f);

            _childView = new View();
            _childView.Size2D = new Size2D(250, 200);
            _childView.PositionUsesPivotPoint = true;
            _childView.PivotPoint = PivotPoint.CenterLeft;
            _childView.ParentOrigin = ParentOrigin.CenterLeft;
            _childView.Position = new Position(25, 0);
            _childView.BackgroundColor = new Color(1.0f, 0.41f, 0.47f, 1.0f);
            
            _textLabel.Text = "PivotPoint: Center\nParentOrigin: Center\nPosition: (25, 0)\nParent View Scale: 1.0 to 2.0";

            animation = new Animation(2000);
            animation.AnimateTo(_parentView, "Scale", new Vector3(2.0f, 1.0f, 1.0f), 0, 1000);
            animation.AnimateTo(_parentView, "Scale", new Vector3(1.0f, 1.0f, 1.0f), 1000, 2000);
            animation.Looping = true;
            animation.Play();

            _parentView.Add(_childView);
            _mainView.Add(_parentView);
        }

        private void view5(){
            _parentView = new View();
            _parentView.Size2D = new Size2D(300, 300);
            _parentView.PositionUsesPivotPoint = true;
            _parentView.PivotPoint = PivotPoint.Center;
            _parentView.ParentOrigin = ParentOrigin.Center;
            _parentView.BackgroundColor = new Color(0.26f, 0.85f, 0.95f, 1.0f);
            _parentView.Orientation = new Rotation(new Radian(new Degree(45.0f)), PositionAxis.Z);

            _childView = new View();
            _childView.Size2D = new Size2D(200, 200);
            _childView.PositionUsesPivotPoint = true;
            _childView.PivotPoint = PivotPoint.Center;
            _childView.ParentOrigin = ParentOrigin.Center;
            _childView.BackgroundColor = new Color(1.0f, 0.41f, 0.47f, 1.0f);

            _textLabel.Text = "PivotPoint: Center\nParentOrigin: Center\nParent View Orientation: 0 to 45";

            animation = new Animation(2000);
            animation.AnimateTo(_parentView, "Orientation", new Rotation(new Radian(new Degree(0.0f)), PositionAxis.Z), 0, 1000);
            animation.AnimateTo(_parentView, "Orientation", new Rotation(new Radian(new Degree(45.0f)), PositionAxis.Z), 1000, 2000);
            animation.Looping = true;
            animation.Play();

            _parentView.Add(_childView);
            _mainView.Add(_parentView);
        }

        private void view6(){
            _parentView = new View();
            _parentView.Size2D = new Size2D(600, 300);
            _parentView.PositionUsesPivotPoint = true;
            _parentView.PivotPoint = PivotPoint.Center;
            _parentView.ParentOrigin = ParentOrigin.Center;
            _parentView.BackgroundColor = new Color(0.26f, 0.85f, 0.95f, 1.0f);

            _childView = new View();
            _childView.Size2D = new Size2D(200, 200);
            _childView.PositionUsesPivotPoint = true;
            _childView.PivotPoint = PivotPoint.Center;
            _childView.ParentOrigin = ParentOrigin.Center;
            _childView.BackgroundColor = new Color(1.0f, 0.41f, 0.47f, 1.0f);

            _textLabel.Text = "Touch event example";

            _parentView.TouchEvent += OnViewTouch;

            _parentView.Add(_childView);
            _mainView.Add(_parentView);
        }

        private bool OnViewTouch(object sender, View.TouchEventArgs e)
        {
            View touchedView = sender as View;
            if (e.Touch.GetState(0) == PointStateType.Down)
            {
                if (_viewTouched)
                {
                    touchedView.BackgroundColor = new Color(0.8f, 0.2f, 0.1f, 1.0f);
                    _viewTouched = false;
                } else {
                    touchedView.BackgroundColor = new Color(0.26f, 0.85f, 0.95f, 1.0f);
                    _viewTouched = true;
                }
            }
            return true;
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
            app.Dispose();
        }
    }
}
