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

using PlayerSample.Tizen.Mobile;
using System;
using System.Threading.Tasks;
using Tizen.Multimedia;
using Xamarin.Forms;
using System.Collections.Generic;
using Tizen;

[assembly: Dependency(typeof(MediaPlayer))]
namespace PlayerSample.Tizen.Mobile
{
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

        public void SetDisplay(object nativeView)
        {
            _player.Display = new Display(nativeView as MediaView);
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
    }
}
