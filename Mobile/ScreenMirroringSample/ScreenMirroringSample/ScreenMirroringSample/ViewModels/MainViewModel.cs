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
using System.Threading.Tasks;
namespace ScreenMirroringSample
{
    
    class MainViewModel : ViewModelBase
    {
        public ICommand PrepareCommand { get; protected set; }
        public ICommand StartCommand { get; protected set; }



        public MainViewModel()
        {
            PrepareCommand = new Command(() =>
            {
                ScreenMirroring.Prepare();
            });
            StartCommand = new Command( async () =>
            {
                await ScreenMirroring.ConnectAsync(WiFiDirect.SourceIp);   
            });
        }
        public View Display { get; set; }
        protected IScreenMirroring ScreenMirroring => DependencyService.Get<IScreenMirroring>();
        protected IWiFiDirect WiFiDirect => DependencyService.Get<IWiFiDirect>();

        private ScreenMirroringState _screenMirroringState;
        public ScreenMirroringState MirroringState
        {
            get => _screenMirroringState;
            set
            {
                if (_screenMirroringState != value)
                {
                    _screenMirroringState = value;

                    OnPropertyChanged(nameof(MirroringState));
                }
            }
        }
        public object PlayerView
        {
            set
            {                       
                if (value != null)
                {
                  ScreenMirroring.SetDisplay(value);
                }
            }
        }
        public bool IsPlaying => ScreenMirroring.IsPlaying;
        private void UpdatePage()
        {
            OnPropertyChanged(nameof(IsPlaying));
        }

        internal async Task OnDisappearing()
        {
            if(ScreenMirroring.State == ScreenMirroringState.Playing)
            {
                ScreenMirroring.Disconnect();
                var timer = Task.Run(async () => { await Task.Delay(1000); });
                timer.Wait();
                ScreenMirroring.Unprepare();
                ScreenMirroring.Dispose();
            }
        }
    }
}