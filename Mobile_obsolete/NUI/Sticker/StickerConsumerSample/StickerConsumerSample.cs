/*
 * Copyright (c) 2022 Samsung Electronics Co., Ltd. All rights reserved.
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

using System.Threading.Tasks;
using Tizen;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.Security;

/// <summary>
/// Namespace for Sticker consumer sample.
/// </summary>
namespace StickerConsumerSample
{
    /// <summary>
    /// Main class for Sticker consumer sample.
    /// </summary>
    class Program : NUIApplication
    {
        /// <summary>
        /// Tag used for Tizen.Log entries.
        /// </summary>
        internal static string logTag = "StickerConsumerSample";

        /// <summary>
        /// Navigator component which navigates pages.
        /// </summary>
        private Navigator navigator = null;

        /// <summary>
        /// Context for privacy privilege manager response.
        /// </summary>
        private static PrivacyPrivilegeManager.ResponseContext context = null;

        /// <summary>
        /// MediaStorage privilege.
        /// </summary>
        private const string privilegeMediaStorage = "http://tizen.org/privilege/mediastorage";

        /// <summary>
        /// OnCreate callback implementation.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();

            CheckPrivilege();
            Window window = GetDefaultWindow();
            navigator = new Navigator();
            navigator.WidthResizePolicy = ResizePolicyType.FillToParent;
            navigator.HeightResizePolicy = ResizePolicyType.FillToParent;
            window.Add(navigator);

            // Create the menu page.
            MenuPage menuPage = new MenuPage(navigator);
            navigator.Push(menuPage);
        }

        /// <summary>
        /// PPM request response callback.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PPM_RequestResponse(object sender, RequestResponseEventArgs e)
        {
            if (e.cause == CallCause.Answer)
            {
                if (e.result != RequestResult.AllowForever)
                {
                    // Posts the Toast message.
                    ToastMessage denyMessage = new ToastMessage
                    {
                        Message = "This application doesn't have permission to use the mediastorage privilege"
                    };
                    denyMessage.Post();
                    Exit();
                }
            }
            else
            {
                Log.Error(logTag, "Error occurred during requesting permission");
            }

            context.ResponseFetched -= PPM_RequestResponse;
        }

        /// <summary>
        /// Check privacy privilege and if need to ask for user, send request for PPM.
        /// </summary>
        private void CheckPrivilege()
        {
            PrivacyPrivilegeManager.GetResponseContext(privilegeMediaStorage).TryGetTarget(out context);
            if (context != null)
            {
                context.ResponseFetched += PPM_RequestResponse;
            }

            CheckResult result = PrivacyPrivilegeManager.CheckPermission(privilegeMediaStorage);
            switch (result)
            {
                case CheckResult.Allow:
                    // Privilege can be used
                    break;
                case CheckResult.Deny:
                    // Privilege can't be used
                    // Posts the Toast message.
                    ToastMessage denyMessage = new ToastMessage
                    {
                        Message = "This application doesn't have permission to use the mediastorage privilege"
                    };
                    denyMessage.Post();
                    Exit();
                    break;
                case CheckResult.Ask:
                    Log.Info(logTag, "Ask to user whether to grant permission to use the " + privilegeMediaStorage + " privilege");
                    // Request a permission
                    // Calling it brings up a system dialog that an app user can decide to approve or deny it.
                    PrivacyPrivilegeManager.RequestPermission(privilegeMediaStorage);
                    break;
                default:
                    Log.Error(logTag, "Unknown result for the " + privilegeMediaStorage + " privilege");
                    break;
            }
        }

        /// <summary>
        /// Main function.
        /// </summary>
        /// <param name="args">arguments of Main(entry point).</param>
        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
