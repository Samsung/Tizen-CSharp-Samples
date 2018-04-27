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

using System.Windows.Input;
using VisionApplicationSamples.Barcode;
using VisionApplicationSamples.Face;
using VisionApplicationSamples.Image;

namespace VisionApplicationSamples
{
    /// <summary>
    /// ViewModel for MainPage.
    /// </summary>
    class MainPageViewModel : ViewModelBase
    {
        // Commands for the buttons.
        public ICommand BarcodeCommand { get; protected set; }
        public ICommand FaceCommand { get; protected set; }
        public ICommand ImageCommand { get; protected set; }

        public MainPageViewModel()
        {
            BarcodeCommand = new NavigationCommands<BarcodePage>();
            FaceCommand = new NavigationCommands<FacePage>();
            ImageCommand = new NavigationCommands<ImagePage>();
        }
    }
}
