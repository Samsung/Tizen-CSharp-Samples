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
using System.Runtime.CompilerServices;
using Tizen.Content.MediaContent;
using VoiceRecorder.Model;
using VoiceRecorder.Tizen.Mobile.Service;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(MediaDatabaseService))]

namespace VoiceRecorder.Tizen.Mobile.Service
{
    /// <summary>
    /// MediaDatabaseService class.
    /// Connects to, disconnects from and updates Media Database.
    /// Implements IMediaDatabaseService interface.
    /// </summary>
    public class MediaDatabaseService : IMediaDatabaseService
    {
        #region fields

        /// <summary>
        /// Path to recorded files.
        /// </summary>
        private const string PATH_TO_RECORDINGS = "/opt/usr/home/owner/media/Music/VoiceRecorder/";

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
        /// Connects to Media Database.
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
        /// Disconnects from Media Database.
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
        /// Updates Media Database.
        /// </summary>
        public void UpdateDatabase()
        {
            try
            {
                _mediaDatabase.ScanFolderAsync(PATH_TO_RECORDINGS);
            }
            catch (Exception exception)
            {
                ErrorHandler("Updating DB error: " + exception.Message);
            }
        }

        /// <summary>
        /// Invokes "ErrorOccurred" to other application's modules.
        /// </summary>
        /// <param name="errorMessage">Message of a thrown error.</param>
        private void ErrorHandler(string errorMessage, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            global::Tizen.Log.Error(Program.LogTag, errorMessage, file, func, line);
            ErrorOccurred?.Invoke(this, errorMessage);
        }

        #endregion
    }
}
