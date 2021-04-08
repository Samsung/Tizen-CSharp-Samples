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
using System.Threading.Tasks;

namespace RadioSample
{
    /// <summary>
    /// Specifies radio states.
    /// </summary>
    public enum RadioState
    {
        /// <summary>
        /// Ready.
        /// </summary>
        Ready,
        /// <summary>
        /// Playing.
        /// </summary>
        Playing,
        /// <summary>
        /// Scanning.
        /// </summary>
        Scanning
    }

    public interface IRadioController
    {
        /// <summary>
        /// Starts the radio.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the radio.
        /// </summary>
        void Stop();

        /// <summary>
        /// Starts scanning.
        /// </summary>
        void StartScan();

        /// <summary>
        /// Seeks up the next effective frequency.
        /// </summary>
        /// <returns>A task that represents the asynchronous seeking operation.</returns>
        Task SeekUpAsync();

        /// <summary>
        /// Seeks down the next effective frequency.
        /// </summary>
        /// <returns>A task that represents the asynchronous seeking operation.</returns>
        Task SeekDownAsync();

        /// <summary>
        /// Gets the current radio state.
        /// </summary>
        RadioState State { get; }

        /// <summary>
        /// Gets or sets the radio frequency.
        /// </summary>
        int Frequency { get; set; }

        /// <summary>
        /// Occurs when scan completed.
        /// </summary>
        event EventHandler ScanCompleted;


        /// <summary>
        /// Occurs when scan updated.
        /// </summary>
        event EventHandler<ScanUpdatedEventArgs> ScanUpdated;
    }

    /// <summary>
    /// Provides data for the <see cref="IRadioController.ScanUpdated"/> event.
    /// </summary>
    public class ScanUpdatedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ScanUpdatedEventArgs class.
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        public ScanUpdatedEventArgs(int frequency)
        {
            Frequency = frequency;
        }

        /// <summary>
        /// Gets the frequency.
        /// </summary>
        public int Frequency { get; }
    }
}
