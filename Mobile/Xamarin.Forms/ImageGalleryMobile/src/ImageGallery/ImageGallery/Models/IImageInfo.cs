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
using System;

namespace ImageGallery.Models
{
    /// <summary>
    /// IImageInfo interface class.
    /// It defines methods and properties that should be implemented by class
    /// used to store image information.
    /// </summary>
    public interface IImageInfo
    {
        #region properties

        /// <summary>
        /// Image ID.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Image file name.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Image file path.
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// Image thumbnail path.
        /// </summary>
        string ThumbnailPath { get; }

        /// <summary>
        /// Addition time of the image.
        /// </summary>
        DateTimeOffset AddedAt { get; }

        /// <summary>
        /// Flag indicating whether image is added to favorites or not.
        /// </summary>
        bool IsFavorite { get; set; }

        #endregion
    }
}
