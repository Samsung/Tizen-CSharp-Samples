using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace NUIGridLayout
{
    class Program : NUIApplication
    {
        private View _rootView, _topView, _buttonView;
        private Button _button1, _button2;
        private static int _itemsCnt = 40;
        private static int _columnCnt = 5;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            InitViews();
            InitGrid();
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

            _topView = new View();
            _topView.PositionUsesPivotPoint = true;
            _topView.PivotPoint = PivotPoint.TopCenter;
            _topView.ParentOrigin = new Vector3(0.5f, 0.05f, 0.0f);
            _topView.WidthResizePolicy = ResizePolicyType.FillToParent;
            _topView.HeightResizePolicy = ResizePolicyType.SizeRelativeToParent;
            _topView.SetSizeModeFactor(new Vector3(0.0f, 0.7f, 0.0f));
            _topView.Padding = new Extents(20, 20, 20, 20);

            GridLayout gridLayout = new GridLayout();
            gridLayout.Columns = _columnCnt;
            _topView.Layout = gridLayout;
            InitGrid();

            _buttonView = new View();
            _buttonView.PositionUsesPivotPoint = true;
            _buttonView.PivotPoint = PivotPoint.TopCenter;
            _buttonView.ParentOrigin = new Vector3(0.5f, 0.85f, 0.0f);
            _buttonView.WidthResizePolicy = ResizePolicyType.FillToParent;
            _buttonView.HeightResizePolicy = ResizePolicyType.SizeRelativeToParent;
            _buttonView.SetSizeModeFactor(new Vector3(0.0f, 0.2f, 0.0f));
            _buttonView.Padding = new Extents(20, 20, 20, 20);

            _rootView.Add(_topView);
            _rootView.Add(_buttonView);
            Window.Instance.Add(_rootView);
            Window.Instance.KeyEvent += OnKeyEvent;
        }

        private void InitGrid()
        {
            for (int i = 0; i < _itemsCnt; i++)
            {
                TextLabel t = new TextLabel();
                t.Margin = new Extents(10, 10, 10, 10);
                t.Text = "X";
                t.BackgroundColor = new Color(0.8f, 0.2f, 0.2f, 1.0f);
                _topView.Add(t);
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
            _button1.Text = "Remove";
            _button1.TextColor = Color.White;
            _button1.Margin = new Extents(10, 10, 10, 10);
            _button1.Weight = 0.5f;

            _button2 = new Button();
            _button2.BackgroundColor = new Color(0.25f, 0.25f, 0.25f, 1.0f);
            _button2.HeightResizePolicy = ResizePolicyType.FillToParent;
            _button2.Text = "Add";
            _button2.TextColor = Color.White;
            _button2.Margin = new Extents(10, 10, 10, 10);
            _button2.Weight = 0.5f;

            _buttonView.Add(_button1);
            _buttonView.Add(_button2);

            _button1.ClickEvent += Button1Clicked;
            _button2.ClickEvent += Button2Clicked;
        }

        private void Button1Clicked(object sender, Button.ClickEventArgs e)
        {
            _columnCnt = _columnCnt - 1 > 0 ? _columnCnt - 1 : 1;
            GridLayout gridLayout = new GridLayout();
            gridLayout.Columns = _columnCnt;
            _topView.Layout = gridLayout;
        }

        private void Button2Clicked(object sender, Button.ClickEventArgs e)
        {
            ++_columnCnt;
            GridLayout gridLayout = new GridLayout();
            gridLayout.Columns = _columnCnt;
            _topView.Layout = gridLayout;
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
