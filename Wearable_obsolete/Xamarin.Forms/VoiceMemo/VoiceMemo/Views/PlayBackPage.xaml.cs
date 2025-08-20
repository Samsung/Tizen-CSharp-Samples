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
using Xamarin.Forms;

namespace VoiceMemo.Views
{
    /// <summary>
    /// PlayBackPage class
    /// In this page, recorded content will play.
    /// </summary>
    public partial class PlayBackPage : CirclePageEx
    {
        PlayBackPageModel viewModel;
        bool hidePageRequested;
        public PlayBackPage(PlayBackPageModel _viewModel)
        {
            BindingContext = this.viewModel = _viewModel;
            InitializeComponent();
            RegisterSomeNotifications();
        }

        void RegisterSomeNotifications()
        {
            // You can get notified whenever playing audio is done.
            MessagingCenter.Subscribe<PlayBackPageModel, bool>(this, MessageKeys.AudioPlayDone, async (obj, finished) =>
            {
                // If playing audio is done, this page will be hidden. (MainPage will be shown.)
                if (finished)
                {
                    await Navigation.PopAsync();
                }
            });
        }
                
        protected override bool OnBackButtonPressed()
        {
            if (hidePageRequested)
            {
                return true;
            }
            else
            {
                hidePageRequested = true;
            }

            if (VolumeView.IsVisible)
            {
                Console.WriteLine("[PlaybackPage.OnBackButtonPressed] VolumeView is shown.");
                viewModel.VolumeViewVisibilityCommand?.Execute(false);
                return false;
            }

            Console.WriteLine("[PlaybackPage.OnBackButtonPressed]  PlayBackPage will be hidden.");
            viewModel.Stop();
            return base.OnBackButtonPressed();
        }

        /// <summary>
        /// Invoked immediately prior to the Page becoming visible.
        /// </summary>
        protected override void OnAppearing()
        {
            Console.WriteLine("[PlaybackPage]  OnAppearing() ...");
            hidePageRequested = false;
        }

        /// <summary>
        /// Invoked when this page disappears.
        /// </summary>
        protected override void OnDisappearing()
        {
            Console.WriteLine("[PlaybackPage]  OnDisappearing");
            viewModel.Stop();
        }

        public void Dispose()
        {
            Console.WriteLine("[PlayBackPageModel]  Dispose");
            MessagingCenter.Unsubscribe<PlayBackPageModel, bool>(this, MessageKeys.AudioPlayDone);
            ((PlayBackPageModel)BindingContext).Dispose();
        }
    }
}