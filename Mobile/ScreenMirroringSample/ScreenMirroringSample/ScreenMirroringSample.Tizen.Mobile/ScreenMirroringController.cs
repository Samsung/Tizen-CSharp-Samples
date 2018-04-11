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
using ScreenMirroringSample.Tizen.Mobile;
using Xamarin.Forms;
using Tizen.Multimedia;
using Multimedia = Tizen.Multimedia;
using Tizen.Multimedia.Remoting;
using System;
using System.Threading.Tasks;
using Tizen;
using XamarinTizen = Xamarin.Forms.Platform.Tizen;
using XamarinExt = Tizen.Xamarin.Forms.Extension;
using ElmSharp;


[assembly: Dependency(typeof(ScreenMirroringController))]
namespace ScreenMirroringSample.Tizen.Mobile
{
    class ScreenMirroringController : EventArgs, IScreenMirroring
    {
        private const string LogTag = "Tizen.Multimedia.ScreenMirroring";
        private readonly ScreenMirroring _screenMirroring = new ScreenMirroring();
        private static Display _display;
        private Window _win;
        private bool _flag = false;
        private ScreenMirroringState _state;
        public ScreenMirroringController()
        {
            Log.Error(LogTag, "start ScreenMirroringController");
            _screenMirroring.StateChanged += (s, e) => StateChanged?.Invoke(this, new StateEventArgs((int)e.State));
            this.StateChanged += OnStateChanged;
            Log.Error(LogTag, "end ScreenMirroringController");
            
        }
        public async void OnStateChanged(object sender, StateEventArgs e)
        {
            Log.Error(LogTag, "in OnStateChanged : "+e.State);
            _state = (ScreenMirroringState)e.State;

            switch (_state)
            {
                case ScreenMirroringState.Connected:
                    await StartAsync();

                    await Task.Delay(7000);
                    Disconnect();
                    break;

                case ScreenMirroringState.Disconnected:
                    Unprepare();
                    Dispose();
                    break;
            }
        }
        public bool StateFlag
        {
            get => _flag;
            set
            {
                _flag = value;
            }
        }
        public ScreenMirroringState State
        {
            get => _state;
            set
            {
                _state = value;
            }
        }
        public Object GetDisplay()
        {
            return _display;
        }
        public event EventHandler<StateEventArgs> StateChanged;

        public async Task ConnectAsync(string sourceIp)
        {
            Log.Error(LogTag, "start ConnectAsync");
            await _screenMirroring.ConnectAsync(sourceIp);
            Log.Error(LogTag, "end ConnectAsync");
        }

        public void SetDisplay(object nativeView)
        {
            Log.Error(LogTag, "start SetDisplay");
            _display = new Display(nativeView as MediaView);
        }

        public static Display Display
        {
            get => _display;
            set => _display = value;
        }
        public void Prepare()
        {
            try
            {
                Log.Error(LogTag, "start Prepare");
                _win = new Window("OverlayDisplay");
                _display = new Multimedia.Display(_win);
                if (_display == null)
                {
                    Log.Error(LogTag,"Display cannot be null!");
                }
                _screenMirroring.Prepare(_display);
                Log.Error(LogTag, "Display2::::::" + _display);
                Log.Error(LogTag, "end Prepare");
            }
            catch (Exception e)
            {
                Log.Error(LogTag, "PrePare error : " + e.Message);
            }
        }
        public void Destroy()
        {
            Log.Error(LogTag, "start Destroy");
            _screenMirroring.Dispose();
            Log.Error(LogTag, "end Destroy");

        }
        public void Dispose()
        {
            _screenMirroring.Dispose();
            _win.Hide();
        }
        public void Disconnect()
        {
            try
            {
                Log.Error(LogTag, "start Disconnects");
                _screenMirroring.Disconnect();
            }
            catch(Exception e)
            {
                Log.Error(LogTag, e.ToString());
            }
        }

        public async Task StartAsync()
        {
            try
            { 
                Log.Error(LogTag, "start StartAsync");
                await _screenMirroring.StartAsync();
                _win.Show();
                Log.Error(LogTag, "end StartAsync");
            }
            catch (Exception e)
            {
                Log.Error(LogTag, "Start error : " + e.Message);
            }
        }

        public void Unprepare()
        {
            try
            {
            Log.Error(LogTag, "start Unprepare");
            _screenMirroring.Unprepare();
            Log.Error(LogTag, "end Unprepare");
            }catch(Exception e)
            {
                Log.Error(LogTag, "Unprepare error : " + e.Message);
            }
        }

        public bool IsPlaying => _flag;
    }
}
