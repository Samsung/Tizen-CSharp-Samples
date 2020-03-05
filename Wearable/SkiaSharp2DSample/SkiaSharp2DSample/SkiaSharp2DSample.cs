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

using System;
using Xamarin.Forms;

namespace SkiaSharp2DSample
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        /// <summary>
        /// Called when the application is launched.
        /// base.OnCreate() should be called to get "Created" event
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            // Initialize to use Tizen.Wearable.CircularUI.Forms APIs
            Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            app.Run(args);
        }
    }
}
