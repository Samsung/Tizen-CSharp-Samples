using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;


namespace NUI_Visuals
{
    class Program : NUIApplication
    {
        // visual view with buttons at the bottom of the window
        private VisualView _buttonVisualView;
        // visual view being the main part of the window - contains all elements of the CurrentVisualView (width = 3 * _winWidth)
        private VisualView _mainVisualView;
        // list with visual views - the main part of the window being displaied after clicking the corresponding button
        private List<VisualView> CurrentVisualView = new List<VisualView>();

        // the width of the window = _currentWidth + 2 * _frameSize
        private int _winWidth;
        // width of the frame around buttons and around the currently visible visual view
        private int _frameSize = 20;
        // _buttonVisualView height to _mainVisualView height ratio
        private float _buttonToMainRatio = 0.1f;
        // the width of the visual views being elements of the CurrentVisualView
        private int _currentWidth;
        // the height of the visual views being elements of the CurrentVisualView
        private int _currentHeight;

        // array of image visuals displayed after clicking the coresponding button
        private string[] _imageType = {"Image", "Animated", "SVG", "Mesh", "NPatch"};
        // number of image visuals
        private uint _numberImages = 5;
        // path to the folder with images
        private static string _imageUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/";
        // relative width of each image (percentage part of the width of CurrentVisualView[i])
        private float _imageRelWidth = 0.395f;
        // relative ehight of each image (percentage part of the hight of CurrentVisualView[i])
        private float _imageRelHeight = 0.2f;

        // array of primitive visuals displayed after clicking the corresponding button
        private string[] _primitiveName = {"Sphere", "ConicalFrustrum", "Cone", "Cylinder", "BevelledCube", "Octahedron", "Cube"};
        // number of primitive visuals
        private uint _numberPrimitives = 7;
        // relative width of each primitive (percentage part of the width of CurrentVisualView[i])
        private float _primitiveRelWidth = 0.4f;
        // relative height of each primitive (percentage part of the height of CurrentVisualView[i])
        private float _primitiveRelHeight = 0.13f;

        // buttons associated with the visuals shown
        private List<Button> _buttons = new List<Button>();
        // color of the button, which is associated with currently visible visuals
        private Color _activeColor = new Color(0.66f, 0.6f, 0.9f, 1);
        // color of the rest of the buttons
        private Color _grayColor = new Color(0.78f, 0.78f, 0.78f, 1);

        // overriden method called when the app is launched
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        // creates a text visual at a relative position 'relPos' determined as the shift of the anchor point
        // (the visual's top center) from the origin (the parent's top begin);
        // the text is being centered horizontally and vertically aligned to the top within its area;
        private TextVisual CreateTextVisual(string label, float size, RelativeVector2 relPos)
        {
            TextVisual text = new TextVisual();
            text.Text = label;
            text.RelativePosition = relPos;
            text.PointSize = size;
            text.MultiLine = true;
            text.FontFamily = "Arial";
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Top;
            text.Origin = Visual.AlignType.TopBegin;
            text.AnchorPoint = Visual.AlignType.TopCenter;

            PropertyMap fontStyle = new PropertyMap();
            fontStyle.Add("weight", new PropertyValue("bold"));
            fontStyle.Add("slant", new PropertyValue("italic"));
            text.FontStyle = fontStyle;

            return text;
        }

        // creates and sets common properties for buttons
        private Button SetButton(string label, int xPosition, Size2D buttonSize)
        {
            Button button = new Button();

            button.Size = buttonSize;
            button.TextLabel.MultiLine = true;
            button.Position2D = new Position2D(xPosition, 0);
            button.BackgroundColor = new Color(0.78f, 0.78f, 0.78f, 1);
            button.PointSizeSelector = new FloatSelector
            {
                Other = 8,
                Pressed = 12
            };
            button.TextColorSelector = new ColorSelector
            {
                Other = new Color(0, 0, 1, 1),
                Pressed = new Color(1, 0, 0, 1)
            };
            button.TextSelector = new StringSelector
            {
                Other = label,
                Pressed = "PRESSED"
            };

            return button;
        }

