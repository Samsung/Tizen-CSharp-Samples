using System;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace NUI_Pagination
{
    // custom style of the pagination
    internal class CustomPaginationStyle : StyleBase
    {
        protected override ViewStyle GetViewStyle()
        {
            PaginationStyle style = new PaginationStyle
            {
                IndicatorSize = new Size(100, 100),
                IndicatorSpacing = 50,
                IndicatorImageUrl = new Selector<string>
                {
                    Normal = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/gray.png",
                    Selected = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/blue.png"
                },
                Name = "Pagination",
                Size = new Size(500, 200),
                BackgroundColor = new Color(1.0f, 1.0f, 1.0f, 1.0f),
                ParentOrigin = ParentOrigin.TopCenter,
                PivotPoint = PivotPoint.TopCenter,
                PositionUsesPivotPoint = true
            };
            return style;
        }
    }

    class Program : NUIApplication
    {
        private View _mainView;
        private VisualView _visualView;
        private Pagination _pagination;
        private int _winWidth;
        private int _winHeight;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window.Instance.KeyEvent += Window_KeyEvent;
            _winWidth = Window.Instance.WindowSize.Width;
            _winHeight = Window.Instance.WindowSize.Height;

            // the view is parent for the _visualView and the _pagination
            _mainView = new View();
            _mainView.Size2D = new Size2D(_winWidth, _winHeight);
            Window.Instance.Add(_mainView);

            // VisualView of the size = 3 * window_size; contains 3 images
            _visualView = new VisualView();
            _visualView.Size2D = new Size2D(3 * _winWidth, _winHeight);
            _visualView.PivotPoint = PivotPoint.TopLeft;
            _visualView.ParentOrigin = ParentOrigin.TopLeft;
            _visualView.PositionUsesPivotPoint = true;
            _visualView.BackgroundColor = Color.White;
            _mainView.Add(_visualView);

            // adds images to the ImageVisuals
            ImageVisual _imageVisual = null;
            for (uint i = 1; i <= 3; ++i)
            {
                _imageVisual = createImageVisual(i);
                _imageVisual.Position = new Vector2((_winWidth - _imageVisual.Size.Width) / 2 + (i - 1) * _winWidth, 50);
                _visualView.AddVisual("image" + i.ToString(), _imageVisual);
            }

            // registers the pagination's custon style
            Tizen.NUI.Components.StyleManager.Instance.RegisterStyle("CustomPagination", null, typeof(NUI_Pagination.CustomPaginationStyle));

            _pagination = new Pagination("CustomPagination");
            _pagination.IndicatorCount = 3;
            _pagination.SelectedIndex = 0;

            _mainView.Add(_pagination);
        }

        // creates the i-th image visual
        private ImageVisual createImageVisual(uint i)
        {
            ImageVisual imageVisual = new ImageVisual();

            imageVisual.URL = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/page" + i.ToString() + ".png";
            imageVisual.Size = new Vector2(600, 900);
            imageVisual.Origin = Visual.AlignType.CenterBegin;
            imageVisual.AnchorPoint = Visual.AlignType.CenterBegin;

            return imageVisual;
        }

        // responds on pressing some buttons
        private void Window_KeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "Escape" || e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "BackSpace")
                {
                    Exit();
                }
                else if (e.Key.KeyPressedName == "Left")
                {
                    if (_pagination.SelectedIndex > 0)
                    {
                        _visualView.Position2D = _visualView.Position2D + new Position2D(_winWidth, 0);
                        _pagination.SelectedIndex = _pagination.SelectedIndex - 1;
                    }
                }
                else if (e.Key.KeyPressedName == "Right")
                {
                    if (_pagination.SelectedIndex < _pagination.IndicatorCount - 1)
                    {
                        _visualView.Position2D = _visualView.Position2D - new Position2D(_winWidth, 0);
                        _pagination.SelectedIndex = _pagination.SelectedIndex + 1;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
