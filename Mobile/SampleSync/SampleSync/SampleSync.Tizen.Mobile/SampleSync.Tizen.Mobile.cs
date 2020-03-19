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

using Xamarin.Forms;
using SampleSync.Tizen.Port;
using SampleSync.Utils;

namespace SampleSync.Tizen.Mobile
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        /// <summary>
        /// An interface for the platform event.
        /// </summary>
        IPlatformEvent pEvent;

        /// <summary>
        /// This method is called when app is created.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            var app = new App();

            // To send event to UI
            pEvent = app;
            LoadApplication(app);
            SampleSyncPort.EventRegister(pEvent);
        }

        static void Main(string[] args)
        {
            var app = new Program();
            DependencyService.Register<SampleSyncPort>();
            Forms.Init(app);
            app.Run(args);
        }
    }
}
