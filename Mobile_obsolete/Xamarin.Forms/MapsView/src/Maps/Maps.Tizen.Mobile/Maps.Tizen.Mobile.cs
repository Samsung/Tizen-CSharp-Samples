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

using Tizen.Security;

using Xamarin.Forms;

namespace Maps.Tizen.Mobile
{
    internal class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        #region methods

        /// <summary>
        /// Initializes Xamarin Forms application.
        /// Checks for location privilege.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            LocationPrivilegeCheck();
        }

        /// <summary>
        /// Starts Xamarin Forms application.
        /// </summary>
        private void StartApplication()
        {
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
        /// Handles privilege request response.
        /// Starts application when privilege check is complete.
        /// </summary>
        /// <param name="sender">Event sender. Not used.</param>
        /// <param name="e">Event arguments.</param>
        private void PPM_RequestResponse(object sender, RequestResponseEventArgs e)
        {
            if (e.cause == CallCause.Answer)
            {
                switch (e.result)
                {
                    case RequestResult.AllowForever:
                        ViewModels.ViewModelLocator.ViewModel.IsGPSAvailable = true;
                        break;
                    case RequestResult.DenyForever:
                        ViewModels.ViewModelLocator.ViewModel.IsGPSAvailable = false;
                        break;
                    case RequestResult.DenyOnce:
                        ViewModels.ViewModelLocator.ViewModel.IsGPSAvailable = false;
                        break;
                };

                StartApplication();
            }
        }

        /// <summary>
        /// Checks location privilege. Request location privilege if not set.
        /// Starts application when privilege check is complete.
        /// </summary>
        private void LocationPrivilegeCheck()
        {
            CheckResult result = PrivacyPrivilegeManager.CheckPermission(Config.LocationPrivilege);

            switch (result)
            {
                case CheckResult.Allow:
                    ViewModels.ViewModelLocator.ViewModel.IsGPSAvailable = true;
                    StartApplication();

                    break;
                case CheckResult.Deny:
                    ViewModels.ViewModelLocator.ViewModel.IsGPSAvailable = false;
                    StartApplication();

                    break;
                case CheckResult.Ask:
                    PrivacyPrivilegeManager.GetResponseContext(Config.LocationPrivilege)
                        .TryGetTarget(out PrivacyPrivilegeManager.ResponseContext context);

                    if (context != null)
                    {
                        context.ResponseFetched += PPM_RequestResponse;
                    }

                    PrivacyPrivilegeManager.RequestPermission(Config.LocationPrivilege);
                    break;
            }
        }

        /// <summary>
        /// Initializes map library.
        /// </summary>
        private static void InitializeMap()
        {
            ViewModels.ViewModelLocator.ViewModel.IsMapInitialized = true;
        }

        #endregion
    }
}