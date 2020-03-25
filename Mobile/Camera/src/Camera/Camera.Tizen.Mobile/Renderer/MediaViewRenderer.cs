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
using Camera.Tizen.Mobile.Control;
using Camera.Tizen.Mobile.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using TMediaView = Tizen.Multimedia.MediaView;

[assembly: ExportRenderer(typeof(MediaView), typeof(MediaViewRenderer))]

namespace Camera.Tizen.Mobile.Renderer
{
    /// <summary>
    /// MediaView control Tizen renderer.
    /// </summary>
    public class MediaViewRenderer : VisualElementRenderer<MediaView>
    {
        #region fields

        /// <summary>
        /// Instance of MediaView class.
        /// </summary>
        TMediaView _control;

        #endregion

        #region methods

        /// <summary>
        /// MediaViewRenderer class constructor.
        /// </summary>
        public MediaViewRenderer()
        {
        }

        /// <summary>
        /// Overrides OnElementChanged method for updating MediaView model based on event data.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<MediaView> e)
        {
            if (_control == null)
            {
                _control = new TMediaView(Forms.NativeParent);
                SetNativeView(_control);
            }

            if (e.OldElement != null)
            {
                _control.Resized -= NatvieViewResized;
            }

            if (e.NewElement != null)
            {
                _control.Resized += NatvieViewResized;
                Element.NativeView = _control;
                IMediaViewController mediaView = Element as IMediaViewController;
                mediaView?.SendNativeViewCreated();
            }

            base.OnElementChanged(e);
        }

        /// <summary>
        /// Calls NativeSizeChanged method.
        /// </summary>
        /// <param name="sender">Instance of an object which invoked the event.</param>
        /// <param name="e">Contains event data.</param>
        void NatvieViewResized(object sender, EventArgs e)
        {
            ((IMediaViewController)Element).NativeSizeChanged();
        }

        #endregion
    }
}