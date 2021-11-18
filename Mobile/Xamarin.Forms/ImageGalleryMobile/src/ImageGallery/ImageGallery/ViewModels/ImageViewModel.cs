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
using ImageGallery.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ImageGallery.ViewModels
{
    /// <summary>
    /// ImageViewModel class.
    /// </summary>
    public class ImageViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Backing field of the IsFavorite property.
        /// </summary>
        private bool _isFavorite;

        /// <summary>
        /// Backing field of the IsSelected property.
        /// </summary>
        private bool _isSelected;

        /// <summary>
        /// Backing field of the ThumbnailPath property.
        /// </summary>
        private string _thumbnailPath;

        /// <summary>
        /// Reference to class handling navigation over views.
        /// </summary>
        private readonly IViewNavigation _navigation;

        #endregion

        #region properties

        /// <summary>
        /// An instance of MainViewModel class.
        /// </summary>
        public MainViewModel AppMainViewModel { set; get; }

        /// <summary>
        /// Displays details page.
        /// </summary>
        public ICommand ShowDetailsCommand { private set; get; }

        /// <summary>
        /// IImageInfo instance.
        /// </summary>
        public IImageInfo ImageInfo { private set; get; }

        /// <summary>
        /// Day property specifying the day the image is grouped to.
        /// </summary>
        public DayViewModel Day { set; get; }

        /// <summary>
        /// Image ID.
        /// </summary>
        public string Id { private set; get; }

        /// <summary>
        /// Image file name.
        /// </summary>
        public string DisplayName { private set; get; }

        /// <summary>
        /// Image file path.
        /// </summary>
        public string FilePath { private set; get; }

        /// <summary>
        /// Addition time of the image.
        /// </summary>
        public DateTimeOffset AddedAt { private set; get; }

        /// <summary>
        /// Flag indicating whether image is added to favorites or not.
        /// </summary>
        public bool IsFavorite
        {
            set => SetProperty(ref _isFavorite, value);
            get => _isFavorite;
        }

        /// <summary>
        /// Flag indicating whether image is added to favorites or not.
        /// </summary>
        public bool IsSelected
        {
            set
            {
                if (!SetProperty(ref _isSelected, value))
                {
                    return;
                };

                if (_isSelected)
                {
                    AppMainViewModel?.AddImageToRemoveList(Id, this);
                }
                else
                {
                    AppMainViewModel?.RemoveImageFromRemoveList(Id);
                }
            }

            get => _isSelected;
        }

        /// <summary>
        /// Image thumbnail path.
        /// </summary>
        public string ThumbnailPath
        {
            set => SetProperty(ref _thumbnailPath, value);
            get => _thumbnailPath;
        }

        #endregion

        #region methods

        /// <summary>
        /// ImageViewModel class constructor.
        /// Initializes provided commands.
        /// </summary>
        /// <param name="imageInfo">An instance of the IImageInfo class.</param>
        public ImageViewModel(IImageInfo imageInfo)
        {
            _navigation = DependencyService.Get<IViewNavigation>();

            ImageInfo = imageInfo;
            Id = imageInfo.Id;
            DisplayName = imageInfo.DisplayName;
            FilePath = imageInfo.FilePath;
            ThumbnailPath = imageInfo.ThumbnailPath;
            AddedAt = imageInfo.AddedAt;
            IsFavorite = imageInfo.IsFavorite;

            ShowDetailsCommand = new Command(ExecuteShowDetailsCommand, CanExecuteShowDetailsCommand);
        }

        /// <summary>
        /// Returns true if the ToggleIsSelectedCommand can be executed, false otherwise.
        /// </summary>
        /// <returns>Value representing current application's delete state.</returns>
        private bool CanExecuteToggleIsSelectedCommand()
        {
            return AppMainViewModel.IsDeleteState;
        }

        /// <summary>
        /// Returns true if the ShowDetailsCommand can be executed, false otherwise.
        /// </summary>
        /// <param name="obj">An Object used as parameter to determine if the Command can be executed.</param>
        /// <returns>Negated value of the current application's delete state.</returns>
        private bool CanExecuteShowDetailsCommand(object obj)
        {
            return !AppMainViewModel.IsDeleteState;
        }

        /// <summary>
        /// Saves current image data in the main view model
        /// and navigates to page specified as a parameter.
        /// </summary>
        /// <param name="obj">Command parameter.</param>
        private void ExecuteShowDetailsCommand(object obj)
        {
            AppMainViewModel.SetCurrentImageData(ImageInfo);
            _navigation.GoToDetailsView();
        }

        #endregion
    }
}
