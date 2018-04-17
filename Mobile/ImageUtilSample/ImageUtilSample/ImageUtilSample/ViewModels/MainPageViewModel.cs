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
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ImageUtilSample
{
    /// <summary>
    /// ViewModel for MainPage.
    /// </summary>
    class MainPageViewModel : ViewModelBase
    {
        // Commands for the buttons.
        public ICommand SelectCommand { get; protected set; }
        public ICommand DecodeCommand { get; protected set; }

        public ICommand RotateCommand { get; protected set; }
        public ICommand FlipCommand { get; protected set; }
        public ICommand ColorSpaceCommand { get; protected set; }
        public ICommand ResizeCommand { get; protected set; }
        public ICommand CropCommand { get; protected set; }

        private IImageUtil ImageUtil => DependencyService.Get<IImageUtil>();

        private Random _random = new Random();

        public MainPageViewModel()
        {
            SelectCommand = new Command(
                async () =>
                {
                    var viewModel = new ListPageViewModel();

                    viewModel.Done += (s, e) => OnFileSelected((s as ListPageViewModel).SelectedItem);

                    await Application.Current.MainPage.Navigation.PushAsync(
                        new ListPage() { BindingContext = viewModel });
                });

            DecodeCommand = new Command(
                async () =>
                {
                    try
                    {
                        await ImageUtil.Decode();

                        DecodeResultText = "Decoded";
                        IsDecodeSucceeded = true;
                        ResultSource = ImageSource.FromFile(ImageUtil.ResultPath);
                    }
                    catch (Exception e)
                    {
                        DecodeResultText = $"Failed to decode : {e.Message}";
                        IsDecodeSucceeded = false;
                    }
                });

            RotateCommand = CreateTransformCommand(Rotate);

            FlipCommand = CreateTransformCommand(Flip);

            ResizeCommand = CreateTransformCommand(Resize);

            CropCommand = CreateTransformCommand(Crop);

            ColorSpaceCommand = CreateTransformCommand(ChangeColorSpace);
        }

        private Command CreateTransformCommand(Func<Task> operation)
        {
            return new Command(async () =>
            {
                try
                {
                    ErrorText = null;

                    await operation();

                    ResultSource = ImageSource.FromFile(ImageUtil.ResultPath);
                }
                catch (Exception e)
                {
                    ErrorText = e.Message;
                }
            });
        }

        private async Task Rotate()
        {
            var rotation = RandomValue<TransformRotation>();

            await ImageUtil.Rotate(rotation);

            CommandText = rotation.ToString();
        }

        private async Task Flip()
        {
            var flip = RandomValue<TransformFlip>();

            CommandText = $"Flip ({flip})";

            await ImageUtil.Flip(flip);
        }

        private async Task Resize()
        {
            var newWidth = ImageUtil.ImageWidth / 8 * 4;
            var newHeight = ImageUtil.ImageHeight / 8 * 4;

            CommandText = $"Resize ({newWidth}, {newHeight})";

            await ImageUtil.Resize(newWidth, newHeight);
        }

        private async Task Crop()
        {
            var left = ImageUtil.ImageWidth / 8 * 2;
            var top = ImageUtil.ImageHeight / 8 * 2;
            var newWidth = ImageUtil.ImageWidth / 8 * 4;
            var newHeight = ImageUtil.ImageHeight / 8 * 4;

            CommandText = $"Crop ({left}, {top}, {newWidth}, {newHeight})";

            await ImageUtil.Crop(left, top, newWidth, newHeight);
        }

        private async Task ChangeColorSpace()
        {
            var colorSpace = TransformColorSpace.Rgb888;

            CommandText = $"ColorSpace => {colorSpace}";

            await ImageUtil.ChangeColorSpace(colorSpace);
        }

        private T RandomValue<T>()
        {
            var array = Enum.GetValues(typeof(T));
            return (T)array.GetValue(_random.Next() % array.Length);
        }

        private void OnFileSelected(string path)
        {
            DecodeResultText = null;
            CommandText = null;
            ErrorText = null;
            IsDecodeSucceeded = false;

            OnPropertyChanged(nameof(IsFileSelected));
        }

        private string _decodeResult;

        public string DecodeResultText
        {
            get => _decodeResult;
            set
            {
                if (_decodeResult != value)
                {
                    _decodeResult = value;

                    OnPropertyChanged(nameof(DecodeResultText));
                }
            }
        }

        private bool _isDecodeSucceeded;

        public bool IsDecodeSucceeded
        {
            get => _isDecodeSucceeded;
            protected set
            {
                if (_isDecodeSucceeded != value)
                {
                    _isDecodeSucceeded = value;

                    OnPropertyChanged(nameof(IsDecodeSucceeded));
                }
            }
        }

        private string _commandText;

        public string CommandText
        {
            get => _commandText;
            set
            {
                if (_commandText != value)
                {
                    _commandText = value;

                    OnPropertyChanged(nameof(CommandText));
                }
            }
        }

        private string _errorText;

        public string ErrorText
        {
            get => _errorText;
            set
            {
                if (_errorText != value)
                {
                    _errorText = value;

                    OnPropertyChanged(nameof(ErrorText));
                }
            }
        }

        public bool IsFileSelected => ImageUtil.ImagePath != null;

        private ImageSource _resultSource;

        public ImageSource ResultSource
        {
            get => _resultSource;
            set
            {
                if (_resultSource != value)
                {
                    _resultSource = value;

                    OnPropertyChanged(nameof(ResultSource));
                }
            }
        }
    }
}
