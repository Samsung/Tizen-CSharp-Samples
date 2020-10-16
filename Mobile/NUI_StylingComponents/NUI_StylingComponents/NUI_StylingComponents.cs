using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace NUI_StylingComponents
{
    internal class Program : NUIApplication
    {
        private Window window = null;
        private Button testButton = null;
        private TextLabel testLabel = null;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }
        private class StylingButton : Button
        {
            public StylingButton(string text, Position2D position)
            {
                base.Size2D = new Size2D(320, 100);
                base.Position2D = position;
                base.Text = text;
                base.TextColorSelector = new ColorSelector
                {
                    Normal = Color.Black,
                    Pressed = new Color(0.4f, 0.4f, 0.4f, 1)
                };
            }
        }

        private void Initialize()
        {
            Tizen.NUI.StyleManager.Get().StyleChanged += OnStyleChanged;

            window = Window.Instance;
            window.KeyEvent += OnKeyEvent;
            window.BackgroundColor = Color.White;

            ImageView bg = new ImageView(DirectoryInfo.Resource + "/images/bg.png");
            bg.Size2D = new Size2D(window.Size.Width, window.Size.Height);
            window.Add(bg);

            NPatchVisual nVisual = new NPatchVisual();
            nVisual.URL = DirectoryInfo.Resource + "/images/rectangle_btn_normal.png";
            nVisual.Border = new Rectangle(5, 5, 5, 5);

            StylingButton applyButton = new StylingButton("Apply Style", new Position2D(200, 1150));
            applyButton.ClickEvent += OnApplyButtonClicked;
            applyButton.Background = nVisual.OutputVisualMap;
            window.Add(applyButton);

            testButton = new Button();
            testButton.Size2D = new Size2D(400, 200);
            testButton.Position = new Position(window.Size.Width / 2 - 200, window.Size.Height / 2 - 100);
            testButton.Text = "TEST BUTTON";
            testButton.BackgroundColor = new Color(80, 80, 80, 1);
            testButton.TextColorSelector = new ColorSelector
            {
                Normal = Color.Black,
                Pressed = Color.Green
            };
            window.Add(testButton);

            testLabel = new TextLabel();
            testLabel.Size2D = new Size2D(400, 200);
            testLabel.Position = new Position(testButton.Position.X, testButton.Position.Y - 200);
            testLabel.Text = "TEST LABEL";
            testLabel.HorizontalAlignment = HorizontalAlignment.Center;
            window.Add(testLabel);

            Tizen.NUI.StyleManager.Get().ApplyTheme(DirectoryInfo.Resource + "/styles/theme.json");
        }

        private void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }

        private void OnApplyButtonClicked(object sender, Button.ClickEventArgs args)
        {
            if (testButton)
            {
                testButton.SetStyleName("CustomButton");
            }

            if (testLabel)
            {
                testLabel.SetStyleName("CustomLabel");
            }
        }

        private void OnStyleChanged(object sender, Tizen.NUI.StyleManager.StyleChangedEventArgs args)
        {
        }

        private static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}