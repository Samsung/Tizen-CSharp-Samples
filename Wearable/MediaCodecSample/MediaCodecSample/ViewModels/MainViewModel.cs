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

using MediaCodecSample.Views;
using System;
using System.Windows.Input;
using Tizen.Multimedia;
using Xamarin.Forms;

namespace MediaCodecSample.ViewModels
{
    /// <summary>
    /// MainViewModel class for the media encoding
    /// </summary>
    class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class
        /// </summary>
        /// <param name="navigation">
        /// Navigation instance
        /// </param>
        public MainViewModel(INavigation navigation)
        {
            Navigation = navigation;
            PushNewPageCommand = new Command<string>(PushNewPage);
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
        /// <param name="mediaFormatType">
        /// Requested media type
        /// </param>
        private void PushNewPage(string mediaFormatType)
        {
            if (Enum.TryParse(mediaFormatType, out MediaFormatType type))
            {
                Navigation.PushModalAsync(new EncodingPage(type));
            }
            else
            {
                throw new ArgumentException("Invalid media format.");
            }
        }
    }
}
