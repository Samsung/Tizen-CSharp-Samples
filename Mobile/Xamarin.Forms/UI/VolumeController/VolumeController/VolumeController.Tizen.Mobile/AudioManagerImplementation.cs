/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
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
using Tizen.Multimedia;
using VolumeController.Tizen.Mobile;

[assembly:Xamarin.Forms.Dependency(typeof(AudioManagerImplementation))]

namespace VolumeController.Tizen.Mobile
{
    /// <summary>
    /// Manager of system audio
    /// </summary>
    public class AudioManagerImplementation : IAudioManager
    {
        /// <summary>
        /// The constructor of AudioManagerImplementation
        /// </summary>
        public AudioManagerImplementation()
        {
        }
        /// <summary>
        /// Get the "type"'s audio value
        /// </summary>
        /// <param name="type">AudioVolumeTypeShare's type</param>
        /// <returns>audio value</returns>
        public int LevelType(VolumeController.AudioVolumeTypeShare type)
        {

                switch (type)
                {
                    case VolumeController.AudioVolumeTypeShare.Alarm:
                        return AudioManager.VolumeController.Level[AudioVolumeType.Alarm];
                    case VolumeController.AudioVolumeTypeShare.Media:
                        return AudioManager.VolumeController.Level[AudioVolumeType.Media];
                    case VolumeController.AudioVolumeTypeShare.Notification:
                        return AudioManager.VolumeController.Level[AudioVolumeType.Notification];
                    case VolumeController.AudioVolumeTypeShare.Ringtone:
                        return AudioManager.VolumeController.Level[AudioVolumeType.Ringtone];
                    case VolumeController.AudioVolumeTypeShare.System:
                        return AudioManager.VolumeController.Level[AudioVolumeType.System];
                    case VolumeController.AudioVolumeTypeShare.Voice:
                        return AudioManager.VolumeController.Level[AudioVolumeType.Voice];
                    case VolumeController.AudioVolumeTypeShare.Voip:
                        return AudioManager.VolumeController.Level[AudioVolumeType.Voip];
                    default:
                        return AudioManager.VolumeController.Level[AudioVolumeType.None];
                }
        }

        /// <summary>
        /// Get the "type"'s max value
        /// </summary>
        /// <param name="type">AudioVolumeTypeShare's type</param>
        /// <returns>max value</returns>
        public int MaxLevel(VolumeController.AudioVolumeTypeShare type)
        {
          switch (type)
            {
                case VolumeController.AudioVolumeTypeShare.Alarm:
                    return AudioManager.VolumeController.MaxLevel[AudioVolumeType.Alarm];
                case VolumeController.AudioVolumeTypeShare.Media:
                    return AudioManager.VolumeController.MaxLevel[AudioVolumeType.Media];
                case VolumeController.AudioVolumeTypeShare.Notification:
                    return AudioManager.VolumeController.MaxLevel[AudioVolumeType.Notification];
                case VolumeController.AudioVolumeTypeShare.Ringtone:
                    return AudioManager.VolumeController.MaxLevel[AudioVolumeType.Ringtone];
                case VolumeController.AudioVolumeTypeShare.System:
                    return AudioManager.VolumeController.MaxLevel[AudioVolumeType.System];
                case VolumeController.AudioVolumeTypeShare.Voice:
                    return AudioManager.VolumeController.MaxLevel[AudioVolumeType.Voice];
                case VolumeController.AudioVolumeTypeShare.Voip:
                    return AudioManager.VolumeController.MaxLevel[AudioVolumeType.Voip];
                default:
                    return AudioManager.VolumeController.MaxLevel[AudioVolumeType.None];
            }
        }

        /// <summary>
        /// Set the "type"'s value by "value"
        /// </summary>
        /// <param name="type">AudioVolumeTypeShare's type</param>
        /// <param name="value">the value to setted</param>
        public void ApplyAudioType(VolumeController.AudioVolumeTypeShare type, int value)
        {
            switch (type)
            {
                case VolumeController.AudioVolumeTypeShare.Alarm:
                   AudioManager.VolumeController.Level[AudioVolumeType.Alarm] = value;
                    break;
                case VolumeController.AudioVolumeTypeShare.Media:
                   AudioManager.VolumeController.Level[AudioVolumeType.Media] = value;
                    break;
                case VolumeController.AudioVolumeTypeShare.Notification:
                   AudioManager.VolumeController.Level[AudioVolumeType.Notification] = value;
                    break;
                case VolumeController.AudioVolumeTypeShare.Ringtone:
                   AudioManager.VolumeController.Level[AudioVolumeType.Ringtone] = value;
                    break;
                case VolumeController.AudioVolumeTypeShare.System:
                   AudioManager.VolumeController.Level[AudioVolumeType.System] = value;
                    break;
                case VolumeController.AudioVolumeTypeShare.Voice:
                   AudioManager.VolumeController.Level[AudioVolumeType.Voice] = value;
                    break;
                case VolumeController.AudioVolumeTypeShare.Voip:
                   AudioManager.VolumeController.Level[AudioVolumeType.Voip] = value;
                    break;
                default:
                   AudioManager.VolumeController.Level[AudioVolumeType.None] = 0;
                    break;

            }
        }
    }
}

