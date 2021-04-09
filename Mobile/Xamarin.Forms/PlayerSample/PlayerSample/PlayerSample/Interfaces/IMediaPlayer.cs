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
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlayerSample
{
    /// <summary>
    /// Provides data for the <see cref="IMediaPlayer.SubtitleUpdated"/> event.
    /// </summary>
    public class SubtitleUpdatedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the SubtitleUpdatedEventArgs class.
        /// </summary>
        /// <param name="subtitle">The subtitle text.</param>
        /// <param name="duration">The duration.</param>
        public SubtitleUpdatedEventArgs(string subtitle, uint duration)
        {
            Subtitle = subtitle;
            Duration = duration;
        }

        /// <summary>
        /// Gets the subtitle.
        /// </summary>
        public string Subtitle { get; }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        public uint Duration { get; }
    }

    /// <summary>
    /// Provides data for the <see cref="IMediaPlayer.ErrorOccurred"/> event.
    /// </summary>
    public class ErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ErrorEventArgs class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ErrorEventArgs(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; }
    }

    /// <summary>
    /// Provides data for the <see cref="IMediaPlayer.Buffering"/> event.
    /// </summary>
    public class BufferingEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the BufferingEventArgs class.
        /// </summary>
        /// <param name="percent">The buffering percentage.</param>
        public BufferingEventArgs(int percent)
        {
            Percent = percent;
        }

        /// <summary>
        /// Gets the buffering percentage.
        /// </summary>
        public int Percent { get; }
    }

    /// <summary>
    /// Interface for the player.
    /// </summary>
    public interface IMediaPlayer
    {
        /// <summary>
        /// Prepares the player, asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous prepare operation.</returns>
        Task PrepareAsync();

        /// <summary>
        /// Unprepares the player.
        /// </summary>
        void Unprepare();

        /// <summary>
        /// Prepares the player.
        /// </summary>
        /// <param name="uri">Uri.</param>
        void SetSource(string uri);

        /// <summary>
        /// Starts or resumes playback.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops playing.
        /// </summary>
        void Stop();

        /// <summary>
        /// Pauses the player.
        /// </summary>
        void Pause();

        /// <summary>
        /// Seeks for playback, asynchronously.
        /// </summary>
        /// <param name="offset">The offset to seek.</param>
        /// <returns>A task that represents the asynchronous seek operation.</returns>
        Task SeekAsync(int offset);

        /// <summary>
        /// Sets the subtitle file.
        /// </summary>
        /// <param name="path">Subtitle path.</param>
        void SetSubtile(string path);

        /// <summary>
        /// Sets the offset for subtitle.
        /// </summary>
        /// <param name="offset">Subtitle offset in milliseconds.</param>
        void SetSubtitleOffset(int offset);

        /// <summary>
        /// Retrieves the stream info of media.
        /// </summary>
        /// <returns>Stream info.</returns>
        IEnumerable<Property> GetStreamInfo();

        /// <summary>
        /// Set display view for player.
        /// </summary>
        void SetDisplay(object nativeView);

        /// <summary>
        /// Gets the state of player.
        /// </summary>
        MediaPlayerState State { get; }

        /// <summary>
        /// Gets the current position.
        /// </summary>
        int Position { get; }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        int Duration { get; }

        /// <summary>
        /// Occurs when the state is changed.
        /// </summary>
        event EventHandler<EventArgs> StateChanged;

        /// <summary>
        /// Occurs when the subtitle is updated.
        /// </summary>
        event EventHandler<SubtitleUpdatedEventArgs> SubtitleUpdated;

        /// <summary>
        /// Occurs when an error occurs.
        /// </summary>
        event EventHandler<ErrorEventArgs> ErrorOccurred;

        /// <summary>
        /// Occurs when the buffering status of streaming is changed.
        /// </summary>
        event EventHandler<BufferingEventArgs> Buffering;
    }
}
