/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace MusicPlayer.Models
{
    /// <summary>
    /// Class that holds information about track.
    /// </summary>
    public class Track
    {
        #region properties

        /// <summary>
        /// Title of the track.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Represents the artist name.
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// Represents the album name.
        /// </summary>
        public string Album { get; set; }

        /// <summary>
        /// Represents the track artwork.
        /// </summary>
        public byte[] Artwork { get; set; }

        /// <summary>
        /// Path to the track.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Duration of the track (in milliseconds).
        /// </summary>
        public int Duration { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes Track class instance.
        /// </summary>
        /// <param name="title">Title of the track.</param>
        /// <param name="artist">Name of the artist.</param>
        /// <param name="album">Title of the album.</param>
        /// <param name="artwork">Artwork of the track.</param>
        /// <param name="path">Path to the track.</param>
        /// <param name="duration">Duration of the track (in milliseconds).</param>
        public Track(string title, string artist, string album, byte[] artwork, string path, int duration)
        {
            Title = title;
            Artist = artist;
            Album = album;
            Artwork = artwork;
            Path = path;
            Duration = duration;
        }

        #endregion
    }
}
