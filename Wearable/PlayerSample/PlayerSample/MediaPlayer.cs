/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
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

using PlayerSample;
using System;
using System.Threading.Tasks;
using Tizen.Multimedia;
using Xamarin.Forms;
using System.Collections.Generic;

[assembly: Dependency(typeof(MediaPlayer))]
namespace PlayerSample
{
    public static class VideoViewController
    {
        public static Func<ElmSharp.Window> MainWindowProvider { get; set; }
    }

    class MediaPlayer : IMediaPlayer
    {
        private readonly Player _player = new Player();

        public MediaPlayer()
        {
            _player.PlaybackCompleted += (s, e) => Stop();

            _player.BufferingProgressChanged += (s, e) =>
                Buffering?.Invoke(this, new BufferingEventArgs(e.Percent));

            _player.ErrorOccurred += (s, e) =>
                ErrorOccurred?.Invoke(this, new ErrorEventArgs(e.Error.ToString()));

            _player.SubtitleUpdated += (s, e) =>
                SubtitleUpdated?.Invoke(this, new SubtitleUpdatedEventArgs(e.Text, e.Duration));
        }

        public MediaPlayerState State => (MediaPlayerState)_player.State;

        public int Position => _player.GetPlayPosition();

        public int Duration => _player.StreamInfo.GetDuration();

        public event EventHandler<SubtitleUpdatedEventArgs> SubtitleUpdated;
        public event EventHandler<ErrorEventArgs> ErrorOccurred;
        public event EventHandler<BufferingEventArgs> Buffering;
        public event EventHandler<EventArgs> StateChanged;

        public void CreateDisplay()
        {
            var VideoWindow = VideoViewController.MainWindowProvider();

            _player.Display = new Display(VideoWindow);
        }
        public IEnumerable<Property> GetStreamInfo()
        {
            yield return new Property("Duration", _player.StreamInfo.GetDuration());
            yield return new Property("Audio.Codec", _player.StreamInfo.GetAudioCodec());
            yield return new Property("Audio.BitRate", _player.StreamInfo.GetAudioProperties().BitRate);
            yield return new Property("Audio.SampleRate", _player.StreamInfo.GetAudioProperties().SampleRate);
            yield return new Property("Audio.Channels", _player.StreamInfo.GetAudioProperties().Channels);

            yield return new Property("Video.Codec", _player.StreamInfo.GetVideoCodec());
            yield return new Property("Video.BitRate", _player.StreamInfo.GetVideoProperties().BitRate);
            yield return new Property("Video.FPS", _player.StreamInfo.GetVideoProperties().Fps);
            yield return new Property("Video.Size", _player.StreamInfo.GetVideoProperties().Size);
        }

        public IEnumerable<Property> GetMetaData()
        {
            for (StreamMetadataKey i = 0; i <= StreamMetadataKey.Year; i++)
                yield return new Property(i.ToString(), _player.StreamInfo.GetMetadata(i));
        }

        private void RaiseStateChanged()
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Pause()
        {
            _player.Pause();

            RaiseStateChanged();
        }

        public async Task PrepareAsync()
        {
            var task = _player.PrepareAsync();

            RaiseStateChanged();

            try
            {
                await task;
            }
            finally
            {
                RaiseStateChanged();
            }
        }

        public Task SeekAsync(int offset)
        {
            var targetPos = Math.Min(Math.Max(_player.GetPlayPosition() + offset, 0), Duration);

            return _player.SetPlayPositionAsync(targetPos, true);
        }

        public void SetSource(string uri)
        {
            _player.SetSource(new MediaUriSource(uri));
        }

        public void ApplyAudioStreamPolicy(AudioStreamPolicy audioStreamPolicy)
        {
            _player.ApplyAudioStreamPolicy(audioStreamPolicy);
        }
        

        public void SetSubtile(string path)
        {
            if (path == null)
            {
                _player.ClearSubtitle();
            }
            else
            {
                _player.SetSubtitle(path);
            }
        }

        public void SetSubtitleOffset(int offset)
        {
            _player.SetSubtitleOffset(offset);
        }

        public void Start()
        {
            _player.Start();

            RaiseStateChanged();
        }

        public void Stop()
        {
            _player.Stop();

            RaiseStateChanged();
        }

        public void Unprepare()
        {
            _player.Unprepare();

            RaiseStateChanged();
        }
        public bool Muted {
            get
            {
                return _player.Muted;
            }
            set
            {
                _player.Muted = value;
            }
        }
        public bool IsLooping
        {
            get
            {
                return _player.IsLooping;
            }
            set
            {
                _player.IsLooping = value;
            }
        }
        public float Volume
        {
            get
            {
                return _player.Volume;
            }
            set
            {
                _player.Volume = value;
            }
        }
        public MediaPlayerRotation Rotation
        {
            get
            {
                return (MediaPlayerRotation)_player.DisplaySettings.Rotation;
            }
            set
            {
                _player.DisplaySettings.Rotation = (Rotation)value;
            }
        }
        public MediaPlayerDisplayMode DisplayMode
        {
            get
            {
                return (MediaPlayerDisplayMode)_player.DisplaySettings.Mode;
            }
            set
            {
                _player.DisplaySettings.Mode = (PlayerDisplayMode)value;
            }
        }
        public bool IsVisible
        {
            get
            {
                return _player.DisplaySettings.IsVisible;
            }
            set
            {
                _player.DisplaySettings.IsVisible = value;
            }
        }
        public float Rate
        {
            set
            {
                _player.SetPlaybackRate(value);
            }
        }
        public int AudioLatencyMode
        {
            get
            {
                return (int)_player.AudioLatencyMode;
            }
            set
            {
                _player.AudioLatencyMode = (AudioLatencyMode)value;
            }
        }
    }
}
