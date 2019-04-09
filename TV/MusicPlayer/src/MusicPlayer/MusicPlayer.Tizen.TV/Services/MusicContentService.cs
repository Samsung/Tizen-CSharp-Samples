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

using MusicPlayer.Models;
using MusicPlayer.Tizen.TV.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tizen;
using Tizen.Content.MediaContent;
using Tizen.Multimedia;
using Tizen.System;

[assembly: Xamarin.Forms.Dependency(typeof(MusicContentService))]
namespace MusicPlayer.Tizen.TV.Services
{
    /// <summary>
    /// Responsible for providing tracks from device.
    /// </summary>
    class MusicContentService : IMusicContentService
    {
        #region fields

        /// <summary>
        /// Sample log tag for Music Player application.
        /// </summary>
        private const string SAMPLE_LOG_TAG = "MusicPlayerSample";

        /// <summary>
        /// Represents the music type in filter query.
        /// </summary>
        private const int TYPE_MUSIC = 3;

        /// <summary>
        /// An instance of MediaDatabase class.
        /// </summary>
        private MediaDatabase _mediaDatabase;

        #endregion

        #region methods

        /// <summary>
        /// MusicContentService class constructor.
        /// </summary>
        public MusicContentService()
        {
            _mediaDatabase = new MediaDatabase();
        }

        /// <summary>
        /// MusicContentService class destructor.
        /// </summary>
        ~MusicContentService()
        {
            _mediaDatabase.Dispose();
        }

        /// <summary>
        /// Gets tracks from device.
        /// </summary>
        /// <returns>A collection of audio tracks.</returns>
        public List<Track> GetTracksFromDevice()
        {
            List<Track> tracklist = new List<Track>();
            try
            {
                _mediaDatabase.Connect();
                var selectArgs = new SelectArguments
                {
                    FilterExpression = "MEDIA_TYPE = " + TYPE_MUSIC
                };

                var mediaInfoCommand = new MediaInfoCommand(_mediaDatabase);
                var selectedMedia = mediaInfoCommand.SelectMedia(selectArgs);
                while (selectedMedia.Read())
                {
                    tracklist.Add(MediaInfoToTrack((AudioInfo)selectedMedia.Current));
                }
            }
            catch (Exception e)
            {
                Log.Error(SAMPLE_LOG_TAG, e.Message);
            }
            finally
            {
                _mediaDatabase.Disconnect();
            }

            return tracklist;
        }

        /// <summary>
        /// Converts 'AudioInfo' object to 'Track' object.
        /// </summary>
        /// <param name="audioInfo">'AudioInfo' object which is to be converted.</param>
        /// <returns>Created 'Track' object.</returns>
        private Track MediaInfoToTrack(AudioInfo audioInfo)
        {
            using (MetadataEditor metadataEditor = new MetadataEditor(audioInfo.Path))
            {
                int pictureCount = metadataEditor.PictureCount;
                byte[] art = null;
                if (pictureCount > 0)
                {
                    Artwork artwork = metadataEditor.GetPicture(0);
                    art = artwork.Data;
                }

                return new Track(audioInfo.Title, audioInfo.Artist, audioInfo.Album, art, audioInfo.Path, audioInfo.Duration);
            }
        }

        #endregion
    }
}
