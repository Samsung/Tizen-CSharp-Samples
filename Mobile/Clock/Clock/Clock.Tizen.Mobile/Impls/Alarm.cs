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

using Clock.Alarm;
using Clock.Common;
using Clock.Interfaces;
using Clock.Tizen.Mobile.Impls;
using Native = Tizen.Applications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tizen.Multimedia;
using Tizen.System;

[assembly: Xamarin.Forms.Dependency(typeof(Alarm))]

namespace Clock.Tizen.Mobile.Impls
{
    /// <summary>
    /// The Alarm class
    /// </summary>
    class Alarm : IAlarm
    {
        AudioStreamPolicy audioStreamPolicy;
        Feedback feedback;
        Player player;
        AlarmRecord _bindableRecord;

        public Alarm()
        {
        }

        /// <summary>
        /// Gets system alarm ID from alarm object
        /// </summary>
        /// <param name="alarm">AlarmRecord object to get system alarm ID from</param>
        /// <seealso cref="AlarmRecord">
        /// <returns> Returns system alarm ID </returns>
        public int GetAlarmID(object alarm)
        {
            Native.Alarm nativeAlarm = alarm as Native.Alarm;
            return nativeAlarm.AlarmId;
        }

        private Native.AlarmWeekFlag Convert(AlarmWeekFlag flag)
        {
            Native.AlarmWeekFlag nativeWeekFlag = 0;

            if (flag == AlarmWeekFlag.AllDays)
            {
                nativeWeekFlag = Native.AlarmWeekFlag.AllDays;
            }
            else if (flag == AlarmWeekFlag.WeekDays)
            {
                nativeWeekFlag = Native.AlarmWeekFlag.WeekDays;
            }
            else
            {
                for (int i = 1; i < 7; i++)
                {
                    int mask = 1 << i;
                    if (((int)flag & mask) > 0)
                    {
                        switch (mask)
                        {
                            case (int)AlarmWeekFlag.Monday:
                                nativeWeekFlag |= Native.AlarmWeekFlag.Monday;
                                break;
                            case (int)AlarmWeekFlag.Tuesday:
                                nativeWeekFlag |= Native.AlarmWeekFlag.Tuesday;
                                break;
                            case (int)AlarmWeekFlag.Wednesday:
                                nativeWeekFlag |= Native.AlarmWeekFlag.Wednesday;
                                break;
                            case (int)AlarmWeekFlag.Thursday:
                                nativeWeekFlag |= Native.AlarmWeekFlag.Thursday;
                                break;
                            case (int)AlarmWeekFlag.Friday:
                                nativeWeekFlag |= Native.AlarmWeekFlag.Friday;
                                break;
                            case (int)AlarmWeekFlag.Saturday:
                                nativeWeekFlag |= Native.AlarmWeekFlag.Saturday;
                                break;
                        }
                    }
                }

                if (((int)flag & 0x1) > 0)
                {
                    nativeWeekFlag |= Native.AlarmWeekFlag.Sunday;
                }
            }

            return nativeWeekFlag;
        }

        /// <summary>
        /// Creates a system alarm of AlarmRecord
        /// </summary>
        /// <param name="binableAlarmRecord">AlarmRecord object to create a system alarm based on</param>
        /// <seealso cref="AlarmRecord">
        /// <returns> Returns native alarm object </returns>
        public int CreateAlarm(AlarmRecord record)
        {
            Native.AppControl appControl = new Native.AppControl()
            {
                ApplicationId = Native.Application.Current.ApplicationInfo.ApplicationId
            };
            appControl.ExtraData.Add("AlarmRecord.UniqueIdentifier", record.GetUniqueIdentifier());
            appControl.ExtraData.Add("RingType", RingType.RING_TYPE_ALARM.ToString());
            Native.AlarmWeekFlag nativeFlag = Convert(record.WeekFlag);
            Native.Alarm nativeAlarm = Native.AlarmManager.CreateAlarm(record.ScheduledDateTime, nativeFlag, appControl);
#if ALARM_DEBUG
            System.Diagnostics.Debug.WriteLine("@@ [Alarm.CreateAlarm] UID : " + record.GetUniqueIdentifier()
               + ", ScheduledDateTime : " + record.ScheduledDateTime + " nativeFlag : " + nativeFlag);
            System.Diagnostics.Debug.WriteLine(" --> Done       Native UID : " + nativeAlarm.AlarmId
                + ", ScheduledDate : " + nativeAlarm.ScheduledDate + " nativeFlag : " + nativeAlarm.WeekFlag);
#endif
            return nativeAlarm.AlarmId;
        }

