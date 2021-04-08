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

using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Tizen; 

namespace VisionApplicationSamples.Barcode
{
    /// <summary>
    /// ViewModel for DetectorPage.
    /// </summary>
    /// 
    class DetectorPageViewModel : ViewModelBase
    {
        private string _detectionResult = "";
        public ImageSource BarcodeImage { get; protected set; }

        public int BarcodeCount { get; protected set; }
        public List<string> Items { get; protected set; }

        public ICommand DetectCommand { get; protected set; }
        protected IDetector BarcodeDetectorImpl => DependencyService.Get<IDetector>();

        public DetectorPageViewModel(string path)
        {
            BarcodeImage = ImageSource.FromFile(path);

            BarcodeDetectorImpl.ImagePath = path;
            BarcodeDetectorImpl.Decode();

            DetectCommand = new Command(
                async () =>
                {
                    try
                    {
                        RefreshPage(await BarcodeDetectorImpl.Detect());
                        DetectionResultText = $"Success";
                    }
                    catch (Exception e)
                    {
                        Log.Info("VisionApplicationSamples", e.Message);
                        DetectionResultText = $"Failure";
                    }
                }
            );
        }

        private void RefreshPage(string source)
        {
            BarcodeImage = ImageSource.FromFile(source);
            OnPropertyChanged(nameof(BarcodeImage));

            RefreshMessage();
        }

        private void RefreshMessage()
        {
            BarcodeCount = BarcodeDetectorImpl.NumberOfBarcodes;
            OnPropertyChanged(nameof(BarcodeCount));
            Items = BarcodeDetectorImpl.Messages;
            OnPropertyChanged(nameof(Items));
        }

        public string DetectionResultText
        {
            get
            {
                return _detectionResult;
            }
            set
            {
                if (_detectionResult != value)
                {
                    _detectionResult = value;
                    OnPropertyChanged(nameof(DetectionResultText));
                }
            }
        }
    }
}
