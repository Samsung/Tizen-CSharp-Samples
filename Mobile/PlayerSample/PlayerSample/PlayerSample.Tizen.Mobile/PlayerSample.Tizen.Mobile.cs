/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace PlayerSample.Tizen.Mobile
{
    class Program : Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        protected override void OnPreCreate() => MainWindow = new ElmSharp.Window("PlayerSample");

        static void Main(string[] args)
        {
            var app = new Program();

            global::Xamarin.Forms.DependencyService.Register<SecurityPort>();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            VideoViewController.MainWindowProvider = () => app.MainWindow;
            app.Run(args);
        }
    }
}
