using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUILayer
{
    class Program : NUIApplication
    {
        private const string LOG_TAG = "NUI_LAYER";
        private const int ANIM_TIME = 1000;

        private Window appWindow;
        private View menu;
        private Layer menuLayer;

        private static readonly string[] icons = {
            "email.png",
            "maps.png",
            "messages.png",
            "notification.png",
            "phone.png",
            "recent.png",
            "schedule.png",
            "settings.png",
            "speedometer.png",
            "stopwatch.png",
            "timer.png",
            "voicememo.png"
        };

        protected override void OnCreate()
        {
            base.OnCreate();

            appWindow = Window.Instance;
            appWindow.BackgroundColor = Color.White;
            appWindow.KeyEvent += OnKeyEvent;
            appWindow.TouchEvent += OnWindowTouched;

            ImageView bg = new ImageView(DirectoryInfo.Resource + "/images/bg.png");
            appWindow.Add(bg);
            bg.Size2D = new Size2D(appWindow.Size.Width, appWindow.Size.Height);
            bg.Position2D = new Position2D(0, 0);

            menuLayer = new Layer();
            appWindow.AddLayer(menuLayer);

            TextLabel leftLabel = new TextLabel("Tap left side of the screen to show menu");
            leftLabel.Size2D = new Size2D(appWindow.Size.Width / 2 - 40, appWindow.Size.Height);
            leftLabel.Position2D = new Position2D(20, 0);
            leftLabel.HorizontalAlignment = HorizontalAlignment.Center;
            leftLabel.VerticalAlignment = VerticalAlignment.Center;
            leftLabel.MultiLine = true;
            bg.Add(leftLabel);

            TextLabel rightLabel = new TextLabel("Tap right side of the screen to hide menu");
            rightLabel.Size2D = new Size2D(appWindow.Size.Width / 2 - 40, appWindow.Size.Height);
            rightLabel.Position2D = new Position2D(appWindow.Size.Width / 2 + 20, 0);
            rightLabel.HorizontalAlignment = HorizontalAlignment.Center;
            rightLabel.VerticalAlignment = VerticalAlignment.Center;
            rightLabel.MultiLine = true;
            bg.Add(rightLabel);

            menu = new View();
            menu.BackgroundColor = new Color(120, 120, 120, 0.5f);
            menu.Size2D = new Size2D(100, appWindow.Size.Height);

            menuLayer.Add(menu);
            addIcons(menu);
        }

        private void addIcons(View v)
        {
            LinearLayout ly = new LinearLayout();
            ly.Padding = new Extents(10, 10, 10, 10);
            ly.LinearOrientation = LinearLayout.Orientation.Vertical;
            ly.CellPadding = new Size2D(20, 38);
            v.Layout = ly;

            foreach (var item in icons)
            {
                ImageView im = new ImageView(DirectoryInfo.Resource + "/images/" + item);
                im.Size2D = new Size2D(70, 70);
                v.Add(im);
            }
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                switch (e.Key.KeyPressedName)
                {
                    case "Escape":
                    case "Back":
                    case "XF86Back":
                        {
                            Exit();
                        }
                        break;
                }
            }
        }

        public void MenuShow()
        {
            menuLayer.Visibility = true;
        }

        public void MenuHide()
        {
            menuLayer.Visibility = false;
        }

        public void OnWindowTouched(object sender, Window.TouchEventArgs args)
        {
            if (args.Touch.GetState(0) == PointStateType.Down)
            {

                if (args.Touch.GetLocalPosition(0).X <= appWindow.Size.Width / 2)
                {
                    MenuShow();
                }
                else if (args.Touch.GetLocalPosition(0).X > appWindow.Size.Width / 2)
                {
                    MenuHide();
                }
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
