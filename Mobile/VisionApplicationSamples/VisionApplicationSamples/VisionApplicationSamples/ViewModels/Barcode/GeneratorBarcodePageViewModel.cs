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
using System.Windows.Input;
using Xamarin.Forms;

namespace VisionApplicationSamples.Barcode
{
    /// <summary>
    /// ViewModel for GeneratorBarcodePage.
    /// </summary>
    class GeneratorBarcodePageViewModel : ViewModelBase
    {
        private int _inputWidth = 300;
        private int _inputHeight = 100;
        private string _inputMessage = "";
        private string _generationResult = "";
        private bool _isValid = false;


        public ICommand GenerateCommand { get; protected set; }
        public ICommand SetBarcodeTypeCommand { get; protected set; }

        public ImageSource GeneratedImage { get; protected set; }

        public GeneratorBarcodePageViewModel()
        {
            SetBarcodeTypeCommand = new Command(barcodeType => BarcodeGenerator.SetBarcodeType((BarcodeType)barcodeType));
            GenerateCommand = new Command(
                () =>
                {
                    try
                    {
                        RefreshPage(BarcodeGenerator.Generate(InputWidth, InputHeight, InputMessage));
                        IsValid = true;
                        GenerationResultText = $"Success:";
                    }
                    catch (Exception e)
                    {
                        IsValid = false;
                        GenerationResultText = $"Failure : {e.Message}. \nCheck type is valid or not";
                    }
                });
        }

        protected IGeneratorBarcode BarcodeGenerator => DependencyService.Get<IGeneratorBarcode>();

        private void RefreshPage(string path)
        {
            GeneratedImage = ImageSource.FromFile(path);
            OnPropertyChanged(nameof(GeneratedImage));
        }
        public int InputWidth
        {
            get
            {
                return _inputWidth;
            }
            set
            {
                if (_inputWidth != value)
                {
                    _inputWidth = value;
                    OnPropertyChanged(nameof(InputWidth));
                }
            }
        }

        public int InputHeight
        {
            get
            {
                return _inputHeight;
            }
            set
            {
                if (_inputHeight != value)
                {
                    _inputHeight = value;
                    OnPropertyChanged(nameof(InputHeight));
                }
            }
        }

        public string InputMessage
        {
            get
            {
                return _inputMessage;
            }
            set
            {
                if (_inputMessage != value)
                {
                    _inputMessage = value;
                    OnPropertyChanged(nameof(InputMessage));
                }
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                if (_isValid != value)
                {
                    _isValid = value;
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }

        public string GenerationResultText
        {
            get
            {
                return _generationResult;
            }
            set
            {
                if (_generationResult != value)
                {
                    _generationResult = value;
                    OnPropertyChanged(nameof(GenerationResultText));
                }
            }
        }
    }
}