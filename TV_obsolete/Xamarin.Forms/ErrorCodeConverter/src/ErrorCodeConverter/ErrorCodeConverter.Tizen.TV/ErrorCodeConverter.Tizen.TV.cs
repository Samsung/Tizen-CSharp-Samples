/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Xamarin.Forms;

namespace ErrorCodeConverter.Tizen.TV
{
    /// <summary>
    /// Main application class.
    /// Provides application's entry point.
    /// </summary>
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        #region methods

        /// <summary>
        /// Loads Xamarin Forms application.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        /// <summary>
        /// Application entry point.
        /// Initializes Xamarin Forms framework.
        /// </summary>
        /// <param name="args">Command line parameters.</param>
        static void Main(string[] args)
        {
            var app = new Program();

            Forms.Init(app);
            app.Run(args);
        }

        #endregion
    }
}
