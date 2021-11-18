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
using Xamarin.Forms;

namespace ImageGallery.Models
{
    /// <summary>
    /// ContentModel class.
    /// Provides methods that allow the application to use the Tizen Content API.
    /// </summary>
    public class ContentModel
    {
        #region fields

        /// <summary>
        /// An instance of IContentService interface.
        /// </summary>
        private IContentService _iContentService;

        #endregion

        #region properties

        /// <summary>
        /// ContentInserted event.
        /// It notifies about insert operation on the database.
        /// </summary>
        public event EventHandler<IContentInsertedArgs> ContentInserted;

        /// <summary>
        /// ContentUpdated event.
        /// It notifies about update operation on the database.
        /// </summary>
        public event EventHandler<IContentUpdatedArgs> ContentUpdated;

        /// <summary>
        /// ContentDeleted event.
        /// It notifies about delete operation on the database.
        /// </summary>
        public event EventHandler<IContentUpdatedArgs> ContentDeleted;

        /// <summary>
        /// ExceptionOccurrence event.
        /// Notifies about API exception occurrence.
        /// </summary>
        public event EventHandler<IExceptionOccurrenceArgs> ExceptionOccurrence;

        #endregion

        #region methods

        /// <summary>
        /// ContentModel class constructor.
        /// Attaches handlers of ContentUpdated and ContentDeleted events of the ContentService class.
        /// </summary>
        public ContentModel()
        {
            _iContentService = DependencyService.Get<IContentService>();

            _iContentService.ContentInserted += ServiceOnContentInserted;
            _iContentService.ContentUpdated += ServiceOnContentUpdated;
            _iContentService.ContentDeleted += ServiceOnContentDeleted;
            _iContentService.ExceptionOccurrence += ServiceOnExceptionOccurrence;
        }

        /// <summary>
        /// Handles "ExceptionOccurrence" of the IContentService object.
        /// Invokes "ExceptionOccurrence" to other application's modules.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">An instance of the ExceptionOccurrenceArgs class providing detailed information about the event.</param>
        private void ServiceOnExceptionOccurrence(object sender, IExceptionOccurrenceArgs e)
        {
            ExceptionOccurrence?.Invoke(this, e);
        }

        /// <summary>
        /// Handles "ContentInserted" of the IContentService object.
        /// Invokes "ContentInserted" to other application's modules.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">An instance of the ContentUpdatedArgs class providing detailed information about the event.</param>
        private void ServiceOnContentInserted(object sender, IContentInsertedArgs e)
        {
            ContentInserted?.Invoke(this, e);
        }

        /// <summary>
        /// Handles "ContentUpdated" of the IContentService object.
        /// Invokes "ContentUpdated" to other application's modules.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">An instance of the ContentUpdatedArgs class providing detailed information about the event.</param>
        private void ServiceOnContentUpdated(object sender, IContentUpdatedArgs e)
        {
            ContentUpdated?.Invoke(this, e);
        }

        /// <summary>
        /// Handles "ContentDeleted" of the IContentService object.
        /// Invokes "ContentDeleted" to other application's modules.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">An instance of the ContentUpdatedArgs class providing detailed information about the event.</param>
        private void ServiceOnContentDeleted(object sender, IContentUpdatedArgs e)
        {
            ContentDeleted?.Invoke(this, e);
        }

        /// <summary>
        /// Returns list of TizenImageInfo instances.
        /// </summary>
        /// <returns>List of TizenImageInfo instances.</returns>
        public IEnumerable<IImageInfo> FindImageContent()
        {
            return _iContentService.FindImageContent();
        }

        /// <summary>
        /// Removes image specified as parameter from device content.
        /// </summary>
        /// <param name="imageInfo">An instance of the IImageInfo class.</param>
        public void DeleteImage(IImageInfo imageInfo)
        {
            _iContentService.DeleteImage(imageInfo);
        }

        /// <summary>
        /// Removes images specified as parameter from device content.
        /// </summary>
        /// <param name="imagesInfo">List of the IImageInfo instances.</param>
        public void DeleteImages(List<IImageInfo> imagesInfo)
        {
            _iContentService.DeleteImages(imagesInfo);
        }

        /// <summary>
        /// Updates image specified as parameter.
        /// </summary>
        /// <param name="imageInfo">An instance of the IImageInfo class.</param>
        public void UpdateImage(IImageInfo imageInfo)
        {
            _iContentService.UpdateImage(imageInfo);
        }

        /// <summary>
        /// Creates the thumbnail image for the given media id.
        /// Returns created or existing thumbnail path.
        /// </summary>
        /// <param name="id">Media id.</param>
        /// <returns>Task with thumbnail path.</returns>
        public Task<string> CreateThumbnail(string id)
        {
            return _iContentService.CreateThumbnail(id);
        }

        #endregion
    }
}
