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

namespace ScrollableBaseExample
{
    class ScrollableBaseExample : NUIApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        /// <summary>
        /// ScrollableBase Simple App initializations.
        /// </summary>
        void Initialize()
        {
            Window window = Window.Instance;
            window.KeyEvent += OnKeyEvent;
            ScrollableBase scrollableBase = new ScrollableBase();
            scrollableBase.Size = new Size(720, 1280);
            scrollableBase.ScrollingDirection = ScrollableBase.Direction.Vertical;
            Window.Instance.GetDefaultLayer().Add(scrollableBase);

            scrollableBase.ScrollDragStarted += ScrollDragStarted;
            scrollableBase.ScrollDragEnded += ScrollDragEnded;

            View[] items;
            items = new View[10];
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new View
                {
                    Position = new Position(0, i * 200),
                    Size = new Size(720, 200),
                };
                if (i % 2 == 0)
                {
                    items[i].BackgroundColor = Color.Magenta;
                }
                else
                {
                    items[i].BackgroundColor = Color.Cyan;
                }
                scrollableBase.Add(items[i]);
            }
        }

        /// <summary>
        /// This Application will be exited when back key entered.
        /// </summary>
        /// <param name="sender">Window.Instance</param>
        /// <param name="e">event</param>
        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape" || e.Key.KeyPressedName == "BackSpace")
                {
                    Exit();
                }
            }
        }

        private void ScrollDragStarted(object sender, ScrollEventArgs e)
        {
            // Do something in response to scroll drag started
        }

        private void ScrollDragEnded(object sender, ScrollEventArgs e)
        {
            // Do something in response to scroll drag ended
        }

        /// <summary>
        /// The enter point of the application.
        /// </summary>
        /// <param name="args">args</param>
        static void Main(string[] args)
        {
            var app = new ScrollableBaseExample();
            app.Run(args);
        }
    }
}
