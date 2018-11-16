/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
using Camera.Model;
using Xamarin.Forms;

[assembly: Dependency(typeof(IMediaDatabaseService))]

namespace Camera.Model
{
    /// <summary>
    /// Delegate for event CameraServicePhotoTaken.
    /// </summary>
    /// <param name="sender">Instance of the object which invokes event.</param>
    /// <param name="photoPath">Path to the last taken photo.</param>
    /// <param name="photoThumbnail">Last taken photo thumbnail.</param>
    public delegate void CameraServicePhotoTakenDelegate(object sender, string photoPath, Image photoThumbnail);

    /// <summary>
    /// ICameraService interface class.
    /// Defines methods and properties that should be implemented by CameraControl class.
    /// </summary>
    public interface ICameraService
    {
        #region properties

        /// <summary>
        /// CameraServicePhotoTaken event.
        /// Invoked when process of capturing a photo finishes successfully.
        /// </summary>
        event CameraServicePhotoTakenDelegate CameraServicePhotoTaken;

        #endregion

        #region methods

        /// <summary>
        /// Starts process of taking and saving a photo.
        /// </summary>
        void TakePhoto();

        /// <summary>
        /// Deletes photo indicated by given a path.
        /// </summary>
        /// <param name="photoPath">Path to the photo to delete.</param>
        /// <returns>Returns true if file has been deleted, false otherwise.</returns>
        bool DeletePhoto(string photoPath);

        /// <summary>
        /// Starts camera preview.
        /// </summary>
        void StartCameraPreview();

        #endregion
    }
}
