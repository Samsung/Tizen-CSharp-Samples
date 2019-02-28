//Copyright 2019 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using Xamarin.Forms.Platform.Tizen;
using Tizen.Wearable.CircularUI.Forms;
using MediaToolSample.Models;
using Tizen.Multimedia;

namespace MediaToolSample.ViewModels
{
    /// <summary>
    /// ViewModel class for the Main Page
    /// </summary>
    class PlayPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayPageViewModel"/> class
        /// </summary>
        public PlayPageViewModel()
        {
            // Create player
            player = new Player();

            // Create display
            var VideoWindow = VideoViewController.MainWindowProvider();
            player.Display = new Display(VideoWindow);
            player.DisplaySettings.IsVisible = true;

            streamSource = new MediaStreamSource(VideoDecoderParser.Format);
            configuration = streamSource.VideoConfiguration;

            player.SetSource(streamSource);

            configuration.BufferMaxSize = (ulong)3 * 1024 * 1024;
            configuration.BufferMinThreshold = 50;
            configuration.SeekingOccurred += (s, e) =>
                MediaStreamSeekingOccurred?.Invoke(this, new MediaStreamSeekingOccurredEventArgs(e.Offset));
            configuration.BufferStatusChanged += (s, e) =>
                MediaStreamBufferStatusChanged?.Invoke(this, new MediaStreamBufferStatusChangedEventArgs(e.Status));

            // Add events
            MediaStreamSeekingOccurred += OnMediaStreamSeekingOccurred;
            MediaStreamBufferStatusChanged += OnMediaStreamBufferStatusChanged;
        }

        Player player;
        MediaStreamSource streamSource;
        MediaStreamConfiguration configuration;

        /// <summary>
        /// Provides data for the <see cref="MediaStreamSeekingOccurredEventArgs"/> event.
        /// </summary>
        public class MediaStreamSeekingOccurredEventArgs : EventArgs
        {
            /// <summary>
            /// Initializes a new instance of the MediaStreamSeekingOccurredEventArgs class.
            /// </summary>
            /// <param name="offset">The offset.</param>
            public MediaStreamSeekingOccurredEventArgs(ulong offset)
            {
                Offset = offset;
            }

            /// <summary>
            /// Gets the offset.
            /// </summary>
            public ulong Offset { get; }
        }

        /// <summary>
        /// Provides data for the <see cref="MediaStreamBufferStatusChangedEventArgs"/> event.
        /// </summary>
        public class MediaStreamBufferStatusChangedEventArgs : EventArgs
        {
            /// <summary>
            /// Initializes a new instance of the MediaStreamBufferStatusChangedEventArgs class.
            /// </summary>
            /// <param name="status">The buffer status.</param>
            public MediaStreamBufferStatusChangedEventArgs(MediaStreamBufferStatus status)
            {
                Status = status;
            }

            /// <summary>
            /// Gets the Status.
            /// </summary>
            public MediaStreamBufferStatus Status { get; }
        }

        /// <summary>
        /// Occurs when an seeking occurs.
        /// </summary>
        event EventHandler<MediaStreamSeekingOccurredEventArgs> MediaStreamSeekingOccurred;

        /// <summary>
        /// Occurs when an seeking occurs.
        /// </summary>
        event EventHandler<MediaStreamBufferStatusChangedEventArgs> MediaStreamBufferStatusChanged;

        /// <summary>
        /// Invoked following the offset callback.
        /// </summary>
        /// <param name="sender">Object that sent event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void OnMediaStreamSeekingOccurred(object sender, MediaStreamSeekingOccurredEventArgs e)
        {
            if (e.Offset < 100)
            {
                OffsetText = $"Offset : {e.Offset}%";
            }
            else
            {
                OffsetText = null;
            }

            OnPropertyChanged(nameof(OffsetText));
        }

        public string OffsetText { get; protected set; }

        /// <summary>
        /// Invoked following the offset callback.
        /// </summary>
        /// <param name="sender">Object that sent event.</param>
        /// <param name="e">Arguments of the event.</param>
        private void OnMediaStreamBufferStatusChanged(object sender, MediaStreamBufferStatusChangedEventArgs e)
        {
        }

        /// <summary>
        /// Invoked when this view appears.
        /// </summary>
        public async override void OnAppearing()
        {
            base.OnAppearing();

            await player.PrepareAsync();

            try
            {
                // Set uri to selected one
                new VideoDecoderParser(ResourcePath.GetPath("test.h264")).Feed((packet) =>
                {
                    streamSource.Push(packet);
                    packet.Dispose();
                });
                Toast.DisplayText("Packets are pushed. It will be terminated.");
                OnDisappearing();
            }
            catch (Exception e)
            {
                Toast.DisplayText(e.Message, 1000);
            }
        }

        /// <summary>
        /// Invoked when this view disappears.
        /// </summary>
        public override void OnDisappearing()
        {
            base.OnDisappearing();

            if (player.State >= PlayerState.Ready)
            {
                player.Unprepare();
            }

            MediaStreamSeekingOccurred -= OnMediaStreamSeekingOccurred;
            MediaStreamBufferStatusChanged -= OnMediaStreamBufferStatusChanged;
        }
    }
}
