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

namespace ImageGallery.Models
{
    /// <summary>
    /// IContentUpdatedArgs interface class.
    /// It defines methods and properties
    /// that should be implemented by class used to notifying about content update.
    /// </summary>
    public interface IContentUpdatedArgs
    {
        #region properties

        /// <summary>
        /// Flag indicating whether update applies single image or multiple images.
        /// </summary>
        bool IsSingleUpdate { get; }

        /// <summary>
        /// Id of the updated image.
        /// </summary>
        string ImageId { get; }

        /// <summary>
        /// List of ids of updated images.
        /// </summary>
        List<string> ImagesIds { get; }

        #endregion
    }
}
