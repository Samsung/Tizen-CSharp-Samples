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

[assembly: Dependency(typeof(CaptureController))]
namespace AudioIOSample.Tizen.Mobile
{
    class CaptureController : Controller, ICaptureController
    {
        private AsyncAudioCapture _asyncAudioCapture;
        private FileStream _saveStream;

        public void Start()
        {
            _saveStream = File.Create(SavePath);

            _asyncAudioCapture = new AsyncAudioCapture(SampleRate, Channel, SampleType);

            // When data is delivered, we just write it to the save stream.
            _asyncAudioCapture.DataAvailable += (s, e) => _saveStream.Write(e.Data, 0, e.Data.Length);

            _asyncAudioCapture.StateChanged += (s, e) =>
                StateChanged?.Invoke(this, new StateChangedEventArgs((State)e.Current));

            _asyncAudioCapture.Prepare();
        }

        public event EventHandler<StateChangedEventArgs> StateChanged;

        public void Pause()
        {
            _asyncAudioCapture.Pause();
        }

        public void Resume()
        {
            _asyncAudioCapture.Resume();
        }

        public void Stop()
        {
            _saveStream.Close();
            _asyncAudioCapture.Dispose();
        }
    }
}
