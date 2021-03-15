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

namespace Accounts
{
    /// <summary>
    /// Represents Xamarin Forms of Tizen platform app.
    /// </summary>
    class Program : Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        /// <summary>
        /// On create method.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();

            LoadApplication(new App());
        }

        /// <summary>
        /// Main method of Accounts for Tizen Wearable.
        /// </summary>
        /// <param name="args"> Arguments. </param>
        static void Main(string[] args)
        {
            var app = new Program();

            Forms.Init(app);
            Tizen.Wearable.CircularUI.Forms.FormsCircularUI.Init();
            app.Run(args);
        }
    }
}
