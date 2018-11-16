/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using Xamarin.Forms;

namespace Camera.Tizen.Mobile.Control
{
    /// <summary>
    /// The MediaView class provides a view of media that is being played by the player or the camera.
    /// </summary>
    /// <example>
    /// <code>
    /// MediaView mediaView = new MediaView();
    /// Tizen.Multimedia.Camera camera = new Tizen.Multimedia.Camera(Tizen.Multimedia.CameraDevice.Rear);
    /// camera.SetDisplay(Tizen.Multimedia.CameraDisplayType.Evas, (Tizen.Multimedia.MediaView) mediaView.NativeView);
    /// </code>
    /// </example>
    public class MediaView : Xamarin.Forms.View, IMediaViewController
    {
        #region properties

        /// <summary>
        /// BindableProperty. Identifies the NativeView bindable property.
        /// </summary>
        internal static readonly BindablePropertyKey NativeViewPropertyKey =
            BindableProperty.CreateReadOnly("NativeView", typeof(object), typeof(MediaView), default(object));

        /// <summary>
        /// BindableProperty. Identifies the NativeView bindable property.
        /// </summary>
        public static readonly BindableProperty NativeViewProperty = NativeViewPropertyKey.BindableProperty;

        /// <summary>
        /// NativeView allows application developers to display the video output on screen.
        /// </summary>
        public object NativeView
        {
            get { return GetValue(NativeViewProperty); }
            internal set { SetValue(NativeViewPropertyKey, value); }
        }

        /// <summary>
        /// Occurs when the NativeView is created.
        /// </summary>
        public event EventHandler NativeViewCreated;

        #endregion

        #region methods

        /// <summary>
        /// Invokes NativeViewCreated event.
        /// </summary>
        void IMediaViewController.SendNativeViewCreated()
        {
            NativeViewCreated?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}