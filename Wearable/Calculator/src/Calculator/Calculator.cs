
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
using Xamarin.Forms;

namespace Calculator
{
    /// <summary>
    /// Calculator application entry point.
    /// </summary>
    class Program : Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        /// <summary>
        /// An AppResourcePath which is used to get full path of the app resources.
        /// </summary>
        public static string AppResourcePath
        {
            get;
            private set;
        }

        /// <summary>
        /// This function is called when the application is created
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();

            AppResourcePath = DirectoryInfo.Resource;

            // Load the application
            LoadApplication(new CalculatorApp());
        }

        /// <summary>
        /// The entry point of the application
        /// </summary>
        /// <param name="args">Arguments</param>
        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            app.Run(args);
        }
    }
}
