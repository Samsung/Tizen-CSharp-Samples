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
    /// LoginPage class
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : CirclePage
    {
        /// <summary>
        /// ViewModel for LoginPage
        /// </summary>
        private LoginViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class
        /// </summary>
        /// <param name="apName">Name of AP to connect</param>
        public LoginPage(string apName)
        {
            _viewModel = new LoginViewModel(apName);

            InitializeComponent();
            BindingContext = _viewModel;
        }
    }
}