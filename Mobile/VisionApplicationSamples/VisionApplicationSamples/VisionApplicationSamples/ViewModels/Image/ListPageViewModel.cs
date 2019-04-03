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

namespace VisionApplicationSamples.Image
{
    class ListPageViewModel : ViewModelBase
    {
        public List<string> TargetItems { get; protected set; }
        public List<string> SceneItems { get; protected set; }

        private string _selectedSceneItem;

        private bool _isSceneItemSelected = false;
        private bool _isTargetItemSelected = false;

        public string SelectedSceneItem
        {
            get
            {
                return _selectedSceneItem;
            }
            set
            {
                if (_selectedSceneItem != value)
                {
                    _selectedSceneItem = value;
                    _isSceneItemSelected = true;

                    OnPropertyChanged(nameof(SelectedSceneItem));

                    if (_isSceneItemSelected && _isTargetItemSelected)
                        OpenCommand.ChangeCanExecute();
                }
            }
        }

        private string _selectedTargetItem;
        public string SelectedTargetItem
        {
            get
            {
                return _selectedTargetItem;
            }
            set
            {
                if (_selectedTargetItem != value)
                {
                    _selectedTargetItem = value;
                    _isTargetItemSelected = true;

                    OnPropertyChanged(nameof(SelectedTargetItem));

                    if (_isSceneItemSelected && _isTargetItemSelected)
                        OpenCommand.ChangeCanExecute();
                }
            }
        }

        public Command OpenCommand { get; protected set; }

        private void LoadImages()
        {
            SceneItems = new List<string>();
            TargetItems = new List<string>();

            foreach (var path in DependencyService.Get<IDataBase>().SelectSceneImages())
            {
                SceneItems.Add(path);
            }

            foreach (var path in DependencyService.Get<IDataBase>().SelectTargetImages())
            {
                TargetItems.Add(path);
            }
        }

        public ListPageViewModel()
        {
            LoadImages();

            OpenCommand = new NavigationCommands<RecognizerPage>(() => new RecognizerPageViewModel(SelectedTargetItem, SelectedSceneItem),
                                                                    () => (SelectedTargetItem != null && SelectedSceneItem != null));
        }
    }
}