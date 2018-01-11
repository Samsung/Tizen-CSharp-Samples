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
using System;
using System.IO;
using System.Timers;
using Xamarin.Forms;

namespace RecorderSample
{
    /// <summary>
    /// Represents Recording ViewModel class.
    /// </summary>
    partial class RecordingViewModel : ViewModelBase
    {
        // The timer is to update elapsed time.
        private readonly Timer _timer = new Timer(500);

        // The watcher is to update recording file size.
        private readonly FileSystemWatcher _fileWatcher = new FileSystemWatcher();
        private readonly FileInfo _saveFileInfo;

        public RecordingViewModel(IRecorderController controller)
        {
            Controller = controller;

            Title = controller is IAudioRecorderController ? "AudioRecorder" : "VideoRecorder";

            _saveFileInfo = new FileInfo(Controller.SavePath);

            if (_saveFileInfo.Exists)
            {
                _saveFileInfo.Delete();
            }

            // Initialize file watcher.
            _fileWatcher.Path = Path.GetDirectoryName(Controller.SavePath);
            _fileWatcher.Filter = Path.GetFileName(Controller.SavePath);
            _fileWatcher.Changed += (s, e) => OnPropertyChanged(nameof(Size));
            _fileWatcher.Deleted += (s, e) => OnPropertyChanged(nameof(Size));

            MediaPlayer.Stopped += OnPlayerStopped;

            _timer.Elapsed += (s, e) => OnPropertyChanged(nameof(PlayText));

            InitializeCommands();
        }

        private void OnPlayerStopped(object sender, EventArgs args)
        {
            _timer.Stop();

            UpdatePage();
        }

        /// <summary>
        /// Updates the page.
        /// </summary>
        private void UpdatePage()
        {
            OnPropertyChanged(nameof(PlayText));
            OnPropertyChanged(nameof(PauseText));
            OnPropertyChanged(nameof(IsPlaying));
            OnPropertyChanged(nameof(State));

            PauseCommand.ChangeCanExecute();
            StartCommand.ChangeCanExecute();
            PlayCommand.ChangeCanExecute();
        }

        /// <summary>
        /// Gets the title of the page.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the controller for recorder.
        /// </summary>
        public IRecorderController Controller { get; }


        /// <summary>
        /// Gets the controller for media player.
        /// </summary>
        protected IMediaPlayer MediaPlayer => DependencyService.Get<IMediaPlayer>();

        /// <summary>
        /// Gets the display text of the prepare button.
        /// </summary>
        public string PrepareText => State == RecorderState.Idle ? "Prepare" : "Unprepare";

        /// <summary>
        /// Gets the display text of the start button.
        /// </summary>
        public string StartText => State == RecorderState.Ready ? "Start" : "Stop";

        /// <summary>
        /// Gets the display text of the pause button.
        /// </summary>
        public string PauseText => State == RecorderState.Paused ? "Resume" : "Pause";

        /// <summary>
        /// Gets the display text of the play button.
        /// </summary>
        public string PlayText
        {
            get
            {
                if (MediaPlayer.IsPlaying == false)
                {
                    return "Play";
                }

                return $"Stop ({TimeSpan.FromSeconds(MediaPlayer.Position / 1000).ToString(@"mm\:ss")} / " +
                    TimeSpan.FromSeconds(MediaPlayer.Duration / 1000).ToString(@"mm\:ss") + ")";
            }
        }

        /// <summary>
        /// Gets the display text of the current file size.
        /// </summary>
        public string Size
        {
            get
            {
                _saveFileInfo.Refresh();
                if (!_saveFileInfo.Exists)
                {
                    return null;
                }

                return $"{_saveFileInfo.Length} Bytes";
            }
        }

        /// <summary>
        /// Gets the state of the recorder.
        /// </summary>
        public RecorderState State => Controller.State;

        /// <summary>
        /// Gets the value indicating whether the media player is playing.
        /// </summary>
        public bool IsPlaying => MediaPlayer.IsPlaying;

        internal void OnDisappearing()
        {
            MediaPlayer.Stopped -= OnPlayerStopped;

            Controller.Unprepare();
        }

        public object CameraView
        {
            set
        {
                if (value != null)
            {
                    (Controller as IVideoRecorderController)?.SetDisplay(value);
                }
            }
        }

        public object PlayerView
        {
            set
            {
                if (value != null)
        {
                    MediaPlayer.SetDisplay(value);
                }
            }
        }
    }
}