        // changes the clicked button color to _activeColor and the other buttons to gray
        private void SetButtonsColors(int choosenButton)
        {
            for (var i = 0; i < _buttons.Count; ++i)
                _buttons[i].BackgroundColor = _grayColor;
            _buttons[choosenButton].BackgroundColor = _activeColor;
        }

        // called after clicking button1 - sets the visibility width of _mainVisualView from 0 to winWidth
        private void OnClickedButton1(object sender, EventArgs e)
        {
            SetButtonsColors(0);
            _mainVisualView.Position2D = new Position2D(0, _frameSize);
        }

        // called after clicking button1 - sets the visibility width of _mainVisualView from winWidth to 2*winWidth
        private void OnClickedButton2(object sender, EventArgs e)
        {
            SetButtonsColors(1);
            _mainVisualView.Position2D = new Position2D(-_winWidth, _frameSize);
        }

        // called after clicking button1 - sets the visibility width of _mainVisualView from 2*winWidth to 3*winWidth
        private void OnClickedButton3(object sender, EventArgs e)
        {
            SetButtonsColors(2);
            _mainVisualView.Position2D = new Position2D(-2 * _winWidth, _frameSize);
        }

        // fills the list of buttons;
        // adds buttons to their view;
        // connects buttons' clicked event with the proper function call
        private void InitializeButtons()
        {
            int buttonWidth = (int)((_buttonVisualView.Size2D.Width - 2 * _frameSize) / 3);
            int buttonHeight = _buttonVisualView.Size2D.Height;
            Size2D buttonSize = new Size2D(buttonWidth, buttonHeight);

            _buttons.Add(SetButton("Color Border Gradient", 0, buttonSize));
            _buttons.Add(SetButton("Images", (buttonWidth + _frameSize), buttonSize));
            _buttons.Add(SetButton("Primitives", 2 * (buttonWidth + _frameSize), buttonSize));

            for (var i = 0; i < _buttons.Count; i++)
                _buttonVisualView.Add(_buttons[i]);

            _buttons[0].Clicked += OnClickedButton1;
            _buttons[1].Clicked += OnClickedButton2;
            _buttons[2].Clicked += OnClickedButton3;
        }

        // creates primitive visual
        // properties for a given primitive are set in the switch-case block
        // properties common for all primitives are set outside the block
        // (except MixColor property, which is set for some primitives individually)
        private PrimitiveVisual CreatePrimitiveVisual(string primitiveName)
        {
            PrimitiveVisual primitiveVisual = new PrimitiveVisual();

            primitiveVisual.MixColor = new Color(0.6f, 0.4f, 1.0f, 1.0f);
            primitiveVisual.LightPosition = new Vector3(0.0f,0.0f,300.0f);
            primitiveVisual.Slices = 100;
            primitiveVisual.Stacks = 100;

            switch (primitiveName)
            {
                case "Sphere":
                    primitiveVisual.Shape = PrimitiveVisualShapeType.Sphere;
                    break;

                case "ConicalFrustrum":
                    primitiveVisual.Shape = PrimitiveVisualShapeType.ConicalFrustrum;
                    primitiveVisual.ScaleHeight = 2.0f;
                    primitiveVisual.ScaleTopRadius = 0.3f;
                    primitiveVisual.ScaleBottomRadius = 1.0f;
                    primitiveVisual.MixColor = Color.Green;
                    break;

                case "Cone":
                    primitiveVisual.Shape = PrimitiveVisualShapeType.Cone;
                    primitiveVisual.ScaleHeight = 2.0f;
                    primitiveVisual.ScaleBottomRadius = 1.0f;
                    primitiveVisual.MixColor = new Color(0.4f, 0.4f, 1.0f, 1.0f);
                    break;

                case "Cylinder":
                    primitiveVisual.Shape = PrimitiveVisualShapeType.Cylinder;
                    primitiveVisual.ScaleHeight = 1.0f;
                    primitiveVisual.ScaleRadius = 0.5f;
                    break;

                case "Cube":
                    primitiveVisual.Shape = PrimitiveVisualShapeType.Cube;
                    primitiveVisual.ScaleDimensions = new Vector3(1.0f, 0.4f, 0.8f);
                    primitiveVisual.MixColor = new Color(0.4f, 0.4f, 1.0f, 1.0f);
                    break;

                case "Octahedron":
                    primitiveVisual.Shape = PrimitiveVisualShapeType.Octahedron;
                    primitiveVisual.ScaleDimensions = new Vector3(1.0f, 0.7f, 1.0f);
                    primitiveVisual.MixColor = Color.Green;
                    break;

                case "BevelledCube":
                    primitiveVisual.Shape = PrimitiveVisualShapeType.BevelledCube;
                    primitiveVisual.ScaleDimensions = new Vector3(0.0f, 0.5f, 1.1f);
                    primitiveVisual.BevelPercentage = 0.3f;
                    primitiveVisual.BevelSmoothness = 0.0f;
                    break;
            }

            return primitiveVisual;
        }

