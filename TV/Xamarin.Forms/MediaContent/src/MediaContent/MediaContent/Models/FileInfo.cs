/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

using System;

namespace MediaContent.Models
{
    /// <summary>
    /// FileInfo class.
    /// Stores information about a file.
    /// </summary>
    public class FileInfo
    {
        #region properties

        /// <summary>
        /// File's altitude.
        /// </summary>
        public double Altitude { get; set; }

        /// <summary>
        /// File's date of addition.
        /// </summary>
        public DateTimeOffset DateAdded { get; set; }

        /// <summary>
        /// File's date of modification.
        /// </summary>
        public DateTimeOffset DateModified { get; set; }

        /// <summary>
        /// File's description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// File's name to be displayed.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// File's size.
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// File's id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The value indicating whether the file is DRM-protected.
        /// </summary>
        public bool IsDrm { get; set; }

        /// <summary>
        /// The value indicating the favorite status of the file.
        /// </summary>
        public bool IsFavorite { get; set; }

        /// <summary>
        /// File's latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// File's longitude.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// File's media type.
        /// </summary>
        public string MediaType { get; set; }

        /// <summary>
        /// File's mime type.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// File's path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// File's rating.
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// The id of the storage that the file is stored on.
        /// </summary>
        public string StorageId { get; set; }

        /// <summary>
        /// The storage type of the storage that the file is stored on.
        /// </summary>
        public string StorageType { get; set; }

        /// <summary>
        /// File's thumbnail.
        /// </summary>
        public string ThumbnailPath { get; set; }

        /// <summary>
        /// File's timeline.
        /// </summary>
        public DateTimeOffset Timeline { get; set; }

        /// <summary>
        /// File's title.
        /// </summary>
        public string Title { get; set; }

        #endregion
    }
}
