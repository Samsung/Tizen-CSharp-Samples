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
using Xamarin.Forms;

namespace MetadataExtractorSample
{
    class ListViewModel : ViewModelBase
    {
        public ListViewModel()
        {
            Items = DependencyService.Get<IMediaContent>().GetAllMedia();

            OpenCommand = new Command(async () =>
            {
                DependencyService.Get<IMetadataExtractor>().Path = SelectedItem;

                await Application.Current.MainPage.Navigation.PopAsync();
            }, () => SelectedItem != null);
        }

        private string _selectedItem;

        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;

                    OpenCommand.ChangeCanExecute();

                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        public IEnumerable<string> Items { get; }

        public Command OpenCommand { get; protected set; }
    }
}