        // creates visual map for different kinds of primitives
        private VisualMap CreateVisualMap(string visualName)
        {
            VisualMap map = null;

            switch (visualName)
            {
                case "Border":
                    BorderVisual borderVisual = new BorderVisual();
                    // obligatory properties
                    borderVisual.Color = Color.Blue;
                    borderVisual.BorderSize = 15.0f;
                    map = borderVisual;
                    break;

                case "Color":
                    ColorVisual colorVisual = new ColorVisual(); 
                    // obligatory properties
                    colorVisual.MixColor = new Color(0.2f, 0.0f, 1.0f, 0.7f);
                    // optional properties
                    colorVisual.CornerRadius = 35.0f;
                    map = colorVisual;
                    break;

                case "RadialGradient":
                    GradientVisual radGradientVisual = new GradientVisual();
                    // obligatory properties
                    // coordinate system: top-left - (-0.5,-0.5); bottom-right - (0.5,0.5)
                    radGradientVisual.Center = new Vector2(0.0f, 0.0f);
                    radGradientVisual.Radius = 0.9f;
                    // optional properties
                    PropertyArray stopColor = new PropertyArray();
                    stopColor.Add(new PropertyValue(Color.Yellow));
                    stopColor.Add(new PropertyValue(Color.Blue));
                    stopColor.Add(new PropertyValue(new Color(0.0f, 1.0f, 0.0f, 1.0f)));
                    stopColor.Add(new PropertyValue(new Vector4(120.0f, 0.0f, 255.0f, 255.0f) / 255.0f));
                    radGradientVisual.StopColor = stopColor;
                    PropertyArray stopOffset = new PropertyArray();
                    stopOffset.Add(new PropertyValue(0.0f));
                    stopOffset.Add(new PropertyValue(0.2f));
                    stopOffset.Add(new PropertyValue(0.4f));
                    stopOffset.Add(new PropertyValue(0.6f));
                    radGradientVisual.StopOffset = stopOffset;
                    map = radGradientVisual;
                    break;

                case "LinearGradient":
                    GradientVisual linGradientVisual = new GradientVisual();
                    // obligatory properties
                    // coordinate system: top-left - (-0.5,-0.5); bottom-right - (0.5,0.5)
                    linGradientVisual.StartPosition = new Vector2(-0.5f, 0.5f);
                    linGradientVisual.EndPosition = new Vector2(0.5f, -0.5f);
                    // optional properties
                    linGradientVisual.StopColor = new PropertyArray();
                    linGradientVisual.StopColor.Add(new PropertyValue(Color.Green));
                    linGradientVisual.StopColor.Add(new PropertyValue(Color.Blue));
                    map = linGradientVisual;
                    break;

                case "Image":
                    ImageVisual imageVisual = new ImageVisual();
                    // obligatory properties
                    imageVisual.URL = _imageUrl + "belt.jpg";
                    // optional properties
                    imageVisual.Origin = Visual.AlignType.TopBegin;
                    imageVisual.AnchorPoint = Visual.AlignType.TopBegin;
                    imageVisual.RelativePosition = new RelativeVector2(0.1f, 0.1f);
                    map = imageVisual;
                    break;

                case "NPatch":
                    NPatchVisual nPatchVisual = new NPatchVisual();
                    // obligatory properties
                    nPatchVisual.URL = _imageUrl + "heartsframe.png";
                    // optional properties (for all visual types)
                    nPatchVisual.Origin = Visual.AlignType.Center;
                    nPatchVisual.AnchorPoint = Visual.AlignType.Center;
                    nPatchVisual.RelativePosition = new RelativeVector2(0.0f, 0.0f);
                    map = nPatchVisual;
                    break;

                case "SVG":
                    SVGVisual svgVisual = new SVGVisual();
                    // obligatory properties
                    svgVisual.URL = _imageUrl + "tiger.svg";
                    // optional properties (for all visual types)
                    svgVisual.Origin = Visual.AlignType.BottomBegin;
                    svgVisual.AnchorPoint = Visual.AlignType.BottomBegin;
                    svgVisual.RelativePosition = new RelativeVector2(0.1f, -0.1f);
                    map = svgVisual;
                    break;

                case "Animated":
                    AnimatedImageVisual animatedVisual = new AnimatedImageVisual();
                    // obligatory properties
                    animatedVisual.URL = _imageUrl + "buble.gif";
                    // optional properties (for all visual types)
                    animatedVisual.Origin = Visual.AlignType.TopEnd;
                    animatedVisual.AnchorPoint = Visual.AlignType.TopEnd;
                    animatedVisual.RelativePosition = new RelativeVector2(-0.1f, 0.1f);
                    map = animatedVisual;
                    break;

                case "Mesh":
                    MeshVisual meshVisual = new MeshVisual();
                    // obligatory properties
                    meshVisual.ObjectURL = _imageUrl + "MickeyMouse.obj";
                    // optional properties (for all visual map types)
                    meshVisual.Origin = Visual.AlignType.BottomEnd;
                    meshVisual.AnchorPoint = Visual.AlignType.BottomEnd;
                    meshVisual.RelativePosition = new RelativeVector2(-0.1f, -0.1f);
                    meshVisual.MixColor = new Color(0.52f, 0.62f, 0.96f, 0.9f);
                    map = meshVisual;
                    break;
            }

            // properties common for visuals groups
            switch (visualName)
            {
                case "Border":
                case "Color":
                case "RadialGradient":
                case "LinearGradient":
                    map.RelativeSize = new RelativeVector2(0.3f, 0.2f);
                    map.Origin = Visual.AlignType.TopBegin;
                    map.AnchorPoint = Visual.AlignType.TopBegin;
                    break;

                case "Image":
                case "NPatch":
                case "SVG":
                case "Animated":
                case "Mesh":
                    map.RelativeSize = new RelativeVector2(_imageRelWidth, _imageRelHeight);
                    break;

                case "Sphere":
                case "ConicalFrustrum":
                case "Cone":
                case "Cylinder":
                case "BevelledCube":
                case "Octahedron":
                case "Cube":
                    map = CreatePrimitiveVisual(visualName);
                    map.RelativeSize = new RelativeVector2(_primitiveRelWidth, _primitiveRelHeight);
                    map.Origin = Visual.AlignType.TopCenter;
                    map.AnchorPoint = Visual.AlignType.Center;
                    break;
            }

            return map;
        }

