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

using RadioSample.Tizen.Mobile;
using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Tizen.Multimedia;

[assembly: Dependency(typeof(RadioController))]
namespace RadioSample.Tizen.Mobile
{
    /// <summary>
    /// Tizen dependent implementation class for radio.
    /// </summary>
    class RadioController : IRadioController
    {
        private Radio _radio;

        public event EventHandler ScanCompleted;
        public event EventHandler<ScanUpdatedEventArgs> ScanUpdated;

        public RadioController()
        {
            _radio = new Radio();

            _radio.ScanUpdated += (s, e) => ScanUpdated?.Invoke(this, new ScanUpdatedEventArgs(e.Frequency));

            _radio.ScanCompleted += (s, e) => ScanCompleted?.Invoke(this, EventArgs.Empty);
        }

        // Convert the tizen-specific enum to the common enum.
        public RadioState State => Enum.Parse<RadioState>(_radio.State.ToString());

        public int Frequency
        {
            get => _radio.Frequency;
            set => _radio.Frequency = value;
        }

        public Task SeekDownAsync()
        {
            return _radio.SeekDownAsync();
        }

        public Task SeekUpAsync()
        {
            return _radio.SeekUpAsync();
        }

        public void Start()
        {
            _radio.Start();
        }

        public void Stop()
        {
            _radio.Stop();
        }

        public void StartScan()
        {
            _radio.StartScan();
        }
    }
}
