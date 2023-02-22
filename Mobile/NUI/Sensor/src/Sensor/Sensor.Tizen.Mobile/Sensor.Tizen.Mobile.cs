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
using Sensor.Pages;
using System;
using System.Threading;
using Tizen.NUI;
using Tizen.NUI.Components;

namespace Sensor.Tizen.Mobile
{
    class Program : NUIApplication
    {
        public static Window window;
        public static Navigator navigator;
        public static SensorManager sensorManager;
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
            navigator.Push(new SensorMain());
        }

        void Initialize()
        {
            window = GetDefaultWindow();
            window.Title = "Sensor";
            window.KeyEvent += OnKeyEvent;
            navigator = window.GetDefaultNavigator();
            sensorManager = new SensorManager();
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
