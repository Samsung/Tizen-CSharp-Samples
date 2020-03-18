/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using VoiceRecorder.Tizen.Mobile.Control;
using VoiceRecorder.Tizen.Mobile.Renderer;
using Xamarin.Forms;
using EPopup = ElmSharp.Popup;

[assembly: Dependency(typeof(ToastRenderer))]

namespace VoiceRecorder.Tizen.Mobile.Renderer
{
    /// <summary>
    /// Toast control Tizen renderer.
    /// </summary>
    internal class ToastRenderer : IToast, IDisposable
    {
        #region fields

        /// <summary>
        /// Default style name.
        /// </summary>
        static readonly string DefaultStyle = "toast";

        /// <summary>
        /// Default part name.
        /// </summary>
        static readonly string DefaultPart = "default";

        /// <summary>
        /// Backing field of the Duration property.
        /// </summary>
        int _duration = 3000;

        /// <summary>
        /// Backing field of the Text property.
        /// </summary>
        string _text = string.Empty;

        /// <summary>
        /// An instance of EPopup class.
        /// </summary>
        EPopup _control = null;

        /// <summary>
        /// Value indicating whether the toast is disposed or not.
        /// </summary>
        bool _isDisposed = false;

        #endregion

        #region properties

        /// <summary>
        /// Property specifying how long the toast is visible.
        /// </summary>
        public int Duration
        {
            get
            {
                return _duration;
            }

            set
            {
                _duration = value;
                UpdateDuration();
            }
        }

        /// <summary>
        /// Property specifying text displayed by the toast.
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                _text = value;
                UpdateText();
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// ToastRenderer class constructor.
        /// Creates an instance of EPopup class.
        /// Executes UpdateText and UpdateDuration methods.
        /// </summary>
        public ToastRenderer()
        {
            _control = new EPopup(Forms.NativeParent)
            {
                Style = DefaultStyle,
                AllowEvents = true,
            };

            UpdateText();
            UpdateDuration();
        }

        /// <summary>
        /// ToastRenderer class destructor.
        /// Executes Dispose method.
        /// </summary>
        ~ToastRenderer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Shows the toast.
        /// </summary>
        public void Show()
        {
            _control.Show();
        }

        /// <summary>
        /// Dismisses the toast.
        /// </summary>
        public void Dismiss()
        {
            _control.Dismiss();
        }

        /// <summary>
        /// Disposes the toast.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the toast.
        /// </summary>
        /// <param name="isDisposing">Flag indicating if the toast is already in progress of disposing.</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (isDisposing)
            {
                if (_control != null)
                {
                    _control.Unrealize();
                    _control = null;
                }
            }

            _isDisposed = true;
        }

        /// <summary>
        /// Updates the toast timeout.
        /// </summary>
        void UpdateDuration()
        {
            _control.Timeout = Duration / 1000.0;
        }

        /// <summary>
        /// Updates the toast text.
        /// </summary>
        void UpdateText()
        {
            _control.SetPartText(DefaultPart, Text);
        }

        #endregion
    }
}