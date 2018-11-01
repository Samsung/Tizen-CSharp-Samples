/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
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

namespace AudioManagerSample
{
    public interface IAudioManagerController
    {
        /// <summary>
        /// Sets volume.
        /// </summary>
        void SetVolume(string type, int level);

        /// <summary>
        /// Gets volume.
        /// </summary>
        int GetVolume(string type);

        /// <summary>
        /// Gets connected device list.
        /// </summary>
        IEnumerable<DeviceItem> GetConnectedDevices();

        /// <summary>
        /// Gets supported sample formats.
        /// </summary>
        IEnumerable<string> GetSupportedSampleFormats(int deviceId);

        /// <summary>
        /// Gets supported sample rates.
        /// </summary>
        IEnumerable<uint> GetSupportedSampleRates(int deviceId);

        /// <summary>
        /// Gets sample format of the device.
        /// </summary>
        string GetSampleFormat(int deviceId);

        /// <summary>
        /// Sets sample format of the device.
        /// </summary>
        void SetSampleFormat(int deviceId, string format);

        /// <summary>
        /// Gets sample rate of the device.
        /// </summary>
        uint GetSampleRate(int deviceId);

        /// <summary>
        /// Sets sample rate of the device.
        /// </summary>
        void SetSampleRate(int deviceId, uint rate);

        /// <summary>
        /// Gets media-streeam-only property of the device.
        /// </summary>
        bool GetMediaStreamOnly(int deviceId);

        /// <summary>
        /// Sets media-streeam-only property of the device.
        /// </summary>
        void SetMediaStreamOnly(int deviceId, bool enable);

        /// <summary>
        /// Gets avoid-resampling property of the device.
        /// </summary>
        bool GetAvoidResampling(int deviceId);

        /// <summary>
        /// Sets avoid-resampling property of the device.
        /// </summary>
        void SetAvoidResampling(int deviceId, bool enable);

        /// <summary>
        /// Occurs when the level of the volume changes.
        /// </summary>
        event EventHandler<VolumeLevelChangedEventArgs> VolumeLevelChanged;

        /// <summary>
        /// Occurs when the connection state of the device changes.
        /// </summary>
        event EventHandler<DeviceConnectionChangedEventArgs> DeviceConnectionChanged;

        /// <summary>
        /// Occurs when the running state of the device changes.
        /// </summary>
        event EventHandler<DeviceRunningChangedEventArgs> DeviceRunningChanged;
    }
}
