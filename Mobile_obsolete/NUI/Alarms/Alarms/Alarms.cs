﻿/* 
  * Copyright (c) 2022 Samsung Electronics Co., Ltd 
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
using Tizen.NUI.Components;

namespace Alarms
{
    public class Program : NUIApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            Window window = GetDefaultWindow();
            window.KeyEvent += OnKeyEvent;

            Navigator navigator = window.GetDefaultNavigator();

            MainPage page = new MainPage();
            navigator.Push(page);
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