/*
 * Copyright (c) 2022 Samsung Electronics Co., Ltd. All rights reserved.
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

using Tizen.NUI;

/// <summary>
/// Namespace for Sticker provider sample.
/// </summary>
namespace StickerProviderSample
{
    /// <summary>
    /// Main class for Sticker provider sample.
    /// </summary>
    class Program : NUIApplication
    {
        /// <summary>
        /// OnCreate callback implementation.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            // When End key, Back key input, this application is terminated.
            Window.Instance.KeyEvent += OnKeyEvent;
        }

        /// <summary>
        /// The method called when key pressed down.
        /// </summary>
        /// <param name="sender">Key instance</param>
        /// <param name="e">Key's args</param>
        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                // Terminate application.
                Exit();
            }
        }

        /// <summary>
        /// Main function.
        /// </summary>
        /// <param name="args">arguments of Main(entry point).</param>
        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
            app.Dispose();
        }
    }
}
