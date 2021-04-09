/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

using Xamarin;
using Xamarin.Forms;

namespace Maps.Tizen.TV
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        #region methods

        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        /// <summary>
        /// Application entry point.
        /// Initializes map if configured.
        /// Initializes Xamarin Forms application.
        /// </summary>
        /// <param name="args">Command line arguments. Not used.</param>
        private static void Main(string[] args)
        {
            var app = new Program();

            if (Config.IsMapProviderConfigured())
            {
                InitializeMap();
            }

            Forms.Init(app);
            app.Run(args);
        }

        /// <summary>
        /// Initializes map library.
        /// </summary>
        private static void InitializeMap()
        {
            FormsMaps.Init(Config.ProviderName, Config.AuthenticationToken);
            ViewModels.ViewModelLocator.ViewModel.IsMapInitialized = true;
        }

        #endregion
    }
}