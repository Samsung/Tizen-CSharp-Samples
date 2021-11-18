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
using ImageGallery.Controls.ToastView;
using ImageGallery.Models;
using ImageGallery.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace ImageGallery.ViewModels
{
    /// <summary>
    /// MultipleContentDeleted delegate.
    /// Notifies about multiple images delete operation on the database.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    public delegate void MultipleContentDeletedDelegate(object sender);

    /// <summary>
    /// DeleteStateChanged delegate.
    /// Notifies about delete state changes.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    public delegate void DeleteStateChangedDelegate(object sender);

    /// <summary>
    /// ImagesToRemoveUpdated delegate.
    /// Notifies about changes of the dictionary storing images to remove.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    public delegate void ImagesToRemoveUpdatedDelegate(object sender);

    /// <summary>
    /// MainViewModel class.
    /// Provides commands and methods responsible for application view model state.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Backing field of the IsStarted property.
        /// </summary>
        private bool _isStarted = false;

        /// <summary>
        /// Backing field of the IsDeleteState property.
        /// </summary>
        private bool _isDeleteState = false;

        /// <summary>
        /// Backing field of the IsSelectAll property.
        /// </summary>
        private bool _isSelectAll = false;

        /// <summary>
        /// Backing field of the IsDayCollectionEmpty property.
        /// </summary>
        private bool _isDayCollectionEmpty = false;

        /// <summary>
        /// Backing field of the IsImagesToRemoveEmpty property.
        /// </summary>
        private bool _isImagesToRemoveEmpty = true;

        /// <summary>
        /// An instance of current IImageInfo class.
        /// </summary>
        private IImageInfo _currentImageInfo;

        /// <summary>
        /// Backing field of the CurrentImageFilePath property.
        /// </summary>
        private string _currentImageFilePath;

        /// <summary>
        /// Backing field of the CurrentImageFileName property.
        /// </summary>
        private string _currentImageFileName;

        /// <summary>
        /// Backing field of the CurrentImageIsFavorite property.
        /// </summary>
        private bool _currentImageIsFavorite;

        /// <summary>
        /// Backing field of the CurrentImageAddedAt property.
        /// </summary>
        private DateTimeOffset _currentImageAddedAt;

        /// <summary>
        /// Backing field of the DayCollection property.
        /// </summary>
        private ObservableCollection<DayViewModel> _dayCollection;

        /// <summary>
        /// Backing field of the ImagesToRemove property.
        /// </summary>
        private Dictionary<string, ImageViewModel> _imagesToRemove;

        /// <summary>
        /// Reference to the object of the ContentModel class.
        /// </summary>
        private ContentModel _contentModel;

        /// <summary>
        /// Dictionary storing instances of the ImageViewModel class indexed by images ids.
        /// </summary>
        public Dictionary<string, ImageViewModel> _imagesByIds;

        /// <summary>
        /// Collection of images whose ThumbnailPath property is null.
        /// </summary>
        private Collection<ImageViewModel> _imagesWithoutThumbnail;

        /// <summary>
        /// Empty image file path.
        /// </summary>
        private const string EMPTY_IMAGE = "empty_image.png";

        /// <summary>
        /// Reference to class handling navigation over views.
        /// </summary>
        private readonly IViewNavigation _navigation;

        #endregion

        #region properties

        /// <summary>
        /// ContentUpdated event.
        /// Notifies about update operation on the database.
        /// </summary>
        public event EventHandler<IContentUpdatedArgs> ContentUpdated;

        /// <summary>
        /// MultipleContentDeleted delegate.
        /// Notifies about multiple images delete operation on the database.
        /// </summary>
        public event MultipleContentDeletedDelegate MultipleContentDeleted;

        /// <summary>
        /// DeleteStateChanged delegate.
        /// Notifies about delete state changes.
        /// </summary>
        public event DeleteStateChangedDelegate DeleteStateChanged;

        /// <summary>
        /// ImagesToRemoveUpdated delegate.
        /// Notifies about changes of the dictionary storing images to remove.
        /// </summary>
        public event ImagesToRemoveUpdatedDelegate ImagesToRemoveUpdated;

        /// <summary>
        /// Sets a property of the view model indicating that the application is in started state.
        /// </summary>
        public ICommand StartApplicationCommand { private set; get; }

        /// <summary>
        /// Removes images selected on the timeline page.
        /// </summary>
        public ICommand DeleteImagesCommand { private set; get; }

        /// <summary>
        /// Removes image presented currently in details.
        /// </summary>
        public ICommand DeleteImageCommand { private set; get; }

        /// <summary>
        /// Sets delete state.
        /// </summary>
        public ICommand SetDeleteStateCommand { private set; get; }

        /// <summary>
        /// Unsets delete state.
        /// </summary>
        public ICommand UnsetDeleteStateCommand { private set; get; }

        /// <summary>
        /// Toggles IsFavorite state of the current images.
        /// </summary>
        public ICommand ToggleIsFavoriteCommand { private set; get; }

        /// <summary>
        /// Toggles select all checkbox state.
        /// </summary>
        public ICommand ToggleSelectAllCommand { private set; get; }

        /// <summary>
        /// Property indicating whether the DayCollection is empty or not.
        /// </summary>
        public bool IsDayCollectionEmpty
        {
            set => SetProperty(ref _isDayCollectionEmpty, value);
            get => _isDayCollectionEmpty;
        }

        /// <summary>
        /// Property indicating whether the ImagesToRemove dictionary is empty or not.
        /// </summary>
        public bool IsImagesToRemoveEmpty
        {
            set => SetProperty(ref _isImagesToRemoveEmpty, value);
            get => _isImagesToRemoveEmpty;
        }

        /// <summary>
        /// Property indicating if the application is in started state.
        /// </summary>
        public bool IsStarted
        {
            set => SetProperty(ref _isStarted, value);
            get => _isStarted;
        }

        /// <summary>
        /// Property indicating if the application is in delete state.
        /// </summary>
        public bool IsDeleteState
        {
            set
            {
                SetProperty(ref _isDeleteState, value);

                if (!value)
                {
                    ImagesToRemove.Clear();
                    IsImagesToRemoveEmpty = ImagesToRemove.Count == 0;
                    IsSelectAll = false;
                    DeselectAllImages();
                }

                DeleteStateChanged?.Invoke(value);
            }

            get => _isDeleteState;
        }

        /// <summary>
        /// Deselects all images stored in the _imagesByIds dictionary.
        /// </summary>
        private void DeselectAllImages()
        {
            foreach (var item in _imagesByIds.Values)
            {
                item.IsSelected = false;
            }
        }

        /// <summary>
        /// Property indicating if the select all checkbox is checked.
        /// When it is set to false, deselects all images only when all of them are selected.
        /// When it is set to true, selects all images only when not all of them are selected.
        /// </summary>
        public bool IsSelectAll
        {
            set
            {
                SetProperty(ref _isSelectAll, value);

                if (!value && ImagesToRemove.Count == _imagesByIds.Count)
                {
                    SelectImages(false);
                }

                if (value && ImagesToRemove.Count != _imagesByIds.Count)
                {
                    SelectImages(true);
                }
            }

            get => _isSelectAll;
        }

        /// <summary>
        /// Current image file path exists property.
        /// </summary>
        public bool CurrentImageFilePathExists { get; set; }

        /// <summary>
        /// Current image file path property.
        /// </summary>
        public string CurrentImageFilePath
        {
            set => SetProperty(ref _currentImageFilePath, value);
            get => _currentImageFilePath;
        }

        /// <summary>
        /// Current image file name property.
        /// </summary>
        public string CurrentImageFileName
        {
            set => SetProperty(ref _currentImageFileName, value);
            get => _currentImageFileName;
        }

        /// <summary>
        /// Current image is favorite flag property.
        /// </summary>
        public bool CurrentImageIsFavorite
        {
            set => SetProperty(ref _currentImageIsFavorite, value);
            get => _currentImageIsFavorite;
        }

        /// <summary>
        /// Current image added at property.
        /// </summary>
        public DateTimeOffset CurrentImageAddedAt
        {
            set => SetProperty(ref _currentImageAddedAt, value);
            get => _currentImageAddedAt;
        }

        /// <summary>
        /// Observable collection of images grouped by creation date.
        /// </summary>
        public ObservableCollection<DayViewModel> DayCollection
        {
            set
            {
                SetProperty(ref _dayCollection, value);
                IsDayCollectionEmpty = value.Count == 0;
            }

            get => _dayCollection;
        }

        /// <summary>
        /// Dictionary storing images to be removed.
        /// </summary>
        public Dictionary<string, ImageViewModel> ImagesToRemove
        {
            set => _imagesToRemove = value;
            get => _imagesToRemove;
        }

        #endregion

        #region methods

        /// <summary>
        /// MainViewModel class constructor.
        /// Initializes view model's properties and commands.
        /// </summary>
        public MainViewModel()
        {
            _navigation = DependencyService.Get<IViewNavigation>();
            _contentModel = new ContentModel();
            _imagesByIds = new Dictionary<string, ImageViewModel>();

            ImagesToRemove = new Dictionary<string, ImageViewModel>();

            StartApplicationCommand = new Command(ExecuteStartApplicationCommand);

            DeleteImageCommand = new Command(ExecuteDeleteImageCommand);
            DeleteImagesCommand = new Command(ExecuteDeleteImagesCommand);
            SetDeleteStateCommand = new Command(ExecuteSetDeleteStateCommand);
            UnsetDeleteStateCommand = new Command(ExecuteUnsetDeleteStateCommand);

            ToggleIsFavoriteCommand = new Command(ExecuteToggleIsFavoriteCommand);
            ToggleSelectAllCommand = new Command(ExecuteToggleSelectAllCommand);

            _contentModel.ContentInserted += ModelOnContentInserted;
            _contentModel.ContentUpdated += ModelOnContentUpdated;
            _contentModel.ContentDeleted += ModelOnContentDeleted;
            _contentModel.ExceptionOccurrence += ModelOnExceptionOccurrence;

            CreateDayCollection();
        }

        /// <summary>
        /// Obtains images from the system content and groups them by days.
        /// </summary>
        private void CreateDayCollection()
        {
            DayCollection = GroupImagesByDays(_contentModel.FindImageContent());
        }

        /// <summary>
        /// Handles "ExceptionOccurrence" event of the ContentModel object.
        /// Displays toast popup with message given as parameter.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ModelOnExceptionOccurrence(object sender, IExceptionOccurrenceArgs e)
        {
            DependencyService.Get<IToastView>().ShowToastMessage(e.Message);
        }

        /// <summary>
        /// Removes selected images.
        /// </summary>
        private void ExecuteDeleteImagesCommand()
        {
            _contentModel.DeleteImages(ImagesToRemove.
                Select(dict => dict.Value).
                Select(image => image.ImageInfo).ToList());
        }

        /// <summary>
        /// Selects/deselects all images when the application is in delete state.
        /// </summary>
        private void ExecuteToggleSelectAllCommand()
        {
            IsSelectAll = !IsSelectAll;
        }

        /// <summary>
        /// Handles "ContentInserted" event of the ContentModel object.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ModelOnContentInserted(object sender, IContentInsertedArgs e)
        {
            InsertIntoDayCollection(e.ImageInfo);
        }

        /// <summary>
        /// Handles "ContentUpdated" event of the ContentModel object.
        /// Invokes "ContentUpdated" event.
        /// Updates image specified as parameter by using the UpdateIsFavoriteImageProperty method.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ModelOnContentUpdated(object sender, IContentUpdatedArgs e)
        {
            ContentUpdated?.Invoke(this, e);
            UpdateIsFavoriteImageProperty(e.ImageId);
        }

        /// <summary>
        /// Handles "ContentDeleted" event of the ContentModel object.
        /// Depending on the IsSingleUpdate property of event arguments
        /// it executes RemoveImage or RemoveImages method.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ModelOnContentDeleted(object sender, IContentUpdatedArgs e)
        {
            if (e.IsSingleUpdate)
            {
                RemoveImage(e.ImageId);
            }
            else
            {
                RemoveImages(e.ImagesIds);
            }
        }

        /// <summary>
        /// Toggles value of the CurrentImageIsFavorite property.
        /// Updates IsFavorite property of the current image.
        /// Executes UpdateImage method of the ContentModel instance.
        /// </summary>
        private void ExecuteToggleIsFavoriteCommand()
        {
            CurrentImageIsFavorite = !CurrentImageIsFavorite;
            _currentImageInfo.IsFavorite = CurrentImageIsFavorite;
            _contentModel.UpdateImage(_currentImageInfo);
        }

        /// <summary>
        /// Removes image specified as parameter.
        /// Switches application back to previous page.
        /// </summary>
        private void ExecuteDeleteImageCommand()
        {
            _contentModel.DeleteImage(_currentImageInfo);
            _navigation.GoToPreviousView();
        }

        /// <summary>
        /// Activates application's delete state.
        /// </summary>
        private void ExecuteSetDeleteStateCommand()
        {
            IsDeleteState = true;
        }

        /// <summary>
        /// Deactivates application's delete state.
        /// </summary>
        private void ExecuteUnsetDeleteStateCommand()
        {
            IsDeleteState = false;
        }

        /// <summary>
        /// Inserts images into collection grouped by days.
        /// </summary>
        /// <param name="imageInfo">An instance of IImageInfo class.</param>
        private void InsertIntoDayCollection(IImageInfo imageInfo)
        {
            DayViewModel dayInformation = null;

            foreach (DayViewModel day in DayCollection)
            {
                if (day.Date.Year == imageInfo.AddedAt.Year && day.Date.Month == imageInfo.AddedAt.Month &&
                    day.Date.Day == imageInfo.AddedAt.Day)
                {
                    dayInformation = day;
                }
            }

            if (dayInformation == null)
            {
                dayInformation = new DayViewModel()
                {
                    Date = imageInfo.AddedAt
                };

                DayCollection.Add(dayInformation);
                DayCollection = new ObservableCollection<DayViewModel>(DayCollection.OrderByDescending(x => x.Date));
            }

            var newImage = new ImageViewModel(imageInfo)
            {
                AppMainViewModel = this,
                Day = dayInformation
            };

            _imagesByIds.Add(newImage.Id, newImage);
            dayInformation.AddImage(newImage);
        }

        /// <summary>
        /// Returns collection of images grouped by days.
        /// </summary>
        /// <param name="imageItems">Set of IImageInfo objects instances.</param>
        /// <returns>Collection of images grouped by days.</returns>
        private ObservableCollection<DayViewModel> GroupImagesByDays(IEnumerable<IImageInfo> imageItems)
        {
            ObservableCollection<DayViewModel> result = new ObservableCollection<DayViewModel>();

            if (imageItems.Count() == 0)
            {
                return result;
            }

            IImageInfo imageFirst = imageItems.First();
            DateTimeOffset addedAt = imageFirst.AddedAt;

            DayViewModel dayInformation = new DayViewModel()
            {
                Date = addedAt
            };
            result.Add(dayInformation);

            int year = addedAt.Year;
            int month = addedAt.Month;
            int day = addedAt.Day;

            _imagesWithoutThumbnail = new Collection<ImageViewModel>();

            foreach (var image in imageItems)
            {
                addedAt = image.AddedAt;
                if (year != addedAt.Year || month != addedAt.Month || day != addedAt.Day)
                {
                    dayInformation = new DayViewModel()
                    {
                        Date = addedAt
                    };
                    result.Add(dayInformation);

                    year = addedAt.Year;
                    month = addedAt.Month;
                    day = addedAt.Day;
                }

                var newImage = new ImageViewModel(image)
                {
                    AppMainViewModel = this,
                    Day = dayInformation
                };

                if (image.ThumbnailPath == null)
                {
                    newImage.ThumbnailPath = EMPTY_IMAGE;
                    _imagesWithoutThumbnail.Add(newImage);
                }

                _imagesByIds.Add(newImage.Id, newImage);
                dayInformation.AddImage(newImage);
            }

            if (_imagesWithoutThumbnail.Count != 0)
            {
                new Thread(async () =>
                {
                    foreach (ImageViewModel image in _imagesWithoutThumbnail)
                    {
                        try
                        {
                            image.ThumbnailPath = await _contentModel.CreateThumbnail(image.Id);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine("CreateThumbnail Exception: " + exception);
                        }
                    }
                }).Start();
            }

            return result;
        }

        /// <summary>
        /// Sets/unsets image of given string id as favorite.
        /// </summary>
        /// <param name="imageId">Id of image which property is updated.</param>
        private void UpdateIsFavoriteImageProperty(string imageId)
        {
            var image = _imagesByIds[imageId];

            if (image != null)
            {
                image.IsFavorite = !image.IsFavorite;
            }
        }

        /// <summary>
        /// Removes image specified by given string id.
        /// </summary>
        /// <param name="imageId">Id of the removed image.</param>
        private void RemoveImage(string imageId)
        {
            var image = _imagesByIds[imageId];

            if (image != null)
            {
                DayViewModel dayViewModel = image.Day;
                dayViewModel.Images.Remove(image);
                _imagesByIds.Remove(imageId);

                if (dayViewModel.Images.Count == 0)
                {
                    DayCollection.Remove(dayViewModel);
                }

                IsDayCollectionEmpty = DayCollection.Count == 0;
            }
        }

        /// <summary>
        /// Removes images specified by given list of string ids.
        /// </summary>
        /// <param name="imagesIds">List of ids of removed images.</param>
        private void RemoveImages(List<string> imagesIds)
        {
            foreach (string imageId in imagesIds)
            {
                var image = _imagesByIds[imageId];

                if (image != null)
                {
                    DayViewModel dayViewModel = image.Day;
                    dayViewModel.Images.Remove(image);
                    _imagesByIds.Remove(imageId);

                    if (dayViewModel.Images.Count == 0)
                    {
                        DayCollection.Remove(dayViewModel);
                    }
                }
            }

            if (_imagesByIds.Count == 0)
            {
                IsDeleteState = false;
            }

            IsDayCollectionEmpty = DayCollection.Count == 0;

            ImagesToRemove.Clear();
            IsImagesToRemoveEmpty = ImagesToRemove.Count == 0;
            MultipleContentDeleted?.Invoke(null);
        }

        /// <summary>
        /// Handles execution of "StartApplicationCommand" command.
        /// Sets application to started state.
        /// </summary>
        private void ExecuteStartApplicationCommand()
        {
            IsStarted = true;
        }

        /// <summary>
        /// Sets current image data based on given image information parameter.
        /// </summary>
        /// <param name="imageInfo">An instance of the IImageInfo class.</param>
        public void SetCurrentImageData(IImageInfo imageInfo)
        {
            _currentImageInfo = imageInfo;

            if (File.Exists(_currentImageInfo.FilePath))
            {
                CurrentImageFilePath = _currentImageInfo.FilePath;
                CurrentImageFilePathExists = true;
            }
            else
            {
                CurrentImageFilePath = EMPTY_IMAGE;
                CurrentImageFilePathExists = false;
            }

            CurrentImageFileName = _currentImageInfo.DisplayName;
            CurrentImageIsFavorite = _currentImageInfo.IsFavorite;
            CurrentImageAddedAt = _currentImageInfo.AddedAt;
        }

        /// <summary>
        /// Adds image to collection of images for removal.
        /// </summary>
        /// <param name="id">Id of the added image.</param>
        /// <param name="imageViewModel">An instance of the ImageViewModel class.</param>
        public void AddImageToRemoveList(string id, ImageViewModel imageViewModel)
        {
            ImagesToRemove.Add(id, imageViewModel);
            ImagesToRemoveUpdated?.Invoke(ImagesToRemove.Count);
            IsImagesToRemoveEmpty = ImagesToRemove.Count == 0;

            if (ImagesToRemove.Count == _imagesByIds.Count)
            {
                IsSelectAll = true;
            }
        }

        /// <summary>
        /// Removes image from collection of images for removal.
        /// </summary>
        /// <param name="id">Id of the removed image.</param>
        public void RemoveImageFromRemoveList(string id)
        {
            if (ImagesToRemove.ContainsKey(id))
            {
                ImagesToRemove.Remove(id);
                ImagesToRemoveUpdated?.Invoke(ImagesToRemove.Count);
                IsImagesToRemoveEmpty = ImagesToRemove.Count == 0;
            }

            if (ImagesToRemove.Count != _imagesByIds.Count)
            {
                IsSelectAll = false;
            }
        }

        /// <summary>
        /// Selects or deselects all images to remove.
        /// </summary>
        /// <param name="value">Flag indicating whether images should be selected or not.</param>
        private void SelectImages(bool value)
        {
            foreach (var day in DayCollection.ToList())
            {
                foreach (var image in day.Images.ToList())
                {
                    if (image.IsSelected != value)
                    {
                        image.IsSelected = value;
                    }
                }
            }

            ImagesToRemoveUpdated?.Invoke(ImagesToRemove.Count);
        }

        #endregion
    }
}
