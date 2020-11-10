using System;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace NUI_Visuals_2
{
    class Program : NUIApplication
    {
        private VisualView _mainVisualView;
        private int _mainVisualMargin = 20;
        private int _numCol = 2;
        private Size2D _visualSep = new Size2D(30, 30);
        private Size2D _visualViewSize;

        private string[] _imageType = {"Image", "NPatch", "SVG", "Animated", "Mesh"};
        private uint _numberImageTypes = 5;
        private static string _imageUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/";

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        private VisualMap createVisualMap(string imageType)
        {
            VisualMap map = null;

            switch (imageType)
            {
                case "Image":
                    ImageVisual imageVisual = new ImageVisual();
                    imageVisual.URL = _imageUrl + "gallery-1.jpg";
                    map = imageVisual;
                    break;
                case "NPatch":
                    NPatchVisual nPatchVisual = new NPatchVisual();
                    nPatchVisual.URL = _imageUrl + "heartsframe.png";
                    map = nPatchVisual;
                    break;
                case "SVG":
                    SVGVisual svgVisual = new SVGVisual();
                    svgVisual.URL = _imageUrl + "Kid1.svg";
                    map = svgVisual;
                    break;
                case "Animated":
                    AnimatedImageVisual animatedVisual = new AnimatedImageVisual();
                    animatedVisual.URL = _imageUrl + "dog-anim.gif";
                    map = animatedVisual;
                    break;
                case "Mesh":
                    MeshVisual meshVisual = new MeshVisual();
                    meshVisual.ObjectURL = _imageUrl + "MickeyMouse.obj";
                    map = meshVisual;
                    break;
            }

            map.Size = new Size2D((int)(_visualViewSize.Width * 0.9), (int)(_visualViewSize.Height * 0.9));
            map.Origin = Visual.AlignType.Center;
            map.AnchorPoint = Visual.AlignType.Center;
            map.Position = new Position2D(0, 0);

            return map;
        }

        private VisualView createVisualView(VisualMap map, Position2D pos)
        {
            VisualView VV = new VisualView();
            VV.PositionUsesPivotPoint = true;
            VV.BackgroundColor = new Color(0.7f, 0.0f, 1.0f, 0.6f);
            VV.ParentOrigin = ParentOrigin.TopLeft;
            VV.PivotPoint = PivotPoint.TopLeft;
            VV.Position2D = pos;
            VV.Size2D = _visualViewSize;

            VV.AddVisual("Visual", map);

            return VV;
        }

        private TextLabel createTextLabel(string label, float size, Position2D pos)
        {
            TextLabel textLabel = new TextLabel();

            textLabel.BackgroundColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            textLabel.PointSize = size;
            textLabel.TextColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            textLabel.Text = label;
            textLabel.Position2D = pos;
            textLabel.ParentOrigin = ParentOrigin.BottomLeft;
            textLabel.HorizontalAlignment = HorizontalAlignment.Center;
            textLabel.VerticalAlignment = VerticalAlignment.Center;

            return textLabel;
        }

        void Initialize()
        {
            Window.Instance.KeyEvent += OnKeyEvent;
            Window.Instance.BackgroundColor = Color.Black;
            Size2D winSize = Window.Instance.WindowSize;

            //-------------------- the main visual view ------------------------//
            _mainVisualView = new VisualView();
            _mainVisualView.Size2D = new Size2D(winSize.Width  - 2 * _mainVisualMargin,
                                                winSize.Height - 2 * _mainVisualMargin);
            _mainVisualView.PivotPoint = PivotPoint.TopLeft;
            _mainVisualView.ParentOrigin = ParentOrigin.TopLeft;
            _mainVisualView.Position2D = new Position2D(_mainVisualMargin, _mainVisualMargin);
            _mainVisualView.BackgroundColor = Color.Blue;
            Window.Instance.Add(_mainVisualView);

            int size = Math.Min((int)(_mainVisualView.Size2D.Width  / _numCol),
                                (int)((_mainVisualView.Size2D.Height -
                                       (1 + _numberImageTypes) * _visualSep.Height) / Math.Ceiling(_numberImageTypes / 2.0f)));
            _visualViewSize = new Size2D(size, size);

            //---------------------------- visuals -----------------------------//
            Position2D visualPos = new Position2D(_visualSep.Width, _visualSep.Height);
            VisualMap visualMap = null;
            VisualView visualView = null;

            int dHeight = _visualViewSize.Height / 2 + _visualSep.Height;
            for (uint i = 0; i < _numberImageTypes; ++i)
            {
                visualMap = createVisualMap(_imageType[i]); 
                visualView = createVisualView(visualMap, visualPos);
                visualView.Add(createTextLabel(_imageType[i], 9.0f, new Position2D(0, 0)));
                _mainVisualView.Add(visualView);
                if (i % 2 == 0)
                    visualPos += new Position2D(_mainVisualView.Size2D.Width - _visualViewSize.Width - 2 * _visualSep.Width,
                                                dHeight);
                else
                    visualPos += new Position2D(-_mainVisualView.Size2D.Width + _visualViewSize.Width + 2 * _visualSep.Width,
                                                dHeight);
            }
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "Escape" ||
                e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "BackSpace"))
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