        // sets the visual view that is visible after clicking the button1 - contains visuals text, border, color, gradient
        private VisualView CreateVisualView1()
        {
            VisualView current = new VisualView();
            current.Size2D = new Size2D(_currentWidth, _currentHeight);
            current.ParentOrigin = ParentOrigin.TopLeft;;
            current.PositionUsesPivotPoint = true;
            current.PivotPoint = PivotPoint.TopLeft;
            current.Position2D = new Position2D(_frameSize, 0);
            current.BackgroundColor = Color.White;

            VisualMap visualMap = null;
            string visualType;

            // the main title
            visualMap = CreateTextVisual("VISUALS", 20.0f, new RelativeVector2(0.5f, 0.0f));
            current.AddVisual("TextVisuals", visualMap);

            // border visual and its title
            visualType = "Border";
            visualMap = CreateVisualMap(visualType);
            visualMap.RelativePosition = new RelativeVector2(0.025f, 0.2f);
            current.AddVisual(visualType, visualMap);

            visualMap = CreateTextVisual(visualType, 13.0f, new RelativeVector2(0.175f, 0.4f));
            current.AddVisual("Text" + visualType, visualMap);

            // border visual - underneath the previous one
            BorderVisual borderVisual = (BorderVisual)CreateVisualMap(visualType);
            borderVisual.Color = Color.Green;
            borderVisual.RelativePosition = new RelativeVector2(0.045f, 0.18f);
            borderVisual.DepthIndex = visualMap.DepthIndex - 1;
            current.AddVisual(visualType + "2", borderVisual);

            // color visual and its title
            visualType = "Color";
            visualMap = CreateVisualMap(visualType);
            visualMap.RelativePosition = new RelativeVector2(0.675f, 0.2f);
            current.AddVisual(visualType, visualMap);

            visualMap = CreateTextVisual(visualType, 13.0f, new RelativeVector2(0.825f, 0.4f));
            current.AddVisual("Text" + visualType, visualMap);

            // radial gradient 1
            visualType = "Gradient";
            visualMap = CreateVisualMap("Radial" + visualType);
            visualMap.RelativePosition = new RelativeVector2(0.025f, 0.6f);
            current.AddVisual("Radial" + visualType + "1", visualMap);

            // linear gradient and the title
            visualMap = CreateVisualMap("Linear" + visualType);
            visualMap.RelativePosition = new RelativeVector2(0.350f, 0.6f);
            current.AddVisual("Linear" + visualType, visualMap);

            visualMap = CreateTextVisual(visualType, 13.0f, new RelativeVector2(0.5f, 0.8f));
            current.AddVisual("Text" + visualType, visualMap);

            // radial gradient 2
            GradientVisual gradientVisual = (GradientVisual)CreateVisualMap("Radial" + visualType);
            gradientVisual.Center = new Vector2(0.2f, 0.4f);
            gradientVisual.Radius = 0.2f;
            gradientVisual.StopColor = new PropertyArray();
            gradientVisual.StopColor.Add(new PropertyValue(Color.Blue));
            gradientVisual.StopColor.Add(new PropertyValue(Color.Green));
            gradientVisual.SpreadMethod = GradientVisualSpreadMethodType.Repeat;
            gradientVisual.RelativePosition = new RelativeVector2(0.675f, 0.6f);
            current.AddVisual("Radial" + visualType + "2", gradientVisual);

            // current added to the _mainVisualView
            _mainVisualView.Add(current);
            return current;
        }

