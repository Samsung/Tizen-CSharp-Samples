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
using System.Collections.Generic;

namespace ImageGallery.Tizen.Mobile.Services
{
    /// <summary>
    /// ContentUpdatedArgs class.
    /// Defines structure of object being parameter of the ContentUpdated event.
    /// </summary>
    public class ContentUpdatedArgs : EventArgs, IContentUpdatedArgs
    {
        #region properties

        /// <summary>
        /// Flag indicating whether update applies single image or multiple images.
        /// </summary>
        public bool IsSingleUpdate { get; set; }

        /// <summary>
        /// Id of the updated image.
        /// </summary>
        public string ImageId { get; set; }

        /// <summary>
        /// List of IDs of updated images.
        /// </summary>
        public List<string> ImagesIds { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// ContentUpdatedArgs class constructor.
        /// </summary>
        public ContentUpdatedArgs()
        {
            this.IsSingleUpdate = true;
        }

        #endregion
    }
}
