using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace NUILinearLayout
{
    class Program : NUIApplication
    {
        private View _rootView;
        private View _view1, _view2, _view3;
        private TextLabel _textLeft, _textRight;
        private ImageView _img1, _img2, _img3;
        private View _color1, _color2, _color3, _color4;
        private static string _resourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/";

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window.Instance.BackgroundColor = Color.Red;
            Size2D screenSize = Window.Instance.WindowSize;
            _rootView = new View();
            _rootView.BackgroundColor = Color.White;
            _rootView.Size2D = screenSize;

            LinearLayout rootLayout = new LinearLayout();
            rootLayout.LinearOrientation = LinearLayout.Orientation.Vertical;

            _rootView.Layout = rootLayout;
            _rootView.Padding = new Extents(20, 20, 20, 20);

            _view1 = new View();
            _view1.BackgroundColor = new Color(0.8f, 0.7f, 0.3f, 1.0f);
            _view1.WidthSpecification = LayoutParamPolicies.MatchParent;
            _view1.Weight = 0.45f;
            _view1.Padding = new Extents(10, 10, 10, 10);
            _view1.Margin = new Extents(5, 5, 5, 5);

            LinearLayout layout1 = new LinearLayout();
            layout1.LinearAlignment = LinearLayout.Alignment.Begin;
            layout1.LinearOrientation = LinearLayout.Orientation.Horizontal;
            _view1.Layout = layout1;
            AddText(_view1);

            _view2 = new View();
            _view2.BackgroundColor = new Color(0.8f, 0.7f, 0.3f, 1.0f);
            _view2.WidthSpecification = LayoutParamPolicies.MatchParent;
            _view2.Weight = 0.25f;
            _view2.Margin = new Extents(5, 5, 5, 5);

            LinearLayout layout2 = new LinearLayout();
            layout2.LinearAlignment = LinearLayout.Alignment.Center;
            layout2.LinearOrientation = LinearLayout.Orientation.Horizontal;
            _view2.Layout = layout2;
            AddImages(_view2);

            _view3 = new View();
            _view3.BackgroundColor = new Color(0.8f, 0.7f, 0.3f, 1.0f);
            _view3.WidthSpecification = LayoutParamPolicies.MatchParent;
            _view3.Weight = 0.3f;
            _view3.Margin = new Extents(5, 5, 5, 5);

            LinearLayout layout3 = new LinearLayout();
            layout3.LinearAlignment = LinearLayout.Alignment.Begin;
            layout3.LinearOrientation = LinearLayout.Orientation.Horizontal;
            _view3.Layout = layout3;
            AddColors(_view3);

            _rootView.Add(_view1);
            _rootView.Add(_view2);
            _rootView.Add(_view3);

            Window.Instance.Add(_rootView);
        }

        private void AddText(View view)
        {
            _textLeft = new TextLabel();
            _textLeft.BackgroundColor = new Color(0.8f, 0.1f, 0.1f, 1.0f);
            _textLeft.HeightSpecification = LayoutParamPolicies.MatchParent;
            _textLeft.Weight = 0.3f;
            _textLeft.TextColor = new Color(0.8f, 0.85f, 0.9f, 1.0f);
            _textLeft.Text = "0.3";
            _textLeft.HorizontalAlignment = HorizontalAlignment.Center;
            _textLeft.VerticalAlignment = VerticalAlignment.Center;
            _textLeft.Margin = new Extents(10, 10, 10, 10);

            _textRight = new TextLabel();
            _textRight.BackgroundColor = new Color(0.8f, 0.1f, 0.1f, 1.0f);
            _textRight.HeightSpecification = LayoutParamPolicies.MatchParent;
            _textRight.Weight = 0.7f;
            _textRight.TextColor = new Color(0.8f, 0.85f, 0.9f, 1.0f);
            _textRight.Text = "0.7";
            _textRight.HorizontalAlignment = HorizontalAlignment.Center;
            _textRight.VerticalAlignment = VerticalAlignment.Center;
            _textRight.Margin = new Extents(10, 10, 10, 10);

            view.Add(_textLeft);
            view.Add(_textRight);
        }

        private void AddImages(View view)
        {
            _img1 = new ImageView();
            _img1.ResourceUrl = _resourceUrl + "img1.svg";
            _img1.Size2D = new Size2D(100, 100);
            _img1.Margin = new Extents(20, 20, 20, 20);

            _img2 = new ImageView();
            _img2.ResourceUrl = _resourceUrl + "img2.svg";
            _img2.Size2D = new Size2D(100, 100);
            _img2.Margin = new Extents(20, 20, 20, 20);

            _img3 = new ImageView();
            _img3.ResourceUrl = _resourceUrl + "img3.svg";
            _img3.Size2D = new Size2D(100, 100);
            _img3.Margin = new Extents(20, 20, 20, 20);

            view.Add(_img1);
            view.Add(_img2);
            view.Add(_img3);
        }

        private void AddColors(View view)
        {
            _color1 = new View();
            _color1.BackgroundColor = new Color(0.1f, 0.1f, 0.1f, 1.0f);
            _color1.HeightSpecification = LayoutParamPolicies.MatchParent;
            _color1.Weight = 0.25f;
            _color1.Margin = new Extents(10, 10, 10, 10);

            _color2 = new View();
            _color2.BackgroundColor = new Color(0.9f, 0.9f, 0.9f, 1.0f);
            _color2.HeightSpecification = LayoutParamPolicies.MatchParent;
            _color2.Weight = 0.25f;
            _color2.Margin = new Extents(10, 10, 10, 10);

            _color3 = new View();
            _color3.BackgroundColor = new Color(0.1f, 0.1f, 0.1f, 1.0f);
            _color3.HeightSpecification = LayoutParamPolicies.MatchParent;
            _color3.Weight = 0.25f;
            _color3.Margin = new Extents(10, 10, 10, 10);

            _color4 = new View();
            _color4.BackgroundColor = new Color(0.9f, 0.9f, 0.9f, 1.0f);
            _color4.HeightSpecification = LayoutParamPolicies.MatchParent;
            _color4.Weight = 0.25f;
            _color4.Margin = new Extents(10, 10, 10, 10);

            view.Add(_color1);
            view.Add(_color2);
            view.Add(_color3);
            view.Add(_color4);
        }
        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "Escape" || e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "BackSpace"))
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
