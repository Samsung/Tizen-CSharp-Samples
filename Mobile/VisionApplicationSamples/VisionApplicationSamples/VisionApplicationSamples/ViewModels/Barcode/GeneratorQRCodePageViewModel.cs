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
    /// ViewModel for QGeneratorQRCodePage.
    /// </summary>
    class GeneratorQRCodePageViewModel : ViewModelBase
    {
        private int _inputWidth = 300;
        private int _inputHeight = 300;
        private string _inputMessage = "";
        private string _generationResult = "";
        private bool _isValid = false;

        public ICommand GenerateCommand { get; protected set; }
        public ICommand SetQrModeTypeCommand { get; protected set; }
        public ICommand SetErrorCorrectionLevelCommand { get; protected set; }
        public ICommand SetVersionCommand { get; protected set; }

        public ImageSource GeneratedImage { get; protected set; }

        public GeneratorQRCodePageViewModel()
        {
            SetQrModeTypeCommand = new Command(qrModeType =>
                QRCodeGenerator.SetQrModeType((QrModeType)qrModeType));
            SetErrorCorrectionLevelCommand = new Command(eccLevel =>
                QRCodeGenerator.SetErrorCorrectionLevel((ErrorCorrectionLevel)eccLevel));
            SetVersionCommand = new Command(version =>
                QRCodeGenerator.SetVersion((int)version));

            GenerateCommand = new Command(
                () =>
                {
                    try
                    {
                        RefreshPage(QRCodeGenerator.Generate(inputWidth, inputHeight, inputMessage));
                        IsValid = true;
                        GenerationResultText = $"Success:";
                    }
                    catch (Exception e)
                    {
                        IsValid = false;
                        GenerationResultText = $"Failure : {e.Message}. \nCheck QrMdoe, ECC, Version are valid or not ";
                    }
                });
        }

        protected IGeneratorQRCode QRCodeGenerator => DependencyService.Get<IGeneratorQRCode>();

        private void RefreshPage(string path)
        {
            GeneratedImage = ImageSource.FromFile(path);
            OnPropertyChanged(nameof(GeneratedImage));
        }

        public int inputWidth
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
                    OnPropertyChanged(nameof(inputWidth));
                }
            }
        }

        public int inputHeight
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
                    OnPropertyChanged(nameof(inputHeight));
                }
            }
        }

        public string inputMessage
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
                    OnPropertyChanged(nameof(inputMessage));
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
