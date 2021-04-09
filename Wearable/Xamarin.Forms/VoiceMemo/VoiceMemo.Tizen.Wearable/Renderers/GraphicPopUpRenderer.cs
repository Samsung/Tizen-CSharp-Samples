/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using ElmSharp;
using System;
using VoiceMemo.Tizen.Wearable.Renderers;
using VoiceMemo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: Xamarin.Forms.Dependency(typeof(GraphicPopUpRenderer))]

namespace VoiceMemo.Tizen.Wearable.Renderers
{
    class GraphicPopUpRenderer : IGraphicPopup, IDisposable
    {
        Popup _popUp;
        bool _isDisposed = false;
        public event EventHandler BackButtonPressed;
        public event EventHandler TimedOut;

        public GraphicPopUpRenderer()
        {
            Console.WriteLine("GraphicPopUpRenderer()  GetHashCode:" + this.GetHashCode());
            _popUp = new Popup(Forms.NativeParent)
            {
                Style = "toast/circle",
                Orientation = PopupOrientation.Center,
                AllowEvents = true,
            };
            var path = ResourcePath.GetPath("tw_ic_popup_btn_check.png");
            var image = new ElmSharp.Image(_popUp);
            image.LoadAsync(path);
            image.Show();
            _popUp.SetPartContent("toast,icon", image);
            _popUp.BackButtonPressed += BackButtonPressedHandler;
            _popUp.TimedOut += TimedOutHandler;
            _popUp.Dismissed += _popUp_Dismissed;
        }

        private void _popUp_Dismissed(object sender, EventArgs e)
        {
            Console.WriteLine("[GraphicPopUpRenderer._popUp_Dismissed] ");
        }

        private void TimedOutHandler(object sender, EventArgs e)
        {
            Console.WriteLine("[GraphicPopUpRenderer.TimedOutHandler] ");
            TimedOut?.Invoke(this, EventArgs.Empty);
            _popUp.Dismiss();
        }

        private void BackButtonPressedHandler(object sender, EventArgs e)
        {
            Console.WriteLine("[GraphicPopUpRenderer.BackButtonPressedHandler] ");
            BackButtonPressed?.Invoke(this, EventArgs.Empty);
            _popUp.Dismiss();
        }

        string _Text;
        public string Text
        {
            get
            {
                return _Text;
            }

            set
            {
                if (_Text != value)
                {
                    _Text = value;
                    Console.WriteLine("[GraphicPopUpRenderer--Update] Text (" + Text + ")");
                    _popUp.SetPartText("elm.text", Text);
                }
            }
        }

        double _Duration;
        public double Duration
        {
            get
            {
                return _Duration;
            }

            set
            {
                if (_Duration != value)
                {
                    _Duration = value;
                    Console.WriteLine("[GraphicPopUpRenderer--Update] Duration (" + Duration + ")");
                    _popUp.Timeout = Duration;
                }
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            Console.WriteLine("[GraphicPopUpRenderer.Dispose] ");
            if (disposing)
            {
                if (_popUp != null)
                {
                    _popUp.BackButtonPressed -= BackButtonPressedHandler;
                    _popUp.TimedOut -= TimedOutHandler;
                    _popUp.Unrealize();
                    _popUp = null;
                }
            }

            _isDisposed = true;
        }

        public void Show()
        {
            Console.WriteLine("[GraphicPopUpRenderer.Show] ");
            _popUp.Show();
        }
    }
}
