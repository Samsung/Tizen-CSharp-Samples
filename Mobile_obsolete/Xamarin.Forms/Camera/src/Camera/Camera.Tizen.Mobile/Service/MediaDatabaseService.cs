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
using Camera.Tizen.Mobile.Service;
using Tizen.Content.MediaContent;
using Xamarin.Forms;

[assembly: Dependency(typeof(MediaDatabaseService))]

namespace Camera.Tizen.Mobile.Service
{
    public class MediaDatabaseService : IMediaDatabaseService
    {
        #region fields

        /// <summary>
        /// MediaDatabase class instance.
        /// </summary>
        MediaDatabase _mediaDatabase;

        #endregion

        #region properties

        /// <summary>
        /// Event invoked whenever media database error occurred.
        /// </summary>
        public event ErrorOccurredDelegate ErrorOccurred;

        #endregion

        #region methods

        /// <summary>
        /// Connects to media database.
        /// </summary>
        public void ConnectDatabase()
        {
            _mediaDatabase = new MediaDatabase();

            try
            {
                _mediaDatabase.Connect();
            }
            catch (Exception exception)
            {
                ErrorHandler("Connecting DB error: " + exception.Message);
            }
        }

        /// <summary>
        /// Disconnects from media database.
        /// </summary>
        public void DisconnectDatabase()
        {
            try
            {
                _mediaDatabase.Disconnect();
                _mediaDatabase.Dispose();
            }
            catch (Exception exception)
            {
                ErrorHandler("Disconnecting DB error: " + exception.Message);
            }
        }

        /// <summary>
        /// Updates a media database.
        /// </summary>
        /// <param name="path">Path to a directory to update.</param>
        public void UpdateDatabase(string path)
        {
            try
            {
                _mediaDatabase.ScanFolderAsync(path);
            }
            catch (Exception exception)
            {
                ErrorHandler("Updating DB error: " + exception.Message);
            }
        }

        /// <summary>
        /// Invokes "ErrorOccurred" event.
        /// </summary>
        /// <param name="errorMessage">Message of a thrown error.</param>
        private void ErrorHandler(string errorMessage)
        {
            ErrorOccurred?.Invoke(this, errorMessage);
        }

        #endregion
    }
}