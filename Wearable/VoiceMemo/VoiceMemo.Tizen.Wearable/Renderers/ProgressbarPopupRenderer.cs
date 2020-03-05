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

[assembly: Xamarin.Forms.Dependency(typeof(ProgressbarPopupRenderer))]

namespace VoiceMemo.Tizen.Wearable.Renderers
{
    class ProgressbarPopupRenderer : IProgressbarPopup, IDisposable
    {
        Popup _popUp;
        Popup _popUp2;
        ElmSharp.Layout _layout;
        ElmSharp.ProgressBar _progressbar;
        bool _isDisposed = false;
        public event EventHandler BackButtonPressed;
        public event EventHandler TimedOut;

        public ProgressbarPopupRenderer()
        {
            _popUp = new Popup(Forms.NativeParent)
            {
                Style = "circle",
                Orientation = PopupOrientation.Center,
            };
            _popUp.BackButtonPressed += BackButtonPressedHandler;
            _popUp.TimedOut += ProgressbarPopup_TimedOutHandler;
            _popUp.Dismissed += _popUp_Dismissed;
            _popUp.ShowAnimationFinished += _popUp_ShowAnimationFinished;

            _layout = new ElmSharp.Layout(_popUp);
            _layout.SetTheme("layout", "popup", "content/circle");
            _popUp.SetContent(_layout);

            _progressbar = new ElmSharp.ProgressBar(Forms.NativeParent)
            {
                Color = ElmSharp.Color.FromRgb(77, 207, 255),
                //BackgroundColor = Color.FromRgb(100, 255, 0),
                //SpanSize = 50,
                Style = "process",
                IsPulseMode = true,
            };
            _progressbar.Deleted += _progressbar_Deleted;
            _popUp.SetPartContent("elm.swallow.progress", _progressbar);

            ///////////////
            _popUp2 = new Popup(Forms.NativeParent)
            {
                Style = "toast/circle/check",
                Orientation = PopupOrientation.Bottom,
                //Timeout = Duration,
            };
            //_popUp2.SetPartText("elm.text", Text);
            _popUp2.TimedOut += TimedOutHandler;
            _popUp2.Dismissed += _popUp2_Dismissed;
        }

        private void _popUp_ShowAnimationFinished(object sender, EventArgs e)
        {
            Console.WriteLine("[ProgressbarPopupRenderer._popUp_ShowAnimationFinished] ");
        }

        private void _popUp_Resized(object sender, EventArgs e)
        {
            Console.WriteLine("[ProgressbarPopupRenderer._popUp_Resized] ");
        }

        private void _popUp_RenderPost(object sender, EventArgs e)
        {
            Console.WriteLine("[ProgressbarPopupRenderer._popUp_RenderPost] ");
        }

        private void _popUp_Moved(object sender, EventArgs e)
        {
            Console.WriteLine("[ProgressbarPopupRenderer._popUp_Moved] ");
        }

        private void _popUp2_Dismissed(object sender, EventArgs e)
        {
            Console.WriteLine("[ProgressbarPopupRenderer._popUp2_Dismissed] ");
        }

        private void _popUp_Dismissed(object sender, EventArgs e)
        {
            Console.WriteLine("[ProgressbarPopupRenderer._popUp_Dismissed] ");
        }

        /// <summary>
        /// Invoked when progressbar is removed from Popup
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">EventArgs</param>
        private void _progressbar_Deleted(object sender, EventArgs e)
        {
            Console.WriteLine("[ProgressbarPopupRenderer._progressbar_Deleted] ");
            _popUp2.Show();
        }

        private void ProgressbarPopup_TimedOutHandler(object sender, EventArgs e)
        {
            Console.WriteLine("[ProgressbarPopupRenderer.ProgressbarPopup_TimedOutHandler] ");
            // Remove Progressbar from Popup when doing progressbar is done
            _popUp.SetPartContent("elm.swallow.progress", null);
        }

        private void TimedOutHandler(object sender, EventArgs e)
        {
            Console.WriteLine("[ProgressbarPopupRenderer.TimedOutHandler] 0");
            TimedOut?.Invoke(this, EventArgs.Empty);
            _popUp.Dismiss();
        }

        private void BackButtonPressedHandler(object sender, EventArgs e)
        {
            Console.WriteLine("[ProgressbarPopupRenderer.BackButtonPressedHandler] ");
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
                    Console.WriteLine("[Update] Text (" + _Text + ")");
                    _popUp2.SetPartText("elm.text", _Text);
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
                    Console.WriteLine("[Update] Duration (" + Duration + ")");
                    _popUp2.Timeout = _Duration;
                }
            }
        }

        string _ProgressbarText;
        public string ProgressbarText
        {
            get
            {
                return _ProgressbarText;
            }

            set
            {
                if (_ProgressbarText != value)
                {
                    _ProgressbarText = value;
                    Console.WriteLine("[Update & Apply] ProgressbarText (" + _ProgressbarText + ")");
                    _layout.SetPartText("elm.text", _ProgressbarText);
                }
            }
        }

        double _ProgressbarDuration;
        public double ProgressbarDuration
        {
            get
            {
                return _ProgressbarDuration;
            }

            set
            {
                if (_ProgressbarDuration != value)
                {
                    _ProgressbarDuration = value;
                    Console.WriteLine("[Update & Apply] ProgressbarDuration (" + _ProgressbarDuration + ")");
                    _popUp.Timeout = _ProgressbarDuration;
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

            Console.WriteLine("[ProgressbarPopupRenderer.Dispose] ");
            if (disposing)
            {
                if (_popUp != null)
                {
                    _layout.Unrealize();
                    _layout = null;
                    _popUp.BackButtonPressed -= BackButtonPressedHandler;
                    _popUp.TimedOut -= TimedOutHandler;
                    _popUp.Unrealize();
                    _popUp = null;
                }

                if (_popUp2 != null)
                {
                    _popUp2.TimedOut -= TimedOutHandler;
                    _popUp2.Unrealize();
                    _popUp2 = null;
                }
            }

            _isDisposed = true;
        }

        public void Show()
        {
            Console.WriteLine("[ProgressbarPopupRenderer.Show] ");
            _progressbar.Value = 0;
            _popUp.Show();
            _progressbar.PlayPulse();
        }
    }
}
