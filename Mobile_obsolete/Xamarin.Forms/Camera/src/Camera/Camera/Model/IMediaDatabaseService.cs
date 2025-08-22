/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
namespace Camera.Model
{

    /// <summary>
    /// Delegate for event ErrorOccurred.
    /// </summary>
    /// <param name="sender">Instance of the object which invokes event.</param>
    /// <param name="errorMessage">Message of an occurred error.</param>
    public delegate void ErrorOccurredDelegate(object sender, string errorMessage);

    /// <summary>
    /// Media Database service interface.
    /// </summary>
    public interface IMediaDatabaseService
    {
        #region properites

        /// <summary>
        /// Event invoked whenever media database error occurred.
        /// </summary>
        event ErrorOccurredDelegate ErrorOccurred;

        #endregion

        #region methods

        /// <summary>
        /// Connects to a media database.
        /// </summary>
        void ConnectDatabase();

        /// <summary>
        /// Disconnects from a media database.
        /// </summary>
        void DisconnectDatabase();

        /// <summary>
        /// Updates a media database.
        /// </summary>
        /// <param name="path">Path to a directory to update.</param>
        void UpdateDatabase(string path);

        #endregion
    }
}