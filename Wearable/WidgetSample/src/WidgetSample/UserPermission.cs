using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tizen;
using Tizen.Security;

namespace WidgetSample
{
    public class UserPermission
    {
        private TaskCompletionSource<bool> tcs;
        
        public UserPermission()
        {
        }

        public async Task<bool> CheckAndRequestPermission(string privilege)
        {
            try
            {
                CheckResult result = PrivacyPrivilegeManager.CheckPermission(privilege);
                Log.Debug(Utility.LOG_TAG, "State: " + result.ToString());
                switch (result)
                {
                    case CheckResult.Allow:
                        return true;
                    case CheckResult.Deny:
                        Log.Debug(Utility.LOG_TAG, "In this case, health data is not available until the user changes the permission state from the Settings application.");
                        return false;
                    case CheckResult.Ask:
                        // Request permission to User
                        PrivacyPrivilegeManager.ResponseContext context;
                        PrivacyPrivilegeManager.GetResponseContext(privilege).TryGetTarget(out context);
                        if (context == null)
                        {
                            return false;
                        }
                        tcs = new TaskCompletionSource<bool>();
                        context.ResponseFetched += PPMResponseHandler;
                        PrivacyPrivilegeManager.RequestPermission(privilege);
                        return await tcs.Task;
                    default:
                        return false;
                }
            }
            catch (Exception e)
            {
                // Handle exception
                Log.Error(Utility.LOG_TAG, "An error occurred. : " + e.Message);
                return false;
            }
        }

        public void PPMResponseHandler(object sender, RequestResponseEventArgs e)
        {
            if (e.cause == CallCause.Error)
            {
                /// Handle errors
                Log.Error(Utility.LOG_TAG, "Error in Request Permission");
                tcs.SetResult(false);
                return;
            }

            switch (e.result)
            {
                case RequestResult.AllowForever:
                    /* Update UI and start accessing protected functionality */
                    Log.Debug(Utility.LOG_TAG, "Response: RequestResult.AllowForever");
                    tcs.SetResult(true);
                    break;
                case RequestResult.DenyForever:
                case RequestResult.DenyOnce:
                    /* Show a message and terminate the application */
                    Log.Debug(Utility.LOG_TAG, "Response: RequestResult." + e.result.ToString());
                    tcs.SetResult(false);
                    break;
            }
        }
    }
}
