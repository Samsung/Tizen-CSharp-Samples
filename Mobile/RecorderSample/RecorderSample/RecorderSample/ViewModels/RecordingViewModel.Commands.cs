/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
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

using Xamarin.Forms;

namespace RecorderSample
{
    /// <summary>
    /// Represents Recording ViewModel class.
    /// </summary>
    partial class RecordingViewModel
    {
        // Commands for the buttons.
        public Command PrepareCommand { get; protected set; }
        public Command StartCommand { get; protected set; }
        public Command SaveCommand { get; protected set; }
        public Command PauseCommand { get; protected set; }
        public Command PlayCommand { get; protected set; }

        /// <summary>
        /// Initialize commands.
        /// </summary>
        public void InitializeCommands()
        {
            PrepareCommand = new Command(() =>
            {
                if (State == RecorderState.Idle)
                {
                    Controller.Prepare();
                }
                else
                {
                    Controller.Unprepare();
                }

                OnPropertyChanged(nameof(StartText));
                OnPropertyChanged(nameof(PrepareText));
                UpdatePage();
            });

            StartCommand = new Command(() =>
            {
                if (State == RecorderState.Ready)
                {
                    Controller.Start();

                    _fileWatcher.EnableRaisingEvents = true;
                }
                else
                {
                    // Commit to save the data
                    Controller.Commit();

                    _fileWatcher.EnableRaisingEvents = false;
                }

                OnPropertyChanged(nameof(StartText));
                UpdatePage();
            }, CanStart);

            PauseCommand = new Command(() =>
            {
                if (State == RecorderState.Paused)
                {
                    Controller.Resume();
                }
                else
                {
                    Controller.Pause();
                }

                OnPropertyChanged(nameof(State));
                OnPropertyChanged(nameof(PauseText));
            }, CanPause);

            PlayCommand = new Command(async () =>
            {
                if (MediaPlayer.IsPlaying == false)
                {
                    await MediaPlayer.Prepare(Controller.SavePath);
                    MediaPlayer.Play();

                    // Start the timer to display playing time.
                    _timer.Start();
                    UpdatePage();
                }
                else
                {
                    MediaPlayer.Stop();
                }
            }, CanPlay);
        }

        /// <summary>
        /// Gets the value indicating whether the start button be enabled.
        /// The button is available when the recorder is prepared and the media player is not playing.
        /// </summary>
        /// <returns>The value indicating whether the start button be enabled.</returns>
        private bool CanStart() => State != RecorderState.Idle && !MediaPlayer.IsPlaying;

        /// <summary>
        /// Gets the value indicating whether the resume button be enabled.
        /// The button is available when the recorder is recording or paused..
        /// </summary>
        /// <returns>The value indicating whether the resume button be enabled.</returns>
        private bool CanPause() => State == RecorderState.Recording || State == RecorderState.Paused;

        /// <summary>
        /// Gets the value indicating whether the play button be enabled.
        /// The button is available when the  file exists and the recorder is not started.
        /// </summary>
        /// <returns>The value indicating whether the play button be enabled.</returns>
        private bool CanPlay()
        {
            _saveFileInfo.Refresh();

            return _saveFileInfo.Exists && State != RecorderState.Recording &&
                State != RecorderState.Paused;
        }
    }
}
