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

using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;

namespace ThumbnailExtractorSample
{
    /// <summary>
    /// Represents Main ViewModel class.
    /// </summary>
    class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies change of a property.
        /// </summary>
        /// <param name="propertyName">A property name.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ImageSource OriginalImage { get; protected set; }
        public ImageSource ThumbnailImage { get; protected set; }

        public ICommand DefaultCommand { get; protected set; }
        public ICommand SizeCommand { get; protected set; }

        public MainPageViewModel()
        {
            OriginalImage = ImageSource.FromFile(DependencyService.Get<IThumbnailExtractor>().ImagePath);

            DefaultCommand = new Command(async () =>
                RefreshPage(await DependencyService.Get<IThumbnailExtractor>().ExtractAsync()));

            SizeCommand = new Command<int>(async size =>
                RefreshPage(await DependencyService.Get<IThumbnailExtractor>().ExtractAsync(size, size)));
        }

        private void RefreshPage(string path)
        {
            ThumbnailImage = ImageSource.FromFile(path);
            OnPropertyChanged(nameof(ThumbnailImage));

            RefreshFileSize(path);
        }

        private void RefreshFileSize(string path)
        {
            Size = new FileInfo(path).Length;
            OnPropertyChanged(nameof(Size));
        }

        public long Size { get; set; }
    }
}
