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
using VoiceRecorder.ViewModel;
using Xamarin.Forms;

namespace VoiceRecorder.Utils
{
    /// <summary>
    /// RecordedFileListItem class.
    /// </summary>
    public class RecordedFileListItem : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Divider to turn seconds into minutes.
        /// </summary>
        private const int TO_MINUTES_DIVIDER = 60;

        /// <summary>
        /// Divider to turn tenths of a second into seconds.
        /// </summary>
        private const int TO_SECONDS_DIVIDER = 10;

        /// <summary>
        /// Private backing field for Duration property.
        /// </summary>
        private string _duration;

        /// <summary>
        /// Private backing field for Length property.
        /// </summary>
        private int _length;

        /// <summary>
        /// Private backing field for Path property.
        /// </summary>
        private string _path;

        #endregion

        #region properties

        /// <summary>
        /// Command which deletes the file.
        /// </summary>
        public Command DeleteItemCommand { get; set; }

        /// <summary>
        /// Command which goes to the player page.
        /// </summary>
        public Command GoToPlayerCommand { get; set; }

        /// <summary>
        /// Representation of the length of the recording in form minutes:seconds.
        /// </summary>
        public string Duration
        {
            get => _duration;
            set => SetProperty(ref _duration, value);
        }

        /// <summary>
        /// Length of the recording (in milliseconds.)
        /// </summary>
        public int Length
        {
            get => _length;
            set => SetProperty(ref _length, value);
        }

        /// <summary>
        /// Path of the recorded file.
        /// </summary>
        public string Path
        {
            get => _path;
            set => SetProperty(ref _path, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// RecordedFileListItem constructor.
        /// </summary>
        /// <param name="path">Path of the recorded file.</param>
        /// <param name="length">Length of the recording.</param>
        /// <param name="goToPlayerCommand">Command which goes to the player page.</param>
        /// <param name="deleteItemCommand">Command which deletes the file.</param>
        public RecordedFileListItem(string path, int length, Command goToPlayerCommand, Command deleteItemCommand)
        {
            Path = path;
            Length = length;
            Duration = ConvertMilisecondsToMinutes(length);
            GoToPlayerCommand = goToPlayerCommand;
            DeleteItemCommand = deleteItemCommand;
        }

        /// <summary>
        /// RecordedFileListItem constructor.
        /// </summary>
        /// <param name="length">Length of the recording.</param>
        /// <returns>Representation of the length of the recording in form minutes:seconds.</returns>
        private string ConvertMilisecondsToMinutes(int length)
        {
            return (length / TO_SECONDS_DIVIDER / TO_MINUTES_DIVIDER).ToString("00") + ":" +
                (length / TO_SECONDS_DIVIDER % TO_MINUTES_DIVIDER).ToString("00");
        }

        #endregion
    }
}
