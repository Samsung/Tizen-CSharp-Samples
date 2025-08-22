/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Tizen;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace SimpleLayout
{
    class SimpleLayout : NUIApplication
    {
        private static readonly string LogTag = "LAYOUT";

        public static readonly string ItemContentNameIcon = "ItemIcon";
        public static readonly string ItemContentNameTitle = "ItemTitle";
        public static readonly string ItemContentNameDescription = "ItemDescription";

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        private class ListItem
        {
            private string IconPath = null;
            private string Label = null;
            private string Description = null;

            public ListItem(string iconPath, string label)
            {
                IconPath = iconPath;
                Label = label;
            }

            public ListItem(string iconPath, string label, string description)
            {
                IconPath = iconPath;
                Label = label;
                Description = description;
            }

            public string GetIconPath()
            {
                return IconPath;
            }

            public string GetLabel()
            {
                return Label;
            }

            public string GetDescription()
            {
                return Description;
            }
        };

        private static readonly ListItem[] ListItems =
        {
            new ListItem("/images/application-icon-101.png", "Gallery", "Local images viewer"),
            new ListItem("/images/application-icon-102.png", "Wifi"),
            new ListItem("/images/application-icon-103.png", "Apps", "List of available applications"),
            new ListItem("/images/application-icon-104.png", "Desktops", "List of available desktops")
        };

        private void Initialize()
        {
            Log.Debug(LogTag, "Application initialize started...");
            // Change the background color of Window to White & respond to key events
            Window window = Window.Instance;
            window.BackgroundColor = Color.White;
            window.KeyEvent += OnKeyEvent;

            //Create background
            Log.Debug(LogTag, "Creating background...");
            ImageView im = new ImageView(DirectoryInfo.Resource + "/images/bg.png");
            im.Size2D = new Size2D(window.Size.Width, window.Size.Height);
            window.Add(im);

            //Create Linear Layout
            Log.Debug(LogTag, "Creating linear layout...");
            LinearLayout linearLayout = new LinearLayout();
            linearLayout.LinearOrientation = LinearLayout.Orientation.Vertical;
            linearLayout.CellPadding = new Size2D(20, 20);

            //Create main view
            View mainView = new View();
            mainView.Size2D = new Size2D(window.Size.Width, window.Size.Height);
            mainView.Layout = linearLayout;

            //Create custom items and add it to view
            for (int i = 0; i < 4; i++)
            {
                //Create item base view.
                View itemView = new View();
                itemView.BackgroundColor = new Color(0.47f, 0.47f, 0.47f, 0.5f);
                itemView.Name = "ItemView_" + i.ToString();

                //Create item layout responsible for positioning in each item
                ItemLayout itemLayout = new ItemLayout();
                itemView.Layout = itemLayout;

                //Crate item icon
                ImageView icon = new ImageView(DirectoryInfo.Resource + ListItems[i].GetIconPath());
                icon.Size2D = new Size2D(100, 100);
                icon.Name = ItemContentNameIcon;
                itemView.Add(icon);

                PropertyMap titleStyle = new PropertyMap();
                titleStyle.Add("weight", new PropertyValue("semiBold"));

                //Create item title
                TextLabel title = new TextLabel(ListItems[i].GetLabel());
                title.Size2D = new Size2D(400, 60);
                title.FontStyle = titleStyle;
                title.Name = ItemContentNameTitle;
                title.PixelSize = 38.0f;
                itemView.Add(title);

                string strDescription = ListItems[i].GetDescription();
                if (strDescription != null)
                {
                    TextLabel description = new TextLabel(strDescription);
                    description.Size2D = new Size2D(500, 50);
                    description.Name = ItemContentNameDescription;
                    description.PixelSize = 28.0f;
                    itemView.Add(description);
                }
                else
                {
                    title.Size2D = new Size2D(400, 110);
                }

                mainView.Add(itemView);
            }

            window.Add(mainView);
        }

        private void OnKeyEvent(object sender, Window.KeyEventArgs eventArgs)
        {
            if (eventArgs.Key.State == Key.StateType.Down)
            {
                switch (eventArgs.Key.KeyPressedName)
                {
                    case "Back":
                    case "XF86Back": //for emulator
                        Exit();
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            SimpleLayout simpleLayout = new SimpleLayout();
            simpleLayout.Run(args);
            simpleLayout.Dispose();
        }
    }
}
