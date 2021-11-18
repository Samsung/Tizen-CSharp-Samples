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
using System.Collections.Generic;
using ImageGallery.Models;
using ImageGallery.Tizen.Mobile.Services;
using Tizen.Content.MediaContent;
using System;
using System.IO;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(ContentService))]
namespace ImageGallery.Tizen.Mobile.Services
{
    /// <summary>
    /// ContentService class.
    /// </summary>
    class ContentService : IContentService
    {
        #region fields

        /// <summary>
        /// An instance of SelectArguments class describing filtering rules for obtained image data.
        /// </summary>
        private readonly SelectArguments _imageSelectArguments;

        /// <summary>
        /// Number of images to be removed.
        /// </summary>
        private int _removedCollectionSize = 0;

        /// <summary>
        /// Flag indicating that some errors occurred while deleting images.
        /// </summary>
        private bool _multipleRemovalException = false;

        /// <summary>
        /// List of ids of images to be removed.
        /// </summary>
        private List<string> _idsOfRemovedImages;

        /// <summary>
        /// An instance of MediaDatabase class, being reference to the media content database.
        /// </summary>
        private MediaDatabase _mediaDatabase;

        /// <summary>
        /// An instance of MediaInfoCommand class, providing methods that allow to perform operations on the database.
        /// </summary>
        private MediaInfoCommand _mediaInfoCommand;

        #endregion

        #region properties

        /// <summary>
        /// ContentInserted event.
        /// Notifies about insert operation on the database.
        /// </summary>
        public event EventHandler<IContentInsertedArgs> ContentInserted;

        /// <summary>
        /// ContentUpdated event.
        /// Notifies about update operation on the database.
        /// </summary>
        public event EventHandler<IContentUpdatedArgs> ContentUpdated;

        /// <summary>
        /// ContentDeleted event.
        /// Notifies about delete operation on the database.
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
        /// ContentService class constructor.
        /// </summary>
        public ContentService()
        {
            _mediaDatabase = new MediaDatabase();
            _mediaInfoCommand = new MediaInfoCommand(_mediaDatabase);

            _imageSelectArguments = new SelectArguments
            {
                SortOrder = MediaInfoColumns.Timeline + " DESC",
                FilterExpression = string.Format("{0} = {1}", MediaInfoColumns.MediaType, (int)MediaType.Image)
            };

            ConnectDatabase();
            RegisterContentUpdateListener();
        }

        /// <summary>
        /// ContentService class destructor.
        /// </summary>
        ~ContentService()
        {
            UnregisterContentUpdateListener();
            DisconnectDatabase();
        }

        /// <summary>
        /// Registers "ContentUpdated" event handler.
        /// </summary>
        private void RegisterContentUpdateListener()
        {
            MediaDatabase.MediaInfoUpdated += OnMediaInfoUpdated;
        }

        /// <summary>
        /// Unregisters "ContentUpdated" event handler.
        /// </summary>
        private void UnregisterContentUpdateListener()
        {
            MediaDatabase.MediaInfoUpdated -= OnMediaInfoUpdated;
        }

        /// <summary>
        /// Handles "MediaInfoUpdated" event of the MediaDatabase object.
        /// Depending on the _removedCollectionSize variable value
        /// it executes NotifyAboutInsertOfSingleImage, NotifyAboutUpdateOfSingleImage
        /// or NotifyAboutRemovalOfMultipleImages method.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">An instance of the MediaInfoUpdatedEventArgs class providing detailed information about the event.</param>
        private void OnMediaInfoUpdated(object sender, MediaInfoUpdatedEventArgs e)
        {
            if (e.OperationType == OperationType.Insert)
            {
                NotifyAboutInsertOfSingleImage(e.Id, e.Path);
            }

            if (_removedCollectionSize == 0)
            {
                NotifyAboutUpdateOfSingleImage(e.OperationType, e.Id);
            }
            else
            {
                NotifyAboutRemovalOfMultipleImages(e.OperationType, e.Id);
            }
        }

        /// <summary>
        /// Notifies about insert of single image.
        /// </summary>
        /// <param name="id">The Id of image, which is inserted.</param>
        /// <param name="path">The Path of image, which is inserted.</param>
        private void NotifyAboutInsertOfSingleImage(string id, string path)
        {
            ContentInserted?.Invoke(this, new ContentInsertedArgs
            {
                ImageInfo = new TizenImageInfo((ImageInfo)_mediaInfoCommand.SelectMedia(id))
            });
        }

