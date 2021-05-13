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
using System.Diagnostics;
using System.Collections.Generic;
using ApplicationControl.Tizen.Mobile;
using ApplicationControl.Interfaces;
using Tizen.Applications;

[assembly: Xamarin.Forms.Dependency(typeof(AppOperations))]

namespace ApplicationControl.Tizen.Mobile
{
    /// <summary>
    /// To get operation related information
    /// </summary>
    class AppOperations : IAppControl, IApplicationInformation
    {
        Dictionary<string, AppControl> _launchedAppControl;

        public AppOperations()
        {
            Initialize();
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
        /// The view operation
        /// </summary>
        public string View
        {
            get
            {
                return AppControlOperations.View;
            }
        }

        /// <summary>
        /// The pick operation
        /// </summary>
        public string Pick
        {
            get
            {
                return AppControlOperations.Pick;
            }
        }

        /// <summary>
        /// The compose operation
        /// </summary>
        public string Compose
        {
            get
            {
                return AppControlOperations.Compose;
            }
        }

        /// <summary>
        /// The send operation
        /// </summary>
        public string Send
        {
            get
            {
                return AppControlOperations.Send;
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
        /// To initialize fields when a class is instanciated
        /// </summary>
        void Initialize()
        {
            _launchedAppControl = new Dictionary<string, AppControl> { };
        }

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
}