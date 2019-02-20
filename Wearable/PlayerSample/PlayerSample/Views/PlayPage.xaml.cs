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

using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;
using PlayerSample.Models;
using PlayerSample.ViewModels;
using Xamarin.Forms;

namespace PlayerSample.Views
{
    /// <summary>
    /// PlayPage class for the PlayerSample application
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayPage : CirclePage
    {
        /// <summary>
        /// ViewModel for the PlayPage
        /// </summary>
        private PlayPageViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayPage"/> class
        /// </summary>
        /// <param name="path">Selected file path or streaming uri</param>
        public PlayPage(string path)
        {
            InitializeComponent();
            _viewModel = new PlayPageViewModel(path);
            BindingContext = _viewModel;
        }

        /// <summary>
        /// Performs action when the page appears.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as ViewModelBase).OnAppearing();
        }

        /// <summary>
        /// Performs action when the page disappears.
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            (BindingContext as ViewModelBase).OnDisappearing();
        }
    }
}
