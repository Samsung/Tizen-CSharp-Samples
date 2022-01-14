﻿/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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
using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Badges
{
    /// <summary>
    /// Represents NUI forms of tizen platform app
    /// </summary>
    public class Program : NUIApplication
    {
        /// <summary>
        /// On create method
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            Window window = Window.Instance;
            window.BackgroundColor = Color.Blue;
            window.KeyEvent += OnKeyEvent;
            MainPage page = new MainPage();
            window.Add(page);
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }
        /// <summary>
        /// Main method of sample account tizen mobile
        /// </summary>
        /// <param name="args"> Arguments </param>
        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}