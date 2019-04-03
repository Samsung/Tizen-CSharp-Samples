//Copyright 2019 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System.Threading;
using System.Threading.Tasks;
using Tizen.Multimedia;
using Xamarin.Forms;

namespace TonePlayerSample.ViewModels
{
    /// <summary>
    /// MainViewModel class for the tone player
    /// </summary>
    class MainViewModel : ViewModelBase
    {
        private CancellationTokenSource _cts = null;
        private AudioStreamPolicy _audioStreamPolicy = null;
        private const int _secUnit = 1_000;
        private bool _isPlaying = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class
        /// </summary>
        /// <param name="navigation">
        /// Navigation instance
        /// </param>
        public MainViewModel(INavigation navigation)
        {
            Navigation = navigation;

            PlayCommand = new Command(async () =>
            {
                _isPlaying = true;
                _cts = new CancellationTokenSource();
                _audioStreamPolicy = new AudioStreamPolicy(AudioStreamType.Media);

                PlayCommand.ChangeCanExecute();
                CancelCommand.ChangeCanExecute();

                try
                {
                    await TonePlayer.StartAsync(ToneType.Default, _audioStreamPolicy, Duration * _secUnit, _cts.Token);
                }
                catch (TaskCanceledException)
                {
                    Tizen.Log.Info("TonePlayer", "A task for playing media was canceled.");
                }
                finally
                {
                    _isPlaying = false;
                    _cts?.Dispose();
                    _audioStreamPolicy?.Dispose();
                }

                PlayCommand.ChangeCanExecute();
                CancelCommand.ChangeCanExecute();
            }, CanPlay);

            CancelCommand = new Command(() =>
            {
                if (_isPlaying)
                {
                    _cts?.Cancel();
                    _isPlaying = false;
                }

                PlayCommand.ChangeCanExecute();
                CancelCommand.ChangeCanExecute();
            }, CanCancel);
        }

        /// <summary>
        /// Gets or sets the duration of playing time in sec.
        /// </summary>
        /// <remarks>
        /// We use only two value in this sample. 1sec or 5sec.
        /// </remarks>        
        public static int Duration { get; set; } = 1;

        /// <summary>
        /// Gets or sets command for starting tone player
        /// </summary>
        public Command PlayCommand { get; set; }

        /// <summary>
        /// Gets or sets command for canceling current playing media
        /// </summary>
        public Command CancelCommand { get; set; }

        /// <summary>
        /// Gets the Navigation instance to push new pages properly
        /// </summary>
        public INavigation Navigation { get; }

        /// <summary>
        /// Gets whether TonePlayer can play now or not.
        /// </summary>
        /// <returns>
        /// false if TonePlayer is playing; otherwise true
        /// </returns>
        private bool CanPlay() => !_isPlaying;

        /// <summary>
        /// Gets whether TonePlayer can cancel now or not.
        /// </summary>
        /// <returns>
        /// true if TonePlayer is playing; otherwise false
        /// </returns>
        private bool CanCancel() => _isPlaying;
    }
}
