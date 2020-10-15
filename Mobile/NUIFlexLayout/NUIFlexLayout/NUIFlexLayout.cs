using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace NUIFlexLayout
{
    class Program : NUIApplication
    {
        private View _rootView, _topView, _buttonView, _flexView;
        private Button _button1, _button2, _button3, _button4;
        private static int _itemsCnt = 20;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            InitViews();
            InitFlex();
            InitButtons();
        }

        private void InitViews()
        {
            Size2D screenSize = Window.Instance.WindowSize;

            _rootView = new View();
            _rootView.PositionUsesPivotPoint = true;
            _rootView.PivotPoint = PivotPoint.Center;
            _rootView.ParentOrigin = ParentOrigin.Center;
            _rootView.WidthResizePolicy = ResizePolicyType.FillToParent;
            _rootView.HeightResizePolicy = ResizePolicyType.FillToParent;
            _rootView.BackgroundColor = Color.Black;

            _topView = new View();
            _topView.PositionUsesPivotPoint = true;
            _topView.PivotPoint = PivotPoint.TopCenter;
            _topView.ParentOrigin = new Vector3(0.5f, 0.05f, 0.0f);
            _topView.WidthResizePolicy = ResizePolicyType.FillToParent;
            _topView.HeightResizePolicy = ResizePolicyType.SizeRelativeToParent;
            _topView.SetSizeModeFactor(new Vector3(0.0f, 0.7f, 0.0f));
            _topView.Padding = new Extents(20, 20, 20, 20);
            _topView.BackgroundColor = Color.Black;

            _buttonView = new View();
            _buttonView.PositionUsesPivotPoint = true;
            _buttonView.PivotPoint = PivotPoint.TopCenter;
            _buttonView.ParentOrigin = new Vector3(0.5f, 0.85f, 0.0f);
            _buttonView.WidthResizePolicy = ResizePolicyType.FillToParent;
            _buttonView.HeightResizePolicy = ResizePolicyType.SizeRelativeToParent;
            _buttonView.SetSizeModeFactor(new Vector3(0.0f, 0.2f, 0.0f));
            _buttonView.Padding = new Extents(20, 20, 20, 20);
            _buttonView.BackgroundColor = Color.Black;

            _rootView.Add(_topView);
            _rootView.Add(_buttonView);
            Window.Instance.Add(_rootView);
            InitFlex();
            Window.Instance.KeyEvent += OnKeyEvent;
        }

        private void InitFlex()
        {
            _flexView = new View();
            _flexView.PositionUsesPivotPoint = true;
            _flexView.PivotPoint = PivotPoint.Center;
            _flexView.ParentOrigin = ParentOrigin.Center;
            _flexView.HeightSpecification = LayoutParamPolicies.MatchParent;
            _flexView.WidthSpecification = LayoutParamPolicies.MatchParent;
            _flexView.BackgroundColor = Color.Black;
            _flexView.Layout = new FlexLayout() {WrapType = FlexLayout.FlexWrapType.Wrap};
            _topView.Add(_flexView);

            for (int i = 0; i < _itemsCnt; i++)
            {
                TextLabel t = new TextLabel();
                t.Margin = new Extents(10, 10, 10, 10);
                t.Text = "X " + i;
                t.BackgroundColor = new Color(0.8f, 0.1f, 0.2f, 1.0f);
                _flexView.Add(t);
            }
        }

        private void InitButtons()
        {
            LinearLayout buttonLayout = new LinearLayout();
            buttonLayout.LinearOrientation = LinearLayout.Orientation.Horizontal;
            _buttonView.Layout = buttonLayout;

            _button1 = new Button();
            _button1.BackgroundColor = new Color(0.25f, 0.25f, 0.25f, 1.0f);
            _button1.HeightResizePolicy = ResizePolicyType.FillToParent;
            _button1.Text = "Dir";
            _button1.TextColor = Color.White;
            _button1.Margin = new Extents(10, 10, 10, 10);
            _button1.Weight = 0.25f;

            _button2 = new Button();
            _button2.BackgroundColor = new Color(0.25f, 0.25f, 0.25f, 1.0f);
            _button2.HeightResizePolicy = ResizePolicyType.FillToParent;
            _button2.Text = "Just";
            _button2.TextColor = Color.White;
            _button2.Margin = new Extents(10, 10, 10, 10);
            _button2.Weight = 0.25f;

            _button3 = new Button();
            _button3.BackgroundColor = new Color(0.25f, 0.25f, 0.25f, 1.0f);
            _button3.HeightResizePolicy = ResizePolicyType.FillToParent;
            _button3.Text = "Align";
            _button3.TextColor = Color.White;
            _button3.Margin = new Extents(10, 10, 10, 10);
            _button3.Weight = 0.25f;

            _button4 = new Button();
            _button4.BackgroundColor = new Color(0.25f, 0.25f, 0.25f, 1.0f);
            _button4.HeightResizePolicy = ResizePolicyType.FillToParent;
            _button4.Text = "Wrap";
            _button4.TextColor = Color.White;
            _button4.Margin = new Extents(10, 10, 10, 10);
            _button4.Weight = 0.25f;

            _buttonView.Add(_button1);
            _buttonView.Add(_button2);
            _buttonView.Add(_button3);
            _buttonView.Add(_button4);

            _button1.ClickEvent += Button1Clicked;
            _button2.ClickEvent += Button2Clicked;
            _button3.ClickEvent += Button3Clicked;
            _button4.ClickEvent += Button4Clicked;
        }

        private void Button1Clicked(object sender, Button.ClickEventArgs e)
        {
            FlexLayout layout = (FlexLayout)_flexView.Layout;
            FlexLayout.FlexDirection newDirection = layout.Direction + 1;
            if (newDirection > FlexLayout.FlexDirection.RowReverse)
                newDirection = FlexLayout.FlexDirection.Column;
            layout.Direction = newDirection;
            _flexView.Layout = layout;
        }

        private void Button2Clicked(object sender, Button.ClickEventArgs e)
        {
            FlexLayout layout = (FlexLayout)_flexView.Layout;
            FlexLayout.FlexJustification newJust = layout.Justification + 1;
            if (newJust > FlexLayout.FlexJustification.SpaceAround)
                newJust = FlexLayout.FlexJustification.FlexStart;
            layout.Justification = newJust;
            _flexView.Layout = layout;
        }

        private void Button3Clicked(object sender, Button.ClickEventArgs e)
        {
            FlexLayout layout = (FlexLayout)_flexView.Layout;
            FlexLayout.AlignmentType newAlign = layout.Alignment + 1;
            if (newAlign > FlexLayout.AlignmentType.Stretch)
                newAlign = FlexLayout.AlignmentType.Auto;
            layout.Alignment = newAlign;
            _flexView.Layout = layout;
        }

        private void Button4Clicked(object sender, Button.ClickEventArgs e)
        {
            FlexLayout layout = (FlexLayout)_flexView.Layout;
            if (layout.WrapType == FlexLayout.FlexWrapType.Wrap)
                layout.WrapType = FlexLayout.FlexWrapType.NoWrap;
            else
                layout.WrapType = FlexLayout.FlexWrapType.Wrap;
            _flexView.Layout = layout;
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
        }
    }
}