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

using Xamarin.Forms;

namespace AudioManagerSample
{
    /// <summary>
    /// ViewModel for VolumePage.
    /// </summary>
    class VolumePageViewModel : BaseViewModel
    {
        public VolumePageViewModel()
        {
            _systemLabel = AMController.GetVolume("System");
            _mediaLabel = AMController.GetVolume("Media");
            _notificationLabel = AMController.GetVolume("Notification");
            _alarmLabel = AMController.GetVolume("Alarm");
            _voiceLabel = AMController.GetVolume("Voice");
            _ringtoneLabel = AMController.GetVolume("Ringtone");

            AMController.VolumeLevelChanged += OnVolumeLevelChanged;
        }
        protected IAudioManagerController AMController => DependencyService.Get<IAudioManagerController>();

        private void OnVolumeLevelChanged(object sender, VolumeLevelChangedEventArgs e)
        {
            ChangedVolumeLabel = e.Type + " volume is changed to " + e.Level;
            if (e.Type == "System")
            {
                _systemLabel = e.Level;
                OnPropertyChanged(nameof(SystemLabel));
            }
            else if (e.Type == "Media")
            {
                _mediaLabel = e.Level;
                OnPropertyChanged(nameof(MediaLabel));
            }
            else if (e.Type == "Notification")
            {
                _notificationLabel = e.Level;
                OnPropertyChanged(nameof(NotificationLabel));
            }
            else if (e.Type == "Alarm")
            {
                _alarmLabel = e.Level;
                OnPropertyChanged(nameof(AlarmLabel));
            }
            else if (e.Type == "Voice")
            {
                _voiceLabel = e.Level;
                OnPropertyChanged(nameof(VoiceLabel));
            }
            else if (e.Type == "Ringtone")
            {
                _ringtoneLabel = e.Level;
                OnPropertyChanged(nameof(RingtoneLabel));
            }
        }

        private int _systemLabel;
        public int SystemLabel
        {
            get => _systemLabel;
            set
            {
                if (_systemLabel != value)
                {
                    _systemLabel = value;

                    OnPropertyChanged(nameof(SystemLabel));
                    AMController.SetVolume("System", value);
                }
            }
        }
        private int _mediaLabel;
        public int MediaLabel
        {
            get => _mediaLabel;
            set
            {
                if (_mediaLabel != value)
                {
                    _mediaLabel = value;

                    OnPropertyChanged(nameof(MediaLabel));
                    AMController.SetVolume("Media", value);
                }
            }
        }

        private int _notificationLabel;
        public int NotificationLabel
        {
            get => _notificationLabel;
            set
            {
                if (_notificationLabel != value)
                {
                    _notificationLabel = value;

                    OnPropertyChanged(nameof(NotificationLabel));
                    AMController.SetVolume("Notification", value);
                }
            }
        }
        private int _alarmLabel;
        public int AlarmLabel
        {
            get => _alarmLabel;
            set
            {
                if (_alarmLabel != value)
                {
                    _alarmLabel = value;

                    OnPropertyChanged(nameof(AlarmLabel));
                    AMController.SetVolume("Alarm", value);
                }
            }
        }
        private int _voiceLabel;
        public int VoiceLabel
        {
            get => _voiceLabel;
            set
            {
                if (_voiceLabel != value)
                {
                    _voiceLabel = value;

                    OnPropertyChanged(nameof(VoiceLabel));
                    AMController.SetVolume("Voice", value);
                }
            }
        }

        private int _ringtoneLabel;
        public int RingtoneLabel
        {
            get => _ringtoneLabel;
            set
            {
                if (_ringtoneLabel != value)
                {
                    _ringtoneLabel = value;

                    OnPropertyChanged(nameof(RingtoneLabel));
                    AMController.SetVolume("Ringtone", value);
                }
            }
        }

        private string _changedVolumeLabel;
        public string ChangedVolumeLabel
        {
            get => _changedVolumeLabel;
            set
            {
                _changedVolumeLabel = value;

                OnPropertyChanged(nameof(ChangedVolumeLabel));
            }
        }

        public override void OnPopped()
        {
            base.OnPopped();

            AMController.VolumeLevelChanged -= OnVolumeLevelChanged;
        }
    }
}