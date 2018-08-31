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

namespace AudioManagerSample
{
    /// <summary>
    /// Provides data for the <see cref="IAudioManagerController.DeviceRunningChanged"/> event.
    /// </summary>
    public class DeviceRunningChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the DeviceRunningChangedEventArgs class.
        /// </summary>
        public DeviceRunningChangedEventArgs(DeviceItem device, bool isRunning)
        {
            IsRunning = isRunning;
            Device = device;
        }

        public bool IsRunning;
        public DeviceItem Device;
    }
}
