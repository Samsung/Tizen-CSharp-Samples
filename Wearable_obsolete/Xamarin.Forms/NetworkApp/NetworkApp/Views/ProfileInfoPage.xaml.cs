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

using NetworkApp.ViewModels;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace NetworkApp.Views
{
    /// <summary>
    /// ProfileInfoPage class
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileInfoPage : IndexPage
    {
        /// <summary>
        /// ViewModel for ProfileInfoPage
        /// </summary>
        private ProfileInfoViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileInfoPage"/> class
        /// </summary>
        /// <param name="profileName">Name of Connection Profile to be shown</param>
        public ProfileInfoPage(string profileName)
        {
            _viewModel = new ProfileInfoViewModel(profileName);

            InitializeComponent();
            BindingContext = _viewModel;
        }
    }
}