/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
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

using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PlayerSample
{
    /// <summary>
    /// ViewModel for PlayPage.
    /// </summary>
    partial class PlayPageViewModel
    {
        public ICommand PrepareCommand { get; protected set; }
        public ICommand PlayCommand { get; protected set; }
        public ICommand PauseCommand { get; protected set; }
        public ICommand SeekBackwardCommand { get; protected set; }
        public ICommand SeekForwardCommand { get; protected set; }
        public ICommand SubtitleCommand { get; protected set; }
        public ICommand StreamInfoCommand { get; protected set; }

        private void InitializeCommands()
        {
            PrepareCommand = new Command(async () =>
            {
                try
                {
                    await MediaPlayer.PrepareAsync();

                    // Calculates seek unit based on duration.
                    _seekUnit = Math.Max(1000, MediaPlayer.Duration / 20);
                }
                catch (Exception e)
                {
                    ErrorText = e.Message;
                }
            });

            PlayCommand = new Command(() =>
            {
                if (PlayerState == MediaPlayerState.Ready)
                {
                    MediaPlayer.Start();

                    IsSeekable = true;
                }
                else
                {
                    MediaPlayer.Stop();
                }
            });

            PauseCommand = new Command(async () =>
            {
                if (PlayerState == MediaPlayerState.Playing)
                {
                    MediaPlayer.Pause();
                    _subtitleDelayCancelSource.Cancel();
                }
                else
                {
                    MediaPlayer.Start();
                    await ResumeSubtitleAsync();
                }
            });

            StreamInfoCommand = new Command(() =>
            {
                Properties = Properties == null ? MediaPlayer.GetStreamInfo() : null;
            });

            SubtitleCommand = new NavigationCommand<ListPage>(() => new SubtitleListPageViewModel());

            SeekBackwardCommand = new Command(async () => await Seek(-_seekUnit));
            SeekForwardCommand = new Command(async () => await Seek(_seekUnit));
        }
    }
}
