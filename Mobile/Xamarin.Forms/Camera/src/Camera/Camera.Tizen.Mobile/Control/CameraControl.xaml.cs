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
using Camera.Tizen.Mobile.Control;
using System;
using System.IO;
using System.Linq;
using Camera.Utils;
using TCamera = Tizen.Multimedia.Camera;
using Xamarin.Forms;
using TMediaView = Tizen.Multimedia.MediaView;
using Tizen.Multimedia;

[assembly: Dependency(typeof(CameraControl))]

namespace Camera.Tizen.Mobile.Control
{
    /// <summary>
    /// CameraControl class.
    /// Shows camera preview and provides methods to capture photos and delete the last one
    /// using the Tizen Multimedia API and Tizen Content API.
    /// Implements ICameraService interface from portable part of the application source code.
    /// </summary>
    public partial class CameraControl : ContentView, ICameraService
    {
        #region fields

        /// <summary>
        /// Aspect ratio 9:16 coefficient.
        /// </summary>
        private const double ASPECT_RATIO_COEFFICIENT = 9d / 16;

        /// <summary>
        /// Captured photos' location template.
        /// </summary>
        private const string PHOTO_PATH_TEMPLATE = "/opt/usr/home/owner/media/DCIM/Camera/{0}";

        /// <summary>
        /// Camera class instance.
        /// </summary>
        private TCamera _camera;

        /// <summary>
        /// View showing camera preview.
        /// </summary>
        private TMediaView _mediaView;

        /// <summary>
        /// Captured photo instance.
        /// </summary>
        private StillImage _capturedPhoto;

        /// <summary>
        /// Path to the last taken photo.
        /// </summary>
        private string _photoPath;

        /// <summary>
        /// Last taken photo thumbnail.
        /// </summary>
        private Image _photoThumbnail;

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
        /// Initializes CameraControl class instance.
        /// </summary>
        public CameraControl()
        {
            InitializeComponent();
            InitCameraControl();
        }

        /// <summary>
        /// Initializes camera preview.
        /// </summary>
        public void InitCameraControl()
        {
            CameraView.NativeViewCreated += (s, e) =>
            {
                _mediaView = (TMediaView)CameraView.NativeView;
                _camera = new TCamera(CameraDevice.Rear);
                _camera.Display = new Display(_mediaView);

                try
                {
                    SetCaptureResolution();
                    _camera.DisplaySettings.Mode = CameraDisplayMode.CroppedFull;
                    _camera.Settings.CapturePixelFormat = CameraPixelFormat.Jpeg;
                    _camera.Settings.EnableTag = true;
                    _camera.Settings.ImageQuality = 100;
                    _camera.Settings.OrientationTag = CameraTagOrientation.RightTop;
                    _camera.Capturing += OnCapturing;
                    _camera.CaptureCompleted += OnCaptureCompleted;
                    StartCameraPreview();
                    SetFocus();
                }
                catch (Exception exception)
                {
                    ErrorHandler(exception.Message);
                }
            };
        }

        /// <summary>
        /// Handles Capturing event.
        /// Stores information about taken photo.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="cameraCapturingEventArgs">Photo's details.</param>
        private void OnCapturing(object sender, CameraCapturingEventArgs cameraCapturingEventArgs)
        {
            _capturedPhoto = cameraCapturingEventArgs.MainImage;
            SetThumbnail(cameraCapturingEventArgs);
        }

        /// <summary>
        /// Handles CaptureCompleted event.
        /// Creates directory where photos are saved (if does not exist).
        /// Saves photo in device memory.
        /// Updates Media Database.
        /// Invokes "CameraServicePhotoTaken" event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Contains event data.</param>
        private void OnCaptureCompleted(object sender, EventArgs e)
        {
            bool errorOccurred = false;
            _photoPath = string.Format(PHOTO_PATH_TEMPLATE, DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".jpg");
            var memoryStream = new MemoryStream(_capturedPhoto.Data);

            try
            {
                Directory.CreateDirectory(string.Format(PHOTO_PATH_TEMPLATE, ""));
                FileStream fileStream = File.Create(_photoPath);
                memoryStream.WriteTo(fileStream);
                fileStream.Dispose();
            }
            catch (Exception exception)
            {
                ErrorHandler("Saving photo error: " + exception.Message);
                errorOccurred = true;
            }

            if (!errorOccurred)
            {
                UpdateDatabase();
                CameraServicePhotoTaken?.Invoke(this, _photoPath, _photoThumbnail);
            }

            StartCameraPreview();
        }

