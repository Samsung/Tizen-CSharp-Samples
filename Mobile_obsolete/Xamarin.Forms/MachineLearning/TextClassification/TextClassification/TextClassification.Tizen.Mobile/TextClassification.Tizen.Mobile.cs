/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd. All rights reserved.
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
namespace TextClassification.Tizen.Mobile
{
    /// <summary>
    /// Represents xamarin forms of tizen platform app
    /// </summary>
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        /// <summary>
        /// On create method
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        /// <summary>
        /// Main method of text classification tizen mobile
        /// </summary>
        /// <param name="args"> Command line arguments. Not used. </param>
        static void Main(string[] args)
        {
            var app = new Program();

            /// Initiailize App
            global::Xamarin.Forms.Forms.Init(app);

            app.Run(args);
        }
    }
}
