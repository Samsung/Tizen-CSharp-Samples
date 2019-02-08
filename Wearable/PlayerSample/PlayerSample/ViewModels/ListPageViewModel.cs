//Copyright 2019 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using PlayerSample.Views;
using PlayerSample.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PlayerSample.ViewModels
{
    /// <summary>
    /// ViewModel class for file list
    /// </summary>
    class ListPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListPageViewModel"/> class
        /// </summary>
        /// <param name="navigation">Navigation instance</param>
        public ListPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Items = DependencyService.Get<IMediaContentDatabase>().SelectAllPlayable();
        }

        /// <summary>
        /// Gets the list of contents
        /// </summary>
        public IEnumerable<MediaItem> Items { get; protected set; }

        /// <summary>
        /// Gets the selected content
        /// </summary>
        private MediaItem _selecetdItem;
        public MediaItem SelectedItem
        {
            get => _selecetdItem;
            set
            {
                if (_selecetdItem != value)
                {
                    _selecetdItem = value;

                    OnPropertyChanged(nameof(SelectedItem));

                    OpenCommand?.ChangeCanExecute();
                }
            }
        }

        /// <summary>
        /// Indicates if page initialization is in progress
        /// </summary>
        private bool _inProgress = false;

        /// <summary>
        /// Gets or sets a value indicating whether 
        /// initialization is in progress
        /// </summary>
        public bool InitializationInProgress
        {
            get => _inProgress;
            set
            {
                SetProperty(ref _inProgress, value);
            }
        }

        /// <summary>
        /// Gets or sets command for pushing new page
        /// </summary>
        public Command OpenCommand => new Command(PushPlayPage);

        /// <summary>
        /// Gets the Navigation instance to push new pages properly
        /// </summary>
        public INavigation Navigation { get; protected set; }

        
        /// <summary>
        /// Pushes new FilesListPage with found files of given media type
        /// </summary>
        private void PushPlayPage()
        {
            if (SelectedItem != null)
                Navigation.PushModalAsync(new PlayPage(SelectedItem.Path));
            else
                Logger.Log("Selected Item is null");
        }
    }
}
