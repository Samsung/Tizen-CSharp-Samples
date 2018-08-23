//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

namespace MediaContent
{
    /// <summary>
    /// The MediaContent application main class
    /// </summary>
    class Program : Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        /// <summary>
        /// Gets a full path of the app resources.
        /// </summary>
        public static string AppResourcePath { get; private set; }

        /// <summary>
        /// Handle when your app creates.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();

            AppResourcePath = DirectoryInfo.Resource;

            LoadApplication(new App());
        }

        /// <summary>
        /// The main method of MediaContent app
        /// </summary>
        /// <param name="args">Arguments</param>
        public static void Main(string[] args)
        {
            var app = new Program();
            Xamarin.Forms.Platform.Tizen.Forms.Init(app);

            Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            app.Run(args);
        }
    }
}
