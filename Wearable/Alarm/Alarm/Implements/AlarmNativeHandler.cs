/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Alarm.Models;
using Native = Tizen.Applications;
using Tizen.Multimedia;
using Tizen.System;
using System;

namespace Alarm.Implements
{
    /// <summary>
    /// The Alarm class
    /// </summary>
    public static class AlarmNativeHandler
    {
        static AudioStreamPolicy audioStreamPolicy;
        static Feedback feedback;
        static Player player;
        static string SoundNotificationUri = "Notification.ogg";

        public static string ResourceDir { get; set; }

        /// <summary>
        /// Gets system alarm ID from alarm object
        /// </summary>
        /// <param name="alarm">AlarmRecord object to get system alarm ID from</param>
        /// <seealso cref="AlarmRecord">
        /// <returns> Returns system alarm ID </returns>
        public static int GetAlarmID(object alarm)
        {
            Native.Alarm nativeAlarm = alarm as Native.Alarm;
            return nativeAlarm.AlarmId;
        }

        /// <summary>
        /// Creates a system alarm of AlarmRecord
        /// </summary>
        /// <param name="binableAlarmRecord">AlarmRecord object to create a system alarm based on</param>
        /// <seealso cref="AlarmRecord">
        /// <returns> Returns native alarm object </returns>
        public static int CreateAlarm(AlarmRecord record)
        {
            Native.AppControl appControl = new Native.AppControl()
            {
                ApplicationId = Native.Application.Current.ApplicationInfo.ApplicationId
            };
            appControl.ExtraData.Add("AlarmRecord.UniqueIdentifier", record.GetUniqueIdentifier());
            //temporary set AllDays , After implement select day of week. additional implementation should be add.
            Native.Alarm nativeAlarm = Native.AlarmManager.CreateAlarm(record.ScheduledDateTime, Native.AlarmWeekFlag.AllDays, appControl);
            Console.WriteLine("@@ [Alarm.CreateAlarm] UID : " + record.GetUniqueIdentifier()
               + ", ScheduledDateTime : " + record.ScheduledDateTime + " NativeAlarmID : " + nativeAlarm.AlarmId);
            return nativeAlarm.AlarmId;
        }

        /// <summary>
        /// Cancel a system alarm of AlarmRecord
        /// </summary>
        /// <param name="binableAlarmRecord">AlarmRecord object relevant to a system alarm to cancel</param>
        /// <seealso cref="AlarmRecord">
        public static void DeleteAlarm(AlarmRecord record)
        {
            var allAlarms = Native.AlarmManager.GetAllScheduledAlarms();
            foreach (Native.Alarm item in allAlarms)
            {
                if (item.AlarmId == record.NativeAlarmID)
                {
                    Console.WriteLine("@@ [Alarm.DeleteAlarm] Cancel NativeAlarmID:" + record.NativeAlarmID + ", UID:" + record.GetUniqueIdentifier());
                    item.Cancel();
                    record.NativeAlarmID = 0;
                    break;
                }
            }
        }

        /// <summary>
        /// Update the system alarm
        /// </summary>
        /// <param name="record">AlarmRecord</param>
        public static void UpdateAlarm(ref AlarmRecord record)
        {
            DeleteAlarm(record);
            record.NativeAlarmID = CreateAlarm(record);
        }

        /// <summary>
        /// Starts to vibrate
        /// </summary>
        public static void StartVibration()
        {
            feedback = new Feedback();
            if (feedback.IsSupportedPattern(FeedbackType.Vibration, "Timer"))
            {
                feedback.Play(FeedbackType.Vibration, "Timer");
            }
        }

        /// <summary>
        /// Stops to vibrate
        /// </summary>
        public static void StopVibration()
        {
            if (feedback != null)
            {
                feedback.Stop();
            }
        }

        /// <summary>
        /// Asynchronously plays ringing sound
        /// </summary>
        async public static void PlaySound()
        {
            if (player == null)
            {
                audioStreamPolicy = new AudioStreamPolicy(AudioStreamType.Alarm);
                audioStreamPolicy.AcquireFocus(AudioStreamFocusOptions.Playback, AudioStreamBehaviors.Fading, null);

                player = new Player();
                string uri = ResourceDir + SoundNotificationUri;
                MediaUriSource soudSource = new MediaUriSource(uri);
                player.SetSource(soudSource);

                player.ApplyAudioStreamPolicy(audioStreamPolicy);
                await player.PrepareAsync();
                player.IsLooping = true;
                player.Volume = 1;
                if (player.State == PlayerState.Ready)
                {
                    player.Start();
                }
            }
        }

        public static void PauseSound()
        {
            if (player.State == PlayerState.Playing)
            {
                player.Stop();
            }
        }

        public static void ResumeSound()
        {
            if (player.State == PlayerState.Paused || player.State == PlayerState.Ready)
            {
                player.Start();
            }
        }

        /// <summary>
        /// Asynchronously stops ringing
        /// </summary>
        public static void StopSound()
        {
            if (player != null)
            {
                player.Stop();
                player.Unprepare();
                audioStreamPolicy.ReleaseFocus(AudioStreamFocusOptions.Playback, AudioStreamBehaviors.Fading, null);
                player.Dispose();
                player = null;
            }

            if (audioStreamPolicy != null)
            {
                audioStreamPolicy.Dispose();
            }
        }
    }
}