        // sets visual view visible after clicking the button2 - contains visuals images
        private VisualView CreateVisualView2()
        {
            VisualView current = new VisualView();
            current.Size2D = new Size2D(_currentWidth, _currentHeight);
            current.ParentOrigin = ParentOrigin.TopLeft;;
            current.PositionUsesPivotPoint = true;
            current.PivotPoint = PivotPoint.TopLeft;
            current.Position2D = new Position2D(_winWidth + _frameSize, 0);
            current.BackgroundColor = Color.White;

            VisualMap visualMap = null;

            // the main title
            visualMap = CreateTextVisual("VISUALS", 20.0f, new RelativeVector2(0.5f, 0.0f));
            current.AddVisual("TextVisuals", visualMap);

            RelativeVector2[] imageTextRelPosition = {new RelativeVector2(0.3f,0.3f),
                                                      new RelativeVector2(0.7f,0.3f),
                                                      new RelativeVector2(0.3f,0.9f),
                                                      new RelativeVector2(0.7f,0.9f),
                                                      new RelativeVector2(0.5f,0.6f)};

            for (uint i = 0; i < _numberImages; ++i)
            {
                visualMap = CreateVisualMap(_imageType[i]);
                // to show how nice NPatch can be stretched
                if (_imageType[i] == "NPatch")
                    visualMap.RelativeSize = new RelativeVector2(2 * _imageRelWidth, _imageRelHeight);
                current.AddVisual(_imageType[i], visualMap);
                current.AddVisual("Text" + _imageType[i], CreateTextVisual(_imageType[i], 9.0f, imageTextRelPosition[i]));
            }

            _mainVisualView.Add(current);
            return current;
        }

