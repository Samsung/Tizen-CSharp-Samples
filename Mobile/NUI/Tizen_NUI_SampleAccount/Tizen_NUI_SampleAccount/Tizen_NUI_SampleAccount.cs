/* 
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

using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using Tizen.Account.AccountManager;

namespace Tizen_NUI_SampleAccount
{
    /// <summary>
    /// Represents NUI forms of tizen platform app
    /// </summary>
    public class Program : NUIApplication
    {
        Window window;
        Navigator navigator;
        AccountSignIn page;
        /// <summary>
        /// On create method
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            window = GetDefaultWindow();
            window.BackgroundColor = Color.Blue;
            window.KeyEvent += OnKeyEvent;

            navigator = window.GetDefaultNavigator();
            page = new AccountSignIn();
            navigator.Push(page);
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
        /// <param name="args"> arguments</param>
        static void Main(string[] args)
        {
            // Initiailize App
            var app = new Program();
            app.Run(args);
        }
    }
}
