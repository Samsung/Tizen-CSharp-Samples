/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
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

namespace Clock.Interfaces
{
    /// <summary>
    /// The enumeration of AppControl operation type
    /// </summary>
    public enum AppControlOperation
    {
        /// <summary>
        /// Identifier for the default operation
        /// </summary>
        DEFAULT,
        /// <summary>
        /// Identifier for the pick operation to provide a selection for requested items and return what is selected
        /// </summary>
        PICK,
    }

    /// <summary>
    /// The enumeration of AppControl launch mode
    /// </summary>
    public enum AppControlLaunchType
    {
        /// <summary>
        /// Identifier for launching an application in a new group
        /// </summary>
        SINGLE,
        /// <summary>
        /// Identifier for launching an application as a sub application which belongs to the same group
        /// </summary>
        GROUP,
    }

    /// <summary>
    /// The IAppControl interfaces
    /// </summary>
    public interface IAppControl
    {
        /// <summary>
        /// Request to launch an application
        /// </summary>
        /// <param name="appId">application's ID to launch</param>
        /// <seealso cref="string">
        /// <param name="op">AppControl operation type</param>
        /// <seealso cref="AppControlOperation">
        /// <param name="type">The launching mode to launch an application</param>
        /// <seealso cref="AppControlLaunchType">
        void ApplicationLaunchRequest(string appId, AppControlOperation op, AppControlLaunchType type);
    }
}
