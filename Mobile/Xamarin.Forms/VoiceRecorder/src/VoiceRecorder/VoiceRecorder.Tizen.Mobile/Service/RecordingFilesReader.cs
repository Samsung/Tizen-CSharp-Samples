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
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using Tizen.Multimedia;
using VoiceRecorder.Model;
using VoiceRecorder.Tizen.Mobile.Service;
using VoiceRecorder.Utils;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(RecordingFilesReader))]

namespace VoiceRecorder.Tizen.Mobile.Service
{
    /// <summary>
    /// RecordingFilesReader class.
    /// Gets recorded files and allows to delete selected one.
    /// Implements IRecordingFilesReader interface.
    /// </summary>
    public class RecordingFilesReader : IRecordingFilesReader
    {
        /// <summary>
        /// Path to directory with recorded files.
        /// </summary>
        private const string PATH_TO_RECORDINGS_DIRECTORY = "/opt/usr/home/owner/media/Music/VoiceRecorder/";

        /// <summary>
        /// Divider to turn milliseconds into tenths of a second.
        /// </summary>
        private const int TO_TENTH_OF_SECONDS_DIVIDER = 100;

        /// <summary>
        /// Event invoked whenever any error occurred.
        /// </summary>
        public event ErrorOccurredDelegate ErrorOccurred;

        /// <summary>
        /// Event invoked when the selected file is deleted.
        /// </summary>
        public event EventHandler ItemDeleted;

        /// <summary>
        /// Deletes a file indicated by the given path.
        /// Invokes "ItemDeleted" to other application's modules.
        /// </summary>
        /// <param name="pathToRecording">Path to the file to delete.</param>
        public void DeleteItem(string pathToRecording)
        {
            try
            {
                File.Delete(pathToRecording);
                using (var databaseConnector = new DatabaseConnector())
                {
                    databaseConnector.ErrorOccurred += OnDatabaseError;
                    databaseConnector.UpdateDatabase();
                }
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
                return;
            }

            ItemDeleted?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Extracts the duration of the media file.
        /// The length is return in tenth of the second format.
        /// </summary>
        /// <param name="pathToRecording">Path to the file from which metadata have to be extracted.</param>
        /// <returns>The length of the selected file or 0 if error occurred.</returns>
        private int GetRecordingLengthMetadata(string pathToRecording)
        {
            try
            {
                using (MetadataExtractor metadataExtractor = new MetadataExtractor(pathToRecording))
                {
                    Metadata metadata = metadataExtractor.GetMetadata();
                    return (int)metadata.Duration / TO_TENTH_OF_SECONDS_DIVIDER;
                }
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
                return 0;
            }
        }

        /// <summary>
        /// Returns the collection of recorded files.
        /// </summary>
        /// <param name="goToPlayerCommand">Command which goes to the player page.</param>
        /// <param name="deleteItemCommand">Command which deletes the file.</param>
        /// <returns>
        /// Returns either the collection of recorded files with ascribed commands
        /// or null if there are not any files
        /// or null if an error occurred.
        /// </returns>
        public ObservableCollection<RecordedFileListItem> GetListOfFiles(
            Command goToPlayerCommand, Command deleteItemCommand)
        {
            ObservableCollection<RecordedFileListItem> listOfFiles = new ObservableCollection<RecordedFileListItem>();
            string[] listOfPaths;

            try
            {
                listOfPaths = Directory.GetFiles(PATH_TO_RECORDINGS_DIRECTORY);
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
                return null;
            }

            if (listOfPaths == null)
            {
                return null;
            }

            Array.Sort(listOfPaths, (x, y) => String.Compare(y, x));

            try
            {
                foreach (var path in listOfPaths)
                {
                    listOfFiles.Add(new RecordedFileListItem(
                        path, GetRecordingLengthMetadata(path), goToPlayerCommand, deleteItemCommand));
                }
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
                return null;
            }

            return listOfFiles;
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

        /// <summary>
        /// Handles "ErrorOccurred" when updating database.
        /// Throws an exception with error message.
        /// </summary>
        /// <param name="sender">Instance of the DatabaseConnector class.</param>
        /// <param name="errorMessage">Exception with message of a thrown error.</param>
        private void OnDatabaseError(object sender, string errorMessage)
        {
            throw new Exception(errorMessage);
        }
    }
}