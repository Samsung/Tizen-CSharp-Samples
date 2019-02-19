//Copyright 2019 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System.Windows.Input;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Tizen.Wearable.CircularUI.Forms;

namespace PlayerSample.ViewModels
{
    /// <summary>
    /// ViewModel class for the Main Page
    /// </summary>
    class PlayPageViewModel : ViewModelBase
    {
        public ICommand PlayCommand { get; protected set; }
        public ICommand StopCommand { get; protected set; }
        public ICommand SeekBackwardCommand { get; protected set; }
        public ICommand SeekForwardCommand { get; protected set; }
        public ICommand StreamInfoCommand { get; protected set; }
        public ICommand ContentInfoCommand { get; protected set; }
        public ICommand MuteCommand { get; protected set; }
        public ICommand LoopCommand { get; protected set; }
        public ICommand VolumeUpCommand { get; protected set; }
        public ICommand VolumeDownCommand { get; protected set; }
        public ICommand ModeCommand { get; protected set; }
        public ICommand RotationCommand { get; protected set; }
        public ICommand VisibleCommand { get; protected set; }
        public ICommand PlaybackRateUpCommand { get; protected set; }
        public ICommand PlaybackRateDownCommand { get; protected set; }
        public ICommand AudioLatencyCommand { get; protected set; }

        // Set the unit to seek according to the duration of a content
        private int _seekUnit;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayPageViewModel"/> class
        /// </summary>
        /// <param name="path">Selected file path or streaming uri</param>
        public PlayPageViewModel(string path)
        {
            SourceText = path;     

            // Set uri to selected one
            MediaPlayer.SetSource(path);

            // Set subtitle uri
            MediaPlayer.SetSubtile(ResourcePath.GetPath("test.srt"));

            // Set audio stream policy and apply it
            Tizen.Multimedia.AudioStreamPolicy audioStreamPolicy = new Tizen.Multimedia.AudioStreamPolicy(Tizen.Multimedia.AudioStreamType.Media);
            MediaPlayer.ApplyAudioStreamPolicy(audioStreamPolicy);

            // Create display
            MediaPlayer.CreateDisplay();
            PlayerState = MediaPlayer.State;

            // Add events
            MediaPlayer.Buffering += OnBuffering;
            MediaPlayer.ErrorOccurred += OnErrorOccurred;
            MediaPlayer.SubtitleUpdated += OnSubtitleUpdated;
            MediaPlayer.StateChanged += OnStateChanged;

            InitializeCommands();
        }

