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
using ElmSharp;


[assembly: Dependency(typeof(ScreenMirroringController))]
namespace ScreenMirroringSample.Tizen.Mobile
{
    class ScreenMirroringController : IScreenMirroring
    {
        private const string LogTag = "Tizen.Multimedia.ScreenMirroring";
        private readonly ScreenMirroring _screenMirroring = new ScreenMirroring();
        private static Display _display;
        private Window _win;
        private bool _flag;
        private ScreenMirroringState _state;

        public ScreenMirroringController()
        {
            _screenMirroring.StateChanged += (s, e) => StateChanged?.Invoke(this, new StateEventArgs((int)e.State));
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
        public event EventHandler<StateEventArgs> StateChanged;
        

        public async Task ConnectAsync(string sourceIp)
        {
            await _screenMirroring.ConnectAsync(sourceIp);
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
                _win = new ElmSharp.Window("Overlay_Window");
                _display = new Multimedia.Display(_win);
                _screenMirroring.Prepare(_display);
            }
            catch (Exception e)
            {
                Log.Error(LogTag, "PrePare error : " + e.Message);
            }
        }

        public void Disconnect()
        {
            try
            {
                _screenMirroring.Disconnect();
            }
            catch(Exception e)
            {
                Log.Error(LogTag, e.ToString());
            }
        }
        public void Dispose()
        {
            _screenMirroring.Dispose();
            _win.Hide();

        }
        public async Task StartAsync()
        {
            try
            { 
                await _screenMirroring.StartAsync();
                _win.Show();
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
                _screenMirroring.Unprepare();
            }
            catch (Exception e)
            {
                Log.Error(LogTag, "Unprepare error : " + e.Message);
            }
        }
        public void PrintStateLog(string name)
        {
            Log.Error(LogTag, "API: " + name + "State: " + State);
        }
        public bool IsPlaying => _flag;
    }
}
