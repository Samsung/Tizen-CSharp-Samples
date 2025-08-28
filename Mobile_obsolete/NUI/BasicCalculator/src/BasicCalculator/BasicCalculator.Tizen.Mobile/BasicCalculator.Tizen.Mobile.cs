/*
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

using BasicCalculator.Tizen.Mobile.Views;
using BasicCalculator.ViewModels;
using Tizen.NUI;
using Tizen.NUI.Binding;

namespace BasicCalculator.Tizen.Mobile
{
    internal class Program : NUIApplication
    {
        #region methods

        private readonly string[] _keyList = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

        /// <summary>
        /// Loads NUI application.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();

            Window window = Window.Instance;
            window.BackgroundColor = Color.Cyan;
            window.KeyEvent += OnKeyEvent;

            MobileMainView page = new MobileMainView();
            page.PositionUsesPivotPoint = true;
            page.ParentOrigin = ParentOrigin.Center;
            page.PivotPoint = PivotPoint.Center;
            page.Size = new Size(window.WindowSize);
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
        /// Main application entry point.
        /// Initializes Tizen extensions.
        /// </summary>
        /// <param name="args">Application arguments.</param>
        private static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }

        #endregion
    }
}
