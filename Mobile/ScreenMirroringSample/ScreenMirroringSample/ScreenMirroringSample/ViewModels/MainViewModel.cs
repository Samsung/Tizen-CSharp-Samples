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
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
namespace ScreenMirroringSample
{
    
    class MainViewModel : ViewModelBase
    {
        public ICommand PrepareCommand { get; protected set; }
        public ICommand StartCommand { get; protected set; }

        public MainViewModel()
        {
            ScreenMirroring.StateChanged += OnStateChanged;
            PrepareCommand = new Command(() =>
            {
                ScreenMirroring.Prepare();
            });
            StartCommand = new Command( async () =>
            {
                await ScreenMirroring.ConnectAsync(WiFiDirect.SourceIp);   
            });
        }
        private async void OnStateChanged(object sender, StateEventArgs e)
        {
            MirroringState = (ScreenMirroringState)e.State;

            switch (MirroringState)
            {
                case ScreenMirroringState.Connected:
                    await ScreenMirroring.StartAsync();

                    await Task.Delay(7000);
                    ScreenMirroring.Disconnect();
                    break;

                case ScreenMirroringState.Disconnected:
                    ScreenMirroring.Unprepare();
                    ScreenMirroring.Dispose();
                    break;
            }
        }

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
                    ScreenMirroring.State = _screenMirroringState;
                    OnPropertyChanged(nameof(MirroringState));
                }
            }
        }
        internal async Task OnDisappearing()
        {
            if(ScreenMirroring.State == ScreenMirroringState.Playing)
            { 
                ScreenMirroring.StateChanged -= OnStateChanged;
                ScreenMirroring.Disconnect();
                var ss = Task.Run(async () => {await Task.Delay(1000); });
                ss.Wait();
                ScreenMirroring.Unprepare();
                ScreenMirroring.Dispose();
            }
        }
        
    }
}
