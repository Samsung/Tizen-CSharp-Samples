/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using ImageGallery.Models;
using System;
using Tizen.Content.MediaContent;

namespace ImageGallery.Tizen.Mobile.Services
{
    /// <summary>
    /// TizenImageInfo class.
    /// </summary>
    internal class TizenImageInfo : IImageInfo
    {
        #region fields

        /// <summary>
        /// Instance of ImageInfo class.
        /// </summary>
        private ImageInfo _source;

        #endregion

        #region properties

        /// <summary>
        /// Instance of ImageInfo class.
        /// </summary>
        internal ImageInfo Source => _source;

        /// <summary>
        /// Image ID.
        /// </summary>
        public string Id => _source.Id;

        /// <summary>
        /// Image file name.
        /// </summary>
        public string DisplayName => _source.DisplayName;

        /// <summary>
        /// Image file path.
        /// </summary>
        public string FilePath => _source.Path;

        /// <summary>
        /// Image thumbnail path.
        /// </summary>
        public string ThumbnailPath => _source.ThumbnailPath;

        /// <summary>
        /// Addition time of the image.
        /// </summary>
        public DateTimeOffset AddedAt => _source.Timeline;

        /// <summary>
        /// Flag indicating whether image is added to favorites or not.
        /// </summary>
        public bool IsFavorite { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// TizenImageInfo class constructor.
        /// </summary>
        /// <param name="source">An instance of the ImageInfo class.</param>
        internal TizenImageInfo(ImageInfo source)
        {
            this._source = source;
        }

        #endregion
    }
}
