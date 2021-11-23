/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

using NotificationManager.Models;
using NotificationManager.Navigation;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace NotificationManager.ViewModels
{
    /// <summary>
    /// NotificationManagerViewModel class.
    /// It implements INotifyPropertyChanged interface.
    /// Provides commands and methods responsible for application view model state.
    /// </summary>
    public class NotificationManagerViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Add image message.
        /// </summary>
        private const string ADD_IMAGE_MSG = "Add image";

        /// <summary>
        /// Add thumbnail message.
        /// </summary>
        private const string ADD_THUMBNAIL_MSG = "Add thumbnail";

        /// <summary>
        /// Add sound message.
        /// </summary>
        private const string ADD_SOUND_MSG = "Add sound";

        /// <summary>
        /// An instance of MyFile class.
        /// </summary>
        private readonly MyFile _myFile;

        /// <summary>
        /// An instance of NotificationManager class.
        /// </summary>
        private readonly Models.NotificationManager _notificationManager;

        /// <summary>
        /// Path to a background image.
        /// </summary>
        private string _backgroundPath;

        /// <summary>
        /// Path to an icon.
        /// </summary>
        private string _iconPath;

        /// <summary>
        /// Path to a sound.
        /// </summary>
        private string _soundPath;

        /// <summary>
        /// Path to a thumbnail.
        /// </summary>
        private string _thumbnailPath;

        /// <summary>
        /// Message describes the 'Unknown path type' error.
        /// </summary>
        private const string UNKNOWN_PATH_TYPE_MSG =
            "Unknown path category to be updated after file selection.";

        /// <summary>
        /// Color of an image and a led.
        /// </summary>
        private Color _imageColor;

        /// <summary>
        /// Duration in milliseconds when a led is turned on.
        /// </summary>
        private int _ledOnValue;

        /// <summary>
        /// Duration in milliseconds when a led is turned off.
        /// </summary>
        private int _ledOffValue;

        #endregion

        #region properties

        /// <summary>
        /// Property consists of a path to a background image.
        /// </summary>
        public string BackgroundPath
        {
            get => string.IsNullOrEmpty(_backgroundPath) ? ADD_IMAGE_MSG : _backgroundPath;
            set => SetProperty(ref _backgroundPath, value);
        }

        /// <summary>
        /// Property consists of a path to an icon.
        /// </summary>
        public string IconPath
        {
            get => string.IsNullOrEmpty(_iconPath) ? ADD_IMAGE_MSG : _iconPath;
            set => SetProperty(ref _iconPath, value);
        }

        /// <summary>
        /// Property consists of a path to a sound.
        /// </summary>
        public string SoundPath
        {
            get => string.IsNullOrEmpty(_soundPath) ? ADD_SOUND_MSG : _soundPath;
            set => SetProperty(ref _soundPath, value);
        }

        /// <summary>
        /// Property consists of a path to a thumbnail.
        /// </summary>
        public string ThumbnailPath
        {
            get => string.IsNullOrEmpty(_thumbnailPath) ? ADD_IMAGE_MSG : _thumbnailPath;
            set => SetProperty(ref _thumbnailPath, value);
        }

        /// <summary>
        /// Indicates if LED is available.
        /// </summary>
        public bool IsLedAvailable => Led.IsLedAvailable;

        /// <summary>
        /// Led on property.
        /// </summary>
        public bool LedOn { get; set; }

        /// <summary>
        /// Sound on property.
        /// </summary>
        public bool SoundOn { get; set; }

        /// <summary>
        /// Vibration on property.
        /// </summary>
        public bool VibrationOn { get; set; }

        /// <summary>
        /// Text in the title label.
        /// </summary>
        public string TitleText { get; set; }

        /// <summary>
        /// Text in the content label.
        /// </summary>
        public string ContentText { get; set; }

        /// <summary>
        /// Image color property.
        /// Color of an image and a led.
        /// </summary>
        public Color ImageColor
        {
            get => _imageColor;
            set => SetProperty(ref _imageColor, value);
        }

        /// <summary>
        /// Led on value property.
        /// Duration in milliseconds when a led is turned on.
        /// </summary>
        public int LedOnValue
        {
            get => _ledOnValue;
            set
            {
                SetProperty(ref _ledOnValue, value);
                OnPropertyChanged(nameof(LedOnOffStatus));
            }
        }

        /// <summary>
        /// Led off value property.
        /// Duration in milliseconds when a led is turned off.
        /// </summary>
        public int LedOffValue
        {
            get => _ledOffValue;
            set
            {
                SetProperty(ref _ledOffValue, value);
                OnPropertyChanged(nameof(LedOnOffStatus));
            }
        }

        /// <summary>
        /// Describes the status of the led: on and off times.
        /// </summary>
        public string LedOnOffStatus => $"On {LedOnValue} ms / off {LedOffValue} ms";

        /// <summary>
        /// Current progress of an ongoing notification.
        /// </summary>
        public int CurrentProgress { get; set; }

        /// <summary>
        /// Max progress of an ongoing notification.
        /// </summary>
        public int MaxProgress { get; set; }

        /// <summary>
        /// An instance of PageNavigation class.
        /// </summary>
        public PageNavigation AppPageNavigation { get; }

        /// <summary>
        /// Command responsible for navigating to new page.
        /// </summary>
        public ICommand NavigateToCommand { get; }

        /// <summary>
        /// Command responsible for navigating back from current page.
        /// </summary>
        public ICommand NavigateBackCommand { get; }

        /// <summary>
        /// Command responsible for launching MyFile application.
        /// </summary>
        public ICommand LaunchMyFileCommand { get; }

        /// <summary>
        /// Command responsible for deleting all notifications.
        /// </summary>
        public ICommand DeleteNotificationsCommand { get; }

        /// <summary>
        /// Command responsible for posting normal notification.
        /// </summary>
        public ICommand PostNormalNotificationCommand { get; }

        /// <summary>
        /// Command responsible for posting ongoing notification.
        /// </summary>
        public ICommand PostOngoingNotificationCommand { get; }

        /// <summary>
        /// Command responsible for resetting notification data.
        /// </summary>
        public ICommand ResetNotificationDataCommand { get; }

        #endregion

        #region methods

        /// <summary>
        /// Executes NavigateToCommand command.
        /// Navigates to a new page.
        /// </summary>
        /// <param name="obj">New page type.</param>
        private void NavigateTo(object obj)
        {
            AppPageNavigation.NavigateToCommand.Execute(obj);
        }

        /// <summary>
        /// Executes NavigateBackCommand command.
        /// Navigates back from a current page.
        /// </summary>
        /// <param name="obj">Unused object.</param>
        private void NavigateBack(object obj)
        {
            AppPageNavigation.NavigateBackCommand.Execute(obj);
        }

        /// <summary>
        /// Executes LaunchMyFileCommand command.
        /// Launches MyFile application.
        /// </summary>
        /// <param name="obj">Path category to be updated.</param>
        private void LaunchMyFile(object obj)
        {
            _myFile.Launch((PathCategory)obj);
        }

        /// <summary>
        /// Executes DeleteNotificationsCommand command.
        /// Deletes all notifications.
        /// </summary>
        /// <param name="obj">Unused object.</param>
        private void DeleteNotifications(object obj)
        {
            _notificationManager.DeleteAll();
        }

        /// <summary>
        /// Executes PostNormalNotificationCommand command.
        /// Posts normal notification.
        /// </summary>
        /// <param name="obj">Unused object.</param>
        private void PostNormalNotification(object obj)
        {
            var notification = new Notification
            {
                Title = TitleText,
                Content = ContentText,
                BackgroundPath = _backgroundPath,
                IconPath = _iconPath,
                SoundPath = _soundPath,
                ThumbnailPath = _thumbnailPath,
                Led = LedOn,
                Sound = SoundOn,
                Vibration = VibrationOn,
                LedOnMs = LedOnValue,
                LedOffMs = LedOffValue,
                LedColor = ImageColor
            };
            _notificationManager.PostNormal(notification);
        }

        /// <summary>
        /// Executes PostOngoingNotificationCommand command.
        /// Posts ongoing notification.
        /// </summary>
        /// <param name="obj">Unused object.</param>
        private void PostOngoingNotification(object obj)
        {
            var notification = new Notification
            {
                Title = TitleText,
                Content = ContentText,
                BackgroundPath = _backgroundPath,
                IconPath = _iconPath,
                SoundPath = _soundPath,
                ThumbnailPath = _thumbnailPath,
                Led = LedOn,
                Sound = SoundOn,
                Vibration = VibrationOn,
                LedOnMs = LedOnValue,
                LedOffMs = LedOffValue,
                LedColor = ImageColor,
                CurrentProgress = CurrentProgress,
                MaxProgress = MaxProgress
            };
            _notificationManager.PostOngoing(notification);
        }

        /// <summary>
        /// Resets notification data.
        /// </summary>
        private void ResetNotificationData()
        {
            LedOn = false;
            SoundOn = true;
            VibrationOn = true;
            ImageColor = Color.Green;
            BackgroundPath = ADD_IMAGE_MSG;
            IconPath = ADD_IMAGE_MSG;
            SoundPath = ADD_SOUND_MSG;
            ThumbnailPath = ADD_THUMBNAIL_MSG;
            CurrentProgress = 0;
            MaxProgress = 0;
        }

        /// <summary>
        /// NotificationManagerViewModel class constructor.
        /// Creates commands, initializes properties and registers an event handler
        /// to respond to MyFile's 'FileSelected' event.
        /// </summary>
        /// <param name="pageNavigation">An instance of PageNavigation class.</param>
        public NotificationManagerViewModel(PageNavigation pageNavigation)
        {
            _myFile = new MyFile();
            _notificationManager = new Models.NotificationManager();

            AppPageNavigation = pageNavigation;

            NavigateToCommand = new Command(NavigateTo);
            NavigateBackCommand = new Command(NavigateBack);
            LaunchMyFileCommand = new Command(LaunchMyFile);
            DeleteNotificationsCommand = new Command(DeleteNotifications);
            PostNormalNotificationCommand = new Command(PostNormalNotification);
            PostOngoingNotificationCommand = new Command(PostOngoingNotification);
            ResetNotificationDataCommand = new Command(ResetNotificationData);

            ResetNotificationData();

            _myFile.FileSelected += (sender, e) =>
            {
                switch (e.Category)
                {
                    case PathCategory.Background:
                        BackgroundPath = e.Path;
                        break;
                    case PathCategory.Icon:
                        IconPath = e.Path;
                        break;
                    case PathCategory.Sound:
                        SoundPath = e.Path;
                        break;
                    case PathCategory.Thumbnail:
                        ThumbnailPath = e.Path;
                        break;
                    default:
                        Debug.WriteLine(UNKNOWN_PATH_TYPE_MSG);
                        return;
                }
            };
        }

        #endregion
    }
}