        /// <summary>
        /// Cancel a system alarm of AlarmReco
        /// </summary>
        /// <param name="binableAlarmRecord">AlarmRecord object relevant to a system alarm to cancel</param>
        /// <seealso cref="AlarmRecord">
        public void DeleteAlarm(AlarmRecord record)
        {
            var allAlarms = Native.AlarmManager.GetAllScheduledAlarms();
            foreach (Native.Alarm item in allAlarms)
            {
                if (item.AlarmId == record.NativeAlarmID)
                {
#if ALARM_DEBUG
                    System.Diagnostics.Debug.WriteLine("@@ [Alarm.DeleteAlarm] Cancel NativeUID:" + record.NativeAlarmID + ", UID:" + record.GetUniqueIdentifier());
#endif
                    item.Cancel();
                    break;
                }
            }
#if ALARM_DEBUG
            Toast.DisplayText("@@ [Alarm.DeleteAlarm] Native Alarm is not found. NativeUID:" + record.NativeAlarmID + ", UID:" + record.GetUniqueIdentifier());
            System.Diagnostics.Debug.WriteLine("@@ [Alarm.DeleteAlarm] Native Alarm is not found. NativeUID:" + record.NativeAlarmID + ", UID:" + record.GetUniqueIdentifier());
#endif
        }

        /// <summary>
        /// Update the system alarm
        /// </summary>
        /// <param name="record">AlarmRecord</param>
        public void UpdateAlarm(ref AlarmRecord record)
        {
            DeleteAlarm(record);
            record.NativeAlarmID = CreateAlarm(record);
        }

        /// <summary>
        /// Activate to ring
        /// </summary>
        /// <param name="delay">The duration of the alarm delay period before an alarm will activate</param>
        /// <seealso cref="int">
        /// <param name="type">RingType for ringing</param>
        /// <seealso cref="RingType">
        public void ActivateAlarm(int delay, RingType type)
        {
            Native.AppControl appControl = new Native.AppControl();
            appControl.ApplicationId = Native.Application.Current.ApplicationInfo.ApplicationId;
            appControl.ExtraData.Add("RingType", type.ToString());
            Native.AlarmManager.CreateAlarm(delay, appControl);
        }

