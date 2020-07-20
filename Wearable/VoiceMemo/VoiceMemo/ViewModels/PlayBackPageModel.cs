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

using System;
using System.Diagnostics;
using System.Windows.Input;
using VoiceMemo.Models;
using VoiceMemo.Services;
using Xamarin.Forms;

namespace VoiceMemo.ViewModels
{
    public enum VolumeControlType
    {
        Minus,
        Plus,
    }

    /// <summary>
    /// Page model class for PlayBack page
    /// </summary>
    public class PlayBackPageModel : BasePageModel
    {
        const string VOLUME_ON = "voice_mamo_slider_volume_on.png"; //"button/details_vol_icon_on.png"
        const string VOLUME_OFF = "voice_mamo_slider_mute.png";  //"button/details_vol_icon_off.png"
        const string VOLUME_ON_SMALL_IMAGE = "button/details_vol_icon_on.png";
        const string VOLUME_OFF_SMALL_IMAGE = "button/details_vol_icon_off.png";
        const string PLAY_ON = "voicerecorder_btn_play.png";
        const string PLAY_OFF = "voicerecorder_btn_pause.png";
        double play_progressbar_delta;

        public PlayBackPageModel(Record record = null)
        {
            if (PlayWatch == null)
            {
                PlayWatch = new Stopwatch();
            }

            _playService = DependencyService.Get<IAudioPlayService>();
            _playService.AudioPlayFinished += _playService_AudioPlayFinished;
            MaxVolumeLevel = _playService.GetMaxVolume();
            // You can get notified whenever the state of audio player has been changed
            MessagingCenter.Subscribe<IAudioPlayService, AudioPlayState>(this, MessageKeys.PlayerStateChanged, (obj, state) =>
            {
                if (state == AudioPlayState.Playing)
                {
                    PlayControlImage = PLAY_OFF;
                    // start to update the remaining time
                    StartTimerForRemainingTime();
                }
                else
                {
                    PlayControlImage = PLAY_ON;
                    // Stop updating the remaining time
                    StopTimerForRemainingTime();
                }
            });
            Init(record);
        }

        public async void Init(Record record)
        {
            PlayControlImage = PLAY_ON;
            _record = record;
            VolumeViewVisible = false;
            CurrentVolume = _playService.Volume;
            if (CurrentVolume == 0)
            {
                Mute = true;
                MuteOnOffImage = VOLUME_OFF;
                MuteOnOffSmallImage = VOLUME_OFF_SMALL_IMAGE;
            }
            else
            {
                Mute = false;
                MuteOnOffImage = VOLUME_ON;
                MuteOnOffSmallImage = VOLUME_ON_SMALL_IMAGE;
            }

            RemainingTime = record.Duration;
            Console.WriteLine(" [PlayBackPageModel] RemainingTime : " + RemainingTime);
            int minutes = RemainingTime / 60000;
            int seconds = (RemainingTime - minutes * 60000) / 1000;
            int total = minutes * 60 + seconds;
            Console.WriteLine(" [PlayBackPageModel] minutes : " + minutes + ", seconds:" + seconds + ", total:" + total);
            PlayingProcess = 0.0;
            VolumeLevelProcess = CurrentVolume / MaxVolumeLevel;
            Console.WriteLine(" [PlayBackPageModel] VolumeLevelProcess : " + VolumeLevelProcess);
            // calculate value (unit: 100ms)
            play_progressbar_delta = (1.0 / total) / 10;
            Console.WriteLine(" [PlayBackPageModel] play_progressbar_delta : " + play_progressbar_delta);
            _Touched = false;

            PlayWatch.Reset();

            _playService.VolumeChanged += _playService_VolumeChanged;
            _playService.RegisterVolumeChangedCallback();
            await _playService.Init(record.Path);
        }

        // It's called when PlayBackPage is hidden.
        public void Stop()
        {
            Console.WriteLine("[PlayBackPageModel.Stop()]  current playService's state :" + _playService.State);
            // stop playing audio file
            _playService.Stop();
            // unregister event callbacks
            _playService.VolumeChanged -= _playService_VolumeChanged;
            _playService.UnregisterVolumeChangedCallback();
        }

        /// <summary>
        /// Start to update the remaining time
        /// </summary>
        void StartTimerForRemainingTime()
        {
            PlayWatch.Start();
            // Any background code that needs to update the user interface
            Device.BeginInvokeOnMainThread(() =>
            {
                // interact with UI elements
                Device.StartTimer(new TimeSpan(0, 0, 0, 1, 0), UpdateRemainingPlayTime);
                Device.StartTimer(new TimeSpan(0, 0, 0, 0, 100), UpdateProgressbar);
            });
        }

