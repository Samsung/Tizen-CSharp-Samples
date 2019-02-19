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
        /// <summary>
        /// To get a window
        /// </summary>
        public static Func<ElmSharp.Window> MainWindowProvider { get; set; }
    }

    class MediaPlayer : IMediaPlayer
    {
        /// <summary>
        /// Create new player
        /// </summary>
        private readonly Player _player = new Player();

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaPlayer"/>
        /// </summary>
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

        /// <summary>
        /// The state of Player
        /// </summary>
        public MediaPlayerState State => (MediaPlayerState)_player.State;

        /// <summary>
        /// Get a position of the player
        /// </summary>
        public int Position => _player.GetPlayPosition();

        /// <summary>
        /// Get a duration of the stream
        /// </summary>
        public int Duration => _player.StreamInfo.GetDuration();

        /// <summary>
        /// Subtitle updated event
        /// </summary>
        public event EventHandler<SubtitleUpdatedEventArgs> SubtitleUpdated;

        /// <summary>
        /// Error occured event
        /// </summary>
        public event EventHandler<ErrorEventArgs> ErrorOccurred;

        /// <summary>
        /// Buffering event
        /// </summary>
        public event EventHandler<BufferingEventArgs> Buffering;

        /// <summary>
        /// State changed event
        /// </summary>
        public event EventHandler<EventArgs> StateChanged;

        /// <summary>
        /// Create window and set display
        /// </summary>
        public void CreateDisplay()
        {
            var VideoWindow = VideoViewController.MainWindowProvider();

            _player.Display = new Display(VideoWindow);
        }

        /// <summary>
        /// Get stream information
        /// </summary>
        /// <returns> Stream's informations </returns>
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

        /// <summary>
        /// Get metadata
        /// </summary>
        /// <returns> MetaData </returns>
        public IEnumerable<Property> GetMetaData()
        {
            for (StreamMetadataKey i = 0; i <= StreamMetadataKey.Year; i++)
            {
                yield return new Property(i.ToString(), _player.StreamInfo.GetMetadata(i));
            }
        }

        /// <summary>
        /// Invoke when state changed
        /// </summary>
        private void RaiseStateChanged()
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Pause the player
        /// </summary>
        public void Pause()
        {
            _player.Pause();

            RaiseStateChanged();
        }

        /// <summary>
        /// Prepare the player
        /// </summary>
        /// <returns>A task that represents the asynchronous prepare operation.</returns>
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

        /// <summary>
        /// Set a position of the player
        /// </summary>
        /// <returns>A task that represents the asynchronous prepare operation.</returns>
        /// <param name="offset">A offset to seek</param>
        public Task SeekAsync(int offset)
        {
            var targetPos = Math.Min(Math.Max(_player.GetPlayPosition() + offset, 0), Duration);

            return _player.SetPlayPositionAsync(targetPos, true);
        }

        /// <summary>
        /// Set uri for playing
        /// </summary>
        /// <param name="uri">Uri to play</param>
        public void SetSource(string uri)
        {
            _player.SetSource(new MediaUriSource(uri));
        }

        /// <summary>
        /// Apply audio stream policy
        /// </summary>
        /// <param name="audioStreamPolicy">Audio stream policy</param>
        public void ApplyAudioStreamPolicy(AudioStreamPolicy audioStreamPolicy)
        {
            _player.ApplyAudioStreamPolicy(audioStreamPolicy);
        }

        /// <summary>
        /// Set subtitle uri
        /// </summary>
        /// <param name="path">A path of subtitle</param>
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

        /// <summary>
        /// Set subtitle offset
        /// </summary>
        /// <param name="offset">The offset for subtitle</param>
        public void SetSubtitleOffset(int offset)
        {
            _player.SetSubtitleOffset(offset);
        }

        /// <summary>
        /// Start the player
        /// </summary>
        public void Start()
        {
            _player.Start();

            RaiseStateChanged();
        }

        /// <summary>
        /// Stop the player
        /// </summary>
        public void Stop()
        {
            _player.Stop();

            RaiseStateChanged();
        }

        /// <summary>
        /// Unprepare the player
        /// </summary>
        public void Unprepare()
        {
            _player.Unprepare();

            RaiseStateChanged();
        }

        /// <summary>
        /// Sets and gets the Mute.
        /// </summary>
        public bool Muted
        {
            get => _player.Muted;
            set => _player.Muted = value;
        }

        /// <summary>
        /// Sets and gets the looping.
        /// </summary>
        public bool IsLooping
        {
            get => _player.IsLooping;
            set => _player.IsLooping = value;
        }

        /// <summary>
        /// Sets and gets the Volume.
        /// </summary>
        public float Volume
        {
            get => _player.Volume;
            set => _player.Volume = value;
        }

        /// <summary>
        /// Sets and gets the Rotation.
        /// </summary>
        public MediaPlayerRotation Rotation
        {
            get => (MediaPlayerRotation)_player.DisplaySettings.Rotation;
            set => _player.DisplaySettings.Rotation = (Rotation)value;
        }

        /// <summary>
        /// Sets and gets the Displaymode.
        /// </summary>
        public MediaPlayerDisplayMode DisplayMode
        {
            get => (MediaPlayerDisplayMode)_player.DisplaySettings.Mode;
            set => _player.DisplaySettings.Mode = (PlayerDisplayMode)value;
        }

        /// <summary>
        /// Sets and gets the Visibility.
        /// </summary>
        public bool IsVisible
        {
            get => _player.DisplaySettings.IsVisible;
            set => _player.DisplaySettings.IsVisible = value;
        }

        /// <summary>
        /// Sets the PlaybackRate.
        /// </summary>
        public float Rate
        {
            set => _player.SetPlaybackRate(value);
        }

        /// <summary>
        /// Sets and gets the AudioLatencyMode.
        /// </summary>
        public int AudioLatencyMode
        {
            get => (int)_player.AudioLatencyMode;
            set => _player.AudioLatencyMode = (AudioLatencyMode)value;
        }
    }
}
