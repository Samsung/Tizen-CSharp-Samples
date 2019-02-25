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

using MediaCodecSample.ViewModels;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Multimedia;

namespace MediaCodecSample.Views
{
    /// <summary>
    /// EncodingPage class for the MediaCodecSample application.
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EncodingPage : CirclePage
	{
        /// <summary>
        /// ViewModel for the EncoderDetailPage.
        /// </summary>
        private EncodingViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodingPage"/> class.
        /// </summary>
        /// <param name="type">
        /// The type of media format.
        /// </param>
        public EncodingPage(MediaFormatType type)
		{
			InitializeComponent();
            _viewModel = new EncodingViewModel(Navigation, type);
            BindingContext = _viewModel;
        }

        /// <summary>
        /// Invoked when this view appears.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as EncodingViewModel).OnAppearing();
        }

        /// <summary>
        /// Invoked when this view disappears.
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            (BindingContext as EncodingViewModel).OnDisappearing();
        }
    }
}