        // setting the visual view visible after clicking the button3 - contains visuals primitives
        private VisualView CreateVisualView3()
        {
            VisualView current = new VisualView();
            current.Size2D = new Size2D(_currentWidth, _currentHeight);
            current.ParentOrigin = ParentOrigin.TopLeft;;
            current.PositionUsesPivotPoint = true;
            current.PivotPoint = PivotPoint.TopLeft;
            current.Position2D = new Position2D(2 * _winWidth + _frameSize, 0);
            current.BackgroundColor = Color.White;

            VisualMap visualMap = null;

            // the main title
            visualMap = CreateTextVisual("VISUALS", 20.0f, new RelativeVector2(0.5f, 0.0f));
            current.AddVisual("TextVisuals", visualMap);

            float dw = (1.0f + _primitiveRelWidth) / 6.0f;
            float dh = (float)((0.9f - Math.Ceiling(_numberPrimitives / 2.0f) * _primitiveRelHeight) / (1.0f + Math.Ceiling(_numberPrimitives / 2.0f)));
            RelativeVector2 visualRelPos = new RelativeVector2(-dw, 0.1f + dh);

            for (uint i = 0; i < _numberPrimitives; ++i)
            {
                visualMap = CreateVisualMap(_primitiveName[i]);
                visualMap.RelativePosition = visualRelPos + new RelativeVector2(0, 0.06f);
                current.AddVisual(_primitiveName[i], visualMap);
                current.AddVisual("Text" + _primitiveName[i], CreateTextVisual(_primitiveName[i], 9.0f, visualRelPos + new RelativeVector2(0.5f, _primitiveRelHeight)));
                if (i % 2 == 0)
                    visualRelPos += new RelativeVector2(2.0f * dw, (dh + _primitiveRelHeight) / 2.0f);
                else
                    visualRelPos += new RelativeVector2(-2.0f * dw, (dh + _primitiveRelHeight) / 2.0f);
            }

            _mainVisualView.Add(current);
            return current;
        }

        void Initialize()
        {
            Window.Instance.KeyEvent += OnKeyEvent;
            Window.Instance.BackgroundColor = Color.Blue;
            Size2D winSize = Window.Instance.WindowSize;
            _winWidth = winSize.Width;
            int winHeight = winSize.Height;


            // the buttons visual view
            _buttonVisualView = new VisualView();
            _buttonVisualView.Size2D = new Size2D((int)(_winWidth - 2 * _frameSize), (int)((winHeight - 3 * _frameSize) * _buttonToMainRatio));
            _buttonVisualView.ParentOrigin = ParentOrigin.BottomLeft;
            _buttonVisualView.PositionUsesPivotPoint = true;
            _buttonVisualView.PivotPoint = PivotPoint.BottomLeft;
            _buttonVisualView.Position2D = new Position2D(_frameSize, -_frameSize);
            InitializeButtons();
            _buttons[0].BackgroundColor = new Color(0.66f, 0.6f, 0.9f, 1);
            Window.Instance.Add(_buttonVisualView);

            // the main visual view
            _mainVisualView = new VisualView();
            _currentWidth = _winWidth - 2 * _frameSize;
            _currentHeight = (int)((winHeight - 3 * _frameSize) * (1 - _buttonToMainRatio));
            _mainVisualView.Size2D = new Size2D(3 * _winWidth, _currentHeight);
            _mainVisualView.ParentOrigin = ParentOrigin.TopLeft;;
            _mainVisualView.PositionUsesPivotPoint = true;
            _mainVisualView.PivotPoint = PivotPoint.TopLeft;
            _mainVisualView.Position2D = new Position2D(0, _frameSize);
            _mainVisualView.BackgroundColor = Color.Blue;
            Window.Instance.Add(_mainVisualView);

            CurrentVisualView.Add(CreateVisualView1());
            CurrentVisualView.Add(CreateVisualView2());
            CurrentVisualView.Add(CreateVisualView3());

        }

        private void OnKeyEvent(object sender, Window.KeyEventArgs e)
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
