/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tizen.Applications;
using ApplicationControl.Models;

namespace ApplicationControl.Helpers
{
    /// <summary>
    /// A singleton class to manipulate application operation
    /// </summary>
    public class ApplicationOperations
    {
        static ApplicationOperations _instance;
        Dictionary<string, AppControl> _launchedAppControl;

        ApplicationOperations()
        {
            _launchedAppControl = new Dictionary<string, AppControl>();
        }

        static public ApplicationOperations Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApplicationOperations();
                }

                return _instance;
            }
        }

        /// <summary>
        /// To get the selected operation id
        /// </summary>
        /// <param name="operation">selected operation name</param>
        /// <returns>selected operation id</returns>
        public IEnumerable<string> GetMatchedApplicationIds(string operation)
        {
            AppControl appControl = new AppControl
            {
                Operation = operation,
                LaunchMode = AppControlLaunchMode.Group
            };
            if (operation.Equals(AppControlOperations.View))
            {
                appControl.Uri = "http";
            }

            return AppControl.GetMatchedApplicationIds(appControl);
        }

        /// <summary>
        /// To send launch request
        /// </summary>
        /// <param name="operation">selected operation</param>
        /// <param name="id">selected operation app</param>
        /// <param name="data">a message to send</param>
        public void SendLaunchRequest(string operation, string id, Message data = null)
        {
            var appCon = new AppControl
            {
                Operation = operation,
                LaunchMode = AppControlLaunchMode.Group
            };

            if (data != null)
            {
                if (!String.IsNullOrEmpty(data.To))
                {
                    appCon.ExtraData.Add(AppControlData.To, data.To);
                }

                appCon.ExtraData.Add(AppControlData.Subject, data.Subject);
                appCon.ExtraData.Add(AppControlData.Text, data.Text);
            }

            if (!string.IsNullOrEmpty(id))
            {
                appCon.ApplicationId = id;
                SaveLaunchedAppControl(appCon);
            }

            try
            {
                AppControl.SendLaunchRequest(appCon);
            }
            catch
            {
                /// Error Handling Codes
            }
        }

        /// <summary>
        /// To send a terminate request
        /// </summary>
        /// <param name="id">a selected application ID</param>
        public void SendTerminateRequest(string id)
        {
            var appCon = GetLaunchedAppControl(id);
            if (appCon == null)
            {
                return;
            }

            try
            {
                AppControl.SendTerminateRequest(appCon);
                RemoveLaunchedAppControl(appCon.ApplicationId);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// To get the application icon path
        /// </summary>
        /// <param name="id">An application ID</param>
        /// <returns>The application icon path</returns>
        public string GetApplicationIconPath(string id)
        {
            var info = new ApplicationInfo(id);
            string iconPath = info.IconPath;
            info.Dispose();
            return iconPath;
        }

        /// <summary>
        /// The view operation
        /// </summary>
        public string View => AppControlOperations.View;

        /// <summary>
        /// The pick operation
        /// </summary>
        public string Pick => AppControlOperations.Pick;

        /// <summary>
        /// The compose operation
        /// </summary>
        public string Compose => AppControlOperations.Compose;

        /// <summary>
        /// The send operation
        /// </summary>
        public string Send => AppControlOperations.Send;

        /// <summary>
        /// To save a launched appcontrol that it will be used to send a kill request later
        /// </summary>
        /// <param name="appCon">An application control to save</param>
        void SaveLaunchedAppControl(AppControl appCon)
        {
            if (!_launchedAppControl.ContainsKey(appCon.ApplicationId))
            {
                _launchedAppControl.Add(appCon.ApplicationId, appCon);
            }
        }

        /// <summary>
        /// To get the application control by an application ID
        /// </summary>
        /// <param name="id">An application ID</param>
        /// <returns>An application control</returns>
        AppControl GetLaunchedAppControl(string id)
        {
            if (!_launchedAppControl.ContainsKey(id))
            {
                return null;
            }

            return _launchedAppControl[id];
        }

        /// <summary>
        /// To remove an application control by an application ID
        /// </summary>
        /// <param name="id">An application ID</param>
        void RemoveLaunchedAppControl(string id)
        {
            _launchedAppControl.Remove(id);
        }
    }

    /// <summary>
    /// A static class to manipulate the application control and the application information functionalities
    /// </summary>
    static public class ApplicationControlHelper
    {
        /// <summary>
        /// To get application IDs matched a specific appcontrol type
        /// </summary>
        /// <param name="type">an appcontrol type</param>
        /// <returns>application IDs</returns>
        public static IEnumerable<string> GetApplicationIdsForSpecificAppControlType(AppControlType type)
        {
            return ApplicationOperations.Instance.GetMatchedApplicationIds(IntToString(type));
        }

        /// <summary>
        /// To get the application icon path for an application
        /// </summary>
        /// <param name="id">an application ID</param>
        /// <returns>the application icon path</returns>
        public static string GetApplicationIconPath(string id)
        {
            return ApplicationOperations.Instance.GetApplicationIconPath(id);
        }

        /// <summary>
        /// To send a launch request
        /// </summary>
        /// <param name="type">an appcontrol type</param>
        /// <param name="id">an application ID</param>
        /// <param name="data">a message to pass with an appcontrol</param>
        public static void ExecuteApplication(AppControlType type, string id, Message data = null)
        {
            ApplicationOperations.Instance.SendLaunchRequest(IntToString(type), id, data);
        }

        /// <summary>
        /// To send a terminate request
        /// </summary>
        /// <param name="id">an application ID</param>
        public static void KillApplication(string id)
        {
            ApplicationOperations.Instance.SendTerminateRequest(id);
        }

        /// <summary>
        /// To change an enumeration to a matched string
        /// </summary>
        /// <param name="type">An enumeration</param>
        /// <returns>A matched string</returns>
        static string IntToString(AppControlType type)
        {
            var operation = ApplicationOperations.Instance;
            switch (type)
            {
                case AppControlType.View:
                    {
                        return operation.View;
                    }

                case AppControlType.Pick:
                    {
                        return operation.Pick;
                    }

                case AppControlType.Compose:
                    {
                        return operation.Compose;
                    }

                case AppControlType.Send:
                    {
                        return operation.Send;
                    }

                default:
                    {
                        return "Unknown";
                    }
            }
        }
    }
}
