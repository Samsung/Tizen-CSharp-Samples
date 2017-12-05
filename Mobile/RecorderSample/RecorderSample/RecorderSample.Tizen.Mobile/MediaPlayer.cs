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

using RecorderSample.Tizen.Mobile;
using System.Threading.Tasks;
using Tizen.Multimedia;
using Xamarin.Forms;
using System;

[assembly: Dependency(typeof(MediaPlayer))]
namespace RecorderSample.Tizen.Mobile
{
    class MediaPlayer : IMediaPlayer
    {
        private readonly Player _player = new Player();

        public event EventHandler Stopped;

        public MediaPlayer()
        {
            // The player should stop manually when the playback completes.
            _player.PlaybackCompleted += (s, e) => Stop();
        }

        public void Play()
        {
            _player.Start();
        }

        public async Task Prepare(string path)
        {
            _player.SetSource(new MediaUriSource(path));

            await _player.PrepareAsync();
        }

        public void Stop()
        {
            _player.Stop();
            _player.Unprepare();

            // Raises the event.
            Stopped?.Invoke(this, EventArgs.Empty);
        }

        public void SetDisplay(object nativeView)
        {
            _player.Display = new Display(nativeView as MediaView);
        }

        public int Duration => _player.StreamInfo.GetDuration();

        public int Position => _player.GetPlayPosition();

        public bool IsPlaying => _player.State == PlayerState.Playing;
    }
}
