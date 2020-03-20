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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlayerSample
{
    /// <summary>
    /// ViewModel for PlayPage.
    /// </summary>
    partial class PlayPageViewModel : ViewModelBase
    {
        // Timer is used to update the position text.
        private readonly System.Timers.Timer _timer = new System.Timers.Timer(500);
        private int _seekUnit;

        public PlayPageViewModel(string uri)
        {
            SourceText = uri;

            MediaPlayer.SetSource(uri);

            _timer.Elapsed += (s, e) => OnPropertyChanged(nameof(PositionText));

            PlayerState = MediaPlayer.State;

            MediaPlayer.Buffering += OnBuffering;
            MediaPlayer.ErrorOccurred += OnErrorOccurred;
            MediaPlayer.SubtitleUpdated += OnSubtitleUpdated;
            MediaPlayer.StateChanged += OnStateChanged;

            InitializeCommands();
        }

        private void OnStateChanged(object sender, EventArgs e)
        {
            PlayerState = MediaPlayer.State;

            switch (PlayerState)
            {
                case MediaPlayerState.Playing:
                    _timer.Start();
                    break;

                case MediaPlayerState.Paused:
                    _timer.Stop();
                    break;

                case MediaPlayerState.Ready:
                    SubtitleText = null;
                    break;
            }

            OnPropertyChanged(nameof(PositionText));
            OnPropertyChanged(nameof(IsStarted));
            OnPropertyChanged(nameof(IsReady));
            OnPropertyChanged(nameof(PlayText));
            OnPropertyChanged(nameof(PauseText));
        }

        private void OnBuffering(object sender, BufferingEventArgs e)
        {
            if (e.Percent < 100)
            {
                BufferingText = $"Buffering : {e.Percent}%";
            }
            else
            {
                BufferingText = null;
            }

            OnPropertyChanged(nameof(BufferingText));
        }

        private void OnErrorOccurred(object sender, ErrorEventArgs e)
        {
            ErrorText = e.Message;
        }

        private CancellationTokenSource _subtitleDelayCancelSource = new CancellationTokenSource();
        private int _subtitleRemainingTime;

        private async Task ClearSubtitleAfter(int delay)
        {
            _subtitleDelayCancelSource = new CancellationTokenSource();

            var displayedTime = DateTime.Now;

            try
            {
                await Task.Delay(delay, _subtitleDelayCancelSource.Token);
                SubtitleText = null;
            }
            catch (TaskCanceledException)
            {
                _subtitleRemainingTime = delay - (DateTime.Now - displayedTime).Milliseconds;
            }
        }

        private async void OnSubtitleUpdated(object sender, SubtitleUpdatedEventArgs e)
        {
            _subtitleDelayCancelSource.Cancel();
            _subtitleRemainingTime = 0;

            SubtitleText = e.Subtitle;
            await ClearSubtitleAfter((int)e.Duration);
        }

        private async Task ResumeSubtitleAsync()
        {
            if (_subtitleRemainingTime > 0 && SubtitleText != null)
            {
                await ClearSubtitleAfter(_subtitleRemainingTime);
            }
        }

        public object PlayerView
        {
            set
            {
                if (value != null)
                {
                    MediaPlayer?.SetDisplay(value);
                }
            }
        }

        private IEnumerable<Property> _properties;

        public IEnumerable<Property> Properties
        {
            get => _properties;
            protected set
            {
                if (_properties != value)
                {
                    _properties = value;

                    OnPropertyChanged(nameof(Properties));
                }
            }
        }

        protected IMediaPlayer MediaPlayer => DependencyService.Get<IMediaPlayer>();

        private MediaPlayerState _playerState;

        public MediaPlayerState PlayerState
        {
            get => _playerState;
            set
            {
                if (_playerState != value)
                {
                    _playerState = value;

                    OnPropertyChanged(nameof(PlayerState));
                }
            }
        }

        public bool IsStarted => PlayerState == MediaPlayerState.Playing ||
            PlayerState == MediaPlayerState.Paused;

        public bool IsReady => IsStarted || PlayerState == MediaPlayerState.Ready;

        private bool _isSeekable;
        public bool IsSeekable
        {
            get => _isSeekable;
            protected set
            {
                if (value != _isSeekable)
                {
                    _isSeekable = value;

                    OnPropertyChanged(nameof(IsSeekable));
                }
            }
        }

        public string SourceText { get; protected set; }

        public string PlayText => PlayerState == MediaPlayerState.Ready ? "Play" : "Stop";

        public string PauseText => PlayerState == MediaPlayerState.Paused ? "Resume" : "Pause";

        private string _errorText;

        public string ErrorText
        {
            get => _errorText;
            protected set
            {
                if (_errorText != value)
                {
                    _errorText = value;

                    OnPropertyChanged(nameof(ErrorText));
                }
            }
        }

        private string _subtitleText;

        public string SubtitleText
        {
            get => _subtitleText;
            protected set
            {
                if (_subtitleText != value)
                {
                    _subtitleText = value;

                    OnPropertyChanged(nameof(SubtitleText));
                }
            }
        }

        public string BufferingText { get; protected set; }

        public string PositionText
        {
            get
            {
                if (PlayerState == MediaPlayerState.Idle || PlayerState == MediaPlayerState.Preparing)
                {
                    return "";
                }

                return $"{TimeSpan.FromSeconds(MediaPlayer.Position / 1000).ToString(@"mm\:ss")}/" +
                      TimeSpan.FromSeconds(MediaPlayer.Duration / 1000).ToString(@"mm\:ss");
            }
        }

        private async Task Seek(int offset)
        {
            IsSeekable = false;
            await MediaPlayer.SeekAsync(offset);

            IsSeekable = true;
            OnPropertyChanged(nameof(PositionText));
        }

        public override void OnPopped()
        {
            base.OnPopped();

            MediaPlayer.Unprepare();
            MediaPlayer.SetSubtile(null);

            _timer.Stop();

            MediaPlayer.Buffering -= OnBuffering;
            MediaPlayer.ErrorOccurred -= OnErrorOccurred;
            MediaPlayer.SubtitleUpdated -= OnSubtitleUpdated;
            MediaPlayer.StateChanged -= OnStateChanged;
        }
    }
}
