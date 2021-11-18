/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
using ImageGallery.Models;
using Tizen.Security;

namespace ImageGallery.Tizen.Mobile
{
    /// <summary>
    /// ImageGallery forms application class for Tizen mobile profile.
    /// </summary>
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        #region fields

        /// <summary>
        /// Media storage privilege key.
        /// </summary>
        private const string MEDIASTORAGE_PRIVILEGE = "http://tizen.org/privilege/mediastorage";

        #endregion

        #region methods

        /// <summary>
        /// Initializes Xamarin.Forms application.
        /// Checks for media storage privilege.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            MediastoragePrivilegeCheck();
        }

        /// <summary>
        /// Starts Xamarin.Forms application.
        /// </summary>
        private void StartApplication()
        {
            LoadApplication(new App());
        }

        /// <summary>
        /// Application entry point.
        /// Initializes Xamarin.Forms application.
        /// </summary>
        /// <param name="args">Launch arguments.</param>
        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            app.Run(args);
        }

        /// <summary>
        /// Handles privilege request response.
        /// Starts application when privilege check is completed.
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
                        PrivilegeState.MediastoragePermissionGranted = true;
                        break;
                    case RequestResult.DenyForever:
                        PrivilegeState.MediastoragePermissionGranted = false;
                        break;
                    case RequestResult.DenyOnce:
                        PrivilegeState.MediastoragePermissionGranted = false;
                        break;
                };
            }

            StartApplication();
        }

        /// <summary>
        /// Checks media storage privilege. Request media storage privilege if not set.
        /// Starts application when privilege check is completed.
        /// </summary>
        private void MediastoragePrivilegeCheck()
        {
            CheckResult result = PrivacyPrivilegeManager.CheckPermission(MEDIASTORAGE_PRIVILEGE);

            switch (result)
            {
                case CheckResult.Allow:
                    PrivilegeState.MediastoragePermissionGranted = true;
                    StartApplication();
                    break;
                case CheckResult.Deny:
                    PrivilegeState.MediastoragePermissionGranted = false;
                    break;
                case CheckResult.Ask:
                    PrivacyPrivilegeManager.GetResponseContext(MEDIASTORAGE_PRIVILEGE)
                        .TryGetTarget(out PrivacyPrivilegeManager.ResponseContext context);

                    if (context != null)
                    {
                        context.ResponseFetched += PPM_RequestResponse;
                    }

                    PrivacyPrivilegeManager.RequestPermission(MEDIASTORAGE_PRIVILEGE);
                    PrivacyPrivilegeManager.GetResponseContext(MEDIASTORAGE_PRIVILEGE).TryGetTarget(out context);

                    break;
            }
        }

        #endregion
    }
}
