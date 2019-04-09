/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Xamarin.Forms;
using MusicPlayer.Views;
using MusicPlayer.Models;
using MusicPlayer.ViewModels;

[assembly: Dependency(typeof(ViewNavigation))]

namespace MusicPlayer.Views
{
    /// <summary>
    /// Class handling navigation over application views.
    /// </summary>
    class ViewNavigation : IViewNavigation
    {
        #region methods

        /// <summary>
        /// Navigates to the view containing list of available soundtracks.
        /// </summary>
        /// <param name="clearHistory">
        /// Flag indicating if navigation history should be cleared.
        /// </param>
        public void GoToSoundtracksList(bool clearHistory = false)
        {
            if (clearHistory)
            {
                Application.Current.MainPage = new NavigationPage(new SoundtracksListPage());
            }
            else
            {
                Application.Current.MainPage.Navigation.PushAsync(new SoundtracksListPage());
            }
        }

        /// <summary>
        /// Navigates to page with track preview.
        /// </summary>
        public void GoToPreview()
        {
            Application.Current.MainPage.Navigation.PushAsync(new PreviewPage());
        }

        #endregion
    }
}
