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

namespace VisionApplicationSamples.Face
{
    /// <summary>
    /// ViewModel for DetectorPage.
    /// </summary>
    class DetectorPageViewModel : ViewModelBase
    {
        public ImageSource FaceImage { get; protected set; }

        public Command DetectCommand { get; protected set; }

        public List<Rectangle> Items { get; protected set; }

        protected IDetector FaceDetectorImpl = DependencyService.Get<IDetector>();

        public int FaceCount { get; protected set; }

        public DetectorPageViewModel(string path)
        {
            FaceImage = ImageSource.FromFile(path);
            FaceDetectorImpl.ImagePath = path;
            FaceDetectorImpl.Decode();

            DetectCommand = new Command(async () => RefreshPage(await FaceDetectorImpl.Detect()));
        }

        private void RefreshPage(string source)
        {
            FaceImage = ImageSource.FromFile(source);
            OnPropertyChanged(nameof(FaceImage));

            RefreshCount();
        }

        private void RefreshCount()
        {
            FaceCount = FaceDetectorImpl.NumberOfFace;
            OnPropertyChanged(nameof(FaceCount));

            Items = FaceDetectorImpl.DetectedFaces;
            OnPropertyChanged(nameof(Items));
        }
    }
}
