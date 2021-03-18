/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
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
 *
 */

using System;
using Tizen;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace NotificationExample
{
    class NotificationExample : NUIApplication
    {
        /// <summary>
        /// Overrides this method if want to handle behaviour.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        /// <summary>
        /// Show Notification function.
        /// </summary>
        void ShowNotification()
        {
            View ContentView = new View();
            ContentView.Size2D = new Size2D(720, 100);
            ContentView.Position = new Position(0, 0, 0);
            ContentView.BackgroundColor = new Color(0, 255, 0, 1.0f);

            Notification notification = new Notification(ContentView);

            Animation DismissAni = new Animation(100);
            notification.SetAnimationOnDismiss(DismissAni);

            Animation PostAni = new Animation(100);
            notification.SetAnimationOnPost(PostAni);

            notification.SetLevel(NotificationLevel.Base);

            Rectangle PositionSize = new Rectangle(20, 0, 680, 100);
            notification.SetPositionSize(PositionSize);

            bool DismissOnTouch = true;
            notification.SetDismissOnTouch(DismissOnTouch);     // Dismiss notification window on touch if the value is true.

            TextLabel text = new TextLabel("This is a notification sample.");
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;

            text.TextColor = Color.Black;
            text.Size2D = new Size2D(720, 100);

            ContentView.Add(text);
            Window.Instance.Add(ContentView);

            // show Notification for 5 seconds
            notification.Post(5000);

        }

        /// <summary>
        /// Notification Sample Application initializations.
        /// </summary>
        void Initialize()
        {
            Window window = Window.Instance;

            View root = new View()
            {
                Size = new Size(window.Size.Width, window.Size.Height),
                BackgroundColor = Color.Black
            };

            window.Add(root);

            window.KeyEvent += OnKeyEvent;

            //show notification each 10 seconds
            Timer timer = new Timer(10000);
            timer.Tick += TimerTick;
            timer.Start();
        }

        /// <summary>
        /// Timer Tick event handling of Window
        /// </summary>
        /// <param name="source">Source Object</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool TimerTick(object source, Timer.TickEventArgs e)
        {
            ShowNotification();
            return true;
        }

        /// <summary>
        /// This Application will be exited when back key entered.
        /// </summary>
        /// <param name="sender">Window.Instance</param>
        /// <param name="e">event</param>
        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }

        /// <summary>
        /// The enter point of the application.
        /// </summary>
        /// <param name="args">args</param>
        static void Main(string[] args)
        {
            Log.Info("Tag", "========== Hello, Notifciation Example ==========");
            var app = new NotificationExample();
            app.Run(args);
        }
    }
}
