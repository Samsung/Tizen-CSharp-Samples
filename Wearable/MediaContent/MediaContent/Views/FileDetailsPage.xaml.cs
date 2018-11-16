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

using Tizen;
using Tizen.Content.MediaContent;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace MediaContent.Views
{
    /// <summary>
    /// Page class for FileDetails. Shows selected file properties.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FileDetailsPage : CirclePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileDetailsPage"/> class
        /// </summary>
        /// <param name="fileInfo">File information to show on a page</param>
        public FileDetailsPage(object fileInfo)
        {
            BindingContext = this;
            SelectedFile = (MediaInfo)fileInfo;
            OnPropertyChanged(nameof(SelectedFile));
            InitializeComponent();
        }

        /// <summary>
        /// Gets MediaInfo of chosen file
        /// </summary>
        public MediaInfo SelectedFile { get; private set; }
    }
}