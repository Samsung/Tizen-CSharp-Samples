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
using Xamarin.Forms;

namespace Camera.Model
{
    /// <summary>
    /// CameraModel class.
    /// Provides methods to capture photos and delete the last one
    /// using the Tizen Multimedia API and Tizen Content API.
    /// </summary>
    public class CameraModel
    {
        #region fields

        /// <summary>
        /// Instance of CameraControl class.
        /// </summary>
        private ICameraService _service;

        #endregion

        #region properties

        /// <summary>
        /// CameraServicePhotoTaken event.
        /// Invoked when process of capturing a photo finishes successfully.
        /// </summary>
        public event CameraServicePhotoTakenDelegate CameraServicePhotoTaken;

        #endregion

        #region methods

        /// <summary>
        /// CameraModel class constructor.
        /// </summary>
        public CameraModel()
        {
            _service = DependencyService.Get<ICameraService>();
            _service.CameraServicePhotoTaken += CameraServiceEventHandler;
        }

        /// <summary>
        /// Starts process of taking and saving a photo.
        /// </summary>
        public void TakePhoto()
        {
            _service.TakePhoto();
        }

        /// <summary>
        /// Deletes photo indicated by a given path.
        /// </summary>
        /// <param name="photoPath">Path to the photo to delete.</param>
        /// <returns>Returns true if file has been deleted, false otherwise.</returns>
        public bool DeletePhoto(string photoPath)
        {
            return _service.DeletePhoto(photoPath);
        }

        /// <summary>
        /// Starts camera preview.
        /// </summary>
        public void StartCameraPreview()
        {
            _service.StartCameraPreview();
        }

        /// <summary>
        /// Handles "CameraServicePhotoTaken" of the ICameraService object.
        /// Invokes "CameraServicePhotoTaken" event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="photoPath">Path to the last taken photo.</param>
        /// <param name="photoThumbnail">Last taken photo thumbnail.</param>
        private void CameraServiceEventHandler(object sender, string photoPath, Image photoThumbnail)
        {
            CameraServicePhotoTaken?.Invoke(this, photoPath, photoThumbnail);
        }

        #endregion
    }
}
