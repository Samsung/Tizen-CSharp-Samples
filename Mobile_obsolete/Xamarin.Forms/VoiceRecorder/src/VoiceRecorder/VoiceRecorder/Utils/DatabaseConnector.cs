/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using VoiceRecorder.Model;
using Xamarin.Forms;

namespace VoiceRecorder.Utils
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
        /// Calls service to update database.
        /// </summary>
        public void UpdateDatabase()
        {
            _service.UpdateDatabase();
        }

        /// <summary>
        /// Disposes unmanaged resources.
        /// Calls service to disconnect from database.
        /// </summary>
        public void Dispose()
        {
            _service.ErrorOccurred -= ErrorOccurredEventHandler;
            _service.DisconnectDatabase();
        }

        /// <summary>
        /// Handles "ErrorOccurred" of the IMediaDatabaseService object.
        /// Invokes "ErrorOccurred" to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the IMediaDatabaseService class.</param>
        /// <param name="errorMessage">Error message.</param>
        private void ErrorOccurredEventHandler(object sender, string errorMessage)
        {
            ErrorOccurred?.Invoke(this, errorMessage);
        }

        #endregion
    }
}