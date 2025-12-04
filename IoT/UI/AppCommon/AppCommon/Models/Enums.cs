/*
 * Copyright (c) 2025 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace AppCommon.Models
{
    /// <summary>
    /// Low battery status enumeration
    /// </summary>
    public enum LowBatteryStatus
    {
        None,
        PowerOff,
        CriticalLow
    }

    /// <summary>
    /// Low memory status enumeration
    /// </summary>
    public enum LowMemoryStatus
    {
        None,
        Normal,
        SoftWarning,
        HardWarning
    }

    /// <summary>
    /// Device orientation status enumeration
    /// </summary>
    public enum DeviceOrientationStatus
    {
        Orientation_0,
        Orientation_90,
        Orientation_180,
        Orientation_270
    }
}
