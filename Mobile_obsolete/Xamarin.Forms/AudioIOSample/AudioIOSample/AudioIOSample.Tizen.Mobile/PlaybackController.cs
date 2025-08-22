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

using AudioIOSample.Tizen.Mobile;
using System;
using System.IO;
using Tizen.Multimedia;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlaybackController))]
namespace AudioIOSample.Tizen.Mobile
{
    class PlaybackController : Controller, IPlaybackController
    {
        private AudioPlayback _audioPlayback;
        private FileStream _saveStream;

        public void Start()
        {
            _saveStream = File.OpenRead(SavePath);

            _audioPlayback = new AudioPlayback(SampleRate, Channel, SampleType);

            _audioPlayback.StateChanged += (s, e) =>
                StateChanged?.Invoke(this, new StateChangedEventArgs((State)e.Current));

            _audioPlayback.Prepare();

            _audioPlayback.BufferAvailable += (s, e) =>
            {
                if (_saveStream.Position == _saveStream.Length)
                {
                    return;
                }

                var buf = new byte[e.Length];
                _saveStream.Read(buf, 0, buf.Length);
                _audioPlayback.Write(buf);
            };

            // Or you can write at once.
             // _audioPlayback.Write(File.ReadAllBytes(SavePath));
        }

        public event EventHandler<StateChangedEventArgs> StateChanged;

        public void Pause()
        {
            _audioPlayback.Pause();
        }

        public void Resume()
        {
            _audioPlayback.Resume();
        }

        public void Stop()
        {
            _saveStream.Close();
            _audioPlayback.Dispose();
        }
    }
}
