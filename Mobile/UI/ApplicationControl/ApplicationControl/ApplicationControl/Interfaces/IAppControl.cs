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

using System.Collections.Generic;

namespace ApplicationControl.Interfaces
{
    /// <summary>
    /// To get Appcontrol operation related information
    /// </summary>
    public interface IAppControl
    {
        /// <summary>
        /// To get the selected operation id
        /// </summary>
        /// <param name="operation">selected operation name</param>
        /// <returns>selected operation id</returns>
        IEnumerable<string> GetMatchedApplicationIds(string operation);

        /// <summary>
        /// To send launch request
        /// </summary>
        /// <param name="operation">selected operation</param>
        /// <param name="id">selected application ID</param>
        /// <param name="data">a message to send</param>
        void SendLaunchRequest(string operation, string id, Message data);

        /// <summary>
        /// To send a terminate request
        /// </summary>
        /// <param name="id">a selected application ID</param>
        void SendTerminateRequest(string id);

        /// <summary>
        /// The pick operation
        /// </summary>
        string Pick { get; }

        /// <summary>
        /// The view operation
        /// </summary>
        string View { get; }

        /// <summary>
        /// The compose operation
        /// </summary>
        string Compose { get; }

        /// <summary>
        /// The send operation
        /// </summary>
        string Send { get; }
    }
}