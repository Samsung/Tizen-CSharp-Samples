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

using System.ComponentModel;
using System.IO;
using Xamarin.Forms;

namespace AudioIOSample
{
    /// <summary>
    /// Represents Main ViewModel class.
    /// </summary>
    class MainViewModel : INotifyPropertyChanged
    {
        // File watcher to update file size.
        private readonly FileSystemWatcher _fileWatcher = new FileSystemWatcher();
        private readonly FileInfo _saveFileInfo;

        public MainViewModel()
        {
            _saveFileInfo = new FileInfo(CaptureController.SavePath);

            // Initialize file watcher.
            _fileWatcher.Path = Path.GetDirectoryName(CaptureController.SavePath);
            _fileWatcher.Filter = Path.GetFileName(CaptureController.SavePath);

            _fileWatcher.Changed += (s, e) => OnPropertyChanged(nameof(Size));
            _fileWatcher.EnableRaisingEvents = true;

            CaptureController.StateChanged += (s, e) => CaptureState = e.State;
            PlaybackController.StateChanged += (s, e) => PlaybackState = e.State;

            CaptureCommand = new Command(() =>
                {
                    if (CaptureState == State.Idle)
                    {
                        CaptureController.Start();
                    }
                    else
                    {
                        CaptureController.Stop();
                    }
                }, () => PlaybackState == State.Idle);

            CapturePauseCommand = new Command(() =>
            {
                if (CaptureState == State.Running)
                {
                    CaptureController.Pause();
                }
                else
                {
                    CaptureController.Resume();
                }

                UpdatePage();
            }, () => CaptureState != State.Idle);

            PlaybackCommand = new Command(() =>
            {
                if (PlaybackState == State.Idle)
                {
                    PlaybackController.Start();
                }
                else
                {
                    PlaybackController.Stop();
                }

                UpdatePage();
            }, () => CaptureState == State.Idle && File.Exists(CaptureController.SavePath));

            PlaybackPauseCommand = new Command(() =>
            {
                if (PlaybackState == State.Running)
                {
                    PlaybackController.Pause();
                }
                else
                {
                    PlaybackController.Resume();
                }

                UpdatePage();
            }, () => PlaybackState != State.Idle);
        }

        // Commands for the buttons.
        public Command CaptureCommand { get; }

        public Command CapturePauseCommand { get; }

        public Command PlaybackCommand { get; }

        public Command PlaybackPauseCommand { get; }

        public ICaptureController CaptureController => DependencyService.Get<ICaptureController>();
        public IPlaybackController PlaybackController => DependencyService.Get<IPlaybackController>();

        public string CaptureText => CaptureState == State.Idle ? "Start" : "Stop";
        public string CapturePauseText => CaptureState == State.Paused ? "Paused" : "Resume";
        public string PlaybackText => PlaybackState == State.Idle ? "Start" : "Stop";
        public string PlaybackPauseText => PlaybackState == State.Paused ? "Resume" : "Paused";

        /// <summary>
        /// Gets the captured file size.
        /// </summary>
        public long Size
        {
            get
            {
                _saveFileInfo.Refresh();

                return _saveFileInfo.Exists ? _saveFileInfo.Length : 0;
            }
        }

        private State _captureState = State.Idle;
        public State CaptureState
        {
            get => _captureState;
            set
            {
                if (_captureState != value)
                {
                    _captureState = value;

                    OnPropertyChanged(nameof(CaptureState));
                    UpdatePage();
                }
            }
        }

        private State _playbackState = State.Idle;

        public State PlaybackState
        {
            get => _playbackState;
            set
            {
                if (_playbackState != value)
                {
                    _playbackState = value;

                    OnPropertyChanged(nameof(PlaybackState));
                    UpdatePage();
                }
            }
        }

        private void UpdatePage()
        {
            CaptureCommand.ChangeCanExecute();
            CapturePauseCommand.ChangeCanExecute();
            PlaybackCommand.ChangeCanExecute();
            PlaybackPauseCommand.ChangeCanExecute();

            OnPropertyChanged(nameof(CaptureText));
            OnPropertyChanged(nameof(CapturePauseText));
            OnPropertyChanged(nameof(PlaybackText));
            OnPropertyChanged(nameof(PlaybackPauseText));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies change of a property.
        /// </summary>
        /// <param name="propertyName">A property name.</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
