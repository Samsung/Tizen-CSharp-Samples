using System;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace NUI_Visuals
{
    class Program : NUIApplication
    {
        private VisualView _mainVisualView;
        private int _mainVisualMargin = 10;
        private int _numCol = 2;
        private Size2D _visualSep = new Size2D(60, 40);
        private Size2D _visualViewSize;
        private PrimitiveVisualShapeType[] _primitiveType = {PrimitiveVisualShapeType.Sphere,
                                                             PrimitiveVisualShapeType.ConicalFrustrum,
                                                             PrimitiveVisualShapeType.Cone,
                                                             PrimitiveVisualShapeType.Cylinder,
                                                             PrimitiveVisualShapeType.Cube,
                                                             PrimitiveVisualShapeType.Octahedron,
                                                             PrimitiveVisualShapeType.BevelledCube};
        private uint _numberPrimitives = 7;
        private string[] _primitiveName = {"Sphere", "ConicalFrustrum", "Cone", "Cylinder", "Cube", "Octahedron", "BevelledCube"};

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        private PrimitiveVisual createPrimitiveVisual(PrimitiveVisualShapeType primitiveType)
        {
            PrimitiveVisual prim = new PrimitiveVisual();
            prim.Shape = primitiveType;
            prim.MixColor = new Vector4(0.6f, 0.0f, 0.6f, 0.9f);
            prim.LightPosition = new Vector3(0.0f,0.0f,0.0f); // top left
            prim.Stacks = 128;
            prim.Slices = 128;

            switch (primitiveType)
            {
                case PrimitiveVisualShapeType.Sphere:
                    break;
                case PrimitiveVisualShapeType.ConicalFrustrum:
                    prim.ScaleHeight = 1.0f;
                    prim.ScaleTopRadius = 3.0f;
                    break;
                case PrimitiveVisualShapeType.Cone:
                    prim.ScaleBottomRadius = 3.0f;
                    prim.ScaleHeight = 2.0f;
                    break;
                case PrimitiveVisualShapeType.Cylinder:
                    prim.ScaleHeight = 1.0f;
                    prim.ScaleRadius = 1.5f;
                    break;
                case PrimitiveVisualShapeType.Cube:
                    prim.ScaleDimensions = new Vector3(0.8f, 0.2f, 1.2f);
                    break;
                case PrimitiveVisualShapeType.Octahedron:
                    prim.ScaleDimensions = new Vector3(0.0f, 0.0f, 1.2f);
                    break;
                case PrimitiveVisualShapeType.BevelledCube:
                    prim.ScaleDimensions = new Vector3(0.0f, 0.5f, 1.1f);
                    prim.BevelPercentage = 0.5f;
                    prim.BevelSmoothness = 0.0f;
                    break;
            }

            return prim;
        }

        private VisualView createVisualView(PrimitiveVisual prim, Position2D pos)
        {
            VisualView VV = new VisualView();
            VV.BackgroundColor = Color.White;
            VV.PositionUsesPivotPoint = true;
            VV.ParentOrigin = ParentOrigin.TopLeft;
            VV.PivotPoint = PivotPoint.TopLeft;
            VV.Position2D = pos;
            VV.Size2D = _visualViewSize;

            VV.AddVisual("Prim", prim);

            return VV;
        }

        private TextLabel createTextLabel(string label, float size, Position2D pos)
        {
            TextLabel textLabel = new TextLabel();

            textLabel.PointSize = size;
            textLabel.TextColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            textLabel.Text = label;
            textLabel.Position2D = pos;
            textLabel.ParentOrigin = ParentOrigin.BottomLeft;
            textLabel.PivotPoint = ParentOrigin.TopLeft;
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
            _mainVisualView.BackgroundColor = new Color(0.3f, 0.0f, 1.0f, 0.5f);
            Window.Instance.Add(_mainVisualView);

            int size = Math.Min((int)((_mainVisualView.Size2D.Width - (_numCol + 1) * _visualSep.Width)  / _numCol),
                                (int)((_mainVisualView.Size2D.Height - (1 + _numberPrimitives) * _visualSep.Height)
                                       / Math.Ceiling(_numberPrimitives / 2.0f)));
            _visualViewSize = new Size2D(size, size);

            //--------------------------- primitive visuals --------------------//
            Position2D visualPos = new Position2D(_visualSep.Width, _visualSep.Height);
            PrimitiveVisual prim = null;
            VisualView visualView = null;

            int dHeight = _visualViewSize.Height / 2 + _visualSep.Height;
            for (uint i = 0; i < _numberPrimitives; ++i)
            {
                prim = createPrimitiveVisual(_primitiveType[i]);
                visualView = createVisualView(prim, visualPos);
                visualView.Add(createTextLabel(_primitiveName[i], 8.0f, new Position2D(0, 0)));
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