        /// <summary>
        /// If an aspect ratio 9:16 of capture resolution is supported, sets the largest resolution with such aspect ratio.
        /// If not, sets the largest capture resolution supported by camera.
        /// </summary>
        private void SetCaptureResolution()
        {
            try
            {
                var supportedResolution = _camera.Capabilities.SupportedCaptureResolutions
                    .LastOrDefault(p => p.Width * ASPECT_RATIO_COEFFICIENT == p.Height);

                if (supportedResolution.Width == 0)
                {
                    _camera.Settings.CaptureResolution = _camera.Capabilities.SupportedCaptureResolutions
                        .LastOrDefault();
                }
                else
                {
                    _camera.Settings.CaptureResolution = supportedResolution;
                }
            }
            catch (Exception exception)
            {
                ErrorHandler("Setting capture resolution error: " + exception.Message);
            }
        }

        /// <summary>
        /// Checks whether the autofocus is supported by camera or not.
        /// If the autofocus is supported, starts focusing.
        /// </summary>
        private void SetFocus()
        {
            try
            {
                if (_camera.Capabilities.SupportedAutoFocusModes.Contains(CameraAutoFocusMode.Normal))
                {
                    _camera.StartFocusing(true);
                }
            }
            catch (Exception exception)
            {
                ErrorHandler("Focusing error: " + exception.Message);
            }
        }

        /// <summary>
        /// Sets thumbnail of the last taken photo.
        /// </summary>
        /// <param name="cameraCapturingEventArgs">Photo's details.</param>
        private void SetThumbnail(CameraCapturingEventArgs cameraCapturingEventArgs)
        {
            try
            {
                _photoThumbnail = new Image();

                if (cameraCapturingEventArgs.Thumbnail is null)
                {
                    _photoThumbnail.Source = ImageSource.FromStream(() =>
                    new MemoryStream(cameraCapturingEventArgs.MainImage.Data));
                }
                else
                {
                    _photoThumbnail.Source = ImageSource.FromStream(() =>
                    new MemoryStream(cameraCapturingEventArgs.Thumbnail.Data));
                }
            }
            catch (Exception exception)
            {
                ErrorHandler("Error while setting thumbnail: " + exception.Message);
            }
        }

        /// <summary>
        /// If camera is in a right state, starts a preview.
        /// </summary>
        public void StartCameraPreview()
        {
            if (!(_camera is null))
            {
                if (_camera.State == CameraState.Created || _camera.State == CameraState.Captured)
                {
                    try
                    {
                        _camera.StartPreview();
                    }
                    catch (Exception exception)
                    {
                        ErrorHandler("Preview error: " + exception.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Updates media database.
        /// </summary>
        private void UpdateDatabase()
        {
            try
            {
                using (var databaseConnector = new DatabaseConnector())
                {
                    databaseConnector.ErrorOccurred += OnDatabaseError;
                    databaseConnector.UpdateDatabase(string.Format(PHOTO_PATH_TEMPLATE, ""));
                }
            }
            catch (Exception exception)
            {
                ErrorHandler("Updating DB error: " + exception.Message);
            }
        }

        /// <summary>
        /// Starts camera capture.
        /// </summary>
        public void TakePhoto()
        {
            try
            {
                _camera.StartCapture();
            }
            catch (Exception exception)
            {
                ErrorHandler("Taking photo error: " + exception.Message);
            }
        }

        /// <summary>
        /// Deletes a photo indicated by a given path.
        /// Updates Media Database.
        /// </summary>
        /// <param name="photoPath">Path to the photo to delete.</param>
        /// <returns>Returns true if file has been deleted, false otherwise.</returns>
        public bool DeletePhoto(string photoPath)
        {
            try
            {
                File.Delete(photoPath);
                UpdateDatabase();
            }
            catch (Exception exception)
            {
                ErrorHandler("Deleting photo error: " + exception.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Notifies user about the error.
        /// </summary>
        /// <param name="errorMessage">Message of a thrown error.</param>
        private void ErrorHandler(string errorMessage)
        {
            Toast.DisplayText(errorMessage);
        }

        /// <summary>
        /// Handles "ErrorOccurred" when updating database.
        /// Throws an exception with error message.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="errorMessage">Exception with message of a thrown error.</param>
        private void OnDatabaseError(object sender, string errorMessage)
        {
            throw new Exception(errorMessage);
        }

        #endregion
    }
}