        void StopTimerForRemainingTime()
        {
            PlayWatch.Stop();
        }

        /// <summary>
        /// Update the value of progressbar at every 100 ms
        /// </summary>
        /// <returns>bool</returns>
        bool UpdateProgressbar()
        {
            if (_playService.State == AudioPlayState.Playing)
            {
                PlayingProcess += play_progressbar_delta;
                //Console.WriteLine("[UpdateRemainingPlayTime]  PlayingProcess " + PlayingProcess);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Update the remaining time at every second
        /// </summary>
        /// <returns>bool</returns>
        bool UpdateRemainingPlayTime()
        {
            //return true to keep the timer running or false to stop it after the current invocation.
            if (_playService.State == AudioPlayState.Playing)
            {
                RemainingTime = RemainingTime - 1000;
                return true;
            }
            else if (_playService.State == AudioPlayState.Idle)
            {
                RemainingTime = 0;
                return false;
            }
            else
            {
                return false;
            }
        }

        Stopwatch PlayWatch;
        IAudioPlayService _playService;
        Record _record;
        bool _Touched;

        /// <summary>
        /// Record to play
        /// </summary>
        public Record Record
        {
            get
            {
                return _record;
            }
        }

        int _remainTime;
        /// <summary>
        /// The remaining time (ms)
        /// </summary>
        public int RemainingTime
        {
            get
            {
                return _remainTime;
            }

            set
            {
                SetProperty(ref _remainTime, value, "RemainingTime");
            }
        }

        double _PlayingProcess;
        public double PlayingProcess
        {
            get
            {
                return _PlayingProcess;
            }

            set
            {
                SetProperty(ref _PlayingProcess, value, "PlayingProcess");
            }
        }

        double MaxVolumeLevel;
        double _VolumeLevelProcess;
        public double VolumeLevelProcess
        {
            get
            {
                return _VolumeLevelProcess;
            }

            set
            {
                SetProperty(ref _VolumeLevelProcess, value, "VolumeLevelProcess");
            }
        }

        int _currentVolume;
        /// <summary>
        /// Volume level
        /// </summary>
        public int CurrentVolume
        {
            get
            {
                return _currentVolume;
            }

            set
            {
                bool changed = SetProperty(ref _currentVolume, value, "CurrentVolume");
                if (changed)
                {
                    // Decide mute mode according to volume level
                    if (CurrentVolume == 0)
                    {
                        Mute = true;
                    }
                    else
                    {
                        Mute = false;
                    }
                }

            }
        }

        bool _volumeViewVisible;
        /// <summary>
        /// Indicate that volume control view is visible or not
        /// </summary>
        public bool VolumeViewVisible
        {
            get
            {
                return _volumeViewVisible;
            }

            set
            {
                SetProperty(ref _volumeViewVisible, value, "VolumeViewVisible");
            }
        }

        bool _mute;
        /// <summary>
        /// indicate that silent mode is on or off
        /// </summary>
        public bool Mute
        {
            get
            {
                return _mute;
            }

            set
            {
                if (SetProperty(ref _mute, value, "Mute"))
                {
                    if (Mute)
                    {
                        // In case that Mute is on
                        _playService.Muted = true;
                        MuteOnOffImage = VOLUME_OFF;
                        MuteOnOffSmallImage = VOLUME_OFF_SMALL_IMAGE;
                        CurrentVolume = 0;
                    }
                    else
                    {
                        // In case that Mute is off
                        _playService.Muted = false;
                        MuteOnOffImage = VOLUME_ON;
                        MuteOnOffSmallImage = VOLUME_ON_SMALL_IMAGE;
                        CurrentVolume = _playService.Volume;
                    }
                }
            }
        }

        string _playcontrolImage;
        public string PlayControlImage
        {
            get
            {
                return _playcontrolImage;
            }

            set
            {
                SetProperty(ref _playcontrolImage, value, "PlayControlImage");
            }
        }

        string _muteOnOffImage;
        /// <summary>
        /// image which show that silent mode is on or not
        /// </summary>
        public string MuteOnOffImage
        {
            get
            {
                return _muteOnOffImage;
            }

            set
            {
                SetProperty(ref _muteOnOffImage, value, "MuteOnOffImage");
            }
        }

        string _muteOnOffSmallImage;
        public string MuteOnOffSmallImage
        {
            get
            {
                return _muteOnOffSmallImage;
            }

            set
            {
                SetProperty(ref _muteOnOffSmallImage, value, "MuteOnOffSmallImage");
            }
        }

        // For changing the visibility of a View which provides a way to control volume level
        public ICommand VolumeViewVisibilityCommand => new Command<bool>(ChangeVolumeViewVisibility);/* { private set; get; }*/
        // To show up VolumeView which provides a way to make the volume up and down
        public ICommand VolumeViewCommand => new Command(ControlVolumeView);
        // For Volume Up/Down
        public ICommand VolumeControlCommand => new Command<VolumeControlType>(ControlVolume);
        // For toggling play / pause functions
        public ICommand PlayControlCommand => new Command(ControlPlay);
        // For Mute On/Off
        public ICommand MuteControlCommand => new Command(ControlMute);
        // Update delay time
        public ICommand DelayTimeCommand => new Command(DelayTime);

        /// <summary>
        /// Make volume level up/down
        /// </summary>
        /// <param name="type">VolumeControlType</param>
        void ControlVolume(VolumeControlType type)
        {
            switch (type)
            {
                case VolumeControlType.Minus:
                    VolumeLevelProcess -= 1 / MaxVolumeLevel;
                    _playService.DecreaseVolume();
                    break;
                case VolumeControlType.Plus:
                    //if (CurrentVolume == 9)
                    //{
                    //    NotifyHearingDamage();
                    //}

                    VolumeLevelProcess += 1 / MaxVolumeLevel;
                    _playService.IncreaseVolume();
                    break;
            }
        }

        void NotifyHearingDamage()
        {
            // TODO
            //MessagingCenter.Send<PlayBackPageModel>(this, MessageKeys.WarnHearingDamange);
        }

        // Toggle Mute On/Off
        void ControlMute()
        {
            Mute = !Mute;
        }

        void ChangeVolumeViewVisibility(bool visible = true)
        {
            VolumeViewVisible = visible;
        }

        void ControlVolumeView()
        {
            Console.WriteLine("[ControlVolumeView] need to show VolumeView.");
            VolumeViewVisible = true;

            // Any background code that needs to update the user interface
            Device.BeginInvokeOnMainThread(() =>
            {
                // interact with UI elements
                Device.StartTimer(new TimeSpan(0, 0, 0, 3, 0), HideVolumeView);
            });
        }

        bool HideVolumeView()
        {
            // In case that screen is touched, VolumeView is still visible.
            if (_Touched)
            {
                Console.WriteLine("In case that screen is touched, VolumeView is still visible.");
                _Touched = false;
                return true;
            }
            else
            {
                // Screen is not touched for last 3 seconds. So VolumeView will be hidden.
                Console.WriteLine("Screen is not touched for last 3 seconds. So VolumeView will be hidden.");
                VolumeViewVisible = false;
                return false;
            }
        }

        void DelayTime()
        {
            Console.WriteLine("  $$$$$$$$$$$$     ");
            Console.WriteLine("  $$$$$$$$$$$$     ");
            Console.WriteLine("          Screen is touched. So delaytime will be updated.");
            Console.WriteLine("  $$$$$$$$$$$$     ");
            Console.WriteLine("  $$$$$$$$$$$$     ");
            _Touched = true;
        }

        void ControlPlay()
        {
            Console.WriteLine(" [ControlPlay]  STATE: " + _playService.State);
            if (_playService.State == AudioPlayState.Playing)
            {
                Console.WriteLine(" [ControlPlay]  Pause");
                _playService.Pause();
                //StopTimerForRemainingTime();
            }
            else if (_playService.State == AudioPlayState.Paused)
            {
                Console.WriteLine(" [ControlPlay]  Start");
                _playService.Start();
                //StartTimerForRemainingTime();
            }
            else if (_playService.State == AudioPlayState.Idle)
            {
                Init(_record);
                _playService.Start();
            }
        }

        private void _playService_VolumeChanged(object sender, AudioVolumeChangedEventArgs e)
        {
            Console.WriteLine("[_playService_VolumeChanged] e.Level : " + e.Level);
            CurrentVolume = e.Level;
        }

        private void _playService_AudioPlayFinished(object sender, System.EventArgs e)
        {
            Console.WriteLine("_playService_AudioPlayFinished()   ");
            MessagingCenter.Send<PlayBackPageModel, bool>(this, MessageKeys.AudioPlayDone, true);
        }

        public void Dispose()
        {
            Console.WriteLine("PlayBackPageModel.Dispose()   _playService.State: " + _playService.State);
            MessagingCenter.Unsubscribe<IAudioPlayService, AudioPlayState>(this, MessageKeys.PlayerStateChanged);
            if (_playService.State == AudioPlayState.Playing)
            {
                _playService.Stop();
            }

            _playService.AudioPlayFinished -= _playService_AudioPlayFinished;
            _playService.VolumeChanged -= _playService_VolumeChanged;
            _playService.Destroy();
        }
    }
}
