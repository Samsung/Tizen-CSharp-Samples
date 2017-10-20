/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
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

using Clock.Common;
using Clock.Alarm;

namespace Clock.Interfaces
{
    /// <summary>
    /// The Alarm Interface
    /// </summary>
    public interface IAlarm
    {
        /// <summary>
        /// Gets system alarm ID from alarm object
        /// </summary>
        /// <param name="alarm">AlarmRecord object to get system alarm ID from</param>
        /// <seealso cref="AlarmRecord">
        /// <returns> Returns system alarm ID </returns>
        int GetAlarmID(object alarm);

        /// <summary>
        /// Creates a system alarm of AlarmRecord
        /// </summary>
        /// <param name="binableAlarmRecord">AlarmRecord object to create a system alarm based on</param>
        /// <seealso cref="AlarmRecord">
        /// <returns> Returns native alarm object </returns>
        int CreateAlarm(AlarmRecord binableAlarmRecord);

        /// <summary>
        /// Cancel a system alarm of AlarmReco
        /// </summary>
        /// <param name="binableAlarmRecord">AlarmRecord object relevant to a system alarm to cancel</param>
        /// <seealso cref="AlarmRecord">
        void DeleteAlarm(AlarmRecord binableAlarmRecord);

        /// <summary>
        /// Update a system alarm of AlarmReco
        /// </summary>
        /// <param name="binableAlarmRecord">AlarmRecord object relevant to a system alarm to cancel</param>
        /// <seealso cref="AlarmRecord">
        void UpdateAlarm(ref AlarmRecord binableAlarmRecord);

        /// <summary>
        /// Activates an alarm
        /// </summary>
        /// <param name="delay">The duration of the alarm delay period before an alarm will activate</param>
        /// <seealso cref="int">
        /// <param name="type">RingType for ringing</param>
        /// <seealso cref="RingType">
        void ActivateAlarm(int delay, RingType type);

        /// <summary>
        /// Deactivates an alarm
        /// </summary>
        void DeactivateAlarm();

        /// <summary>
        /// Lanches alarm tone application control
        /// </summary>
        /// <param name="alarmRecord">AlarmRecord object</param>
        void LaunchAlarmToneAppControl(AlarmRecord alarmRecord);

        /// <summary>
        /// Starts to vibrate
        /// </summary>
        void StartVibration();

        /// <summary>
        /// Stops to vibrate
        /// </summary>
        void StopVibration();

        /// <summary>
        /// Plays ringing sound
        /// </summary>
        void PlaySound();

        /// <summary>
        /// Plays ringing sound with the given alarm tone type
        /// </summary>
        /// <param name="targetVolume">float</param>
        /// <param name="toneTypes">AlarmToneTypes</param>
        void PlaySound(float targetVolume, AlarmToneTypes toneTypes);

        /// <summary>
        /// Stops ringing
        /// </summary>
        void StopSound();

    }
}