        /// <summary>
        /// Initialize all comands
        /// </summary>
        private void InitializeCommands()
        {
            PlayCommand = new Command(async () =>
            {
                try
                {
                    if (PlayerState == MediaPlayerState.Ready || PlayerState == MediaPlayerState.Paused)
                    {
                        MediaPlayer.Start();
                        await ResumeSubtitleAsync();
                    }
                    else
                    {
                        MediaPlayer.Pause();
                        _subtitleDelayCancelSource.Cancel();
                    }
                }
                catch (Exception e)
                {
                    Toast.DisplayText(e.Message, 1000);
                }
            });

            StopCommand = new Command(() =>
            {
                try
                {
                    if (PlayerState == MediaPlayerState.Playing || PlayerState == MediaPlayerState.Paused)
                    {
                        MediaPlayer.Stop();
                    }
                }
                catch (Exception e)
                {
                    Toast.DisplayText(e.Message, 1000);
                }
            });

            SeekBackwardCommand = new Command(async () =>
            {
                try
                {
                    await Seek(-_seekUnit);
                }
                catch (Exception e)
                {
                    Toast.DisplayText(e.Message, 1000);
                }
            });

            SeekForwardCommand = new Command(async () =>
            {
                try
                {
                    await Seek(_seekUnit);
                }
                catch (Exception e)
                {
                    Toast.DisplayText(e.Message, 1000);
                }
            });

            StreamInfoCommand = new Command(() =>
            {
                try
                {
                    Properties = (Properties == null ? MediaPlayer.GetStreamInfo() : null);

                    var infoPopUp = new InformationPopup();
                    if (infoPopUp != null)
                    {
                        foreach (var Property in MediaPlayer.GetStreamInfo())
                        {
                            if (infoPopUp.Text != null)
                            {
                                infoPopUp.Text += ", ";
                            }

                            infoPopUp.Text += ($"{Property.Name}[{Property.Value}]");
                        }

                        infoPopUp.Show();
                    }

                    infoPopUp.BackButtonPressed += (s, e) =>
                    {
                        infoPopUp.Dismiss();
                    };
                }
                catch (Exception e)
                {
                    Toast.DisplayText(e.Message, 1000);
                }
            });

            ContentInfoCommand = new Command(() =>
            {
                try
                {
                    Properties = (Properties == null ? MediaPlayer.GetMetaData() : null);

                    var infoPopUp = new InformationPopup();
                    if (infoPopUp != null)
                    {
                        foreach (var Property in MediaPlayer.GetMetaData())
                        {
                            if (infoPopUp.Text != null)
                            {
                                infoPopUp.Text += ", ";
                            }

                            infoPopUp.Text += ($"{Property.Name}[{Property.Value}]");
                        }

                        infoPopUp.Show();
                    }

                    infoPopUp.BackButtonPressed += (s, e) =>
                    {
                        infoPopUp.Dismiss();
                    };
                }
                catch (Exception e)
                {
                    Toast.DisplayText(e.Message, 1000);
                }
            });

            MuteCommand = new Command(() =>
            {
                MediaPlayer.Muted = ((MediaPlayer.Muted == true) ? false : true);
                OnPropertyChanged(nameof(MuteText));
                Toast.DisplayText(MuteText, 1000);
            });

            LoopCommand = new Command(() =>
            {
                MediaPlayer.IsLooping = ((MediaPlayer.IsLooping == true) ? false : true);
                OnPropertyChanged(nameof(LoopText));
                Toast.DisplayText(LoopText, 1000);
            });

            VolumeUpCommand = new Command(() =>
            {
                float value = MediaPlayer.Volume + 0.1f;
                if (value <= 1.0f)
                {
                    MediaPlayer.Volume = value;
                }

                OnPropertyChanged(nameof(StatusText));
                Toast.DisplayText(StatusText, 1000);
            });

            VolumeDownCommand = new Command(() =>
            {
                float value = MediaPlayer.Volume - 0.1f;
                if (value >= 0.0f)
                {
                    MediaPlayer.Volume = value;
                }

                OnPropertyChanged(nameof(StatusText));
                Toast.DisplayText(StatusText, 1000);
            });

            ModeCommand = new Command(() =>
            {
                if (MediaPlayerDisplayMode.Roi.Equals(MediaPlayer.DisplayMode))
                {
                    MediaPlayer.DisplayMode = MediaPlayerDisplayMode.LetterBox;
                }
                else
                {
                    MediaPlayer.DisplayMode++;
                }

                OnPropertyChanged(nameof(StatusText));
            });

            RotationCommand = new Command(() =>
            {
                if (MediaPlayerRotation.Rotate270.Equals(MediaPlayer.Rotation))
                {
                    MediaPlayer.Rotation = MediaPlayerRotation.Rotate0;
                }
                else
                {
                    MediaPlayer.Rotation++;
                }

                OnPropertyChanged(nameof(StatusText));
            });

            PlaybackRateUpCommand = new Command(() =>
            {
                if (Rate == 5.0f)
                {
                    return;
                }

                Rate += 0.5f;
                MediaPlayer.Rate = Rate;
                Toast.DisplayText(StatusText, 1000);
            });

            PlaybackRateDownCommand = new Command(() =>
            {
                if (Rate == 1.0f)
                {
                    return;
                }

                Rate -= 0.5f;
                MediaPlayer.Rate = Rate;
                Toast.DisplayText(StatusText, 1000);
            });

            VisibleCommand = new Command(() =>
            {
                MediaPlayer.IsVisible = ((MediaPlayer.IsVisible == true) ? false : true);
            });

            AudioLatencyCommand = new Command(() =>
            {
                if (MediaPlayer.AudioLatencyMode < 2)
                {
                    MediaPlayer.AudioLatencyMode++;
                }
                else
                {
                    MediaPlayer.AudioLatencyMode = 0;
                }

                Toast.DisplayText(MediaPlayer.AudioLatencyMode.ToString(), 1000);
            });
        }

