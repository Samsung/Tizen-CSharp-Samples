/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
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

using CircularUIMediaPlayer.Utilities;
using System;
using System.Runtime.InteropServices;
using System.Windows.Input;
using Tizen;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace CircularUIMediaPlayer.ViewModels
{
    public class MainPageModel : BasePageModel
    {
        MediaPlayer player;

        public MainPageModel()
        {
            PlayBtnImage = Utility.PLAY_IMAGE;

        }

        string _playBtnImage;
        public string PlayBtnImage
        {
            get => _playBtnImage;
            set => SetProperty(ref _playBtnImage, value);
        }

        string _mediaSource;
        public string MediaSource
        {
            get => _mediaSource;
            set => SetProperty(ref _mediaSource, value);
        }

        bool _autoPlay;
        public bool AutoPlay
        {
            get => _autoPlay;
            set => SetProperty(ref _autoPlay, value);
        }

        bool _usesEmbeddingControls;
        public bool UsesEmbeddingControls
        {
            get => _usesEmbeddingControls;
            set => SetProperty(ref _usesEmbeddingControls, value);
        }

        bool _playControlViewVisible;
        public bool PlayControlViewVisible
        {
            get => _playControlViewVisible;
            set => SetProperty(ref _playControlViewVisible, value);
        }

        public void SetPlayer(MediaPlayer p)
        {
            player = p;
            player.BufferingStarted += Player_BufferingStarted;
            player.BufferingCompleted += Player_BufferingCompleted;
            player.PlaybackStarted += Player_PlaybackStarted;
            player.PlaybackCompleted += Player_PlaybackCompleted;
            player.PlaybackPaused += Player_PlaybackPaused;
            player.PlaybackStopped += Player_PlaybackStopped;
        }

        public ICommand ChangeVisibilityCommand => new Command(ChangeVisibility);
        public ICommand SetUpPlayerCommand => new Command<string>(SetUp);
        public ICommand PlayOrStopCommand => new Command(PlayOrStop);
        public ICommand BackwardCommand => new Command(BackwardTenSec);
        public ICommand ForwardCommand => new Command(ForwardTenSec);

        void ChangeVisibility()
        {
            if (UsesEmbeddingControls)
            {
                PlayControlViewVisible = false;
                return;
            }

            PlayControlViewVisible = !PlayControlViewVisible;
        }

        void SetUp(string resource)
        {
            if (resource == Utility.EXTERNAL_URL)
            {
                MediaSource = Utility.FileToPlay;
                AutoPlay = false;
                UsesEmbeddingControls = false;
            }
            else
            {
                MediaSource = "galaxy-watch-active.mp4";
                UsesEmbeddingControls = true;
                AutoPlay = true;
            }
        }

        void PlayOrStop()
        {
            try
            {
                if (player.State == PlaybackState.Stopped || player.State == PlaybackState.Paused)
                {
                    MakeScreenOn();
                    player.Start();
                }
                else if (player.State == PlaybackState.Playing)
                {
                    MakeScreenOff();
                    player.Pause();
                }

                ChangeVisibility();
            }
            catch (Exception e)
            {
                Console.WriteLine("  #### MediaControl : " + e.Message);
                DisplayMessageAsync("PlayOrStop", "Error occurred. :" + e.Message);
            }
        }

        /// <summary>
        /// Seek backward 10 seconds.
        /// </summary>
        void BackwardTenSec()
        {
            // 10 sec
            player.Seek(player.Position - 10000);
            ChangeVisibility();
        }

        /// <summary>
        /// Seek forward 10 seconds.
        /// </summary>
        void ForwardTenSec()
        {
            player.Seek(player.Position + 10000);
            ChangeVisibility();
        }

        /// <summary>
        /// Invoked when buffering is completed.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">EventArgs</param>
        private void Player_BufferingCompleted(object sender, EventArgs e)
        {
            //Console.WriteLine("[Player_BufferingCompleted] " + e.ToString());
        }

        /// <summary>
        /// Invoked when buffering is started.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">EventArgs</param>
        private void Player_BufferingStarted(object sender, EventArgs e)
        {
            //Console.WriteLine("[Player_BufferingStarted] " + e.ToString());
        }

        /// <summary>
        /// Invoked when MediaPlayer is stopped
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void Player_PlaybackStopped(object sender, EventArgs e)
        {
            PlayBtnImage = Utility.PLAY_IMAGE;
            MakeScreenOff();
        }

        /// <summary>
        /// Invoked when MediaPlayer is stopped
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">EventArgs</param>
        private void Player_PlaybackPaused(object sender, EventArgs e)
        {
            PlayBtnImage = Utility.PLAY_IMAGE;
            MakeScreenOff();
        }

        /// <summary>
        /// Invoked when playing a video content is done.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">EventArgs</param>
        private void Player_PlaybackCompleted(object sender, EventArgs e)
        {
            PlayBtnImage = Utility.PLAY_IMAGE;
            MakeScreenOff();
            player.Stop();
        }

        /// <summary>
        /// Invoked when playing a video content is started
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">EventArgs</param>
        private void Player_PlaybackStarted(object sender, EventArgs e)
        {
            PlayBtnImage = Utility.PAUSE_IMAGE;
            MakeScreenOn();
        }

        void MakeScreenOn()
        {
            int ret = DevicePowerRequestLock(1, 0); // type : CPU:0, DisplayNormal:1, DisplayDim:2
            //if (ret != 0)
            //{
            //    Exception exp;
            //    switch (ret)
            //    {
            //        case -22:// invalid parameter
            //            exp = new InvalidOperationException("[device_power_request_lock] invalid parameter.");
            //            break;
            //        case -13: // permission denied
            //            exp = new UnauthorizedAccessException("[device_power_request_lock] permission denied.");
            //            break;
            //        case -0x01140000 | 0x01: // operation failed
            //            exp = new InvalidOperationException("[device_power_request_lock] Operation Failed");
            //            break;
            //        default:
            //            exp = new InvalidOperationException("[device_power_request_lock] The unknown error occurs.");
            //            break;
            //    }
            //    throw exp;
            //}
        }

        void MakeScreenOff()
        {
            int ret = DevicePowerReleaseLock(1);    // type : CPU:0, DisplayNormal:1, DisplayDim:2
            //if (ret != 0)
            //{
            //    Exception exp;
            //    switch (ret)
            //    {
            //        case -22:// invalid parameter
            //            exp = new InvalidOperationException("[device_power_release_lock] invalid parameter.");
            //            break;
            //        case -13: // permission denied
            //            exp = new UnauthorizedAccessException("[device_power_release_lock] permission denied.");
            //            break;
            //        case -0x01140000 | 0x01: // operation failed
            //            exp = new InvalidOperationException("[device_power_release_lock] Operation Failed");
            //            break;
            //        default:
            //            exp = new InvalidOperationException("[device_power_release_lock] The unknown error occurs.");
            //            break;
            //    }
            //    throw exp;
            //}
        }

        [DllImport("libcapi-system-device.so.0", EntryPoint = "device_power_request_lock", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int DevicePowerRequestLock(int type, int timeout_ms);

        [DllImport("libcapi-system-device.so.0", EntryPoint = "device_power_release_lock", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int DevicePowerReleaseLock(int type);
    }
}
