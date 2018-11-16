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
using Camera.Navigation;
using System;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;

namespace Camera.ViewModel
{
    /// <summary>
    /// Provides commands and methods responsible for application view model state.
    /// </summary>
    public class CameraViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Flag indicating if the confirmation popup is visible.
        /// </summary>
        private bool _isConfirmPopup;

        /// <summary>
        /// Flag indicating if the timer countdown is started.
        /// </summary>
        private bool _isCountdownStarted;

        /// <summary>
        /// Flag indicating if the photo preview is visible.
        /// </summary>
        private bool _isPreviewVisible;

        /// <summary>
        /// Number representing the slider value which indicates timer value.
        /// </summary>
        private double _timerValue;

        /// <summary>
        /// Path to the last taken photo.
        /// </summary>
        private string _photoPath;

        /// <summary>
        /// Name of the last taken photo.
        /// </summary>
        private string _photoName;

        /// <summary>
        /// Backing field of PhotoThumbnail property.
        /// </summary>
        private ImageSource _photoThumbnail;

        /// <summary>
        /// An instance of CameraModel class.
        /// </summary>
        private CameraModel _cameraModel;

        /// <summary>
        /// Stores <see cref="PageNavigation" /> class instance.
        /// </summary>
        private PageNavigation _pageNavigation;

        #endregion

        #region properties

        /// <summary>
        /// Property indicating if the confirmation popup is visible.
        /// </summary>
        public bool IsConfirmPopup
        {
            get { return _isConfirmPopup; }
            set { SetProperty(ref _isConfirmPopup, value); }
        }

        /// <summary>
        /// Property indicating if the timer countdown is started.
        /// </summary>
        public bool IsCountdownStarted
        {
            get { return _isCountdownStarted; }
            set { SetProperty(ref _isCountdownStarted, value); }
        }

        /// <summary>
        /// Property indicating if the photo preview is visible.
        /// </summary>
        public bool IsPreviewVisible
        {
            get { return _isPreviewVisible; }
            set { SetProperty(ref _isPreviewVisible, value); }
        }

        /// <summary>
        /// Property with the number representing the slider value which indicates timer value.
        /// </summary>
        public double TimerValue
        {
            get { return _timerValue; }
            set { SetProperty(ref _timerValue, value); }
        }

        /// <summary>
        /// Property with the value representing path to the last taken photo.
        /// </summary>
        public string PhotoPath
        {
            get { return _photoPath; }
            set { SetProperty(ref _photoPath, value); }
        }

        /// <summary>
        /// Property with the value representing name of the last taken photo.
        /// </summary>
        public string PhotoName
        {
            get { return _photoName; }
            set { SetProperty(ref _photoName, value); }
        }

        /// <summary>
        /// Last taken photo thumbnail.
        /// </summary>
        public ImageSource PhotoThumbnail
        {
            get { return _photoThumbnail; }
            set { SetProperty(ref _photoThumbnail, value); }
        }

        /// <summary>
        /// Navigates to the preview page.
        /// </summary>
        public ICommand NavigateToPreviewPageCommand { get; }

        /// <summary>
        /// Starts timer countdown.
        /// </summary>
        public ICommand StartCountdownCommand { get; }

        /// <summary>
        /// Starts deletion of the photo process.
        /// </summary>
        public ICommand DeletePhotoCommand { get; }

        /// <summary>
        /// Cancels deletion of the photo.
        /// </summary>
        public ICommand CancelDeletePhotoCommand { get; }

        /// <summary>
        /// Confirms deletion of the photo.
        /// </summary>
        public ICommand ConfirmDeletePhotoCommand { get; }

        /// <summary>
        /// Changes page to the previous one.
        /// </summary>
        public ICommand BackToPreviousPageCommand { get; }

        #endregion

        #region methods

