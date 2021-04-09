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

using System;
using ElmSharp;
using Xamarin.Forms.Platform.Tizen;

using Alarm.Controls;
using Alarm.Tizen.Renderers;


[assembly: Xamarin.Forms.ExportRenderer(typeof(ImageButton), typeof(Alarm.Tizen.Renderers.ImageButtonRenderer))]
namespace Alarm.Tizen.Renderers
{
    /// <summary>
    /// Calculator element (Operator, numbers) button custom renderer
    /// Actually to implement command button, A image is used instead a button to display as a calculator button.
    /// </summary>
    /// <remarks>
    /// Please refer to Xamarin Custom Renderer
    /// https://developer.xamarin.com/guides/xamarin-forms/custom-renderer/
    /// </remarks>
    class ImageButtonRenderer : ImageRenderer//ViewRenderer<Xamarin.Forms.Image, Image>
    {
        /// <summary>
        /// Tizen's gesture recognizer for Tap gesture, Long Tap gesture, Line gesture and so on.
        /// </summary>
        private ElmSharp.GestureLayer GestureRecognizer;

        /// <summary>
        /// A flags that indicates whether the touch is handled or not.
        /// </summary>
        private volatile bool isTouched;

        public ImageButtonRenderer()
        {
            RegisterPropertyHandler(ImageButton.BlendColorProperty, UpdateBlendColor);
        }

        /// <summary>
        /// Making a button with a image and set the image's color and blending color by inputted background color.
        /// Register touch event callback for the Tap, the Long Tap and the Line behavior. </summary>
        /// <remarks>
        /// When the button is touched, This class should change the image for each touch down/up situation.
        /// Even a button touching  starts at the Tap touch down, but touch up will be happen in several situations such as the Tap, the Long Tap, the Line.
        /// </remarks>
        /// <param name="args"> A Image element changed event's argument </param>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Image> args)
        {
            base.OnElementChanged(args);

            if (Control == null)
            {
                return;
            }

            if (GestureRecognizer == null)
            {
                GestureRecognizer = new ElmSharp.GestureLayer(Control);
                GestureRecognizer.Attach(Control);
            }

            if (args.NewElement == null)
            {
                GestureRecognizer.ClearCallbacks();
                return;
            }
            else
            {
                Control.Clicked += SendClicked;
            }

            ImageButton BtnElement = args.NewElement as ImageButton;
            if (BtnElement == null)
            {
                return;
            }

            GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.Tap, ElmSharp.GestureLayer.GestureState.Start, x =>
            {
                KeyDown();
            });
            GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.Tap, ElmSharp.GestureLayer.GestureState.End, x =>
            {
                KeyUp();
            });
            GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.LongTap, ElmSharp.GestureLayer.GestureState.End, x =>
            {
                KeyUp();
            });
            GestureRecognizer.SetTapCallback(ElmSharp.GestureLayer.GestureType.LongTap, ElmSharp.GestureLayer.GestureState.Abort, x =>
            {
                KeyUp();
            });

        }

        /// <summary>
        /// A Action delegate which is restore button image as default
        /// and execute button's Command with CommandParameter. </summary>
        private void KeyUp()
        {
            if (Control == null)
            {
                return;
            }

            if (isTouched)
            {
                //invoke click event when a user releases image button.
                ((ImageButton)Element).SendReleased();
            }

            Control.Color = Color.Default;
            isTouched = false;
        }

        /// <summary>
        /// A Action delegate which is restore button image as pressed situation. </summary>
        private void KeyDown()
        {
            ImageButton BtnElement = Element as ImageButton;

            if (BtnElement == null ||
                Control == null)
            {
                return;
            }

            Control.Color = BtnElement.BlendColor.ToNative();
            isTouched = true;
        }

        private void UpdateBlendColor(bool obj)
        {
            ImageButton BtnElement = Element as ImageButton;
            Control.Color = BtnElement.BlendColor.ToNative();
        }

        /// <summary>
        /// To send a click event to the element
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        void SendClicked(object sender, EventArgs e)
        {
            ((ImageButton)Element).SendClicked();
        }

    }
}