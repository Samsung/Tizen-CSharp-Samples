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

using SpeechToText.ViewModels;
using System;
using System.ComponentModel;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace SpeechToText.Views
{
    /// <summary>
    /// The applications's main page class.
    /// Shows the recognized text and allows the user to control the process (start, pause, stop, clear).
    /// Provides also a button which navigates to application settings.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CirclePage
    {
        /// <summary>
        /// The class constructor.
        /// Initializes the component.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            BindingContextChanged += OnBindingContextChanged;
        }

        /// <summary>
        /// Handles binding context change.
        ///
        /// Adds callback to property change event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void OnBindingContextChanged(object sender, EventArgs eventArgs)
        {
            MainViewModel context = BindingContext as MainViewModel;

            if (context == null)
            {
                return;
            }

            context.PropertyChanged += ContextOnPropertyChanged;

        }

        /// <summary>
        /// Handles property change of the binding context.
        ///
        /// If result changes, the scroll-view will scroll to the end.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="propertyChangedEventArgs">Event arguments.</param>
        private void ContextOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(MainViewModel.ResultText))
            {
                ResultScrollView.ScrollToAsync(0, ResultScrollView.ContentSize.Height, true);
            }
        }
    }
}