        /// <summary>
        /// Cancel to ring
        /// </summary>
        public void DeactivateAlarm()
        {
            IEnumerable<Native.Alarm> alarmList = Native.AlarmManager.GetAllScheduledAlarms();

            // Find an alarm for Timer page in native all registered alarms
            foreach (Native.Alarm alarm in alarmList)
            {
                Native.AppControl appControl = alarm.AlarmAppControl;
                try
                {
                    string type = (string)appControl.ExtraData.Get("RingType");
                    if (type == RingType.RING_TYPE_TIMER.ToString())
                    {
                        alarm.Cancel();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("[DeactivateAlarm] Exception - Message:{0}", e.Message);
                }
            }
        }

        /// <summary>
        /// This method launches another tone setting app and set proper type based on selection.
        /// </summary>
        /// <param name="alarmRecord">AlarmRecord object</param>
        public void LaunchAlarmToneAppControl(AlarmRecord alarmRecord)
        {
            try
            {
                /// Sets app id
                Native.AppControl appControl = new Native.AppControl()
                {
                    ApplicationId = "org.tizen.setting-ringtone"
                };
                /// Sets operation for app control
                appControl.Operation = "http://tizen.org/appcontrol/operation/pick";
                /// Adds selection mode
                appControl.ExtraData.Add("http://tizen.org/appcontrol/data/selection_mode", "single");
                /// Prepares tone resources
                List<string> toneResources = new List<string>()
                {
                    Native.Application.Current.DirectoryInfo.SharedResource + "ringtones/alarm.mp3",
                    Native.Application.Current.DirectoryInfo.SharedResource + "ringtones",
                };
                /// Sets launch mode
                appControl.LaunchMode = Native.AppControlLaunchMode.Group;
                /// Sets tone resource collections
                appControl.ExtraData.Add("http://tizen.org/appcontrol/data/path", toneResources);
                /// Sets alarm record to update based on app control result
                _bindableRecord = alarmRecord;
                /// Requests launch
                Native.AppControl.SendLaunchRequest(appControl, ReplyAfterLaunching);
            }
            catch (Exception e)
            {
                Debug.WriteLine("[LaunchAlarmToneAppControl] Exception - Message:{0}", e.Message);
            }
        }

        private void ReplyAfterLaunching(Native.AppControl launchRequest, Native.AppControl replyRequest, Native.AppControlReplyResult result)
        {
            //_bindableRecord.AlarmToneType = AlarmToneTypes.RingtoneSdk;
            if (result == Native.AppControlReplyResult.Succeeded)
            {
                List<string> data = (List<string>)replyRequest.ExtraData.Get("http://tizen.org/appcontrol/data/selected");
                if (data.Count == 1)
                {
                    Debug.WriteLine("[LaunchAlarmToneAppControl] Type {0}", data[0]);
                    switch (data[0])
                    {
                        case "/opt/usr/data/settings/Ringtones/rintone_sdk.mp3":
                            _bindableRecord.AlarmToneType = AlarmToneTypes.RingtoneSdk;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Starts to vibrate
        /// </summary>
        public void StartVibration()
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
        public void StopVibration()
        {
            if (feedback != null)
            {
                feedback.Stop();
            }
        }

        /// <summary>
        /// Asynchronously plays ringing sound
        /// </summary>
        async public void PlaySound()
        {
            if (player == null)
            {
                audioStreamPolicy = new AudioStreamPolicy(AudioStreamType.Alarm);
                audioStreamPolicy.AcquireFocus(AudioStreamFocusOptions.Playback, AudioStreamBehaviors.NoResume, null);

                player = new Player();
                MediaUriSource soudSource = new MediaUriSource(SystemSettings.IncomingCallRingtone);
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

        /// <summary>
        /// Asynchronously plays ringing sound
        /// </summary>
        /// <param name="targetVolume">The volume level at playing</param>
        /// <param name="toneTypes">AlarmToneTypes to play</param>
        async public void PlaySound(float targetVolume, AlarmToneTypes toneTypes)
        {
            audioStreamPolicy = new AudioStreamPolicy(AudioStreamType.Alarm);
            audioStreamPolicy.AcquireFocus(AudioStreamFocusOptions.Playback, AudioStreamBehaviors.NoResume, null);

            player = new Player();
            MediaUriSource soudSource = new MediaUriSource(SystemSettings.IncomingCallRingtone);
            player.SetSource(soudSource);

            player.ApplyAudioStreamPolicy(audioStreamPolicy);
            await player.PrepareAsync();
            player.IsLooping = true;
            player.Volume = targetVolume;
            if (player.State == PlayerState.Ready)
            {
                player.Start();
            }
        }

        /// <summary>
        /// Asynchronously stops ringing
        /// </summary>
        public void StopSound()
        {
            if (player != null)
            {
                player.Stop();
                player.Unprepare();
                audioStreamPolicy.ReleaseFocus(AudioStreamFocusOptions.Playback, AudioStreamBehaviors.NoResume, null);
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
