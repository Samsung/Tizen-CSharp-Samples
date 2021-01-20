/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUILayer
{
    class Program : NUIApplication
    {
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

            ImageView background = new ImageView(DirectoryInfo.Resource + "/images/bg.png");
            appWindow.Add(background);
            background.Size2D = new Size2D(appWindow.Size.Width, appWindow.Size.Height);
            background.Position2D = new Position2D(0, 0);

            menuLayer = new Layer();
            appWindow.AddLayer(menuLayer);

            TextLabel leftLabel = new TextLabel("Tap left side of the screen to show menu");
            leftLabel.Size2D = new Size2D(appWindow.Size.Width / 2 - 40, appWindow.Size.Height);
            leftLabel.Position2D = new Position2D(20, 0);
            leftLabel.HorizontalAlignment = HorizontalAlignment.Center;
            leftLabel.VerticalAlignment = VerticalAlignment.Center;
            leftLabel.MultiLine = true;
            background.Add(leftLabel);

            TextLabel rightLabel = new TextLabel("Tap right side of the screen to hide menu");
            rightLabel.Size2D = new Size2D(appWindow.Size.Width / 2 - 40, appWindow.Size.Height);
            rightLabel.Position2D = new Position2D(appWindow.Size.Width / 2 + 20, 0);
            rightLabel.HorizontalAlignment = HorizontalAlignment.Center;
            rightLabel.VerticalAlignment = VerticalAlignment.Center;
            rightLabel.MultiLine = true;
            background.Add(rightLabel);

            menu = new View();
            menu.BackgroundColor = new Color(0.6f, 0.6f, 0.6f, 0.5f);
            menu.Size2D = new Size2D(100, appWindow.Size.Height);

            menuLayer.Add(menu);
            addIcons(menu);
        }

        private void addIcons(View view)
        {
            LinearLayout iconLayout = new LinearLayout();
            iconLayout.Padding = new Extents(10, 10, 10, 10);
            iconLayout.LinearOrientation = LinearLayout.Orientation.Vertical;
            iconLayout.CellPadding = new Size2D(20, 38);
            view.Layout = iconLayout;

            foreach (var item in icons)
            {
                ImageView image = new ImageView(DirectoryInfo.Resource + "/images/" + item);
                image.Size2D = new Size2D(70, 70);
                view.Add(image);
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
                //Verify witch side of the screen was clicked by user.
                //Touch position is compared with half of the screen size.
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
