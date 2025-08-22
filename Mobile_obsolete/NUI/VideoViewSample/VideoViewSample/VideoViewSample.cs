/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace VideoViewSample
{
    class Program : NUIApplication
    {
        /// <summary>
        /// Main Application Window instance.
        /// </summary>
        private Window ApplicationWindow;

        /// <summary>
        /// Button names array. Used to verify wich button was pressed and control the video player actions.
        /// </summary>
        private readonly string[] buttonNames = { "play", "pause", "stop", "forward", "backward" };

        /// <summary>
        /// Video Player Component. It is used to show video in the application window.
        /// </summary>
        private VideoView player;

        /// <summary>
        /// OnCreate Event Handler. First function called after Application constructor.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        /// <summary>
        /// On Key Pressed Event Handler. Used to exit application when back button was pressed.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event parameters</param>
        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                switch (e.Key.KeyPressedName)
                {
                    case "Escape":
                    case "Back":
                    case "XF86Back": //Handle back key for emulator
                        {
                            Exit();
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Function initializes application UI.
        /// </summary>
        void Initialize()
        {
            //Save the application window instance.
            ApplicationWindow = Window.Instance;
            ApplicationWindow.BackgroundColor = Color.White;

            //Setup event handlers.
            ApplicationWindow.KeyEvent += OnKeyEvent;

            //Create main application view.
            View mainView = new View();

            //Setup linear layout for components stored in main view.
            LinearLayout mainLayout = new LinearLayout()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                LinearOrientation = LinearLayout.Orientation.Vertical,
                CellPadding = new Size2D(0, 10),
                Padding = new Extents(10, 10, 10, 10),
            };
            mainView.WidthResizePolicy = ResizePolicyType.FillToParent;
            mainView.HeightResizePolicy = ResizePolicyType.FillToParent;
            mainView.Layout = mainLayout;
            ApplicationWindow.Add(mainView);

            //Create video view and play movie automatically.
            player = new VideoView()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                ResourceUrl = DirectoryInfo.Resource + "sample.3gp",
                Weight = 0.5f,
            };
            player.Play();
            mainView.Add(player);

            //Create button container layout.
            View buttonContainer = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                BackgroundColor = Color.Gainsboro,
                Weight = 0.5f,
            };
            LinearLayout buttonLayout = new LinearLayout()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                LinearOrientation = LinearLayout.Orientation.Vertical,
                CellPadding = new Size2D(0, 10),
            };
            buttonContainer.Layout = buttonLayout;
            mainView.Add(buttonContainer);

            //Create buttons and push them into the button container.
            for (int i = 0; i < buttonNames.Length; ++i)
            {
                Button btn = new Button
                {
                    Size2D = new Size2D(300, 100),
                    TextColor = Color.White,
                    Text = buttonNames[i],
                };
                btn.Clicked += OnClicked;
                buttonContainer.Add(btn);
            }
        }

        /// <summary>
        /// Button Clicked event handler.
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event parameters</param>
        private void OnClicked(object sender, ClickedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Text)
            {
                case "play":
                    player.Play();
                    break;
                case "pause":
                    player.Pause();
                    break;
                case "stop":
                    player.Stop();
                    break;
                case "forward":
                    player.Forward(2500); // +2.5 sec
                    break;
                case "backward":
                    player.Backward(2500); // -2.5 sec
                    break;
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
