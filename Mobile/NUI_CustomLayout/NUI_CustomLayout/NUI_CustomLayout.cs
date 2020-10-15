using System.Collections;
using System.Diagnostics;
using System.Net.Mime;
using System.Security;
using Tizen;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace SimpleLayout
{
    class SimpleLayout : NUIApplication
    {
        private static readonly string LOG_TAG = "LAYOUT";

        public static readonly string ITEM_CONTENT_NAME_ICON = "ItemIcon";
        public static readonly string ITEM_CONTENT_NAME_TITLE = "ItemTitle";
        public static readonly string ITEM_CONTENT_NAME_DESCRIPTION = "ItemDescription";
      
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

        private static readonly ListItem[] ListItems = {
            new ListItem("/images/application-icon-101.png", "Gallery", "Local images viewer"),
            new ListItem("/images/application-icon-102.png", "Wifi"),
            new ListItem("/images/application-icon-103.png", "Apps", "List of available applications"),
            new ListItem("/images/application-icon-104.png", "Desktops", "List of available desktops")
        };

        private void Initialize()
        {
            Log.Debug(LOG_TAG, "Application initialize started...");
            // Change the background color of Window to White & respond to key events
            Window window = Window.Instance;
            window.BackgroundColor = Color.White;
            window.KeyEvent += OnKeyEvent;

            //Create background
            Log.Debug(LOG_TAG, "Creating background...");
            ImageView im = new ImageView(DirectoryInfo.Resource + "/images/bg.png");
            im.Size2D = new Size2D(window.Size.Width, window.Size.Height);
            window.Add(im);

            //Create Linear Layout
            Log.Debug(LOG_TAG, "Creating linear layout...");
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
                View itemView = new View();
                itemView.BackgroundColor = new Color(120, 120, 120, 0.5f);
                itemView.Name = "ItemView_" + i.ToString();

                ItemLayout itemLayout = new ItemLayout();
                itemView.Layout = itemLayout;

                ImageView icon = new ImageView(DirectoryInfo.Resource + ListItems[i].GetIconPath());
                icon.Size2D = new Size2D(100, 100);
                icon.Name = ITEM_CONTENT_NAME_ICON;
                itemView.Add(icon);

                PropertyMap titleStyle = new PropertyMap();
                titleStyle.Add("weight", new PropertyValue(600));

                TextLabel title = new TextLabel(ListItems[i].GetLabel());
                title.Size2D = new Size2D(400, 50);
                title.FontStyle = titleStyle;
                title.Name = ITEM_CONTENT_NAME_TITLE;

                itemView.Add(title);

                string strDescription = ListItems[i].GetDescription();
                if (strDescription != null)
                {
                    TextLabel description = new TextLabel(strDescription);
                    description.Size2D = new Size2D(500, 50);
                    description.Name = ITEM_CONTENT_NAME_DESCRIPTION;
                    description.PixelSize = 24.0f;
                    itemView.Add(description);
                }

                mainView.Add(itemView);
            }

            window.Add(mainView);
        }

        /// <summary>
        /// Called when any key event is received.
        /// Will use this to exit the application if the Back or Escape key is pressed
        /// </summary>
        private void OnKeyEvent(object sender, Window.KeyEventArgs eventArgs)
        {
            if (eventArgs.Key.State == Key.StateType.Down)
            {
                switch (eventArgs.Key.KeyPressedName)
                {
                    case "Escape":
                    case "Back":
                        {
                            Exit();
                        }
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Log.Debug(LOG_TAG, "============================= START APP =====================================");
            SimpleLayout simpleLayout = new SimpleLayout();
            simpleLayout.Run(args);
        }
    }
}
