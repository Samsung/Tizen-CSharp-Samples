
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

using AudioManagerSample.Tizen.Mobile;
using System;
using Tizen.Multimedia;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioManagerController))]
namespace AudioManagerSample.Tizen.Mobile
{
    class AudioManagerController : IAudioManagerController
    {
        private static readonly string VOL_TYPE_SYSTEM = "System";
        private static readonly string VOL_TYPE_MEDIA = "Media";
        private static readonly string VOL_TYPE_NOTIFICATION = "Notification";
        private static readonly string VOL_TYPE_ALARM = "Alarm";
        private static readonly string VOL_TYPE_VOICE = "Voice";

        private string ConvertVolumeTypeToString(AudioVolumeType type)
        {
            if (type == AudioVolumeType.System)
                return VOL_TYPE_SYSTEM;
            if (type == AudioVolumeType.Media)
                return VOL_TYPE_MEDIA;
            if (type == AudioVolumeType.Notification)
                return VOL_TYPE_NOTIFICATION;
            if (type == AudioVolumeType.Alarm)
                return VOL_TYPE_ALARM;
            if (type == AudioVolumeType.Voice)
                return VOL_TYPE_VOICE;

            return "unknown";
        }

        public AudioManagerController()
        {
            AudioManager.VolumeController.Changed += (s, e) =>
                VolumeLevelChanged?.Invoke(this, new VolumeLevelChangedEventArgs(ConvertVolumeTypeToString(e.Type), e.Level));
        }

        public event EventHandler<VolumeLevelChangedEventArgs> VolumeLevelChanged;

        public int GetVolume(string type)
        {
            if (type == VOL_TYPE_SYSTEM)
                return AudioManager.VolumeController.Level[AudioVolumeType.System];

            if (type == VOL_TYPE_MEDIA)
                return AudioManager.VolumeController.Level[AudioVolumeType.Media];

            if (type == VOL_TYPE_NOTIFICATION)
                return AudioManager.VolumeController.Level[AudioVolumeType.Notification];

            if (type == VOL_TYPE_ALARM)
                return AudioManager.VolumeController.Level[AudioVolumeType.Alarm];

            if (type == VOL_TYPE_VOICE)
                return AudioManager.VolumeController.Level[AudioVolumeType.Voice];

            throw new NotSupportedException();
        }

        public void SetVolume(string type, int level)
        {
            if (type == VOL_TYPE_SYSTEM)
            {
                AudioManager.VolumeController.Level[AudioVolumeType.System] = level;
                return;
            }
                
            if (type == VOL_TYPE_MEDIA)
            {
                AudioManager.VolumeController.Level[AudioVolumeType.Media] = level;
                return;
            }

            if (type == VOL_TYPE_NOTIFICATION)
            {
                AudioManager.VolumeController.Level[AudioVolumeType.Notification] = level;
                return;
            }

            if (type == VOL_TYPE_ALARM)
            {
                AudioManager.VolumeController.Level[AudioVolumeType.Alarm] = level;
                return;
            }

            if (type == VOL_TYPE_VOICE)
            {
                AudioManager.VolumeController.Level[AudioVolumeType.Voice] = level;
                return;
            }

            throw new NotSupportedException();
        }
    }
}
