/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
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

using Xamarin.Forms;
using System.Threading.Tasks;
using Tizen.Security;

namespace WebProxySample
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        public const string LOG_TAG = "WebProxySample";
        // Internet privilege
        public const string InternetPrivilege = "http://tizen.org/privilege/internet";

        protected override void OnCreate()
        {
            base.OnCreate();
            GetPermission();
            LoadApplication(new App());
        }

        async void GetPermission()
        {
            Tizen.Log.Info(LOG_TAG, " <<< Internet Privilege : " + PrivacyPrivilegeManager.CheckPermission(InternetPrivilege).ToString());
            bool internetGranted = false;
            if (PrivacyPrivilegeManager.CheckPermission(InternetPrivilege) != CheckResult.Allow)
            {
                internetGranted = await RequestPermission(InternetPrivilege);
            }
            else
            {
                internetGranted = true;
            }

            if (!internetGranted)
            {
                Tizen.Log.Error(LOG_TAG, "Failed to obtain user consent. So, app is going to be terminated.");
                // Terminate this application.
                Tizen.Applications.Application.Current.Exit();
                return;
            }
        }

        static Task<bool> RequestPermission(string privilege)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            var response = PrivacyPrivilegeManager.GetResponseContext(privilege);
            PrivacyPrivilegeManager.ResponseContext target;
            response.TryGetTarget(out target);
            target.ResponseFetched += (s, e) =>
            {
                if (e.cause == CallCause.Error)
                {
                    /// Handle errors
                    Tizen.Log.Error(LOG_TAG, "An error occurred while requesting an user permission");
                    tcs.SetResult(false);
                    return;
                }

                // Now, we can check if the permission is granted or not
                switch (e.result)
                {
                    case RequestResult.AllowForever:
                        // Permission is granted.
                        // We can do this permission-related task we want to do.
                        Tizen.Log.Info(LOG_TAG, "Response: RequestResult.AllowForever");
                        tcs.SetResult(true);
                        break;
                    case RequestResult.DenyForever:
                    case RequestResult.DenyOnce:
                        // Functionality that depends on this permission will not be available
                        Tizen.Log.Info(LOG_TAG, "Response: RequestResult." + e.result.ToString());
                        tcs.SetResult(false);
                        break;
                }

            };
            PrivacyPrivilegeManager.RequestPermission(privilege);

            return tcs.Task;
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            global::Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            app.Run(args);
        }
    }
}
