using System;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace NUI_CheckBox
{
    // Custom style of the checkbox
    internal class CustomCheckBoxStyle : StyleBase
    {
        string _URL = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/";
        private Size2D _size = new Size2D(200,200);

        protected override ViewStyle GetViewStyle()
        {
            ButtonStyle style = new ButtonStyle
            {
                // Square area connected with click event
                Size = _size,
                Icon = new ImageViewStyle
                {
                    Size = _size,
                    ResourceUrl = new Selector<string>
                    {
                        Other = "",
                        Selected = _URL + "RoundCheckMark.png"
                    },
                    // Round shadow, but the area connected with click event remains square
                    ImageShadow = new ImageShadow(_URL + "RoundShadow.png"),
                },
                ParentOrigin = ParentOrigin.BottomCenter,
                PositionUsesPivotPoint = true,
                PivotPoint = PivotPoint.BottomCenter
            };
            return style;
        }
    }

    class Program : NUIApplication
    {
        // Path to the directory with images
        static string _URL = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/";
        private View _view;
        private CheckBox _checkBox;
        private CheckBox _customCheckBox;
        private Size2D _checkBoxSize = new Size2D(200,200);
        private int _space = 50;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window.Instance.KeyEvent += Window_KeyEvent;

            _view = new View();
            _view.Size = new Size2D(1, 4) * _checkBoxSize + new Size2D(100, 5 * _space);
            _view.PivotPoint = PivotPoint.Center;
            _view.ParentOrigin = ParentOrigin.Center;
            _view.PositionUsesPivotPoint = true;
            _view.BackgroundColor = Color.White;
            Window.Instance.Add(_view);

            // Default CheckBox
            _checkBox = new CheckBox();
            _checkBox.Size = _checkBoxSize;
            _checkBox.ParentOrigin = ParentOrigin.TopCenter;
            _checkBox.PositionUsesPivotPoint = true;
            _checkBox.PivotPoint = PivotPoint.TopCenter;
            _checkBox.Position = new Position(0, _space);
            _view.Add(_checkBox);

            // Create with properties
            _checkBox = new CheckBox();
            _checkBox.Size = _checkBoxSize;
            _checkBox.ParentOrigin = ParentOrigin.Center;
            _checkBox.PositionUsesPivotPoint = true;
            _checkBox.PivotPoint = PivotPoint.Center;
            _checkBox.Position = new Position(0, - (_checkBoxSize.Height + _space) / 2);
            // Set the icon images for different check box states 
            StringSelector _iconURL = new StringSelector()
            {
                Normal           = _URL + "Blue.png",
                Selected         = _URL + "BlueCheckMark.png",
                Pressed          = _URL + "Red.png",
           };
           _checkBox.IconURLSelector = _iconURL;
           _checkBox.Icon.Size = new Size2D(160,160);
           _checkBox.BackgroundColor = new Color(0.57f, 0.7f, 1.0f, 0.8f);
           // CheckBox initial state set to be selected
           _checkBox.IsSelected = true;
           // CheckBox can be selected
           _checkBox.IsSelectable = true;
           // CheckBox is enabled
           _checkBox.IsEnabled = true;
           _view.Add(_checkBox);

            // Create with style - since the CheckBox inherits after the Button class, the ButtonStyle is used
            ButtonStyle _style = new ButtonStyle
            {
                IsSelectable = true,
                ParentOrigin = ParentOrigin.Center,
                PositionUsesPivotPoint = true,
                PivotPoint = PivotPoint.Center,
                Position = new Position(0, (_checkBoxSize.Height + _space) / 2),
                Size = _checkBoxSize,
                // Gray structural background
                BackgroundImage = _URL + "Struct.png",
                // Image overlaid on the background
                Icon = new ImageViewStyle
                {
                    Size =  _checkBoxSize,
                    // Different icon used depending on the status
                    ResourceUrl = new Selector<string>
                    {
                        Other = _URL + "XSign.png",
                        Selected = _URL + "CheckMark.png"
                    },
                    // Icon opacity set to 0.8 for all checkbox states
                    Opacity = 0.8f,
                    // Shadow visible for all states
                    ImageShadow = new ImageShadow(_URL + "Shadow.png")
                },
                // Style of the overlay image
                Overlay = new ImageViewStyle()
                {
                    ResourceUrl = new Selector<string>
                    {
                        Pressed = _URL + "Red.png",
                        Other   = _URL + "LightBlue.png"
                    },
                    Opacity = new Selector<float?> {Pressed = 0.3f, Other = 1.0f}
                }
            };
            _checkBox = new CheckBox(_style);
            _view.Add(_checkBox);

            // Create with custom style
            // Custom style registration
            Tizen.NUI.Components.StyleManager.Instance.RegisterStyle("_CustomCheckBoxStyle", null, typeof(NUI_CheckBox.CustomCheckBoxStyle));
            _customCheckBox = new CheckBox("_CustomCheckBoxStyle");
            _customCheckBox.Position = new Position(0, -_space);
            // Click event handle
            _customCheckBox.Clicked += OnClicked;
            _view.Add(_customCheckBox);
        }

        // Called after clicking the checkbox
        private void OnClicked(object sender, EventArgs e)
        {
            if (_customCheckBox.IsSelected)
                _view.BackgroundColor = Color.Green;
            else
                _view.BackgroundColor = Color.White;
        }

        // Responds on pressing chosen buttons
        private void Window_KeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "Escape" || e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "BackSpace")
                {
                    Exit();
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
