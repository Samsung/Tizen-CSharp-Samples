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

using TonePlayerSample.ViewModels;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace TonePlayerSample.Views
{
    /// <summary>
    /// MainPage class for the TonePlayerSample application
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CirclePage
    {
        /// <summary>
        /// ViewModel for the MainPage
        /// </summary>
        private MainViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainViewModel(Navigation);
            BindingContext = _viewModel;
        }

        /// <summary>
        /// Called when radio button is clicked in MainPage.
        /// </summary>
        /// <param name="sender">The sender of this event</param>
        /// <param name="args">The event argument</param>
        public void OnSelected(object sender, SelectedEventArgs args)
        {
            Radio radio = sender as Radio;
            if (radio != null && radio.IsSelected)
            {
                MainViewModel.Duration = int.Parse(radio.Value);
            }
        }
    }
}