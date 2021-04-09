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
using VoiceRecorder.Utils;
using Xamarin.Forms;

namespace VoiceRecorder.Model
{
    /// <summary>
    /// RecordingFilesReader service interface.
    /// </summary>
    public interface IRecordingFilesReader
    {
        #region properties

        /// <summary>
        /// Event invoked whenever file reading error occurred.
        /// </summary>
        event ErrorOccurredDelegate ErrorOccurred;

        /// <summary>
        /// Event invoked when the selected file is deleted.
        /// </summary>
        event EventHandler ItemDeleted;

        #endregion

        #region methods

        /// <summary>
        /// Deletes a file indicated by the given path.
        /// </summary>
        /// <param name="pathToRecording">Path to the file to delete.</param>
        void DeleteItem(string pathToRecording);

        /// <summary>
        /// Returns the collection of recorded files.
        /// </summary>
        /// <param name="goToPlayerCommand">Command which goes to the player page.</param>
        /// <param name="deleteItemCommand">Command which deletes the file.</param>
        /// <returns>The collection of recorded files with ascribed commands.</returns>
        ObservableCollection<RecordedFileListItem> GetListOfFiles(
            Command goToPlayerCommand, Command deleteItemCommand);

        #endregion
    }
}