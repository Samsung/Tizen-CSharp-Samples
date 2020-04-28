/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using AppCommon.Extensions;
using AppCommon.Tizen.Mobile.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using EPopup = ElmSharp.Popup;

[assembly: Dependency(typeof(DialogImplementation))]

namespace AppCommon.Tizen.Mobile.Renderers
{
    class DialogImplementation : IDialog, IDisposable
    {
        EPopup _control;
        View _content;
        Button _positive;
        Button _neutral;
        Button _negative;
        string _title;
        StackLayout _contentView;
        LayoutOptions _horizontalOption = LayoutOptions.Center;
        LayoutOptions _verticalOption = LayoutOptions.End;

        LayoutOptions _previousHorizontalOption = LayoutOptions.Center;

        bool _isDisposed = false;

        ElmSharp.Button _nativePositive;

        ElmSharp.Button _nativeNeutral;
        ElmSharp.Button _nativeNegative;
        ElmSharp.EvasObject _nativeContent;

        public event EventHandler Hidden;

        public event EventHandler OutsideClicked;

        public event EventHandler Shown;

        public event EventHandler BackButtonPressed;

        public DialogImplementation()
        {
            _control = new EPopup(Forms.NativeParent);

            _control.ShowAnimationFinished += ShowAnimationFinishedHandler;
            _control.Dismissed += DismissedHandler;
            _control.OutsideClicked += OutsideClickedHandler;
            _control.BackButtonPressed += BackButtonPressedHandler;

            _contentView = new StackLayout();
        }

        ~DialogImplementation()
        {
            Dispose(false);
        }

        public View Content
        {
            get
            {
                return _content;
            }

            set
            {
                _content = value;
                UpdateContent();
            }
        }

        public Button Positive
        {
            get
            {
                return _positive;
            }

            set
            {
                _positive = value;
                UpdatePositive();
            }
        }

        public Button Neutral
        {
            get
            {
                return _neutral;
            }

            set
            {
                _neutral = value;
                UpdateNeutral();
            }
        }

        public Button Negative
        {
            get
            {
                return _negative;
            }

            set
            {
                _negative = value;
                UpdateNegative();
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
                UpdateTitle();
            }
        }


        public void Show()
        {
            _control.Show();
        }

        public void Hide()
        {
            _control.Hide();
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

            if (disposing)
            {
                if (_nativePositive != null)
                {
                    _nativePositive.Unrealize();
                    _nativePositive = null;
                }

                if (_nativeNeutral != null)
                {
                    _nativeNeutral.Unrealize();
                    _nativeNeutral = null;
                }

                if (_nativeNegative != null)
                {
                    _nativeNegative.Unrealize();
                    _nativeNegative = null;
                }

                if (_nativeContent != null)
                {
                    _nativeContent.Unrealize();
                    _nativeContent = null;
                }

                if (_control != null)
                {
                    _control.ShowAnimationFinished -= ShowAnimationFinishedHandler;
                    _control.Dismissed -= DismissedHandler;
                    _control.OutsideClicked -= OutsideClickedHandler;
                    _control.BackButtonPressed -= BackButtonPressedHandler;

                    _control.Unrealize();
                    _control = null;
                }
            }

            _isDisposed = true;
        }

        void ShowAnimationFinishedHandler(object sender, EventArgs e)
        {
            _nativeContent?.MarkChanged();
            Shown?.Invoke(this, EventArgs.Empty);
        }

        void DismissedHandler(object sender, EventArgs e)
        {
            Hidden?.Invoke(this, EventArgs.Empty);
        }

        void OutsideClickedHandler(object sender, EventArgs e)
        {
            OutsideClicked?.Invoke(this, EventArgs.Empty);
        }

        void BackButtonPressedHandler(object sender, EventArgs e)
        {
            BackButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        void UpdateContent()
        {
            _contentView.Children.Clear();

            if (Content != null)
            {
                _contentView.Children.Add(Content);

                var renderer = Platform.GetOrCreateRenderer(_contentView);
                (renderer as LayoutRenderer)?.RegisterOnLayoutUpdated();

                var sizeRequest = _contentView.Measure(Forms.NativeParent.Geometry.Width, Forms.NativeParent.Geometry.Height).Request.ToPixel();

                _nativeContent = renderer.NativeView;
                _nativeContent.MinimumHeight = sizeRequest.Height;

                _control.SetPartContent("default", _nativeContent, true);
            }
            else
            {
                _control.SetPartContent("default", null, true);
            }
        }

        void UpdatePositive()
        {
            _nativePositive?.Hide();

            if (Positive != null)
            {
                _nativePositive = (ElmSharp.Button)Platform.GetOrCreateRenderer(Positive).NativeView;
                _nativePositive.Style = "popup";
            }
            else
            {
                _nativePositive = null;
            }

            _control.SetPartContent("button1", _nativePositive, true);
        }

        void UpdateNeutral()
        {
            _nativeNeutral?.Hide();

            if (Neutral != null)
            {
                _nativeNeutral = (ElmSharp.Button)Platform.GetOrCreateRenderer(Neutral).NativeView;
                _nativeNeutral.Style = "popup";
            }
            else
            {
                _nativeNeutral = null;
            }

            _control.SetPartContent("button2", _nativeNeutral, true);
        }

        void UpdateNegative()
        {
            _nativeNegative?.Hide();

            if (Negative != null)
            {
                _nativeNegative = (ElmSharp.Button)Platform.GetOrCreateRenderer(Negative).NativeView;
                _nativeNegative.Style = "popup";
            }
            else
            {
                _nativeNegative = null;
            }

            _control.SetPartContent("button3", _nativeNegative, true);
        }

        void UpdateTitle()
        {
            _control.SetPartText("title,text", Title);
        }

    }
}