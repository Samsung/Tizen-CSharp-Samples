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
    /// <summary>
    /// ViewModel for RecognizerPage.
    /// </summary>
    class RecognizerPageViewModel : ViewModelBase
    {
        public ImageSource TargetImage { get; protected set; }

        public ImageSource SceneImage { get; protected set; }

        public List<Point> Items { get; protected set; }

        public IRecognizer ImageRecogImpl = DependencyService.Get<IRecognizer>();

        public Command SetTargetCommand { get; protected set; }
        public Command RecognizeCommand { get; protected set; }

        public bool Success { get; protected set; }

        public RecognizerPageViewModel(string targetPath, string scenePath)
        {
            TargetImage = ImageSource.FromFile(targetPath);
            SceneImage = ImageSource.FromFile(scenePath);
            ImageRecogImpl.TargetImagePath = targetPath;
            ImageRecogImpl.SceneImagePath = scenePath;
            ImageRecogImpl.Decode();

            SetTargetCommand = new Command(() => ReadyToRecognize(ImageRecogImpl.FillTarget()));
            RecognizeCommand = new Command(async () => RefreshPage(await ImageRecogImpl.Recognize()), () => ImageRecogImpl.IsTargetFilled);
        }

        private void ReadyToRecognize(bool isReady)
        {
            if (isReady)
            {
              RecognizeCommand.ChangeCanExecute();
              OnPropertyChanged(nameof(RecognizeCommand));

            }
        }

        private void RefreshPage(string source)
        {
            SceneImage = ImageSource.FromFile(source);
            OnPropertyChanged(nameof(SceneImage));

            RefreshResult();
        }

        private void RefreshResult()
        {
            Success = ImageRecogImpl.Success;
            OnPropertyChanged(nameof(Success));

            Items = ImageRecogImpl.RecognizedTarget;
            OnPropertyChanged(nameof(Items));
        }
    }
}