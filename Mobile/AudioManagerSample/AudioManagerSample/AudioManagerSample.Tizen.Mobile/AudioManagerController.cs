
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
using System.Collections.Generic;
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
        private static readonly string VOL_TYPE_RINGTONE = "Ringtone";

        private static readonly string DEV_TYPE_AUDIOJACK = "Audio Jack";
        private static readonly string DEV_TYPE_BT_MEDIA = "Bluetooth Media";
        private static readonly string DEV_TYPE_BT_VOICE = "Bluetooth Voice";
        private static readonly string DEV_TYPE_BUILTIN_MIC = "Built-in Mic";
        private static readonly string DEV_TYPE_BUILTIN_RECEIVER = "Built-in Receiver";
        private static readonly string DEV_TYPE_BUILTIN_SPEAKER = "Built-in Speaker";
        private static readonly string DEV_TYPE_HDMI = "HDMI";
        private static readonly string DEV_TYPE_USB_AUDIO = "USB Audio";

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
            if (type == AudioVolumeType.Ringtone)
                return VOL_TYPE_RINGTONE;

            return "unknown";
        }

        private string ConvertDeviceTypeToString(AudioDeviceType type)
        {
            if (type == AudioDeviceType.AudioJack)
                return DEV_TYPE_AUDIOJACK;
            if (type == AudioDeviceType.BluetoothMedia)
                return DEV_TYPE_BT_MEDIA;
            if (type == AudioDeviceType.BluetoothVoice)
                return DEV_TYPE_BT_VOICE;
            if (type == AudioDeviceType.BuiltinMic)
                return DEV_TYPE_BUILTIN_MIC;
            if (type == AudioDeviceType.BuiltinReceiver)
                return DEV_TYPE_BUILTIN_RECEIVER;
            if (type == AudioDeviceType.BuiltinSpeaker)
                return DEV_TYPE_BUILTIN_SPEAKER;
            if (type == AudioDeviceType.Hdmi)
                return DEV_TYPE_HDMI;
            if (type == AudioDeviceType.UsbAudio)
                return DEV_TYPE_USB_AUDIO;

            return "unknown";
        }

        public AudioManagerController()
        {
            AudioManager.VolumeController.Changed += (s, e) =>
                VolumeLevelChanged?.Invoke(this, new VolumeLevelChangedEventArgs(ConvertVolumeTypeToString(e.Type), e.Level));
            AudioManager.DeviceConnectionChanged += (s, e) =>
                DeviceConnectionChanged?.Invoke(this, new DeviceConnectionChangedEventArgs(new DeviceItem(e.Device.Id, ConvertDeviceTypeToString(e.Device.Type), e.Device.Name, "Idle"), e.IsConnected));
            AudioManager.DeviceRunningChanged += (s, e) =>
                DeviceRunningChanged?.Invoke(this, new DeviceRunningChangedEventArgs(new DeviceItem(e.Device.Id, ConvertDeviceTypeToString(e.Device.Type), e.Device.Name, e.Device.IsRunning ? "Running" : "Idle"), e.IsRunning));
        }

        public event EventHandler<VolumeLevelChangedEventArgs> VolumeLevelChanged;
        public event EventHandler<DeviceConnectionChangedEventArgs> DeviceConnectionChanged;
        public event EventHandler<DeviceRunningChangedEventArgs> DeviceRunningChanged;

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

            if (type == VOL_TYPE_RINGTONE)
                return AudioManager.VolumeController.Level[AudioVolumeType.Ringtone];

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

            if (type == VOL_TYPE_RINGTONE)
            {
                AudioManager.VolumeController.Level[AudioVolumeType.Ringtone] = level;
                return;
            }

            throw new NotSupportedException();
        }

        public IEnumerable<DeviceItem> GetConnectedDevices()
        {
            IEnumerable<AudioDevice> items = AudioManager.GetConnectedDevices();

            foreach (AudioDevice item in items)
            {
                yield return new DeviceItem(item.Id, ConvertDeviceTypeToString(item.Type), item.Name, item.IsRunning ? "Running" : "Idle");
            }
        }
    }
}
