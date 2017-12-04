/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace PlayerSample
{
    /// <summary>
    /// Base ViewModel for ListPage.
    /// </summary>
    class ListPageViewModel : ViewModelBase
    {
        public IEnumerable<MediaItem> Items { get; protected set; }

        public MediaItem SelectedItem { get; set; }

        public ICommand OpenCommand { get; protected set; }
    }

    /// <summary>
    /// ViewModel for ListPage that lists media files.
    /// </summary>
    class MediaListPageViewModel : ListPageViewModel
    {
        public MediaListPageViewModel()
        {
            Items = DependencyService.Get<IMediaContentDatabase>().SelectAllPlayable();

            OpenCommand = new Command(async () =>
            {
                if (SelectedItem == null)
                {
                    return;
                }

                await Application.Current.MainPage.Navigation.PushAsync(new PlayPage()
                {
                    BindingContext = new PlayPageViewModel(SelectedItem.Path)
                });
            });
        }
    }

    /// <summary>
    /// ViewModel for ListPage that lists subtitle files.
    /// </summary>
    class SubtitleListPageViewModel : ListPageViewModel
    {
        public SubtitleListPageViewModel()
        {
            Items = DependencyService.Get<IMediaContentDatabase>().SelectAllSubtitles();

            OpenCommand = new Command(async () =>
            {
                if (SelectedItem == null)
                {
                    return;
                }

                DependencyService.Get<IMediaPlayer>().SetSubtile(SelectedItem.Path);

                await Application.Current.MainPage.Navigation.PopAsync();
            });
        }
    }
}