        /// <summary>
        /// CameraViewModel class constructor.
        /// </summary>
        /// <param name="pageNavigation">Instance of the PageNavigation class.</param>
        public CameraViewModel(PageNavigation pageNavigation)
        {
            _pageNavigation = pageNavigation;
            NavigateToPreviewPageCommand = new Command(ExecuteNavigateToPreviewPageCommand);
            StartCountdownCommand = new Command(ExecuteStartCountdownCommand);
            DeletePhotoCommand = new Command(ExecuteDeletePhotoCommand);
            CancelDeletePhotoCommand = new Command(ExecuteCancelDeletePhotoCommand);
            ConfirmDeletePhotoCommand = new Command(ExecuteConfirmDeletePhotoCommand);
            BackToPreviousPageCommand = new Command(ExecuteBackToPreviousPageCommand);
            _cameraModel = new CameraModel();
            _cameraModel.CameraServicePhotoTaken += OnPhotoTaken;
        }

        /// <summary>
        /// Starts camera preview.
        /// </summary>
        public void StartCameraPreview()
        {
            _cameraModel.StartCameraPreview();
        }

        /// <summary>
        /// Handles execution of NavigateToPreviewPageCommand.
        /// </summary>
        private void ExecuteNavigateToPreviewPageCommand()
        {
            _pageNavigation.NavigateToPreviewPage();
        }

        /// <summary>
        /// Handles execution of StartCountdownCommand.
        /// Updates value of the IsCountdownStarted property.
        /// Updates value of the IsPreviewVisible property.
        /// Starts timer countdown and updates TimerValue property.
        /// When countdown is finished, starts process of taking photo.
        /// </summary>
        private void ExecuteStartCountdownCommand()
        {
            IsCountdownStarted = !IsCountdownStarted;

            if (IsCountdownStarted)
            {
                IsPreviewVisible = false;
            }
            else
            {
                if (PhotoPath != null)
                {
                    IsPreviewVisible = true;
                }
            }

            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                if (IsCountdownStarted)
                {
                    if (TimerValue > 0)
                    {
                        TimerValue -= 0.01;
                    }
                    else
                    {
                        _cameraModel.TakePhoto();
                        TimerValue = 0;
                        IsCountdownStarted = false;
                    }
                }

                return IsCountdownStarted;
            });
        }

        /// <summary>
        /// Handles "CameraServicePhotoTaken" event of the CameraModel object.
        /// Updates value of the PhotoThumbnail property.
        /// Updates value of the PhotoPath property.
        /// Updates value of the PhotoName property.
        /// Updates value of the IsPreviewVisible property.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="photoPath">Path to the last taken photo.</param>
        /// <param name="photoThumbnail">Last taken photo thumbnail.</param>
        private void OnPhotoTaken(object sender, string photoPath, Image photoThumbnail)
        {
            PhotoThumbnail = photoThumbnail.Source;
            PhotoPath = photoPath;
            PhotoName = Path.GetFileName(PhotoPath);
            IsPreviewVisible = true;
        }

        /// <summary>
        /// Handles execution of DeletePhotoCommand.
        /// Updates value of the IsConfirmPopup property.
        /// </summary>
        private void ExecuteDeletePhotoCommand()
        {
            IsConfirmPopup = true;
        }

        /// <summary>
        /// Handles execution of CancelDeletePhotoCommand.
        /// Updates value of the IsConfirmPopup property.
        /// </summary>
        private void ExecuteCancelDeletePhotoCommand()
        {
            IsConfirmPopup = false;
        }

        /// <summary>
        /// Handles execution of ConfirmDeletePhotoCommand.
        /// Updates value of the IsConfirmPopup property.
        /// Deletes photo indicated by "PhotoPath".
        /// Checks if any error has occurred while deleting a photo.
        /// Navigates to the previous page.
        /// </summary>
        private void ExecuteConfirmDeletePhotoCommand()
        {
            IsConfirmPopup = false;
            if (_cameraModel.DeletePhoto(PhotoPath))
            {
                PhotoPath = null;
                IsPreviewVisible = false;
                _pageNavigation.NavigateToPreviousPage();
            }
        }

        /// <summary>
        /// Handles execution of BackToPreviousPageCommand.
        /// Navigates to the previous page.
        /// </summary>
        private void ExecuteBackToPreviousPageCommand()
        {
            IsConfirmPopup = false;
            _pageNavigation.NavigateToPreviousPage();
        }

        #endregion
    }
}
