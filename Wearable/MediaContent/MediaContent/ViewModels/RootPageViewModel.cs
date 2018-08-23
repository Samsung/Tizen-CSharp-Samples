//Copyright 2018 Samsung Electronics Co., Ltd
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

using MediaContent.Views;
using NetworkApp.Models;
using System;
using System.Windows.Input;
using Tizen.Content.MediaContent;
using Xamarin.Forms;

namespace MediaContent.ViewModels
{
    /// <summary>
    /// ViewModel class for the Root Page
    /// </summary>
    class RootPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RootPageViewModel"/> class
        /// </summary>
        /// <param name="navigation">Navigation instance</param>
        public RootPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            PushNewPageCommand = new Command<string>(PushNewPageWithFoundFiles);
        }

        /// <summary>
        /// Gets or sets command for pushing new page with found files
        /// </summary>
        public ICommand PushNewPageCommand { get; set; }

        /// <summary>
        /// Gets the Navigation instance to push new pages properly
        /// </summary>
        public INavigation Navigation { get; }

        /// <summary>
        /// Pushes new FilesListPage with found files of given media type
        /// </summary>
        /// <param name="mediaType">Requested media type</param>
        private void PushNewPageWithFoundFiles(string mediaType)
        {
            if (Enum.TryParse(mediaType, out MediaType type))
            {
                Navigation.PushModalAsync(new FilesListPage(type));
            }
            else
            {
                Logger.Log("Error while parsing MediaType enum");
            }
        }
    }
}
