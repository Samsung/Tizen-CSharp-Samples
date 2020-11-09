using System;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace NUI_Visuals_1
{
    class Program : NUIApplication
    {
        private VisualView _mainVisualView;
        private int _numRow = 2;
        private int _numCol = 3;
        private Size2D _visualSep = new Size2D(10, 10);
        private Size2D _visualViewSize;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        private VisualView createVisualView(GradientVisual grad, Position2D pos)
        {
            VisualView VV = new VisualView();
            VV.PositionUsesPivotPoint = true;
            VV.BackgroundColor = Color.Magenta;
            VV.ParentOrigin = ParentOrigin.TopLeft;
            VV.PivotPoint = PivotPoint.TopLeft;
            VV.Position2D = pos;
            VV.Size2D = _visualViewSize;

            VV.AddVisual("Visual", grad);

            return VV;
        }

        private TextVisual createTextVisual(string label, float size, Position2D pos)
        {
            TextVisual text = new TextVisual();
            text.Text = label;
            text.Position = pos;
            text.PointSize = size;
            text.MultiLine = true;
            text.FontFamily = "Arial";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Top;
            text.Origin = Visual.AlignType.BottomBegin;
            text.AnchorPoint = Visual.AlignType.TopBegin;

            PropertyMap fontStyle = new PropertyMap();
            fontStyle.Add("weight", new PropertyValue("bold"));
            fontStyle.Add("slant", new PropertyValue("italic"));
            text.FontStyle = fontStyle;

            return text;
        }

        void Initialize()
        {
            Window.Instance.KeyEvent += OnKeyEvent;
            Window.Instance.BackgroundColor = Color.Blue;
            Size2D winSize = Window.Instance.WindowSize;

            //-------------------- the main visual view ------------------------//
            _mainVisualView = new VisualView();
            _mainVisualView.Size2D = new Size2D((int)(winSize.Width * 0.9), (int)(winSize.Height * 0.8));
            _mainVisualView.PivotPoint = PivotPoint.TopLeft;
            _mainVisualView.ParentOrigin = ParentOrigin.TopLeft;;
            _mainVisualView.Position2D = new Position2D((int)(winSize.Width  - _mainVisualView.Size2D.Width) / 2,
                                                        (int)(winSize.Height - _mainVisualView.Size2D.Height) / 2);
            _mainVisualView.BackgroundColor = Color.White;
            Window.Instance.Add(_mainVisualView);

            int size = Math.Min((_mainVisualView.Size2D.Width  - (_numCol + 1) * _visualSep.Width)  / _numCol,
                                (_mainVisualView.Size2D.Height - (_numRow + 1) * _visualSep.Height) / _numRow);
            _visualViewSize = new Size2D(size, size);
            _visualSep.Width  = Math.Max(0, (_mainVisualView.Size2D.Width  - _numCol * size) / (_numCol + 1));
            _visualSep.Height = Math.Max(0, (_mainVisualView.Size2D.Height - _numRow * size) / (_numRow + 1));

            //-------------------------------- TEXT ----------------------------//
            TextVisual textVisual;
            textVisual = createTextVisual("VISUALS", 20.0f, new Position2D(0, -_mainVisualView.Size2D.Height));
            _mainVisualView.AddVisual("Text Visuals", textVisual);

            //-------------------------- VISUAL VIEW ---------------------------//
            VisualView visualView = new VisualView();
            visualView.PositionUsesPivotPoint = true;
            visualView.BackgroundColor = Color.Green;
            visualView.ParentOrigin = ParentOrigin.TopRight;
            visualView.PivotPoint = PivotPoint.TopRight;
            visualView.Position2D = new Position2D(-_visualSep.Width, _visualSep.Height);
            visualView.Size2D = _visualViewSize;

            _mainVisualView.Add(visualView);

            //-------------------------- BORDER --------------------------------//
            BorderVisual borderVisual;

            borderVisual = new BorderVisual();
            borderVisual.Size = _visualViewSize / 2;
            borderVisual.Color = Color.Black;
            borderVisual.BorderSize = 6.0f;
            borderVisual.AnchorPoint = Visual.AlignType.TopEnd;
            borderVisual.Origin = Visual.AlignType.TopEnd;
            borderVisual.Position = new Position2D(-7,7);

            visualView.AddVisual("Border 1", borderVisual);

            borderVisual = new BorderVisual();
            borderVisual.Size = _visualViewSize / 2;
            borderVisual.Color = Color.Blue;
            borderVisual.BorderSize = 6.0f;
            borderVisual.AnchorPoint = Visual.AlignType.BottomBegin;
            borderVisual.Origin = Visual.AlignType.BottomBegin;
            borderVisual.Position = new Position2D(7,-7);

            visualView.AddVisual("Border 2", borderVisual);

            textVisual = createTextVisual("Border", 10.0f, new Position2D(0, 5));
            visualView.AddVisual("Text", textVisual);

            //------------------------------- COLOR ----------------------------//
            ColorVisual colorVisual = new ColorVisual();
            colorVisual.Color = new Vector4(0.2f, 0.0f, 1.0f, 0.7f);
            colorVisual.Size = _visualViewSize;
            colorVisual.AnchorPoint = Visual.AlignType.TopBegin;
            colorVisual.Origin = Visual.AlignType.TopBegin;
            colorVisual.Position = new Position2D(_visualSep.Width, _visualSep.Height);

            _mainVisualView.AddVisual("Color", colorVisual);

            textVisual = createTextVisual("Color", 10.0f,
                                  new Position2D(-_mainVisualView.Size2D.Width / 2 + _visualViewSize.Width / 2,
                                                 -(_numRow - 1) * _visualViewSize.Height - _numRow * _visualSep.Height + 5));
            _mainVisualView.AddVisual("Text", textVisual);

            //----------------------------- GRADIENTS --------------------------//
            GradientVisual gradientVisual;
            PropertyArray stopColor;
            PropertyArray stopOffset;
            Position2D visualPos;

            //------------------------- radial gradient 1 ----------------------//
            gradientVisual = new GradientVisual();
            gradientVisual.Center = new Vector2(0.0f, 0.0f);
            gradientVisual.Radius = 1.0f;

            stopColor = new PropertyArray();
            stopColor.Add(new PropertyValue(Color.Yellow));
            stopColor.Add(new PropertyValue(Color.Blue));
            stopColor.Add(new PropertyValue(new Vector4(0.0f, 1.0f, 0.0f, 1.0f)));
            stopColor.Add(new PropertyValue(new Vector4(128.0f, 0.0f, 255.0f, 200.0f) / 255.0f));
            gradientVisual.StopColor = stopColor;

            stopOffset = new PropertyArray();
            stopOffset.Add(new PropertyValue(0.0f));
            stopOffset.Add(new PropertyValue(0.2f));
            stopOffset.Add(new PropertyValue(0.4f));
            stopOffset.Add(new PropertyValue(0.6f));
            stopOffset.Add(new PropertyValue(0.6f));
            gradientVisual.StopOffset = stopOffset;

            visualPos = new Position2D(_visualSep.Width, 2 * _visualSep.Height + _visualViewSize.Height);
            visualView = createVisualView(gradientVisual, visualPos);
            _mainVisualView.Add(visualView);

            //------------------------- radial gradient 2 ----------------------//
            gradientVisual = new GradientVisual();
            gradientVisual.Center = new Vector2(0.2f, 0.4f);
            gradientVisual.Radius = 0.2f;
            stopColor = new PropertyArray();
            stopColor.Add(new PropertyValue(Color.Blue));
            stopColor.Add(new PropertyValue(Color.Green));
            gradientVisual.StopColor = stopColor;
            gradientVisual.SpreadMethod = GradientVisualSpreadMethodType.Repeat;

            visualPos += new Position2D(_visualViewSize.Width + _visualSep.Width, 0);
            visualView = createVisualView(gradientVisual, visualPos);
            _mainVisualView.Add(visualView);

            textVisual = createTextVisual("Visual Gradients", 10.0f, new Position2D(0, 5));
            visualView.AddVisual("Text", textVisual);

            //------------------------- linear gradient ------------------------//
            gradientVisual = new GradientVisual();
            gradientVisual.StartPosition = new Vector2(-0.5f, 0.5f);
            gradientVisual.EndPosition = new Vector2(0.5f, -0.5f);
            stopColor = new PropertyArray();
            stopColor.Add(new PropertyValue(Color.Green));
            stopColor.Add(new PropertyValue(Color.Blue));
            gradientVisual.StopColor = stopColor;

            visualPos += new Position2D(_visualViewSize.Width + _visualSep.Width, 0);
            visualView = createVisualView(gradientVisual, visualPos);
            _mainVisualView.Add(visualView);
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
