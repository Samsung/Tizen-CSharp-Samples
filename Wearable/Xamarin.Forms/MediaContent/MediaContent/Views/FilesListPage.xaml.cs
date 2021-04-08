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

using MediaContent.ViewModels;
using Tizen.Content.MediaContent;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace MediaContent.Views
{
    /// <summary>
    /// Page class for FilesList. Shows media files on the device
    /// with desired media type.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilesListPage : CirclePage
    {
        /// <summary>
        /// ViewModel for FilesListPage
        /// </summary>
        private FilesListViewModel _viewModel;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FilesListPage"/> class
        /// </summary>
        /// <param name="mediaType">Chosen media type of files to show</param>
        public FilesListPage(MediaType mediaType)
        {
            _viewModel = new FilesListViewModel(mediaType, Navigation);
            InitializeComponent();
            BindingContext = _viewModel;
        }
    }
}