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

namespace VisionApplicationSamples.Barcode
{
    /// <summary>
    /// Base ViewModel for ListPage.
    /// </summary>
    class ListPageViewModel : ViewModelBase
    {
        public List<string> Items { get; protected set; }

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

        public Command OpenCommand { get; protected set; }

        private void LoadImages()
        {
            Items = new List<string>();

            foreach (var path in DependencyService.Get<IDataBase>().SelectImages())
            {
                Items.Add(path);
            }
        }
        public ListPageViewModel()
        {
            LoadImages();

            OpenCommand = new NavigationCommands<DetectorPage>(() => new DetectorPageViewModel(SelectedItem), () => SelectedItem != null);

        }
    }
}
