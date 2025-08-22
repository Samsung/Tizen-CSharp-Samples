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
    /// WiFiInfoPage class
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WiFiInfoPage : IndexPage
    {
        /// <summary>
        /// ViewModel for WiFiInfoPage
        /// </summary>
        private WiFiInfoViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="WiFiInfoPage"/> class
        /// </summary>
        public WiFiInfoPage()
        {
            _viewModel = new WiFiInfoViewModel(Navigation);

            InitializeComponent();
            BindingContext = _viewModel;
        }
    }
}