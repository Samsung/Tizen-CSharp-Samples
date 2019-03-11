using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIFontTest
{
    class Program : NUIApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window.Instance.KeyEvent += OnKeyEvent;
            FontClient.Instance.AddCustomFontDirectory(this.DirectoryInfo.Resource);
            TextLabel text = new TextLabel("Label using Tizen default font");
            text.Position = new Position(new Position2D(15, 140));
            //text.HorizontalAlignment = HorizontalAlignment.Center;
            //text.VerticalAlignment = VerticalAlignment.Center;
            text.TextColor = Color.Blue;
            text.PointSize = 6.0f;
            text.HeightResizePolicy = ResizePolicyType.FillToParent;
            text.WidthResizePolicy = ResizePolicyType.FillToParent;
            Window.Instance.GetDefaultLayer().Add(text);

            TextLabel text2 = new TextLabel("Apply a custom font to this label!");
            text2.FontFamily = "Lobster";
            text2.Position = new Position(new Position2D(30, 210));
            text2.TextColor = Color.White;
            text2.PointSize = 6.0f;
            text2.HeightResizePolicy = ResizePolicyType.FillToParent;
            text2.WidthResizePolicy = ResizePolicyType.FillToParent;
            Window.Instance.GetDefaultLayer().Add(text2);

            //Animation animation = new Animation(2000);
            //animation.AnimateTo(text, "Orientation", new Rotation(new Radian(new Degree(180.0f)), PositionAxis.X), 0, 500);
            //animation.AnimateTo(text, "Orientation", new Rotation(new Radian(new Degree(0.0f)), PositionAxis.X), 500, 1000);
            //animation.Looping = true;
            //animation.Play();
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
