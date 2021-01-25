﻿/*
  * Copyright (c) 2016 Samsung Electronics Co., Ltd
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

namespace NUI_CustomView
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
            Window window = Window.Instance;
            window.KeyEvent += OnKeyEvent;

            ImageView background = new ImageView(DirectoryInfo.Resource + "/images/bg.png");
            background.Size2D = new Size2D(window.Size.Width, window.Size.Height);
            window.Add(background);
            View mainView = new View();

            LinearLayout linearLayout = new LinearLayout();
            linearLayout.LinearOrientation = LinearLayout.Orientation.Vertical;
            linearLayout.CellPadding = new Size2D(0, 13);
            linearLayout.Padding = new Extents(10, 10, 10, 10);

            mainView.Layout = linearLayout;
            window.Add(mainView);

            for (int i = 0; i < 9; i++)
            {
                NUI_CustomView.ContactView contactView = new ContactView("Test: " + i.ToString(), DirectoryInfo.Resource);
                contactView.Size2D = new Size2D(window.Size.Width - 20, window.Size.Height / 10);
                mainView.Add(contactView);
            }
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
            app.Dispose();
        }
    }
}
