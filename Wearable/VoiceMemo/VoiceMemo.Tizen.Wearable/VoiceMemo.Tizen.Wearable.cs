/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Xamarin.Forms;
using Tizen.Applications;
using Tizen.Wearable.CircularUI.Forms.Renderer;
using VoiceMemo.Tizen.Wearable.Services;

namespace VoiceMemo.Tizen.Wearable
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        App app;
        /// <summary>
        /// Called when this application is launched
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            app = new App();
            LoadApplication(app);
        }

        /// <summary>
        /// Called when this application is terminated
        /// </summary>
        protected override void OnTerminate()
        {
            Console.WriteLine("[Program.OnTerminate] LastUsedID " + AudioRecordService.numbering);
            // Save the index of recording files
            Preference.Set(DeviceInformation.LastUsedID, AudioRecordService.numbering);
            base.OnTerminate();
            app.Terminate();
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            // It's mandatory to initialize Circular UI for using Tizen Wearable Circular UI API
            FormsCircularUI.Init();
            app.Run(args);
        }
    }
}