        protected IMediaPlayer MediaPlayer => DependencyService.Get<IMediaPlayer>();
        public string PauseText => PlayerState != MediaPlayerState.Playing ? "Play" : "Pause";
        public string MuteText => MediaPlayer.Muted ? "\uD83D\uDD07" : "\uD83D\uDD0A";
        public string LoopText => MediaPlayer.IsLooping ? "\uD83D\uDD01" : "\uD83D\uDD02";
        public string SourceText { get; protected set; }

        /// <summary>
        /// Describe player state.
        /// </summary>
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

        /// <summary>
        /// Properties of streaming information
        /// </summary>
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

        /// <summary>
        /// To show updated subtitle.
        /// </summary>
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

        /// <summary>
        /// To set playback rate.
        /// </summary>
        private float _rate = 1.0f;
        public float Rate
        {
            get => _rate;
            set
            {
                if (_rate != value)
                {
                    _rate = value;
                    OnPropertyChanged(nameof(StatusText));
                }
            }
        }

        /// <summary>
        /// To show playback status.
        /// </summary>
        public string StatusText
        {
            get
            {
                return string.Format("PlaySpeed[{0:0.0}], Volume[{1:0.0}], {2}, {3}",
                    Rate, MediaPlayer.Volume, MediaPlayer.DisplayMode, MediaPlayer.Rotation);
            }
        }

        /// <summary>
        /// Seek when it is available.
        /// </summary>
        /// <returns>A task that represents the asynchronous prepare operation.</returns>
        /// <param name="offset"> A offset to seek</param>
        private async Task Seek(int offset)
        {
            IsSeekable = false;
            await MediaPlayer.SeekAsync(offset);

            IsSeekable = true;
        }

        /// <summary>
        /// Describe whether or not seek is available.
        /// </summary>
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

        /// <summary>
        /// Invoked when the state is changed.
        /// </summary>
        /// <param name="sender">Object that sent event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void OnStateChanged(object sender, EventArgs e)
        {
            PlayerState = MediaPlayer.State;

            switch (PlayerState)
            {
                case MediaPlayerState.Playing:
                    break;

                case MediaPlayerState.Paused:
                    break;

                case MediaPlayerState.Ready:
                    SubtitleText = null;
                    break;
            }

            OnPropertyChanged(nameof(PauseText));
        }

        /// <summary>
        /// Invoked following the buffering callback.
        /// </summary>
        /// <param name="sender">Object that sent event.</param>
        /// <param name="e">Arguments of the event.</param>
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

        public string BufferingText { get; protected set; }

        /// <summary>
        /// Invoked when the error occurs.
        /// </summary>
        /// <param name="sender">Object that sent event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void OnErrorOccurred(object sender, ErrorEventArgs e)
        {
            Toast.DisplayText(e.Message, 1000);
        }

        #region subtitle
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

        /// <summary>
        /// Invoked when the subtitle is updated.
        /// </summary>
        /// <param name="sender">Object that sent event.</param>
        /// <param name="e">Arguments of the event.</param>
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
        #endregion

        /// <summary>
        /// Invoked when this view appears.
        /// </summary>
        public async override void OnAppearing()
        {
            base.OnAppearing();

            await MediaPlayer.PrepareAsync();

            // Calculates seek unit based on duration.
            _seekUnit = Math.Max(1000, MediaPlayer.Duration / 20);
        }

        /// <summary>
        /// Invoked when this view disappears.
        /// </summary>
        public override void OnDisappearing()
        {
            base.OnDisappearing();

            if (PlayerState >= MediaPlayerState.Ready)
            {
                MediaPlayer.Unprepare();
            }

            MediaPlayer.SetSubtile(null);

            MediaPlayer.Buffering -= OnBuffering;
            MediaPlayer.ErrorOccurred -= OnErrorOccurred;
            MediaPlayer.SubtitleUpdated -= OnSubtitleUpdated;
            MediaPlayer.StateChanged -= OnStateChanged;
        }
    }
}
