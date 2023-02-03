/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
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
 */

using Piano.Models;
using Piano.Views;
using System;
using Tizen.NUI;

namespace Piano.Tizen.Mobile
{
    class Program : NUIApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();

            Window window = Window.Instance;
            window.BackgroundColor = Color.Cyan;
            window.KeyEvent += OnKeyEvent;

            try
            {
                Sound.Init();
            }
            catch (Exception e)
            {
                global::Tizen.Log.Error("Piano", "Init : " + e.Message);
            }

            MainPage page = new MainPage();
            window.Add(page);
            window.AddAvailableOrientation(Window.WindowOrientation.Landscape);
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