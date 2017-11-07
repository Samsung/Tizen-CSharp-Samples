/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
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

using Clock.Controls;
using Clock.Tizen.Mobile.Renderers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(MoreMenuItem), typeof(MoreMenuItemRenderer))]

namespace Clock.Tizen.Mobile.Renderers
{
    /// <summary>
    /// The Renderer class of a MoreMenuItem widget
    /// It extends LabelRenderer class.
    /// </summary>
    class MoreMenuItemRenderer : LabelRenderer
    {
        ElmSharp.GestureLayer gestureRec;

        /// <summary>
        /// Invoked whenever the element has been changed in Xamarin.
        /// </summary>
        /// <param name="e">ElementChangedEventArgs<Label></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                return;
            }

            MoreMenuItem sb = e.NewElement as MoreMenuItem;
            if (sb == null)
            {
                return;
            }

            // // Register a callback to detect tap Gesture
            gestureRec = new ElmSharp.GestureLayer(Control);
            gestureRec.Attach(Control);
            gestureRec.LongTapTimeout = 0.001;

            Action<ElmSharp.GestureLayer.TapData> tapAction = (ev) =>
            {
                sb.Command?.Execute(sb.CommandParameter);
            };
            gestureRec.SetTapCallback(ElmSharp.GestureLayer.GestureType.Tap, ElmSharp.GestureLayer.GestureState.End, tapAction);
        }
    }
}
