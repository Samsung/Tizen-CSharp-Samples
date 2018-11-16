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
using System;
using Camera.Model;
using Xamarin.Forms;

namespace Camera.Utils
{
    /// <summary>
    /// DatabaseConnector class.
    /// Controls database connection.
    /// </summary>
    public class DatabaseConnector : IDisposable
    {
        #region fields

        /// <summary>
        /// Instance of the MediaDatabaseService class.
        /// </summary>
        private IMediaDatabaseService _service;

        #endregion

        #region properties

        /// <summary>
        /// Event invoked whenever media database error occurred.
        /// </summary>
        public event ErrorOccurredDelegate ErrorOccurred;

        #endregion

        #region methods

        /// <summary>
        /// Calls service to connect to database.
        /// </summary>
        public DatabaseConnector()
        {
            _service = DependencyService.Get<IMediaDatabaseService>();
            _service.ErrorOccurred += ErrorOccurredEventHandler;
            _service.ConnectDatabase();
        }

        /// <summary>
        /// Updates a media database.
        /// </summary>
        /// <param name="path">Path to a directory to update.</param>
        public void UpdateDatabase(string path)
        {
            _service.UpdateDatabase(path);
        }

        /// <summary>
        /// Calls service to disconnect from database.
        /// </summary>
        public void Dispose()
        {
            _service.ErrorOccurred -= ErrorOccurredEventHandler;
            _service.DisconnectDatabase();
        }

        /// <summary>
        /// Handles "ErrorOccurred" of the IMediaDatabaseService object.
        /// Invokes "ErrorOccurred" event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="errorMessage">Error message.</param>
        private void ErrorOccurredEventHandler(object sender, string errorMessage)
        {
            ErrorOccurred?.Invoke(this, errorMessage);
        }

        #endregion
    }
}