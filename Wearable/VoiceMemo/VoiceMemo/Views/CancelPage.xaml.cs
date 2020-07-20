/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using VoiceMemo.ViewModels;
using VoiceMemo.Resx;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace VoiceMemo.Views
{
    /// <summary>
    /// CancelPage class
    /// It provides a way to cancel voice recording
    /// </summary>
    public partial class CancelPage : TwoButtonPage
    {
        RecordingPageModel ViewModel;

        public CancelPage(RecordingPageModel vm)
        {
            // Hide navigation bar
            NavigationPage.SetHasNavigationBar(this, false);
            // Subscribe notification of locale changes to update text based on locale
            MessagingCenter.Subscribe<App>(this, MessageKeys.UpdateByLanguageChange, (obj) =>
            {
                // Update text that has been translated into the current language.
                CancelLabel.Text = AppResources.CancelRecording;
            });
            InitializeComponent();
            Init(vm);
        }

        void Init(RecordingPageModel vm)
        {
            BindingContext = ViewModel = vm;
        }
        
        // Keep recording
        // CancelPage --> RecordingPage
        async void OnAbortCancelClicked(object sender, EventArgs args)
        {
            // Request to Resume
            ViewModel.RequestCommand.Execute(RecordingCommandType.Resume);
            await Navigation.PopAsync();
        }

        // Cancel recording voice
        // CancelPage --> StandByPage(First main page)
        async void OnCancelClicked(object sender, EventArgs args)
        {
            // Request to Cancel
            ViewModel.RequestCommand.Execute(RecordingCommandType.Cancel);
            await Navigation.PopToRootAsync(); // @20180218-vincent: This should never be used without clearing
        }
    }
}