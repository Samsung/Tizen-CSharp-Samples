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

using Xamarin.Forms;
using ApplicationControl.Interfaces;
using System.Collections.Generic;

namespace ApplicationControl
{
    /// <summary>
    /// An enumeration for appcontrol type
    /// </summary>
    public enum AppControlType
    {
        View,
        Pick,
        Compose,
        Send,
        Unknown,
    }

    /// <summary>
    /// A singleton class to manipulate application operation
    /// </summary>
    public class ApplicationOperations
    {
        static ApplicationOperations _instance;

        ApplicationOperations()
        {
            Operation = DependencyService.Get<IAppControl>();
            Info = DependencyService.Get<IApplicationInformation>();
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

        public IAppControl Operation { get; private set; }
        public IApplicationInformation Info { get; private set; }
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
            return ApplicationOperations.Instance.Operation.GetMatchedApplicationIds(IntToString(type));
        }

        /// <summary>
        /// To get the application icon path for an application
        /// </summary>
        /// <param name="id">an application ID</param>
        /// <returns>the application icon path</returns>
        public static string GetApplicationIconPath(string id)
        {
            return ApplicationOperations.Instance.Info.GetApplicationIconPath(id);
        }

        /// <summary>
        /// To send a launch request
        /// </summary>
        /// <param name="type">an appcontrol type</param>
        /// <param name="id">an application ID</param>
        /// <param name="data">a message to pass with an appcontrol</param>
        public static void ExecuteApplication(AppControlType type, string id, Message data = null)
        {
            ApplicationOperations.Instance.Operation.SendLaunchRequest(IntToString(type), id, data);
        }

        /// <summary>
        /// To sen a terminate request
        /// </summary>
        /// <param name="id">an application ID</param>
        public static void KillApplication(string id)
        {
            ApplicationOperations.Instance.Operation.SendTerminateRequest(id);
        }

        /// <summary>
        /// To change an enumeration to a matched string
        /// </summary>
        /// <param name="type">An enumeration</param>
        /// <returns>A matched string</returns>
        static string IntToString(AppControlType type)
        {
            var operation = ApplicationOperations.Instance.Operation;
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