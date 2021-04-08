/*
 *  Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 *  Contact: Ernest Borowski <e.borowski@partner.samsung.com>
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License
 *
 *
 * @file        Certificates.cs
 * @author      Ernest Borowski (e.borowski@partner.samsung.com)
 * @version     1.0
 * @brief       This file contains Program class with Main method.
 */
using Xamarin.Forms;

namespace SecureRepository.Tizen
{
    /// <summary>
    /// Program class contains Main method.
    /// </summary>
    public class Program : Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        public static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            app.Run(args);
        }

        /// <summary>
        /// Handles Application create state.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            this.LoadApplication(new App());
        }
    }
}
