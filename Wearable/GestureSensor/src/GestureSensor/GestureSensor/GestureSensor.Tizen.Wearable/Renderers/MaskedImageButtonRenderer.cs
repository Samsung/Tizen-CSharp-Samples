/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
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

﻿using GestureSensor.Tizen.Wearable.Controls;
using GestureSensor.Tizen.Wearable.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using EGestureLayer = ElmSharp.GestureLayer;

[assembly: ExportRenderer(typeof(MaskedImageButton), typeof(MaskedImageButtonRenderer))]
namespace GestureSensor.Tizen.Wearable.Renderers
{
    /// <summary>
    /// Renderer for <see cref="MaskedImageButton"/>.
    /// </summary>
    public class MaskedImageButtonRenderer : MaskedImageRenderer
    {
        private EGestureLayer GestureLayer;

        /// <summary>
        /// Attaches gesture layer to control.
        /// </summary>
        /// <param name="e">Element that changed.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            if (Control == null || Element == null)
            {
                return;
            }

            if (GestureLayer == null)
            {
                GestureLayer = new EGestureLayer(Control);
                GestureLayer.Attach(Control);
            }

            if (e.NewElement == null)
            {
                GestureLayer.ClearCallbacks();
                return;
            }

            GestureLayer.SetTapCallback(EGestureLayer.GestureType.Tap, EGestureLayer.GestureState.Start, x => KeyDown());
            GestureLayer.SetTapCallback(EGestureLayer.GestureType.Tap, EGestureLayer.GestureState.End, x => ExecuteTapCommand());
            GestureLayer.SetTapCallback(EGestureLayer.GestureType.LongTap, EGestureLayer.GestureState.End, x => ExecuteTapCommand());
        }

        private void KeyDown()
        {
            var maskedImageButton = Element as MaskedImageButton;
            maskedImageButton?.PressCommand?.Execute(null);
        }

        private void KeyUp()
        {
            var maskedImageButton = Element as MaskedImageButton;
            maskedImageButton?.ReleaseCommand?.Execute(null);
        }

        private void ExecuteTapCommand()
        {
            var maskedImageButton = Element as MaskedImageButton;
            maskedImageButton?.Command?.Execute(null);

            KeyUp();
        }
    }
}