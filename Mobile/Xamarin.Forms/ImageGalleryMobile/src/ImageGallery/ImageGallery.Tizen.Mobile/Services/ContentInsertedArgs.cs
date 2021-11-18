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

namespace ImageGallery.Tizen.Mobile.Services
{
    /// <summary>
    /// ContentInsertedArgs class.
    /// Defines structure of object being parameter of the ContentInserted event.
    /// </summary>
    class ContentInsertedArgs : EventArgs, IContentInsertedArgs
    {
        #region properties

        /// <summary>
        /// Image inserted to the device.
        /// </summary>
        public IImageInfo ImageInfo { get; set; }

        #endregion
    }
}
