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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageGallery.Models
{
    /// <summary>
    /// IContentService interface class.
    /// It defines methods and properties
    /// that should be implemented by ContentService class.
    /// </summary>
    public interface IContentService
    {
        #region properties

        /// <summary>
        /// ContentInserted event.
        /// It notifies about insert operation on the database.
        /// </summary>
        event EventHandler<IContentInsertedArgs> ContentInserted;

        /// <summary>
        /// ContentUpdated event.
        /// It notifies about update operation on the database.
        /// </summary>
        event EventHandler<IContentUpdatedArgs> ContentUpdated;

        /// <summary>
        /// ContentDeleted event.
        /// It notifies about delete operation on the database.
        /// </summary>
        event EventHandler<IContentUpdatedArgs> ContentDeleted;

        /// <summary>
        /// ExceptionOccurrence event.
        /// Notifies about API exception occurrence.
        /// </summary>
        event EventHandler<IExceptionOccurrenceArgs> ExceptionOccurrence;

        #endregion

        #region methods

        /// <summary>
        /// Returns list of TizenImageInfo instances.
        /// </summary>
        /// <returns>List of TizenImageInfo instances.</returns>
        IEnumerable<IImageInfo> FindImageContent();

        /// <summary>
        /// Removes image from device content based on data passed in the IImageInfo instance.
        /// </summary>
        /// <param name="imageInfo">An instance of the IImageInfo class.</param>
        void DeleteImage(IImageInfo imageInfo);

        /// <summary>
        /// Removes images from device content based on data passed in the list of IImageInfo instances.
        /// </summary>
        /// <param name="imagesInfo">List of the IImageInfo instances.</param>
        void DeleteImages(List<IImageInfo> imagesInfo);

        /// <summary>
        /// Updates image specified as parameter.
        /// </summary>
        /// <param name="imageInfo">An instance of the ImageInfo class.</param>
        void UpdateImage(IImageInfo imageInfo);

        /// <summary>
        /// Creates the thumbnail image for the given media id.
        /// Returns created or existing thumbnail path.
        /// </summary>
        /// <param name="id">Media id.</param>
        /// <returns>Task with thumbnail path.</returns>
        Task<string> CreateThumbnail(string id);

        #endregion
    }
}