        /// <summary>
        /// Notifies about removal of one of multiple images.
        /// Invokes "ExceptionOccurrence" event if images deleting failed.
        /// </summary>
        /// <param name="operationType">Operation type.</param>
        /// <param name="id">The Id of image, which is updated.</param>
        private void NotifyAboutRemovalOfMultipleImages(OperationType operationType, string id)
        {
            if (operationType == OperationType.Delete)
            {
                _removedCollectionSize -= 1;
                _idsOfRemovedImages.Add(id);

                if (_removedCollectionSize == 0)
                {
                    ContentDeleted?.Invoke(this, new ContentUpdatedArgs
                    {
                        IsSingleUpdate = false,
                        ImagesIds = _idsOfRemovedImages
                    });

                    if (_multipleRemovalException)
                    {
                        ExceptionOccurrence?.Invoke(this, new ExceptionOccurrenceArgs
                        {
                            Message = "There were errors when deleting images."
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Notifies about update of single image.
        /// </summary>
        /// <param name="operationType">Operation type.</param>
        /// <param name="id">The Id of image, which is updated.</param>
        private void NotifyAboutUpdateOfSingleImage(OperationType operationType, string id)
        {
            switch (operationType)
            {
                case OperationType.Update:
                    ContentUpdated?.Invoke(this, new ContentUpdatedArgs
                    {
                        ImageId = id
                    });
                    break;
                case OperationType.Delete:
                    ContentDeleted?.Invoke(this, new ContentUpdatedArgs
                    {
                        ImageId = id
                    });
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Sets connection to the content database.
        /// Invokes "ExceptionOccurrence" event if any error occur.
        /// </summary>
        private void ConnectDatabase()
        {
            try
            {
                _mediaDatabase.Connect();
            }
            catch (Exception exception)
            {
                ExceptionOccurrence?.Invoke(this, new ExceptionOccurrenceArgs
                {
                    Message = "Connecting database error: " + exception.Message
                });
            }
        }

        /// <summary>
        /// Closes connection to the content database.
        /// Invokes "ExceptionOccurrence" event if any error occur.
        /// </summary>
        private void DisconnectDatabase()
        {
            try
            {
                _mediaDatabase.Disconnect();
            }
            catch (Exception exception)
            {
                ExceptionOccurrence?.Invoke(this, new ExceptionOccurrenceArgs
                {
                    Message = "Disconnecting database error: " + exception.Message
                });
            }
        }

        /// <summary>
        /// Returns MediaDataReader containing images provided by the Tizen Content API.
        /// </summary>
        /// <returns>An instance of MediaDataReader class.</returns>
        private MediaDataReader<MediaInfo> GetImageDataReader()
        {
            return _mediaInfoCommand.SelectMedia(_imageSelectArguments);
        }

        /// <summary>
        /// Returns list of TizenImageInfo instances.
        /// Invokes "ExceptionOccurrence" event if any error occur.
        /// </summary>
        /// <returns>List of TizenImageInfo instances.</returns>
        public IEnumerable<IImageInfo> FindImageContent()
        {
            List<TizenImageInfo> list = new List<TizenImageInfo>();

            try
            {
                var imageDataReader = GetImageDataReader();

                while (imageDataReader.Read())
                {
                    ImageInfo imageInfo = (ImageInfo)imageDataReader.Current;

                    if (imageInfo != null)
                    {
                        list.Add(new TizenImageInfo(imageInfo));
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionOccurrence?.Invoke(this, new ExceptionOccurrenceArgs
                {
                    Message = "Finding image content error: " + exception.Message
                });
            }

            return list;
        }

        /// <summary>
        /// Removes image from device content based on data passed in the IImageInfo instance.
        /// Invokes "ExceptionOccurrence" event if any error occur.
        /// </summary>
        /// <param name="imageInfo">An instance of the IImageInfo class.</param>
        public void DeleteImage(IImageInfo imageInfo)
        {
            TizenImageInfo nativeImage = imageInfo as TizenImageInfo;

            try
            {
                _mediaInfoCommand.Delete(nativeImage.Id);
                File.Delete(nativeImage.FilePath);
            }
            catch (Exception exception)
            {
                ExceptionOccurrence?.Invoke(this, new ExceptionOccurrenceArgs
                {
                    Message = "Deleting image error: " + exception.Message
                });
            }
        }

        /// <summary>
        /// Removes images from device content based on data passed in the imagesInfo list.
        /// </summary>
        /// <param name="imagesInfo">List of the IImageInfo instances.</param>
        public void DeleteImages(List<IImageInfo> imagesInfo)
        {
            _removedCollectionSize = imagesInfo.Count;
            _multipleRemovalException = false;
            _idsOfRemovedImages = new List<string>();

            foreach (IImageInfo imageInfo in imagesInfo)
            {
                TizenImageInfo nativeImage = imageInfo as TizenImageInfo;

                try
                {
                    _mediaInfoCommand.Delete(nativeImage.Id);
                    File.Delete(nativeImage.FilePath);
                }
                catch (Exception)
                {
                    _multipleRemovalException = true;
                }
            }
        }

        /// <summary>
        /// Updates image specified as parameter.
        /// Invokes "ExceptionOccurrence" event if any error occur.
        /// </summary>
        /// <param name="imageInfo">An instance of the IImageInfo class.</param>
        public void UpdateImage(IImageInfo imageInfo)
        {
            TizenImageInfo nativeImage = imageInfo as TizenImageInfo;

            try
            {
                _mediaInfoCommand.UpdateFavorite(nativeImage.Id, nativeImage.IsFavorite);
            }
            catch (Exception exception)
            {
                ExceptionOccurrence?.Invoke(this, new ExceptionOccurrenceArgs
                {
                    Message = "Updating image error: " + exception.Message
                });
            }
        }

        /// <summary>
        /// Creates the thumbnail image for the given media id.
        /// Returns created or existing thumbnail path.
        /// </summary>
        /// <param name="id">Media id.</param>
        /// <returns>Task with thumbnail path.</returns>
        public async Task<string> CreateThumbnail(string id)
        {
            return await _mediaInfoCommand.CreateThumbnailAsync(id);
        }

        #endregion
    }
}
